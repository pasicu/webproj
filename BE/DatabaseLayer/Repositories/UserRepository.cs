using Common.Models.Database;
using DatabaseLayer.Repositories.Interfaces.SpecificRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Repositories
{
    public class UserRepository : GenericRepository<UserModel>, IUserRepository
    {
        public UserRepository(DbContextClass dbContext) : base(dbContext) { }
    }
}
