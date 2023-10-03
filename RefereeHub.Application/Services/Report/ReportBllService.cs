using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RefereeHub.Application.Hubs;
using RefereeHub.Domain.Interfaces.Repositories;
using RefereeHub.Domain.Report.Dtos;
using RefereeHub.Domain.Report.Interfaces;

namespace RefereeHub.Application.Services.Report;

public class ReportBllService : ControllerBase, IReportBllService
{
    private readonly IHubContext<ReportHub> _hub;
    private readonly IReportService _reportService;
    private readonly IUnitOfWork _unitOfWork;

    public ReportBllService(IUnitOfWork unitOfWork,
        IReportService reportService,
        IHubContext<ReportHub> hub)
    {
        _unitOfWork = unitOfWork;
        _reportService = reportService;
        _hub = hub;
    }

    public async Task<IActionResult> GetAll()
    {
        return Ok(await _reportService.GetAllReports());
    }

    public async Task<IActionResult> GetAllFromRefereeId(int id)
    {
        return Ok(await _reportService.GetAllFromRefereeId(id));
    }

    public async Task<IActionResult> GetAllFromRefereeName(string name)
    {
        return Ok(await _reportService.GetAllFromRefereeName(name));
    }

    public async Task<IActionResult> GetIdByName(string name)
    {
        return Ok(await _reportService.GetIdByName(name));
    }

    public async Task<IActionResult> Create(CreateReportDto report)
    {
        await _reportService.Create(report);

        if (await _unitOfWork.SaveChangesAsync() <= 0) return BadRequest();
        await _hub.Clients.All.SendAsync("reportsUpdated", _reportService.GetAllReports());
        return Ok(report);
    }

    public async Task<IActionResult> Update(UpdateReportDto reportDto)
    {
        await _reportService.Update(reportDto);
        return await _unitOfWork.SaveChangesAsync() > 0
            ? Ok(reportDto)
            : BadRequest();
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _reportService.Delete(id);
        return await _unitOfWork.SaveChangesAsync() > 0
            ? Ok()
            : BadRequest();
    }

    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _reportService.GetById(id));
    }
}