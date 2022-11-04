using E_Tracker.Domain.Entities.Common;

namespace E_Tracker.Domain.Entities
{
    public class Order: BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public ICollection<Product> Products { get; set; }
        public Customer Customer { get; set; }

        public Order()
        {
            Products = new HashSet<Product>();
        }

    }
}
