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
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Forms;
using TextBox = System.Windows.Controls.TextBox;

namespace WPF_SHF_Element_lib
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private Dictionary<string, string> operators = new Dictionary<string, string>()
        {
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
        string file;
        bool exit = false;
        public Window1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
            if (string.IsNullOrEmpty(Data.values_text) == false) values_textbox.Text = Data.values_text;
            if (Data.group != 0) comboBox1.SelectedIndex = Data.group - 1;
            if (string.IsNullOrEmpty(Data.name) == false) nameElement.Text = Data.name;
            if (string.IsNullOrEmpty(Data.parametersText) == false) params_textbox.Text = Data.parametersText;


        }

        public void copy_file()
        {
            File.Copy(file, AppDomain.CurrentDomain.BaseDirectory + @"\element_picture\" + Path.GetFileName(nameElement.Text) + ".png");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, string> groupFileName = new Dictionary<int, string>()
            {
                {1,"two_pole.json" },
                {2,"four_pole.json" },
                {3,"six_pole.json" },
                {4,"eight_pole.json" },
                {5,"ten_pole.json" },
                {6,"twelve_pole.json" },
                {7,"fourteen_pole.json" },
                {8,"sixteen_pole .json" },
                {9,"eighteen_pole.json" },
                {10,"twentee_pole.json" },
                {11,"twenteetwo_pole.json" },
                {12,"twentyfour_pole.json" },
            };

            Data.name = nameElement.Text;
            Data.group = comboBox1.SelectedIndex + 1;
            groupFileName.TryGetValue(Data.group, out string value);
            Data.fileName = value;

            if (string.IsNullOrEmpty(nameElement.Text) == true)
            {
                MessageBox.Show("Введите имя для элемента");
            }
            else if (Element.searchName() == true)
                MessageBox.Show("Элемент с таким именем уже существует");
            else
            {
                Data.IndexGroup = comboBox1.SelectedIndex + 1;
                if (values_textbox.Text != Data.values_text && !string.IsNullOrEmpty(values_textbox.Text))
                {
                    Data.values = (values_textbox.Text.Replace(" ", "")).Split(',').ToList();
                    Data.values_text = values_textbox.Text;
                    Data.ValuesChanged = true;
                }
                else
                {
                    Data.ValuesChanged = false;
                }
                if (params_textbox.Text != Data.parametersText && !string.IsNullOrEmpty(params_textbox.Text))
                {
                    Data.parameters = (params_textbox.Text.Replace(" ", "")).Split(',').ToList();
                    Data.parametersText = params_textbox.Text;
                    Data.ParamsChanged = true;
                }
                else
                {
                    Data.ParamsChanged = false;
                }
                exit = true;
                Window2 window2 = new Window2();

                window2.Owner = this.Owner;
                this.Close();
                window2.ShowDialog();

            }
            if (string.IsNullOrEmpty(Data.ImagePath)) { Data.ImagePath = null; }


        }

        private void WrapPanel_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!(params_textbox.IsFocused) && !(values_textbox.IsFocused)) return;
            Point pt1 = e.GetPosition(wrapSymb);
            HitTestResult result1 = VisualTreeHelper.HitTest(wrapSymb, pt1);
            System.Windows.Controls.Button button = Data.FindParent<System.Windows.Controls.Button>(result1.VisualHit);
            if (button != null)
            {
                TextBox textBox = null;
                if (params_textbox.IsFocused) textBox = params_textbox;
                else textBox = values_textbox;
                var temp = (params_textbox).SelectionStart;
                if (operators.TryGetValue(button.Content.ToString(), out string value))
                {
                    textBox.SelectedText = value;
                    textBox.SelectionStart = temp + 1;
                }
            }
        }






        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var choose_pic = new OpenFileDialog();
            choose_pic.Filter = "Images files |*.jpg;*.jpeg;*.png;*.svg;*.ico; | All files | *.*";

            if (choose_pic.ShowDialog() == true)
            {
                file = choose_pic.FileName;

                selectedImage.Source = new BitmapImage(new Uri(file));
                Data.ImagePath = file;
            }
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
