using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Tracker.Application.Services;
using E_Tracker.Domain.Entities;

namespace E_Tracker.Persistence.Implementing
{
    public class ProductService: IProductService
    {
        public List<Product> GetAllProducts()
            => new()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product1",
                    Stock = 10,
                    Price = 1000
                }
            };

    }
}
