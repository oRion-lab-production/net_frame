using Layer.Domain.DataModels.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.DataModels.Common
{
    public  class Product
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual double Price { get; set; }
        public virtual int Quantity { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual RecordTransaction Record { get; set; }

        public Product() 
        { 
            Id = Guid.NewGuid();
            IsActive = true;
        }    
    }
}
