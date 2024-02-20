using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models
{
	public class User
	{
		public int Id { get; set; }
		[DisplayName("Tên người dùng")]
		public string UserName { get; set; }
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Số điện thoại")]
        [RegularExpression(@"0\d{9}", ErrorMessage = "SĐT không hợp lệ")]
        public string Phone { get; set; }
        
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("Tên đầy đủ")]
        public string FullName { get; set; }
        [DisplayName("Trạng thái ADMIN")]
        public bool IsAdmin { get; set; }
        [DisplayName("Giao diện người dùng")]
        public string Avatar { get; set; }
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }

	
	}
}
