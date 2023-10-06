using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace ProductsShop.Models.DTO
{
    public class Product
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string? ProductName { get; set; }

        [Required]
        [Comment("Price of the product")]
        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string FormattedPrice
        {
            get
            {
                // Get the current culture info for the local currency formatting
                CultureInfo cultureInfo = CultureInfo.CurrentCulture;

                // Format the price using the current culture's currency symbol and formatting rules
                return _price.ToString("C", cultureInfo);
            }
            set
            {
                // Attempt to parse the input string to decimal and set the Price property
                if (decimal.TryParse(value, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal parsedPrice))
                {
                    _price = parsedPrice;
                }
                else
                {
                    // Handle invalid input if necessary
                    Console.WriteLine("Invalid price format.");
                }
            }
        }
    }
}
