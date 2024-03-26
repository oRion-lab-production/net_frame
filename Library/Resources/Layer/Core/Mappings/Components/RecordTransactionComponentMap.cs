using FluentNHibernate.Mapping;
using Layer.Domain.DataModels.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Core.Mappings.Components
{
    public class RecordTransactionComponentMap : ComponentMap<RecordTransaction>
    {
        public RecordTransactionComponentMap()
        {
            Map(c => c.CreatedBy, "CreatedBy");
            Map(c => c.CreatedDateTime, "CreatedDt");
            Map(c => c.ModifiedBy, "ModifiedBy");
            Map(c => c.ModifiedDateTime, "ModifiedDt");
        }
    }
}
