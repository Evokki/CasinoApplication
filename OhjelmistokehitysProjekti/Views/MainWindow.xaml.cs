using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GambleAssetsLibrary;
using OhjelmistokehitysProjekti.ViewModels;
using OhjelmistokehitysProjekti.Views;

namespace OhjelmistokehitysProjekti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel;

            Loaded += ValidateCurrentUser;
        }
        public void StartBlackjack(object sender, RoutedEventArgs e)
        {
            BlackJackView bjVIew = new BlackJackView();
            bjVIew.Show();
        }
        public void StartSlots(object sender, RoutedEventArgs e)
        {
            SlotsView slotsView = new SlotsView();
            slotsView.Show();
        }
        private void ValidateCurrentUser(object obj, RoutedEventArgs e)
        {
            if(UserHandler.GetUser() == null)
            {
                this.IsEnabled = false;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                this.IsEnabled = true;
            }
        }
    }
}
