

using E_Tracker.Application.Repositories.Product;
using E_Tracker.Persistence.Contexts;

namespace E_Tracker.Persistence.Repositories.Product ;
public class ProductWriteRepository :WriteRepository<Domain.Entities.Product>,IProductWriteRepository
    {
        public ProductWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

