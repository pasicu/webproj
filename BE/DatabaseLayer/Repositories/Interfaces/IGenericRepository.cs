using System.Linq.Expressions;

namespace DatabaseLayer.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);

        Task<IEnumerable<T>> GetAll();

        Task Add(T entity);

        void Delete(T entity);

        Task<T> Update(T entity, object id);

        Task<T> FindAsync(Expression<Func<T, bool>> filter);
    }
}