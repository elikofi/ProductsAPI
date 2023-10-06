using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace ProductsShop.Models.DTO
{
    //[Table("Products")]
    public class Product
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string? ProductName { get; set; }

        
        [Required]
        public double ProductPrice { get; set; }
    }
}
