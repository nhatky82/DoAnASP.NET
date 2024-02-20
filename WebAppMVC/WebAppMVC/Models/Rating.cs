using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class Rating
	{
		public int Id { get; set; }
		public int CommentID { get; set; }
		public string Image { get; set; }
		public int SumStar { get; set; }
		public int UserID { get; set; }
		public int ProductID { get; set; }
		public bool Status { get; set; }
		[DisplayName("Sản phẩm")]
		public Product Product { get; set; }

		public User User { get; set; }
	}
}
