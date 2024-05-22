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
using Service;
using ModelsLibrary.UserModels;

namespace ClientWPFGUI
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public Service.Service Service = new();
        public User User = new();
        public bool UserChanged=false;

        private bool autorizationButtonClicked = false;

        public AuthorizationWindow()
        {
            InitializeComponent();
            //if(WelcomeWindow)
            //    this.
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new(this.LoginTextBox.Text, this.PasswordTextBox.Text);
            registrationWindow.ShowDialog();
            User=new User(registrationWindow.User);
            this.LoginTextBox.Text = User.Name;
            this.PasswordTextBox.Text = User.Password;
        }

        private async void AthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                if(autorizationButtonClicked)
                {
                    MessageBox.Show("Запрос на авторизацию уже отправлен", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    autorizationButtonClicked = true;
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
                    else
                    {
                        try
                        {
                            KeyValuePair<ModelsLibrary.UserModels.Enums.AuthorizationCode, string> info =
                            Service.PostAsyncDesktopAuthorization
                            (
                                new ModelsLibrary.UserModels.User
                                (
                                    login,
                                    password
                                ),
                                new Uri("http://localhost:5250/api/User/Authorization")
                            ).Result;
                            if (info.Key == ModelsLibrary.UserModels.Enums.AuthorizationCode.AthorizedSuccessful)
                            {
                                User = new(login, password);
                                User.Token = info.Value;
                                Dispatcher.Invoke(() =>
                                {
                                    UserChanged = true;
                                    Close();
                                });
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Не удалось авторизоваться\nОшибка доступа к серверу", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    autorizationButtonClicked = false;
                }
            });
        }
    }
}
