using RefereeHub.Domain.Interfaces.Repositories;

namespace RefereeHub.Domain.Report.Interfaces;

public interface IReportRepository : IBaseRepository<Report>
{
    Task<IEnumerable<Report>> GetAllReports();
    Task<IEnumerable<Report>> GetReportsByRefereeId(int id);
    Task<IEnumerable<Report>> GetReportsByRefereeName(string name);
    Task<Report> GetReportById(int id);
    Task<int> GetIdByName(string name);
}