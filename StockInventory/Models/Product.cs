using System.ComponentModel.DataAnnotations;

namespace StockInventory.Models
{
    public class Product
    {
        [Key]
        public long ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [StringLength(50,ErrorMessage ="Limited charaters")]
        public string Description { get; set; }
       
        [RegularExpression("^[0-9]*$", ErrorMessage = " Must be numeric")]
        public int Quantity { get; set; }
        
        [RegularExpression("^[0-9]*$", ErrorMessage = " Must be numeric")]
        public double Price { get; set; }
        public virtual User User { get; set; }
    }
}
