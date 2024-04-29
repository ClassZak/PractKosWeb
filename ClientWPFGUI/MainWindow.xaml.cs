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
            Service.Get();
            UpdateView();
            //MessageBox.Show(this.LV_Messages.DataContext.ToString(), "");
        }



        private async void UpdateView()
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    ListViewItem item = new ListViewItem();

                if (Service.Messages.Count == 0)
                    Thread.Sleep(5000);
                if (Service.Messages.Count == 0)
                {
                    MessageBox.Show("Нет элементов в списке сообщений", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            
                var data = new {  ID = Service.Messages.Last().Id, Column2 = "Данные2", Column3 = "Данные3", Column4 = "Данные4" };
                item.Content = data;
                this.LV_Messages.Items.Add(item);
                });
                
            });
        }


        private async void UpdateWaiting()
        {
            bool serverFailed = true;
            await Task.Run(async () =>
            {
                await Task.Run(() =>
                {
                    while(true)
                    if (Service.Messages.Count != 0)
                        serverFailed = false;
                });
                Thread.Sleep(5000);
                    
            });
            if(serverFailed)
            {
                MessageBox.Show("Нет элементов в списке сообщений", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}