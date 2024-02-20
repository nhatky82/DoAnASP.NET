namespace WebAppMVC.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public List<Cart> Carts { get; set; }

        public Invoice Invoice { get; set; }

        public InvoiceDetail InvoiceDetail { get; set; }

        public Voucher Vouchers { get; set; }
    }
}
