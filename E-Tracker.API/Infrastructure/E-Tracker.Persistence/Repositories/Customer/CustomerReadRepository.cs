using System.Linq.Expressions;
using E_Tracker.Application.Repositories.Customer;
using E_Tracker.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace E_Tracker.Persistence.Repositories.Customer;

public class CustomerReadRepository:ReadRepository<Domain.Entities.Customer>,IReadCustomerRepository
{
    public CustomerReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}