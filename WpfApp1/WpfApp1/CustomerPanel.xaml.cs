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
    /// Interaction logic for CustomerPanel.xaml
    /// </summary>
    public partial class CustomerPanel : Window
    {
        MainWindow main_window { get; set; }
        public CustomerPanel(MainWindow window)
        {
            main_window = window;
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
            bookmarksGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Hidden;
        }

        private void CartBtn_Click(object sender, RoutedEventArgs e)
        {
            cartGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Hidden;
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

        }

        private void cartBuyBtn_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
