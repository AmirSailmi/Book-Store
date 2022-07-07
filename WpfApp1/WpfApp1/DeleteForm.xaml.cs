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
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WpfApp1
{
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
            bool ok;
            string bookname = Bookname.Text.ToString().Trim();
            SQLmethodes.DeleteBookFromBookTable(bookname, out ok);
            if (!ok) return;
            MessageBoxResult message = MessageBox.Show("Book Deleted Successfuly!");
        }
    }
}
