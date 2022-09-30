using System.ComponentModel.DataAnnotations;

namespace StockInventory.Models
{
    public class LoggedIn
    {
        [Key]
        public long LoggedInId { get; set; }
        public long UserFind { get; set; }
        public virtual User User { get; set; }
    }
}
