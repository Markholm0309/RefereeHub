namespace RefereeHub.Domain.Events;

public class Event
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string timeStamp { get; set; }
    public int ReportId { get; set; }
    public Report.Report Report { get; set; }
}


