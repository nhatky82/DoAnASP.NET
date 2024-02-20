using WebAppMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Drawing;

namespace WebAppMVC.Data
{
    public class WebAppMVCContext:DbContext
    {
        public WebAppMVCContext(DbContextOptions<WebAppMVCContext> options) : base(options)

            { }
            
            public DbSet<User> Users { get; set; }
		    public DbSet<Product> Products { get; set; }
		    public DbSet<ProductType> ProductTypes { get; set; }
            public DbSet<Cart> Carts { get; set; }
            public DbSet<Combo> Combos { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<DetailCombo> DetailCombos { get; set; }
            public DbSet<Favourite> Favourites { get; set; }
            public DbSet<Invoice> Invoices { get; set; }
            public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
            public DbSet<PaymentMethods> PaymentMethods { get; set; }
            public DbSet<ProductVoucher> ProductVouchers { get; set; }
            public DbSet<Rating> Ratings { get; set; }
            public DbSet<Voucher> Vouchers { get; set; }
            
            public DbSet<Img> Imgs { get; set; }
    }
    
}
