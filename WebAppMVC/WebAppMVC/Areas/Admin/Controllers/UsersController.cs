using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("adminUsers")]
    public class UsersController : Controller
    {
        private readonly WebAppMVCContext _context;
        public UsersController(WebAppMVCContext context)
        {
            _context = context;
        }
        [Route("Users")]
        public IActionResult Index()
        {
            var user = _context.Users.ToList();
            return View(user);
        }
        [Route("Details")]
        public IActionResult Details(int? id)
        {
            var user = _context.Users.FirstOrDefault(m => m.Id == id);

            return View(user);
        }
        [Route("Edit")]
        [HttpGet("Edit")]
        public IActionResult Edit(int? id)
        {
            var user = _context.Users.Find(id);
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name");
            return View(user);

        }

        [HttpPost("Edit")]
        public IActionResult Edit(int? id, [Bind("Id,UserName,Password,Email,Phone,Address,FullName,IsAdmin,Avatar,Status")] User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Edit", "Users");
        }

    }
}
