using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class Combo
	{
		public int Id { get; set; }

        [DisplayName("Tên Combo ")]
        public string Name { get; set; }
        [DisplayName("Mô tả ")]
        public string Description { get; set; }
        [DisplayName("Giá tiền")]
        public int Price { get; set; }
        [DisplayName("Số lượng ")]
        public int Quantity { get; set; }

	}
}
