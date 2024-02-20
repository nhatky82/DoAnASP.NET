using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class PaymentMethods
	{
        public int ID { get; set; }
		[DisplayName("Tên phương thức thanh toán")]
		public string Name { get; set; }
		[DisplayName("Trạng thái")]
		public bool Status { get; set; }
    }
}
