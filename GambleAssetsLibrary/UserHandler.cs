using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambleAssetsLibrary
{
    public class UserHandler
    {
        private static List<User> _Users = new List<User>();
        private static User? currentUser { get; set; }
        public static User CreateNewUser(string username, string password)
        {
            return new User(username, password);
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
