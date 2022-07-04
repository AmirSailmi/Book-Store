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
            string searchedbook, type="", command;
            bool IsVip;
            float Fee;
            string fee = "";
            bool exist = false;
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

            if (!float.TryParse(VIPmonthlyFee.Text.ToString(), out Fee)) { MessageBoxResult message = MessageBox.Show("Enter monthly fee!"); return; } else
            {
                if (Fee < 0)
                {
                    MessageBoxResult message = MessageBox.Show("Enter positive value"); return;
                }

                if (Type.Text.ToString() == "Normal" && Fee>0)
                {
                    MessageBoxResult message = MessageBox.Show("when account is normal you can not set VIP subscription monthly fee"); return;
                }
            }

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Books.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            command = "select * from BookTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == SearchedBookName.Text.ToString())
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no book with this name!");
                return;
            }

            string name;
            string price;
            string year;
            string authorname;
            string authorprofile;
            string bookdescription;
            bool isvip = IsVip;
            int salenumber;
            int point;
            string bookimagepath;
            float vipfee = Fee;
            string timefordiscount;
            float discount;

            name = data.Rows[row][0].ToString();
            authorname = data.Rows[row][1].ToString();
            year = data.Rows[row][2].ToString();
            price = data.Rows[row][3].ToString();
            bookdescription = data.Rows[row][4].ToString();
            authorprofile = data.Rows[row][5].ToString();
            salenumber = int.Parse(data.Rows[row][7].ToString());
            point = int.Parse(data.Rows[row][8].ToString());
            bookimagepath = data.Rows[row][9].ToString();
            timefordiscount = data.Rows[row][11].ToString();
            discount = float.Parse(data.Rows[row][12].ToString());

            if (isvip == false)
                Fee = 0;

            string DeleteCommand = "";
            string AddCommand = "";

            DeleteCommand = "delete from BookTable where BookName = '" + searchedbook + "'";
            SqlCommand DeleteRow = new SqlCommand(DeleteCommand, connection);
            DeleteRow.ExecuteNonQuery();

            AddCommand = "insert into BookTable values" +
                    "('" + name + "','" + authorname.Trim() + "' , '" + year.Trim() + "','" + price.Trim() + "','" + bookdescription.Trim() + "','" + authorprofile.Trim() + "','" + isvip + "','" + salenumber + "','" + point + "' , '" + bookimagepath + "','" + vipfee + "','" + timefordiscount + "','" + discount + "')";
            SqlCommand AddRow = new SqlCommand(AddCommand, connection);
            AddRow.ExecuteNonQuery();

            connection.Close();
            MessageBoxResult endmessage = MessageBox.Show($"Changes Submited successfuly!\n=> name = {searchedbook}\ntype = {type}\nVIP Subscription fee = {Fee}");
        }

        private void SubmitChangesForDiscount(object sender, RoutedEventArgs e)
        {
            bool exist=false;
            int dayInt, monthInt, yearInt;
            float discountpercentage = 0;
            string date, choosedbook,command;

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

            date = $"{dayInt}/{monthInt}/{yearInt}";

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Books.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            command = "select * from BookTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == choosedbook)
                {
                    exist = true;
                    row = i;
                }
            }

            if (!exist)
            {
                connection.Close();
                MessageBoxResult messageBox = MessageBox.Show("There is no book with this name!");
                return;
            }

            string name;
            string price;
            string year;
            string authorname;
            string authorprofile;
            string bookdescription;
            bool isvip ;
            int salenumber;
            int point;
            string bookimagepath;
            float vipfee ;
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
            timefordiscount = date;
            discount = discountpercentage;

            string DeleteCommand = "";
            string AddCommand = "";

            DeleteCommand = "delete from BookTable where BookName = '" + choosedbook + "'";
            SqlCommand DeleteRow = new SqlCommand(DeleteCommand, connection);
            DeleteRow.ExecuteNonQuery();

            AddCommand = "insert into BookTable values" +
                    "('" + name + "','" + authorname.Trim() + "' , '" + year.Trim() + "','" + price.Trim() + "','" + bookdescription.Trim() + "','" + authorprofile.Trim() + "','" + isvip + "','" + salenumber + "','" + point + "' , '" + bookimagepath + "','" + vipfee + "','" + timefordiscount + "','" + discount + "')";
            SqlCommand AddRow = new SqlCommand(AddCommand, connection);
            AddRow.ExecuteNonQuery();

            connection.Close();
            MessageBoxResult endmessage = MessageBox.Show($"Changes Submited successfuly!\n=> name = {choosedbook}\ntime = {date}\nDiscount Percentage = {discountpercentage}");
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
            adminPanel.Show();
        }
    }
}
