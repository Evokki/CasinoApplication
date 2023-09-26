using System;
using System.Collections.ObjectModel;

namespace GambleAssetsLibrary
{
    public static class GambleExtensionMethods
    {
        public static string toString(this ObservableCollection<Card> l)
        {
            int x = 0;
            foreach (var c in l)
            {
                x += c.Value;
            }
            return x.ToString();
        }
        public static int BJHandValues(this ObservableCollection<Card> l)
        {
            int x = 0;
            foreach (var c in l)
            {
                if(c.Value > 10)
                {
                    x += 10;
                }
                else
                {
                    x += c.Value;
                }
            }
            return x;
        }
        public static string CardSuitPath(this string suit)
        {
            return "/Ohjelmistokehitysprojekti;Component/Resources/Images/suits_" + suit + ".png";
        }
    }
}
