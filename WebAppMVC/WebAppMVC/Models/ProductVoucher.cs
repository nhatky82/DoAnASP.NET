using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class ProductVoucher
	{
		public int Id { get; set; }
		public int ProductID { get; set; }
		public int VoucherID { get; set; }
		[DisplayName("Sản phẩm")]
		public Product Product { get; set; }
		[DisplayName("Mã giảm giá")]
		public Voucher Voucher { get; set; }

	}
}
