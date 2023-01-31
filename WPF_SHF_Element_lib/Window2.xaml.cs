using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;

namespace WPF_SHF_Element_lib
{

    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        // Create the interop host control.
        System.Windows.Forms.Integration.WindowsFormsHost host =
            new System.Windows.Forms.Integration.WindowsFormsHost();
        System.Windows.Forms.Integration.WindowsFormsHost host1 =
            new System.Windows.Forms.Integration.WindowsFormsHost();

        // Create the MaskedTextBox control.
        DataGridView dataGridValues = new DataGridView();
        DataGridView dataGridParameters = new DataGridView();
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
            host.Child = dataGridValues;
            host1.Child = dataGridParameters;
            Grid.SetColumnSpan(host, 3);
            Grid.SetColumnSpan(host1, 3);
            Grid.SetRow(host, 1);
            Grid.SetRow(host1, 0);
            this.win3.Children.Add(host);
            this.win3.Children.Add(host1);

            dataGridValues.ColumnCount = 1;
            dataGridValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridValues.RowHeadersVisible = true;
            dataGridValues.SelectionMode= DataGridViewSelectionMode.CellSelect;
            dataGridValues.AllowUserToDeleteRows = false;
            dataGridValues.AllowUserToAddRows = false;
            dataGridValues.BackgroundColor = System.Drawing.Color.White;
            dataGridValues.Columns[0].HeaderText = "Формула";

            dataGridParameters.ColumnCount = 1;
            dataGridParameters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridParameters.RowHeadersVisible = true;
            dataGridParameters.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridParameters.AllowUserToDeleteRows = false;
            dataGridParameters.AllowUserToAddRows = false;
            dataGridParameters.BackgroundColor = System.Drawing.Color.White;
            dataGridParameters.Columns[0].HeaderText = "Единица измерения";

            dataGridValues.CellMouseClick += DataGridView_CellMouseClick;
           

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
                    for (int i = 0; i < Data.values.Count; i++)
                    {
                        
                        Data.dataGrid1_Elements.Add(new DataGrid1_Elements { headerColumn = Data.values[i] });
                    }
                  
                    Data.ValuesChanged = false;
                }
            }
            if (!f)
            {
                for (int i=0;i< Data.values.Count;i++) 
                {
                    
                    dataGridValues.Rows.Add();                    
                    dataGridValues.Rows[i].HeaderCell.Value=Data.values[i];
                    dataGridValues.Rows[i].Cells[0].Value = Data.dataGrid1_Elements[i].formulaColumn;
                }
            }   
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
                    for (int i = 0; i < Data.parameters.Count; i++)
                    {
                        Data.dataGrid1_Parameters.Add(new DataGrid1_Parameters { paramColumn = Data.parameters[i] });
                        
                    }
                    Data.ParamsChanged = false;
                }
            }
            if (!f)
            {
                    for (int i = 0; i < Data.parameters.Count; i++)
                    {
                         dataGridParameters.Rows.Add();
                        dataGridParameters.Rows[i].HeaderCell.Value = Data.parameters[i];
                        dataGridParameters.Rows[i].Cells[0].Value = Data.dataGrid1_Parameters[i].unitColumn;
                    }
            } 
                

        }


        private void DataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridValues.BeginEdit(false);
        }







        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            dataGridParameters.EndEdit();
            dataGridValues.EndEdit();
           
            int i = 0;
            bool f = false;
            
            foreach (DataGrid1_Parameters element in Data.dataGrid1_Parameters)
            {
                if (dataGridParameters.Rows[i].Cells[0].Value == null)
                {
                    f = true;
                    break;
                }
                element.unitColumn = dataGridParameters.Rows[i].Cells[0].Value.ToString();
                i++;

               
            }
            i = 0;

            
            foreach (DataGrid1_Elements element in Data.dataGrid1_Elements)
            {

                if (dataGridValues.Rows[i].Cells[0].Value == null)
                {
                    f = true;
                    break;
                }
                element.formulaColumn = dataGridValues.Rows[i].Cells[0].Value.ToString();
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

        private Dictionary<string, string> operators = new Dictionary<string, string>()
        {
            {"+","+"},
            {"-","-"},
            {"∙","*"},
            {"/","/"},
            {"√x","sqrt()"},
            {"xⁿ","^()"},
            {"|x|","||"},
            {"x!","!"},
            {"()","()"},
            {"sin(x)","sin()"},
            {"cos(x)","cos()"},
            {"tan(x)","tan()"},
            {"cot(x)","cot()"},
            {"˚","˚"},
        };

        private void WrapPanel_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(wrapOperators);
            HitTestResult result = VisualTreeHelper.HitTest(wrapOperators, pt);
            System.Windows.Controls.Button button = Data.FindParent<System.Windows.Controls.Button>(result.VisualHit);
            if (button != null)
            {
                if (dataGridValues.SelectedCells.Count == 0)
                { }
                else
                {
                    var temp = ((System.Windows.Forms.TextBox)dataGridValues.EditingControl).SelectionStart;
                    if (operators.TryGetValue(button.Content.ToString(), out string value))
                    {
                        int pos = 0;
                        if (value.Length == 5) pos = 4;
                        else if (value.Length == 6) pos = 5;
                        else if (value.Length == 1) pos = 1;
                        else if (value.Length == 2) pos = 1;
                        else if (value.Length == 3) pos = 2;
                        else { }
                        ((System.Windows.Forms.TextBox)dataGridValues.EditingControl).SelectedText = value;
                        dataGridValues.EndEdit();
                        dataGridValues.BeginEdit(false);
                        ((System.Windows.Forms.TextBox)dataGridValues.EditingControl).SelectionStart = temp + pos;
                    }
                }
            }
        }



        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (DataGrid1_Parameters element in Data.dataGrid1_Parameters)
            {
                if (dataGridParameters.Rows[i].Cells[0].Value == null)
                {
                    element.unitColumn = "";
                }
                else
                element.unitColumn = dataGridParameters.Rows[i].Cells[0].Value.ToString();
                i++;


            }
            i = 0;


            foreach (DataGrid1_Elements element in Data.dataGrid1_Elements)
            {

                if (dataGridValues.Rows[i].Cells[0].Value == null)
                {
                    element.formulaColumn = "";
                }
                else
                element.formulaColumn = dataGridValues.Rows[i].Cells[0].Value.ToString();
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
