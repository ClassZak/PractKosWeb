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
            try
            {
                Service.PostAsyncDesktop
                (
                    new ModelsLibrary.Messages.MessageRequest
                    (
                        this.Input.Text, userSettingsManager.Username
                    )
                );
                UpdateView();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("Ошибка сервера\nПопробуйте обратиться к администратору", "Плохой запрос на сервер", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }



        private void UpdateView()
        {
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                Service.GetAsyncDesktop();


                bool serverFailed=false;
                serverFailed = Task.Run<bool>(() =>
                {
                    for (byte i=0;i!=50;++i)
                    {
                        Thread.Sleep(100);
                        if (Service.Messages.Count != 0)
                            return false;
                    }
                    return Service.Messages.Count == 0;
                }).Result;



                while (Service.Messages.Count == 0)
                    if (serverFailed) break;

                if (Service.Messages.Count == 0)
                {
                    MessageBox.Show("Нет элементов в списке сообщений", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //this.listViewMessages.Items.Clear();
                Dispatcher.Invoke(new Action(() =>
                {
                    this.listViewMessages.Items.Clear();
                    foreach (var mItem in  Service.Messages)
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
                        this.listViewMessages.Items.Add(item);
                    } 
                }));
            });
        }
    }
}