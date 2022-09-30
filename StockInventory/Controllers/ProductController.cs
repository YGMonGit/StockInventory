using Microsoft.AspNetCore.Mvc;
using StockInventory.Models;
using StockInventory.Models.Services;
using StockInventory.Services;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StockInventory.Controllers
{
    public class ProductController : Controller
    {
        private readonly IService<Product> _service;
        private readonly IService<LoggedIn> _service2;
        private readonly LoggedInService _service3;
        private readonly ApplicationDbContext _context;

        public ProductController(IService<Product> service, IService<LoggedIn> service2, LoggedInService service3, ApplicationDbContext context)
        {
            _context = context;
            _service = service;
            _service2 = service2;
            _service3 = service3;
        }

        public IActionResult Index()
        {
            var data = _service.GetAll();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string ProductSearch)
        {
            ViewData["GetTheData"] = ProductSearch;
            User loggedInUser = _service3.getLogInUser();
            var srcQuery = _context.Products.Where(x => x.User.UserId == loggedInUser.UserId);
            if (!String.IsNullOrEmpty(ProductSearch))
            {
                srcQuery = srcQuery.Where(x => x.ProductName.Contains(ProductSearch));
            }
            return View(await srcQuery.AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            User loggedInUser = _service3.getLogInUser();
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            product.User = loggedInUser;
            _service.Add(product);
            return RedirectToAction("Index");
        }
        public IActionResult Details(long id)
        {
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            return View(data);
        }

        public IActionResult Edit(long id)
        {
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _service.Update(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long id)
        {
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _service.Delete(product);
            return RedirectToAction("Index");
        }

    }
}
