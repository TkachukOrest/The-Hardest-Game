using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHardestGame.ConsoleUI
{
	internal static class Syncronization
	{
		private static readonly object _sync = new object();

		public static object SyncObj
		{
			get { return _sync; }
		}
	}
}
