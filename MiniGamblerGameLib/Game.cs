using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGamblerGameLib
{
	public class Game
	{
		private Player[] Players { get; set; }
		public bool Started = false;
		int turn = 0;

		public Game(byte NumPlayers)
		{
			Players = new Player[NumPlayers];
		}

		public Game(Player[] Players)
		{
			this.Players = Players ?? throw new ArgumentNullException("Players", "Players cannot be null!");
		}

		public void Stop() => Started = false;

		public void Start()
		{
			if (Players.ToList().FindIndex(p => p == null) == -1)
			{
				return;
			}
			Started = true;
		}

		public void AdvanceTurn()
		{
			if (Started)
			{
				turn = turn == Players.Length ? 0 : turn + 1;
			}
		}
	}
}
