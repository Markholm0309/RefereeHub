using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Report.Dtos;

namespace RefereeHub.Domain.Report.Interfaces;

public interface IReportBllService
{
    Task<IActionResult> GetAll();
    Task<IActionResult> GetAllFromRefereeId(int id);
    Task<IActionResult> GetAllFromRefereeName(string name);
    Task<IActionResult> GetIdByName(string name);
    Task<IActionResult> Create(CreateReportDto report);
    Task<IActionResult> Update(UpdateReportDto reportDto);
    Task<IActionResult> Delete(int id);
}