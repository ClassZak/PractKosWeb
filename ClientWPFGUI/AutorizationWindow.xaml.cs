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
        public User User = new User();
        public string Token = "";

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }

        private async void AthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                string login="",password="";
                Dispatcher.Invoke(() =>
                {
                    login = this.LoginTextBox.Text;
                    password = this.PasswordTextBox.Text;
                });

                if (login == "" || password == "")
                {
                    MessageBox.Show("Введите логин и пароль", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    ModelsLibrary.UserModels.Enums.AuthorizationCode code =
                    Service.PostAsyncDesktopAuthorization
                    (
                        new ModelsLibrary.UserModels.User
                        (
                            login,
                            password
                        ),
                        new Uri("http://localhost:5250/api/User/Authorization")
                    ).Result;
                    if (code == ModelsLibrary.UserModels.Enums.AuthorizationCode.AthorizedSuccessful)
                        Dispatcher.Invoke(() => { Close(); });
                        
                }
            });
        }
    }
}
