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



        private void UpdateView()
        {
            

            bool serverFailed=false;
            serverFailed = Task.Run<bool>(() =>
            {
                if(Service.Messages.Count == 0)
                Thread.Sleep(5000);
                return Service.Messages.Count == 0;
            }).Result;



            while (Service.Messages.Count == 0)
                if (serverFailed) break;

            if (Service.Messages.Count == 0)
            {
                MessageBox.Show("Нет элементов в списке сообщений", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //this.LV_Messages.Items.Clear();
            foreach(var mItem in  Service.Messages)
            {
                ListViewItem item = new ListViewItem();
                var data = new
                { 
                    ID = mItem.Id, 
                    User = mItem.Username, 
                    Time = mItem.DateTime.ToUniversalTime().ToString(), 
                    Message = mItem.Content 
                };
                item.Content = data;
                this.LV_Messages.Items.Add(item);
            }

            
                
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