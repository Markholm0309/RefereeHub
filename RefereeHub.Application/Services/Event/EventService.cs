using Mapster;
using RefereeHub.Domain.Events.Dtos;
using RefereeHub.Domain.Events.Interfaces;
using RefereeHub.Domain.Interfaces.Repositories;

namespace RefereeHub.Application.Services.Event;

public class EventService : IEventService
{
    private readonly IUnitOfWork _unitOfWork;

    public EventService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<EventDto>> GetAllEvents()
    {
        var fromRepo = await _unitOfWork.Events.GetAllAsync();
        return fromRepo.Adapt<IEnumerable<EventDto>>();
    }

    public async Task<IEnumerable<EventDto>> GetAllEventsFromReport(int id)
    {
        var fromRepo = await _unitOfWork.Events.GetAllEventsFromReport(id);
        return fromRepo.Adapt<IEnumerable<EventDto>>();    
    }

    public async Task<EventDto> GetById(int id)
    {
        var fromRepo = await _unitOfWork.Events.FindAsync(id);
        return fromRepo.Adapt<EventDto>();
    }

    public async Task<int> GetReportIdByEventId(int id)
    {
        return await _unitOfWork.Events.GetReportId(id);
    }

    public async Task Delete(int id)
    {
        var reportEvent = await _unitOfWork.Events.FindAsync(id);
        _unitOfWork.Events.Remove(reportEvent);   
    }

    public async Task Create(CreateEventDto eventDto)
    {
        await _unitOfWork.Events.InsertAsync(eventDto.Adapt<Domain.Events.Event>());
    }

    public void Update(EventDto eventDto)
    {
        _unitOfWork.Events.Update(eventDto.Adapt<Domain.Events.Event>());
    }
}