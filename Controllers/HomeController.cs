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

        public IActionResult UserRegister(string name, string mail, string username, string password, string month, string day, string year)
        {
            User userCheck = db.Users.FirstOrDefault(u => u.Mail.Equals(mail) || u.Username.ToLower().Equals(username.ToLower()));

            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

            String dy = datevalue.Day.ToString();
            String mn = monthWithName(datevalue.Month);
            String yy = datevalue.Year.ToString();
            

            if(userCheck == null){
                User newUser = new User{
                Name = name,
                Mail = mail,
                Username = username,
                Password = password,
                Month = month,
                Day = day,
                Year = year,
                CreationDay = dy,
                CreationMonth = mn,
                CreationYear = yy,
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

        public string monthWithName(int month)
        {
            string monthname = null;
            switch(month)
            {
                case 1:
                    monthname = "enero";
                    break;
                case 2:
                    monthname = "febrero";
                    break;
                case 3:
                    monthname =  "marzo";
                    break;
                case 4:
                    monthname =  "abril";
                    break;
                case 5:
                    monthname =  "mayo";
                    break;
                case 6:
                    monthname =  "junio";
                    break;
                case 7:
                    monthname =  "julio";
                    break;
                case 8:
                    monthname =  "agosto";
                    break;
                case 9:
                    monthname =  "septiembre";
                    break;
                case 10:
                    monthname =  "octubre";
                    break;
                case 11:
                    monthname =  "noviembre";
                    break;
                case 12:
                    monthname =  "diciembre";
                    break;
                default:
                    monthname =  "Mes inválido";
                    break;
            }

            return monthname;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
