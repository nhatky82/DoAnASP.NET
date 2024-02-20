using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.Data;
using WebAppMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Completion;

namespace WebAppMVC.Controllers
{
    public class UsersController : Controller
    {
        private WebAppMVCContext _context;
        public UsersController(WebAppMVCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int? id = HttpContext.Session.GetInt32("Id");
            if (id != null)
            {
                var user = _context.Users.Find(id);
                return View(user);
            }
            return RedirectToAction("Login", "Users");

        }

        
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            var Users = _context.Users.SingleOrDefault(a => a.UserName == Username && a.Password == Password);
            if (Users == null)
            {

                ViewBag.Error = "Tài khoản hoặc mật khẩu sai!";
                return View();

            }
            else
            {

                var UserId = _context.Users.FirstOrDefault(a => a.UserName == Username).Id;
                HttpContext.Session.SetString("username", Username);
                HttpContext.Session.SetString("password", Password);
                HttpContext.Session.SetInt32("Id", UserId);
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Id");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Register([Bind("UserName, Password, Email, Phone, Address, FullName, IsAdmin, Avatar, Status")] User user, string confirmPassword)
        {
            if (ModelState.IsValid == false)
            {
                if (user.Password != confirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu nhập lại không khớp với mật khẩu đã nhập.");

                }

                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại.");
                    return View();
                }
                if (_context.Users.Any(u => u.UserName == user.UserName))
                {
                    ModelState.AddModelError("Username", "Tài khoản đã tồn tại.");
                    return View();
                }

                if (_context.Users.Any(u => u.Phone == user.Phone))
                {
                    ModelState.AddModelError("Phone", "Số điện thoại đã tồn tại.");
                    return View();
                }
                return View();
            }
            else
            {

                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Đăng ký thành công!";
                return RedirectToAction("Login", "Users");
            }


        }

        [HttpGet("Edit")]
        public IActionResult Edit()
        {
            int? id = HttpContext.Session.GetInt32("Id");
            var user = _context.Users.Find(id);

            return View(user);
        }

        [HttpPost("Edit")]
        public IActionResult Edit([Bind("Id, UserName, Password, Email, Phone, Address,FullName, Avatar")] User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Users");
        }

        [HttpGet("Detail")]
        public IActionResult Detail()
        {
            int? id = HttpContext.Session.GetInt32("Id");
            var user = _context.Users.Find(id);

            return View(user);
        }

        [HttpPost("Detail")]
        public IActionResult Detail(string password, string passwordNew1, string passwordNew2)
        {
            int? id = HttpContext.Session.GetInt32("Id");
            var Username = HttpContext.Session.GetString("username");
            var User = _context.Users.FirstOrDefault(c => c.Password == password && c.UserName == Username);

            if (User == null)
            {
                ViewBag.ErrorPass = "Mẫt Khẩu cũ Không đúng! Vui lòng nhập lại";
                return View();
            }
            else if (passwordNew1 != passwordNew2)
            {
                ViewBag.ErrorPass2 = "Mật khẩu mới không khớp! Vui lòng nhập lại";
                return View();
            }
            else
            {
                User.Password = passwordNew1;
                _context.Users.Update(User);
            }
            ViewBag.ErrorPass3 = true;
            _context.SaveChanges();
            return View();
        }


    }
}
