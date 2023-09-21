using OhjelmistokehitysProjekti.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GambleAssetsLibrary;
using System.ComponentModel;

namespace OhjelmistokehitysProjekti.ViewModels
{
    class LoginViewModel: BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { get; set; }
        public ICommand PlayAsGuestCommand { get; set; }
        public string? _Username { get; set; }
        public string? _Password { get; set; }
        private bool _LoginOrSignup { get; set; }
        public bool LoginOrSignup
        {
            get
            {
                return _LoginOrSignup
            }
            set
            {
                _LoginOrSignup = value;
                OnPropertyChanged();
            }
        }
        public string _LoginMode 
        {
            get
            {
                return _LoginOrSignup ? "Create new User" : "Login";
            }
        }
        public void OnPropertyChanged(string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
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
                if(UserHandler.LoginUser(_Username, _Password) == false)
                {
                    MainViewModel.NotifyUser("Wrong username or password!");
                    return;
                }
                
                CloseWindow(null);
            }
        }

        private void PlayAsGuest(object obj)
        {
            UserHandler.SetCurrentUser(new User("Guest", "0000"));
            MainViewModel.NotifyUser("User " + _Username + " has been logged in"); //Testi ilmoitus
            CloseWindow(null);
        }
    }
}
