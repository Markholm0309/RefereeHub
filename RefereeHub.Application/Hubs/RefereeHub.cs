using Microsoft.AspNetCore.SignalR;
using RefereeHub.Domain.Referee.Interfaces;

namespace RefereeHub.Application.Hubs;

public class RefereeHub : Hub
{
    private readonly IRefereeBllService _bllService;
    private readonly IRefereeService _service;

    public RefereeHub(IRefereeBllService bllService, IRefereeService service)
    {
        _bllService = bllService;
        _service = service;
    }
}