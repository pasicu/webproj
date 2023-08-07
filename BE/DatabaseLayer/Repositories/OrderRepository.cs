using Common.Models.Database;
using DatabaseLayer;
using DatabaseLayer.Repositories;
using DatabaseLayer.Repositories.Interfaces.SpecificRepositories; 

namespace DatabaseLayer.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContextClass context) : base(context)
        {
        }
    }
}