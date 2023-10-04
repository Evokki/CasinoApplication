using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Navigation;
using GambleAssetsLibrary;
using OhjelmistokehitysProjekti.Commands;
using OhjelmistokehitysProjekti.ViewModels;
using OhjelmistokehitysProjekti.Views;

namespace OhjelmistokehitysProjekti
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public string LocalTime
        {
            get { return DateTime.Now.ToString("HH:mm"); }
        }
        private User _User;
        public User? user
        {
            get { return _User; }
            set { _User = value; OnPropertyChanged("user"); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand DepositCommand { get; set; }
        public ICommand WithdrawCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand Refresh { get; set; }

        public UserViewModel()
        {
            DepositCommand = new RelayCommand(Deposit, canExecute);
            WithdrawCommand = new RelayCommand(Withdraw, canExecute);
            LogOutCommand = new RelayCommand(Logout, canExecute);
            Refresh = new RelayCommand(ValidateUser, canExecute);

            ValidateUser(null);
            _ = UpdateTime();
        }
        public void ValidateUser(object parameter)
        {
            user = UserHandler.GetUser();
            if (user == null)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                user = UserHandler.GetUser();
            }
        }
        public void Deposit(object parameter)
        {
            decimal amount = 10;
            UserHandler.GetUser().IncreaseBalance(amount);
        }
        public void Withdraw(object parameter)
        {
            AShowPopup(parameter);
            decimal amount = 10;
            UserHandler.GetUser().DecreaseBalance(amount);
        }

        public void AShowPopup(object obj)
        {
            Popup popup = new Popup();
            Border b = new Border();
            b.BorderThickness = new System.Windows.Thickness(3);
            TextBox t = new TextBox();
            b.Child = t;
            popup.Child = b;
            
            popup.IsOpen = true;
        }
        public void Logout(object parameter)
        {
            UserHandler.LogOutUser();
            ValidateUser(null);
        }
        public bool canExecute(object obj)
        {
            return true;
        }
        private async Task UpdateTime()
        {
            while (true)
            {
                await Task.Delay(60000);
                OnPropertyChanged("LocalTime");
            }
        }
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
