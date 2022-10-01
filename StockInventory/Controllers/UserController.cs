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


        public IActionResult Edit()
        {
            User loggedInUser = _service3.getLogInUser();
            long id = loggedInUser.UserId;
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");

            return View(data);
        }

       [HttpPost]
        public IActionResult Edit(User user)
        {
            _service.Update(user);
            return RedirectToAction("accountsetting", "Home");
          
        }


        public IActionResult Delete()
        {
            User loggedInUser = _service3.getLogInUser();
            long id = loggedInUser.UserId;
            LoggedIn liUser = _service3.FindLiId(loggedInUser.UserId);
            _service2.Delete(liUser);
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");

            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            _service.Delete(user);
            return RedirectToAction("accountsetting", "Home");

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
