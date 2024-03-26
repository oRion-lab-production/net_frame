using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Domain.IBaseModel.Components;

namespace Tools.Domain.BaseModel.Components
{
    public class ResponseBaseModel : IResponseBaseModel, ICloneable
    {
        public virtual string Result { get; set; }
        public virtual string Description { get; set; }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
