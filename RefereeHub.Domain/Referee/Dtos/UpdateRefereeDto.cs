namespace RefereeHub.Domain.Referee.Dtos;

public class UpdateRefereeDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public string CurrentLeague { get; set; }
    public int MatchesRefereed { get; set; }

    public UpdateRefereeDto(int id, string fullName, int age, string currentLeague, int matchesRefereed)
    {
        this.Id = id;
        this.FullName = fullName;
        this.Age = age;
        this.CurrentLeague = currentLeague;
        this.MatchesRefereed = matchesRefereed;
    }
}