using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_SHF_Element_lib
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
           
            grid.ItemsSource = elements;
        }

        List<string> elements = new List<string>();
        Data data = new Data();
        

        public void dataGrid1_Fill()
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            elements = data.elements;
            int i =0;
            foreach (string element in data.elements)
                MessageBox.Show(element);
            foreach (string element in elements)
            {
                if (grid.Items.Count - i == 0) grid.Items.Add(element);
            }
        }
    }


    public class Element
    {
        public Element(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public string LastName { get { return this.lastName; } set { this.lastName = value; } }
 
        private string firstName;
        private string lastName;
     
    }






}
