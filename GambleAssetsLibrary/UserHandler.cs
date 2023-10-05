using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GambleAssetsLibrary
{
    public class UserHandler: INotifyPropertyChanged
    {
        private static List<User> _Users;

        public event PropertyChangedEventHandler? PropertyChanged;

        private static User? currentUser { get; set; }
        public User? CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged("CurrentUser"); }
        }
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public static void CreateNewUser(string username, string password)
        {
            User u = new User(username, password);
            if(!_Users.Contains(u))
                _Users.Add(u);
            JsonDatabaseHandler.SaveJsonData(_Users);
            MessageBox.Show("Created a new user.");
            LoginUser(username, password);
        }

        public static void InitUserList()
        {
            string jsonUsers = JsonDatabaseHandler.LoadJsonData();
            if(!string.IsNullOrEmpty(jsonUsers))
            {
                _Users = JsonDatabaseHandler.ConvertJsonToObject(jsonUsers);
            }
            else
            {
                _Users = new List<User>();
            }
        }

        public static bool LoginUser(string username, string password)
        {
            if(username == "Guest" || password == "1234")
            {
                SetCurrentUser(new User(username, password));
                return true;
            }
            if(_Users == null)
            {
                return false;
            }

            foreach(var user in _Users)
            {
                if(user.ToString() == username)
                {
                    if(user.Login(username, password))
                    {
                        SetCurrentUser(user);
                        return true;
                    }
                }
            }
            return false;
        }
        public static void LogOutUser()
        {
            if (currentUser != null)
            {
                string msg = currentUser.Username + " has been logged out.";
                MessageBox.Show(msg);
                SaveUserData();
                SetCurrentUser(null);
            }
        }

        public static void SaveUserData()
        {
            JsonDatabaseHandler.SaveJsonData(_Users);
        }

        private static void SetCurrentUser(User? u)
        {
            if(currentUser == null)
            {
                currentUser = u;
                string msg = "Logged in as " + currentUser.Username;
                MessageBox.Show(msg);
            }else if(u == null)
            {
                currentUser = null;
            }
        }

        public static User? GetUser()
        {
            return currentUser;
        }
    }
}
