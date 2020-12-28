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
    public class StartController : Controller
    {
        private readonly ILogger<StartController> logger;
        private readonly TwitterContext db;

        public StartController(ILogger<StartController> logger, TwitterContext context)
        {
            this.logger = logger;
            this.db = context;
        }


        public IActionResult Inicio()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
