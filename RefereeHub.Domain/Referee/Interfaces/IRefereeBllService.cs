using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Referee.Dtos;
using RefereeHub.Domain.Report.Dtos;

namespace RefereeHub.Domain.Referee.Interfaces;

public interface IRefereeBllService
{
    Task<IActionResult> GetAll();
    Task<IActionResult> GetById(int id);
    Task<IActionResult> GetByName(string name);
    Task<IActionResult> GetIdByName(string name);
    Task<IActionResult> Create(CreateRefereeDto dto);
    Task<IActionResult> Update(UpdateRefereeDto dto);
    Task<IActionResult> Delete(int id);
    Task<IActionResult> Delete(string name);
    Task<IActionResult> IsExisting(string name);
    Task<IActionResult> UpdateMatchesRefereed(string state, int id);

}