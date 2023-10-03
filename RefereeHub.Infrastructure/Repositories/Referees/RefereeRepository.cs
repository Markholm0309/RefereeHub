using Microsoft.EntityFrameworkCore;
using RefereeHub.Domain.Referee;
using RefereeHub.Domain.Referee.Interfaces;
using RefereeHub.Infrastructure.Data;

namespace RefereeHub.Infrastructure.Repositories.Referees;

public class RefereeRepository : BaseRepository<Referee>, IRefereeRepository
{
    private readonly ApplicationDbContext _context;

    public RefereeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<Referee> GetRefereeByName(string name)
    {
        return await _context.Referees.SingleOrDefaultAsync(r => r.FullName.ToLower() == name.ToLower())
               ?? throw new InvalidOperationException($"Name: {name} doesnt match any referees in the database");
    }

    public async Task<int> GetIdByName(string name)
    {
        var entity = await _context.Referees.FirstOrDefaultAsync(x => x.FullName == name);
        return entity != null ? entity.Id : throw new ArgumentException($"No referees with that name - {name}");
    }

    public async Task<bool> IsExisting(string name)
    {
        var entity = await _context.Referees.FirstOrDefaultAsync(x => x.FullName == name);
        return entity != null;
    }

    public async Task<int> UpdateMatchesRefereed(string state, int id)
    {
        var entity = await _context.Referees.FirstOrDefaultAsync(x => x.Id == id);
        
        if (entity == null) return 0;

        return state switch
        {
            "delete" => entity.MatchesRefereed--,
            "create" => entity.MatchesRefereed++,
            _ => 0
        };
    }
}