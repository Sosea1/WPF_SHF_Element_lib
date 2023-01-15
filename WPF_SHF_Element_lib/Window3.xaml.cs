using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using System.IO;
using AngouriMath;
using System.Windows.Media;

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
        bool exit= false;
        bool keyHandler = false;
        string tempCellValue;
        Entity formulaRaw;
        int rowIndex, columnIndex;

        public Window3()
        {
            InitializeComponent();
            //   grid.ItemsSource= GridItems;
          


        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            exit = true;    
            Data.matrixElements.Clear();
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    Data.matrixElements.Add(new MatrixElements { indexexOfRowAndColumn = i + "," + j, element = dataGridView.Rows[j].Cells[i].Value == null ? null : dataGridView.Rows[j].Cells[i].Value.ToString() });
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
                    Data.matrixElements.Add(new MatrixElements { indexexOfRowAndColumn = i+","+j, element = dataGridView.Rows[j].Cells[i].Value.ToString() });
                }
                if(f) break;
            }
            if (f) MessageBox.Show("Не все данные были введены");
            else
            {
               
                try
                {

                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\ImageElements\") == true)
                    {
                        if (Data.ImagePath == null)
                        {
                            element.AddNewElement(Data.fileName, Data.group, Data.name, Data.dataGrid1_Parameters, Data.dataGrid1_Elements, Data.matrixElements, Data.ImagePath);
                        }
                        else
                        {
                            element.AddNewElement(Data.fileName, Data.group, Data.name, Data.dataGrid1_Parameters, Data.dataGrid1_Elements, Data.matrixElements, Data.ImagePath);
                            File.Copy(Data.ImagePath, AppDomain.CurrentDomain.BaseDirectory + @"\ImageElements\" + Data.name + ".png");
                        }

                    }
                    else
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\ImageElements\");
                        File.Copy(Data.ImagePath, AppDomain.CurrentDomain.BaseDirectory + @"\ImageElements\" + Data.name + ".png");
                        element.AddNewElement(Data.fileName, Data.group, Data.name, Data.dataGrid1_Parameters, Data.dataGrid1_Elements, Data.matrixElements, Data.ImagePath);
                    }
                   
                    MessageBoxResult addElementMess = MessageBox.Show("Данные успешно добавлены!\nХотите добавить еще 1 элемент?", "Внимание!",MessageBoxButton.YesNo);
                    if (addElementMess == MessageBoxResult.Yes)
                    {
                        exit = true;
                        Data.clearData();
                        Window1 window1 = new Window1();
                        this.Close();
                        window1.Show();
                    }
                    else
                    {   
                        Data.clearData();
                        exit = true;
                        this.Close();
                    }

                }
                catch (Exception q) 
                {
                    MessageBox.Show(q.Message);
                }
            }
            
        }
    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dataGridView.EditingControlShowing += DataGridView_EditingControlShowing;
            //dataGridView.CellBeginEdit += DataGridView_CellBeginEdit;
            //dataGridView.CellEndEdit += DataGridView_CellEndEdit;

            // это латекс парсер и отображение

            // Assign the MaskedTextBox control as the host control's child.
            host.Child = dataGridView;
            
            
            Grid.SetColumn(host, 1);
            Grid.SetRow(host, 1);
            
            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.grid1.Children.Add(host);
            dataGridView.RowCount = Data.IndexGroup+1;
            dataGridView.DefaultCellStyle.Font=new System.Drawing.Font("Vardana", 12.5F,System.Drawing.GraphicsUnit.Pixel);
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
                    Data.matrixElements.Add(new MatrixElements { indexexOfRowAndColumn = i + "," + j, element = dataGridView.Rows[j].Cells[i].Value == null ? null : dataGridView.Rows[j].Cells[i].Value.ToString() });
                    if(Data.matrixElements[a].element != null) dataGridView.Rows[j].Cells[i].Value = Data.matrixElements[a].element;
                    a++;
                }
            }
            dataGridView.CellMouseClick += DataGridView_CellMouseClick;

        }

        private void DataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            rowIndex = dataGridView.CurrentCell.RowIndex;
            columnIndex = dataGridView.CurrentCell.ColumnIndex;
        }

        private void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {   
            if (dataGridView.CurrentCell.Value != null)
                tempCellValue = dataGridView.CurrentCell.Value.ToString();
            DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
            tb.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
            
        
        }
        private void dataGridViewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

                tempCellValue = dataGridView.CurrentCell.EditedFormattedValue.ToString();

            if (string.IsNullOrEmpty(tempCellValue) == true)
            {

            }
            else
            {
             
             //latexFormula.Formula = tempCellValue.Latexise();
            }
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            keyHandler = false;
        }

        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            keyHandler = true;
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
                        e.Cancel = true;
                }
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
            {"x!","x!"},
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
               if(operators.TryGetValue(button.Content.ToString(), out string value))
                {
                    dataGridView.EndEdit();
                    dataGridView.Rows[rowIndex].Cells[columnIndex].Value += value;
                }
            }
        }
    }

    public class MatrixElements
    {
        public string indexexOfRowAndColumn { get; set; }
        public string element { get; set; }
    }
}
