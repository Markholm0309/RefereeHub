namespace RefereeHub.Tests.Helpers;

public abstract class RefereeData
{
    public static IEnumerable<object[]> AllReferees
    {
        get
        {
            return Referees.Select(person => new object[] { person }).ToList();
        }
    }
    
    public static readonly List<Domain.Referee.Referee> Referees = new()
    {
        new Domain.Referee.Referee
        {
            FullName = "Mark",
            Age = 22,
            MatchesRefereed = 1,
            CurrentLeague = "Serie 1",
            Rating = 3,
            Image = "null"
        },
        new Domain.Referee.Referee
        {
            FullName = "Morten",
            Age = 20,
            MatchesRefereed = 1,
            CurrentLeague = "Serie 1",
            Rating = 3,
            Image = "null"
        },
    };
}