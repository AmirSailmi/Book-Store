using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
        string PDFpath { get; set; }
        public float cost { get; set; }
        public bool isBuyingVIP { get; set; }
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
            nameofbook = nameofbook.Replace("Name =", "").Replace(",", "").Trim();
            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;


            if (bookmarked.Contains($"{nameofbook}"))
            {
                bookmarked = bookmarked.Replace($"{nameofbook}, ", "");
                MessageBoxResult message = MessageBox.Show("You unbookmaarked the book");
            }
            else bookmarked += $"{nameofbook}, ";

            SQLmethodes.UpdateUserTable(EmailOfUser, email, name, family, password, shoppinglist, buyedlist, bookmarked, wallet, VIPTime, out exist);
            if (!exist)
            {
                MessageBoxResult message = MessageBox.Show($"Bookmarking Book was unsuccessful!");
                return;
            }
        }

        private void CartBtn_Click(object sender, RoutedEventArgs e)
        {
            cartGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Hidden;

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            List<string> books = new List<string>();
            string[] arr = shoppinglist.Split(',');



            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].Replace(" ", ",Price = ");
                //cost += int.Parse(arr[i].Substring(arr[i].IndexOf("Price =") + 8));
            }

            foreach (string book in arr) { books.Add(book.Trim()); }

            ShoppingListview.ItemsSource = books;
        }

        private void WalletBtn_Click(object sender, RoutedEventArgs e)
        {
            walletGrid.Visibility = Visibility.Visible;
            //balanceValue.IsReadOnly = true;
            MainGrid.Visibility = Visibility.Hidden;
        }

        private void EditProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            editProfileGrid.Visibility = Visibility.Visible;
            editPanelGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Hidden;
        }

        private void VIPBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime time = DateTime.Now;
            string nowtime = time.ToString("MM/dd/yyyy");
            float fee;
            bool exist;

            SQLmethodes.ReturnVIPfee(out exist, out fee);

            VIPprice.Text = fee.ToString();

            VIPGrid.Visibility = Visibility.Visible;
            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            
            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            if (VIPTime == "")
            {
                nonVIPGrid.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                string endTime = VIPTime;
                string now = nowtime;

                if (!CheckTime(now, endTime))
                {
                    MessageBoxResult message = MessageBox.Show("Your monthly subscription has expired");

                    haveVIPGrid.Visibility = Visibility.Hidden;
                    nonVIPGrid.Visibility = Visibility.Visible;
                    return;
                }

                VIPtimeLeft.Text = VIPTime;

                haveVIPGrid.Visibility = Visibility.Visible;
            }
        }

        public bool CheckTime(string now , string endtime)//if now < endTime returns true
        {
            string[] month_day_year_NOW = now.Split('/');

            int dayNow = int.Parse(month_day_year_NOW[1]);
            int monthNow = int.Parse(month_day_year_NOW[0]);
            int yearNow = int.Parse(month_day_year_NOW[2]);

            string[] month_day_year_End = endtime.Split('/');

            int dayEnd = int.Parse(month_day_year_End[1]);
            int monthEnd = int.Parse(month_day_year_End[0]);
            int yearEnd = int.Parse(month_day_year_End[2]);

            if (yearNow > yearEnd) return false;
            else if (yearNow < yearEnd) return true;
            else
            {
                if (monthNow > monthEnd) return false;
                else if (monthNow < monthEnd) return true;
                else
                {
                    if (dayNow > dayEnd) return false;
                    else if (dayNow < dayEnd) return true;
                    else return false;
                }
            }
        }

        private void bookmarksBack_Click(object sender, RoutedEventArgs e)
        {
            bookmarksGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void cartBuyBtn_Click(object sender, RoutedEventArgs e)
        {
            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            if (shoppinglist.Trim() == "") { MessageBoxResult message6 = MessageBox.Show("Cart is empty!"); return; }

            string[] BooksNamesAndPrice = shoppinglist.Split(',');
            string[] BooksNames = new string[BooksNamesAndPrice.Length];

            for (int i = 0; i < BooksNamesAndPrice.Length; i++)
            {
                string nameandprice;
                nameandprice = BooksNamesAndPrice[i];
                BooksNames[i] = nameandprice.Split(' ')[0].Trim();
            }

            cost = DiscountCalculator(BooksNames);
            
            MessageBoxResult meessage = MessageBox.Show($"Total Cost : {cost}");

            buyingPage.Visibility = Visibility.Visible;
            cartGrid.Visibility = Visibility.Hidden;
        }
        public void DateSetting(string datenow , ref string VIPTime)
        {
            string[] month_day_year = datenow.Split('/');

            int day = int.Parse(month_day_year[1]);
            int month = int.Parse(month_day_year[0]);
            int year = int.Parse(month_day_year[2]);

            day += 30;
            int newday = day % 30;
            month++;

            if (month == 13)
            {
                month = month % 12;
                year++;
            }
            VIPTime = $"{month}/{newday}/{year}";
        }

        private void BuyFromWallet(object sender, RoutedEventArgs e)
        {
            float price = cost;

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            if (price > wallet) { MessageBoxResult message = MessageBox.Show($"money in wallet is not enough \nWallet : {wallet}\nCost : {price}"); return; }
            else
            {
                wallet -= price;

                SqlConnection con = SQLmethodes.SQLconnectionToBooksTable();
                con.Open();
                string command = "select * from TotalMoney";
                SqlDataAdapter adapter = new SqlDataAdapter(command, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                float t = float.Parse(data.Rows[0][0].ToString());
                t += price;

                con = SQLmethodes.SQLconnectionToBooksTable();
                con.Open();
                command = "delete from TotalMoney";
                SqlCommand com = new SqlCommand(command, con);
                com.ExecuteNonQuery();
                con.Close();

                con = SQLmethodes.SQLconnectionToBooksTable();
                con.Open();
                command = "insert into TotalMoney values('" + t + "')";
                com = new SqlCommand(command, con);
                com.ExecuteNonQuery();
                con.Close();


                con.Close();

                if (isBuyingVIP)
                {
                    var date = DateTime.Now;

                    string datenow = date.ToString("MM/dd/yyyy");

                    DateSetting(datenow, ref VIPTime);
                    VIPtimeLeft.Text = VIPTime;

                    haveVIPGrid.Visibility = Visibility.Visible;
                    nonVIPGrid.Visibility = Visibility.Hidden;
                    VIPGrid.Visibility = Visibility.Visible;
                    buyingPage.Visibility = Visibility.Hidden;
                    isBuyingVIP = false;
                }
                else
                {
                    string[] BooksNamesAndPrice = shoppinglist.Split(',');
                    string[] BooksNames = new string[BooksNamesAndPrice.Length];

                    for (int i = 0; i < BooksNamesAndPrice.Length; i++)
                    {
                        BooksNames[i] = BooksNamesAndPrice[i].Split(' ')[0];
                        
                        string bookname;
                        string bookprice;
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
                        int numberofpoints;
                        string pdfpath;

                        SQLmethodes.ReturnBookStats(0, BooksNames[i], out bookname, out authorname, out year, out bookprice, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount, out numberofpoints, out pdfpath, out exist);

                        salenumber++;

                        if (bookname.Trim() == "") continue;

                        bool ok;
                        SQLmethodes.DeleteBookFromBookTable(bookname, out ok);
                        if (!ok) return;

                        bool isok;
                        SQLmethodes.AddBookToBookTable(bookname, authorname, year, bookprice, bookdescription, authorprofile, isvip, salenumber, point, bookimagepath, vipfee, timefordiscount, discount, numberofpoints, pdfpath, out isok);
                        if (!isok) return;
                    }

                    buyedlist = shoppinglist;
                    shoppinglist = "";
                }

                SQLmethodes.UpdateUserTable(EmailOfUser, email, name, family, password, shoppinglist, buyedlist, bookmarked, wallet, VIPTime, out exist);
                if (!exist)
                {
                    MessageBoxResult message1 = MessageBox.Show($"Your purchase was unsuccessful");
                    return;
                }

                cost = 0;

                MessageBoxResult message = MessageBox.Show($"Your purchase was successful");
            }

        }
        private void ByfromCard(object sender, RoutedEventArgs e)
        {
            if (!Check.checkid(numberofcard.Text.ToString().Trim()))
            {
                MessageBoxResult message = MessageBox.Show("Card number is false"); return;
            }
            if (!Check.PasswordCheck(passwordofcard.Text.ToString().Trim()))
            {
                MessageBoxResult message = MessageBox.Show("pattern of Password is not true!"); return;
            }
            if (!Check.CVVCheck(cvvofcard.Text.ToString().Trim()))
            {
                MessageBoxResult message = MessageBox.Show("pattern of CVV is not true!"); return;
            }

            //Check card in sql table

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;
            if (isBuyingVIP)
            {

                var date = DateTime.Now;

                string datenow = date.ToString("MM/dd/yyyy");

                DateSetting(datenow, ref VIPTime);
                VIPtimeLeft.Text = VIPTime;

                haveVIPGrid.Visibility = Visibility.Visible;
                nonVIPGrid.Visibility = Visibility.Hidden;
                VIPGrid.Visibility = Visibility.Visible;
                buyingPage.Visibility = Visibility.Hidden;
                isBuyingVIP = false;
            }
            else
            {
                float price = 0;
                string[] BooksNamesAndPrice = shoppinglist.Split(',');
                string[] BooksNames = new string[BooksNamesAndPrice.Length];
                string[] BooksPrices = new string[BooksNamesAndPrice.Length];

                for (int i = 0; i < BooksNamesAndPrice.Length; i++)
                {
                    BooksNames[i] = BooksNamesAndPrice[i].Split(' ')[0];

                    string bookname;
                    string bookprice;
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
                    int numberofpoints;
                    string pdfpath;

                    SQLmethodes.ReturnBookStats(0, BooksNames[i], out bookname, out authorname, out year, out bookprice, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount, out numberofpoints, out pdfpath, out exist);
                    if (!exist) return;
                    price += (float.Parse(bookprice)*((100- discount)/100));
                    salenumber++;

                    bool ok;
                    SQLmethodes.DeleteBookFromBookTable(bookname, out ok);
                    if (!ok) return;

                    bool isok;
                    SQLmethodes.AddBookToBookTable(bookname, authorname, year, bookprice, bookdescription, authorprofile, isvip, salenumber, point, bookimagepath, vipfee, timefordiscount, discount, numberofpoints, pdfpath, out isok);
                    if (!isok) return;
                }

                SqlConnection con = SQLmethodes.SQLconnectionToBooksTable();
                con.Open();
                string command = "select * from TotalMoney";
                SqlDataAdapter adapter = new SqlDataAdapter(command, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                float t = float.Parse(data.Rows[0][0].ToString());
                t += price;

                con = SQLmethodes.SQLconnectionToBooksTable();
                con.Open();
                command = "delete from TotalMoney";
                SqlCommand com = new SqlCommand(command, con);
                com.ExecuteNonQuery();
                con.Close();

                con = SQLmethodes.SQLconnectionToBooksTable();
                con.Open();
                command = "insert into TotalMoney values('" + t + "')";
                com = new SqlCommand(command, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Close();

                buyedlist = shoppinglist;
                shoppinglist = "";
            }

            SQLmethodes.UpdateUserTable(EmailOfUser, email, name, family, password, shoppinglist, buyedlist, bookmarked, wallet, VIPTime, out exist);
            if (!exist)
            {
                MessageBoxResult message2 = MessageBox.Show($"Your purchase was unsuccessful");
                return;
            }

            cost = 0;
            MessageBoxResult message1 = MessageBox.Show($"Your purchase was successful");
        }

        public float DiscountCalculator(string[] BooksNames)
        {
            float price = 0;
            float total_price = 0;
            for (int i = 0; i < BooksNames.Length; i++)
            {
                string bookname;
                string bookprice;
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
                int numberofpoints;
                string pdfpath;
                bool exist;
                try
                {
                    SQLmethodes.ReturnBookStats(0, BooksNames[i].Trim(), out bookname, out authorname, out year, out bookprice, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount, out numberofpoints, out pdfpath, out exist);
                    if (bookprice.Trim() == "") return total_price;
                    price = float.Parse(bookprice) - ((float.Parse(bookprice)) * (discount / 100));
                    total_price += price;
                }catch(Exception error)
                {
                    MessageBoxResult boxResult = MessageBox.Show(error.Message);
                    return 0;
                }
            }
            return total_price;
        }

        private void removeBookIDBtn_Click(object sender, RoutedEventArgs e)
        {
            if (removeBookName.Text.ToString() == null) { MessageBoxResult message1 = MessageBox.Show("Enter a name"); return; }
            string nameofbook = removeBookName.Text.ToString();

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            string[] arr = shoppinglist.Split(',');
            List<string> books = new List<string>();

            if (shoppinglist.Contains(nameofbook))
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i != arr.Length - 1)
                    {
                        if (arr[i].Contains(nameofbook)) continue;
                        else books.Add(arr[i] + ",");
                    }
                    else
                    {
                        if (arr[i].Contains(nameofbook)) continue;
                        else books.Add(arr[i]);
                    }
                }

            }
            string NewShoppingList = "";
            foreach (string i in books) { NewShoppingList += i; }

            shoppinglist = NewShoppingList;

            SQLmethodes.UpdateUserTable(EmailOfUser, email, name, family, password, shoppinglist, buyedlist, bookmarked, wallet, VIPTime, out exist);
            if (!exist)
            {
                MessageBoxResult message2 = MessageBox.Show($"book isn\'t deleted");
                return;
            }

            MessageBoxResult message = MessageBox.Show("Deleted successfuly");
        }

        private void cartBack_Click(object sender, RoutedEventArgs e)
        {
            cartGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void AddBallanceBtn_Click(object sender, RoutedEventArgs e)
        {
            float AddBalanceValue = 0;
            if (AddBallanceAmount == null || !float.TryParse(AddBallanceAmount.Text.ToString(), out AddBalanceValue))
            {
                MessageBoxResult message = MessageBox.Show("Enter correct value"); return;
            }

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            wallet += AddBalanceValue;

            SQLmethodes.UpdateUserTable(EmailOfUser, email, name, family, password, shoppinglist, buyedlist, bookmarked, wallet, VIPTime, out exist);

            if (!exist)
            {
                MessageBoxResult message3 = MessageBox.Show($"balance isn\'t added");
                return;
            }

            walletBall.Text = wallet.ToString();
            //balanceValue.IsReadOnly = true;

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
            string Newname, Newfamily;

            if (!Check.NameCheck(newFirstName.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Enter a correct name"); return;
            }
            if (!Check.NameCheck(newFamillyName.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Enter a correct lastname"); return;
            }

            Newname = newFirstName.Text.ToString();
            Newfamily = newFamillyName.Text.ToString();

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            name = Newname;
            family = Newfamily;

            SQLmethodes.UpdateUserTable(EmailOfUser, email, name, family, password, shoppinglist, buyedlist, bookmarked, wallet, VIPTime, out exist);
            if (!exist)
            {
                MessageBoxResult message3 = MessageBox.Show($"Edits Sumbiton was unsuccessful");
                return;
            }

            MessageBoxResult message1 = MessageBox.Show("Edits Sumbited successfuly");
        }

        private void editNameBack_Click(object sender, RoutedEventArgs e)
        {
            editNameGrid.Visibility = Visibility.Hidden;
            editPanelGrid.Visibility = Visibility.Visible;
        }
        private void editPassBtn_Click(object sender, RoutedEventArgs e)
        {
            editPassGrid.Visibility = Visibility.Visible;
            editPanelGrid.Visibility = Visibility.Hidden;
        }

        private void editPassSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            string Password, RepeatPasswoed;

            if (!Check.PasswordCheck(newPass.Password.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Enter a correct Password"); return;
            }
            if (!Check.PasswordCheck(repeatPass.Password.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Enter a correct Password"); return;
            }

            Password = newPass.Password.ToString();
            RepeatPasswoed = repeatPass.Password.ToString();

            if (Password != RepeatPasswoed)
            {
                MessageBoxResult message = MessageBox.Show("Passwords are not equal"); return;
            }

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            password = Password;

            SQLmethodes.UpdateUserTable(EmailOfUser, email, name, family, password, shoppinglist, buyedlist, bookmarked, wallet, VIPTime, out exist);
            if (!exist)
            {
                MessageBoxResult message3 = MessageBox.Show($"Edits Sumbiton was unsuccessful");
                return;
            }

            MessageBoxResult message1 = MessageBox.Show("Edits Sumbited successfuly");
        }

        private void editPassBack_Click(object sender, RoutedEventArgs e)
        {
            editPassGrid.Visibility = Visibility.Hidden;
            editPanelGrid.Visibility = Visibility.Visible;
        }

        private void editProfileBack_Click(object sender, RoutedEventArgs e)
        {
            editProfileGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void buyVIPBtn_Click(object sender, RoutedEventArgs e)
        {
            float fee;
            bool exist;
            SQLmethodes.ReturnVIPfee(out exist, out fee);
            if (!exist) { MessageBoxResult message = MessageBox.Show("No VIP set");return; }
            isBuyingVIP = true;
            cost = fee;
            VIPGrid.Visibility = Visibility.Hidden;
            buyingPage.Visibility = Visibility.Visible;
        }

        private void VIPBack_Click(object sender, RoutedEventArgs e)
        {
            VIPGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void SearchDependingBook(object sender, RoutedEventArgs e)
        {
            if (searchBook.Text.ToString()==null)
            {
                MessageBoxResult message = MessageBox.Show("Enter name"); return;
            }

            string Name = searchBook.Text.ToString();

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
            int numberofpoints;
            string pdfpath;
            bool exist;

            SQLmethodes.ReturnBookStats(0, Name, out name, out authorname, out year, out price, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount, out numberofpoints, out pdfpath, out exist);
            if (!exist) return;
            try
            {
                imageofbook.Source = new BitmapImage(new Uri(bookimagepath));
            }catch(Exception error)
            {
                MessageBoxResult boxResult = MessageBox.Show(error.Message);return;
            }

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
                MessageBoxResult message5 = MessageBox.Show("Fill just one of the fields"); return;
            }

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            SqlConnection connectiontobooktable = SQLmethodes.SQLconnectionToBooksTable();
            connectiontobooktable.Open();

            string command = "select * from BookTable";
            SqlDataAdapter adapter2 = new SqlDataAdapter(command, connectiontobooktable);
            DataTable databook = new DataTable();
            adapter2.Fill(databook);

            string price = "";

            for (int i = 0; i < databook.Rows.Count; i++)
            {
                if (nameofbook != null && databook.Rows[i][0].ToString().ToLower() == nameofbook.ToLower())
                {

                    if (databook.Rows[i][6].ToString() == "True" && VIPTime == "")
                    {
                        MessageBoxResult message3 = MessageBox.Show($"Adding Book was unsuccessful! \n this book needs VIP subscription");
                        return;
                    }
                    price = databook.Rows[i][3].ToString();
                    break;
                }
                if (nameofauthor != null && databook.Rows[i][1].ToString().ToLower() == searchAuthor.Text.ToString())
                {
                    if (databook.Rows[i][6].ToString() == "True" && VIPTime == "")
                    {
                        MessageBoxResult message3 = MessageBox.Show($"Adding Book was unsuccessful! \n this book needs VIP subscription");
                        return;
                    }
                    price = databook.Rows[i][3].ToString();
                    break;
                }
            }

            connectiontobooktable.Close();

            shoppinglist += $"{nameofbook} {price},";


            SQLmethodes.UpdateUserTable(EmailOfUser, email, name, family, password, shoppinglist, buyedlist, bookmarked, wallet, VIPTime, out exist);
            if (!exist)
            {
                MessageBoxResult message3 = MessageBox.Show($"Adding Book was unsuccessful!");
                return;
            }
            MessageBoxResult message = MessageBox.Show("Book Added successfuly!");


            searchGrid.Visibility = Visibility.Hidden;
            ShowBook.Visibility = Visibility.Visible;
        }

        private void SearchDependingAuthor(object sender, RoutedEventArgs e)
        {
            if (searchAuthor.Text.ToString()==null)
            {
                MessageBoxResult message = MessageBox.Show("Enter name"); return;
            }

            string Name = searchAuthor.Text.ToString();
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
            int numberofpoints;
            string pdfpath;
            bool exist;

            SQLmethodes.ReturnBookStats(1, Name, out name, out authorname, out year, out price, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount, out numberofpoints, out pdfpath, out exist);
            if (!exist) return;

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

            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            //list
            List<string> books = new List<string>();
            string[] arr = bookmarked.Split(',');

            foreach (string nameofbookmarked in arr) { books.Add(nameofbookmarked.Trim()); }

            YourListBox.ItemsSource = books;
            bookmarksGrid.Visibility = Visibility.Visible;
        }

        private void WalletGrid_Loaded(object sender, RoutedEventArgs e)
        {
            string email;
            string name;
            string family;
            string password;
            string shoppinglist;
            string buyedlist;
            string bookmarked;
            float wallet;
            string VIPTime;
            bool exist;

            SQLmethodes.ReturnUserStats(EmailOfUser, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            walletBall.Text = wallet.ToString();
        }

        private void SumbitPoint(object sender, RoutedEventArgs e)
        {
            int userpoint;

            if (PointGiven.Text.ToString() == null || !int.TryParse(PointGiven.Text.ToString() , out userpoint))
            { MessageBoxResult message = MessageBox.Show("Enter an integer between 0 - 5"); return; }

            if(userpoint>5 || userpoint < 0) { MessageBoxResult message = MessageBox.Show("Enter an integer between 0 - 5"); return; }

            string Name = searchBook.Text.ToString();

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
            int numberofpoints;
            string pdfpath;
            bool exist;

            SQLmethodes.ReturnBookStats(0, Name, out name, out authorname, out year, out price, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount, out numberofpoints, out pdfpath, out exist);
            if (!exist) return;

            if (point == 0) point += userpoint;
            else
            {
                point += userpoint;
                numberofpoints++;
                point = point / 2;
            }

            bool ok;
            SQLmethodes.DeleteBookFromBookTable(Name, out ok);
            if (!ok) return;

            bool isok;
            SQLmethodes.AddBookToBookTable(name, authorname, year, price, bookdescription, authorprofile, isvip, salenumber, point, bookimagepath, vipfee, timefordiscount, discount, numberofpoints, pdfpath, out isok);
            if (!isok) return;

            MessageBoxResult messageBox = MessageBox.Show("Point Submited successfuly");
            PointGiven.Text = null;
        }

        private void BuyingPageBack_Click(object sender, RoutedEventArgs e)
        {
            cost = 0;
            buyingPage.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
            numberofcard.Text = "";
            passwordofcard.Text = "";
            cvvofcard.Text = "";
        }

        private void SeePdf(object sender, RoutedEventArgs e)
        {
           string Name = searchBook.Text.ToString();

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
            int numberofpoints;
            string pdfpath;
            bool exist;

            SQLmethodes.ReturnBookStats(0, Name, out name, out authorname, out year, out price, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount, out numberofpoints, out pdfpath, out exist);
            if (!exist) return;

            try
            {
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = pdfpath;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception error)
            {
                MessageBox.Show("Could not open the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
