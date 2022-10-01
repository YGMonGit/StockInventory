using StockInventory.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StockInventory.Models.Services
{
    public class SupplierService : IService<Supplier>
    {
        private readonly ApplicationDbContext _context;
        private readonly LoggedInService _service;
        public SupplierService(ApplicationDbContext context, LoggedInService service)
        {
            _context = context;
            _service = service;
        }

        public void Add(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public void Delete(Supplier supplier)
        {
            _context.Remove(supplier);
            _context.SaveChanges();
        }

        public Supplier FindId( String CompanyName)
        {
            var result = _context.Suppliers.Where(x => x.CompanyName == CompanyName).SingleOrDefault();
            return result;
        }
       

        public Supplier FindId(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        /*public Supplier GetAll(long userid)
        {

            var result = _context.Suppliers.Find(userid);
            return result;
        }*/

        public List<Supplier> GetAll()
        {
            User loggedInUser = _service.getLogInUser();
            var result = _context.Suppliers.Where(x => x.User.UserId == loggedInUser.UserId).ToList();
            return result;
        }

        public Supplier GetById(long id)
        {
            var result = _context.Suppliers.Find(id);
            return result;
        }

        public void Update(Supplier supplier)
        {
            _context.Update(supplier);
            _context.SaveChanges();
        }

    }
}
