using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMug.Models
{
    public class ProductForUpdateDto
    {
        [Required(ErrorMessage = "Name not provided")]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price not provided")]
        public decimal Price { get; set; }

        public ICollection<ProductAddonDto> Addons { get; set; } = new List<ProductAddonDto>();
    }
}
