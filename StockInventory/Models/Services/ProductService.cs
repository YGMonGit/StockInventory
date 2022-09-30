using StockInventory.Services;
using System.Collections.Generic;
using System.Linq;

namespace StockInventory.Models.Services
{
    public class ProductService : IService<Product>
    {
        private readonly ApplicationDbContext _context;
        private readonly LoggedInService _service;
        public ProductService(ApplicationDbContext context, LoggedInService service)
        {
            _context = context;
            _service = service;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }

        public Product FindId(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public List<Product> GetAll()
        {
            User loggedInUser = _service.getLogInUser();
            var result = _context.Products.Where(x => x.User.UserId == loggedInUser.UserId).ToList();
            return result;
        }

        public Product GetById(long id)
        {
            var result = _context.Products.Find(id);
            return result;
        }

        public void Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }
    }
}
