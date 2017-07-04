using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BattleBands.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Bands()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult NewArtist()
        {
            return View();
        }
       /* public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }*/

        public IActionResult Error()
        {
            return View();
        }
    }
}
