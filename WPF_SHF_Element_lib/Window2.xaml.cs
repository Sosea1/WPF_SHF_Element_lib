using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace WPF_SHF_Element_lib
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            grid.CanUserAddRows= false;
           
            //grid.ItemsSource = elements;
        }

        List<string> elements = new List<string>();
        List<DataGrid1_Elements> dataGrid1_Elements = new List<DataGrid1_Elements>();
        List<string> temp_values = new List<string>();

        public void dataGrid1_Fill()
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Data.ValuesChanged == true)
            {
                Data.dataGrid1_Elements.Clear();
                foreach (string element in Data.elements)
                {
                    Data.dataGrid1_Elements.Add(new DataGrid1_Elements { headerColumn = element });
                }
                Data.temp_elements = Data.elements;
                Data.ValuesChanged = false;
            }
            grid.ItemsSource = Data.dataGrid1_Elements;
        }


        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (DataGrid1_Elements element in dataGrid1_Elements)
            {
                var x = grid.Columns[1].GetCellContent(grid.Items[i]) as TextBlock;
                element.formulaColumn = x.Text;
                i++;
            }
            Data.dataGrid1_Elements = dataGrid1_Elements;
            this.Hide();
            Window3 win = new Window3();
            win.Show();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (DataGrid1_Elements element in Data.dataGrid1_Elements)
            {
                var x = grid.Columns[1].GetCellContent(grid.Items[i]) as TextBlock;
                element.formulaColumn = x.Text;
                i++;
            }
            Window1 win = new Window1();
            this.Hide();
            win.Show();
        }
    }

    public class DataGrid1_Elements
    {
        public string headerColumn { get; set; } 
        public string formulaColumn { get; set;}
    }
}
