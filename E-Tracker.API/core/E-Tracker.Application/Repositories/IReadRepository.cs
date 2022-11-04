using System.Linq.Expressions;
using E_Tracker.Domain.Entities.Common;

namespace E_Tracker.Application.Repositories;

public interface IReadRepository<T>: IRepository<T>where  T : BaseEntity
{
    IQueryable<T> GetAll(bool tracking = true);
    IQueryable<T> GetWhere(Expression<Func<T,bool>>filter, bool tracking = true);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true);
    Task<T> GetByIdAsync(string id,bool tracking = true);

}