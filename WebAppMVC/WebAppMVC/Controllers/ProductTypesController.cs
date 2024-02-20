using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class ProductTypesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
