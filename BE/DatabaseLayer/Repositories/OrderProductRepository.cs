using Common.Models.Database;
using DatabaseLayer;
using DatabaseLayer.Repositories.Interfaces.SpecificRepositories;

namespace DatabaseLayer.Repositories
{
    public class OrderProductRepository : GenericRepository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(DbContextClass context) : base(context)
        {
        }
    }
}