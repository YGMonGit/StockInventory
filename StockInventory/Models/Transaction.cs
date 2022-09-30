using System;
using System.ComponentModel.DataAnnotations;

namespace StockInventory.Models
{
    public class Transaction
    {
        [Key]
        public long TransactionId { get; set; }
        public int PurchasedQuantity { get; set; }
        public int SoldQuantity { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public long? ProductId { get; set; }
        public long? SupplierId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual User User { get; set; }
    }
}
