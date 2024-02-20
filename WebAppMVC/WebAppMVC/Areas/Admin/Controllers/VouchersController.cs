using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("adminVouchers")]
    public class VouchersController : Controller
    {
        private readonly WebAppMVCContext _context;
        public VouchersController(WebAppMVCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Vouchers.ToList());
        }
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create([Bind("Id,Name,Description,Quantity,StartDate,EndDate")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voucher);
                _context.SaveChanges();
                return RedirectToAction("Create");
            }
            return View(voucher);
        }
    }
}
