using Microsoft.EntityFrameworkCore;
using PersonalAssistantApi.Domain.Entities;
using PersonalAssistantApi.Domain.Repositories;
using PersonalAssistantApi.Infrastructure.Persistence;

namespace PersonalAssistantApi.Infrastructure.Repositories;

public class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : Entity
{
    protected readonly AppDbContext _context = context;

    public async Task<TEntity?> GetByIdAsync(Guid id) => 
        await _context.Set<TEntity>().FindAsync(id);

    public async Task<IEnumerable<TEntity>> GetAllAsync() => 
        await _context.Set<TEntity>().AsNoTracking().ToListAsync();

    public async Task AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
