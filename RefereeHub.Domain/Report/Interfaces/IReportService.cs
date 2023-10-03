using RefereeHub.Domain.Report.Dtos;
using RefereeHub.Domain.Report.ViewModels;

namespace RefereeHub.Domain.Report.Interfaces;

public interface IReportService
{
    Task<IEnumerable<ReportResponse>> GetAllReports();
    Task<IEnumerable<ReportResponse>> GetAllFromRefereeId(int id);
    Task<IEnumerable<ReportResponse>> GetAllFromRefereeName(string name);
    Task<ReportResponse> GetById(int id);
    Task<int> GetIdByName(string name);
    Task Delete(int id);
    Task Create(CreateReportDto report);
    Task Update(UpdateReportDto reportDto);
}