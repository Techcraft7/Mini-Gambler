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
			new Packet(0x2, new PacketMethod(JoinRoom)),
			new Packet(0x3, new PacketMethod(RoomStatus))
		};

		private static uint GetRoomID(byte[] data) => BitConverter.ToUInt32(data, 0);
		private static Room GetRoom(uint ID) => Program.Rooms.Find(r => r.ID == ID);

		private static void RoomStatus(byte[] data, Socket client)
		{
			try
			{
				uint ID = GetRoomID(data);
				Room room = GetRoom(ID);

			}
			catch
			{
				ConsoleUtil.WriteLineColor("Error handling Room Status packet!", ConsoleColor.Red);
			}
		}

		private static void JoinRoom(byte[] data, Socket client)
		{
			try
			{
				uint ID = GetRoomID(data);
				Room room = GetRoom(ID);
				byte nameLength = data[4];
				string name = Encoding.ASCII.GetString(data.Skip(5).ToArray()).Substring(0, nameLength);
				Console.WriteLine("Got name: " + name + " Bytes: " + BitConverter.ToString(data, 5, nameLength).ToUpper());
				if (room != null)
				{
					if (room.IsNameAvailable(name) && name.ToCharArray().ToList().FindIndex(c => c < 33 || c > 126) == -1 && nameLength > 0)
					{
						Console.WriteLine($"Adding {name} to room {ID}");
						client.Send(new byte[] { 1 });
						room.Join(client, name);
					}
					else
					{
						client.Send(new byte[] { 2 });
					}
				}
				else
				{
					client.Send(new byte[] { 0 });
				}
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
				uint ID = uint.MaxValue;
				while (ID == uint.MaxValue || Program.Rooms.Find(v => v.ID == ID) != null)
				{
					ID = (uint)Program.SystemRNG.Next();
				}
				Console.WriteLine("Creating room " + ID.ToString("X8"));
				client.Send(BitConverter.GetBytes(ID));
				byte numPlayers = data[0];
				if (numPlayers < 2 || numPlayers > 10)
				{
					client.Send(BitConverter.GetBytes(uint.MaxValue));
					return;
				}
				Room room = new Room(ID, numPlayers);
				Program.Rooms.Add(room);
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
