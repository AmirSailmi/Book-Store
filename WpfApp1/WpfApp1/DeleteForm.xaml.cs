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
            string bookname = Bookname.Text.ToString().Trim();
            //Check database and delete
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Books.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();

                string command = "DELETE FROM BookTable WHERE BookName = '"+bookname+"' ";//????????
                SqlCommand com = new SqlCommand(command, con);
                com.BeginExecuteNonQuery();

                con.Close();

                MessageBoxResult message = MessageBox.Show($"Book : {bookname} Deleted successfuly! ");

            }
            catch (Exception E)
            {
                MessageBoxResult message = MessageBox.Show($"Book : {bookname} isn\'t Deleted ! \nError description : \n{E.Message}");
            }

            //Delete does not work
        }
    }
}
