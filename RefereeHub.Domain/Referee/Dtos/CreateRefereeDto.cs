namespace RefereeHub.Domain.Referee.Dtos;

public class CreateRefereeDto
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public string CurrentLeague { get; set; }
    public string Image { get; set; }
}