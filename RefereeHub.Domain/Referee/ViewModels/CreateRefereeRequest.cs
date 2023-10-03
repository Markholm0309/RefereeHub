namespace RefereeHub.Domain.Referee.ViewModels;

public class CreateRefereeRequest
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public string CurrentLeague { get; set; }
}