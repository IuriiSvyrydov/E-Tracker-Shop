using System.Linq.Expressions;
using E_Tracker.Application.Repositories;
using E_Tracker.Domain.Entities.Common;
using E_Tracker.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace E_Tracker.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public ReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter, bool tracking = true)
    {
      var  query = Table.Where(filter);
      if (!tracking)
          query = query.AsNoTracking();
      return query;
    }


    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking(); 
        return  await query.FirstOrDefaultAsync(filter);
    }

    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));

    }
}
  