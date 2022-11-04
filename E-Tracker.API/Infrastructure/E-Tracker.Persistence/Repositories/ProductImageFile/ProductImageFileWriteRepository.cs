using E_Tracker.Application.Repositories.ProductImageFile;
using E_Tracker.Persistence.Contexts;

namespace E_Tracker.Persistence.Repositories.ProductImageFile
{
    public class ProductImageFileWriteRepository: WriteRepository<Domain.Entities.ProductImageFile>,IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
