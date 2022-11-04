using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using E_Tracker.Domain.Entities;

namespace E_Tracker.Application.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
    }
}
