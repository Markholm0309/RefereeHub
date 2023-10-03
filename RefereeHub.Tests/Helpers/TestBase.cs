using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;
using RefereeHub.Application.Services.Referee;
using RefereeHub.Domain.Referee.Interfaces;
using RefereeHub.Infrastructure.Data;
using RefereeHub.Infrastructure.Repositories;
using RefereeHub.Infrastructure.Repositories.Events;
using RefereeHub.Infrastructure.Repositories.Referees;
using RefereeHub.Infrastructure.Repositories.Reports;
using RefereeHub.Presentation.Controllers;

namespace RefereeHub.Tests.Helpers;

public class TestBase : ControllerBase
{
    protected static readonly IActionResult OkActionResult = new OkResult();
    protected static readonly IActionResult BadActionResult = new BadRequestResult();

    protected ApplicationDbContext ApplicationDbContext;

    protected RefereeController Controller { get; private set; }
    protected RefereeController ControllerMock { get; private set; }
    protected Mock<IRefereeBllService> RefereeBllMock { get; private set; }

    protected async Task Initialize()
    {
        ApplicationDbContext = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        await ApplicationDbContext.Referees.AddRangeAsync(RefereeData.Referees);
        await ApplicationDbContext.SaveChangesAsync();

        var unitOfWork = new UnitOfWork(
            ApplicationDbContext,
            new ReportRepository(ApplicationDbContext),
            new RefereeRepository(ApplicationDbContext),
            new EventRepository(ApplicationDbContext));

        Controller = new RefereeController(new RefereeBllService(unitOfWork, new RefereeService(unitOfWork),
            new Mock<IHubContext<Application.Hubs.RefereeHub>>().Object));

        RefereeBllMock = new Mock<IRefereeBllService>();
        ControllerMock = new RefereeController(RefereeBllMock.Object);
    }

    protected static object GetActionResult(string expectedResult)
    {
        return expectedResult switch
        {
            nameof(OkActionResult) => OkActionResult,
            nameof(BadActionResult) => BadActionResult,

            _ => throw new ArgumentOutOfRangeException(nameof(expectedResult), expectedResult, null)
        };
    }
}