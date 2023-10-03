using System.ComponentModel.DataAnnotations;
using RefereeHub.Domain.Events.Dtos;

namespace RefereeHub.Domain.Report.ViewModels;

public class CreateReportRequest
{
    [Required] public string Title { get; set; }
    [Required] public int RefereeId { get; set; }
    [Required] public List<CreateEventDto> Events { get; set; }
    [Required] public int Rating { get; set; }
}