using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GambleAssetsLibrary
{
    public class Blackjack : Game
    {
        private Deck deck;
        private List<Card> houseHand = new List<Card>();
        private List<Card> userHand = new List<Card>();
        public Blackjack(string s = "Blackjack") : base(s)
        {
            deck = new Deck();
        }

        public override void StartGame()
        {
            Console.WriteLine("Blackjack start");
        }
        public override void EndGame()
        {
            Console.WriteLine("Blackjack end");
        }

        public override void Play()
        {
            InitHands();
        }

        public void HandleHitOrStand(bool state)
        {
            int userAmount = CalculateHand(userHand);
            if (state) //PLayer wants a new card
            {
                userHand.Add(deck.GetCard());
                if (userAmount < 21)
                {
                    //PLayloop continues
                }
                else
                {
                    HandleGameResults();
                }
            }
            else //Player stands. House turn
            {
                HouseTurn(userAmount);
            }
        }
        private void HouseTurn(int userAmount) //House logic. Called after user clicks stand
        {
            int houseAmount = CalculateHand(houseHand);

            if (userAmount > houseAmount) //User has higher hand than house. House draws a card.
            {
                houseHand.Add(deck.GetCard());
                houseAmount = CalculateHand(houseHand);

                if (houseAmount < 21) //House Didnt go over 21. Checking again
                {
                    HouseTurn(userAmount);
                }
                else if(houseAmount == 21) //House has a highest possible hand. House wins
                {
                    HandleGameResults();
                }
                else //House went over 21. User wins
                {
                    HandleGameResults();
                }
            }
            else //House has a higher or equal hand. House wins
            {
                HandleGameResults();
            }
        }

        private int CalculateHand(List<Card> hand) //Calculates the hand size
        {
            int i = 0;
            foreach(Card card in hand)
            {
                i += card.GetValue();
            }
            return i;
        }

        private void InitHands() //Creates hands of 2 cards for house and user
        {
            for(int i = 0; i < 2; i++)
            {
                houseHand.Add(deck.GetCard());
                userHand.Add(deck.GetCard());
            }
            RaiseGameLogicEndedEvent(null);
        }

        public override void HandleGameResults()
        {
            decimal bet = 0.2m;
            decimal win = 1.0m;

            GameResult res = new GameResult(GetName(), win, bet);
            RaiseGameResultsEvent(res);
        }
    }
}
