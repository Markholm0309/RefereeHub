using Mapster;
using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Report.Dtos;
using RefereeHub.Domain.Report.Interfaces;
using RefereeHub.Domain.Report.ViewModels;

namespace RefereeHub.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportBllService _service;

    public ReportController(IReportBllService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return await _service.GetAll();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAll(int id)
    {
        try
        {
            return await _service.GetAllFromRefereeId(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetAll(string name)
    {
        try
        {
            return await _service.GetAllFromRefereeName(name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet("GetIdByName/{name}")]
    public async Task<IActionResult> GetIdByName(string name)
    {
        try
        {
            return await _service.GetIdByName(name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReportRequest request)
    {
        try
        {
            return await _service.Create(request.Adapt<CreateReportDto>());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateReportRequest request)
    {
        try
        {
            return await _service.Update(request.Adapt<UpdateReportDto>());
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