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
    public class SupplierController : Controller
    {
        private readonly IService<Supplier> _service;
        private readonly IService<LoggedIn> _service2;
        private readonly LoggedInService _service3;
        private readonly ApplicationDbContext _context;

        public SupplierController(IService<Supplier> service, IService<LoggedIn> service2, LoggedInService service3, ApplicationDbContext context)
        {
            _service = service;
            _service2 = service2;
            _service3 = service3;
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _service.GetAll();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string supplierSearch)
        {
            ViewData["GetTheData"] = supplierSearch;
            User loggedInUser = _service3.getLogInUser();
            var srcQuery = _context.Suppliers.Where(x => x.User.UserId == loggedInUser.UserId);
            if (!String.IsNullOrEmpty(supplierSearch))
            {
                IQueryable<Supplier> suppliers = srcQuery.Where(x => x.CompanyName.Contains(supplierSearch));

                srcQuery = suppliers;
            }
            return View(await srcQuery.AsNoTracking().ToListAsync());
        }

        public IActionResult Details(long id)
        {
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            User loggedInUser = _service3.getLogInUser();
            if (!ModelState.IsValid)
            {
                return View(supplier);
            }
            supplier.User = loggedInUser;
            _service.Add(supplier);
            return RedirectToAction("Index");
        }
      
        
        public IActionResult Edit(long id)
        {
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            _service.Update(supplier);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long id)
        {
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(Supplier supplier)
        {
            _service.Delete(supplier);
            return RedirectToAction("Index");
        }



    }
}
