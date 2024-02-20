using System.ComponentModel;

namespace WebAppMVC.Models
{
	public class DetailCombo
	{
        public int Id { get; set; }

		public int ProductID { get; set; }
		
		public int comboID { get; set; }
        [DisplayName("Trạng thái")]
        public bool Status { get; set;}

		public Product Product { get; set; }

		public Combo Combo { get; set; }
	}
    
}
