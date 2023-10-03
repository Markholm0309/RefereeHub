using RefereeHub.Domain.Interfaces.Repositories;

namespace RefereeHub.Domain.Events.Interfaces;

public interface IEventRepository : IBaseRepository<Event>
{
    Task<IEnumerable<Event>> GetAllEventsFromReport(int id);
    Task<int> GetReportId(int eventId);
}