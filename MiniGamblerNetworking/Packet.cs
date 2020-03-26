using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiniGamblerNetworking
{
	public class Packet
	{
		public byte ID = 0xFF;

		public Action<byte[], Socket> PacketHandler;

		public Packet(byte ID, Action<byte[], Socket> Handler)
		{
			this.ID = ID != 0 ? ID : throw new ArgumentException("The ID of a packet cannot be zero!", "ID", new PacketReservedException());
			PacketHandler = Handler ?? throw new ArgumentNullException("Handler", "The handler of a packet cannot be null!");
		}

		public void HandlePacket(byte[] data, Socket client)
		{
			if (data[0] != ID)
			{
				return;
			}
			Console.WriteLine($"Handling packet: {ID}");
			data = data.Skip(1).ToArray();
			PacketHandler.Invoke(data, client);
		}

		public override string ToString() => $"Packet: {{ ID: {ID.ToString("X2")} }}";
	}
}
