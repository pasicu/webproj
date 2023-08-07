﻿using DatabaseLayer.Repositories.Interfaces;
using DatabaseLayer.Repositories.Interfaces.SpecificRepositories;

namespace DatabaseLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;
        public IUserRepository Users { get; }
        public IProductRepository Products { get; }
        public IOrderRepository Orders { get; }

        public UnitOfWork(DbContextClass dbContext,
                            IUserRepository users,
                            IProductRepository products,
                            IOrderRepository orders)
        {
            _dbContext = dbContext;
            Users = users;
            Products = products;
            Orders = orders;
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}