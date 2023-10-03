using Microsoft.EntityFrameworkCore;
using RefereeHub.Domain.Events;
using RefereeHub.Domain.Events.Interfaces;
using RefereeHub.Infrastructure.Data;

namespace RefereeHub.Infrastructure.Repositories.Events;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllEventsFromReport(int id)
    {
        return await _context.Events.Where(x => x.Id == id).ToListAsync();
    }

    public async Task<int> GetReportId(int eventId)
    {
        var entity = await _context.Events.FindAsync(eventId);
        return entity?.ReportId ?? throw new ArgumentException("No events with that id");
    }
}