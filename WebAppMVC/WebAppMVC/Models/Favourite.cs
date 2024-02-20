using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class Favourite
	{
		public int Id { get; set; }
		public int UserID { get; set; }

		public int ProductID { get; set; }
		[DisplayName("Sản phẩm")]
		public Product Product { get; set; }
		[DisplayName("Người dùng")]
		public User User { get; set; }
	}
}
