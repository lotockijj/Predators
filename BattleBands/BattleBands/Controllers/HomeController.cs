﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BattleBands.ViewModels;
using BattleBands.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BattleBands.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<User> _userManager;

        //class constructor
        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
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
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Account/Register");
            }
            else 
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
