﻿using Common.Models.Database;
using DatabaseLayer.Repositories.Interfaces.SpecificRepositories;

namespace DatabaseLayer.Repositories
{
    public class UserRepository : GenericRepository<UserModel>, IUserRepository
    {
        public UserRepository(DbContextClass dbContext) : base(dbContext)
        {
        }
    }
}