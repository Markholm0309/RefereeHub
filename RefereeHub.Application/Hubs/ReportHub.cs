using Microsoft.AspNetCore.SignalR;
using RefereeHub.Domain.Referee.Interfaces;
using RefereeHub.Domain.Report.Interfaces;

namespace RefereeHub.Application.Hubs;

public class ReportHub : Hub
{
    private readonly IReportBllService _bllService;
    private readonly IReportService _service;

    public ReportHub(IReportBllService bllService, IReportService service)
    {
        _bllService = bllService;
        _service = service;
    }
}