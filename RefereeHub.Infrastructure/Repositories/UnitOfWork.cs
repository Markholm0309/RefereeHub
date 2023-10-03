using RefereeHub.Domain.Events.Interfaces;
using RefereeHub.Domain.Interfaces.Repositories;
using RefereeHub.Domain.Referee.Interfaces;
using RefereeHub.Domain.Report.Interfaces;
using RefereeHub.Infrastructure.Data;

namespace RefereeHub.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(
        ApplicationDbContext context, 
        IReportRepository reports, 
        IRefereeRepository referees,
        IEventRepository events)
    {
        _context = context;
        Reports = reports;
        Referees = referees;
        Events = events;
    }

    public IReportRepository Reports { get; set; }
    public IRefereeRepository Referees { get; set; }
    public IEventRepository Events { get; set; }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}