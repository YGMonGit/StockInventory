using Microsoft.EntityFrameworkCore;
using StockInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockInventory.Models.Services
{
    public class TransactionService : IService<Transaction>
    {
        private readonly ApplicationDbContext _context;
        private readonly LoggedInService _service;
        public TransactionService(ApplicationDbContext context, LoggedInService service)
        {
            _context = context;
            _service = service;
        }

        public void Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void Delete(Transaction transaction)
        {
            _context.Remove(transaction);
            _context.SaveChanges();
        }

        public Transaction FindId(string username, string password)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetAll()
        {
            User loggedInUser = _service.getLogInUser();
            var result = _context.Transactions.Include(log => log.User).Include(log => log.Product).Include(log => log.Supplier).Where(x => x.User.UserId == loggedInUser.UserId).ToList();
            return result;
        }

        public Transaction GetById(long id)
        {
            var result = _context.Transactions.Include(log => log.Product).Include(log => log.Supplier).Where(x => x.TransactionId == id).SingleOrDefault();
            return result;
        }

        public void Update(Transaction transaction)
        {
            _context.Update(transaction);
            _context.SaveChanges();
        }
    }
}
