using System;
using System.Drawing;
using System.Windows.Forms;

using TheHardestGame.GameEngine.AdditionalClasses;

namespace TheHardestGame.DesktopUI
{
	internal class DesktopDrawing
	{

		public void DrawLevelBackground(Graphics graphics, PictureBox pb, int areaHeight, int areaWidth, double rectHeight, double rectWidth)
		{
			graphics.FillRectangle(Brushes.Cyan, new Rectangle(0, 0, areaWidth, areaHeight));
			for (int a = 1; a < 5; a++)
			{
				for (int b = 5; b < 15; b++)
				{
					EngineRectangle er = new EngineRectangle(b * rectWidth, a * rectHeight, rectHeight, rectWidth);
					if ((a + b)%2 == 0)
					{
						DrawRectangle(graphics, pb, er, Brushes.Gray);
					}
					else
					{
						DrawRectangle(graphics, pb, er, Brushes.White);
					}
				}
			}

			EngineRectangle topGray = new EngineRectangle(new EnginePoint(rectWidth * 14, 0), new EngineSize(rectHeight, rectWidth));
			EngineRectangle bottomGray = new EngineRectangle(new EnginePoint(rectWidth * 5, rectHeight * 5), new EngineSize(rectHeight, rectWidth));
			DrawRectangle(graphics, pb, topGray, Brushes.Gray);
			DrawRectangle(graphics, pb, bottomGray, Brushes.Gray);

			EngineRectangle topWhite = new EngineRectangle(new EnginePoint(rectWidth * 15, 0), new EngineSize(rectHeight, rectWidth));
			EngineRectangle bottomWhite = new EngineRectangle(new EnginePoint(rectWidth * 4, rectHeight * 5), new EngineSize(rectHeight, rectWidth));
			DrawRectangle(graphics, pb, topWhite, Brushes.White);
			DrawRectangle(graphics, pb, bottomWhite, Brushes.White);

			EngineRectangle green = new EngineRectangle(new EnginePoint(0, 0), new EngineSize(rectHeight * 6, rectWidth * 4));
			EngineRectangle green2 = new EngineRectangle(new EnginePoint(rectWidth * 16, 0), new EngineSize(rectHeight * 6, rectWidth * 4));
			DrawRectangle(graphics, pb, green, Brushes.DarkGreen);
			DrawRectangle(graphics, pb, green2, Brushes.DarkGreen);

			pb.Invalidate();
		}

		public void DrawRectangle(Graphics graphics, PictureBox pb, EngineRectangle er, Brush brush, bool ellipse = false)
		{
			if (!ellipse)
			{
				graphics.FillRectangle(brush, TransformToRectangleF(er));
			}
			else
			{
				graphics.FillEllipse(brush, TransformToRectangleF(er));
			}
			//pb.Invalidate(new Rectangle((int)Math.Floor(er.X), (int)Math.Floor(er.Y), (int)Math.Ceiling(er.Width), (int)Math.Ceiling(er.Height)));
			pb.Invalidate();
		}


		private RectangleF TransformToRectangleF(EngineRectangle er)
		{
			return new RectangleF(Convert.ToSingle(er.X), Convert.ToSingle(er.Y), Convert.ToSingle(er.Width), Convert.ToSingle(er.Height));
		}


		//public void DrawAfterMoving(EngineRectangle er, int offset, ConsoleColor[][] colors)
		//{
		//	switch (er.Direction)
		//	{
		//		case 0:
		//			for (int a = (int)er.X; a < (int)(er.X + er.Width); a++)
		//			{
		//				Console.SetCursorPosition(a, (int)(er.Y + er.Height));
		//				Console.BackgroundColor = colors[Console.CursorTop][Console.CursorLeft];
		//				Console.Write(" ");
		//			}
		//			break;
		//		case 1:
		//			for (int a = (int)er.X; a < (int)(er.X + er.Width); a++)
		//			{
		//				Console.SetCursorPosition(a, (int)(er.Y - 1));
		//				Console.BackgroundColor = colors[Console.CursorTop][Console.CursorLeft];
		//				Console.Write(" ");
		//			}
		//			break;
		//		case 2:
		//			for (int a = (int)er.Y; a < (int)(er.Y + er.Height); a++)
		//			{
		//				Console.SetCursorPosition((int)(er.X + er.Width), a);
		//				Console.BackgroundColor = colors[Console.CursorTop][Console.CursorLeft];
		//				Console.Write(" ");
		//			}
		//			break;
		//		case 3:
		//			for (int a = (int)er.Y; a < (int)(er.Y + er.Height); a++)
		//			{
		//				Console.SetCursorPosition((int)(er.X - 1), a);
		//				Console.BackgroundColor = colors[Console.CursorTop][Console.CursorLeft];
		//				Console.Write(" ");
		//			}
		//			break;
		//	}
		//}

		//public void Redraw(EngineRectangle er, ConsoleColor[][] colors)
		//{
		//	for (int a = (int)er.Y; a < (int)(er.Y + er.Height); a++)
		//	{
		//		for (int b = (int)er.X; b < (int)(er.X + er.Width); b++)
		//		{
		//			Console.SetCursorPosition(b, a);
		//			Console.BackgroundColor = colors[Console.CursorTop][Console.CursorLeft];
		//			Console.Write(" ");
		//		}
		//	}
		//}

	}
}
