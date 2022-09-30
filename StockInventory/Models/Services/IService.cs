using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StockInventory.Services
{
    public interface IService<T>
    {
        List<T> GetAll();
        void Add(T t);
        T GetById(long t);
        void Update(T t);
        void Delete(T t);

        T FindId(String username, String password);
    }
}