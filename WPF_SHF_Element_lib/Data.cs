using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        public static List<string> parameters = new List<string>();
        public static string parametersText;
        public static int IndexGroup;
        public static string ImagePath;

        //----------Window2------------//
        public static List<DataGrid1_Elements> dataGrid1_Elements = new List<DataGrid1_Elements>();

        //----------Window3------------//
        public static List<MatrixElements> matrixElements = new List<MatrixElements>();
        public static string fileName;
        public static void clearData()
        {
           values_text = null;
            elements = new List<string>();
            temp_elements = new List<string>();
            ValuesChanged = false;
            group = null;
            name = null;
            parameters = new List<string>();
            parametersText = null;
            IndexGroup = 0;
            dataGrid1_Elements = new List<DataGrid1_Elements>();
            matrixElements = new List<MatrixElements>();
            fileName = null;
            ImagePath = null;
        }
        public static T FindParent<T>(DependencyObject child) where T: DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
    }
}
