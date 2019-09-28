using System;
using System.Collections.Generic;

namespace CoffeeMug.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int NumberOfAddons { get { return Addons.Count; } }

        public ICollection<ProductAddonDto> Addons { get; set; } = new List<ProductAddonDto>();
    }
}
