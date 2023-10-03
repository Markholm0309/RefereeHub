using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Referee.ViewModels;
using RefereeHub.Tests.Helpers;

namespace RefereeHub.Tests.Referee;

public sealed class Get : TestBase
{
    public static IEnumerable<object[]> Referees => RefereeData.AllReferees;

    [Fact]
    public async Task GetAllReferees()
    {
        await Initialize();

        var data = new List<RefereeResponse>
        {
            new()
            {
                FullName = "Mark",
                Age = 22,
                MatchesRefereed = 1,
                CurrentLeague = "Serie 1",
                Rating = 3,
                Image = "null"
            },
            new()
            {
                FullName = "Morten",
                Age = 20,
                MatchesRefereed = 1,
                CurrentLeague = "Serie 1",
                Rating = 3,
                Image = "null"
            }
        };

        Controller.GetAll().Result.Should().BeEquivalentTo(new OkObjectResult(data));
    }

    [Fact]
    public async Task GetAllAccounts_Failure()
    {
        await Initialize();

        var data = new List<string>
        {
            "1", "2", "3"
        };

        Controller.GetAll().Result.Should().NotBeEquivalentTo(new OkObjectResult(data));
    }

    [Theory]
    [MemberData(nameof(Referees))]
    public async Task GetByName_Success(Domain.Referee.Referee referee)
    {
        await Initialize();
        Controller.GetByName(referee.FullName).Result
            .Should()
            .BeEquivalentTo(GetActionResult(nameof(OkActionResult)));
    }
    
    [Theory]
    [InlineData("Simon")]
    [InlineData("Lærke")]
    public async Task GetByName_Failure(string fullName)
    {
        await Initialize();
        var referee = async () => await Controller.GetByName(fullName);
        await referee.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Name: {fullName} doesnt match any referees in the database");

    }   
}