using Common.Models.Database;
using DatabaseLayer;
using DatabaseLayer.Repositories;
using ShopManagement.Database.Repositories.Contracts;

namespace ShopManagement.Database.Repositories
{
    public class OrderProductRepository : GenericRepository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(DbContextClass context) : base(context)
        {
        }
    }
}