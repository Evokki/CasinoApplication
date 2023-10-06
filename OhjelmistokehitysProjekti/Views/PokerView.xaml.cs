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

namespace OhjelmistokehitysProjekti.Views
{
    /// <summary>
    /// Interaction logic for PokerView.xaml
    /// </summary>
    public partial class PokerView : Window, IPopUpHelper
    {
        public Poker PokerGame;
        public PokerViewModel ViewModel;
        private PopupHandler _PopupHandler;
        public PokerView()
        {
            InitializeComponent();
            PokerGame = new Poker("Poker");
            ViewModel = new PokerViewModel(PokerGame);

            _PopupHandler = new PopupHandler(this, UserBalancePopup, PopupTextBox, PopupTitle);
            MainWindow._UserViewModel.OnDeposit += _PopupHandler.SetDepositHandler;
            MainWindow._UserViewModel.OnWithdraw += _PopupHandler.SetWithdrawHandler;

            this.DataContext = ViewModel;

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
