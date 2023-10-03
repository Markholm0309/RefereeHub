using RefereeHub.Domain.Events.Dtos;

namespace RefereeHub.Domain.Events.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllEvents();
    Task<IEnumerable<EventDto>> GetAllEventsFromReport(int id);
    Task<EventDto> GetById(int id);
    Task<int> GetReportIdByEventId(int id);
    Task Delete(int id);
    Task Create(CreateEventDto eventDto);
    void Update(EventDto eventDto);
}