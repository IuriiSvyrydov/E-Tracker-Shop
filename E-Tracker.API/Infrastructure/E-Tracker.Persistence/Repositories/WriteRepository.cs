using E_Tracker.Application.Repositories;
using E_Tracker.Domain.Entities.Common;
using E_Tracker.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E_Tracker.Persistence.Repositories;

public class WriteRepository<T>: IWriteRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public WriteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T entity)
    {
       EntityEntry<T> entityState =  await Table.AddAsync(entity);
       return entityState.State == EntityState.Added;
    } 


    public async Task<bool> AddRangeAsync(List<T> entity)
    {
        await Table.AddRangeAsync(entity);
        return true;
    }

    public  bool UpdateAsync(T entity)
    {
        EntityEntry<T> entityState =  Table.Update(entity);
        return entityState.State == EntityState.Modified;
    }

    public  bool RemoveAsync(T entity)
    {
        EntityEntry<T> entry = Table.Remove(entity);
        return entry.State == EntityState.Deleted;

    }

    public bool RemoveRangeAsync(List<T> model)
    {
         Table.RemoveRange(model);
         return true;
    }

    public async Task<int> SaveChanges() => await _context.SaveChangesAsync();
  

    public async Task<bool> RemoveAsync(string id)
    {
         T entity = await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
         return RemoveAsync(entity);
    }
}