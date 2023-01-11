using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WPF_SHF_Element_lib
{
    /// <summary>
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : System.Windows.Window
    {   
        List<string> nameElements = new List<string>();
        public Window4()
        {
            
            InitializeComponent();
            pole();
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pole();
        }

        public void pole()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Data.fileName = "2pole.json";
                    break;
                case 1:
                    Data.fileName = "4pole.json";
                    break;
                case 2:
                    Data.fileName = "6pole.json";
                    break;
                case 3:
                    Data.fileName = "8pole.json";
                    break;
            }
            string filePath = AppDomain.CurrentDomain.BaseDirectory + Data.fileName;
            if (File.Exists(filePath))
            {
                var jsonString = File.ReadAllText(filePath);
                var elementsList = JsonSerializer.Deserialize<List<Element>>(jsonString);
                foreach (Element element in elementsList)
                {
                    nameElements.Add(element.name);

                }

                listView.ItemsSource = nameElements;
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                
            }
        }
    }

}

