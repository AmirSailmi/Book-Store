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
    /// Interaction logic for Fund.xaml
    /// </summary>
    public partial class Fund : Window
    {
        Window1 AdminPanel { get; set; }
        public Fund(Window1 adminpanel)
        {
            AdminPanel = adminpanel;
            InitializeComponent();
            SqlConnection con = SQLmethodes.SQLconnectionToBooksTable();
            con.Open();
            string command = "select * from TotalMoney";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable data = new DataTable();
            adapter.Fill(data);
            fundMoney.Text = data.Rows[0][0].ToString();

            con.Close();

        }

        private void FundBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AdminPanel.Show();
        }
    }
}
