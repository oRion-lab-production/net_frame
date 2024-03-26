using Layer.Domain.DataModels.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.DataAccess;

namespace Layer.Domain.IRepositories.Membership
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetRoleByName(string name);
    }
}
