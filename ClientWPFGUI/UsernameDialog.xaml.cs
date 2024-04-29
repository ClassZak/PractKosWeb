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

namespace ClientWPFGUI
{
    /// <summary>
    /// Логика взаимодействия для UsernameDialog.xaml
    /// </summary>
    public partial class UsernameDialog : Window
    {
        public string username="";
        public UsernameDialog()
        {
            InitializeComponent();
        }
        public UsernameDialog(string username) : this()
        {
            this.username = username;
            this.UsernameBox.Text = this.username;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.username=this.UsernameBox.Text;
            this.Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            
        }
    }
}
