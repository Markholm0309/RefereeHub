using Mapster;
using RefereeHub.Domain.Interfaces.Repositories;
using RefereeHub.Domain.Referee.Dtos;
using RefereeHub.Domain.Referee.Interfaces;
using RefereeHub.Domain.Referee.ViewModels;

namespace RefereeHub.Application.Services.Referee;

public class RefereeService : IRefereeService
{
    private readonly IUnitOfWork _unitOfWork;

    public RefereeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<RefereeResponse>> GetAll()
    {
        var fromRepo = await _unitOfWork.Referees.GetAllAsync();
        return fromRepo.Adapt<IEnumerable<RefereeResponse>>();
    }

    public async Task<RefereeResponse> GetById(int id)
    {
        var fromRepo = await _unitOfWork.Referees.FindAsync(id);
        return fromRepo.Adapt<RefereeResponse>();    
    }

    public async Task<RefereeResponse> GetByName(string name)
    {
        var fromRepo = await _unitOfWork.Referees.GetRefereeByName(name);
        return fromRepo.Adapt<RefereeResponse>();
    }

    public async Task<int> GetIdByName(string name)
    {
        return await _unitOfWork.Referees.GetIdByName(name);
    }

    public async Task Delete(int id)
    {
        var referee = await _unitOfWork.Referees.FindAsync(id);
        _unitOfWork.Referees.Remove(referee);
    }
    
    public async Task Delete(string name)
    {
        var referee = await _unitOfWork.Referees.GetRefereeByName(name);
        _unitOfWork.Referees.Remove(referee);
    }

    public async Task Create(CreateRefereeDto dto)
    {
        var referee = new RefereeDto
        {
            FullName = dto.FullName,
            Age = dto.Age,
            CurrentLeague = dto.CurrentLeague,
            Image = dto.Image
        };
        
        await _unitOfWork.Referees.InsertAsync(referee.Adapt<Domain.Referee.Referee>());
    }

    public async void Update(UpdateRefereeDto dto)
    {
        var referee = await _unitOfWork.Referees.FindAsync(dto.Id);

        referee.FullName = dto.FullName;
        referee.Age = dto.Age;
        referee.CurrentLeague = dto.CurrentLeague;

        _unitOfWork.Referees.Update(referee);
    }

    public async Task<bool> IsExisting(string name)
    {
        return await _unitOfWork.Referees.IsExisting(name);
    }

    public async Task<int> UpdateMatchesRefereed(string state, int id)
    {
        return await _unitOfWork.Referees.UpdateMatchesRefereed(state, id);
    }
}