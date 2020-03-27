using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniGamblerGameLib;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MiniGamblerTests
{
	using static CardType;
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void HandTests()
		{
			Hand[] hands = new Hand[]
			{
				new Hand(ID1, ID2, ID3, ID4, ID5),
				new Hand(ID1, ID1, ID3, ID4, ID5),
				new Hand(ID1, ID1, ID2, ID2, ID5),
				new Hand(ID1, ID1, ID1, ID2, ID5),
				new Hand(ID1, ID1, ID1, ID2, ID2),
				new Hand(ID1, ID1, ID1, ID1, ID2),
				new Hand(ID1, ID1, ID1, ID1, ID1)
			};
			foreach (Hand h in hands)
			{
				Console.WriteLine("Hand Type: " + h.GetType());
			}
		}

		[TestMethod]
		public void WinTests()
		{
			Hand h1 = Hand.RandomHand();
			Hand h2 = Hand.RandomHand();
			h1.Cards.ForEach(c => { Console.Write(c + " "); });
			Console.WriteLine(h1.GetType() + "\nVS");
			h2.Cards.ForEach(c => { Console.Write(c + " "); });
			Console.WriteLine(h2.GetType());
			int w = 0;
			switch (h1.CompareTo(h2))
			{
				case 1:
					w = 1;
					break;
				case -1:
					w = 2;
					break;
				case -2:
					w = -1;
					break;
			}
			if (w == 0)
			{
				Console.WriteLine("It\'s a draw!");
			}
			else if (w == -1)
			{
				Console.WriteLine("Error!");
			}
			else
			{
				Console.WriteLine($"Hand {w} wins!");
			}
		}
	}
}
