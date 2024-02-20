using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class FavouritesController : Controller
    {
        public WebAppMVCContext _context;
        public FavouritesController(WebAppMVCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int? UserId = HttpContext.Session.GetInt32("Id");
            if (UserId != null)
            {
                var favourite = _context.Favourites.Include(c => c.Product).Where(c => c.UserID == UserId);
                return View(favourite);
            }
            return RedirectToAction("Login", "Users");
        }

        public IActionResult Add(int id, int quantity = 1)
        {
            int? UserId = HttpContext.Session.GetInt32("Id");
            if (UserId != null)
            {
                var favourite = _context.Favourites.FirstOrDefault(c => c.UserID == UserId && c.ProductID == id);
                if (favourite == null)
                {
                    favourite = new Favourite()
                    {
                        UserID = (int)UserId,
                        ProductID = id
                    };
                    _context.Favourites.Add(favourite);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Users");
        }

        public IActionResult Close(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("Id");
            if (UserId != null)
            {
                var favourites = _context.Favourites.FirstOrDefault(c => c.UserID == UserId && c.ProductID == id);
                _context.Favourites.Remove(favourites);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
