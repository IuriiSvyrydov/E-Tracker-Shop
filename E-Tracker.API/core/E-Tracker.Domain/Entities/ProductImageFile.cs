namespace E_Tracker.Domain.Entities
{
    public class ProductImageFile: File
    {
        public ICollection<Product> Products { get; set; }

        public ProductImageFile()
        {
            Products = new HashSet<Product>();
        }
    }
}
