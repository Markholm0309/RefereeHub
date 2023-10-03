using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Referee.ViewModels;
using RefereeHub.Tests.Helpers;

namespace RefereeHub.Tests.Referee;

public class Delete : TestBase
{
    [Fact]
    public async Task Delete_Success()
    { 
        await Initialize();
        
        var entity = Controller.GetIdByName("Mark").Result;
        var ok = entity as OkObjectResult;
        var id = ok?.Value is int ? (int)(ok?.Value ?? throw new InvalidOperationException()) : 0;

        Controller.Delete(id)
            .Result
            .Should()
            .BeEquivalentTo(GetActionResult(nameof(OkActionResult)));
    }

    [Fact]
    public async Task Delete_Failure()
    {
        await Initialize();
        Controller.Delete(13).Should().NotBeEquivalentTo(GetActionResult(nameof(OkActionResult)));
    }
}