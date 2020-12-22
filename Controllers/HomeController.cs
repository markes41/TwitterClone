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
        private readonly ILogger<HomeController> _logger;
        private readonly TwitterContext db;

        public HomeController(ILogger<HomeController> logger, TwitterContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult RegistrarUsuario(string mail, string nombre, string username, string password, string claveadmin)
        {
            User userCheck = db.Users.FirstOrDefault(u => u.Mail.Equals(mail) || u.Username.ToLower().Equals(username.ToLower()));

            if(userCheck == null){
                User newUser = new User{
                Mail = mail,
                Nombre = nombre,
                Username = username,
                Password = password,
                Rol = rol
            };

            db.Usuarios.Add(nuevoUsuario);
            db.SaveChanges();
            return View("Login");

            }else{
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
