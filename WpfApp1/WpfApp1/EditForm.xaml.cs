using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
namespace WpfApp1
{

    public partial class EditForm : Window
    {
        Window1 AdminPanel { get; set; }
        public EditForm(Window1 adminpanel)
        {
            AdminPanel = adminpanel;
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
            AdminPanel.Show();
        }


        private void SubmitChange(object sender, RoutedEventArgs e)
        {
            bool exist = false;
            string nameofchoosedbook = NameOfSearchedBook.Text.ToString();
            string  choosedfield;

            if (ChoosedField.SelectedItem == null) { MessageBoxResult message = MessageBox.Show("Choose one option!"); return; }

            ComboBoxItem cbi = (ComboBoxItem)ChoosedField.SelectedItem;
            choosedfield = cbi.Content.ToString();

            string editedText;
            if (EditText.Text.ToString() != null || EditText.Text.ToString().Trim() != "") { editedText = EditText.Text.ToString().Trim(); }
            else { MessageBoxResult message = MessageBox.Show("Fill Edit Part!");return; }
            string command;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coding\ApProject\Book-Store-\WpfApp1\Books.mdf;Integrated Security=True;Connect Timeout=30");

            connection.Open();

            command = "select * from BookTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            int row = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if(data.Rows[i][0].ToString() == nameofchoosedbook)
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

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();
            
            

            
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


            name = data.Rows[row][0].ToString();
            authorname = data.Rows[row][1].ToString();
            year = data.Rows[row][2].ToString();
            price = data.Rows[row][3].ToString();
            bookdescription = data.Rows[row][4].ToString();
            authorprofile = data.Rows[row][5].ToString();
            isvip = Convert.ToBoolean( data.Rows[row][6] );
            salenumber = int.Parse( data.Rows[row][7].ToString() );
            point = int.Parse( data.Rows[row][8].ToString() );
            bookimagepath = data.Rows[row][9].ToString();

            string DeleteCommand="";
            string AddCommand="";
            

            switch (choosedfield)
            {
                case "Name":
                    {
                        if (!Regex.IsMatch(editedText, @"^[A-Za-z0-9\s]+$") || editedText.Trim() == "")
                        {
                            MessageBoxResult message = MessageBox.Show("Enter a name for book");
                            return;
                        }
                        name = editedText;
                        break;
                    }
                case "Price":
                    {
                        if (!Regex.IsMatch(editedText, "^[0-9]+$"))
                        {
                            MessageBoxResult message = MessageBox.Show("Enter a Price");
                            return;
                        }
                        price = editedText;
                        break;
                    }
                case "Year":
                    {
                        if (!Regex.IsMatch(editedText, "^[0-9]+$"))
                        {
                            MessageBoxResult message = MessageBox.Show("Enter a Year");
                            return;
                        }
                        year = editedText;
                        break;
                    }
                case "Author":
                    {
                        if (!Check.NameCheck(editedText))
                        {
                            MessageBoxResult message = MessageBox.Show("Enter a name for author");
                            return;
                        }
                        authorname = editedText;
                        break;
                    }
                case "Author_profile":
                    {
                        if (!Regex.IsMatch(editedText, @"^[A-Za-z0-9()_.-?!\s]+$"))
                        {
                            MessageBoxResult message = MessageBox.Show("Enter a description for author");
                            return;
                        }
                        authorprofile = editedText;
                        break;
                    }
                case "Book_description":
                    {
                        if (!Regex.IsMatch(editedText, @"^[A-Za-z0-9()_.-?!\s]+$"))
                        {
                            MessageBoxResult message = MessageBox.Show("Enter a description for book");
                            return;
                        }
                        bookdescription = editedText;
                        break;
                    }
            }

            DeleteCommand = "delete from BookTable where BookName = '" + nameofchoosedbook + "'";
            SqlCommand DeleteRow = new SqlCommand(DeleteCommand, connection);
            DeleteRow.ExecuteNonQuery();

            AddCommand = "insert into BookTable values" +
                    "('" + name + "','" + authorname.Trim() + "' , '" + year.Trim() + "','" + price.Trim() + "','" + bookdescription.Trim() + "','" + authorprofile.Trim() + "','" + isvip + "','" + salenumber + "','" + point + "' , '" + bookimagepath + "')";
            SqlCommand AddRow = new SqlCommand(AddCommand , connection);
            AddRow.ExecuteNonQuery();
            connection.Close();
            MessageBoxResult endmessage = MessageBox.Show("Changes Submited successfuly!");
        }
    }
}
