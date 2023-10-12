using GambleAssetsLibrary;
using OhjelmistokehitysProjekti.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OhjelmistokehitysProjekti.ViewModels
{
    public class PokerViewModel : BaseGameViewModel
    {
        public Poker CurrentGame;
        private PokerGameStatus _CurrentStatus;
        public PokerGameStatus CurrentStatus
        {
            get { return _CurrentStatus; }
            set { _CurrentStatus = value; OnPropertyChanged("CurrentStatus"); }
        }
        public ICommand SelectOneCommand { get; set; }
        public ICommand SelectTwoCommand { get; set; }

        public PokerViewModel(Game game) : base(game)
        {
            CurrentGame = (Poker)game;
            SelectOneCommand = new RelayCommand(SelectStackOne, CanExecute);
            SelectTwoCommand = new RelayCommand(SelectStackTwo, CanExecute);
        }
        public void SelectStackOne(object obj)
        {
            if(CurrentStatus != null)
                if (CurrentStatus.isDouble)
                {
                    CurrentGame.OnDoubleSelected("Clubs Spades");
                }
                else
                {
                    CurrentGame.OnSelected(0);
                }
            else
            {
                CurrentGame.OnSelected(0);
            }
        }

        public void SelectStackTwo(object obj)
        {
            if (CurrentStatus != null)
                if (CurrentStatus.isDouble)
                {
                    CurrentGame.OnDoubleSelected("Hearts Diamonds");
                }
                else
                {
                    CurrentGame.OnSelected(1);
                }
            else
            {
                CurrentGame.OnSelected(1);
            }
        }

        public override void HandleGameStatus(GameCallback result)
        {
            CurrentStatus = (PokerGameStatus)result;
            OnPropertyChanged("Card1");
            base.HandleGameStatus(result);
        }
        public override void HandleGameResult(GameCallback res)
        {
            base.HandleGameResult(res);
        }
    }
}
