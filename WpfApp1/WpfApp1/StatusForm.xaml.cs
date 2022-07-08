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
    /// Interaction logic for StatusForm.xaml
    /// </summary>
    public partial class StatusForm : Window
    {
        Window1 adminPanel { get; set; }
        public StatusForm(Window1 window)
        {
            InitializeComponent();
            adminPanel = window;
        }

        private void SubmitChangesForType(object sender, RoutedEventArgs e)
        {
            string searchedbook, type = "";
            bool IsVip;
            float Fee;

            if (Check.NameCheck(SearchedBookName.Text.ToString()))
            {
                searchedbook = SearchedBookName.Text.ToString();
            }
            else { MessageBoxResult message = MessageBox.Show("Enter Name of Book!"); return; }

            if (Type.SelectedItem != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)Type.SelectedItem;
                type = cbi.Content.ToString();
            }
            else
            {
                MessageBoxResult message = MessageBox.Show("Choose one type!"); return;
            }

            if (type == "VIP") IsVip = true;
            else if (type == "Normal") IsVip = false;
            else { MessageBoxResult message = MessageBox.Show("Choose one type!"); return; }

            if (!float.TryParse(VIPmonthlyFee.Text.ToString(), out Fee)) { MessageBoxResult message = MessageBox.Show("Enter monthly fee!"); return; }
            else
            {
                if (Fee < 0)
                {
                    MessageBoxResult message = MessageBox.Show("Enter positive value"); return;
                }

                if (Type.Text.ToString() == "Normal" && Fee > 0)
                {
                    MessageBoxResult message = MessageBox.Show("when account is normal you can not set VIP subscription monthly fee"); return;
                }
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
            int numberofpoints;
            string pdfpath;
            bool exist;

            SQLmethodes.ReturnBookStats(0, searchedbook, out name, out authorname, out year, out price, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount, out numberofpoints, out pdfpath, out exist);
            if (!exist) return;

            isvip = IsVip;
            vipfee = Fee;

            if (isvip == false)
                Fee = 0;

            bool ok;
            SQLmethodes.DeleteBookFromBookTable(name, out ok);
            if (!ok) return;

            bool isok;
            SQLmethodes.AddBookToBookTable(name, authorname, year, price, bookdescription, authorprofile, isvip, salenumber, point, bookimagepath, vipfee, timefordiscount, discount, numberofpoints, pdfpath, out isok);
            if (!isok) return;

            MessageBoxResult endmessage = MessageBox.Show($"Changes Submited successfuly!\n=> name = {searchedbook}\ntype = {type}\nVIP Subscription fee = {Fee}");
        }

        private void SubmitChangesForDiscount(object sender, RoutedEventArgs e)
        {
            int dayInt, monthInt, yearInt;
            float discountpercentage = 0;
            string date, choosedbook;

            if (!Check.NameCheck(ChoosedBook.Text.ToString()))
            {
                MessageBoxResult message = MessageBox.Show("Enter name"); return;
            }
            else { choosedbook = ChoosedBook.Text.ToString(); }

            if (day.Text.ToString() == null || !int.TryParse(day.Text.ToString(), out dayInt) || int.Parse(day.Text.ToString()) < 1 || int.Parse(day.Text.ToString()) > 31)
            {
                MessageBoxResult message = MessageBox.Show("Enter day"); return;
            }
            else if (month.Text.ToString() == null || !int.TryParse(month.Text.ToString(), out monthInt) || int.Parse(month.Text.ToString()) < 1 || int.Parse(month.Text.ToString()) > 12)
            {
                MessageBoxResult message = MessageBox.Show("Enter month"); return;
            }
            else if (YEAR.Text.ToString() == null || !int.TryParse(YEAR.Text.ToString(), out yearInt) || int.Parse(YEAR.Text.ToString()) < 0)
            {
                MessageBoxResult message = MessageBox.Show("Enter year"); return;
            }

            if (DiscountPercentage.Text.ToString() == null || !float.TryParse(DiscountPercentage.Text.ToString(), out discountpercentage) || float.Parse(DiscountPercentage.Text.ToString()) < 0)
            {
                MessageBoxResult message = MessageBox.Show("Enter a dicount percentage"); return;
            }

            date = $"{monthInt}/{dayInt}/{yearInt}";

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

            SQLmethodes.ReturnBookStats(0, choosedbook, out name, out authorname, out year, out price, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount, out numberofpoints, out pdfpath, out exist);
            if (!exist) return;

            timefordiscount = date;
            discount = discountpercentage;

            bool ok;
            SQLmethodes.DeleteBookFromBookTable(name, out ok);
            if (!ok) return;

            bool isok;
            SQLmethodes.AddBookToBookTable(name, authorname, year, price, bookdescription, authorprofile, isvip, salenumber, point, bookimagepath, vipfee, timefordiscount, discount, numberofpoints, pdfpath, out isok);
            if (!isok) return;

            MessageBoxResult endmessage = MessageBox.Show($"Changes Submited successfuly!\n=> name = {choosedbook}\ntime = {date}\nDiscount Percentage = {discountpercentage}");
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
            adminPanel.Show();
        }
    }
}
