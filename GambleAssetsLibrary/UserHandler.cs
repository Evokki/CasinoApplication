using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GambleAssetsLibrary
{
    public class UserHandler: INotifyPropertyChanged
    {
        private static List<User> _Users;

        public event PropertyChangedEventHandler? PropertyChanged;

        private static User? currentUser { get; set; }
        public User CurrentUser
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
            JsonDatabaseHandler.SaveJsonData(u);
        }

        public static void InitUserList()
        {
            string jsonUsers = JsonDatabaseHandler.LoadJsonData();
            if(!string.IsNullOrEmpty(jsonUsers))
            {
                _Users = JsonDatabaseHandler.ConvertJsonToObject(jsonUsers);
            }
        }

        public static bool LoginUser(string username, string password)
        {
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
        public static void SetCurrentUser(User u)
        {
            currentUser = u;
        }
        public static User GetUser()
        {
            return currentUser;
        }
    }
}
