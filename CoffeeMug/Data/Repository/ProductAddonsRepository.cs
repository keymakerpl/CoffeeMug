using CoffeeMug.Data.Entities;
using Infrastructure.Repository;

namespace CoffeeMug.Data.Repository
{
    public class ProductAddonsRepository : GenericRepository<ProductAddon, CoffeeMugDbContext>, IProductAddonRepository
    {
        protected ProductAddonsRepository(CoffeeMugDbContext context) : base(context)
        {
        }
    }
}
