using Mapster;
using RefereeHub.Domain.Events.ViewModels;
using RefereeHub.Domain.Interfaces.Repositories;
using RefereeHub.Domain.Report.Dtos;
using RefereeHub.Domain.Report.Interfaces;
using RefereeHub.Domain.Report.ViewModels;

namespace RefereeHub.Application.Services.Report;

public class ReportService : IReportService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;   
    }

    public async Task<IEnumerable<ReportResponse>> GetAllReports()
    {
        var fromRepo = await _unitOfWork.Reports.GetAllReports();
        return fromRepo
            .Select(report => new ReportResponse
            {
                Id = report.Id,
                Title = report.Title, 
                TimeCreated = report.TimeCreated, 
                Referee = report.Referee.FullName, 
                Events = report.Events.Adapt<List<EventResponse>>(),
                Rating = report.Rating
            }).ToList();
    }

    public async Task<IEnumerable<ReportResponse>> GetAllFromRefereeId(int id)
    {
        var fromRepo = await _unitOfWork.Reports.GetReportsByRefereeId(id);
        return fromRepo
            .Select(report => _unitOfWork.Reports.GetReportById(report.Id))
            .Select(reportFromRepo => new ReportResponse 
            {
                Id = reportFromRepo.Result.Id,
                Title = reportFromRepo.Result.Title, 
                TimeCreated = reportFromRepo.Result.TimeCreated, 
                Referee = reportFromRepo.Result.Referee.FullName, 
                Events = reportFromRepo.Result.Events.Adapt<List<EventResponse>>(),
                Rating = reportFromRepo.Result.Rating
            }).ToList();
    }

    public async Task<IEnumerable<ReportResponse>> GetAllFromRefereeName(string name)
    {
        var fromRepo = await _unitOfWork.Reports.GetReportsByRefereeName(name);
        return fromRepo
            .Select(report => _unitOfWork.Reports.GetReportById(report.Id))
            .Select(reportFromRepo => new ReportResponse 
            {
                Id = reportFromRepo.Result.Id,
                Title = reportFromRepo.Result.Title, 
                TimeCreated = reportFromRepo.Result.TimeCreated, 
                Referee = reportFromRepo.Result.Referee.FullName, 
                Events = reportFromRepo.Result.Events.Adapt<List<EventResponse>>(),
                Rating = reportFromRepo.Result.Rating
            }).ToList();
    }

    public async Task<ReportResponse> GetById(int id)
    {
        var fromRepo = await _unitOfWork.Reports.FindAsync(id);
        return fromRepo.Adapt<ReportResponse>();
    }

    public async Task<int> GetIdByName(string name)
    {
        return await _unitOfWork.Reports.GetIdByName(name);
    }

    public async Task Delete(int id)
    {
        var report = await _unitOfWork.Reports.FindAsync(id);
        _unitOfWork.Reports.Remove(report);
    }

    public async Task Create(CreateReportDto report)
    {
        report.TimeCreated = DateTime.Now;
        await _unitOfWork.Reports.InsertAsync(report.Adapt<Domain.Report.Report>());
    }

    public async Task Update(UpdateReportDto reportDto)
    {
        var report = await _unitOfWork.Reports.GetReportById(reportDto.Id);
        
        report.Title = reportDto.Title;
        report.RefereeId = reportDto.RefereeId;
        report.Rating = reportDto.Rating;
        
        foreach (var eventDto in report.Events)
        {
            var reportEvent = report.Events.FirstOrDefault(c => c.Id == eventDto.Id);
            
            if (reportEvent == null)
            {
                report.Events.Add(eventDto);
            }
            else
            {
                reportEvent.Description = eventDto.Description;
                reportEvent.timeStamp = eventDto.timeStamp;
                reportEvent.ReportId = eventDto.ReportId;
            }
        }
        
        _unitOfWork.Reports.Update(report.Adapt<Domain.Report.Report>());
    }
}