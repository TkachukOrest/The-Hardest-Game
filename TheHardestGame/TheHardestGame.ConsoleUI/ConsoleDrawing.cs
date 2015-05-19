using System;

using TheHardestGame.GameEngine.AdditionalClasses;
using TheHardestGame.GameEngine.Enums;
using TheHardestGame.GameEngine.Game;

namespace TheHardestGame.ConsoleUI
{
	internal class ConsoleDrawing
	{
		#region Methods

		public void DrawLevelBackground(int consoleHeight, int consoleWidth, int rectHeight, int rectWidth, ConsoleColor[][] colors)
		{
			Console.BackgroundColor = ConsoleColor.Cyan;
			for (int a = 0; a < consoleHeight; a++)
			{
				Console.SetCursorPosition(0, a);
				for (int b = 0; b < consoleWidth; b++)
				{
					colors[a][b] = Console.BackgroundColor;
					Console.Write(" ");
				}
			}

			for (int a = 1; a < 5; a++)
			{
				for (int b = 5; b < 15; b++)
				{
					if ((a + b)%2 != 0)
					{
						Console.BackgroundColor = ConsoleColor.White;
					}
					else
					{
						Console.BackgroundColor = ConsoleColor.Gray;
					}
					EngineRectangle er = new EngineRectangle(b * rectWidth, a * rectHeight, rectHeight, rectWidth);
					this.DrawRectangle(er, Console.BackgroundColor);
					this.GetRectangeColors(er, colors);
				}
			}

			EngineRectangle topGray = new EngineRectangle(new EnginePoint(rectWidth * 14, 0), new EngineSize(rectHeight, rectWidth));
			EngineRectangle bottomGray = new EngineRectangle(new EnginePoint(rectWidth * 5, rectHeight * 5), new EngineSize(rectHeight, rectWidth));
			this.DrawRectangle(topGray, ConsoleColor.Gray);
			this.GetRectangeColors(topGray, colors);
			this.DrawRectangle(bottomGray, ConsoleColor.Gray);
			this.GetRectangeColors(bottomGray, colors);

			EngineRectangle topWhite = new EngineRectangle(new EnginePoint(rectWidth * 15,0),new EngineSize(rectHeight,rectWidth));
			EngineRectangle bottomWhite = new EngineRectangle(new EnginePoint(rectWidth * 4, rectHeight * 5), new EngineSize(rectHeight, rectWidth));
			this.DrawRectangle(topWhite, ConsoleColor.White);
			this.GetRectangeColors(topWhite, colors);
			this.DrawRectangle(bottomWhite, ConsoleColor.White);
			this.GetRectangeColors(bottomWhite, colors);


			EngineRectangle green = new EngineRectangle(new EnginePoint(0,0),new EngineSize(rectHeight * 6 ,rectWidth * 4));
			EngineRectangle green2 = new EngineRectangle(new EnginePoint(rectWidth * 16, 0), new EngineSize(rectHeight * 6, rectWidth * 4));
			this.DrawRectangle(green, ConsoleColor.DarkGreen);
			this.GetRectangeColors(green, colors);
			this.DrawRectangle(green2, ConsoleColor.DarkGreen);
			this.GetRectangeColors(green2, colors);
		}

		public void DrawRectangle(EngineRectangle er, ConsoleColor color)
		{
			lock (Syncronization.SyncObj)
			{
				Console.BackgroundColor = color;
			}

			for (int a = (int) er.Y; a < er.Y + er.Height; a++)
			{
				lock (Syncronization.SyncObj)
				{
					Console.SetCursorPosition((int) er.X, a);
					for (int b = (int) er.X; b < er.X + er.Width; b++)
					{
						Console.Write(" ");
					}
				}
			}
			
		}

		public void GetRectangeColors(EngineRectangle er, ConsoleColor[][] colors)
		{
			for (int a = (int) er.Y; a < (int) (er.Y + er.Height); a++)
			{
				for (int b = (int) er.X; b < (int) (er.X + er.Width); b++)
				{
					lock (Syncronization.SyncObj)
					{
						colors[a][b] = Console.BackgroundColor;
					}
				}
			}
		}

		public void DrawAfterMoving(EngineRectangle er, int offset, ConsoleColor[][] colors)
		{
			switch (er.Direction)
			{
				case Directions.Up:
					for (int a = (int) er.X; a < (int) (er.X + er.Width); a++)
					{
						lock (Syncronization.SyncObj)
						{
							Console.SetCursorPosition(a, (int) (er.Y + er.Height));
							Console.BackgroundColor = colors[Console.CursorTop][Console.CursorLeft];
							Console.Write(" ");
						}
					}
					break;
				case Directions.Down:
					for (int a = (int) er.X; a < (int) (er.X + er.Width); a++)
					{
						lock (Syncronization.SyncObj)
						{
							Console.SetCursorPosition(a, (int) (er.Y - 1));
							Console.BackgroundColor = colors[Console.CursorTop][Console.CursorLeft];
							Console.Write(" ");
						}
					}
					break;
				case Directions.Left:
					for (int a = (int) er.Y; a < (int) (er.Y + er.Height); a++)
					{
						lock (Syncronization.SyncObj)
						{
							Console.SetCursorPosition((int) (er.X + er.Width), a);
							Console.BackgroundColor = colors[Console.CursorTop][Console.CursorLeft];
							Console.Write(" ");
						}
					}
					break;
				case Directions.Right:
					for (int a = (int) er.Y; a < (int) (er.Y + er.Height); a++)
					{
						lock (Syncronization.SyncObj)
						{
							Console.SetCursorPosition((int) (er.X - 1), a);
							Console.BackgroundColor = colors[Console.CursorTop][Console.CursorLeft];
							Console.Write(" ");
						}
					}
					break;
			}
		}

		public void Redraw(EngineRectangle er, ConsoleColor[][] colors)
		{
			for (int a = (int) er.Y; a < (int) (er.Y + er.Height); a++)
			{
				for (int b = (int) er.X; b < (int) (er.X + er.Width); b++)
				{
					lock (Syncronization.SyncObj)
					{
						Console.SetCursorPosition(b, a);
						Console.BackgroundColor = colors[Console.CursorTop][Console.CursorLeft];
						Console.Write(" ");
					}
				}
			}
		}

		#endregion
	}
}
