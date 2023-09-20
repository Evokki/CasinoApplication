using OhjelmistokehitysProjekti.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GambleAssetsLibrary;
using System.Collections.ObjectModel;

namespace OhjelmistokehitysProjekti.ViewModels
{
    public abstract class BaseGameViewModel : BaseViewModel
    {
        public Game currentGame;
        public bool AllowBetChange = true;
        public int betIndex = 0;
        public double[] bets = {0.20, 0.40 , 0.60, 0.80, 1.00};
        ICommand BetUpCommand { get; set; }
        ICommand BetDownCommand { get; set; }
        ICommand DoubleCommand { get; set; }
        ICommand RollCommand { get; set; }
        public BaseGameViewModel(Game game)
        {
            BetUpCommand = new RelayCommand(IncreaseBet, CanChangeBet);
            BetDownCommand = new RelayCommand(DecreaseBet, CanChangeBet);
            DoubleCommand = new RelayCommand(DoubleOrNothing, CanExecute);
            RollCommand = new RelayCommand(RollGame, CanExecute);

            currentGame = game;
        }

        public virtual void IncreaseBet(object obj)
        {
            if(betIndex < bets.Length - 1) {
                betIndex++;
            }
        }
        public virtual void DecreaseBet(object obj)
        {
            if (betIndex > 0)
            {
                betIndex--;
            }
        }
        public double GetCurrentBet()
        {
            return bets[betIndex];
        }
        public virtual void DoubleOrNothing(object obj)
        {
            //Call games doubling system
        }

        public virtual void RollGame(object obj)
        {
            currentGame.Roll();
        }
        public virtual bool CanChangeBet(object obj)
        {
            return AllowBetChange;
        }

        public override bool CanExecute(object obj)
        {
            return true;
        }
    }
}
