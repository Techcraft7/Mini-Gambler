using MiniGamblerGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Gambler_Server
{
	internal class Room
	{
		public uint ID;
		public readonly Game game;
		private readonly byte NumberOfPlayers;
		private Dictionary<Socket, Player> Players = new Dictionary<Socket, Player>();

		public Room(uint ID, byte numPlayers)
		{
			this.ID = ID;
			NumberOfPlayers = numPlayers;
			game = new Game(NumberOfPlayers);
		}

		public void Join(Socket player, string name)
		{
			Players.Add(player, new Player(name));
		}

		public bool IsNameAvailable(string name) => Players.Values.ToList().FindIndex(p => p.Name == name) < 0;
	}
}