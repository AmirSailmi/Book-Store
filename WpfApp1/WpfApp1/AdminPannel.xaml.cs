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

            SQLmethodes.ReturnBookStats(0, Name, out name, out authorname, out year, out price, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount,out  numberofpoints , out  pdfpath, out exist);
            if (!exist) return;

            SearchBook searchBook = new SearchBook(name, price, year, authorname, authorprofile, bookdescription, isvip, salenumber, point, bookimagepath, vipfee, timefordiscount, discount, numberofpoints , pdfpath , this);
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
            string Name = nameofbook.Text.ToString();
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

            SQLmethodes.ReturnBookStats(1, Authorname, out name, out authorname, out year, out price, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount, out numberofpoints , out pdfpath,out exist);
            if (!exist) return;

            SearchBook searchBook = new SearchBook(name, price, year, authorname, authorprofile, bookdescription, isvip, salenumber, point, bookimagepath, vipfee, timefordiscount, discount,numberofpoints , pdfpath, this);
            this.Visibility = Visibility.Hidden;
            searchBook.Show();
        }

        private void EmailSearchedButton(object sender, RoutedEventArgs e)
        {

        }

        private void SetVIPprice(object sender, RoutedEventArgs e)
        {
            VIPSET.Visibility = Visibility.Visible;
        }

        private void SumbitmonthlyFee_Click(object sender, RoutedEventArgs e)
        {
            float monthlyFee;
            if(!float.TryParse(MonthlyFee.Text.ToString() , out monthlyFee) || monthlyFee < 0)
            {
                MessageBoxResult message = MessageBox.Show("Enter fee");return;
            }

            float fee;
            bool exist;

            SQLmethodes.ReturnVIPfee(out exist, out fee);
            if (fee==null)
            {
                bool done;
                SQLmethodes.AddToVIPfee(fee,out done);
                if (!done)
                {
                    MessageBoxResult message = MessageBox.Show("VIP fee isn\'t added");return;
                }
            }
            else
            {
                bool isok;
                SQLmethodes.EditVIPfee(monthlyFee, out isok);
                if (!isok)
                {
                    MessageBoxResult messageBox = MessageBox.Show("Edit isn\'t sumbited");return;
                }
            }
            MessageBoxResult message1 = MessageBox.Show("Fee sumbited"); 

            VIPSET.Visibility = Visibility.Hidden;
        }
    }
}
