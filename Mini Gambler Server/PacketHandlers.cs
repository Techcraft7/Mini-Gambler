using MiniGamblerNetworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PacketMethod = System.Action<byte[], System.Net.Sockets.Socket>;


namespace Mini_Gambler_Server
{
	internal static class PacketHandlers
	{
		public static Packet[] PACKETS = new Packet[]
		{
			new Packet(0x1, new PacketMethod(CreateRoom)),
			new Packet(0x2, new PacketMethod(JoinRoom))
		};

		private static void JoinRoom(byte[] data, Socket client)
		{
			try
			{

			}
			catch
			{
				ConsoleUtil.WriteLineColor("Error handling Join Room packet!", ConsoleColor.Red);
			}
		}

		private static void CreateRoom(byte[] data, Socket client)
		{
			try
			{
				byte[] b = new byte[4];
				uint ID = BitConverter.ToUInt32(b, 0);
				do
				{
					Program.SystemRNG.NextBytes(b);
					ID = BitConverter.ToUInt32(b, 0);
				}
				while (ID != uint.MaxValue);
				Console.WriteLine("Creating room " + ID);
				client.Send(b);
				byte numPlayers = data[0];
				if (numPlayers < 2 || numPlayers > 10)
				{
					client.Send(BitConverter.GetBytes(uint.MaxValue));
					return;
				}
				Room r = new Room(ID, numPlayers, client);
				Program.Rooms.Add(r);
			}
			catch
			{
				ConsoleUtil.WriteLineColor("Error handling Create Room packet!", ConsoleColor.Red);
			}
		}

		public static void HandlePing(byte[] data, Socket client)
		{
			try
			{
				client.Send(data);
			}
			catch
			{
				ConsoleUtil.WriteLineColor("Error handling Ping packet!", ConsoleColor.Red);
			}
		}
	}
}
