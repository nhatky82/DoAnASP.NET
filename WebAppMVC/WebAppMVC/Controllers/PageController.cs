using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
	public class PageController : Controller
	{
        private readonly WebAppMVCContext _context;

        public PageController(WebAppMVCContext context)
        {
            _context = context;
        }
        public IActionResult Index(int productType)
		{
            List<Product> products = _context.Products.ToList();
            return View(products);
        }
	
        public IActionResult Nhancach()
		{
            List<Product> products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult Toan()
		{
            List<Product> products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult Van()
		{
            List<Product> products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult Tieuthuyet()
		{
            List<Product> products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult ProductCategory(int Id)
        {
            var listProduct=_context.Products.Where(n=>n.ProductTypeID==Id).ToList();
            return View(listProduct);
        }
	}
}
