using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;
using WebAppMVC.Data;
using WebAppMVC.Migrations;
using WebAppMVC.Models;
using WebAppMVC.Models.ViewModels;


namespace WebAppMVC.Controllers
{
    public class InvoicesController : Controller
    {
        public WebAppMVCContext _context;
        public InvoicesController(WebAppMVCContext context)
        {
            _context = context;
        }

        

        public IActionResult index()
        {
            int? userId = HttpContext.Session.GetInt32("Id");
          
            List<Cart> carts = _context.Carts.Include(c => c.Product).Where(c => c.UserID == userId).ToList();
            var ViewModel = new InvoiceViewModel
            {
                Carts = carts
            };

            var total = HttpContext.Session.GetInt32("Total");
            ViewBag.Total = total;

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

            // Kiểm tra mã giảm giá có tồn tại không
            ViewBag.Error = HttpContext.Session.GetString("Error");

            return View(ViewModel);
        }


        public IActionResult _Partial_Voucher()
        {
            var total = HttpContext.Session.GetInt32("Total");
            ViewBag.Total = total;

            var reduce = HttpContext.Session.GetInt32("Reduce");
            ViewBag.Reduce = reduce;

            return PartialView();
        }

        public IActionResult CheckOut(string Name, string ShippingAddress, int ShippingPhone, int pay, int vouchersId, int total)
        {
            if (ModelState.IsValid)
            {
                int? UserId = HttpContext.Session.GetInt32("Id");
                if (UserId != null)
                {
                    var carts = _context.Carts.Include(c => c.Product).Where(c => c.UserID == UserId);

                    // Lấy mã ngẫu nhiên 9 kí tự
                    var random = new Random();
                    var code = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 9)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());

                    // Lưu hóa đơn
                    Invoice invoice = new Invoice()
                    {
                        Code = code,
                        InvoiceDate = DateTime.Now,
                        UserID = (int)UserId,
                        PaymentMethodsID = pay,
                        VoucherID = vouchersId,

                        Name = Name,
                        ShippingAddress = ShippingAddress,
                        ShippingPhone = ShippingPhone,
                        TotalPrice = total,

                        status = true
                    };

                    _context.Invoices.Add(invoice);
                    _context.SaveChanges();

                    // Cập nhật vouchers
                    var voucher = _context.Vouchers.FirstOrDefault(c => c.Id == vouchersId);
                    voucher.Quantity -= 1;
                    _context.Vouchers.Update(voucher);

                    _context.SaveChanges();

                    // Lưu chi tiết hóa đơn
                    foreach (var item in carts)
                    {
                        InvoiceDetail detail = new InvoiceDetail()
                        {

                            InvoiceID = invoice.Id,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            Price = item.Product.Price,

                        };

                        item.Product.Quantity -= item.Quantity;
                        _context.InvoiceDetails.Add(detail);
                        _context.Products.Update(item.Product);
                        _context.Carts.Remove(item);

                    }
                    _context.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Login", "Users");
                }

                return RedirectToAction("Index", "Carts");
            }

            return RedirectToAction("Index", "Invoices");
        }

        //public IActionResult MenuOrder(int orderId)
        //{
        //    int? userId = HttpContext.Session.GetInt32("Id");
        //    if(userId != null)
        //    {
        //        var invoice = _context.Invoices.Where(a => a.UserID == userId);



        //        var invoiceDetail = _context.InvoiceDetails.Where(a => a.InvoiceID == );



        //        var ViewModel = new InvoiceViewModel
        //        {
        //            Invoice = invoice,

        //        };
        //    }

        //    return View();
        //}


        //public IActionResult Detail()
        //{
        //    int? userId = HttpContext.Session.GetInt32("Id");

        //    var invoice = _context.Invoices.FirstOrDefault(a => a.UserID == userId);
        //    if (invoice != null)
        //    {
        //        var ViewModel = new InvoiceViewModel
        //        {
        //            Invoice = invoice,

        //        };
        //        return View(ViewModel);
        //    }
            
        //    return View();
        //}
       
    }
}
