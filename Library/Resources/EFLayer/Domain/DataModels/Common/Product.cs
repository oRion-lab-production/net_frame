using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLayer.Domain.DataModels.Common
{
    [Table("Product")]
    public partial class Product
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int Quantity { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDt { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime ModifiedDt { get; set; }

        public Product()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            CreatedBy = "system";
            CreatedDt = DateTime.Now;
            ModifiedDt = DateTime.Now;
        }
    }
}
