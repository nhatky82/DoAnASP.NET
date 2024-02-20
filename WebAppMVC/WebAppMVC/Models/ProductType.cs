using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class ProductType
	{
        public int Id { get; set; }

		[DisplayName("Tên loại sản phẩm")]
		public string Name { get; set; }

        public string Status { get; set; }
    }
}
