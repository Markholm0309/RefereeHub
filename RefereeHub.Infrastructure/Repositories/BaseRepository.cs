using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RefereeHub.Domain.Interfaces;
using RefereeHub.Domain.Interfaces.Repositories;
using RefereeHub.Infrastructure.Data;

namespace RefereeHub.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T: class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _entity = _context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entity.ToListAsync();
    }
    
    public async Task<T> FindAsync(params object[] keyValues)
    {
        var entity = await _entity.FindAsync(keyValues);
        
        if (entity == null)
        {
            throw new Exception("Null");
        }
        
        return entity;    
    }

    public T Find(params object[] keyValues)
    {
        var entity = _entity.Find(keyValues);

        if (entity == null)
        {
            throw new Exception("Null");
        }

        return entity;
    }

    // public async Task<T> FindAsync(params object[] keyValues)
    // {
    //     var entity = await _entity.FindAsync(keyValues);
    //     
    //     if (entity == null)
    //     {
    //         throw new Exception("Null");
    //     }
    //     
    //     return entity;    
    // }

    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entity.SingleOrDefaultAsync(predicate!);
    }

    public async Task InsertAsync(T entity)
    {
        await _context.AddAsync(entity);
    }

    public async Task InsertRangeAsync(IEnumerable entities)
    {
        await _context.AddRangeAsync(entities);
    }

    public void InsertRange(IEnumerable entities)
    {
        _context.AddRange(entities);
    }

    public void Update(T entity)
    {
        _entity.Update(entity);
    }
    
    public void Remove(int id)
    {
        var entity = _entity.Find(id);
        if (entity != null) _entity.Remove(entity);
    }
    
    public void Remove(T entity)
    {
        _entity.Remove(entity);
    }
    
    public Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        var enumerable = entities as T[] ?? entities.ToArray();
        
        if (enumerable.Any())
        { 
            _entity.RemoveRange(enumerable);
        }

        return Task.CompletedTask;
    }
}