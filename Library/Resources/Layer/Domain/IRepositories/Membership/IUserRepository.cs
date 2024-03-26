using Layer.Domain.DataModels.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.DataAccess;

namespace Layer.Domain.IRepositories.Membership
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByName(string name);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByNameOrEmail(string name, string email);
    }
}
