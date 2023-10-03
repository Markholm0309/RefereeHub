using RefereeHub.Domain.Referee.Dtos;
using RefereeHub.Domain.Referee.ViewModels;

namespace RefereeHub.Domain.Referee.Interfaces;

public interface IRefereeService
{
    Task<IEnumerable<RefereeResponse>> GetAll();
    Task<RefereeResponse> GetById(int id);
    Task<RefereeResponse> GetByName(string name);
    Task<int> GetIdByName(string name);
    Task Delete(int id);
    Task Delete(string name);
    Task Create(CreateRefereeDto dto);
    void Update(UpdateRefereeDto dto);
    Task<bool> IsExisting(string name);
    Task<int> UpdateMatchesRefereed(string state, int id);
}