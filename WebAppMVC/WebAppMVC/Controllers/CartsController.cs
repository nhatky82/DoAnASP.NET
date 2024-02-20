using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class CartsController : Controller
    {
        public WebAppMVCContext _context;
        public CartsController(WebAppMVCContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            int? UserId = HttpContext.Session.GetInt32("Id");
            if (UserId != null)
            {
                var cart = _context.Carts.Include(c => c.Product).Where(c => c.UserID == UserId);

                var total = 0;
                foreach (var item in cart)
                {
                    total += item.Quantity * item.Product.Price;
                }
                HttpContext.Session.SetInt32("Total", total);

                return View(cart);
            }


            var reduce = HttpContext.Session.GetInt32("Reduce");
            // Lấy Id voucher 
            var voucher = HttpContext.Session.GetInt32("VoucherId");
            if (reduce != null)
            {
                ViewBag.Reduce = reduce;
                ViewBag.VoucherId = voucher;
            }
            else
            {
                ViewBag.Reduce = 0;
            }



            return RedirectToAction("Login", "Users");
        }

        public IActionResult Add(int id, int quantity = 1)
        {
            int? UserId = HttpContext.Session.GetInt32("Id");
            if (UserId != null)
            {
                var cart = _context.Carts.FirstOrDefault(c => c.UserID == UserId && c.ProductID == id);
                if (cart != null)
                {
                    cart.Quantity += quantity;
                    _context.Carts.Update(cart);
                }
                else
                {
                    cart = new Cart()
                    {
                        UserID = (int)UserId,
                        ProductID = id,
                        Quantity = quantity
                    };
                    _context.Carts.Add(cart);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("Login", "Users");
        }

        public IActionResult Decrease(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("Id");
            if (UserId != null)
            {
                var cart = _context.Carts.FirstOrDefault(c => c.UserID == UserId && c.ProductID == id);
                if (cart.Quantity > 1)
                {
                    --cart.Quantity;
                    _context.Carts.Update(cart);
                }
                else
                {
                    _context.Carts.Remove(cart);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Increase(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("Id");
            if (UserId != null)
            {
                var cart = _context.Carts.FirstOrDefault(c => c.UserID == UserId && c.ProductID == id);
                if (cart.Quantity >= 1)
                {
                    ++cart.Quantity;
                    _context.Carts.Update(cart);
                }
                else
                {
                    _context.Carts.Remove(cart);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Close(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("Id");
            if (UserId != null)
            {
                var cart = _context.Carts.FirstOrDefault(c => c.UserID == UserId && c.ProductID == id);
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Listcart()
        {
            int? UserId = HttpContext.Session.GetInt32("Id");
            var cart = _context.Carts.Include(c => c.Product).Where(c => c.UserID == UserId);
            return PartialView("_ListCart", cart);

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
            return RedirectToAction("Index", "Carts");

        }


    }
}
