using System.ComponentModel.DataAnnotations;

namespace StockInventory.Models
{
    public class Product
    {
        [Key]
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public virtual User User { get; set; }
    }
}
