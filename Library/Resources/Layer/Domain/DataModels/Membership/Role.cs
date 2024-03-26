using Layer.Domain.DataModels.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.DataModels.Membership
{
    public class Role
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual RecordTransaction Record { get; set; }

        public Role() { 
            Id = Guid.NewGuid();
            IsActive = true;
            Record = new RecordTransaction();
        }
    }
}
