using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.DataModels.Components
{
    public class RecordTransaction
    {
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }

        public RecordTransaction()
        {
            CreatedBy = "system";
            CreatedDateTime = DateTime.Now;
            ModifiedDateTime = DateTime.Now;
        }
    }
}
