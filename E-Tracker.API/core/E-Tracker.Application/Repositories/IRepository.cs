using E_Tracker.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace E_Tracker.Application.Repositories;

public interface IRepository<T> where T : BaseEntity
{
   DbSet<T> Table { get; }
}