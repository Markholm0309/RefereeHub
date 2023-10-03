using RefereeHub.Domain.Events.Dtos;
using RefereeHub.Domain.Events.ViewModels;

namespace RefereeHub.Domain.Report.ViewModels;

public class ReportResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime TimeCreated { get; set; }
    public string Referee { get; set; }
    public List<EventResponse> Events { get; set; }
    public int Rating { get; set; }
}