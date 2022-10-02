using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StockInventory.Models
{
    public class User
    {
        [Key]
        public long UserId { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Limited charaters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Limited charaters")]
        public string LastName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Limited charaters")]
        public string UserName { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "8 charaters for password")]
        public string Password { get; set; }
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string PhoneNumber { get; set; }
    }
}
