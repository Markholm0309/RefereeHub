using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Events.Dtos;
using RefereeHub.Domain.Events.Interfaces;
using RefereeHub.Domain.Interfaces.Repositories;

namespace RefereeHub.Application.Services.Event;

public class EventBllService : ControllerBase, IEventBllService
{
    private readonly IEventService _eventService;
    private readonly IUnitOfWork _unitOfWork;

    public EventBllService(IEventService eventService, IUnitOfWork unitOfWork)
    {
        _eventService = eventService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _eventService.GetAllEvents());
    }

    public async Task<IActionResult> GetAllEventsByReportId(int id)
    {
        return Ok(await _eventService.GetAllEventsFromReport(id));
    }

    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _eventService.GetById(id));
    }

    public async Task<IActionResult> GetReportIdById(int id)
    {
        return Ok(await _eventService.GetReportIdByEventId(id));
    }

    public async Task<IActionResult> Create(CreateEventDto dto)
    {
        await _eventService.Create(dto);
        return await _unitOfWork.SaveChangesAsync() > 0
            ? Ok(dto)
            : BadRequest();
    }

    public async Task<IActionResult> Update(EventDto dto)
    {
        _eventService.Update(dto);
        return await _unitOfWork.SaveChangesAsync() > 0
            ? Ok(dto)
            : BadRequest();
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _eventService.Delete(id);
        return await _unitOfWork.SaveChangesAsync() > 0
            ? Ok()
            : BadRequest();
    }
}