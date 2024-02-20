using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models
{
	public class Invoice
	{
        public int Id { get; set; }
		public string Code { get; set; }

		[DisplayName("Hiệu lực hóa đơn")]
		public DateTime InvoiceDate{ get; set; }
		public int UserID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên của bạn.")]
        public int PaymentMethodsID { get; set; }

		[DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên của bạn.")]
        public string Name { get; set; }

		[DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ của bạn.")]
        public string ShippingAddress { get; set; }

		[DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện của bạn.")]
        public int ShippingPhone { get; set; }

        public int VoucherID { get; set; }
		[DisplayName("Tổng tiền ")]
		public int TotalPrice { get; set; }

		[DisplayName("Trạng thái đơn hàng")]
		public bool status { get; set; }

		[DisplayName("Phương thức thanh toán")]
		public PaymentMethods PaymentMethods { get; set; }

		[DisplayName("Người dùng")]
		public User User { get; set; }

		[DisplayName("Mã giảm giá")]
		public Voucher Voucher { get; set; }
    }
}
