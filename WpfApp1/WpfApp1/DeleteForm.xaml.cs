using System;
using System.Collections.Generic;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for DeleteForm.xaml
    /// </summary>
    public partial class DeleteForm : Window
    {
        public Window ReformtoAdmin { get; set; }
        public DeleteForm(Window window)
        {
            ReformtoAdmin = window;
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
            ReformtoAdmin.Show();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            string name = Bookname.Text.ToString();
            //Check database and delete
        }
    }
}
