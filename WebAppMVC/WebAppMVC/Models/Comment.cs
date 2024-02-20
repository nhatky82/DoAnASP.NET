using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMVC.Models
{
	public class Comment
	{
		public int Id { get; set; }

		[DisplayName("Nội dung bình luận")]	
		public string Content { get; set; }
		[DisplayName("Ngày bình luận")]
		public DateTime CommentDate { get; set; }

		[ForeignKey("ParentCommentID" )]

        public int? ParentCommentID { get; set; }

		public int UserID { get; set; }

		public int ProductID { get; set; }
		[DisplayName("Trạng thái bình luận")]
		public bool Status { get; set; }

		[DisplayName("Comment Cha")]
		public Comment ParentComment { get; set; }
		[DisplayName("Sản phẩm")]
		public Product Product { get; set; }
    }
}
