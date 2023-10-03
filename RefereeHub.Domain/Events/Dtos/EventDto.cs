using System.ComponentModel.DataAnnotations;

namespace RefereeHub.Domain.Events.Dtos;

public class EventDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string timeStamp { get; set; }
    public int ReportId { get; set; }
}