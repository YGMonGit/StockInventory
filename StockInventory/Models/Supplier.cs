using System.ComponentModel.DataAnnotations;

namespace StockInventory.Models
{
    public class Supplier
    {
        [Key]
        public long SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual User User { get; set; }
    }
}
