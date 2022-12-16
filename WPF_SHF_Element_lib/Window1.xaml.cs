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
        string group, name;
        string[] parameters;
        string file;
        string temp_values;
        
        public List<string> elements;
        public Window1()
        {
            InitializeComponent();
            if (Data.values_text != null) values_textbox.Text = Data.values_text;
        }

        public void copy_file()
        {
            File.Copy(file, AppDomain.CurrentDomain.BaseDirectory + @"\element_picture\" + Path.GetFileName(nameElement.Text) + ".png");
        }
            
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Data.IndexGroup = comboBox1.SelectedIndex + 1;
            MessageBox.Show(Data.IndexGroup.ToString());
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
            this.Close();
            Window2 window2 = new Window2();
            window2.Show();
        }

        public void dataGrid1_Fill()
        {
            elements = values_textbox.Text.Split(',').ToList();
            Data.elements = elements;
        }
        public List<string> dataElements()
        {
            return elements;
        }
    }

}
