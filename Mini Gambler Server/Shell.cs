using System;
using System.Collections.Generic;
using System.Linq;
using CmdKV = System.Collections.Generic.KeyValuePair<Mini_Gambler_Server.CommandInfo, System.Action<string[]>>;

namespace Mini_Gambler_Server
{
	internal class Shell
	{
		private static List<CmdKV> Commands;

		public Shell()
		{
			Commands = new List<CmdKV>()
			{
				new CmdKV(new CommandInfo("stop", "Stop the server"), new Action<string[]>(Stop))
			};
		}

		private static void Stop(string[] args)
		{
			ConsoleUtil.WriteLineColor("Stopping server!", ConsoleColor.Magenta);
			Program.StopServer();
		}

		internal void Run()
		{
			while (Program.Run)
			{
				string input = Console.ReadLine();
				string cmd = input.Split(' ')[0];
				if (string.IsNullOrWhiteSpace(cmd))
				{
					continue;
				}
				string[] args = input.Split(' ').Skip(1).ToArray();
				int cmdIndex = Commands.FindIndex(c => c.Key.Name == cmd);
				if (cmdIndex > -1)
				{
					Commands[cmdIndex].Value.Invoke(args);
				}
			}
		}
	}
}
