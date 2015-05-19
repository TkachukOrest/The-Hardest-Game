using System;

namespace TheHardestGame.DesktopUI
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			//Application.EnableVisualStyles();
			//Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new MainWindow());

			DesktopUiGame duig = new DesktopUiGame();
			//duig.StartGame();
			duig.StartGame();
		}
	}
}