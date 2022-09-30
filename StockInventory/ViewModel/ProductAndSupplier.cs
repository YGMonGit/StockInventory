using StockInventory.Models;
using System.Collections;
using System.Collections.Generic;

namespace StockInventory.ViewModel
{
    public class ProductAndSupplier
    {
        public IEnumerable<Product> Prodcut { get; set; }
        public IEnumerable<Supplier> Supplier { get; set; }
    }
}
