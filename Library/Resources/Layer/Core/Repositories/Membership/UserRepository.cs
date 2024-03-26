using Layer.Domain.DataModels.Membership;
using Layer.Domain.IRepositories.Membership;
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
    public class UserRepository : RepoBase<User>, IUserRepository
    {
        private ISession _session { get; set; }

        public UserRepository(IAppSessionBuilder sessionBuilder) : base(sessionBuilder) 
        { 
            _session = sessionBuilder.GetSession();
        }

        public async Task<User> GetUserByName(string name) => await this.Criteria().Add(Restrictions.Eq(Projections.Property("Name"), name)).UniqueResultAsync<User>();
        public async Task<User> GetUserByEmail(string email) => await this.Criteria().Add(Restrictions.Eq(Projections.Property("Email"), email)).UniqueResultAsync<User>();
        public async Task<User> GetUserByNameOrEmail(string name, string email) => await this.Criteria().Add(
            Restrictions.Disjunction()
                .Add(Restrictions.Eq(Projections.Property("Name"), name))
                .Add(Restrictions.Eq(Projections.Property("Email"), email))
        ).UniqueResultAsync<User>();
    }
}
