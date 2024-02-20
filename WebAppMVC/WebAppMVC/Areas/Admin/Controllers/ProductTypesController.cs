using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("adminProductTypes")]
    public class ProductTypesController : Controller
    {

        private readonly WebAppMVCContext _context;

        public ProductTypesController(WebAppMVCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ProductTypes.ToList());
        }
        [Route("Details")]
        public IActionResult Details(int? id)
        {
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }
            var productType = _context.ProductTypes.FirstOrDefault(m => m.Id == id);
            if(productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        [Route("Create")]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create([Bind("Id,Name,Status")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productType);
                _context.SaveChanges();
                return RedirectToAction("Create");
            }
            return View(productType);
        }

        [Route("Edit")]
        public IActionResult Edit(int? id)
        {
            if(id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }
            var productType = _context.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        [HttpPost("Edit")]
        public IActionResult Edit(int id, [Bind("Id,Name,Status")] ProductType productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productType);
                    _context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Create");
            }
            return View(productType);
        }
        [Route("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var productType =  _context.ProductTypes
                .FirstOrDefault(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }
        [HttpPost("Delete")]
        public  IActionResult DeleteConfirmed(int id)
        {
            if (_context.ProductTypes == null)
            {
                return Problem("Entity set 'WebAppMVCContext.ProductTypes'  is null.");
            }
            var productType = _context.ProductTypes.Find(id);
            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        private bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(e => e.Id == id);
        }
    }
}
