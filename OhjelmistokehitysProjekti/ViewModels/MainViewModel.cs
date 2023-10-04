using GambleAssetsLibrary;
using OhjelmistokehitysProjekti.Commands;

using System.Windows;
using System.Windows.Input;

namespace OhjelmistokehitysProjekti.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        public MainViewModel():base() {

        }

        public override void CloseWindow(object obj)
        {
            UserHandler.SaveUserData();

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
