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
    /// Interaction logic for VIPmembers.xaml
    /// </summary>
    public partial class VIPmembers : Window
    {
        public VIPmembers()
        {
            InitializeComponent();
        }

        private void VIPmembersShowBtn_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection con = SQLmethodes.SQLconnectionToUsersTable();
            con.Open();
            string command = "select * from UserTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable data = new DataTable();
            adapter.Fill(data);
            List<string> names = new List<string>();
            List<string> Emails = new List<string>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][8] != "")
                {
                    names.Add(data.Rows[i][1].ToString());
                    Emails.Add(data.Rows[i][0].ToString());
                }
            }
            VIPnamesList.ItemsSource = names;
            VIPemailsList.ItemsSource = Emails;
            con.Close();

        }

        private void VIPmembersBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //ReformtoAdmin.Show();
        }
    }
}
