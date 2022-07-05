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
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Users.mdf;Integrated Security=True;Connect Timeout=30");

            if (!Check.NameCheck(Customername_SignUP.Text.ToString()) || !Check.NameCheck(CustomerFamily_SignUP.Text.ToString())) { MessageBoxResult message = MessageBox.Show("Wrong name / family", "Sign up error"); return; }

            if (!Check.EmailCheck(CustomerEmail_SignUP.Text.ToString())) { MessageBoxResult message = MessageBox.Show("Wrong email", "Sign up error"); return; }

            if (!Check.PasswordCheck(CustomerPassword_SignUP.Password.ToString())) { MessageBoxResult message = MessageBox.Show("Wrong passwoord", "Sign up error"); return; }

            string Name = Customername_SignUP.Text.ToString();
            string Family = CustomerFamily_SignUP.Text.ToString();
            string Email = CustomerEmail_SignUP.Text.ToString();
            string Password = CustomerPassword_SignUP.Password.ToString();
            string ShoppingList = "", BuyedList = "", BookMarked = "";

            try
            {
                connection.Open();

                string command = "insert into UserTable values" +
                        "('" + Email.Trim() + "','" + Name.Trim() + "' , '" + Family.Trim() + "','" + Password.Trim() + "','" + ShoppingList.Trim() + "','" + BuyedList + "','" + BookMarked + "')";
                SqlCommand Command = new SqlCommand(command, connection);
                Command.ExecuteNonQuery();

                connection.Close();
            }catch(Exception Er)
            {
                connection.Close();
                MessageBoxResult message = MessageBox.Show(Er.Message);
                return;
            }

            MessageBoxResult messageend = MessageBox.Show($"{Name} {Family} signed up successfuly");
        }

        private void UserSingIn(object sender, RoutedEventArgs e)
        {
            if (!Check.EmailCheck(CustomerEmail_SignIn.Text.ToString())) { MessageBoxResult message = MessageBox.Show("Wrong email", "Sign up error"); return; }

            if (!Check.PasswordCheck(Password_SignIn.Password.ToString())) { MessageBoxResult message = MessageBox.Show("Wrong passwoord", "Sign up error"); return; }

            string email = CustomerEmail_SignIn.Text.ToString();
            string password = Password_SignIn.Password.ToString();

            string Command = "select * from UserTable";
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Users.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(Command, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][0].ToString().ToLower() == email.ToLower())
                {
                    if (dataTable.Rows[i][3].ToString() != password)
                    {
                        MessageBoxResult message = MessageBox.Show("Password uncorrect!");
                        return;
                    }
                    break;
                }
            }


            CustomerPanel customerPanel = new CustomerPanel(this);
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
