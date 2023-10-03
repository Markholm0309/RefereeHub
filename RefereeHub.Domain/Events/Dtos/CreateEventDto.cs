namespace RefereeHub.Domain.Events.Dtos;

public class CreateEventDto
{
    public string Description { get; set; }
    public string timeStamp { get; set; }
    public int ReportId { get; set; }
}