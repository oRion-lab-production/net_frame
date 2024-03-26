using FluentNHibernate.Mapping;
using Layer.Domain.DataModels.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Core.Mappings.Membership
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id, "Id").GeneratedBy.Assigned();
            Map(x => x.Name, "Name");
            Map(x => x.Email, "Email");
            Map(x => x.Password, "Password");
            References(x => x.Role).Column("RoleId");
            Map(x => x.IsActive, "IsActive");
            Component(x => x.Record);
        }
    }
}
