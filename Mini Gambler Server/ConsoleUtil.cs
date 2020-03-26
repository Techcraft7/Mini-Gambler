using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Gambler_Server
{
	internal static class ConsoleUtil
	{
		public static void WriteLineColor(string text, ConsoleColor color) => WriteColor(text + "\n", color);

		public static void WriteColor(string text, ConsoleColor color)
		{
			ConsoleColor prev = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.Write(text);
			Console.ForegroundColor = prev;
		}
	}
}
