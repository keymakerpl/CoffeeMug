using System.ComponentModel.DataAnnotations;

namespace CoffeeMug.Models
{
    public class ProductAddonForCreationDto
    {
        [Required(ErrorMessage = "Name not provided")]        
        [MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
