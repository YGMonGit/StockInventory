using Microsoft.EntityFrameworkCore;
using StockInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockInventory.Models.Services
{
    public class LoggedInService : IService<LoggedIn>
    {
        private readonly ApplicationDbContext _context;
        public LoggedInService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(LoggedIn loggedIn)
        {
            _context.LoggedIns.Add(loggedIn);
            _context.SaveChanges();
        }

        public void Delete(LoggedIn loggedIn)
        {
            _context.Remove(loggedIn);
            _context.SaveChanges();
        }

        public User FindId(String username, String password)
        {
            throw new System.NotImplementedException();
        }

        public LoggedIn FindLiId(long usersId)
        {
            var result = _context.LoggedIns.Where(x => x.User.UserId == usersId).SingleOrDefault();
            return result;
        }

        public List<LoggedIn> GetAll()
        {
            var result = _context.LoggedIns.ToList();
            return result;
        }

        public LoggedIn GetById(long id)
        {
            var result = _context.LoggedIns.Find(id);
            return result;
        }

        public void Update(LoggedIn loggedIn)
        {
            _context.Update(loggedIn);
            _context.SaveChanges();
        }

        LoggedIn IService<LoggedIn>.FindId(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        //public User getLogInUser()
        //{
        //    return _context.LoggedIns.Include(log => log.User).Where(log => log.LoggedInId == 1).SingleOrDefault().User;
        //}

        public User getLogInUser()
        {
            var log = _context.LoggedIns.Include(log => log.User).Where(log => log.UserFind == 1).SingleOrDefault();
            if (log == null)
            {
                return null;
            }
            return log.User;
        }
    }
}
