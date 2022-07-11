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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for BookList.xaml
    /// </summary>
    public partial class BookList : Window
    {
        Window1 adminpanel { get; set; }

        public BookList(Window1 window)
        {
            adminpanel = window;
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
            adminpanel.Show();
        }

        private void Show(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = SQLmethodes.SQLconnectionToBooksTable())
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

                string CmdString = string.Empty;

                using (SqlConnection con = SQLmethodes.SQLconnectionToBooksTable())

                {

                    CmdString = "SELECT BookName, AuthorName, Price, IsVIP, Year, Point, VIPsubscriptionmonthlyfee, Discount, TimeforDiscount FROM BookTable";

                    SqlCommand cmd = new SqlCommand(CmdString, con);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable("BookTable");

                    sda.Fill(dt);

                    DataGrid.AutoGenerateColumns = false;
                    DataGrid.IsReadOnly = true;
                    DataGrid.ItemsSource = dt.DefaultView;

                }
            }
        }
    }
}
