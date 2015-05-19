using System;

namespace TheHardestGame.GameEngine.Events
{
	public class DieEventArgs : EventArgs
	{
		#region Fields

		public bool Die { get; private set; }

		#endregion

		#region Constructors

		public DieEventArgs(bool die)
		{
			this.Die = die;
		}

		#endregion
	}
}
