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
using ModelsLibrary;
using ModelsLibrary.UserModels;
using Newtonsoft.Json.Linq;

namespace ClientWPFGUI
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public Service.Service Service = new();
        public User User = new User();
        private bool registrationRequested=false;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private async void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (!registrationRequested)
                        registrationRequested = true;
                    else
                        return;

                    string login = "", password = "";
                    Dispatcher.Invoke(() =>
                    {
                        login = this.LoginTextBox.Text;
                        password = this.PasswordTextBox.Text;
                    });
                    if (login == "" || password == "")
                    {
                        MessageBox.Show
                        (
                            "Введите логин и пароль",
                            "Ошибка ввода",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning
                        );
                        return;
                    }

                    UserAuthorizationArg userAuthorizationArg =
                    new UserAuthorizationArg(login, password);




                    string Token=
                    Service.PostAsyncDesktopRegistration
                    (
                        userAuthorizationArg,
                        new Uri("http://localhost:5250/api/User/AddUser")
                    ).Result.Value;

                    if (Token != "")
                    {
                        User = new User(userAuthorizationArg, Token);
                        MessageBox.Show($"Ваш логин:\n{User.Name}\nВаш пароль:\n{User.Password}\nВаш токен:\n{User.Token}");
                        Dispatcher.Invoke(() => { Close(); });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Ошибка регистрации",MessageBoxButton.OK, MessageBoxImage.Error);
                    registrationRequested = false;
                }
            });
        }
    }
}
