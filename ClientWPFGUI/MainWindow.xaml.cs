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

        static private UserSettingsManager userSettingsManager = new UserSettingsManager();
        public MainWindow()
        {
            InitializeComponent();
            this.UserNameBox.Text=userSettingsManager.Username;
        }

        private void UserNameUse_Click(object sender, RoutedEventArgs e)
        {
            UsernameDialog dlg = new UsernameDialog(userSettingsManager.Username);
            dlg.ShowDialog();
            userSettingsManager.SetUsername(dlg.username);
            this.UserNameBox.Text = userSettingsManager.Username;
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            Service.Post
            (
                new ModelsLibrary.Messages.MessageRequest
                (
                    this.Input.Text, userSettingsManager.Username
                )
            );
        }
    }
}