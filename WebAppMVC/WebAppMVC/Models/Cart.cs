using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class Cart
	{
		public int Id { get; set; }
		public int UserID { get; set; }
		public int ProductID { get; set; }
		[DisplayName("Số lượng sản phẩm")]
		public int Quantity { get; set; }
		[DisplayName("Sản phẩm")]
		public Product Product { get; set; }
		[DisplayName("Tên User")]
		public User User  { get; set; }

	}
}
