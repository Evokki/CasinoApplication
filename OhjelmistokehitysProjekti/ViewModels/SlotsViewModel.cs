using GambleAssetsLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            set { _Num1 = value; OnPropertyChanged("Num1"); OnPropertyChanged("Num1Path"); }
        }
        private int _Num2;
        public int Num2
        {
            get { return _Num2; }
            set { _Num2 = value; OnPropertyChanged("Num2"); OnPropertyChanged("Num2Path"); }
        }
        private int _Num3;
        public int Num3
        {
            get { return _Num3; }
            set { _Num3 = value; OnPropertyChanged("Num3"); OnPropertyChanged("Num3Path"); }
        }
        public string Num1Path
        {
            get { return Num1.SlotIconPath(); }
        }
        public string Num2Path
        {
            get { return Num2.SlotIconPath(); }
        }
        public string Num3Path
        {
            get { return Num3.SlotIconPath(); }
        }
        public string Icons1Result
        {
            get { return "= " + GetCurrentBet() * 3 + " €"; }
        }
        public string Icons2Result
        {
            get { return "= " + GetCurrentBet() * 10 + " €"; }
        }
        public string Icons3Result
        {
            get { return "= " + GetCurrentBet() * 15 + " €"; }
        }
        public string Icons4Result
        {
            get { return "= " + GetCurrentBet() * 30 + " €"; }
        }
        public string Icons5Result
        {
            get { return "= " + GetCurrentBet() * 60 + " €"; }
        }
        public string Icons6Result
        {
            get { return "= " + GetCurrentBet() * 2 + " €"; }
        }
        public string Icons7Result
        {
            get { return "= " + GetCurrentBet() * 5 + " €"; }
        }

        public SlotsViewModel(Game game) : base(game)
        {
            CurrentGame = (Slots)game;
        }
        public override void IncreaseBet(object obj)
        {
            base.IncreaseBet(obj);
            RefreshBets();
        }
        public override void DecreaseBet(object obj)
        {
            base.DecreaseBet(obj);
            RefreshBets();
        }
        private void RefreshBets()
        {
            OnPropertyChanged("Icons1Result");
            OnPropertyChanged("Icons2Result");
            OnPropertyChanged("Icons3Result");
            OnPropertyChanged("Icons4Result");
            OnPropertyChanged("Icons5Result");
            OnPropertyChanged("Icons6Result");
            OnPropertyChanged("Icons7Result");
        }
        public override void HandleGameStatus(GameCallback res)
        {
            base.HandleGameStatus(res);
            CurrentStatus = (SlotsGameStatus)res;
            Num1 = CurrentStatus.Numbers[0];
            Num2 = CurrentStatus.Numbers[1];
            Num3 = CurrentStatus.Numbers[2];
            
            Console.WriteLine(Num1Path);
            ChangeGameState(2);
        }
    }
}
