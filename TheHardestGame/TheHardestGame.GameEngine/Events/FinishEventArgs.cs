using System;

namespace TheHardestGame.GameEngine.Events
{
	public class FinishEventArgs : EventArgs
	{
		#region Fields

		public bool Finish { get; private set; }

		#endregion

		#region Constructors

		public FinishEventArgs(bool finish)
		{
			this.Finish = finish;
		}

		#endregion
	}
}
