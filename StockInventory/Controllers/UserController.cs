using Microsoft.AspNetCore.Mvc;
using StockInventory.Models;
using StockInventory.Models.Services;
using StockInventory.Services;
using System;

namespace StockInventory.Controllers
{
    public class UserController : Controller
    {

        private readonly IService<User> _service;
        private readonly IService<LoggedIn> _service2;
        private readonly LoggedInService _service3;

        public UserController(IService<User> service, IService<LoggedIn> service2, LoggedInService service3)
        {
            _service = service;
            _service2 = service2;
            _service3 = service3;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            _service.Add(user);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            User loggedInUser = _service3.getLogInUser();
            if(loggedInUser == null)
            {
                return View();
            }
            return RedirectToAction("LoggedIn", "Home");
        }

        [HttpPost]
        public IActionResult Login(String username, String password)
        {
            User usersId = _service.FindId(username, password);
            if (usersId == null)
            {
                return View("NotFound");
            }
            else
            {
                LoggedIn ln = new LoggedIn();

                ln.UserFind = 1;
                ln.User = usersId;
                _service2.Add(ln);
                return RedirectToAction("Index", "Home");
            }
        }
        //public IActionResult Login(String username, String password)
        //{
        //    User usersId = _service.FindId(username, password);
        //    if(usersId == null)
        //    {
        //        return View("NotFound");
        //    }
        //    else
        //    {

        //        return RedirectToAction("Index", "Home");
        //    }

        //}



    }
}
