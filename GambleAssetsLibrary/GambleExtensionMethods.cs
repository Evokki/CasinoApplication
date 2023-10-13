using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

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
            int i = 0;
            foreach (Card card in l)
            {
                if (card.Value >= 10)
                {
                    i += 10;
                }
                else if (card.Value > 1)
                {
                    i += card.Value;
                }
                else
                {
                    i += 11;
                }
            }
            if (i > 21 && l.Any(x => x.Value == 1))
            {
                i -= 10;
            }
            return i;
        }
        public static string CardSuitPath(this string suit)
        {
            return "/Ohjelmistokehitysprojekti;Component/Resources/Images/suits_" + suit + ".png";
        }

        public static string SlotIconPath(this int icon)
        {
            return "/Ohjelmistokehitysprojekti;Component/Resources/Images/Slots_" + icon + ".png";
        }
    }
}
