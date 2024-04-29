using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

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
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), FILENAME), Username);
        }
        private void LoadUsernameFromFile()
        {
            Username = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), FILENAME));
        }
    }
}
