using Layer.Domain.DataModels.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.DataModels.Membership
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual Role Role { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual RecordTransaction Record { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            Record = new RecordTransaction();
        }
    }
}
