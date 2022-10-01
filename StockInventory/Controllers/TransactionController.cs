using Microsoft.AspNetCore.Mvc;
using StockInventory.Models;
using StockInventory.Models.Services;
using StockInventory.Services;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using ClosedXML.Excel;


namespace StockInventory.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IService<Transaction> _service;
        private readonly IService<Product> _service2;
        private readonly LoggedInService _service3;
        private readonly IService<Supplier> _service4;
        private readonly ApplicationDbContext _context;

        public TransactionController(IService<Transaction> service, IService<Product> service2, LoggedInService service3, IService<Supplier> service4, ApplicationDbContext context)
        {
            _context = context;
            _service = service;
            _service2 = service2;
            _service3 = service3;
            _service4 = service4;
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
            var srcQuery = _context.Transactions.Include(log => log.User).Include(log => log.Product).Include(log => log.Supplier).Where(x => x.User.UserId == loggedInUser.UserId);
            if (!String.IsNullOrEmpty(ProductSearch))
            {
                srcQuery = srcQuery.Include(log => log.User).Include(log => log.Product).Include(log => log.Supplier).Where(x => x.Product.ProductName.Contains(ProductSearch));
            }
            return View(await srcQuery.AsNoTracking().ToListAsync());
        }

        public IActionResult Details(long id)
        {
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            return View(data);
        }

        public IActionResult CreateSell()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSell(Transaction transaction)
        {
            User loggedInUser = _service3.getLogInUser();
            if (!ModelState.IsValid)
            {
                return View(transaction);
            }
            transaction.User = loggedInUser;
            transaction.PurchasedQuantity = 0;
            transaction.DateOfPurchase = DateTime.Now;
            _service.Add(transaction);

            Transaction tran = _service.GetById(transaction.TransactionId);

            Product pro = tran.Product;

            var data = _service2.GetById(tran.Product.ProductId);
            data.Quantity -= transaction.SoldQuantity;
            _service2.Update(data);


            return RedirectToAction("Index");
        }

        public IActionResult CreatePurchase()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePurchase(Transaction transaction)
        {
            User loggedInUser = _service3.getLogInUser();
            if (!ModelState.IsValid)
            {
                return View(transaction);
            }
            transaction.User = loggedInUser;
            transaction.SoldQuantity = 0;
            transaction.DateOfPurchase = DateTime.Now;
            _service.Add(transaction);

            Transaction tran = _service.GetById(transaction.TransactionId);

            var data = _service2.GetById(tran.Product.ProductId);
            data.Quantity += transaction.PurchasedQuantity;
            _service2.Update(data);

            return RedirectToAction("Index");
        }

        public int oldvals, sorps;

        public IActionResult Edit(long id)
        {
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            if (data.PurchasedQuantity == 0)
            {
                sorps = 1;
                oldvals = data.SoldQuantity;
            }
            else
            {
                sorps = 0;
                oldvals = data.PurchasedQuantity;
            }
            TempData["val"] = oldvals;
            TempData["sor"] = sorps;
            return View(data);

        }
        [HttpPost]
        public IActionResult Edit(Transaction transaction)
        {
            int newVal,oldval,sorp;

            oldval = Int32.Parse(TempData["val"].ToString());
            sorp = Int32.Parse(TempData["sor"].ToString());

            _service.Update(transaction);

            var tran = _service.GetById(transaction.TransactionId);
            var data = _service2.GetById(tran.Product.ProductId);

            if (tran.PurchasedQuantity == 0){
                newVal = tran.SoldQuantity;
            }else{
                newVal = tran.PurchasedQuantity;
            }


            if(sorp == 0){
                if (oldval <= newVal){
                    newVal -= oldval;
                    data.Quantity += newVal;
                }else{
                    oldval -= newVal;
                    data.Quantity -= oldval;
                }
            }else{
                if (oldval <= newVal){
                    newVal -= oldval;
                    data.Quantity -= newVal;
                }else{
                    oldval -= newVal;
                    data.Quantity += oldval;
                }
            }
            _service2.Update(data);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long id)
        {
            var data = _service.GetById(id);
            if (data == null) return View("NotFound");
            var prod = _service2.GetById(data.Product.ProductId);
            if (data.PurchasedQuantity == 0)
            {
                prod.Quantity += data.SoldQuantity;
            }
            else
            {
                prod.Quantity -= data.PurchasedQuantity;
            }
            _service2.Update(prod);

            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(Transaction transaction)
        {
            _service.Delete(transaction);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public FileResult Export(string TrasactionSearch)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Purchased Quantity"),
                                          new DataColumn("Date Of Purchase"),
                                          new DataColumn("Purchased Price"),
                                            new DataColumn("Sold Quantity"),

                                            new DataColumn(" Date Of Sold") ,
                                          });
            var data = _service.GetAll();
            if (data != null)
            {
                if (String.IsNullOrEmpty(TrasactionSearch))
                {
                    foreach (var item in data)
                    {
                        dt.Rows.Add(item.PurchasedQuantity, item.DateOfPurchase,  item.SoldQuantity);
                    }

                }
                else
                {
                    ViewData["GetTheData"] = TrasactionSearch;
                    User loggedInUser = _service3.getLogInUser();
                    var srcQuery = _context.Transactions.Include(log => log.User).Include(log => log.Product).Include(log => log.Supplier).Where(x => x.User.UserId == loggedInUser.UserId);
                    if (!String.IsNullOrEmpty(TrasactionSearch))
                    {
                        srcQuery = srcQuery.Include(log => log.User).Include(log => log.Product).Include(log => log.Supplier).Where(x => x.Product.ProductName.Contains(TrasactionSearch));
                    }

                    foreach (var item in srcQuery)
                    {
                        dt.Rows.Add(item.PurchasedQuantity, item.DateOfPurchase, item.SoldQuantity);
                    }

                }
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Transction.xlsx");
                }
            }
        }
    }
}
