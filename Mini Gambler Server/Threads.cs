using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using MiniGamblerNetworking;

namespace Mini_Gambler_Server
{
	using static ConsoleColor;
	internal static class Threads
	{
		public static ulong ThreadID = 0;

		public static void ClientHandlerThread(object c)
		{
			ulong ID = ThreadID;
			ThreadID++;
			Socket client = (Socket)c;
			while (Program.Run && client.Connected)
			{
				try
				{
					byte[] data = new byte[1024];
					Log("Waiting for data", ID, Yellow);
					int len = client.Receive(data, data.Length, SocketFlags.None);
					Log($"Got data: 0x{BitConverter.ToString(data, 0, len).Replace("-", string.Empty).ToUpper()}", ID, Green);
					if (data[0] == 0)
					{
						PacketHandlers.HandlePing(data, client);
						continue;
					}
					foreach (Packet p in PacketHandlers.PACKETS)
					{
						p.HandlePacket(data, client);
					}
				}
				catch (Exception e)
				{
					Log(e.Message, ID, Red);
				}
			}
			Log("Exiting!", ID, Cyan);
		}

		public static void ConnectionHandlerThread()
		{
			ulong ID = ThreadID;
			ThreadID++;
			try
			{
				while (Program.Run && (Program.Clients.Count > 0 ? Program.Clients.FindIndex(s => s.Connected) > -1 : true))
				{
					Log("Waiting for a connection", ID);
					Socket client = Program.ServerSocket.Accept();
					Program.Clients.Add(client);
					Log("We got a connection!", ID, Green);
					Thread clientThread = new Thread(new ParameterizedThreadStart(ClientHandlerThread));
					clientThread.Start(client);
				}
			}
			catch (Exception e)
			{
				Log(e.Message, ID, Red);
			}
			Log("Exiting!", ID, Cyan);
		}

		private static void Log(string text, ulong ID, ConsoleColor color = Gray)
		{
			Thread.Sleep(100);
			StackTrace stackTrace = new StackTrace();
			string name = string.Empty;
			foreach (char c in stackTrace.GetFrame(1).GetMethod().Name)
			{
				if (c.ToString() == c.ToString().ToUpper())
				{
					name += " ";
				}
				name += c;
			}

			ConsoleUtil.WriteLineColor($"[THREAD:{name}#{ID}]: {text}", color);
		}
	}
}
