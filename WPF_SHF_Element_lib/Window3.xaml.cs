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
using MessageBox = System.Windows.MessageBox;

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
        List<MatrixElements> matrixElements= new List<MatrixElements>();
        List<string> GridItems = new List<string>();

        public Window3()
        {
            InitializeComponent();
         //   grid.ItemsSource= GridItems;
            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Data.matrixElements.Clear();
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    Data.matrixElements.Add(new MatrixElements { column = i, element = dataGridView.Rows[j].Cells[i].Value == null ? null : dataGridView.Rows[j].Cells[i].Value.ToString() });
                }
            }
            Window2 win = new Window2();
            this.Close();
            win.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGridView.EndEdit();
            Data.matrixElements.Clear();
            Element element = new Element();
            bool f = false;
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    //matrixElements.Add(new MatrixElements { column = i, element= dataGridView[j, i].Value == null ? null : dataGridView[j, i].Value.ToString()});
                    if (dataGridView.Rows[j].Cells[i].Value == null)
                    {
                        f = true;
                        break;
                    }
                    else
                    Data.matrixElements.Add(new MatrixElements { column = i, element = dataGridView.Rows[j].Cells[i].Value.ToString() });
                }
                if(f) break;
            }
            if (f) MessageBox.Show("Не все данные были введены");
            else
            element.AddNewElement(Data.fileName, Data.group, Data.name, Data.parameters.ToArray(), Data.dataGrid1_Elements, Data.matrixElements);
            
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
            dataGridView.RowCount = Data.IndexGroup+1;
            dataGridView.ColumnCount = Data.IndexGroup;
            dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersVisible = false;
            for (int i = 0; i < Data.IndexGroup; i++)
            {
                dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dataGridView.AllowUserToAddRows= false;
            int a = 0;
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    Data.matrixElements.Add(new MatrixElements { column = i, element = dataGridView.Rows[j].Cells[i].Value == null ? null : dataGridView.Rows[j].Cells[i].Value.ToString() });
                    if(Data.matrixElements[a].element != null) dataGridView.Rows[j].Cells[i].Value = Data.matrixElements[a].element;
                    a++;
                }
            }

        }
    }

    public class MatrixElements
    {
        public int column { get; set; }
        public string element { get; set; }
    }


}
