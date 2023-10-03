using Mapster;
using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Events.Dtos;
using RefereeHub.Domain.Referee.Dtos;
using RefereeHub.Domain.Referee.Interfaces;
using RefereeHub.Domain.Referee.ViewModels;
using RefereeHub.Domain.Report.ViewModels;

namespace RefereeHub.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RefereeController : ControllerBase
{
    private readonly IRefereeBllService _service;

    public RefereeController(IRefereeBllService service)
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
    
    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            return await _service.GetById(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        try
        {
            return await _service.GetByName(name);
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
    public async Task<IActionResult> Create(CreateRefereeRequest request)
    {
        try
        {
            return await _service.Create(request.Adapt<CreateRefereeDto>());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPatch]
    public async Task<IActionResult> Update(UpdateRefereeRequest request)
    {
        try
        {
            return await _service.Update(request.Adapt<UpdateRefereeDto>());
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
    
    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        try
        {
            return await _service.Delete(name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet("exists/{name}")]
    public async Task<IActionResult> Exists(string name)
    {
        try
        {
            return await _service.IsExisting(name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}