using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.LibrariesModels.DataTable
{
    public class DT_requestedValue<T> where T : class
    {
        public IEnumerable<T> Model { get; set; }
        public int Count { get; set; }
    }
}
