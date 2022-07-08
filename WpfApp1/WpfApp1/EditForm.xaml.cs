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
            string nameofchoosedbook = NameOfSearchedBook.Text.ToString();
            string  choosedfield;
            if (ChoosedField.SelectedItem == null) { MessageBoxResult message = MessageBox.Show("Choose one option!"); return; }
            ComboBoxItem cbi = (ComboBoxItem)ChoosedField.SelectedItem;
            choosedfield = cbi.Content.ToString();

            string editedText;
            if (EditText.Text.ToString() != null || EditText.Text.ToString().Trim() != "") { editedText = EditText.Text.ToString().Trim(); }
            else { MessageBoxResult message = MessageBox.Show("Fill Edit Part!");return; }

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

            SQLmethodes.ReturnBookStats(0, nameofchoosedbook, out name, out authorname, out year, out price, out bookdescription, out authorprofile, out isvip, out salenumber, out point, out bookimagepath, out vipfee, out timefordiscount, out discount,out numberofpoints , out pdfpath ,out exist);
            if (!exist) return;

            switch (choosedfield)
            {
                case "Name":
                    {
                        if (Check.NameCheck(editedText))
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
                        if (!Regex.IsMatch(editedText, @"^[A-Za-z0-9()_.-?!,\s]+$"))
                        {
                            MessageBoxResult message = MessageBox.Show("Enter a description for author");
                            return;
                        }
                        authorprofile = editedText;
                        break;
                    }
                case "Book_description":
                    {
                        if (!Regex.IsMatch(editedText, @"^[A-Za-z0-9()_.-?,!\s]+$"))
                        {
                            MessageBoxResult message = MessageBox.Show("Enter a description for book");
                            return;
                        }
                        bookdescription = editedText;
                        break;
                    }
            }

            bool ok;
            SQLmethodes.DeleteBookFromBookTable(nameofchoosedbook, out ok);
            if (!ok) return;

            bool isok;
            SQLmethodes.AddBookToBookTable(name, authorname, year, price, bookdescription, authorprofile, isvip, salenumber, point, bookimagepath, vipfee, timefordiscount, discount,numberofpoints , pdfpath ,out isok);
            if (!isok) return;
          
            MessageBoxResult endmessage = MessageBox.Show("Changes Submited successfuly!");
        }
    }
}
