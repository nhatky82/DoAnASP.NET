using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMVC.Data;
using WebAppMVC.Models;
using WebAppMVC.Models.Authentication;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace WebAppMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("HomeAdmin")]
    public class HomeAdminController : Controller
    {
        private readonly WebAppMVCContext _context;

        public HomeAdminController(WebAppMVCContext context)
        {
            _context = context;
        }
        //[Authentication]
        [Route("Dashboard")]
        public IActionResult Index()
        {
            //var data = _context.Products
            //    .GroupBy(b => b.ProductTypeID)
            //    .Select(group => new { ProductTypeID = group.Key, Count = group.Count() })
            //    .ToList();

            // Truyền dữ liệu cho view
            //ViewBag.ChartData = data;

            return View();
        }
    }
}
