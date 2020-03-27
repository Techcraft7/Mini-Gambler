using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGamblerGameLib
{
	public class Player
	{
		public readonly string Name;
		public int Coins = 150;
		public Hand hand = Hand.RandomHand();
		public bool Alive { get; private set; }

		public Player(string name)
		{
			Name = name;
			Alive = true;
		}

		public void Kill()
		{
			Alive = false;
		}
	}
}
