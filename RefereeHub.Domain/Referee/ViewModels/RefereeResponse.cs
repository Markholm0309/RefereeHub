namespace RefereeHub.Domain.Referee.ViewModels;

public class RefereeResponse
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public int MatchesRefereed { get; set; }
    public string CurrentLeague { get; set; }
    public int Rating { get; set; }
    public string Image { get; set; }
}