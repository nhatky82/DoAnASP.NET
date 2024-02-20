using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly WebAppMVCContext _context;

        public ProductsController(WebAppMVCContext context)
        {
            _context = context;
        }
        public IActionResult Detail(int id)
        {
            var products = _context.Products.Find(id);
            return View(products);  
        }
    }
}