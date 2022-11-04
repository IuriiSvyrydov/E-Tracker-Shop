using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Tracker.Application.Repositories;
using E_Tracker.Application.Repositories.Customer;
using E_Tracker.Persistence.Contexts;

namespace E_Tracker.Persistence.Repositories.Customer
{
    public class CustomerWriteRepository: WriteRepository<Domain.Entities.Customer>,IWriteCustomerRepository
    {
        public CustomerWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
