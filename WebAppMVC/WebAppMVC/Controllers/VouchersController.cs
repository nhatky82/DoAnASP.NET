using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Data;

namespace WebAppMVC.Controllers
{
    public class VouchersController : Controller
    {
        public WebAppMVCContext _context;
        public VouchersController(WebAppMVCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CodeVoucher(string codeVoucher)
        {
            int? UserId = HttpContext.Session.GetInt32("Id");
            double? Total = HttpContext.Session.GetInt32("Total");

            double doubleTotal = (double)Total;
            double TotalView = 0;
            if (UserId != null)
            {

                var voucher = _context.Vouchers.FirstOrDefault(a => a.Name == codeVoucher);
                if (voucher == null)
                {

                    HttpContext.Session.SetString("Error", "Mã Không tồn tại!");
                    return RedirectToAction("Index", "Invoices");
                }
                else
                {
                    var voucherId = _context.Vouchers.FirstOrDefault(a => a.Name == codeVoucher).Id;
                    double reduce = _context.Vouchers.FirstOrDefault(a => a.Name == codeVoucher).Reduce;

                    TotalView = doubleTotal - doubleTotal * (reduce / 100);
                    TotalView -= doubleTotal;
                    HttpContext.Session.SetInt32("Reduce", (int)TotalView);
                    HttpContext.Session.SetInt32("VoucherId", voucherId);
                }
                
            }
            return RedirectToAction("Index", "Invoices");

        }
    }
}
