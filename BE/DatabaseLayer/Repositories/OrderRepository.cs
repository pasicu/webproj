using Common.Models.Database;
using DatabaseLayer;
using DatabaseLayer.Repositories;
using ShopManagement.Database.Repositories.Contracts;

namespace ShopManagement.Database.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContextClass context) : base(context)
        {
        }
    }
}