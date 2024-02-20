namespace WebAppMVC.Models
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
