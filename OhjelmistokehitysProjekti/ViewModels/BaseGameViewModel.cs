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
        private GameStatus _GameStatus;
        public GameStatus gameStatus
        {
            get { return _GameStatus; }
            set { _GameStatus = value; OnPropertyChanged("GameStatus"); }
        }
        private GameResult _GameResult;
        public GameResult gameResult
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

        private decimal[] _Bets = { 0.20m, 0.40m, 0.60m, 0.80m, 1.00m , 2.00m, 3.00m, 4.00m, 5.00m , 7.50m, 10.00m, 20.00m, 50.00m, 100.00m};
        private int _BetIndex = 0;
        public int BetIndex
        {
            get{ return _BetIndex; }
            set{  _BetIndex = value; OnPropertyChanged("BetIndex"); }
        }
        public decimal CurrentBet
        {
            get { return _Bets[BetIndex]; }
        }

        public ICommand BetUpCommand { get; set; }
        public ICommand BetDownCommand { get; set; }
        public ICommand DoubleCommand { get; set; }
        public ICommand CashOutCommand { get; set; }
        public ICommand PlayCommand { get; set; }

        public override event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            if(PropertyName == "BetIndex")
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentBet"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        //Constructor. Assings values to all ICommands and sets up Game model.
        public BaseGameViewModel(Game game) : base()
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
        public virtual void IncreaseBet(object obj)
        {
            if (BetIndex < _Bets.Length - 1) {
                BetIndex += 1;
                //AudioManager.PlayAudio(AudioManager.ClickSoundPath);
            }
        }
        // Called from BetDownCommand
        public virtual void DecreaseBet(object obj)
        {
            if (BetIndex > 0)
            {
                BetIndex -= 1;
                //AudioManager.PlayAudio(AudioManager.ClickSoundPath);
            }
        }
        public decimal GetCurrentBet() => CurrentBet;
        #endregion

        #region GameResult Functionality
        // Called from DoubleCommand
        // Calls Game.DoubleOrNothing doubling logic and returns a new result. Changes game state to 0 = (InputState) if no win.
        private void DoubleOrNothing(object obj)
        {
            currentGame.DoubleOrNothing(_GameResult);
            if(gameResult.WinAmount == 0) {
                ChangeGameState(0);
            }
        }

        // Called from CashOutCommand
        // Adds winning from GameResult. Changes game state to 0 = (InputState).
        private void CashOut(object obj)
        {
            MainWindow._UserViewModel.ChangeUserMoney(true, gameResult.WinAmount);
            OnPropertyChanged("user");
            ChangeGameState(0);
        }
        #endregion

        //Called from PlayCommand
        // Initiates gamelogic in currentGame<Game>. After game logic an event Game.OnGameStatus(GameCallbackObjects.GameStatus result) is called.
        private void PlayGame(object obj)
        {
            if (UserHandler.GetUser().IsThereEnoughBalance(CurrentBet))
            {
                MainWindow._UserViewModel.ChangeUserMoney(false, CurrentBet);
                currentGame.Play(CurrentBet);
            }
            else
            {
                MessageBox.Show("User Account doesnt have enough balance");
            }
        }
        //GameStatus Listener. Listens to OnGameStatus event from class Game. Event returns a GameStatus object = Contains data about game state (Cards)
        public virtual void HandleGameStatus(GameCallback res)
        {
            gameStatus = (GameStatus) res;
            ChangeGameState(1);
        }
        //GameResult Listener. Listens to OnGameResult event from class Game. Event returns a GameResult object = Contains data about winnings
        public virtual void HandleGameResult(GameCallback res)
        {
            gameResult = (GameResult) res;
            if (gameResult.userWon)
            {
                //AudioManager.PlayAudio(AudioManager.WinSoundPath);
                MainViewModel.NotifyUser($"You won {gameResult.WinAmount}!");
                ChangeGameState(2);
            }
            else
            {
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