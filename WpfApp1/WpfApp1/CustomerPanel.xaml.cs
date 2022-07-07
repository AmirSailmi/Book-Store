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

    public partial class CustomerPanel : Window
    {
        public float cost { get; set; }
        public string EmailOfUser { get; set; }
        public string PassWordOfUser { get; set; }
        MainWindow main_window { get; set; }
        public CustomerPanel(MainWindow window, string email, string pass)
        {
            main_window = window;
            EmailOfUser = email;
            PassWordOfUser = pass;

            InitializeComponent();
        }

        private void saerchBookBtn_Click(object sender, RoutedEventArgs e)
        {
            searchGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Hidden;
        }

        private void BackToLoginPage(object sender, RoutedEventArgs e)
        {
            this.Close();
            main_window.Show();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
            main_window.Close();
        }

        private void searchBack_Click(object sender, RoutedEventArgs e)
        {
            searchGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void BookmarkBtn_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Hidden;
            string nameofbook = BookName.Text.ToString();
            nameofbook = nameofbook.Replace("Name =", "").Replace(",","").Trim();
            
            bool exist = false;
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Users.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from UserTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == EmailOfUser)
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no user with this email!");
                return;
            }

            connection.Close();

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;

            email = data.Rows[row][0].ToString();
            name = data.Rows[row][1].ToString();
            family = data.Rows[row][2].ToString();
            password = data.Rows[row][3].ToString();
            shoppinglist = data.Rows[row][4].ToString();
            buyedlist = data.Rows[row][5].ToString();
            bookmarked = data.Rows[row][6].ToString();
            wallet = float.Parse(data.Rows[row][7].ToString());

            if (bookmarked.Contains($"{nameofbook}"))
            {
                bookmarked = bookmarked.Replace($"{nameofbook}, ", "");
                MessageBoxResult message = MessageBox.Show("You unbookmaarked the book");
            }
            else bookmarked += $"{nameofbook}, ";

            try
            {
                connection.Open();

                string DeleteCommand;
                DeleteCommand = "delete from UserTable where Email = '" + EmailOfUser + "'";
                SqlCommand DeleteRow = new SqlCommand(DeleteCommand, connection);
                DeleteRow.ExecuteNonQuery();

                command = "insert into UserTable values" +
                        "('" + EmailOfUser.Trim() + "','" + name.Trim() + "' , '" + family.Trim() + "','" + password.Trim() + "','" + shoppinglist.Trim() + "','" + buyedlist + "','" + bookmarked + "','" + wallet + "')";

                SqlCommand Command = new SqlCommand(command, connection);
                Command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception Error)
            {
                connection.Close();
                MessageBoxResult message = MessageBox.Show($"Bookmarking Book was unsuccessful!/nError descriiption : \n{Error.Message}");return;
            }
        }

        private void CartBtn_Click(object sender, RoutedEventArgs e)
        {
            cartGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Hidden;

            bool exist = false;
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Users.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from UserTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == EmailOfUser)
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no user with this email!");
                return;
            }

            connection.Close();
            string ShoppingList;
            ShoppingList = data.Rows[row][4].ToString();

            List<string> books = new List<string>();
            string[] arr = ShoppingList.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].Replace(" ", ",Price = ");
                cost += int.Parse(arr[i].Substring(arr[i].IndexOf("Price =") + 8));
            }

            foreach (string book in arr) { books.Add(book.Trim()); }

            ShoppingListview.ItemsSource = books;
        }

        private void WalletBtn_Click(object sender, RoutedEventArgs e)
        {
            walletGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Hidden;
        }

        private void EditProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            editPanelGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Hidden;
        }

        private void VIPBtn_Click(object sender, RoutedEventArgs e)
        {
            VIPGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Hidden;
        }

        private void SearchSearchBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bookmarksBack_Click(object sender, RoutedEventArgs e)
        {
            bookmarksGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void cartBuyBtn_Click(object sender, RoutedEventArgs e)
        {
            buyingPage.Visibility = Visibility.Visible;
            cartGrid.Visibility = Visibility.Hidden;
        }

        private void BuyFromWallet(object sender, RoutedEventArgs e)
        {
            float price = cost;

            bool exist = false;
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Users.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from UserTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == EmailOfUser)
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no user with this email!");
                return;
            }

            connection.Close();

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;

            email = data.Rows[row][0].ToString();
            name = data.Rows[row][1].ToString();
            family = data.Rows[row][2].ToString();
            password = data.Rows[row][3].ToString();
            shoppinglist = data.Rows[row][4].ToString();
            buyedlist = data.Rows[row][5].ToString();
            bookmarked = data.Rows[row][6].ToString();
            wallet = float.Parse(data.Rows[row][7].ToString());

            if (price > wallet) { MessageBoxResult message = MessageBox.Show($"money in wallet is not enough \nWallet : {wallet}\nCost : {price}"); return; }
            else 
            {
                wallet -= price;
                shoppinglist = ""; 

                connection.Open();

                string DeleteCommand;
                DeleteCommand = "delete from UserTable where Email = '" + email + "'";
                SqlCommand DeleteRow = new SqlCommand(DeleteCommand, connection);
                DeleteRow.ExecuteNonQuery();

                command = "insert into UserTable values" +
                        "('" + email.Trim() + "','" + name.Trim() + "' , '" + family.Trim() + "','" + password.Trim() + "','" + shoppinglist.Trim() + "','" + buyedlist + "','" + bookmarked + "','" + wallet + "')";

                SqlCommand Command = new SqlCommand(command, connection);
                Command.ExecuteNonQuery();
                connection.Close();

                cost = 0;
                MessageBoxResult message = MessageBox.Show($"Your purchase was successful");

            }

        }
        private void ByfromCard(object sender, RoutedEventArgs e)
        {
            if (!Check.PassesLuhnCheck(numberofcard.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Card number is false");return;
            }
            if (!Check.PasswordCheck(passwordofcard.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("pattern of Password is not true!"); return;
            }
            if (!Check.CVVCheck(cvvofcard.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("pattern of CVV is not true!"); return;
            }

            //Check card in sql table

            cost = 0;

            bool exist = false;
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Users.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from UserTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == EmailOfUser)
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no user with this email!");
                return;
            }

            connection.Close();

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;

            email = data.Rows[row][0].ToString();
            name = data.Rows[row][1].ToString();
            family = data.Rows[row][2].ToString();
            password = data.Rows[row][3].ToString();
            shoppinglist = data.Rows[row][4].ToString();
            buyedlist = data.Rows[row][5].ToString();
            bookmarked = data.Rows[row][6].ToString();
            wallet = float.Parse(data.Rows[row][7].ToString());

            shoppinglist = "";

            connection.Open();

            string DeleteCommand;
            DeleteCommand = "delete from UserTable where Email = '" + email + "'";
            SqlCommand DeleteRow = new SqlCommand(DeleteCommand, connection);
            DeleteRow.ExecuteNonQuery();

            command = "insert into UserTable values" +
                    "('" + email.Trim() + "','" + name.Trim() + "' , '" + family.Trim() + "','" + password.Trim() + "','" + shoppinglist.Trim() + "','" + buyedlist + "','" + bookmarked + "','" + wallet + "')";

            SqlCommand Command = new SqlCommand(command, connection);
            Command.ExecuteNonQuery();
            connection.Close();

            cost = 0;
            MessageBoxResult message1 = MessageBox.Show($"Your purchase was successful");

        }

        private void removeBookIDBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cartBack_Click(object sender, RoutedEventArgs e)
        {
            cartGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void AddBallanceBtn_Click(object sender, RoutedEventArgs e)
        {
            float AddBalanceValue = 0;
            if (AddBallanceAmount==null || !float.TryParse(AddBallanceAmount.Text.ToString(),out AddBalanceValue))
            {
                MessageBoxResult message = MessageBox.Show("Enter correct value");return;
            }

            bool exist = false;
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Users.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from UserTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == EmailOfUser)
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no user with this email!");
                return;
            }

            connection.Close();

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;

            email = data.Rows[row][0].ToString();
            name = data.Rows[row][1].ToString();
            family = data.Rows[row][2].ToString();
            password = data.Rows[row][3].ToString();
            shoppinglist = data.Rows[row][4].ToString();
            buyedlist = data.Rows[row][5].ToString();
            bookmarked = data.Rows[row][6].ToString();
            wallet = float.Parse(data.Rows[row][7].ToString());

            wallet += AddBalanceValue;

            connection.Open();

            string DeleteCommand;
            DeleteCommand = "delete from UserTable where Email = '" + email + "'";
            SqlCommand DeleteRow = new SqlCommand(DeleteCommand, connection);
            DeleteRow.ExecuteNonQuery();

            command = "insert into UserTable values" +
                    "('" + email.Trim() + "','" + name.Trim() + "' , '" + family.Trim() + "','" + password.Trim() + "','" + shoppinglist.Trim() + "','" + buyedlist + "','" + bookmarked + "','" + wallet + "')";

            SqlCommand Command = new SqlCommand(command, connection);
            Command.ExecuteNonQuery();
            connection.Close();

            balanceValue.Text = wallet.ToString();
            MessageBoxResult message2 = MessageBox.Show("Added successfuly");
        }

        private void walletBack_Click(object sender, RoutedEventArgs e)
        {
            walletGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void editNameBtn_Click(object sender, RoutedEventArgs e)
        {
            editPanelGrid.Visibility = Visibility.Hidden;
            editNameGrid.Visibility = Visibility.Visible;
        }

        private void editNameSubmitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editNameBack_Click(object sender, RoutedEventArgs e)
        {
            editNameGrid.Visibility = Visibility.Hidden;
            editPanelGrid.Visibility = Visibility.Visible;
        }
        private void editPassBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editPassSubmitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editPassBack_Click(object sender, RoutedEventArgs e)
        {
            editPassGrid.Visibility = Visibility.Hidden;
            editPanelGrid.Visibility = Visibility.Visible;
        }

        private void editProfileBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buyVIPBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VIPBack_Click(object sender, RoutedEventArgs e)
        {
            VIPGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void SearchDependingBook(object sender, RoutedEventArgs e)
        {
            if (!Check.NameCheck(searchBook.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Enter name"); return;
            }

            string Name = searchBook.Text.ToString();
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
                    break;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no book with this name!");
                return;
            }

            connection.Close();

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

            imageofbook.Source = new BitmapImage(new Uri(bookimagepath));

            YEAR.Text = year;
            authorName.Text = authorname;
            pointe.Text = "point = " + point.ToString();
            BookName.Text = "Name = " + name;
            description.Text = "Description = " + bookdescription;
            Pricee.Text = "Price = " + price;

            if (isvip == true)
            {
                VIPe.Text = "VIP";
                VIPFEE.Text = "fee = " + vipfee.ToString();
            }
            if (discount != 0)
            {
                discounte.Text = $"{discount} percebtage off untill {timefordiscount}";
            }
            else
            {
                discounte.Text = "No Discount";
            }
            searchGrid.Visibility = Visibility.Hidden;
            ShowBook.Visibility = Visibility.Visible;
        }

        private void BackToSearchBookPaage(object sender, RoutedEventArgs e)
        {
            ShowBook.Visibility = Visibility.Hidden;
            searchGrid.Visibility = Visibility.Visible;
        }

        private void AddToShoppingList(object sender, RoutedEventArgs e)
        {
            string nameofbook = searchBook.Text.ToString();
            string nameofauthor = searchAuthor.Text.ToString();

            if (nameofbook.Trim() != "" && nameofauthor.Trim() != "")
            {
                MessageBoxResult message = MessageBox.Show("Fill just one of the fields"); return;
            }
            bool exist = false;
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Users.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from UserTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString().ToLower() == EmailOfUser)
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no user with this email!");
                return;
            }

            connection.Close();

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;

            email = data.Rows[row][0].ToString();
            name = data.Rows[row][1].ToString();
            family = data.Rows[row][2].ToString();
            password = data.Rows[row][3].ToString();
            shoppinglist = data.Rows[row][4].ToString();
            buyedlist = data.Rows[row][5].ToString();
            bookmarked = data.Rows[row][6].ToString();
            wallet = float.Parse(data.Rows[row][7].ToString());

            SqlConnection connectiontobooktable = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Books.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from BookTable";
            SqlDataAdapter adapter2 = new SqlDataAdapter(command, connectiontobooktable);
            DataTable databook = new DataTable();
            adapter2.Fill(databook);

            string price = "";

            for (int i = 0; i < databook.Rows.Count; i++)
            {
                if (nameofbook != null && databook.Rows[i][0].ToString().ToLower() == nameofbook.ToLower())
                {
                    price = databook.Rows[i][3].ToString();
                    break;
                }
                if (nameofauthor != null && databook.Rows[i][1].ToString().ToLower() == searchAuthor.Text.ToString())
                {
                    price = databook.Rows[i][3].ToString();
                    break;
                }
            }

            connection.Close();



            shoppinglist += $"{nameofbook} {price},";

            try
            {
                connection.Open();

                string DeleteCommand;
                DeleteCommand = "delete from UserTable where Email = '" + email + "'";
                SqlCommand DeleteRow = new SqlCommand(DeleteCommand, connection);
                DeleteRow.ExecuteNonQuery();

                command = "insert into UserTable values" +
                        "('" + email.Trim() + "','" + name.Trim() + "' , '" + family.Trim() + "','" + password.Trim() + "','" + shoppinglist.Trim() + "','" + buyedlist + "','" + bookmarked + "','" + wallet + "')";

                SqlCommand Command = new SqlCommand(command, connection);
                Command.ExecuteNonQuery();
                connection.Close();
                MessageBoxResult message = MessageBox.Show("Book Added successfuly!");
            }
            catch (Exception Error)
            {
                connection.Close();
                MessageBoxResult message = MessageBox.Show($"Adding Book was unsuccessful!/nError descriiption : \n{Error.Message}");
            }
            searchGrid.Visibility = Visibility.Hidden;
            ShowBook.Visibility = Visibility.Visible;
        }

        private void SearchDependingAuthor(object sender, RoutedEventArgs e)
        {
            if (!Check.NameCheck(searchAuthor.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Enter name"); return;
            }

            string Name = searchAuthor.Text.ToString();
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
                if (data.Rows[i][1].ToString().ToLower() == Name.ToLower())
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no book with this author!");
                return;
            }

            connection.Close();

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

            imageofbook.Source = new BitmapImage(new Uri(bookimagepath));

            YEAR.Text = year;
            authorName.Text = authorname;
            pointe.Text = "point = " + point.ToString();
            BookName.Text = "Name = " + name;
            description.Text = "Description = " + bookdescription;
            Pricee.Text = "Price = " + price;
            if (isvip == true)
            {
                VIPe.Text = "VIP";
                VIPFEE.Text = "fee = " + vipfee.ToString();
            }
            if (discount != 0)
            {
                discounte.Text = $"{discount} percebtage off untill {timefordiscount}";
            }
            else
            {
                discounte.Text = "No Discount";
            }
            searchGrid.Visibility = Visibility.Hidden;
            ShowBook.Visibility = Visibility.Visible;
        }

        private void BookIist(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Hidden;
            bool exist = false;
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Users.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from UserTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString().ToLower() == EmailOfUser)
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no user with this email!");
                return;
            }

            connection.Close();
            string bookmarked;
            bookmarked = data.Rows[row][6].ToString();

            //list
            List<string> books = new List<string>();
            string []arr = bookmarked.Split(',');

            foreach(string nameofbookmarked in arr) { books.Add(nameofbookmarked.Trim()); }

            YourListBox.ItemsSource = books;
            bookmarksGrid.Visibility = Visibility.Visible;
        }

        private void WalletGrid_Loaded(object sender, RoutedEventArgs e)
        {
            bool exist = false;
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Users.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from UserTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == EmailOfUser)
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no user with this email!");
                return;
            }

            connection.Close();

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;

            email = data.Rows[row][0].ToString();
            name = data.Rows[row][1].ToString();
            family = data.Rows[row][2].ToString();
            password = data.Rows[row][3].ToString();
            shoppinglist = data.Rows[row][4].ToString();
            buyedlist = data.Rows[row][5].ToString();
            bookmarked = data.Rows[row][6].ToString();
            wallet = float.Parse(data.Rows[row][7].ToString());

            balanceValue.Text = wallet.ToString();
        }
    }
}
