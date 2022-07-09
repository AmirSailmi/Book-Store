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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AdminSignIn(object sender, RoutedEventArgs e)
        {
            string Username = username.Text.ToString();
            string Password = password.Password.ToString();

            if (Check.NameCheck(Username) && Check.PasswordCheck(Password))
            {
                Window1 AdminPanel = new Window1(Username, Password, this);
                this.Visibility = Visibility.Hidden;
                AdminPanel.Show();
            }
            else
            {
                MessageBoxResult message = MessageBox.Show("Wrong input", "Sign in error");
            }
        }

        private void UserSignUp(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Saleh\PROGRAMING\DB\APproject\users.MDF;Integrated Security=True;Connect Timeout=30");

            if (!Check.NameCheck(Customername_SignUP.Text.ToString()) || !Check.NameCheck(CustomerFamily_SignUP.Text.ToString())) { MessageBoxResult message = MessageBox.Show("Wrong name / family", "Sign up error"); return; }

            if (!Check.EmailCheck(CustomerEmail_SignUP.Text.ToString())) { MessageBoxResult message = MessageBox.Show("Wrong email", "Sign up error"); return; }

            if (!Check.PasswordCheck(CustomerPassword_SignUP.Password.ToString())) { MessageBoxResult message = MessageBox.Show("Wrong passwoord", "Sign up error"); return; }

            string Name = Customername_SignUP.Text.ToString();
            string Family = CustomerFamily_SignUP.Text.ToString();
            string Email = CustomerEmail_SignUP.Text.ToString();
            string Password = CustomerPassword_SignUP.Password.ToString();
            string ShoppingList = "", BuyedList = "", BookMarked = "",VIPTime="";
            float wallet = 0;
            bool ok;

            SQLmethodes.AddToUserTable(Email, Name, Family, Password, ShoppingList, BuyedList, BookMarked, wallet, VIPTime, out ok);
            if (!ok) return;

            MessageBoxResult messageend = MessageBox.Show($"{Name} {Family} signed up successfuly");
        }

        private void UserSingIn(object sender, RoutedEventArgs e)
        {
            if (!Check.EmailCheck(CustomerEmail_SignIn.Text.ToString())) { MessageBoxResult message = MessageBox.Show("Wrong email", "Sign up error"); return; }

            if (!Check.PasswordCheck(Password_SignIn.Password.ToString())) { MessageBoxResult message = MessageBox.Show("Wrong passwoord", "Sign up error"); return; }

            string Email = CustomerEmail_SignIn.Text.ToString();
            string Password = Password_SignIn.Password.ToString();

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

            SQLmethodes.ReturnUserStats(Email, out email, out name, out family, out password, out shoppinglist, out buyedlist, out bookmarked, out wallet, out VIPTime, out exist);
            if (!exist) return;

            if (password != Password)
            {
                MessageBoxResult message = MessageBox.Show("Wrong password");return;
            }

            CustomerPanel customerPanel = new CustomerPanel(this , email ,password);
            customerPanel.Show();
            this.Visibility = Visibility.Hidden;
        }

        public void ExitAdminForm(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

        private void ExitUserForm(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
