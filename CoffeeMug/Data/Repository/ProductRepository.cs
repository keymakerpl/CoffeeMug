using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMug.Data.Entities;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMug.Data.Repository
{
    public class ProductRepository : GenericRepository<Product, CoffeeMugDbContext>, IProductRepository
    {
        public ProductRepository(CoffeeMugDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Context.Set<Product>().Include(p => p.Addons).ToListAsync();
        }
    }
}
