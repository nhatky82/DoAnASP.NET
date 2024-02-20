using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class InvoiceDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
