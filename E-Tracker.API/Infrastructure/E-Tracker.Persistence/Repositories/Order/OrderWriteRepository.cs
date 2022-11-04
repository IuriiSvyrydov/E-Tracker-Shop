using E_Tracker.Application.Repositories.Order;
using E_Tracker.Persistence.Contexts;

namespace E_Tracker.Persistence.Repositories.Order;

public class OrderWriteRepository: WriteRepository<Domain.Entities.Order>,IOrderWriteRepository
{
    public OrderWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}