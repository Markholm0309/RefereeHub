using RefereeHub.Domain.Interfaces.Repositories;

namespace RefereeHub.Domain.Referee.Interfaces;

public interface IRefereeRepository : IBaseRepository<Referee>
{
    Task<Referee> GetRefereeByName(string name);
    Task<int> GetIdByName(string name);
    Task<bool> IsExisting(string name);
    Task<int> UpdateMatchesRefereed(string state, int id);

}