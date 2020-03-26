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
		public int Coins = 100;

		public Player(string name)
		{
			Name = name;
		}
	}
}
