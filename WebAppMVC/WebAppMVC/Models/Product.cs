using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMVC.Models
{
	public class Product
	{
        [DisplayName("Kiểu bán sách")]
        public string Type { get; set; }
        
        public int Id { get; set; }

		[DisplayName("Tên sản phẩm")]
		public string Name { get; set; }

		[DisplayName("Mã lưu kho")]
		public string SKU { get; set; }

		[DisplayName("Mô tả sản phẩm")]
		public string Description { get; set; }

		[DisplayName("Tác giả")]
		public string Publisher { get; set; }

		[DisplayName("Gía tiền")]
		public int Price { get; set; }

		[DisplayName("Số lượng sản phẩm")]
		public int Quantity { get; set; }

        [DisplayName("Ảnh mih họa")]
        public string Image { get; set; }

        [NotMapped]
        [DisplayName("Ảnh minh họa")]
        public IFormFile ImageFile { get; set; }
        [DisplayName("Loại sách")]
        public int ProductTypeID { get; set; }
		[DisplayName("Trạng thái sản phẩm")]
		public bool Status { get; set; }

		[DisplayName("Đường dẫn download ebook")]
		public string LinkEbook { get; set; }
        [DisplayName("Loại sản phẩm")]
        public ProductType ProductType { get; set; }
		
	}
}
