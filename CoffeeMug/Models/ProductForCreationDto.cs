using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMug.Models
{
    public class ProductForCreationDto
    {
        [Required(ErrorMessage = "Name not provided")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price not provided")]
        public decimal Price { get; set; }
    }
}
