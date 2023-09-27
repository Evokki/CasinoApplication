using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambleAssetsLibrary
{
    public class Poker : Game
    {
        public Deck Deck; // Tossa sun pakkas. Saat Card-objectin sielt käyttämäl GetCard funktiota eli Deck.GetCard()//Jos haluut katella tota lisää ni se löytyy tuolt libraryst BaseClasses.cs
        public List<Card> Playerhand;             
        public Poker(string S) : base(S)
        {
            Deck = new Deck();
            Playerhand = new List<Card>
            {
                Deck.GetCard(), // 2 ensimmäistä korttia
                Deck.GetCard(),

                Deck.GetCard(), // kaksi korttia lisää mistä käyttäjä valitsee yhden
                Deck.GetCard()
            };


        }

      
        public override void StartGame() // Tätä funktiota ei ees callata mistää ni älä välitä
        {
            Console.WriteLine("Slots start"); //Tää on vaa sen takii et softa toimii. Se throw new exception kaataa aina pelin ni korvasin vaa tällä.
        }

        public override void EndGame()
        {
            Console.WriteLine("Slots end"); //Tää on vaa sen takii et softa toimii. Se throw new exception kaataa aina pelin ni korvasin vaa tällä.
        }


        public override void Play(decimal bet) //Tää callataan PokerView napista
        {
            Console.WriteLine("play"); //Tähän pelilogiikka
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
