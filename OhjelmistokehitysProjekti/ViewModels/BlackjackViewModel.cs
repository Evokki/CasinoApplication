using GambleAssetsLibrary;
using OhjelmistokehitysProjekti.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OhjelmistokehitysProjekti.ViewModels
{
    public class BlackjackViewModel : BaseGameViewModel
    {
        private Blackjack _currentGame;
        private BlackjackGameStatus _currentGameStatus;
        public ICommand HitCommand { get; set; }
        public ICommand StandCommand { get; set; }

        private int _UserHandValue;
        public int UserHandValue
        {
            get { return _UserHandValue; }
            set { _UserHandValue = value; OnPropertyChanged("UserHandValue"); }
        }
        private int _HouseHandValue;
        public int HouseHandValue
        {
            get { return _HouseHandValue; }
            set { _HouseHandValue = value; OnPropertyChanged("HouseHandValue"); }
        }

        public BlackjackViewModel(Game game) : base(game)
        {
            _currentGame = (Blackjack)game;
            HitCommand = new RelayCommand(HandleHitCommand, CanExecute);
            StandCommand = new RelayCommand(HandleStandCommand, CanExecute);
        }

        private void HandleHitCommand(object obj)
        {
            Console.WriteLine("hit cmd");
            _currentGame.HandleHitOrStand(true);
        }
        private void HandleStandCommand(object obj)
        {
            Console.WriteLine("stand cmd");
            _currentGame.HandleHitOrStand(false);
        }

        public override void HandleGameStatus(GameCallback res)
        {
            base.GameStatus = (GameStatus)res;
            _currentGameStatus = (BlackjackGameStatus)res;
            HouseHandValue = GambleExtensionMethods.BJHandValues(_currentGameStatus.HouseCards);
            UserHandValue = GambleExtensionMethods.BJHandValues(_currentGameStatus.UserCards);
            ChangeGameState(1);
        }

        public override void CloseWindow(object obj)
        {
            base.CloseWindow(obj);
        }
    }
}
