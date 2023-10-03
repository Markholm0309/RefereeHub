    using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Events.Dtos;
using RefereeHub.Domain.Events.Interfaces;

namespace RefereeHub.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventBllService _service;

    public EventController(IEventBllService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEventsFromReport(int id)
    {
        try
        {
            return await _service.GetAllEventsByReportId(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("GetReportId/{id:int}")]
    public async Task<IActionResult> GetReportIdByEventId(int id)
    {
        try
        {
            return await _service.GetReportIdById(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateEvent(EventDto dto)
    {
        try
        {
            return await _service.Update(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventDto dto)
    {
        try
        {
            return await _service.Create(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _service.Delete(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}