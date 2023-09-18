using OhjelmistokehitysProjekti.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GambleAssetsLibrary;

namespace OhjelmistokehitysProjekti.ViewModels
{
    class LoginViewModel: BaseViewModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand PlayAsGuestCommand { get; set; }
        public string? _Username { get; set; }
        public string? _Password { get; set; }
        public bool _Remember { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginUser, CanExecute);
            PlayAsGuestCommand = new RelayCommand(PlayAsGuest, CanExecute);
        }

        private void LoginUser(object obj)
        {
            if(string.IsNullOrEmpty(_Username) || string.IsNullOrEmpty(_Password))
            {
                MainViewModel.NotifyUser("Username or password is empty!");
            }
            else
            {
                User U = UserHandler.CreateNewUser(_Username, _Password, _Remember);
                UserHandler.SetCurrentUser(U);
                MainViewModel.NotifyUser("User " + _Username + " has been logged in");
                CloseWindow(null);
            }
        }

        private void PlayAsGuest(object obj)
        {
            User U = UserHandler.CreateNewUser("Guest User", "1234", false);
            UserHandler.SetCurrentUser(U);
            MainViewModel.NotifyUser("User " + _Username + " has been logged in");
        }
    }
}
