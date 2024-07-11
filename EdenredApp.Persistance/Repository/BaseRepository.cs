using EdenredApp.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EdenredApp.Persistance.Repository;


public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;
    
    public BaseRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync()
        =>  await _dbSet.ToListAsync();

    public async Task<T> GetByIdAsync(int id)
        =>  await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}