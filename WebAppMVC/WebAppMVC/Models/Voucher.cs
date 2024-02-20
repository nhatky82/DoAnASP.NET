using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class Voucher
	{
		public int Id { get; set; }

		[DisplayName("Tên voucher")]
		public string Name { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Số lượng")]
        public int  Quantity { get; set;}

        public int Reduce { get; set;}

        [DisplayName("Ngày bắt đầu dùng voucher")]
        public DateTime StartDate { get; set; }

        [DisplayName("Ngày kết thức voucher")]
        public DateTime EndDate { get; set; }
	}
}
