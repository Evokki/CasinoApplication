using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Navigation;

namespace GambleAssetsLibrary
{
    public class User
    {
        private string _Username;
        private string _Password;
        private decimal _AccountBalance;
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
        public delegate void GameLogicCallback(GameResult? result);
        public GameLogicCallback OnGameLogicEnded;
        public GameLogicCallback OnGameResult;
        public Game(string S)
        {
            DisplayName = S;
        }
        public string GetName() => this.DisplayName;
        public abstract void StartGame(); //Called when opening a game for the first time
        public abstract void Play(); //Called after player wants to start the game logic. 
        public virtual void RaiseGameLogicEndedEvent(GameResult result)
        {
            OnGameLogicEnded?.Invoke(result);
        }
        public abstract void EndGame();
        public abstract void HandleGameResults(); //Call this after the gamelogic to handle if user won or lost. Create Gameresult object
        public virtual void RaiseGameResultsEvent(GameResult result) //Call this at the end of HandleGameResults.
        {
            OnGameResult?.Invoke(result);
        }
        public virtual GameResult DoubleOrNothing(GameResult result) //Called after HandlePayout if user clicks double
        {
            result.WinAmount *= 2;
            return result;
        }
    }

    public class GameResult
    {
        public string GameName;
        public decimal WinAmount;
        public decimal UsedBet;
        public GameResult(string s, decimal w, decimal b)
        {
            this.GameName = s;
            this.WinAmount = w;
            this.UsedBet = b;
        }
    }
    public class Card
    {
        public enum CardHouse
        {
            Heart = 0,
            Spade = 1,
            Diamond = 2,
            Club = 3
        };

        private CardHouse _House;
        private string _Name;
        private int _Value;

        public Card(int value, CardHouse house)
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
            this._House = house;
        }
        public string GetName() => this._Name;
        public int GetValue() => this._Value;
        public string GetHouse() => this._House.ToString();
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
                Card.CardHouse house = (Card.CardHouse)i;
                for(int j = 0; j < 14; j++)
                {
                    cards.Add(new Card(i+1, (Card.CardHouse)i));
                }
            }
        }
        public Card GetCard()
        {
            int i = rng.Next(cards.Count);
            Card card = cards[i];
            cards.RemoveAt(i);
            return card;
        }
    }
}
