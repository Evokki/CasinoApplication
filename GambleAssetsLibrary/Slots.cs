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
            throw new NotImplementedException();
        }

        public override void Play(decimal bet)
        {
            throw new NotImplementedException();
        }

        public override void HandleGameResults(bool userWon = false)
        {
            throw new NotImplementedException();
        }
    }
}
