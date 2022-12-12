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
        Window2 window2 = new Window2();
        Window3 window3 = new Window3();
        Data data = new Data();
        public List<string> elements;
        public Window1()
        {

            InitializeComponent();
        }




        public void copy_file()
        {
            File.Copy(file, AppDomain.CurrentDomain.BaseDirectory + @"\element_picture\" + Path.GetFileName(nameElement.Text) + ".png");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1_Fill();
            this.Hide();
            window2.Show();
        }

        private void Label1_Next_Click(object sender, EventArgs e)
        {
            if (values_textbox.Text != temp_values)
            {
                // dataGrid1_Fill();
            }
            temp_values = values_textbox.Text;
            group = comboBox1.SelectedItem.ToString();
            name = nameElement.Text;
            parameters = params_textbox.Text.Split(',');
            //string[] values = textBox2.Text.Split(',');
            //int i = 0;
            /* foreach (string value in values)
             {
                 dataGridView1.Rows.Add();
                 dataGridView1.Rows[i].HeaderCell.Value = value;
                 i++;
             }*/

        }


        public void dataGrid1_Fill()
        {
            elements = values_textbox.Text.Split(',').ToList();
            foreach(string element in elements)
            {
                MessageBox.Show(element);
            }
            data.elements = elements;
            foreach(string element in data.elements)
            MessageBox.Show(element);
        }
        public List<string> dataElements()
        {
            return elements;
        }
    }

}
