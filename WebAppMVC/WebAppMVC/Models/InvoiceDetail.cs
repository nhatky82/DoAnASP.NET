using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class InvoiceDetail
	{
        public int Id { get; set; }
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
		[DisplayName("Số lượng sản phẩm")]
		public int Quantity { get; set; }
		[DisplayName("Gía tiền")]
		public int Price { get; set; }
		[DisplayName("Sản phẩm")]
		public Product Product { get; set; }
		[DisplayName("Hóa đơn")]
		public Invoice Invoice { get; set; }
	}
}
