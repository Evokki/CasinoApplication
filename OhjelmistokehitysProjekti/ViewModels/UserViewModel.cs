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
        public User? user
        {
            get { return UserHandler.GetUser(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand DepositCommand { get; set; }
        public ICommand WithdrawCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public event Action OnDeposit;
        public event Action OnWithdraw;
        public UserViewModel()
        {
            DepositCommand = new RelayCommand(Deposit, canExecute);
            WithdrawCommand = new RelayCommand(Withdraw, canExecute);
            LogOutCommand = new RelayCommand(Logout, canExecute);
            //RefreshCommand = new RelayCommand(ValidateUser, canExecute);

            ValidateUser();
            _ = UpdateTime();
        }

        public void ValidateUser()
        {
            if (user == null)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
            OnPropertyChanged("user");
        }
        public void Deposit(object parameter)
        {
            OnDeposit?.Invoke();
        }
        public void Withdraw(object parameter)
        {
            OnWithdraw?.Invoke();
        }
        public void ChangeUserMoney(bool Add, decimal amount)
        {
            if (Add)
            {
                user.IncreaseBalance(amount);

            }
            else {
                user.DecreaseBalance(amount);
            }
            OnPropertyChanged("user");
        }

        public void ShowPopup(object obj)
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
            OnPropertyChanged("user");
            ValidateUser();
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
