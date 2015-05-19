using System;
using System.Timers;
using TheHardestGame.GameEngine.Events;
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

		private EngineRectangle _square;
		private readonly EngineRectangle[] _barriers;

		private readonly System.Timers.Timer _timer;

		
		#endregion

		#region Constructors

		public ConsoleUiGame()
		{
			Console.CursorVisible = false;

			this._timer = new System.Timers.Timer();
			this._timer.Interval = 50.0;
			this._timer.Elapsed += new ElapsedEventHandler(this.TimerElapsed);

			this._game = new EngineGame();
			this.RegisterEvents();

			this._square = this._game.GetStartSquare();
			this._barriers = this._game.GetStartBarriers();

			this._cd = new ConsoleDrawing();
		}

		#endregion

		#region Methods

		public EngineRectangle TransformEngineRectangle(EngineRectangle er)
		{
			return new EngineRectangle((int)(er.X / this._engineToConsoleMultiplier), (int)(er.Y / this._engineToConsoleMultiplier), (int)(er.Height / this._engineToConsoleMultiplier), (int)(er.Width / this._engineToConsoleMultiplier), er.Direction, er.IsBarrier);
		}

		public EngineRectangle TransformConsoleRectangle(EngineRectangle er)
		{
			return new EngineRectangle(er.X * this._engineToConsoleMultiplier, er.Y * this._engineToConsoleMultiplier, er.Height * this._engineToConsoleMultiplier, er.Width * this._engineToConsoleMultiplier, er.Direction, er.IsBarrier);
		}

		public void StartGame(int consoleHeight, int consoleWidth)
		{
			this._colors = new ConsoleColor[consoleHeight][];
			for (int a = 0; a < this._colors.Length; a++)
			{
				this._colors[a] = new ConsoleColor[consoleWidth];
			}

			this._engineToConsoleMultiplier = Math.Max(this._game.Width / consoleWidth, this._game.Height / consoleHeight);

			this._cd.DrawLevelBackground(consoleHeight, consoleWidth, (int)(this._game.RectHeight / this._engineToConsoleMultiplier),
				(int)(this._game.RectWidth / this._engineToConsoleMultiplier), this._colors);

			
			this._square = this.TransformEngineRectangle(this._square);
			for (int a = 0; a < this._barriers.Length; a++)
			{
				this._barriers[a] = this.TransformEngineRectangle(this._barriers[a]);
				this._cd.DrawRectangle(this._barriers[a], ConsoleColor.Blue);
			}

			this._cd.DrawRectangle(this._square, ConsoleColor.Red);

			Console.ResetColor();
			Console.SetWindowPosition(0,0);
			Console.SetCursorPosition(0, consoleHeight - 1);

			this._timer.Start();

			for (;;)
			{
				ConsoleKeyInfo cki = Console.ReadKey();

				if (cki.Key == ConsoleKey.Q)
				{
					lock (Syncronization.SyncObj)
					{
						Console.ResetColor();
						Console.SetCursorPosition(0, Console.WindowHeight);
					}
					break;
				}
				else if (cki.Key == ConsoleKey.UpArrow)
				{
					this.MoveRectangle(ref this._square, ConsoleColor.Red, Directions.Up, 1);
				}
				else if (cki.Key == ConsoleKey.DownArrow)
				{
					this.MoveRectangle(ref this._square, ConsoleColor.Red, Directions.Down, 1);
				}

				else if (cki.Key == ConsoleKey.LeftArrow)
				{
					this.MoveRectangle(ref this._square, ConsoleColor.Red, Directions.Left, 1);
				}

				else if (cki.Key == ConsoleKey.RightArrow)
				{
					this.MoveRectangle(ref this._square, ConsoleColor.Red, Directions.Right, 1);
				}

				lock (Syncronization.SyncObj)
				{
					EngineRectangle temp = this.TransformConsoleRectangle(this._square);
					EngineRectangle[] barrs = new EngineRectangle[this._barriers.Length];
					for (int s = 0; s < barrs.Length; s++)
					{
						barrs[s] = this.TransformConsoleRectangle(this._barriers[s]);
					}
					this._game.CheckDeath(temp, barrs);
				}

				lock (Syncronization.SyncObj)
				{
					Console.BackgroundColor = ConsoleColor.DarkGreen;
					Console.SetCursorPosition(0, Console.WindowHeight - 1);
				}
			}
		}

		public void MoveRectangle(ref EngineRectangle er, ConsoleColor color, Directions direction, double offset)
		{
			er = this.TransformConsoleRectangle(er);
			if (!er.IsBarrier)
			{
				er.Direction = direction;
			}

			bool moved = this._game.MoveRectangle(ref er, offset*this._engineToConsoleMultiplier);
			er = this.TransformEngineRectangle(er);
			if (moved)
			{
				this._cd.DrawAfterMoving(er, 1, this._colors);
			}
			this._cd.DrawRectangle(er, color);
		}

		private void RegisterEvents()
		{
			this._game.EventFinish += this.FinishRaise;
			this._game.EventDie += this.DieRaise;
		}

		private void UnregisterEvents()
		{
			this._game.EventFinish -= this.FinishRaise;
			this._game.EventDie -= this.DieRaise;
		}

		private void TimerElapsed(object sender, ElapsedEventArgs e)
		{
			lock (Syncronization.SyncObj)
			{
				for (int a = 0; a < this._barriers.Length; a++)
				{
					if (a%2 == 0)
					{
						this.MoveRectangle(ref this._barriers[a], ConsoleColor.Blue, Directions.Right, 1);
					}
					else
					{
						this.MoveRectangle(ref this._barriers[a], ConsoleColor.Blue, Directions.Left, 1);
					}
				}
			}

			lock (Syncronization.SyncObj)
			{
				Console.SetCursorPosition(0, Console.WindowHeight - 1);
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			}

			this._timer.Interval = 50.0;
			this._game.CheckDeath(this._square, this._barriers);
		}

		#endregion

		#region Event Methods

		private void FinishRaise(Object sender, FinishEventArgs e)
		{
			if (e.Finish)
			{
				lock (Syncronization.SyncObj)
				{
					Console.SetCursorPosition(1, Console.WindowHeight);
					Console.ResetColor();
					Console.WriteLine("CONGRATULATION!!! You have finished level");
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

		private void DieRaise(Object sender, DieEventArgs e)
		{
			if (e.Die)
			{
				this._cd.Redraw(this._square, this._colors);
				this._square = this.TransformEngineRectangle(this._game.GetStartSquare());
				this._cd.DrawRectangle(this._square, ConsoleColor.Red);
			}
		}

		#endregion
	}
}
