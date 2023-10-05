using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsShop.Models.DTO
{
    public class Product
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string? ProductName { get; set; }
    }
}
