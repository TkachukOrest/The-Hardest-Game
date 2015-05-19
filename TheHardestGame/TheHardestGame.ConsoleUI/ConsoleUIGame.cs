using System;
using System.Timers;
using TheHardestGame.ConsoleUI.Utils;
using TheHardestGame.GameEngine.AdditionalClasses;
using TheHardestGame.GameEngine.Enums;
using TheHardestGame.GameEngine.Game;

namespace TheHardestGame.ConsoleUI
{
	internal class ConsoleUiGame
	{
		#region Fields

		private readonly EngineGame _game;
		private readonly ConsoleDrawing _cd;

		private double _engineToConsoleMultiplier;
		private ConsoleColor[][] _colors;

		private readonly Timer _timer;

		#endregion

		#region Constructors

		public ConsoleUiGame()
		{
			Console.CursorVisible = false;

			this._timer = new Timer();
			this._timer.Interval = 50.0;
			this._timer.Elapsed += new ElapsedEventHandler(this.TimerElapsed);

			this._game = new EngineGame();
			this.RegisterEvents();

			this._cd = new ConsoleDrawing();
		}

		#endregion

		#region Methods

		public void DisplayRules()
		{
			Console.WriteLine("GAME DESCRIPTION\n");
			Console.WriteLine(
				"The aim of this game is to move red square from left green zone to right one avoiding blue barriers.\n");
			Console.WriteLine("Use arrow keys to move red square.\n");
			Console.WriteLine("If red square steps on blue barrier, it will be returned to its start position.\n");
			Console.WriteLine("Press \"Q\" to stop the game.\n");
			Console.WriteLine("ENJOY)\n\n");

			Console.WriteLine("Press \"Enter\" to start the game.\n");

			bool start = false;
			for (; !start;)
			{
				ConsoleKeyInfo cki = Console.ReadKey();
				if (cki.Key == ConsoleKey.Enter)
				{
					start = true;
				}
				Console.SetCursorPosition(0, Console.CursorTop);
			}
		}

		public EngineRectangle TransformEngineRectangle(EngineRectangle er)
		{
			return new EngineRectangle((int) (er.X/this._engineToConsoleMultiplier), (int) (er.Y/this._engineToConsoleMultiplier),
				(int) (er.Height/this._engineToConsoleMultiplier), (int) (er.Width/this._engineToConsoleMultiplier), 0, er.Direction,
				er.IsBarrier);
		}

		public void StartGame(int consoleHeight, int consoleWidth)
		{
			this.DisplayRules();

			this._colors = new ConsoleColor[consoleHeight][];
			for (int a = 0; a < this._colors.Length; a++)
			{
				this._colors[a] = new ConsoleColor[consoleWidth];
			}

			this._engineToConsoleMultiplier = Math.Max(this._game.Width/consoleWidth, this._game.Height/consoleHeight);

			this._cd.DrawLevelBackground(consoleHeight, consoleWidth,
				(int) (this._game.RectHeight/this._engineToConsoleMultiplier),
				(int) (this._game.RectWidth/this._engineToConsoleMultiplier), this._colors);


			this._cd.DrawRectangle(this.TransformEngineRectangle(this._game.Square), ConsoleColor.Red);

			Console.ResetColor();
			Console.SetWindowPosition(0, 0);
			Console.SetCursorPosition(0, consoleHeight - 1);

			this._timer.Start();

			for (;;)
			{
				bool moved = false;
				ConsoleKeyInfo cki = Console.ReadKey();

				if (cki.Key == ConsoleKey.Q)
				{
					lock (Syncronization.SyncObj)
					{
						this._timer.Stop();
						Console.ResetColor();
						Console.SetCursorPosition(0, Console.WindowHeight);
						break;
					}
				}
				else if (cki.Key == ConsoleKey.UpArrow)
				{
					this._game.Square.Direction = Directions.Up;
					double offset = 1*this._engineToConsoleMultiplier;
					this._game.MoveRectangle(this._game.Square, offset);
				}

				else if (cki.Key == ConsoleKey.DownArrow)
				{
					this._game.Square.Direction = Directions.Down;
					double offset = 1*this._engineToConsoleMultiplier;
					this._game.MoveRectangle(this._game.Square, offset);
				}

				else if (cki.Key == ConsoleKey.LeftArrow)
				{
					this._game.Square.Direction = Directions.Left;
					double offset = 1*this._engineToConsoleMultiplier;
					this._game.MoveRectangle(this._game.Square, offset);
				}

				else if (cki.Key == ConsoleKey.RightArrow)
				{
					this._game.Square.Direction = Directions.Right;
					double offset = 1*this._engineToConsoleMultiplier;
					this._game.MoveRectangle(this._game.Square, offset);
				}

				if (moved)
				{
					lock (Syncronization.SyncObj)
					{
						this._cd.DrawRectangle(this.TransformEngineRectangle(this._game.Square), ConsoleColor.Red);
						this._cd.DrawAfterMoving(this.TransformEngineRectangle(this._game.Square), 1, this._colors);
					}
				}

				lock (Syncronization.SyncObj)
				{
					Console.BackgroundColor = ConsoleColor.DarkGreen;
					Console.SetCursorPosition(0, Console.WindowHeight - 1);
				}
			}
		}

		private void RegisterEvents()
		{
			this._game.EventFinish += this.FinishRaise;
			this._game.EventDie += this.DieRaise;
			this._game.EventMove += this.MoveRaise;
		}

		private void UnregisterEvents()
		{
			this._game.EventFinish -= this.FinishRaise;
			this._game.EventDie -= this.DieRaise;
			this._game.EventMove -= this.MoveRaise;
		}

		private void TimerElapsed(object sender, ElapsedEventArgs e)
		{
			for (int a = 0; a < this._game.Barriers.Length; a++)
			{
				lock (Syncronization.SyncObj)
				{
					double offset = 1*this._engineToConsoleMultiplier;
					this._game.MoveRectangle(this._game.Barriers[a], offset);
					this._cd.DrawRectangle(this.TransformEngineRectangle(this._game.Barriers[a]), ConsoleColor.Blue);
					this._cd.DrawAfterMoving(this.TransformEngineRectangle(this._game.Barriers[a]), 1, this._colors);
				}
			}

			lock (Syncronization.SyncObj)
			{
				Console.SetCursorPosition(0, Console.WindowHeight - 1);
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			}
		}

		#endregion

		#region Event Methods

		private void FinishRaise(Object sender, bool e)
		{
			if (e)
			{
				lock (Syncronization.SyncObj)
				{
					Console.SetCursorPosition(1, Console.WindowHeight);
					Console.ResetColor();
					Console.WriteLine("CONGRATULATION!!! You have finished this game.");
					Console.WriteLine("Press \"q\" to exit.");
				}
				this.UnregisterEvents();
				this._timer.Stop();
				for (;;)
				{
					ConsoleKeyInfo cki = Console.ReadKey();
					lock (Syncronization.SyncObj)
					{
						Console.ResetColor();
						Console.SetCursorPosition(0, Console.WindowHeight);
					}
					if (cki.Key == ConsoleKey.Q)
					{
						lock (Syncronization.SyncObj)
						{
							Console.SetCursorPosition(1, Console.WindowHeight + 1);
							Console.WriteLine();
						}
						Environment.Exit(0);
					}
				}
			}
		}

		private void DieRaise(Object sender, EngineRectangle e)
		{
			lock (Syncronization.SyncObj)
			{
				this._cd.DrawAfterMoving(this.TransformEngineRectangle(e), 1, this._colors);
				this._cd.Redraw(this.TransformEngineRectangle(e), this._colors);
				this._cd.DrawRectangle(this.TransformEngineRectangle(this._game.Square), ConsoleColor.Red);
			}
		}

		private void MoveRaise(object sender, EngineRectangle e)
		{
			this._cd.DrawAfterMoving(this.TransformEngineRectangle(e), 1, this._colors);
			this._cd.DrawRectangle(this.TransformEngineRectangle(e), ConsoleColor.Red);
		}

		#endregion
	}
}