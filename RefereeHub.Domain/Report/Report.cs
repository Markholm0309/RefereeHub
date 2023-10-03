
using RefereeHub.Domain.Events;

namespace RefereeHub.Domain.Report;

public class Report
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime TimeCreated { get; set; } 
    public Referee.Referee Referee { get; set; }
    public int RefereeId { get; set; }
    public List<Event> Events { get; set; }
    public int Rating { get; set; }
}