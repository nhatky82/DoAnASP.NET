using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class Img
	{
        public int Id { get; set; }
		[DisplayName("Đường dẫn")]
		public string URL { get; set; }
		public int ProductID { get; set; }
		[DisplayName("Sản phẩm")]
		public Product Product { get; set; }
    }
}
