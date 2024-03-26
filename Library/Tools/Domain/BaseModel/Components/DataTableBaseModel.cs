using Tools.Domain.IBaseModel.Components;

namespace Tools.Domain.BaseModel.Components
{
    public abstract class DataTable_getBaseModel : IDataTable_getBaseModel, ICloneable
    {
        public virtual int draw { get; set; }
        public virtual int recordsTotal { get; set; }
        public virtual int recordsFiltered { get; set; }

        public DataTable_getBaseModel()
        {
            draw = 0;
            recordsTotal = 0;
            recordsFiltered = 0;
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
