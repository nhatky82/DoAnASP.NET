using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
	public class ContactsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
