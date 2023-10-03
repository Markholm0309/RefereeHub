using Microsoft.EntityFrameworkCore;
using RefereeHub.Domain.Report;
using RefereeHub.Domain.Report.Interfaces;
using RefereeHub.Infrastructure.Data;

namespace RefereeHub.Infrastructure.Repositories.Reports;

public class ReportRepository : BaseRepository<Report>, IReportRepository
{
    private readonly ApplicationDbContext _context;

    public ReportRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Report>> GetAllReports()
    {
        return await _context.Reports
            .Include(r => r.Referee)
            .Include(x => x.Events)
            .ToListAsync();
    }

    public async Task<IEnumerable<Report>> GetReportsByRefereeId(int id)
    {
        return await _context.Reports
            .Where(x => x.Referee.Id == id)
            .Include(r => r.Referee)
            .Include(e => e.Events)
            .ToListAsync();
    }

    public async Task<IEnumerable<Report>> GetReportsByRefereeName(string name)
    {
        return await _context.Reports
            .Where(x => x.Referee.FullName == name)
            .Include(r => r.Referee)
            .Include(e => e.Events)
            .ToListAsync();
    }

    public async Task<Report> GetReportById(int id)
    {
        var entity = await _context.Reports
            .Include(e => e.Events)
            .SingleOrDefaultAsync(p => p.Id == id) ?? throw new InvalidOperationException("Error");

        _context.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    public async Task<int> GetIdByName(string name)
    {
        var entity = await _context.Reports.FirstOrDefaultAsync(x => x.Title == name);
        return entity != null ? entity.Id : throw new ArgumentException($"No reports with that name - {name}");
    }
}