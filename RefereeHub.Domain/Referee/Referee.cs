namespace RefereeHub.Domain.Referee;

public class Referee
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public int MatchesRefereed { get; set; }
    public string CurrentLeague { get; set; }
    public int Rating { get; set; }
    public string Image { get; set; }
}