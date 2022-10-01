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
        private readonly IService<Supplier> _service4;
        private readonly IService<Product> _service5;
        private readonly IService<Transaction> _service6;


        public UserController(IService<User> service, IService<LoggedIn> service2, LoggedInService service3, IService<Supplier> service4, IService<Product> service5, IService<Transaction> service6)
        {
            _service = service;
            _service2 = service2;
            _service3 = service3;
            _service4 = service4;
            _service5 = service5;
            _service6 = service6;
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
                return RedirectToAction("LoggedIn", "Home");
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
            return RedirectToAction("AccountSetting", "Home");
        }

        public IActionResult Delete()
        {
            User loggedInUser = _service3.getLogInUser();
            long id = loggedInUser.UserId;
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");

            var product = _service5.GetAll();
            var supplier = _service4.GetAll();
            var transaction = _service6.GetAll();
            foreach (var item in product)
            {
                _service5.Delete(item);
            }
            foreach (var item in supplier)
            {
                _service4.Delete(item);
            }
            foreach (var item in transaction)
            {
                _service6.Delete(item);
            }

            LoggedIn liUser = _service3.FindLiId(loggedInUser.UserId);
            _service3.Delete(liUser);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            _service.Delete(user);
            return RedirectToAction("Index", "Home");
        }

    }
}
