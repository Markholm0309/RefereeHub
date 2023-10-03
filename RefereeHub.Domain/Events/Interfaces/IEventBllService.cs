using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Events.Dtos;
using RefereeHub.Domain.Referee.Dtos;

namespace RefereeHub.Domain.Events.Interfaces;

public interface IEventBllService
{
    Task<IActionResult> GetAll();
    Task<IActionResult> GetAllEventsByReportId(int id);
    Task<IActionResult> GetById(int id);
    Task<IActionResult> GetReportIdById(int id);
    Task<IActionResult> Create(CreateEventDto dto);
    Task<IActionResult> Update(EventDto dto);
    Task<IActionResult> Delete(int id);
}