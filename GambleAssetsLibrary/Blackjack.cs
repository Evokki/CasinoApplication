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
        public Deck deck;
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
            Console.WriteLine("Blackjack roll");
        }
    }
}
