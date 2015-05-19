using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHardestGame.DesktopUI
{
	public class MoveEventArgs : EventArgs
	{
		public int Direction { get; private set; }

		public MoveEventArgs(int direction)
		{
			Direction = direction;
		}
	}
}
