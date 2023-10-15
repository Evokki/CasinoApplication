using OhjelmistokehitysProjekti.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GambleAssetsLibrary;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace OhjelmistokehitysProjekti.ViewModels
{
    class LoginViewModel: BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { get; set; }
        public ICommand PlayAsGuestCommand { get; set; }
        private string _Username { get; set; }
        public string Username
        {
            get { return _Username; }
            set { _Username = value; OnPropertyChanged("Username"); }
        }

        private string _Password { get; set; }
        public string? Password 
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged("Password"); }
        }

        private bool _LoginOrSignup { get; set; }
        public bool LoginOrSignup
        {
            get{ return _LoginOrSignup; }
            set{ _LoginOrSignup = value; OnPropertyChanged("LoginOrSignup"); }
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
        public void UpdateUsername(string s)
        {
            Username = s;
        }
        public void UpdatePassword(string s)
        {
            Password = s;
        }

        private void LoginUser(object obj)
        {
            if(string.IsNullOrEmpty(_Username) || string.IsNullOrEmpty(_Password))
            {
                MainViewModel.NotifyUser("Username or password is empty!");
            }
            if (!LoginOrSignup)
            {
                if(UserHandler.LoginUser(_Username, _Password) == false)
                {
                    MainViewModel.NotifyUser("Wrong username or password!");
                    return;
                }
                
                OnPropertyChanged("user");
                CloseWindow(null);
            }
            else
            {
                UserHandler.CreateNewUser(_Username, _Password);
                OnPropertyChanged("user");
                CloseWindow(null);
            }
        }

        private void PlayAsGuest(object obj)
        {
            UserHandler.CreateNewUser("Guest", "1234");
            UserHandler.GetUser().IncreaseBalance(100);
            OnPropertyChanged("user");
            CloseWindow(null);
        }
    }
}
