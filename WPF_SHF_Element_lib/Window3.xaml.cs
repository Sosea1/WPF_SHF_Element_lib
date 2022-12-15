using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using MathNet.Symbolics;

namespace WPF_SHF_Element_lib
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        // Create the interop host control.
        System.Windows.Forms.Integration.WindowsFormsHost host =
            new System.Windows.Forms.Integration.WindowsFormsHost();

        // Create the MaskedTextBox control.
        DataGridView dataGridView = new DataGridView();
        Dictionary<string, string> values = new Dictionary<string, string>();
        string group, name;
        string[] parameters;
        string file;
        string temp_values;


        List<MatrixElements> MatrixElements = new List<MatrixElements>();
        List<string> GridItems = new List<string>();

        public Window3()
        {
            InitializeComponent();
         //   grid.ItemsSource= GridItems;
            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Window2 win = new Window2();
            this.Close();
            win.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Element element = new Element();
            List<string> matrix = new List<string>();
            int a = 0;
            string[] Matrix = matrix.ToArray();
            element.AddNewElement(Data.group, Data.name, Data.parameters.ToArray(), Data.dataGrid1_Elements, Matrix);
        }
    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            

            // Assign the MaskedTextBox control as the host control's child.
            host.Child = dataGridView;
            Grid.SetColumn(host, 1);
            Grid.SetRow(host, 1);
            
            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.grid1.Children.Add(host);
            dataGridView.RowCount = Data.IndexGroup;
            dataGridView.ColumnCount = Data.IndexGroup;
            dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersVisible = false;
            for (int i = 0; i < Data.IndexGroup; i++)
            {
                dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
           
        }
    }

    public class MatrixElements
    {
        public string element;
    }


}
