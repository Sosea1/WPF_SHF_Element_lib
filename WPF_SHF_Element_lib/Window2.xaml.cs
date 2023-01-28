using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPF_SHF_Element_lib
{

    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        bool exit = false;
        public Window2()
        {
            InitializeComponent();
           
        }

        List<string> elements = new List<string>();
        List<DataGrid1_Elements> dataGrid1_Elements = new List<DataGrid1_Elements>();
        List<string> temp_values = new List<string>();

        public void dataGrid1_Fill()
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            bool f = false;
            if(Data.ValuesChanged == true)
            {
                Data.dataGrid1_Elements.Clear();
                if(string.IsNullOrEmpty(Data.values_text)==true)
                {
                    f = true;
                }
                else
                {
                    foreach (string element in Data.elements)
                    {
                        Console.WriteLine(element);
                        Data.dataGrid1_Elements.Add(new DataGrid1_Elements { headerColumn = element });
                    }
                  
                    Data.ValuesChanged = false;
                }
            }
            if (!f) 
            grid.ItemsSource = Data.dataGrid1_Elements;



            f = false;
            if (Data.ParamsChanged == true)
            {
                Data.dataGrid1_Parameters.Clear();
                if (string.IsNullOrEmpty(Data.parametersText) == true)
                {
                    f = true;
                }
                else
                {
                    foreach (string parameter in Data.parameters)
                    {
                        Data.dataGrid1_Parameters.Add(new DataGrid1_Parameters { paramColumn = parameter });
                    }
                 
                    Data.ParamsChanged = false;
                }
            }
            if (!f)
                grid1.ItemsSource = Data.dataGrid1_Parameters;
        }
    









        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            bool f = false;
            foreach (DataGrid1_Elements element in Data.dataGrid1_Elements)
            {
                var x = grid.Columns[1].GetCellContent(grid.Items[i]) as TextBlock;
                if (string.IsNullOrEmpty(x.Text))
                {
                    f = true;
                    break;
                }
                element.formulaColumn = x.Text;
                i++;
            }
            i = 0;
            foreach (DataGrid1_Parameters element in Data.dataGrid1_Parameters)
            {
                var x = grid1.Columns[1].GetCellContent(grid1.Items[i]) as TextBlock;
                if (string.IsNullOrEmpty(x.Text))
                {
                    f = true;
                    break;
                }
                element.unitColumn = x.Text;
                i++;
            }
            if (f)
            {
                MessageBox.Show("Не все формулы были введены");
            }
            else
            {
                exit = true;
                Window3 win = new Window3();
                this.Close();
                win.Show();
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            
            foreach (DataGrid1_Elements element in Data.dataGrid1_Elements)
            {

                var x = grid.Columns[1].GetCellContent(grid.Items[i]) as TextBlock;
                if (string.IsNullOrEmpty(x.Text))
                {
                    element.formulaColumn = "";
                }
                else
                {
                    element.formulaColumn = x.Text;
                }
                
                i++;
            }
            i = 0;
            foreach (DataGrid1_Parameters element in Data.dataGrid1_Parameters)
            {

                var x = grid1.Columns[1].GetCellContent(grid1.Items[i]) as TextBlock;
                if (string.IsNullOrEmpty(x.Text))
                {
                    element.unitColumn = "";
                }
                else
                {
                    element.unitColumn = x.Text;
                }

                i++;
            }

            exit = true;
            Window1 win = new Window1();
            this.Close();
            win.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
            {
                {
                    MessageBoxResult res = MessageBox.Show("Вы уверены что хотите выйти?", "Внимание!", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        Data.clearData();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }

    public class DataGrid1_Elements
    {
        public string headerColumn { get; set; } 
        public string formulaColumn { get; set;}
    }
    public class DataGrid1_Parameters
    {
        public string paramColumn { get; set; }
        public string unitColumn { get; set; }
    }
}
