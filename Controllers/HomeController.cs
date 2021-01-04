using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly TwitterContext db;

        public HomeController(ILogger<HomeController> logger, TwitterContext context)
        {
            this.logger = logger;
            this.db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult UserLogin(string email, string password)
        {
            User userCheckLogged = HttpContext.Session.Get<User>("UsuarioLogueado");

            if(userCheckLogged == null)
            {
                userCheckLogged  = db.Users.FirstOrDefault(u => u.Mail.Equals(email));
                if(userCheckLogged != null && userCheckLogged.Password.Equals(password))
                {
                    HttpContext.Session.Set<User>("UsuarioLogueado", userCheckLogged);
                    return RedirectToAction("Inicio", "Start");
                }
                else
                {
                    ViewBag.errorCredenciales = true;
                    return View("Login");
                }
            }
            else
            {
                return RedirectToAction("Inicio", "Start");
            }
        }

        public IActionResult UserRegister(string mail, string username, string password, string month, string day, string year)
        {
            User userCheck = db.Users.FirstOrDefault(u => u.Mail.Equals(mail) || u.Username.ToLower().Equals(username.ToLower()));

            if(userCheck == null){
                User newUser = new User{
                Mail = mail,
                Username = username,
                Password = password,
                Month = month,
                Day = day,
                Year = year
            };

            db.Users.Add(newUser);
            db.SaveChanges();
            return View("Login");

            }
            else
            {
                ViewBag.existeUsuario = true;
                return View("Register");
            }   
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
