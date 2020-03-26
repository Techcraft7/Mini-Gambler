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
		public uint iD;
		private readonly byte NumberOfPlayers;
		private Dictionary<Socket, Player> Players = new Dictionary<Socket, Player>();

		public Room(uint iD, byte numPlayers, Socket client)
		{
			this.iD = iD;
			NumberOfPlayers = numPlayers;
			Players.Add(client, new Player("Player1"));
		}
	}
}
