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
            CurrentGame.OnSelected(0);
        }

        public void SelectStackTwo(object obj)
        {
            CurrentGame.OnSelected(1);
        }

        public override void HandleGameStatus(GameCallback result)
        {
            CurrentStatus = (PokerGameStatus)result;
            base.HandleGameStatus(result);
        }
        public override void HandleGameResult(GameCallback res)
        {
            base.HandleGameResult(res);
        }
    }
}
