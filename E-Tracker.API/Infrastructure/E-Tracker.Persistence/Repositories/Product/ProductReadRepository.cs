using E_Tracker.Application.Repositories.Product;
using E_Tracker.Persistence.Contexts;

namespace E_Tracker.Persistence.Repositories.Product;

public class ProductReadRepository: ReadRepository<Domain.Entities.Product>,IProductReadRepository
{
    public ProductReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}