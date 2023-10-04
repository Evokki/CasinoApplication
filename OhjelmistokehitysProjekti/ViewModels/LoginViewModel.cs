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
        public string? Username { get; set; }
        public string? Password { get; set; }
        private bool _LoginOrSignup { get; set; }
        public bool LoginOrSignup
        {
            get{ return _LoginOrSignup; }
            set{ _LoginOrSignup = value; OnPropertyChanged("LoginOrSignup"); Console.WriteLine("Changed login or sign up to " + _LoginOrSignup); }
        }
        public string LoginMode 
        {
            get
            {
                Console.WriteLine("Get L of S = " + _LoginOrSignup);  if (LoginOrSignup) { return "Login"; } else return "Create User";
                }
        }
        public void OnPropertyChanged(string PropertyName = null)
        {
            if(PropertyName == "LoginOrSignup")
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LoginMode"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginUser, CanExecute);
            PlayAsGuestCommand = new RelayCommand(PlayAsGuest, CanExecute);
        }

        private void LoginUser(object obj)
        {
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MainViewModel.NotifyUser("Username or password is empty!");
            }
            if (!LoginOrSignup)
            {
                if(UserHandler.LoginUser(Username, Password) == false)
                {
                    MainViewModel.NotifyUser("Wrong username or password!");
                    return;
                }
                
                CloseWindow(null);
            }
            else
            {
                UserHandler.CreateNewUser(Username, Password);
                CloseWindow(null);
            }
        }

        private void PlayAsGuest(object obj)
        {
            UserHandler.CreateNewUser("Guest", "1234");
            UserHandler.GetUser().IncreaseBalance(100);
            CloseWindow(null);
        }
        public override void CloseWindow(object obj)
        {
            if(obj != null)
            {
                MessageBoxResult result = MessageBox.Show("Exit Application?",
                    "Notification", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    App.Current.Shutdown();
                }
            }
            base.CloseWindow(obj);
        }
    }
}
