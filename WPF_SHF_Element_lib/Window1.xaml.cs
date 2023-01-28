using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;
using MessageBox = System.Windows.MessageBox;
using AngouriMath;
using WpfMath;
using WpfMath.Controls;

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
            comboBox1.SelectedIndex= 0;
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

            Data.group = comboBox1.Text;
            Data.name = nameElement.Text;
            
            if (string.IsNullOrEmpty(nameElement.Text) == true)
            {
                MessageBox.Show("Введите имя для элемента");
            }
            else  if (Element.searchName()==true)
                MessageBox.Show("Элемент с таким именем уже существует");
            else
            {   Data.IndexGroup = comboBox1.SelectedIndex + 1;
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
                if (params_textbox.Text != Data.parametersText)
                {
                    Data.parameters = (params_textbox.Text.Replace(" ","")).Split(',').ToList();
                    Data.parametersText = params_textbox.Text;
                    Data.ParamsChanged = true;
                }
                else
                {
                    Data.ParamsChanged = false;
                }

                this.Close();
                Window2 window2 = new Window2();
                window2.Show();
               
            }
            if (string.IsNullOrEmpty(Data.ImagePath)) { Data.ImagePath = null; }

          
        }

        public void dataGrid1_Fill()
        {
            Data.elements = values_textbox.Text.Split(',').ToList();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           var choose_pic = new OpenFileDialog();
            choose_pic.Filter = "Images files |*.jpg;*.jpeg;*.png;*.svg;*.ico; | All files | *.*";

            if (choose_pic.ShowDialog() == true)
            {
                file = choose_pic.FileName;

                selectedImage.Source = new BitmapImage( new Uri(file));
                Data.ImagePath = file;
            }
        }
    }

}
