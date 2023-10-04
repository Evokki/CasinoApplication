using GambleAssetsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhjelmistokehitysProjekti.ViewModels
{
    internal class SlotsViewModel : BaseGameViewModel
    {
        public Slots CurrentGame;
        public SlotsGameStatus CurrentStatus;
        private int _Num1;
        public int Num1
        {
            get { return _Num1; }
            set { _Num1 = value; OnPropertyChanged("Num1"); }
        }
        private int _Num2;
        public int Num2
        {
            get { return _Num2; }
            set { _Num2 = value; OnPropertyChanged("Num2"); }
        }
        private int _Num3;
        public int Num3
        {
            get { return _Num3; }
            set { _Num3 = value; OnPropertyChanged("Num3"); }
        }
        
        public SlotsViewModel(Game game) : base(game)
        {
            CurrentGame = (Slots)game;
        }

        public override void HandleGameStatus(GameCallback res)
        {
            base.HandleGameStatus(res);
            CurrentStatus = (SlotsGameStatus)res;
            Num1 = CurrentStatus.Numbers[0];
            Num2 = CurrentStatus.Numbers[1];
            Num3 = CurrentStatus.Numbers[2];
            ChangeGameState(2);
        }
    }
}
