using Layer.Domain.DataModels.Membership;
using Layer.Domain.IRepositories.Membership;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using NHibernate.Criterion;
using Structures.Conn_nHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Core.Repositories.Membership
{
    public class RoleRepository : RepoBase<Role>, IRoleRepository
    {
        private ISession _session { get; set; }

        public RoleRepository(IAppSessionBuilder sessionBuilder) : base(sessionBuilder) 
        {
            _session = sessionBuilder.GetSession();
        }

        public async Task<Role> GetRoleByName(string name) => await this.Criteria().Add(Restrictions.Eq(Projections.Property("Name"), name)).UniqueResultAsync<Role>();
    }
}
