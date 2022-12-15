using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_SHF_Element_lib
{
    public static class Data
    {
        //----------Window1------------//
        public static string values_text;
        public static List<string> elements = new List<string>();
        public static List<string> temp_elements = new List<string>();
        public static bool ValuesChanged = false;
        public static string group;
        public static string name;
        public static List<string> parameters;

        //----------Window2------------//
        public static List<DataGrid1_Elements> dataGrid1_Elements = new List<DataGrid1_Elements>();
       
        //----------Window3------------//
        public static int IndexGroup;
    }
}
