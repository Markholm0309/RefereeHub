using FluentAssertions;
using Mapster;
using RefereeHub.Domain.Referee.Dtos;
using RefereeHub.Domain.Referee.ViewModels;
using RefereeHub.Tests.Helpers;

namespace RefereeHub.Tests.Referee;

public class Create : TestBase
{
    public static IEnumerable<object[]> Referees => RefereeData.AllReferees;

    [Theory]
    [MemberData(nameof(Referees))]
    public async Task Create_Success(Domain.Referee.Referee referee)
    {
        await Initialize();

        var request = new CreateRefereeRequest
        {
            FullName = referee.FullName,
            Age = referee.Age,
            CurrentLeague = referee.CurrentLeague
        };
            
        Controller.Create(request)
            .Result
            .Should()
            .BeEquivalentTo(GetActionResult(nameof(OkActionResult)));
    }
    
    [Theory]
    [MemberData(nameof(Referees))]
    public async Task Create_Failure(Domain.Referee.Referee referee)
    {
        await Initialize();
        
        var request = new CreateRefereeRequest()
        {
            FullName = referee.FullName,
            Age = referee.Age,
            CurrentLeague = referee.CurrentLeague
        };

        RefereeBllMock.Setup(x => x
            .Create(request.Adapt<CreateRefereeDto>()))
            .Throws(new Exception("Error"));
        
        ControllerMock.Create(request)
            .Result
            .Should()
            .BeEquivalentTo<CreateRefereeRequest>(null!);    
    }
}