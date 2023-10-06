using GambleAssetsLibrary;
using OhjelmistokehitysProjekti.ViewModels;
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
using System.Windows.Shapes;

namespace OhjelmistokehitysProjekti
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class BlackJackView : Window, IPopUpHelper
    {
        Blackjack bjGame;
        BlackjackViewModel viewModel;
        private PopupHandler _PopupHandler;
        public BlackJackView()
        {
            InitializeComponent();
            bjGame = new Blackjack();
            viewModel = new BlackjackViewModel(bjGame);
            this.DataContext = viewModel;

            _PopupHandler = new PopupHandler(this, UserBalancePopup, PopupTextBox, PopupTitle);
            MainWindow._UserViewModel.OnDeposit += _PopupHandler.SetDepositHandler;
            MainWindow._UserViewModel.OnWithdraw += _PopupHandler.SetWithdrawHandler;

            UserPanel.DataContext = MainWindow._UserViewModel;
        }

        public void OnDeposit(decimal amount)
        {
            MainWindow._UserViewModel.ChangeUserMoney(true, amount);
        }

        public void OnWithdraw(decimal amount)
        {
            MainWindow._UserViewModel.ChangeUserMoney(false, amount);
        }

        private void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    
    }
}
