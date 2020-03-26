using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace Mini_Gambler_Server
{
	using static Console;
	using static ConsoleColor;
	using static ConsoleUtil;
	class Program
	{
		public static Random SystemRNG = new Random();
		public static List<Room> Rooms = new List<Room>();
		public static List<Socket> Clients = new List<Socket>();
		public static Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		public static bool Run { get; private set; }

		public const int SERVER_PORT = 1234;

		private static Shell shell = new Shell();
		private static Thread ConnectionThread = new Thread(new ThreadStart(Mini_Gambler_Server.Threads.ConnectionHandlerThread));

		static void Main(string[] args)
		{
			Run = true;
			WriteLineColor("Server is starting!", Yellow);
			ServerSocket.Bind(new IPEndPoint(IPAddress.Any, SERVER_PORT));
			ServerSocket.Listen(100);
			ConnectionThread.Start();
			WriteLineColor("Server is running!", Green);
			shell.Run();
			Thread.Sleep(1000);
			WriteLine("Press enter to exit...");
			Read();
		}

		public static void StopServer()
		{
			Run = false;
			foreach(Socket s in Clients)
			{
				if (s.Connected)
				{
					s.Close();
					s.Dispose();
				}
			}
			ServerSocket.Close();
			ServerSocket.Dispose();
		}
	}
}
