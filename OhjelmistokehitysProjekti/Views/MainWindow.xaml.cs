using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public partial class MainWindow : Window, IPopUpHelper
    {
        public Window? gameView;
        public static UserViewModel _UserViewModel { get; set; }

        public PopupHandler popHandler;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            MainViewModel mainViewModel = new MainViewModel();
            GameButtonPanel.DataContext = mainViewModel;

            popHandler = new PopupHandler(this, UserBalancePopup, PopupTextBox, PopupTitle);
            if(_UserViewModel == null)
            {
                _UserViewModel = new UserViewModel();
                _UserViewModel.OnDeposit += popHandler.SetDepositHandler;
                _UserViewModel.OnWithdraw += popHandler.SetWithdrawHandler;
            }
            UserPanel.DataContext = _UserViewModel;

        }

        public void ClearGameView(object? sender, EventArgs e) {
            gameView = null;
        }

        public void StartBlackjack(object sender, RoutedEventArgs e)
        {
            if(gameView == null)
            {
                gameView = new BlackJackView();
                gameView.Closed += ClearGameView;
            }
            gameView.Show();
        }
        public void StartSlots(object sender, RoutedEventArgs e)
        {
            if (gameView == null)
            {
                gameView = new SlotsView();
                gameView.Closed += ClearGameView;
            }
            gameView.Show();
        }
        private void StartPoker(object sender, RoutedEventArgs e)
        {
            if (gameView == null)
            {
                gameView = new PokerView();
                gameView.Closed += ClearGameView;
            }
            gameView.Show();
        }

        public void OnDeposit(decimal amount)
        {
            Debug.WriteLine("On depo: " + this.Name);
            _UserViewModel.ChangeUserMoney(true, amount);
        }

        public void OnWithdraw(decimal amount)
        {
            _UserViewModel.ChangeUserMoney(false, amount);
        }
    }
}
