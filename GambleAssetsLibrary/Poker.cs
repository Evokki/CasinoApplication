using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambleAssetsLibrary
{
    public class Poker : Game
    {
        public Deck Deck; // Tossa sun pakkas. Saat Card-objectin sielt käyttämäl GetCard funktiota eli Deck.GetCard()
        //Jos haluut katella tota lisää ni se löytyy tuolt libraryst BaseClasses.cs
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
            Console.WriteLine("Slots end"); //Tää on vaa sen takii et softa toimii. Se throw new exception kaataa aina pelin ni korvasin vaa tällä.
        }


        public override void Play(decimal bet) //Tää callataan PokerView napista // en tiiä mihivälii asioita pitää tunkee jote ne nyt on tässä kai? -Allu
        { }
            public class PokerGame
        {
            private Deck deck;
            private List<Card> playerHand;

            public PokerGame()
            {
                deck = new Deck();
                playerHand = new List<Card>();

                // Deal two random cards to the player's hand.
                playerHand.Add(deck.GetCard());
                playerHand.Add(deck.GetCard());

                // Deal two additional random cards to the player's hand.
                playerHand.Add(deck.GetCard());
                playerHand.Add(deck.GetCard());
            }

            public void DisplayPlayerHand()
            {
                Console.WriteLine("Your Hand:");
                for (int i = 0; i < playerHand.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {playerHand[i].Name} of {playerHand[i].Suit}");
                }
            }

            public void SelectCard()
            {
                Console.WriteLine("Choose a card to keep (enter the number):");
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid input. Enter the number of the card you want to keep.");
                }

                // Remove the unselected card from the player's hand.
                playerHand.RemoveAt(choice - 1);
            }



            public void EvaluateHand()
            {
                playerHand.Sort((card1, card2) => card1.Value.CompareTo(card2.Value));
                // Check for a pair (two cards with the same value).
                for (int i = 0; i < playerHand.Count - 1; i++)
                {
                    if (playerHand[i].Value == playerHand[i + 1].Value)
                    {
                        Console.WriteLine("You have a pair!");
                        return;
                    }
                }

                // Check for three of a kind (three cards with the same value).
                for (int i = 0; i < playerHand.Count - 2; i++)
                {
                    if (playerHand[i].Value == playerHand[i + 1].Value && playerHand[i].Value == playerHand[i + 2].Value)
                    {
                        Console.WriteLine("You have three of a kind!");
                        return;
                    }
                }

                // Checkki for four of a kind (4 samaa) ehkä oikein?
                for (int i = 0; i < playerHand.Count - 3; i++)
                {
                    if (playerHand[i].Value == playerHand[i + 1].Value && playerHand[i].Value == playerHand[i + 2].Value && playerHand[i].Value == playerHand[i + 3].Value)
                    {
                        Console.WriteLine("You have four of a kind!");
                        return;
                    }
                }



                 //2 paria checkki? ei mitää hajua miten tätä käytetään 
                 bool HasPair(List<Card> hand)
                {
                    Dictionary<int, int> rankCount = new Dictionary<int, int>();

                    foreach (var card in hand)
                    {
                        if (!rankCount.ContainsKey(card.Value))
                        {
                            rankCount[card.Value] = 1;
                        }
                        else
                        {
                            rankCount[card.Value]++;
                        }
                    }

                    return rankCount.Any(pair => pair.Value == 2);
                }


                
                // If no winning hands were found, you can display a message for a high card.
                Console.WriteLine("High card!");
            }
        }
       
        
         

        class Program
        {
            static void Main(string[] args)
            {
                PokerGame game = new PokerGame();
                game.DisplayPlayerHand();
                game.SelectCard(); // Let the player choose a card to keep.

                // Implement the rest of the game logic here, including hand evaluation.
            }
        }

    
    public override void HandleGameResults(bool userWon = false)
        {

            decimal win = 0;
            if (userWon)
            {
                win = currentBet * 10; //muokkaa toi win sillee et se antaa korttien perusteel voiton
            }

            RaiseGameResultsEvent(new GameResult(GetName(), win, currentBet, userWon)); //älä koske
        }

    }
}
