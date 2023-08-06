using Common.Models.Database;
using DatabaseLayer.Repositories.Interfaces;

namespace ShopManagement.Database.Repositories.Contracts
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
    }
}