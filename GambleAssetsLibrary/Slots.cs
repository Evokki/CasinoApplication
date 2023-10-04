using System;
using System.Collections.Generic;
using System.Linq;

namespace GambleAssetsLibrary
{
    public class Slots : Game
    {
        private int WinMultiplier = 0;
        public Slots(string s = "Slots") : base(s)
        {
            
        }
        public override void StartGame()
        {
           
        }

        public override void EndGame()
        {
            Console.WriteLine("Slots end"); //Tää on vaa sen takii et softa toimii. Se throw new exception kaataa aina pelin ni korvasin vaa tällä.
        }

        public override void Play(decimal bet)
        {
            static int GenerateRandomNumber(int[] numbers, double[] probabilities, Random random)
            {
                double randomValue = random.NextDouble();
                double cumulativeProbability = 0;

                for (int i = 0; i < numbers.Length; i++)
                {
                    cumulativeProbability += probabilities[i];

                    if (randomValue < cumulativeProbability)
                    {
                        return numbers[i];
                    }
                }


                throw new InvalidOperationException("Invalid probabilities");
            }

            int[] numbers = { 1, 2, 3, 4, 5 };
            double[] probabilities = { 0.5, 0.25, 0.125, 0.085, 0.04 };

            Random random = new Random();
            int[] rolledNumbers = new int[3];

            for (int i = 0; i < 3; i++)
            {
                rolledNumbers[i] = GenerateRandomNumber(numbers, probabilities, random);
                
            }
            int Win1 = rolledNumbers.Count(x => x == 1);
            int Win2 = rolledNumbers.Count(x => x == 2);
            int Win3 = rolledNumbers.Count(x => x == 3);
            int Win4 = rolledNumbers.Count(x => x == 4);
            int Win5 = rolledNumbers.Count(x => x == 5);

            bool won = false;

            if (Win1 == 3)
            {
                won = true;
                
                //win1 = currentBet * 5;
            }
            if (Win2 == 3)
            {
                won = true;
               
                //win2 = currentBet * 10;
            }
            if (Win3 == 3)
            {
                won = true;
               
                //win3 = currentBet * 20;
            }
            if (Win4 == 3)
            {
                won = true;
               
                //win4 = currentBet * 50;
            }
            if (Win5 == 3)
            {
                won = true;
                WinMultiplier = 100; // näin

                //win5 = currentBet * 100;

            }

            RaiseGameLogicEndedEvent(new GameStatus(GetName()));
            HandleGameResults(won); //älä koske
        }

        public override void HandleGameResults(bool userWon = false) //Callaa tää sen jälkee ku numerot on arvottu
        {
            
            decimal win = 0;
            if (userWon)
            {
                win = currentBet * WinMultiplier; //muokkaa toi win sillee et se antaa niiden numerojen perusteel voiton
            }

            RaiseGameResultsEvent(new GameResult(GetName(),win, currentBet, userWon)); //älä koske
        }
    }
}


