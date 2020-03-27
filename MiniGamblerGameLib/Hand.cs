using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGamblerGameLib
{
	public class Hand : IComparable<Hand>
	{
		public readonly List<CardType> Cards = new List<CardType>();

		public Hand(params CardType[] cards)
		{
			if (cards.Length != 5)
			{
				throw new ArgumentException("You must specify 5 cards!", "cards");
			}
			Cards = cards.Take(5).ToList();
			Cards.Sort();
		}

		public static Hand RandomHand()
		{
			//allow random to reset because System.Random is broken :(
			System.Threading.Thread.Sleep(30);
			CardType[] t = new CardType[5];
			Random rng = new Random();
			for (int i = 0; i < 5; i++)
			{
				t[i] = (CardType)Math.Floor(rng.NextDouble() * Enum.GetValues(typeof(CardType)).Length);
			}
			return new Hand(t);
		}

		public Dictionary<CardType, int> GetPairs()
		{
			Dictionary<CardType, int> pairs = new Dictionary<CardType, int>();
			foreach (CardType t in Enum.GetValues(typeof(CardType)))
			{
				pairs.Add(t, 0);
			}
			foreach (CardType c in Cards)
			{
				pairs[c]++;
			}
			pairs = pairs.Where(kv => kv.Value > 1).ToDictionary(x => x.Key, x => x.Value);
			pairs = pairs.OrderBy(kv => kv.Value).ToDictionary(x => x.Key, x => x.Value);
			return pairs;
		}

		public bool Beats(Hand other) => CompareTo(other) == 1;

		public new HandType GetType()
		{
			var pairs = GetPairs();
			switch (pairs.Count)
			{
				case 0:
					return HandType.Junk;
				case 1:
					switch (pairs.First().Value)
					{
						case 3:
							return HandType.ThreeOfAKind;
						case 4:
							return HandType.FourOfAKind;
						case 5:
							return HandType.FiveOfAKind;
					}
					return HandType.OnePair;
				case 2:
					if (pairs.First().Value == 2 && pairs.Last().Value == 3)
					{
						return HandType.FullHouse;
					}
					return HandType.TwoPairs;
				default:
					return HandType.Junk;
			}
		}

		public Dictionary<CardType, int> GetPairsByHighestCard()
		{
			var pairs = GetPairs();
			pairs = pairs.OrderByDescending(kv => kv.Key).ToDictionary(x => x.Key, x => x.Value);
			return pairs;
		}

		public int CompareTo(Hand other)
		{
			//get pairs as IEnumberable
			var thisPairs = GetPairs().Select(x => x);
			var otherPairs = other.GetPairs().Select(x => x);
			//If both are the same type
			if (GetType() == other.GetType())
			{
				//if both are junk
				if (GetType() == 0)
				{
					return 0;
				}
				else
				{
					//get the pairs by the highest number of cards
					thisPairs = thisPairs.Reverse();
					otherPairs = otherPairs.Reverse();
					//remove all of the pairs that have the same key
					while (thisPairs.First().Key == otherPairs.First().Key)
					{
						thisPairs = thisPairs.Skip(1);
						otherPairs = otherPairs.Skip(1);
						if (thisPairs.Count() == 0 || otherPairs.Count() == 0)
						{
							return 0;
						}
					}
					//check for the highest card
					//check if draw
					if (thisPairs.ToArray().Length == 0 || otherPairs.ToArray().Length == 0)
					{
						return 0;
					}
					else if (thisPairs.First().Key > otherPairs.First().Key)
					{
						return 1;
					}
					else
					{
						return -1;
					}
				}
			}
			else if (GetType() > other.GetType())
			{
				return 1;
			}
			else if (GetType() < other.GetType())
			{
				return -1;
			}
			return -2;
		}
	}
}
