﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockInventory.Models;
using StockInventory.Models.Services;
using StockInventory.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StockInventory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LoggedInService _service;

        public HomeController(ILogger<HomeController> logger, LoggedInService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            User loggedInUser = _service.getLogInUser();
            if (loggedInUser == null)
            {
                return View();
            }
            return RedirectToAction("LoggedIn", "Home");
        }

        public IActionResult LearnMore()
        {
            return View();
        }

        public IActionResult LoggedIn()
        {
            User loggedInUser = _service.getLogInUser();
            //long id = loggedInUser.UserId;
            if(loggedInUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(loggedInUser);
        }

        public IActionResult AccountSetting()
        {
            User loggedInUser = _service.getLogInUser();
            if(loggedInUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult LogOut()
        {
            User loggedInUser = _service.getLogInUser();
            
            if(loggedInUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            LoggedIn liUser = _service.FindLiId(loggedInUser.UserId);
            return View(liUser);
        }
        [HttpPost]
        public IActionResult LogOut(LoggedIn loggedIn)
        {
            _service.Delete(loggedIn);
            return RedirectToAction("Index");
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
