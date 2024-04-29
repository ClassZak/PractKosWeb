using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Windows;

namespace ClientWPFGUI
{
    public class UserSettingsManager
    {
        const string FILENAME = "UserSettings.txt";
        public string Username
        {
            get;
            private set;
        }

        
        public UserSettingsManager()
        {
            Username = "Пользователь";
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), FILENAME)))
            {
                SaveUsernameToFile();
                File.SetAttributes(Path.Combine(Directory.GetCurrentDirectory(), FILENAME), FileAttributes.Hidden);
            }
            else
            {
                LoadUsernameFromFile();
            }
        }

        public void SetUsername(string newUsername)
        {
            Username = newUsername;
            SaveUsernameToFile();
        }


        private void SaveUsernameToFile()
        {
            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), FILENAME));

            StreamWriter streamWriter = new StreamWriter(new FileStream(FILENAME, FileMode.Create, FileAccess.Write));
            streamWriter.WriteLine(Username);
            streamWriter.Close();
        }
        private void LoadUsernameFromFile()
        {
            try
            {
                StreamReader streamReader = new StreamReader(new FileStream(FILENAME, FileMode.Open, FileAccess.Read));
                Username = streamReader.ReadLine();
                streamReader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка при открытии файла настроек\n\n"+ex.Message,"Ошибка настроек",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
