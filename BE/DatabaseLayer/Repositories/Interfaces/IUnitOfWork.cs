using DatabaseLayer.Repositories.Interfaces.SpecificRepositories;

namespace DatabaseLayer.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }

        Task<int> SaveChanges();
    }
}