using StockInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockInventory.Models.Services
{
    public class UserService : IService<User>
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }

        public User FindId(String username, String password)
        {
            var result = _context.Users.Where(x => x.UserName == username && x.Password == password).SingleOrDefault();
            return result;
        }

        public List<User> GetAll()
        {
            var result = _context.Users.ToList();
            return result;
        }

        public User GetById(long id)
        {
            var result = _context.Users.Find(id);
            return result;
        }

        public void Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
    }
}
