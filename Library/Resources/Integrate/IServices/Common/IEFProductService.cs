using EFLayer.Domain.DataModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrate.IServices.Common
{
    public interface IEFProductService
    {
        IEnumerable<Product> Get();
    }
}
