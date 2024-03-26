using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Domain.IBaseModel.Components
{
    public interface IResponseBaseModel
    {
        string Result { get; set; }
        string Description { get; set; }
    }
}
