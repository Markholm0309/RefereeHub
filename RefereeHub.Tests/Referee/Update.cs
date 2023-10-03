using FluentAssertions;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Referee.Dtos;
using RefereeHub.Domain.Referee.ViewModels;
using RefereeHub.Tests.Helpers;

namespace RefereeHub.Tests.Referee;

public class Update : TestBase
{
    [Theory]
    [InlineData("Mark", 22, "Serie 1")]
    public async Task Update_Success(string fullName, int age, string currentLeague)
    {
        await Initialize();
        
        var entity = Controller.GetIdByName(fullName).Result;
        var ok = entity as OkObjectResult;
        var id = ok?.Value is int ? (int)(ok?.Value ?? throw new InvalidOperationException()) : 0;
        
        var request = new UpdateRefereeRequest(id, fullName, age, currentLeague);
        Controller.Update(request).Result.Should().BeEquivalentTo(GetActionResult(nameof(OkActionResult)));
    }
    
    [Theory]
    [InlineData("Mark", 22, "Superliga")]
    public async Task Update_Failure(string fullName, int age, string currentLeague)    
    {
        await Initialize();

        var referee = new UpdateRefereeRequest(99, fullName, age, currentLeague);

        RefereeBllMock.Setup(x => x
                .Update(referee.Adapt<UpdateRefereeDto>()))
            .Throws(new Exception("Error"));
        
        ControllerMock.Update(referee)
            .Result
            .Should()
            .BeEquivalentTo<CreateRefereeRequest>(null!); 
    }
}