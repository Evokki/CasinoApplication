using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambleAssetsLibrary
{
    public class Slots : Game
    {
        public Slots(string s = "Slots"): base(s) { 
        }
        public override void StartGame()
        {
            throw new NotImplementedException();
        }
        //kolme listaa 
        //Listoihin eri hedelmä ja niiden arpominen tiettyjen mahdollisuukisen perusteella
        //Paina spin 
        //arpoo listojen hedelmät
        //palauta saadut hedelmät tulosruutuun
        public override void EndGame()
        {
            throw new NotImplementedException();
        }

        public override void Roll()
        {
            throw new NotImplementedException();
        }
    }
}
