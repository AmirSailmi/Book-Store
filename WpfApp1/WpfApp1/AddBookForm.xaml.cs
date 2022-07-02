using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddBookForm.xaml
    /// </summary>
    public partial class AddBookForm : Window
    {
        public string filepath;
        public Window ReformtoAdmin { get; set; }
        public AddBookForm( Window window)
        {
            ReformtoAdmin = window;
            InitializeComponent();
        }

        public void UploadPhoto(object sender, RoutedEventArgs e)
        {
            filepath = null;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                BookImage.Source = new BitmapImage(new Uri(op.FileName));
                filepath = op.FileName;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
            ReformtoAdmin.Show();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (filepath == null)
            {
                MessageBoxResult message = MessageBox.Show("Upload image of book");
                return;
            }

            if(Regex.IsMatch( Price.Text.ToString() , "[^0-9]+"))
            {
                MessageBoxResult message = MessageBox.Show("Enter a Price");
                return;
            }
        }
    }
}
