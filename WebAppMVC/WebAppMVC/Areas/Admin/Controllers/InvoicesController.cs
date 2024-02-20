using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("adminInvoices")]
    public class InvoicesController : Controller
    {
        private readonly WebAppMVCContext _context;
        public InvoicesController(WebAppMVCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var invoice = _context.Invoices.Include(i => i.User);
            return View(invoice.ToList());
        }
        [Route("Details")]
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoices
                .Include(i => i.User)
                .FirstOrDefault(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Password");
            return View();
        }
        [HttpPost("Create")]
        public IActionResult Create([Bind("Id,Code,InvoiceDate,UserID,PaymentMethodsID,ShippingAddress,ShippingPhone,VoucherID,TotalPrice,status")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Password", invoice.UserID);
            return View(invoice);
        }
        //[HttpPost("Edit")]
        //public  IActionResult Edit(int? id)
        //{
        //    if (id == null || _context.Invoices == null)
        //    {
        //        return NotFound();
        //    }

        //    var invoice =  _context.Invoices.Find(id);
        //    if (invoice == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Password", invoice.UserID);
        //    return View(invoice);
        //}
        //[HttpPost("Edit")]
        //public  IActionResult Edit(int? id, [Bind("Id,Code,InvoiceDate,UserID,PaymentMethodsID,ShippingAddress,ShippingPhone,VoucherID,TotalPrice,status")] Invoice invoice)
        //{
        //    if (id != invoice.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(invoice);
        //             _context.SaveChanges();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!InvoiceExists(invoice.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Password", invoice.UserID);
        //    return View(invoice);
        //}
        [HttpGet("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoices
                .Include(i => i.User)
                .FirstOrDefault(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }
        [HttpPost("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'WebAppMVCContext.Invoices'  is null.");
            }
            var invoice = _context.Invoices.Find(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }

        [HttpGet("ExportExcel")]
        public IActionResult ExportExcel()
        {
            var invoices = _context.Invoices.ToList();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Invoices");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Code";
                worksheet.Cell(1, 3).Value = "IvnoiceDate";
                worksheet.Cell(1, 4).Value = "UserId";
                worksheet.Cell(1, 5).Value = "PaymentMethods";
                worksheet.Cell(1, 6).Value = "ShippingAddress";
                worksheet.Cell(1, 7).Value = "ShippingPhone";
                worksheet.Cell(1, 8).Value = "VoucherId";
                worksheet.Cell(1, 9).Value = "TotalPrice";
                worksheet.Cell(1, 10).Value = "status";

                foreach (var invoice in invoices)
                {
                    worksheet.Cell(invoice.Id + 1, 1).Value = invoice.Id;
                    worksheet.Cell(invoice.Id + 1, 2).Value = invoice.Code;
                    worksheet.Cell(invoice.Id + 1, 3).Value = invoice.InvoiceDate;
                    worksheet.Cell(invoice.Id + 1, 4).Value = invoice.UserID;
                    worksheet.Cell(invoice.Id + 1, 5).Value = invoice.PaymentMethodsID;
                    worksheet.Cell(invoice.Id + 1, 6).Value = invoice.ShippingAddress;
                    worksheet.Cell(invoice.Id + 1, 7).Value = invoice.ShippingPhone;
                    worksheet.Cell(invoice.Id + 1, 8).Value = invoice.VoucherID;
                    worksheet.Cell(invoice.Id + 1, 9).Value = invoice.TotalPrice;
                    worksheet.Cell(invoice.Id + 1, 10).Value = invoice.status;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string filename = string.Format("invoices.xlsx");
                    return File(content, contentType, filename);
                }
            }
        }
    }
}
