using CoffeeMug.Data.Entities;
using Infrastructure.Repository;

namespace CoffeeMug.Data.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}