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
    /// <summary>
    /// Interaction logic for CustomerPanel.xaml
    /// </summary>
    public partial class CustomerPanel : Window
    {
        public CustomerPanel()
        {
            InitializeComponent();
        }

        private void saerchBookBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackToLoginPage(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
