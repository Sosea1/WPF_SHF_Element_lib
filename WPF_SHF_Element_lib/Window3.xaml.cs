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

namespace WPF_SHF_Element_lib
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {

        List<MatrixElements> MatrixElements = new List<MatrixElements>();
        List<string> GridItems = new List<string>();

        public Window3()
        {
            InitializeComponent();
            for (int i = 0; i < Data.IndexGroup; i++)
            {
                DataGridTextColumn column = new DataGridTextColumn()
                {
                    Header = ""
                };
                
               // grid.Columns.Add(column);
              //  GridItems.Add(i.ToString());
            }
            for(int a = 0;a< Data.IndexGroup* Data.IndexGroup;a++)
            {
                MatrixElements.Add(new MatrixElements { element = a.ToString()});
            }
         //   grid.ItemsSource= GridItems;
            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int a = 0;
            for(int i = 0; i < Data.IndexGroup; i++)
            {
                for(int j =0; j< Data.IndexGroup; j++)
                {
                 //   var x = grid.Columns[i].GetCellContent(grid.Items[j]) as TextBlock;
                 //   MatrixElements[a].element = x.Text;
                  //B  System.Windows.MessageBox.Show(x.Text);
                }
                
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
          DataGridView dataGridView = new DataGridView();

            // Assign the MaskedTextBox control as the host control's child.
            host.Child = dataGridView;
            Grid.SetColumn(host, 1);
            Grid.SetRow(host, 1);
            
            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.grid1.Children.Add(host);
            dataGridView.RowCount = 4;
            dataGridView.ColumnCount = 4;
            dataGridView.BackgroundColor = System.Drawing.Color.White;
        }
    }

    public class MatrixElements
    {
        public string element;
    }


}
