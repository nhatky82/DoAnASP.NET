using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Data;

namespace WebAppMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("adminComments")]
    public class CommentsController : Controller
    {
        private readonly WebAppMVCContext _context;
        public CommentsController(WebAppMVCContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            var comment = _context.Comments.ToList();
            return View(comment);
        }
    }
}
