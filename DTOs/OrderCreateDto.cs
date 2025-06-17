using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.DTOs
{
    public class OrderCreateDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public List<OrderItemCreateDto> OrderItems { get; set; }
    }
}
