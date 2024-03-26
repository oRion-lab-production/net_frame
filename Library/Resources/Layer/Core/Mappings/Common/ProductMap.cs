using FluentNHibernate.Mapping;
using Layer.Domain.DataModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Core.Mappings.Common
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap() 
        {
            Table("Product");
            Id(x => x.Id, "Id").GeneratedBy.Assigned();
            Map(x => x.Name, "Name");
            Map(x => x.Description, "Description");
            Map(x => x.Price, "Price");
            Map(x => x.Quantity, "Quantity");
            Map(x => x.IsActive, "IsActive");
            Component(x => x.Record);
        }
    }
}
