using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambleAssetsLibrary
{
    public class Blackjack: Game 
    {
        public Deck deck;
        public Blackjack()
        {
            deck = new Deck();
            StartGame();
        }
        public override void StartGame()
        {
            throw new NotImplementedException();
        }
        public override void EndGame()
        {
            throw new NotImplementedException();
        }
    }
}
