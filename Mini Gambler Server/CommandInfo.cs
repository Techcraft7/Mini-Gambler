using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Gambler_Server
{
	internal class CommandInfo
	{
		public string Name;
		public string Help;

		public CommandInfo(string name, string help)
		{
			Name = name;
			Help = help;
		}
	}
}
