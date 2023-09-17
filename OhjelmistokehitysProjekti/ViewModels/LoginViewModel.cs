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
    class LoginViewModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand PlayAsGuestCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginUser, CanExecute);
            PlayAsGuestCommand = new RelayCommand(PlayAsGuest, CanExecute);
        }

        private void LoginUser(object obj)
        {
            string _Username = "";
            string _Password = "";

            User U = UserHandler.CreateNewUser(_Username, _Password);
            UserHandler.SetCurrentUser(U);
        }

        private void PlayAsGuest(object obj)
        {
            User U = UserHandler.CreateNewUser("Guest User", "1234");
            UserHandler.SetCurrentUser(U);
        }
        private bool CanExecute(object obj)
        {
            return true;
        }
    }
}
