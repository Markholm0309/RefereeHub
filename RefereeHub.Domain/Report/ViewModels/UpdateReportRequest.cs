using RefereeHub.Domain.Events.Dtos;

namespace RefereeHub.Domain.Report.ViewModels;

public class UpdateReportRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int RefereeId { get; set; }
    public List<EventDto> Events { get; set; }
    public int Rating { get; set; }
}