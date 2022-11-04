using E_Tracker.Domain.Entities.Common;

namespace E_Tracker.Domain.Entities
{
    public class Customer: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new HashSet<Order>();
        }
    }
    
}
