using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambleAssetsLibrary
{
    public class PokerHands
    {
        public static bool IsPair(List<Card> hand)
        {
            var values = hand.Select(card => card.Value).ToList();
            return values.Any(v => values.Count(val => val == v) == 2);
        }

        public static bool IsTwoPairs(List<Card> hand)
        {
            var values = hand.Select(card => card.Value).ToList();
            return values.Where(v => values.Count(val => val == v) == 2).Distinct().Count() == 2;
        }

        public static bool IsThreeOfAKind(List<Card> hand)
        {
            var values = hand.Select(card => card.Value).ToList();
            return values.Any(v => values.Count(val => val == v) == 3);
        }

        public static bool IsFours(List<Card> hand)
        {
            var values = hand.Select(card => card.Value).ToList();
            return values.Any(v => values.Count(val => val == v) == 4);
        }

        public static bool IsFlush(List<Card> hand)
        {
            var Suits = hand.Select(card => card.Suit).ToList();
            return Suits.Any(Suit => Suits.Count(h => h == Suit) == 5);
        }

        public static bool IsStraight(List<Card> hand)
        {
            var values = hand.Select(card => card.Value).ToList();
            values.Sort();
            return Enumerable.Range(1, values.Count - 1).All(i => values[i] == values[i - 1] + 1);
        }

        public static bool IsStraightFlush(List<Card> hand)
        {
            return IsFlush(hand) && IsStraight(hand);
        }

        public static bool IsRoyalFlush(List<Card> hand)
        {
            var values = hand.Select(card => card.Value).ToList();
            var royalFlushValues = new List<int> { 10, 11, 12, 13, 1 };
            return royalFlushValues.Any(v => values.Contains(v)) && IsFlush(hand);
        }

        public static bool IsFullSuit(List<Card> hand)
        {
            return IsPair(hand) && IsThreeOfAKind(hand);
        }

        public static int CheckHand(List<Card> hand)
        {
            int i = 9;
            Func<List<Card>, bool>[] bestHands = {
            IsRoyalFlush, IsStraightFlush, IsFours, IsFullSuit, IsStraight,
            IsFlush, IsThreeOfAKind, IsTwoPairs, IsPair
            };
            foreach(var best in bestHands)
            {
                i--;
                if (best(hand))
                {
                    return i;
                }
            }

            return 0;
        }
    }

}
