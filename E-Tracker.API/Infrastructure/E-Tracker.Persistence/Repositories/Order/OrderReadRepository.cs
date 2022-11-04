using E_Tracker.Application.Repositories.Order;
using E_Tracker.Persistence.Contexts;

namespace E_Tracker.Persistence.Repositories.Order;

public class OrderReadRepository: ReadRepository<Domain.Entities.Order>,IOrderReadRepository
{
    public OrderReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}