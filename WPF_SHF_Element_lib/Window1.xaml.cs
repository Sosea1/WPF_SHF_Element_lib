using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

namespace WPF_SHF_Element_lib
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //Dictionary<string, string> values = new Dictionary<string, string>();
        string file;

        public Window1()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(Data.values_text) == false) values_textbox.Text = Data.values_text;
            if (string.IsNullOrEmpty(Data.group) == false) comboBox1.Text = Data.group;
            if (string.IsNullOrEmpty(Data.name) == false) nameElement.Text = Data.name;
            if (string.IsNullOrEmpty(Data.parametersText) == false) params_textbox.Text = Data.parametersText;
        }

        public void copy_file()
        {
            File.Copy(file, AppDomain.CurrentDomain.BaseDirectory + @"\element_picture\" + Path.GetFileName(nameElement.Text) + ".png");
        }
            
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: Data.fileName = "2pole.json";
                break;
                case 1: Data.fileName = "4pole.json";
                break;
                case 2: Data.fileName = "6pole.json";
                break;
                case 3: Data.fileName = "8pole.json";
                break;
            }

            Data.IndexGroup = comboBox1.SelectedIndex + 1;
            if (values_textbox.Text != Data.values_text)
            {
                dataGrid1_Fill();
                Data.values_text = values_textbox.Text;
                Data.ValuesChanged = true;
            }
            else
            {
                Data.ValuesChanged = false;
            }
            Data.group = comboBox1.Text;
            Data.name = nameElement.Text;
            Data.parameters = params_textbox.Text.Split(',').ToList();
            Data.parametersText = params_textbox.Text;
            this.Close();
            Window2 window2 = new Window2();
            window2.Show();
        }

        public void dataGrid1_Fill()
        {
            Data.elements = values_textbox.Text.Split(',').ToList();
        }
    }

}
