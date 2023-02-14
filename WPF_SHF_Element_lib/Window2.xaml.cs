using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using WPF_SHF_Element_lib.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MessageBox = System.Windows.MessageBox;

namespace WPF_SHF_Element_lib
{

    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : System.Windows.Window
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

            Console.WriteLine(Data.values[0]);
            Console.WriteLine(Data.parameters.Count);
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
                        
                        Data.dataGrid1_Elements.Add(new DataGrid1_Elements (Data.values[i], null));
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
                        Data.dataGrid1_Parameters.Add(new DataGrid1_Parameters(Data.parameters[i], null));
                        
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
                if (string.IsNullOrEmpty(dataGridParameters.Rows[i].Cells[0].Value.ToString()) )
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

                if (string.IsNullOrEmpty(dataGridValues.Rows[i].Cells[0].Value.ToString()))
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
                win.Owner = this.Owner;
                this.Close();
                win.ShowDialog();
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
            {"α","α"},
            {"β","β"},
            {"γ","γ"},
            {"δ","δ"},
            {"ε","ε"},
            {"η","η"},
            {"θ","θ"},
            {"λ","λ"},
            {"μ","μ"},
            {"φ","φ"},
            {"σ","σ"},
            {"ρ","ρ"},
            {"ω","ω"},
            {"Δ","Δ"},
            {"Θ","Θ"},
            {"Λ","Λ"},
            {"Ω","Ω"},
        };

        private void WrapPanel_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
                {
            Point pt = e.GetPosition(wrapOperators);
            Point pt1 = e.GetPosition(wrapSymb);
            HitTestResult result = VisualTreeHelper.HitTest(wrapOperators, pt);
            HitTestResult result1 = VisualTreeHelper.HitTest(wrapSymb, pt1);
            if (VisualTreeHelper.HitTest(wrapOperators, pt) == null && VisualTreeHelper.HitTest(wrapSymb, pt1) == null)
            { }
            else if (result != null && pt != new Point(0,0))
            {
                
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
            else if (result1 != null && pt1 != new Point(0, 0))
            {
                
                System.Windows.Controls.Button button = Data.FindParent<System.Windows.Controls.Button>(result1.VisualHit);
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
            
        }



        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            dataGridParameters.EndEdit();
            dataGridValues.EndEdit();

            int i = 0;
            foreach (DataGrid1_Parameters element in Data.dataGrid1_Parameters)
            {
                if (dataGridParameters.Rows[i].Cells[0].Value == null || string.IsNullOrEmpty(dataGridParameters.Rows[i].Cells[0].Value.ToString()))
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

                if (dataGridValues.Rows[i].Cells[0].Value == null || string.IsNullOrEmpty(dataGridValues.Rows[i].Cells[0].Value.ToString()))
                {
                    element.formulaColumn = "";
                }
                else
                element.formulaColumn = dataGridValues.Rows[i].Cells[0].Value.ToString();
                i++;
            }

           

            exit = true;
            Window1 win = new Window1();
            win.Owner = this.Owner;
            this.Close();
            win.ShowDialog();
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
    
}
