using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Domain.BaseModel.Components;

namespace Layer.Domain.GenericModels
{
    public class TransformResponse
    {
        private static string getConsValResult(ConstantValue.OperationResult result)
        {
            return result switch {
                ConstantValue.OperationResult.Failure => ConstantValue.OperationResult.Failure.ToString(),
                ConstantValue.OperationResult.Success => ConstantValue.OperationResult.Success.ToString(),
                _ => null,
            };
        }

        public class ObjectResponseVal: ResponseBaseModel
        {
            public object Datas { get; set; }

            public ObjectResponseVal() { }

            public ObjectResponseVal(ConstantValue.OperationResult resultVal, string descriptionVal, object datasVal)
            {
                this.Result = getConsValResult(resultVal);
                this.Description = descriptionVal;
                this.Datas = datasVal;
            }

            public void SetObjectResponseVal(ConstantValue.OperationResult resultVal, string descriptionVal)
            {
                this.Result = getConsValResult(resultVal);
                this.Description = descriptionVal;
            }

            public void SetObjectResponseVal(ConstantValue.OperationResult resultVal, string descriptionVal, object datasVal)
            {
                this.Result = getConsValResult(resultVal);
                this.Description = descriptionVal;
                this.Datas = datasVal;
            }

            public void SetResult(ConstantValue.OperationResult resultVal) => this.Result = getConsValResult(resultVal);

            public void SetDescription(string desciptionVal) => this.Description = desciptionVal;

            public void SetObjectVal(object datasVal) => this.Datas = datasVal;
        }
    }
}
