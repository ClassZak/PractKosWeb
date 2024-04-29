using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Service;

namespace ClientWPFGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static private Service.Service Service =
            new Service.Service("localhost",5250);

        private string Username = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserNameUse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            Username=this.UserNameBox.Text;
            Service.Post(new ModelsLibrary.Messages.MessageRequest(this.Input.Text, Username));
        }
    }
}