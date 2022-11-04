using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Tracker.Application.Repositories.ProductImageFile;
using E_Tracker.Persistence.Contexts;

namespace E_Tracker.Persistence.Repositories.ProductImageFile
{
    public class ProductImageFileReadRepository: ReadRepository<Domain.Entities.ProductImageFile>,IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
