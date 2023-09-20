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
    public class MainViewModel: BaseViewModel
    {
        public ICommand StartGameCommand { get; set; }
        private BlackJackView? _GameWindow = null;

        public MainViewModel() {
            StartGameCommand = new RelayCommand(StartGame, CanExecute);
        }

        private void StartGame(object obj)
        {
            if(_GameWindow == null)
            {
                _GameWindow = new BlackJackView();
                _GameWindow.Show();
            }
            else
            {
                _GameWindow.Focus();
            }
        }

        public override void CloseWindow(object obj)
        {
            //JsonDatabaseHandler.SaveJsonData(UserHandler.GetUser());

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
    }
}
