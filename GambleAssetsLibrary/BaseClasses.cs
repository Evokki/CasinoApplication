using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Navigation;
namespace GambleAssetsLibrary
{
    public class User: INotifyPropertyChanged
    {
        private string _Username;
        public string Username
        {
            get { return _Username; }
            set { _Username = value; OnPropertyChanged("Username"); }
        }

        private void OnPropertyChanged([CallerMemberName] string PropertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string _Password;
        private decimal _AccountBalance;
        public decimal AccountBalance
        {
            get { return _AccountBalance; }
            set { _AccountBalance = value; OnPropertyChanged("AccountBalance"); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public User(string name, string pass)
        {
            this._Username = name;
            this._Password = pass;
        }
        public void IncreaseBalance(decimal amount)
        {
            this._AccountBalance += amount;
        }
        public void DecreaseBalance(decimal amount)
        {
            if(amount <= this._AccountBalance)
            {
                this._AccountBalance -= amount;
            }
            else
            {

            }
        }
        public bool IsThereEnoughBalance(decimal amount)
        {
            if(_AccountBalance >= amount)
            {
                return true;
            }else { return false; }
        }
        public bool Login(string username, string password)
        {
            return this._Username == username && this._Password == password;
        }
        public override string ToString()
        {
            return _Username;
        }
    }

    public abstract class Game
    {
        private string DisplayName = "";
        public delegate void GameLogicCallback(GameCallback result);
        public GameLogicCallback OnGameStatus;
        public GameLogicCallback OnGameResult;
        public Game(string S)
        {
            DisplayName = S;
        }
        public string GetName() => this.DisplayName;
        public abstract void StartGame(); //Called when opening a game for the first time
        public abstract void Play(); //Called after player wants to start the game logic. 
        public virtual void RaiseGameLogicEndedEvent(GameStatus StatusResult)
        {
            OnGameStatus?.Invoke(StatusResult);
        }
        public abstract void EndGame();
        public abstract void HandleGameResults(bool userWon = false); //Call this after the gamelogic to handle if user won or lost. Create Gameresult object
        public virtual void RaiseGameResultsEvent(GameResult Result) //Call this at the end of HandleGameResults.
        {
            OnGameResult?.Invoke(Result);
        }
        public virtual GameResult DoubleOrNothing(GameResult result) //Called after HandlePayout if user clicks double
        {
            result.WinAmount *= 2;
            return result;
        }
    }

    public class Card: INotifyPropertyChanged
    {
        const string Clubs = "Clubs";
        const string Hearts = "Hearts";
        const string Diamonds = "Diamonds";
        const string Spades = "Spades";
        public static string[] _Suits = { Spades, Hearts, Clubs, Diamonds };

        private string _Suit;
        public string Suit
        {
            get { return _Suit; }
            set { _Suit = value; OnPropertyChanged("Suit"); }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { _Value = value; OnPropertyChanged("Value"); }
        }
        public string CardSuitPath
        {
            get{ return GambleExtensionMethods.CardSuitPath(Suit); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            Console.WriteLine(CardSuitPath);
            if(PropertyName == "Suit")
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CardSuitPath"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public Card(int value, string house)
        {
            if(value == 1)
            {
                this._Name = "A";
            }
            else if (value == 11)
            {
                this._Name = "J";
            }
            else if (value == 12)
            {
                this._Name = "Q";    
            }
            else if (value == 13)
            {
                this._Name = "K";
            }
            else
            {
                this._Name = value.ToString();
            }
            
            this._Value = value;
            this._Suit = house;
        }
    }

    public class Deck
    {
        private List<Card> cards = new List<Card>();
        private Random rng = new Random();
        public Deck()
        {
            CreateFullDéck();
        }
        private void CreateFullDéck()
        {
            for(int i = 0; i < 4; i++) {
                string house = Card._Suits[i];
                for(int j = 0; j < 14; j++)
                {
                    cards.Add(new Card(j+1, house));
                }
            }
        }
        public Card GetCard()
        {
            int i = rng.Next(cards.Count);
            Card card = cards.ElementAt(i);
            cards.RemoveAt(i);
            return card;
        }
    }
}
