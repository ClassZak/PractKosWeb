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
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            UserAuthorizationArg userAuthorizationArg =
            new UserAuthorizationArg(this.LoginTextBox.Text, this.PasswordTextBox.Text);

            string Token=
            Service.PostAsyncDesktopRegistration
            (
                userAuthorizationArg,
                new Uri("http://localhost:5250/api/User/AddUser")
            ).Result.Value;

            if (Token != "")
            {
                this.LoginTextBox.Text = Token;
                User = new User(userAuthorizationArg, Token, true);
                MessageBox.Show($"Ваш логин:\n{User.Name}\nВаш пароль:\n{User.Password}\nВаш токен:\n{User.Token}");
            }
        }
    }
}
