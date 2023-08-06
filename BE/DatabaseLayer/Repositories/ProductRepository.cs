using Common.Models.Database;
using DatabaseLayer.Repositories.Interfaces.SpecificRepositories;

namespace DatabaseLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContextClass dbContext) : base(dbContext)
        {
        }
    }
}