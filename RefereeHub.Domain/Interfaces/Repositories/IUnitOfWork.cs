using RefereeHub.Domain.Events.Interfaces;
using RefereeHub.Domain.Referee.Interfaces;
using RefereeHub.Domain.Report.Interfaces;

namespace RefereeHub.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    IReportRepository Reports { get; }
    IRefereeRepository Referees { get; }
    IEventRepository Events { get; }
}