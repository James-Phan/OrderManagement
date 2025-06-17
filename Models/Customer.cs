using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace OrderManagement.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
