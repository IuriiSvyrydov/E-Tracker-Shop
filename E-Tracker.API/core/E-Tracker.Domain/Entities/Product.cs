
using E_Tracker.Domain.Entities.Common;

namespace E_Tracker.Domain.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        public ICollection<Order> Orders { get; set; } 
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }

        public Product()
        {
            Orders = new HashSet<Order>();
            ProductImageFiles = new HashSet<ProductImageFile>();
        }

    }
}
