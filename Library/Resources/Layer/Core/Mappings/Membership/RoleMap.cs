using FluentNHibernate.Mapping;
using Layer.Core.Mappings.Common;
using Layer.Domain.DataModels.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Core.Mappings.Membership
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap() 
        {
            Table("Role");
            Id(x => x.Id, "Id").GeneratedBy.Assigned();
            Map(x => x.Name, "Name");
            Map(x => x.IsActive, "IsActive");
            Component(x => x.Record);
        }
    }
}
