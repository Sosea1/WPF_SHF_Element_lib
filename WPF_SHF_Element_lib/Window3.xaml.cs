﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using System.IO;
using AngouriMath;
using System.Windows.Media;
using WPF_SHF_Element_lib.Models;

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
            dataGridView.EndEdit();
            exit = true;    
            Data.matrixElements.Clear();
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    Data.matrixElements.Add(new MatrixElements(i + "," + j, dataGridView.Rows[j].Cells[i].Value == null ? null : dataGridView.Rows[j].Cells[i].Value.ToString()));
                }
            }
            Window2 win = new Window2();
            win.Owner = this.Owner;
            this.Close();
            win.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGridView.EndEdit();
            Data.matrixElements.Clear();
            Element element = new Element();
            bool f = false;
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                {
                    //matrixElements.Add(new MatrixElements { column = i, element= dataGridView[j, i].Value == null ? null : dataGridView[j, i].Value.ToString()});
                    if (dataGridView.Rows[i].Cells[j].Value == null)
                    {
                        f = true;
                        break;
                    }
                    else
                    Data.matrixElements.Add(new MatrixElements (i + "," + j, dataGridView.Rows[i].Cells[j].Value.ToString()));
                }
                if(f) break;
            }
            if (f) MessageBox.Show("Не все данные были введены");
            else
            {
               
                try
                {
                    if (Data.ImagePath == null)
                    {
                        element.AddNewElement(Data.fileName, Data.group, Data.name, Data.dataGrid1_Parameters, Data.dataGrid1_Elements, Data.matrixElements, Data.ImagePath);
                    }
                    else
                    {
                        if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\ImageElements\") == true)
                        {
                            element.AddNewElement(Data.fileName, Data.group, Data.name, Data.dataGrid1_Parameters, Data.dataGrid1_Elements, Data.matrixElements, @"\ImageElements\" + Data.name + ".png");
                            File.Copy(Data.ImagePath, AppDomain.CurrentDomain.BaseDirectory + @"\ImageElements\" + Data.name + ".png");
                        }
                        else
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\ImageElements\");
                            File.Copy(Data.ImagePath, AppDomain.CurrentDomain.BaseDirectory + @"\ImageElements\" + Data.name + ".png");
                            element.AddNewElement(Data.fileName, Data.group, Data.name, Data.dataGrid1_Parameters, Data.dataGrid1_Elements, Data.matrixElements, @"\ImageElements\" + Data.name + ".png");
                        }
                    }

                    MessageBoxResult addElementMess = MessageBox.Show("Данные успешно добавлены!\nХотите добавить еще 1 элемент?", "Внимание!",MessageBoxButton.YesNo);
                    if (addElementMess == MessageBoxResult.Yes)
                    {
                        exit = true;
                        Data.clearData();
                        Window1 window1 = new Window1();
                        window1.Owner = this.Owner;
                        this.Close();
                        window1.ShowDialog();
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
            dataGridView.CellMouseClick += DataGridView_CellMouseClick;

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
                    Data.matrixElements.Add(new MatrixElements (i + "," + j, dataGridView.Rows[j].Cells[i].Value == null ? null : dataGridView.Rows[j].Cells[i].Value.ToString()));
                    if(Data.matrixElements[a].element != null) dataGridView.Rows[j].Cells[i].Value = Data.matrixElements[a].element;
                    a++;
                }
            }
            dataGridView.ClearSelection();

        }

        private void DataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            rowIndex = dataGridView.CurrentCell.RowIndex;
            columnIndex = dataGridView.CurrentCell.ColumnIndex;
            dataGridView.BeginEdit(false);
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
            dataGridView.BeginEdit(false);
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
            {"x!","!"},
            {"()","()"},
            {"sin(x)","sin()"},
            {"cos(x)","cos()"},
            {"tan(x)","tan()"},
            {"cot(x)","cotan()"},
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
            HitTestResult result = VisualTreeHelper.HitTest(wrapOperators, pt);
            System.Windows.Controls.Button button = Data.FindParent<System.Windows.Controls.Button>(result.VisualHit);
            if (button != null)
            {
                if (dataGridView.SelectedCells.Count == 0)
                { }
                else
                { 
                int temp = ((System.Windows.Forms.TextBox)dataGridView.EditingControl).SelectionStart;

                if (operators.TryGetValue(button.Content.ToString(), out string value))
                    {
                        int pos = 0;
                        if (value.Length == 5) pos = 4;
                        else if (value.Length == 6) pos = 5;
                        else if (value.Length == 1) pos = 1;
                        else if (value.Length == 2) pos = 1;
                        else if (value.Length == 3) pos = 2;
                        else { }
                        ((System.Windows.Forms.TextBox)dataGridView.EditingControl).SelectedText = value;
                        dataGridView.EndEdit();
                        dataGridView.BeginEdit(false);
                        ((System.Windows.Forms.TextBox)dataGridView.EditingControl).SelectionStart = temp + pos;
                    
                    }
                }
            }         
        }
    }

  
}
