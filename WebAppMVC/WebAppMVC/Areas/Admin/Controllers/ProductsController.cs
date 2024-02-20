using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMVC.Data;
using WebAppMVC.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using ClosedXML.Excel;
using System.Linq;


namespace WebAppMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("adminProduct")]
    public class ProductsController : Controller
    {
        private readonly WebAppMVCContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductsController(WebAppMVCContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [Route("Product")]
        public IActionResult Products(int p = 1, int pageSize = 8)
        {
            var products = _context.Products.Skip((p - 1) * pageSize).Take(pageSize);
            ViewBag.PageCount = Math.Floor(_context.Products.Count() / (double)pageSize) + 1;
            return View(products);
        }
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name");
            return View();
        }
        [HttpPost("Create")]
        public IActionResult Create([Bind("Id,Type,Name,SKU,Description,Publisher,Price,Quantity,ImageFile,ProductTypeID,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Image = "";
                _context.Products.Add(product);
                _context.SaveChanges();

                if (product.ImageFile != null)
                {
                    if (!product.ImageFile.ContentType.StartsWith("image"))
                    {
                        ViewBag.ErrorMessage = "Chỉ được upload file ảnh";
                        ViewBag.ProducrTypes = new SelectList(_context.ProductTypes, "Id", "Name");
                        return View(product);
                    }
                    if (product.ImageFile.Length > 2 * 1024 * 1024)
                    {
                        ViewBag.ErrorMessage = "File vượt quá dung lượng cho phép (2MB)";
                        ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name");
                        return View(product);
                    }

                    var fileName = product.Id.ToString() + Path.GetExtension(product.ImageFile.FileName);
                    var uploadFolder = Path.Combine(_environment.WebRootPath, "Images", "Product/SachTamly");
                    var uploadPath = Path.Combine(uploadFolder, fileName);
                    using (FileStream fs = System.IO.File.Create(uploadPath))
                    {
                        product.ImageFile.CopyTo(fs);
                        fs.Flush();
                    }
                    product.Image = fileName;
                    _context.Products.Update(product);
                    _context.SaveChanges();

                    return RedirectToAction("Product", "AdminProduct");

                }
            }
            else {

            }

            ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name");
            return View(product);
        }
        [Route("Details")]
        public IActionResult Details(int? id)
        {
            var product = _context.Products
                                  .Include(p => p.ProductType)
                                  .FirstOrDefault(p => p.Id == id);
            return View(product);
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("Edit")]
        [HttpGet("Edit")]
        public IActionResult Edit(int? id)
        {
            var product = _context.Products.Find(id);
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name", product.ProductTypeID);
            return View(product);

        }

        [HttpPost("Edit")]
        public IActionResult Edit(int? id, [Bind("Id,Type,Name,SKU,Description,Publisher,Price,Quantity,ImageFile,ProductTypeID,Status")] Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Products", "Products");
        }
        [Route("Delete")]
        [HttpGet("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = _context.Products.Include(p => p.ProductType).FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Enity set 'WebAppMVCContext.Products' is null.");
            }
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            _context.SaveChanges();
            return RedirectToAction("Product", "adminProduct");
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        [HttpGet("ExportExcel")]
        public IActionResult ExportExcel()
        {
            var products = _context.Products.Include(p => p.ProductType).ToList();
            using (var workbook=new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Type";
                worksheet.Cell(1, 3).Value = "Name";
                worksheet.Cell(1, 4).Value = "SKU";
                worksheet.Cell(1, 5).Value = "Description";
                worksheet.Cell(1, 6).Value = "Publisher";
                worksheet.Cell(1, 7).Value = "Price";
                worksheet.Cell(1, 8).Value = "Quantity";
                worksheet.Cell(1, 9).Value = "Image";
                worksheet.Cell(1, 10).Value = "ProductTypeID";
                worksheet.Cell(1, 11).Value = "Status";
                worksheet.Cell(1, 12).Value = "LinkEbook";

                foreach(var product in products)
                {
                    worksheet.Cell(product.Id + 1, 1).Value = product.Id;
                    worksheet.Cell(product.Id + 1, 2).Value = product.Type;
                    worksheet.Cell(product.Id + 1, 3).Value = product.Name;
                    worksheet.Cell(product.Id + 1, 4).Value = product.SKU;
                    worksheet.Cell(product.Id + 1, 5).Value = product.Description;
                    worksheet.Cell(product.Id + 1, 6).Value = product.Publisher;
                    worksheet.Cell(product.Id + 1, 7).Value = product.Price;
                    worksheet.Cell(product.Id + 1, 8).Value = product.Quantity;
                    worksheet.Cell(product.Id + 1, 9).Value = product.Image;
                    worksheet.Cell(product.Id + 1, 10).Value = product.ProductTypeID;
                    worksheet.Cell(product.Id + 1, 11).Value = product.Status;
                    worksheet.Cell(product.Id + 1, 12).Value = product.LinkEbook;

                }

                using (var stream =new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content=stream.ToArray();
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string filename = string.Format("Products.xlsx");
                    return File(content, contentType, filename);
                }
            }
        }
        [HttpPost]
        public IActionResult Search(string Name)
        {
            // Tìm kiếm sản phẩm theo tên
            var products = _context.Products.Where(p => p.Name.Contains(Name)).ToList();
            return View(products);
        }

    }
    
    
}
