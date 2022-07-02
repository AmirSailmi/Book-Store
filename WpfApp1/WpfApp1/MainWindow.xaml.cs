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
                Window1 AdminPanel = new Window1(Username , Password);
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
            bool error = false;
            string Name = Customername_SignUP.Text.ToString();
            string Family = CustomerFamily_SignUP.Text.ToString();
            string Email = CustomerEmail_SignUP.Text.ToString();
            string Password = CustomerPassword_SignUP.Password.ToString();

            if(Check.NameCheck(Name) && Check.NameCheck(Family)) { MessageBoxResult message = MessageBox.Show("Wrong name / family", "Sign up error");error = true; }

            if (Check.EmailCheck(Email)) { MessageBoxResult message = MessageBox.Show("Wrong email", "Sign up error"); error = true; }

            if (Check.PasswordCheck(Password)) { MessageBoxResult message = MessageBox.Show("Wrong passwoord", "Sign up error"); error = true; }

            if(error != true)
            {
                //Store data in database and open user panel
            }
        }

        private void UserSingIn(object sender, RoutedEventArgs e)
        {
            //search sql for user
        }

        private void ExitAdminForm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExitUserForm(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
