using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for GetMoney.xaml
    /// </summary>
    public partial class GetMoney : Window
    {
        Window1 AdminPanel { get; set; }

        public GetMoney(Window1 adminpanel)
        {
            AdminPanel = adminpanel;
            InitializeComponent();
        }

        private void GetMoneyBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AdminPanel.Show();
        }

        private void GetMoneySubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = SQLmethodes.SQLconnectionToBooksTable();
            con.Open();
            string command = "select * from TotalMoney";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable data = new DataTable();
            adapter.Fill(data);
            float t = float.Parse(data.Rows[0][0].ToString());
            con.Close();
            if (Check.checkid(CardNumber.Text))
            {
                if (t > float.Parse(MoneyAmount.Text))
                {
                    t -= float.Parse(MoneyAmount.Text);
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

                    MessageBox.Show("Operation done successfully.");
                }
                else
                {
                    MessageBox.Show("there is not enough money to get");
                }
            }
            else
            {
                MessageBox.Show("invalid card number");
            }
        }
    }
}
