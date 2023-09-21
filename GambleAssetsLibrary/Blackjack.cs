using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        public override void Roll()
        {
            InitHands();
        }
        public void HandleHitOrStand(bool state)
        {
            if (state)
            {
                userHand
            }
            else
            {

            }
        }
        private void InitHands()
        {
            for(int i = 0; i < 2; i++)
            {
                houseHand.Add(deck.GetCard());
                userHand.Add(deck.GetCard());
            }
        }

        public override double HandlePayout()
        {
            return 0.0;
        }
    }
}
