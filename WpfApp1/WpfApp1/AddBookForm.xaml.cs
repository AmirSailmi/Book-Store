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
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WpfApp1
{

    public partial class AddBookForm : Window
    {
        public string filepath;
        public Window ReformtoAdmin { get; set; }
        public AddBookForm(Window window)
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
            string bookname, authorName, year, price, BookDescription, authorProfile, TimeForDiscount;
            bool isVIP;
            float VIPSubscriptionMonthlyFee , Discount ;
            int point, saleNumber;

            if (filepath == null)
            {
                MessageBoxResult message = MessageBox.Show("Upload image of book");
                return;
            }

            if (!Regex.IsMatch(BookName.Text.ToString(), @"^[A-Za-z0-9\s]+$") || BookName.Text.ToString().Trim()=="")
            {
                MessageBoxResult message = MessageBox.Show("Enter a name for book");
                return;
            }

            if (!Regex.IsMatch(Price.Text.ToString(), "^[0-9]+$"))
            {
                MessageBoxResult message = MessageBox.Show("Enter a Price");
                return;
            }

            if (!Regex.IsMatch(Year.Text.ToString(), "^[0-9]+$"))
            {
                MessageBoxResult message = MessageBox.Show("Enter a Year");
                return;
            }

            if (!Check.NameCheck(AuthorName.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Enter a name for author");
                return;
            }

            if (!Regex.IsMatch(AuthorProfile.Text.ToString(), @"^[A-Za-z0-9()_.-?!\s]+$"))
            {
                MessageBoxResult message = MessageBox.Show("Enter a description for author");
                return;
            }

            if (!Regex.IsMatch(AboutBook.Text.ToString(), @"^[A-Za-z0-9()_.-?!\s]+$"))
            {
                MessageBoxResult message = MessageBox.Show("Enter a description for book");
                return;
            }

            price = (Price.Text.ToString());
            year = (Year.Text.ToString());
            authorName = (AuthorName.Text.ToString());
            authorProfile = AuthorProfile.Text.ToString();
            BookDescription = AboutBook.Text.ToString();
            bookname = BookName.Text.ToString();
            isVIP = false;
            point = 0;
            saleNumber = 0;
            TimeForDiscount = "";
            VIPSubscriptionMonthlyFee = 0;
            Discount = 0;
            //Save to database
            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Books.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string command;
                command = "insert into BookTable values" +
                    "('" + bookname.Trim() + "','" + authorName.Trim() + "' , '" + year.Trim() + "','" + price.Trim() + "','" + BookDescription.Trim() + "','" + authorProfile.Trim() + "','" + isVIP + "','" + saleNumber + "','" + point + "' , '"+filepath+"','"+VIPSubscriptionMonthlyFee+"','"+TimeForDiscount+"','"+Discount+"')";
                SqlCommand Command = new SqlCommand(command, connection);
                Command.ExecuteNonQuery();
                connection.Close();
                MessageBoxResult message = MessageBox.Show("Book Added successfuly!");
            }
            catch(Exception Error)
            {
                MessageBoxResult message = MessageBox.Show($"Adding Book was unsuccessful!/nError descriiption : \n{Error.Message}");
            }
        }
    }
}
