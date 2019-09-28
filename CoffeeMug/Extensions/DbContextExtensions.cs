using CoffeeMug.Data;
using CoffeeMug.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMug.Extensions
{
    public static class DbContextExtensions
    {
        public static void EnsureSeedData(this CoffeeMugDbContext context)
        {
            if (context.Products.Any()) return;

            var products = new List<Product>()
            {
                new Product() { Name = "Pompka", Price = 33, Addons = new List<ProductAddon>() { new ProductAddon(){ Name = "Zakrętka" } } },
                new Product() { Name = "Łańcuch", Price = 29},
                new Product() { Name = "Dzwonek", Price = 19}
            };

            context.AddRange(products);
            context.SaveChanges();
        }
    }
}
