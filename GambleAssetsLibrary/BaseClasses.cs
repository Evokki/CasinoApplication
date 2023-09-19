using System;
using System.Collections.Generic;

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
        public string DisplayName = "";
        public virtual void Setup(string S)
        {
            DisplayName = S;
        }
        public abstract void StartGame();
        public abstract void EndGame();
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

        public CardHouse House;
        public string Name;
        public int Value;

        public Card(int value, CardHouse house)
        {
            if(value == 1)
            {
                this.Name = "A";
            }
            else if (value == 11)
            {
                this.Name = "J";
            }
            else if (value == 12)
            {
                this.Name = "Q";    
            }
            else if (value == 13)
            {
                this.Name = "K";
            }
            else
            {
                this.Name = value.ToString();
            }
            
            this.Value = value;
            this.House = house;
        }
    }

    public class Deck
    {
        private List<Card> cards = new List<Card>();
        public Deck()
        {
            for(int i = 0; i < 4; i++) {
                Card.CardHouse house = (Card.CardHouse)i;
                for(int j = 0; j < 14; j++)
                {
                    cards.Add(new Card(i+1, (Card.CardHouse)i));
                }
            }
        }
    }
}
