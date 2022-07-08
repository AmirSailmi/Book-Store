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

    public partial class SearchBook : Window
    {
        public string name;
        string price;
        string year;
        string authorname;
        string authorprofile;
        string bookdescription;
        bool isvip;
        int salenumber;
        int point;
        public string bookimagepath;
        float vipfee;
        string timefordiscount;
        float discount;
        int numberofpoints;
        string pdfpath;
        Window1 adminpanel;
        public SearchBook(string name, string price, string year, string authorname,
            string authorprofile, string bookdescription, bool isvip, int salenumber
            , int point, string bookimagepath, float vipfee, string timefordiscount, float discount,int numberofpoints ,string pdfpath ,Window1 adminpanel)
        {
            this.name = name;
            this.price = price;
            this.year = year;
            this.authorname = authorname;
            this.authorprofile = authorprofile;
            this.bookdescription = bookdescription;
            this.isvip = isvip;
            this.salenumber = salenumber;
            this.point = point;
            this.bookimagepath = bookimagepath;
            this.vipfee = vipfee;
            this.timefordiscount = timefordiscount;
            this.discount = discount;
            this.adminpanel = adminpanel;
            this.numberofpoints = numberofpoints;
            this.pdfpath = pdfpath;

            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            imageofbook.Source = new BitmapImage(new Uri(bookimagepath));

            YEAR.Text = year;
            authorName.Text = authorname;
            pointe.Text ="point = " + point.ToString();
            BookName.Text ="Name = " + name;
            description.Text = "Description = " +bookdescription;
            Pricee.Text ="Price = "+ price;
            if (isvip == true)
            {
                VIPe.Text = "VIP";
                VIPFEE.Text = "fee = " +vipfee.ToString();
            }
            if (discount != 0)
            {
                discounte.Text = $"{discount} percebtage off untill {timefordiscount}";
            }
            else
            {
                discounte.Text = "No Discount";
            }
        }
    }

    
}
