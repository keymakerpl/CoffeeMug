using CoffeeMug.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace CoffeeMug.Data
{
    public class CoffeeMugDbContext : DbContext
    {
        private readonly ILogger<CoffeeMugDbContext> _logger;

        public CoffeeMugDbContext(DbContextOptions<CoffeeMugDbContext> dbContextOptions, ILogger<CoffeeMugDbContext> logger) : base(dbContextOptions)
        {
            _logger = logger;

            Initialize();
        }

        private void Initialize()
        {
            try
            {
                Database.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption has been thrown durring DB migration. \n{ex.Message}");
            }
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAddon> Addons { get; set; }
    }
}
