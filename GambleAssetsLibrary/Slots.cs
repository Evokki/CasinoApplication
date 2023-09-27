using System;
using System.Collections.Generic;


namespace GambleAssetsLibrary
{
    public class Slots : Game
    {
        public Slots(string s = "Slots") : base(s)
        {
            
        }
        public override void StartGame()
        {
            List<int> Lista1 = new List<int>();

            Random rng = new Random();

            int[] numerot = { 1, 2, 3, 4, 5 };
            double[] mahdollisuudet = { 0.10, 0.15, 0.2, 0.25, 0.30 };

            for (int i = 0; i < 3; i++)
            {
                int randomnumero = uusirng(rng, numerot, mahdollisuudet);
                Lista1.Add(randomnumero);

            }


            foreach (int i in Lista1)
            {
                Console.Write(i + " ");

            }

            static int uusirng(Random rng, int[] numerot, double[] mahdollisuudet)
            {
                double randomsumma = rng.NextDouble();
                double jakautuminen = 0;

                for (int i = 0; i < 5; i++)
                {
                    jakautuminen += mahdollisuudet[i];
                    if (randomsumma < jakautuminen)
                        return numerot[i];
                }
                throw new Exception();

            }
        }

        public override void EndGame()
        {
            Console.WriteLine("Slots end"); //Tää on vaa sen takii et softa toimii. Se throw new exception kaataa aina pelin ni korvasin vaa tällä.
        }

        public override void Play(decimal bet)
        {
            Console.WriteLine("Lul"); // Tänne se arpomis systeemi

            bool won = true; //muokkaa tota sit voitonperusteel

            RaiseGameLogicEndedEvent(new GameStatus(GetName()));
            HandleGameResults(won); //älä koske
        }

        public override void HandleGameResults(bool userWon = false) //Callaa tää sen jälkee ku numerot on arvottu
        {
            //Lisää tähän logiikka mil se voitto lasketaa
            decimal win = 0;
            if (userWon)
            {
                win = currentBet * 10; //muokkaa toi win sillee et se antaa niiden numerojen perusteel voiton
            }

            RaiseGameResultsEvent(new GameResult(GetName(),win, currentBet, userWon)); //älä koske
        }
    }
}
