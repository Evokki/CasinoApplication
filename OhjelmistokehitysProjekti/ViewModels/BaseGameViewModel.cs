using OhjelmistokehitysProjekti.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GambleAssetsLibrary;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;

namespace OhjelmistokehitysProjekti.ViewModels
{
    public abstract class BaseGameViewModel : BaseViewModel
    {
        public Game? currentGame;
        public GameResult latestGameResult;
        private int _GameState = 0;
        public int GameState
        {
            get
            {
                return _GameState;
            }
            set
            {
                _GameState = value;
                OnPropertyChanged("GameState");
            }
        }

        private bool _AllowBetChange = true;
        public bool AllowBetChange{
            get{
                return _AllowBetChange;
            }
            set{
                _AllowBetChange = value;
                OnPropertyChanged("AllowBetChange");
            }
        }

        private bool _AllowUserInput = true;
        public bool AllowUserInput
        {
            get
            {
                return _AllowUserInput;
            }
            set
            {
                _AllowUserInput = value;
                OnPropertyChanged("AllowUserInput");
            }
        }

        private int _BetIndex = 0;
        public int BetIndex{
            get{
                return _BetIndex;
            }
            set{
                _BetIndex += value;
                OnPropertyChanged("BetIndex");
                }
            }

        public decimal[] bets = { 0,20m, 0.40m, 0.60m, 0.80m, 1.00m };
        public ICommand BetUpCommand { get; set; }
        public ICommand BetDownCommand { get; set; }
        public ICommand DoubleCommand { get; set; }
        public ICommand CashOutCommand { get; set; }
        public ICommand PlayCommand { get; set; }

        public override event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            Console.WriteLine("Prop changed " + PropertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public BaseGameViewModel(Game game)
        {
            BetUpCommand = new RelayCommand(IncreaseBet, CanChangeBet);
            BetDownCommand = new RelayCommand(DecreaseBet, CanChangeBet);
            PlayCommand = new RelayCommand(PlayGame, CanExecute);
            DoubleCommand = new RelayCommand(DoubleOrNothing, CanExecute);
            CashOutCommand = new RelayCommand(CashOut, CanExecute);

            currentGame = game;
            currentGame.OnGameLogicEnded += HandleGameLogicEnded;
            currentGame.OnGameResult += HandleGameResult;
        }

        private void IncreaseBet(object obj)
        {
            if (BetIndex < bets.Length - 1) {
                BetIndex += 1;
            }
            Console.WriteLine("Bet +. Current bet " + bets[BetIndex]);
        }
        private void DecreaseBet(object obj)
        {
            if (BetIndex > 0)
            {
                BetIndex -= 1;
            }
            Console.WriteLine("Bet -. Current bet " + bets[BetIndex] );
        }
        public decimal GetCurrentBet() => bets[BetIndex];

        private void DoubleOrNothing(object obj)
        {
            latestGameResult = currentGame.DoubleOrNothing(latestGameResult);
            if(latestGameResult.WinAmount == 0) {
                GameState = 0;
            }
        }

        private void CashOut(object obj)
        {
            User u = UserHandler.GetUser();
            u.IncreaseBalance(latestGameResult.WinAmount);
            GameState = 0;
        }

        private void PlayGame(object obj)
        {
            User u = UserHandler.GetUser();
            if (u.IsThereEnoughBalance(bets[BetIndex]))
            {
                u.DecreaseBalance(bets[BetIndex]);
                currentGame.Play();
            }
            else
            {
                MessageBox.Show("User Account doesnt have enough balance");
            }
        }
        private void HandleGameLogicEnded(GameResult res)
        {
            GameState = 1;
        }
        private void HandleGameResult(GameResult res)
        {
            latestGameResult = res;
            GameState = 2;
        }

        public virtual bool CanChangeBet(object obj)
        {
            return _AllowBetChange;
        }

        public override bool CanExecute(object obj)
        {
            return _AllowUserInput;
        }
    }
}