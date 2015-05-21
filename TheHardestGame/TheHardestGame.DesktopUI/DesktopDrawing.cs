using System;
using System.Drawing;
using System.Windows.Forms;
using TheHardestGame.GameEngine.AdditionalClasses;
using TheHardestGame.GameEngine.Enums;

namespace TheHardestGame.DesktopUI
{
	internal class DesktopDrawing
	{
		private Color[][] _colors;

		public void DrawLevelBackground(Graphics graphics, PictureBox pb, double rectHeight, double rectWidth,
			double horizontalOffset, double verticalOffset)
		{
			graphics.FillRectangle(Brushes.Cyan, new Rectangle(0, 0, pb.Width, pb.Height));
			for (int a = 1; a < 5; a++)
			{
				for (int b = 5; b < 15; b++)
				{
					EngineRectangle er = new EngineRectangle(b*rectWidth + horizontalOffset, a*rectHeight + verticalOffset, rectHeight,
						rectWidth);
					if ((a + b)%2 == 0)
					{
						this.DrawRectangle(graphics, pb, er, Brushes.Gray);
					}
					else
					{
						this.DrawRectangle(graphics, pb, er, Brushes.White);
					}
				}
			}

			EngineRectangle topGray = new EngineRectangle(new EnginePoint(rectWidth*14 + horizontalOffset, 0 + verticalOffset),
				new EngineSize(rectHeight, rectWidth));
			EngineRectangle bottomGray =
				new EngineRectangle(new EnginePoint(rectWidth*5 + horizontalOffset, rectHeight*5 + verticalOffset),
					new EngineSize(rectHeight, rectWidth));
			this.DrawRectangle(graphics, pb, topGray, Brushes.Gray);
			this.DrawRectangle(graphics, pb, bottomGray, Brushes.Gray);

			EngineRectangle topWhite = new EngineRectangle(new EnginePoint(rectWidth*15 + horizontalOffset, 0 + verticalOffset),
				new EngineSize(rectHeight, rectWidth));
			EngineRectangle bottomWhite =
				new EngineRectangle(new EnginePoint(rectWidth*4 + horizontalOffset, rectHeight*5 + verticalOffset),
					new EngineSize(rectHeight, rectWidth));
			this.DrawRectangle(graphics, pb, topWhite, Brushes.White);
			this.DrawRectangle(graphics, pb, bottomWhite, Brushes.White);

			EngineRectangle green = new EngineRectangle(new EnginePoint(0 + horizontalOffset, 0 + verticalOffset),
				new EngineSize(rectHeight*6, rectWidth*4));
			EngineRectangle green2 = new EngineRectangle(new EnginePoint(rectWidth*16 + horizontalOffset, 0 + verticalOffset),
				new EngineSize(rectHeight*6, rectWidth*4));
			this.DrawRectangle(graphics, pb, green, Brushes.DarkGreen);
			this.DrawRectangle(graphics, pb, green2, Brushes.DarkGreen);

			this.GetColors(pb);
            //Peer Review: перед else зайвий пропуск
			if (pb.InvokeRequired)
			{
				Action refresh = new Action(pb.Invalidate);
				pb.Invoke(refresh);
			}

			else
			{
				pb.Invalidate();
			}
		}

		private void GetColors(PictureBox pb)
		{
			this._colors = new Color[pb.Height][];
			for (int a = 0; a < this._colors.Length; a++)
			{
				this._colors[a] = new Color[pb.Width];
			}

			Bitmap bmp = pb.Image as Bitmap;

			for (int a = 0; a < pb.Height; a++)
			{
				for (int b = 0; b < pb.Width; b++)
				{
					this._colors[a][b] = bmp.GetPixel(b, a);
				}
			}
		}

		public void DrawRectangle(Graphics graphics, PictureBox pb, EngineRectangle er, Brush brush)
		{
			if (!er.IsBarrier)
			{
				graphics.FillRectangle(brush, this.TransformToRectangleF(er));
			}
			else
			{
				graphics.FillEllipse(brush, this.TransformToRectangleF(er));
			}

			if (pb.InvokeRequired)
			{
				Action refresh = new Action(pb.Invalidate);
				pb.Invoke(refresh);
			}

			else
			{
				pb.Invalidate();
			}

			//if (pb.InvokeRequired)
			//{
			//	Action<Rectangle> refresh = new Action<Rectangle>(pb.Invalidate);
			//	pb.Invoke(refresh, new Rectangle((int) Math.Floor(er.X), (int) Math.Floor(er.Y), (int) Math.Ceiling(er.Width),
			//		(int) Math.Ceiling(er.Height)));
			//}
			//else
			//{
			//	pb.Invalidate(new Rectangle((int) Math.Floor(er.X), (int) Math.Floor(er.Y), (int) Math.Ceiling(er.Width),
			//		(int) Math.Ceiling(er.Height)));
			//}
		}


		private RectangleF TransformToRectangleF(EngineRectangle er)
		{
			return new RectangleF(Convert.ToSingle(er.X), Convert.ToSingle(er.Y), Convert.ToSingle(er.Width),
				Convert.ToSingle(er.Height));
		}


		public void DrawAfterMoving(PictureBox pb, EngineRectangle er) ////////////////////////////////
		{
			Bitmap bmp = pb.Image as Bitmap;

			Rectangle rectangle = new Rectangle(-1, -1, 0, 0);

			double offset = er.LastOffset;

			switch (er.Direction)
			{
				case Directions.Up:
					rectangle = new Rectangle((int) (Math.Floor(er.X)), (int) (Math.Floor(er.Y + er.Height)),
						(int) (Math.Ceiling(er.X + er.Width) - Math.Floor(er.X)),
						(int) (Math.Ceiling(er.Y + er.Height + offset) - Math.Floor(er.Y + er.Height)));
					for (int a = (int) (Math.Floor(er.Y + er.Height)); a < (int) (Math.Ceiling(er.Y + er.Height + offset)); a++)
					{
						for (int b = (int) (Math.Floor(er.X)); b < (int) (Math.Ceiling(er.X + er.Width)); b++)
						{
							bmp.SetPixel(b, a, this._colors[a][b]);
						}
					}

					break;

				case Directions.Down:
					rectangle = new Rectangle((int) (Math.Floor(er.X)), (int) (Math.Floor(er.Y - offset)),
						(int) (Math.Ceiling(er.X + er.Width) - Math.Floor(er.X)),
						(int) (Math.Round(er.Y, MidpointRounding.AwayFromZero) - Math.Floor(er.Y - offset)));
					for (int a = (int) (Math.Floor(er.Y - offset)); a < (int) (Math.Ceiling(er.Y)); a++)
					{
						for (int b = (int) (Math.Floor(er.X)); b < (int) (Math.Ceiling(er.X + er.Width)); b++)
						{
							bmp.SetPixel(b, a, this._colors[a][b]);
						}
					}

					break;

				case Directions.Left:
					rectangle = new Rectangle((int) (Math.Floor(er.X + er.Width - er.Width/2)), (int) (Math.Floor(er.Y)),
						(int) (Math.Ceiling(er.X + er.Width + offset) - Math.Floor(er.X + er.Width - er.Width/2)),
						(int) (Math.Ceiling(er.Y + er.Height) - Math.Floor(er.Y)));
					for (int a = (int) (Math.Floor(er.Y)); a < (int) (Math.Ceiling(er.Y + er.Height)); a++)
					{
						for (int b = (int) (Math.Floor(er.X + er.Width - er.Width/2));
							b < (int) (Math.Ceiling(er.X + er.Width + offset));
							b++)
						{
							bmp.SetPixel(b, a, this._colors[a][b]);
						}
					}

					break;

				case Directions.Right:
					rectangle = new Rectangle((int) (Math.Floor(er.X - offset)), (int) (Math.Floor(er.Y)),
						(int) (Math.Ceiling(er.Width/2)), (int) (Math.Ceiling(er.Y + er.Height) - Math.Floor(er.Y)));
					for (int a = (int) (Math.Floor(er.Y)); a < (int) (Math.Ceiling(er.Y + er.Height)); a++)
					{
						for (int b = (int) (Math.Floor(er.X - offset)); b < (int) (Math.Ceiling(er.X + er.Width/2)); b++)
						{
							bmp.SetPixel(b, a, this._colors[a][b]);
						}
					}

					break;
			}
            //Peer Review: перед else зайвий пропуск
			if (pb.InvokeRequired)
			{
				Action refresh = new Action(pb.Invalidate);
				pb.Invoke(refresh);
			}

			else
			{
				pb.Invalidate();
			}
		}

		public void Redraw(PictureBox pb, EngineRectangle er)
		{
			Bitmap bmp = pb.Image as Bitmap;

			for (int a = (int) (Math.Floor(er.Y)); a < (int) (Math.Ceiling(er.Y + er.Height)); a++)
			{
				for (int b = (int) (Math.Floor(er.X)); b < (int) (Math.Ceiling(er.X + er.Width)); b++)
				{
					bmp.SetPixel(b, a, this._colors[a][b]);
				}
			}

            //Peer Review: перед else зайвий пропуск
			if (pb.InvokeRequired)
			{
				Action refresh = new Action(pb.Invalidate);
				pb.Invoke(refresh);
			}

			else
			{
				pb.Invalidate();
			}
		}
	}
}