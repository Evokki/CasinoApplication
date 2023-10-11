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
        private int Limit = 21;
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
            houseHand.Clear();
            userHand.Clear();
            deck.ResetDeck();
        }

        public override void Play(decimal bet)
        {
            currentBet = bet;
            InitHands();
        }

        public void HandleHitOrStand(bool state)
        {
            int userAmount = CalculateHand(userHand);
            if (state) //Player wants a new card
            {
                userHand.Add(deck.GetCard());
                userAmount = CalculateHand(userHand);
                RaiseGameLogicEndedEvent(new BlackjackGameStatus(GetName(), houseHand, userHand));
                if (userAmount > Limit)
                {
                    HandleGameResults(false);
                }
                else if(userAmount == Limit)
                {
                    HouseTurn(userAmount);
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
                RaiseGameLogicEndedEvent(new BlackjackGameStatus(GetName(), houseHand, userHand));
                houseAmount = CalculateHand(houseHand);

                if (houseAmount < 21) //House Didnt go over 21. Checking again
                {
                    HouseTurn(userAmount);
                }
                else if(houseAmount == 21) //House has a highest possible hand. House wins
                {
                    HandleGameResults(false);
                }
                else //House went over 21. User wins
                {
                    HandleGameResults(true);
                }
            }
            else //House has a higher or equal hand. House wins
            {
                HandleGameResults(false);
            }
        }

        private int CalculateHand(List<Card> hand) //Calculates the hand size
        {
            int i = 0;
            foreach(Card card in hand)
            {
                if(card.Value >= 10)
                {
                    i += 10;
                }
                else if (card.Value > 1){ 
                    i += card.Value;
                }
                else
                {
                    i += 11;
                }
            }
            if(i > Limit && hand.Any(x => x.Value == 1))
            {
                i -= 10;
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
            RaiseGameLogicEndedEvent(new BlackjackGameStatus(GetName(), houseHand, userHand));
        }

        public override void DoubleOrNothing(GameResult result)
        {
            Play(result.UsedBet * 2);
        }

        public override void HandleGameResults(bool won)
        {
            decimal win = 0m;
            if (won)
            {
                win = currentBet * 5;
            }

            GameResult res = new GameResult(GetName(), win, currentBet, won);
            RaiseGameResultsEvent(res);
            EndGame();
        }
    }
}
