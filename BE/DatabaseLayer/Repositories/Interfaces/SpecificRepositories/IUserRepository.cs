using Common.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Repositories.Interfaces.SpecificRepositories
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
    }
}
