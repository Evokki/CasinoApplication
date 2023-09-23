﻿using OhjelmistokehitysProjekti.Commands;
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
        private GameStatus _GameStatus;
        public GameStatus GameStatus
        {
            get { return _GameStatus; }
            set { _GameStatus = value; OnPropertyChanged("GameStatus"); }
        }
        private GameResult _GameResult;
        public GameResult GameResult
        {
            get { return _GameResult; }
            set { _GameResult = value; OnPropertyChanged("GameResult"); }
        }
        private int _GameState = 0;
        public int GameState
        {
            get { return _GameState;}
            set{ _GameState = value; OnPropertyChanged("GameState"); }
        }

        private bool _AllowBetChange = true;
        public bool AllowBetChange{
            get{ return _AllowBetChange; }
            set{ _AllowBetChange = value; OnPropertyChanged("AllowBetChange"); }
        }

        private bool _AllowUserInput = true;
        public bool AllowUserInput
        {
            get { return _AllowUserInput; }
            set{ _AllowUserInput = value ;OnPropertyChanged("AllowUserInput"); }
        }

        private int _BetIndex = 0;
        public int BetIndex
        {
            get{ return _BetIndex; }
            set{  _BetIndex = value; OnPropertyChanged("BetIndex"); }
        }

        public decimal[] bets = { 0,20m, 0.40m, 0.60m, 0.80m, 1.00m };
        public ICommand BetUpCommand { get; set; }
        public ICommand BetDownCommand { get; set; }
        public ICommand DoubleCommand { get; set; }
        public ICommand CashOutCommand { get; set; }
        public ICommand PlayCommand { get; set; }

        public override event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            Console.WriteLine("Prop changed " + PropertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        //Constructor. Assings values to all ICommands and sets up Game model.
        public BaseGameViewModel(Game game)
        {
            BetUpCommand = new RelayCommand(IncreaseBet, CanChangeBet);
            BetDownCommand = new RelayCommand(DecreaseBet, CanChangeBet);
            PlayCommand = new RelayCommand(PlayGame, CanExecute);
            DoubleCommand = new RelayCommand(DoubleOrNothing, CanExecute);
            CashOutCommand = new RelayCommand(CashOut, CanExecute);

            currentGame = game;
            currentGame.OnGameStatus += HandleGameStatus;
            currentGame.OnGameResult += HandleGameResult;
        }

        /// <summary>
        //  States, limiting user view inputs
        //  0 = Initial Input state | BetUp, BetDown and Play Commands
        /// 1 = User game selection state | User selection commands, Changes per game
        /// 2 = Game result state | Double and cashout commands
        /// </summary>
        public void ChangeGameState(int newState) 
        {
            GameState = newState;
        }
        #region Betting Functionality
        // Called from BetUpCommand
        private void IncreaseBet(object obj)
        {
            if (BetIndex < bets.Length - 1) {
                BetIndex += 1;
            }
            else
            {
                BetIndex = bets.Length - 1;
            }
            Console.WriteLine("Bet +. Current bet " + bets[BetIndex]);
        }
        // Called from BetDownCommand
        private void DecreaseBet(object obj)
        {
            if (BetIndex > 0)
            {
                BetIndex -= 1;
            }
            Console.WriteLine("Bet -. Current bet " + bets[BetIndex] );
        }
        public decimal GetCurrentBet() => bets[BetIndex];
        #endregion

        #region GameResult Functionality
        // Called from DoubleCommand
        // Calls Game.DoubleOrNothing doubling logic and returns a new result. Changes game state to 0 = (InputState) if no win.
        private void DoubleOrNothing(object obj)
        {
            GameResult = currentGame.DoubleOrNothing(GameResult);
            if(GameResult.WinAmount == 0) {
                ChangeGameState(0);
            }
        }

        // Called from CashOutCommand
        // Adds winning from GameResult. Changes game state to 0 = (InputState).
        private void CashOut(object obj)
        {
            User u = UserHandler.GetUser();
            u.IncreaseBalance(GameResult.WinAmount);
            ChangeGameState(0);
        }
        #endregion

        //Called from PlayCommand
        // Initiates gamelogic in currentGame<Game>. After game logic an event Game.OnGameStatus(GameCallbackObjects.GameStatus result) is called.
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
        //GameStatus Listener. Listens to OnGameStatus event from class Game. Event returns a GameStatus object = Contains data about game state (Cards)
        public virtual void HandleGameStatus(GameCallback res)
        {
            GameStatus = (GameStatus) res;
            ChangeGameState(1);
        }
        //GameResult Listener. Listens to OnGameResult event from class Game. Event returns a GameResult object = Contains data about winnings
        public virtual void HandleGameResult(GameCallback res)
        {
            GameResult = (GameResult) res;
            if (GameResult.userWon)
            {
                MainViewModel.NotifyUser($"You won {GameResult.WinAmount}!");
                ChangeGameState(2);
            }
            else
            {
                MainViewModel.NotifyUser("House won!");
                ChangeGameState(0);
            }
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