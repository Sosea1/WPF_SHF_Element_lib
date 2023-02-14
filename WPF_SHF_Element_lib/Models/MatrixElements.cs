using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SHF_Element_lib.Models
{
    public class MatrixElements : ICloneable
    {
        public string indexexOfRowAndColumn { get; set; }
        public string element { get; set; }

        public MatrixElements(string indexexOfRowAndColumn, string element)
        {
            this.indexexOfRowAndColumn = indexexOfRowAndColumn;
            this.element = element;
        }

        public object Clone()
        {
            return new MatrixElements(indexexOfRowAndColumn, element);
        }
    }
}
