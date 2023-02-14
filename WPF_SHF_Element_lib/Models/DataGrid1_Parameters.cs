using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SHF_Element_lib.Models
{
    public class DataGrid1_Parameters: ICloneable
    {
        public string paramColumn { get; set; }
        public string unitColumn { get; set; }

        public DataGrid1_Parameters(string paramColumn, string unitColumn)
        {
            this.paramColumn = paramColumn;
            this.unitColumn = unitColumn;
        }

        public object Clone()
        {
            return new DataGrid1_Parameters(paramColumn, unitColumn);
        }
    }
}
