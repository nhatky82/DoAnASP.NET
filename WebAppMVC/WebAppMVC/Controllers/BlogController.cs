using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
	public class BlogController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
