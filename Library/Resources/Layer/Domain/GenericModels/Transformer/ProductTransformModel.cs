using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.GenericModels.Transformer
{
    public class ProductTransformModel { }

    public class ProductReadDtoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }

        public ProductReadDtoModel() { }
    }

    public class ProductCreateDtoModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public ProductCreateDtoModel() { }
    }

    public class ProductUpdateDtoModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public ProductUpdateDtoModel() { }
    }
}
