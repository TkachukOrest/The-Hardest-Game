using System;

namespace TheHardestGame.ConsoleUI
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WindowWidth = 100;
			Console.WindowHeight = 30;

			ConsoleUiGame cuig = new ConsoleUiGame();
			cuig.StartGame(Console.WindowHeight, Console.WindowWidth);
			
		}
	}
}
