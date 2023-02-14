using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SHF_Element_lib.Models
{
    public class DataGrid1_Elements : ICloneable
    {
        public string headerColumn { get; set; }
        public string formulaColumn { get; set; }

        public DataGrid1_Elements(string headerColumn, string formulaColumn)
        {
            this.headerColumn = headerColumn;
            this.formulaColumn = formulaColumn;
        }

        public object Clone()
        {
            return new DataGrid1_Elements(this.headerColumn, this.formulaColumn);
        }
    }
}
