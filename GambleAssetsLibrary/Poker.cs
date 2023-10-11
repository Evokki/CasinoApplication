using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GambleAssetsLibrary
{
    public class Poker : Game
    {
        public Deck Deck; 
        private List<Card> playerHand = new List<Card>();
        private List<Card> stack1 = new List<Card>();
        private List<Card> stack2 = new List<Card>();
        private int[] winMultipliers = { 0, 1, 2, 3, 5, 8, 10, 15, 20};
        private int WinMultiplier = 0;
        public Poker(string S) : base(S)
        {
            Deck = new Deck();
        }

        public override void StartGame() // Tätä funktiota ei ees callata mistää ni älä välitä
        {
            Console.WriteLine("Slots start"); //Tää on vaa sen takii et softa toimii. Se throw new exception kaataa aina pelin ni korvasin vaa tällä.
        }

        public override void EndGame()
        {
            playerHand.Clear();
            stack1.Clear();
            stack2.Clear();
            Deck.ResetDeck();
        }

        public override void Play(decimal bet) //Tää callataan PokerView napista // en tiiä mihivälii asioita pitää tunkee jote ne nyt on tässä kai? -Allu
        {
            currentBet = bet;
            for(int i = 0; i < 3; i++)
            {
                if(i < 2)
                {
                    playerHand.Add(Deck.GetCard());
                }
                stack1.Add(Deck.GetCard());
                stack2.Add(Deck.GetCard());
            }
            RaiseGameLogicEndedEvent(new PokerGameStatus(GetName(), playerHand, stack1, stack2));
        }

        public void OnSelected(int stack)
        {
            if(stack == 0)
            {
                playerHand = playerHand.Concat(stack1).ToList();
            }else if (stack == 1)
            {
                playerHand = playerHand.Concat(stack2).ToList();
            }

            stack1.Clear();
            stack2.Clear();

            int HandResult = PokerHands.CheckHand(playerHand);
            WinMultiplier = winMultipliers[HandResult];

            RaiseGameLogicEndedEvent(new PokerGameStatus(GetName(), playerHand, stack1, stack2));

            HandleGameResults(HandResult > 0);
        }

        public override void HandleGameResults(bool userWon = false)
        {

            decimal win = 0;
            if (userWon)
            {
                win = currentBet * WinMultiplier; 
            }

            RaiseGameResultsEvent(new GameResult(GetName(), win, currentBet, userWon));
            EndGame();
        }

    }
}
