using RefereeHub.Domain.Events.Dtos;

namespace RefereeHub.Domain.Report.Dtos;

public class ReportDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime TimeCreated { get; set; }
    public int RefereeId { get; set; }
    public List<EventDto> Events { get; set; }
    public int Rating { get; set; }
}