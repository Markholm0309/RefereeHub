using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RefereeHub.Domain.Interfaces.Repositories;
using RefereeHub.Domain.Referee.Dtos;
using RefereeHub.Domain.Referee.Interfaces;

namespace RefereeHub.Application.Services.Referee;

public class RefereeBllService : ControllerBase, IRefereeBllService
{
    private readonly IRefereeService _service;
    private readonly IHubContext<Hubs.RefereeHub> _hub;
    private readonly IUnitOfWork _unitOfWork;

    public RefereeBllService(IUnitOfWork unitOfWork, IRefereeService service, IHubContext<Hubs.RefereeHub> hub)
    {
        _unitOfWork = unitOfWork;
        _service = service;
        _hub = hub;
    }

    public async Task<IActionResult> GetAll()
    {
        var referees = await _service.GetAll();

        foreach (var referee in referees)
        {
            var reports = await _unitOfWork.Reports.GetReportsByRefereeName(referee.FullName);
            var enumerable = reports.ToList();
            if (enumerable.Any())
            {
                var ratings = enumerable.Select(report => report.Rating).ToList();
                var averageRating = ratings.Average();
                var roundedAverage = Math.Ceiling(averageRating);
                referee.Rating = (int)roundedAverage;
            }
            else
            {
                referee.Rating = 0;
            }
        }

        return Ok(referees);
    }

    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetById(id));
    }

    public async Task<IActionResult> GetByName(string name)
    {
        var referee = await _service.GetByName(name);
        var reports = await _unitOfWork.Reports.GetReportsByRefereeName(name);
        var enumerable = reports.ToList();

        if (enumerable.Any())
        {
            var ratings = enumerable.Select(report => report.Rating).ToList();
            var averageRating = ratings.Average();
            var roundedAverage = Math.Ceiling(averageRating);
            referee.Rating = (int)roundedAverage;
        }
        else
        {
            referee.Rating = 0;
        }

        return Ok(referee);
    }

    public async Task<IActionResult> GetIdByName(string name)
    {
        return Ok(await _service.GetIdByName(name));
    }

    public async Task<IActionResult> Create(CreateRefereeDto dto)
    {
        dto.Image = GenerateRandomAvatar();
        await _service.Create(dto);
        if (await _unitOfWork.SaveChangesAsync() <= 0) return BadRequest();
        await _hub.Clients.All.SendAsync("refereesUpdated", _service.GetAll());
        return Ok(dto);
    }

    public async Task<IActionResult> Update(UpdateRefereeDto dto)
    {
        _service.Update(dto);
        return await _unitOfWork.SaveChangesAsync() > 0
            ? Ok(dto)
            : BadRequest();
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return await _unitOfWork.SaveChangesAsync() > 0
            ? Ok()
            : BadRequest();
    }

    public async Task<IActionResult> Delete(string name)
    {
        await _service.Delete(name);
        if (await _unitOfWork.SaveChangesAsync() <= 0) return BadRequest();
        await _hub.Clients.All.SendAsync("refereesUpdated", _service.GetAll());
        return Ok();
    }

    public async Task<IActionResult> IsExisting(string name)
    {
        return Ok(await _service.IsExisting(name));
    }

    public async Task<IActionResult> UpdateMatchesRefereed(string state, int id)
    {
        await _service.UpdateMatchesRefereed(state, id);
        return await _unitOfWork.SaveChangesAsync() > 0
            ? Ok()
            : BadRequest();
    }

    private static string GenerateRandomAvatar()
    {
        var avatars = new List<string>
        {
            "assets/user_1.svg",
            "assets/user_2.svg",
            "assets/user_3.svg",
            "assets/user_4.svg",
            "assets/user_5.svg",
        };
        
        var random = new Random();
        var index = random.Next(0, avatars.Count);
        return avatars[index];
    }
}