using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAppMVC.Data;

using WebAppMVC.Models;
using WebAppMVC.Models.Authentication;

namespace WebAppMVC.Controllers
{
	public class HomeController : Controller
	{

		private readonly  WebAppMVCContext _context;

		public HomeController(WebAppMVCContext context)
		{
			_context = context;
		}
		//[Authentication]
		public IActionResult Index(string searchTerm, string selectedSection, int pageIndex = 1, int pageSize = 8)
		{
			IQueryable<Product> productsQuery = _context.Products.AsQueryable();
      
            if (!string.IsNullOrEmpty(searchTerm))
			{
				productsQuery = productsQuery
					.Where(p => p.Name.ToUpper().Contains(searchTerm.ToUpper()));
				ViewBag.SearchTerm = searchTerm;
			}
			if (!string.IsNullOrEmpty(selectedSection))
			{
				switch (selectedSection)
				{
					case "Dưới 100k":
						productsQuery = productsQuery.Where(p => p.Price < 100000);
						break;
					case "Từ 100k -> 200k":
						productsQuery = productsQuery.Where(p => p.Price >= 100000 && p.Price <= 200000);
						break;
					case "Trên 200k":
						productsQuery = productsQuery.Where(p => p.Price > 200000);
						break;
					// Trường hợp mặc định là hiển thị tất cả
					default:
						break;
				}
			}
			ViewBag.SelectedSection = selectedSection;
			
        
            var totalItems = productsQuery.Count();
			
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = pageIndex,
                ItemsPerPage = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
         
            };
            var products = productsQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var viewModel = new ProductViewModel
            {
                Products = products,
                PaginationInfo = paginationInfo
            };

            if (products.Count == 0)
			{
				ViewBag.Message = "Không tìm thấy kết quả nào.";
			}
			List<ProductType> productTypes = _context.ProductTypes.ToList();
			ViewBag.ProductTypes = productTypes;
            // Truyền dữ liệu từ action Search đến view của action Index

            int? UserId = HttpContext.Session.GetInt32("Id");
			if (UserId != null)
			{

				int QuantityCart = _context.Carts.Include(c => c.Product).Where(c => c.UserID == UserId).Count();
                int QuantityFa = _context.Favourites.Include(c => c.Product).Where(c => c.UserID == UserId).Count();

				HttpContext.Session.SetInt32("QuantityCart", QuantityCart);
                HttpContext.Session.SetInt32("QuantityFa", QuantityFa);
            }

                return View("Index", viewModel);

		}




		public IActionResult Detail(int Id)
        {
            var products = _context.Products.Find(Id);
            return View(products);
        }
        public IActionResult Privacy()
		{
			return View();
		}
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		//[HttpGet]
		//[Route("Home/Search")]
		//public IActionResult Search(string searchTerm)
		//{
		//	IQueryable<Product> productsQuery = _context.Products;

		//	if (!string.IsNullOrEmpty(searchTerm))
		//	{
		//		productsQuery = productsQuery
		//			.Where(p => p.Name.ToUpper().Contains(searchTerm.ToUpper()));
		//	}

		//	var products = productsQuery.ToList();

		//	// Truyền dữ liệu từ action Search đến view của action Index
		//	return View("Index", products);
		//}


	}
}