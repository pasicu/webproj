using DatabaseLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatabaseLayer.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContextClass _dbContext;

        protected GenericRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(T entity, object id)
        {
            T currentEntity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Entry(currentEntity).CurrentValues.SetValues(entity);
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}