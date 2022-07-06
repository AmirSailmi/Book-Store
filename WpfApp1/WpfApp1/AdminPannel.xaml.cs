using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string name { get; set; }
        string pass { get; set; }
        MainWindow Mainwindow { get; set; }

        public Window1(string name , string pass , MainWindow window)
        {
            this.name = name;
            this.pass = pass;
            Mainwindow = window;
            InitializeComponent();
        }

        private void AddBook(object sender, RoutedEventArgs e)
        {
            AddBookForm form = new AddBookForm( this);
            this.Visibility = Visibility.Hidden;
            form.Show();
        }

        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            DeleteForm form = new DeleteForm(this);
            this.Visibility = Visibility.Hidden;
            form.Show();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            EditForm editForm = new EditForm(this);
            this.Visibility = Visibility.Hidden;
            editForm.Show();
        }

        private void Status(object sender, RoutedEventArgs e)
        {
            StatusForm statusForm = new StatusForm(this);
            statusForm.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
            Mainwindow.Close();
        }

        private void ShowListOfVIPmembers(object sender, RoutedEventArgs e)
        {

        }

        private void ShowListOfNormalMembers(object sender, RoutedEventArgs e)
        {

        }

        private void EmailSearchedText(object sender, TextChangedEventArgs e)
        {

        }

        private void BackToLoginPage(object sender, RoutedEventArgs e)
        {
           

            this.Close();

            Mainwindow.Show();
        }

        private void ShowBooksList(object sender, RoutedEventArgs e) //show name - author name - point - sale stats- cost - normal or VIP - Time Discount - year - breif description
        {
            BookList bookList = new BookList(this);
            bookList.Show();
            this.Visibility = Visibility.Hidden;
        }


        private void SearchBookByBookName(object sender, RoutedEventArgs e)
        {
            if (!Check.NameCheck(nameofbook.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Enter name");
            }

            string Name = nameofbook.Text.ToString();
            bool exist = false;
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Books.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from BookTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString().ToLower() == Name.ToLower())
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no book with this name!");
                return;
            }

            string name;
            string price;
            string year;
            string authorname;
            string authorprofile;
            string bookdescription;
            bool isvip;
            int salenumber;
            int point;
            string bookimagepath;
            float vipfee;
            string timefordiscount;
            float discount;

            name = data.Rows[row][0].ToString();
            authorname = data.Rows[row][1].ToString();
            year = data.Rows[row][2].ToString();
            price = data.Rows[row][3].ToString();
            bookdescription = data.Rows[row][4].ToString();
            authorprofile = data.Rows[row][5].ToString();
            isvip = Convert.ToBoolean(data.Rows[row][6]);
            salenumber = int.Parse(data.Rows[row][7].ToString());
            point = int.Parse(data.Rows[row][8].ToString());
            bookimagepath = data.Rows[row][9].ToString();
            vipfee = float.Parse(data.Rows[row][10].ToString());
            timefordiscount = data.Rows[row][11].ToString().ToString();
            discount = float.Parse(data.Rows[row][12].ToString());

            SearchBook searchBook = new SearchBook(name, price, year, authorname, authorprofile, bookdescription, isvip, salenumber, point, bookimagepath, vipfee, timefordiscount, discount, this);
            this.Visibility = Visibility.Hidden;
            searchBook.Show();
        }

        private void SearchBookByAuthorName(object sender, RoutedEventArgs e)
        {
            
            if (!Check.NameCheck(authornamee.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Enter name");
            }
            string Authorname = authornamee.Text.ToString();
            bool exist = false;
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Books.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from BookTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][1].ToString().ToLower() == Authorname.ToLower())
                {
                    exist = true;
                    row = i;
                    break;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no book with this author!");
                return;
            }

            string name;
            string price;
            string year;
            string authorname;
            string authorprofile;
            string bookdescription;
            bool isvip;
            int salenumber;
            int point;
            string bookimagepath;
            float vipfee;
            string timefordiscount;
            float discount;

            name = data.Rows[row][0].ToString();
            authorname = data.Rows[row][1].ToString();
            year = data.Rows[row][2].ToString();
            price = data.Rows[row][3].ToString();
            bookdescription = data.Rows[row][4].ToString();
            authorprofile = data.Rows[row][5].ToString();
            isvip = Convert.ToBoolean(data.Rows[row][6]);
            salenumber = int.Parse(data.Rows[row][7].ToString());
            point = int.Parse(data.Rows[row][8].ToString());
            bookimagepath = data.Rows[row][9].ToString();
            vipfee = float.Parse(data.Rows[row][10].ToString());
            timefordiscount = data.Rows[row][11].ToString().ToString();
            discount = float.Parse(data.Rows[row][12].ToString());

            SearchBook searchBook = new SearchBook(name, price, year, authorname, authorprofile, bookdescription, isvip, salenumber, point, bookimagepath, vipfee, timefordiscount, discount, this);
            this.Visibility = Visibility.Hidden;
            searchBook.Show();
        }

        private void EmailSearchedButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
