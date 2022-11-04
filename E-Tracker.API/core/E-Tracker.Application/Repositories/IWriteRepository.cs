using E_Tracker.Domain.Entities.Common;

namespace E_Tracker.Application.Repositories;

public interface IWriteRepository<T>: IRepository<T>where T:BaseEntity
{
    Task<bool>AddAsync(T entity);
    Task<bool> AddRangeAsync(List<T> entity);
    bool UpdateAsync(T entity);
    bool RemoveAsync(T entity);
    bool RemoveRangeAsync(List<T> model);
    Task<int> SaveChanges();
    Task<bool> RemoveAsync(string id);
}