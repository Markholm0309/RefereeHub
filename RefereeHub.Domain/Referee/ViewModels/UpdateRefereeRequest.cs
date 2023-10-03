namespace RefereeHub.Domain.Referee.ViewModels;

public class UpdateRefereeRequest
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public string CurrentLeague { get; set; }

    public UpdateRefereeRequest(int id, string fullName, int age, string currentLeague)
    {
        Id = id;
        FullName = fullName;
        Age = age;
        CurrentLeague = currentLeague;
    }
}