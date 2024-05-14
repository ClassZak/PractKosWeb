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
using ModelsLibrary.UserModels;
using Service;

namespace ClientWPFGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public User User = new();

        static private Service.Service Service =
            new Service.Service("localhost",5250);

        static private UserSettingsManager userSettingsManager = new UserSettingsManager();
        public MainWindow()
        {
            InitializeComponent();


            this.UserNameBox.Text=userSettingsManager.Username;




            AuthorizationWindow window = new AuthorizationWindow();
            window.ShowDialog();
            User = new User(window.User);

            if (window.UserChanged)
                this.UserNameBox.Text = User.Name;
        }

        private void UserNameUse_Click(object sender, RoutedEventArgs e)
        {
            /*UsernameDialog dlg = new UsernameDialog(userSettingsManager.Username);
            dlg.ShowDialog();
            userSettingsManager.SetUsername(dlg.username);
            this.UserNameBox.Text = userSettingsManager.Username;*/

            AuthorizationWindow window = new AuthorizationWindow(false);
            window.ShowDialog();
            User = new User(window.User);

            if(window.UserChanged)
                this.UserNameBox.Text = User.Name;
        }

        private async void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    Dispatcher.Invoke(() => {Service.SetNewAddress(this.IPBox.Text,this.PortBox.Text); });
                    
                }
                catch
                {
                    MessageBox.Show("Неверный адрес сервера", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }



                try
                {
                    ModelsLibrary.Messages.MessageRequest msg=new ModelsLibrary.Messages.MessageRequest();
                    Dispatcher.Invoke(() =>
                    {
                        msg = 
                        new ModelsLibrary.Messages.MessageRequest(this.Input.Text, userSettingsManager.Username);
                    });

                    int res= Service.PostAsyncDesktop(msg).Result;



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
            });
        }



        private void UpdateView()
        {
            Task.Run(() =>
            {
                Service.GetAsyncDesktop();


                bool serverFailed=false;
                serverFailed = Task.Run<bool>(() =>
                {
                    for (byte i=0;i!=100;++i)
                    {
                        Thread.Sleep(50);
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
                    for
                    (
                        int i= listViewMessages.Items.Count==0 ? 0 : listViewMessages.Items.Count; 
                        i<Service.Messages.Count;
                        ++i
                    )
                    {
                        ListViewItem item = new ListViewItem();
                        var data = new
                        {
                            ID = Service.Messages[i].Id,
                            User = Service.Messages[i].Username,
                            Time = Service.Messages[i].DateTime,
                            Message = Service.Messages[i].Content
                        };
                        item.Content = data;
                        if(!this.listViewMessages.Items.Contains(item))
                            this.listViewMessages.Items.Add(item);
                    }
                }));
            });
        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}