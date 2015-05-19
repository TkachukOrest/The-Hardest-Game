namespace TheHardestGame.ConsoleUI.Utils
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