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
        [MaxLength(12)]
        [MinLength(3)]
      
        public string FirstName { get; set; }
        [MaxLength(12)]
        [MinLength(3)]

        public string LastName { get; set; }
        [Required]
        [MaxLength(12)]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(12)]
        [MinLength(3)]

        public string Password { get; set; }
        [MaxLength(12)]
       

        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string PhoneNumber { get; set; }
    }
}
