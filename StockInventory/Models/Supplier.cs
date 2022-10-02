using System.ComponentModel.DataAnnotations;

namespace StockInventory.Models
{
    public class Supplier
    {
        [Key]
        public long SupplierId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Limited charaters")]
        public string CompanyName { get; set; }
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string PhoneNumber { get; set; }
        public virtual User User { get; set; }
    }
}
