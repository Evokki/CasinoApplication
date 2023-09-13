using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFgambleLibrary
{
    public class Blackjack: Game 
    {
        public Deck deck;
        public Blackjack()
        {
            deck = new Deck();
            Start();
        }
        public override void Start()
        {
            throw new NotImplementedException();
        }
        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
