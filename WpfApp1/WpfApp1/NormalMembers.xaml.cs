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
    /// Interaction logic for NormalMembers.xaml
    /// </summary>
    public partial class NormalMembers : Window
    {
        Window1 AdminPanel { get; set; }
        public NormalMembers(Window1 adminpanel)
        {
            AdminPanel = adminpanel;
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
                if (data.Rows[i][8] == "")
                {
                    names.Add(data.Rows[i][1].ToString());
                    Emails.Add(data.Rows[i][0].ToString());
                }
            }
            NormalnamesList.ItemsSource = names;
            NormalemailsList.ItemsSource = Emails;
            con.Close();
        }

        private void NormalMembersBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AdminPanel.Show();
        }
    }
}
