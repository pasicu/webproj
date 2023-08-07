using Common.Models.Database;
using DatabaseLayer.Repositories.Interfaces;

namespace DatabaseLayer.Repositories.Interfaces.SpecificRepositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
    }
}