using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult LearnMore()
        {
            return View();
        }

        public IActionResult LoggedIn()
        {
            User loggedInUser = _service.getLogInUser();
            //long id = loggedInUser.UserId;
            return View(loggedInUser);
        }

        public IActionResult AccountSetting()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            User loggedInUser = _service.getLogInUser();
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
