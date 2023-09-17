using GambleAssetsLibrary;
using OhjelmistokehitysProjekti.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OhjelmistokehitysProjekti.ViewModels
{
    public class MainViewModel
    {
        public ICommand StartGameCommand { get; set; }
        public ICommand ExitAppCommand { get; set; }
        private GameWindow? _GameWindow = null;

        public MainViewModel() {
            StartGameCommand = new RelayCommand(StartGame, CanShowWindow);
            ExitAppCommand = new RelayCommand(ExitApplication, CanShowWindow);
        }

        private void StartGame(object obj)
        {
            if(_GameWindow == null)
            {
                _GameWindow = new GameWindow();
                _GameWindow.Show();
            }
            else
            {
                _GameWindow.Focus();
            }
        }

        public void ExitApplication(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Exit Application?",
                "Notification", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }

        public static void NotifyUser(string msg)
        {
            MessageBox.Show(msg,
                "Notification", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }
    }
}
