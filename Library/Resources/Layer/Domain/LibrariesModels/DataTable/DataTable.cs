using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Domain.BaseModel.Components;

namespace Layer.Domain.LibrariesModels.DataTable
{
    #region DataTable post model -> post data from client side to server side for datatable request
    public class DataTable_postModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public SearchDataTable_postModel search { get; set; }
        public IList<OrderDataTable_postModel> order { get; set; }
        public IList<ColumnDataTable_postModel> columns { get; set; }
    }

    public class SearchDataTable_postModel
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

    public class OrderDataTable_postModel
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class ColumnDataTable_postModel
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public SearchDataTable_postModel search { get; set; }
    }
    #endregion

    #region DataTable get model -> get data from server side to client side for datatable rendering data
    //Passing get model only with data without parameter
    public class DataTable_getModel : DataTable_getBaseModel
    {
        public IList<string[]> data { get; set; }

        public DataTable_getModel()
        {
            data = new List<string[]>();
        }

        public void AddData(string[] dataSrc)
        {
            if (this.data == null)
                this.data = new List<string[]>();

            if (dataSrc != null && dataSrc.Any())
                this.data.Add(dataSrc);
        }

        public void AddDataList(List<string[]> dataSrcList)
        {
            if (dataSrcList != null && dataSrcList.Any())
                dataSrcList.ForEach(x => this.AddData(x));
        }
    }

    //Passing get model only with data with parameter with value for every <tr>
    public class DataTable_getModel_Parameter : DataTable_getBaseModel
    {
        public ICollection<Dictionary<string, dynamic>> data { get; set; }

        public DataTable_getModel_Parameter()
        {
            data = new Collection<Dictionary<string, dynamic>>();
        }

        public void AddData(Dictionary<string, dynamic> dataSrc, DataTable_getModel_AdditionData additionData = null)
        {
            if (this.data == null)
                this.data = new Collection<Dictionary<string, dynamic>>();

            if (dataSrc != null && dataSrc.Any()) {
                if (additionData != null) {
                    if (!string.IsNullOrEmpty(additionData.DT_RowId))
                        dataSrc.Add(nameof(additionData.DT_RowId), additionData.DT_RowId);

                    if (!string.IsNullOrEmpty(additionData.DT_RowClass))
                        dataSrc.Add(nameof(additionData.DT_RowClass), additionData.DT_RowClass);

                    if (additionData.DT_RowData != null)
                        dataSrc.Add(nameof(additionData.DT_RowData), additionData.DT_RowData);

                    if (additionData.DT_RowAttr != null)
                        dataSrc.Add(nameof(additionData.DT_RowAttr), additionData.DT_RowAttr);
                }

                this.data.Add(dataSrc);
            }
        }

        public void AddDataList(List<Dictionary<string, dynamic>> dataSrcList)
        {
            if (dataSrcList != null && dataSrcList.Any())
                dataSrcList.ForEach(x => this.AddData(x));
        }

        public void AddDataList(List<Dictionary<string, dynamic>> dataSrcList, DataTable_getModel_AdditionData additionData)
        {
            if (dataSrcList != null && dataSrcList.Any())
                dataSrcList.ForEach(x => this.AddData(x, additionData));
        }

        public void AddDataList(List<Dictionary<string, dynamic>> dataSrcList, List<DataTable_getModel_AdditionData> additionData)
        {
            if (dataSrcList != null && dataSrcList.Any())
                dataSrcList.ForEach(x => this.AddData(x, additionData[dataSrcList.IndexOf(x)]));
        }
    }

    public class DataTable_getModel_AdditionData
    {
        public string DT_RowId { get; set; }
        public string DT_RowClass { get; set; }
        public DataTable_getModel_AdditionData_RowData DT_RowData { get; set; }
        public DataTable_getModel_AdditionData_RowAttr DT_RowAttr { get; set; }
    }

    public class DataTable_getModel_AdditionData_RowData
    {
        public int pkey { get; set; }
    }

    public class DataTable_getModel_AdditionData_RowAttr
    {
        //<tr> attr class
    }
    #endregion
}
