using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMug.Data.Entities
{
    public class ProductAddon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
