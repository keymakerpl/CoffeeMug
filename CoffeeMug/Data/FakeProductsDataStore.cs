using CoffeeMug.Models;
using System;
using System.Collections.Generic;

namespace CoffeeMug.Data
{
    /// <summary>
    /// Fake Data Store
    /// </summary>
    public class FakeProductsDataStore
    {
        public static FakeProductsDataStore Current { get; } = new FakeProductsDataStore();

        public List<ProductDto> ProductDtos { get; set; }

        public FakeProductsDataStore()
        {
            ProductDtos = new List<ProductDto>()
            {
                new ProductDto(){ Id =  new Guid("7E6718B1-7028-4148-8223-AB3FB2A4EB4E"), Name = "Pompka", Price = 21},
                new ProductDto(){ Id =  new Guid("379799E9-8D30-438F-BBF9-99EC86846892"), Name = "Dętka", Price = 31,
                    Addons = new List<ProductAddonDto>()
                    {
                        new ProductAddonDto() {Id = new Guid("A7BD0736-11B0-46B1-9D7E-09FFC1D5AD49"), Name = "Zaworek", Description = "Gratis" }
                    }
                }
            };
        }
    }
}
