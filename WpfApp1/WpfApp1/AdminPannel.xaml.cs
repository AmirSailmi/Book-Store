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

        }

        private void Status(object sender, RoutedEventArgs e)
        {

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

        }


        private void SearchBookByBookName(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBookByAuthorName(object sender, RoutedEventArgs e)
        {

        }

        private void EmailSearchedButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
