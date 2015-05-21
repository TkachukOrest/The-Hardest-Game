using System;
using System.Threading;
using TheHardestGame.GameEngine.AdditionalClasses;
using TheHardestGame.GameEngine.Enums;
using Timer = System.Timers.Timer;

namespace TheHardestGame.GameEngine.Game
{
	public class EngineGame
	{
		#region Constants
        //Pere Review: Володимиром було запропоновано називати всі змінні в цьому проекті за стилем
        //THE_DEFAULTPRECISSION, хоча Решарпер і FxCop рекомендують називати  DefaultPrecision
		private const double DefaultPrecision = 15000.0;

		private const int Horizontal = 20;
		private const int Vertical = 6;

		#endregion

		#region Fields

		public double Height { get; private set; }
		public double Width { get; private set; }

		public double RectHeight { get; private set; }
		public double RectWidth { get; private set; }

		public EngineRectangle Square { get; private set; }
		public EngineRectangle[] Barriers { get; private set; }

		private EngineRectangle[] _forbidenZones;
        //Peer Review: змінна, яка не використовується в цьому класі - необхідно видалити
		public Timer EngineTimer { get; private set; }


		public States State { get; set; }

		public int Deaths { get; set; }

		#endregion

		#region Events
        
		public event EventHandler<bool> EventFinish;
		public event EventHandler<EngineRectangle> EventDie;
		public event EventHandler<EngineRectangle> EventMove;

		#endregion

		#region Constructors

		public EngineGame(double precision = DefaultPrecision)
		{
			this.Width = precision;
			this.Height = this.Width/Horizontal*Vertical;

			this.RectWidth = this.Width/Horizontal;
			this.RectHeight = this.Height/Vertical;
            
			//this.EngineTimer = new Timer(50.0);
			//this.EngineTimer.Elapsed += this.TimerElapsed;
			//this.EngineTimer.AutoReset = true;

			this.GetStartSquare();
			this.GetStartBarriers();
			this.GetForbiddenZones();

			this.State = States.Running;
			this.Deaths = 0;
		}

		#endregion

		#region Methods

		public void GetStartSquare()
		{
			this.Square = new EngineRectangle(new EnginePoint(this.RectWidth*1.5, this.RectHeight*2.5),
				new EngineSize(this.RectHeight*0.8, this.RectWidth*0.8));
		}

		public void GetStartBarriers()
		{
			this.Barriers = new EngineRectangle[4];
			this.Barriers[0] =
				new EngineRectangle(
					new EnginePoint(this.RectWidth*5 + this.RectWidth*0.25, this.RectHeight + this.RectHeight*0.25),
					new EngineSize(this.RectHeight*0.5, this.RectWidth*0.5), 0, Directions.Right, true);
			this.Barriers[1] =
				new EngineRectangle(
					new EnginePoint(this.RectWidth*14 + this.RectWidth*0.25, this.RectHeight*2 + this.RectHeight*0.25),
					new EngineSize(this.RectHeight*0.5, this.RectWidth*0.5), 0, Directions.Left, true);
			this.Barriers[2] =
				new EngineRectangle(
					new EnginePoint(this.RectWidth*5 + this.RectWidth*0.25, this.RectHeight*3 + this.RectHeight*0.25),
					new EngineSize(this.RectHeight*0.5, this.RectWidth*0.5), 0, Directions.Right, true);
			this.Barriers[3] =
				new EngineRectangle(
					new EnginePoint(this.RectWidth*14 + this.RectWidth*0.25, this.RectHeight*4 + this.RectHeight*0.25),
					new EngineSize(this.RectHeight*0.5, this.RectWidth*0.5), 0, Directions.Left, true);
		}

		private void GetForbiddenZones()
		{
			this._forbidenZones = new EngineRectangle[4];
			this._forbidenZones[0] = new EngineRectangle(new EnginePoint(this.RectWidth*4, 0),
				new EngineSize(this.RectHeight*5, this.RectWidth));
			this._forbidenZones[1] = new EngineRectangle(new EnginePoint(this.RectWidth*15, this.RectHeight),
				new EngineSize(this.RectHeight*5, this.RectWidth));
			this._forbidenZones[2] = new EngineRectangle(new EnginePoint(this.RectWidth*5, 0),
				new EngineSize(this.RectHeight, this.RectWidth*9));
			this._forbidenZones[3] = new EngineRectangle(new EnginePoint(this.RectWidth*6, this.RectHeight*5),
				new EngineSize(this.RectHeight, this.RectWidth*9));
		}

		public void MoveRectangle(EngineRectangle er, double offset)
		{
			if (this.ChangeOffsetMoveRectangle(er, ref offset))
			{
				er.Move(offset);
				er.LastOffset = offset;
				if (this.IsDied(this.Square, this.Barriers))
				{
					EngineRectangle err = new EngineRectangle(this.Square);
					this.GetStartSquare();
					//this.MoveRaise(er);
					this.DieRaise(err);
				}
				else
				{
					this.MoveRaise(er);

					if (er.X >= this.RectWidth*16)
					{
						this.FinishRaise(true);
					}
					else
					{
						this.FinishRaise(false);
					}
				}
			}

			else if ((er.IsBarrier) && (er.Direction == Directions.Left))
			{
				er.Direction = Directions.Right;
				this.MoveRectangle(er, offset);
			}

			else if ((er.IsBarrier) && (er.Direction == Directions.Right))
			{
				er.Direction = Directions.Left;
				this.MoveRectangle(er, offset);
			}
		}

		private bool ChangeOffsetMoveRectangle(EngineRectangle er, ref double offset)
		{
			EngineRectangle erTemp = er;
			switch (er.Direction)
			{
				case Directions.Up:
					if (er.Y > offset)
					{
						erTemp = new EngineRectangle(new EnginePoint(er.X, er.Y - offset), er.Size);
					}

					else
					{
						erTemp = new EngineRectangle(new EnginePoint(er.X, 0), er.Size);
						offset = er.Y;
					}

					break;
				case Directions.Down:
					if ((er.Y + er.Height + offset) < this.Height)
					{
						erTemp = new EngineRectangle(new EnginePoint(er.X, er.Y + offset), er.Size);
					}

					else
					{
						erTemp = new EngineRectangle(new EnginePoint(er.X, this.Height - er.Height), er.Size);
						offset = Math.Abs(er.Y - erTemp.Y);
					}
					break;
				case Directions.Left:
					if (er.X > offset)
					{
						erTemp = new EngineRectangle(new EnginePoint(er.X - offset, er.Y), er.Size);
					}

					else
					{
						erTemp = new EngineRectangle(new EnginePoint(0, er.Y), er.Size);
						offset = er.X;
					}
					break;
				case Directions.Right:
					if ((er.X + er.Width + offset) < this.Width)
					{
						erTemp = new EngineRectangle(new EnginePoint(er.X + offset, er.Y), er.Size);
					}

					else
					{
						erTemp = new EngineRectangle(new EnginePoint(this.Width - er.Width, er.Y), er.Size);
						offset = Math.Abs(er.X - erTemp.X);
					}
					break;
			}

			if (this.ChangeOffsetOverlap(er, erTemp, ref offset))
			{
				return false;
			}

			return true;
		}

		private bool ChangeOffsetOverlap(EngineRectangle erPrev, EngineRectangle erNew, ref double offset
			/*, EngineRectangle[] forbiden*/)
		{
			EnginePoint[] edges = new EnginePoint[4];
			edges[0] = new EnginePoint(erNew.X, erNew.Y);
			edges[1] = new EnginePoint(erNew.X + erNew.Width - 1, erNew.Y);
			edges[2] = new EnginePoint(erNew.X, erNew.Y + erNew.Height - 1);
			edges[3] = new EnginePoint(erNew.X + erNew.Width - 1, erNew.Y + erNew.Height - 1);

			for (int a = 0; a < this._forbidenZones.Length; a++)
			{
				for (int b = 0; b < edges.Length; b++)
				{
					if ((edges[b].X >= this._forbidenZones[a].X) &&
					    (edges[b].X < (this._forbidenZones[a].X + this._forbidenZones[a].Width)) &&
					    (edges[b].Y >= this._forbidenZones[a].Y) &&
					    (edges[b].Y < (this._forbidenZones[a].Y + this._forbidenZones[a].Height)))
					{
						bool overlap = false;

						switch (erPrev.Direction)
						{
							case Directions.Up:
								if ((this._forbidenZones[a].Y + this._forbidenZones[a].Height) < erPrev.Y)
								{
									offset = erPrev.Y - this._forbidenZones[a].Y - this._forbidenZones[a].Height;
								}
								else
								{
									overlap = true;
								}

								break;
							case Directions.Down:
								if (this._forbidenZones[a].Y > (erPrev.Y + erPrev.Height))
								{
									offset = this._forbidenZones[a].Y - erPrev.Y - erPrev.Height;
								}
								else
								{
									overlap = true;
								}

								break;
							case Directions.Left:
								if ((this._forbidenZones[a].X + this._forbidenZones[a].Width) < erPrev.X)
								{
									offset = erPrev.X - this._forbidenZones[a].X - this._forbidenZones[a].Width;
								}
								else
								{
									overlap = true;
								}

								break;
							case Directions.Right:
								if (this._forbidenZones[a].X > (erPrev.X + erPrev.Width))
								{
									offset = this._forbidenZones[a].X - erPrev.X - erPrev.Width;
								}
								else
								{
									overlap = true;
								}

								break;
						}

						return overlap;
					}
				}
			}

			return false;
		}

		private bool IsDied(EngineRectangle er, EngineRectangle[] barriers)
		{
			EnginePoint[] points = new EnginePoint[barriers.Length];

			for (int a = 0; a < points.Length; a++)
			{
				points[a] = new EnginePoint(barriers[a].X + barriers[a].Width/2, barriers[a].Y + barriers[a].Height/2);

				bool die = false;
				for (double b = er.X; b < er.X + er.Width; b += 0.5)
				{
					if ((points[a].GetDistance(b, er.Y) <= barriers[a].Height/2) ||
					    (points[a].GetDistance(b, er.Y + er.Height - 1) <= barriers[a].Height/2))
					{
						die = true;
						break;
					}
				}
                
				if (!die)
				{
					for (double b = er.Y; b < er.Y + er.Height; b += 0.5)
					{
						if ((points[a].GetDistance(er.X, b) <= barriers[a].Height/2) ||
						    (points[a].GetDistance(er.X + er.Width - 1, b) <= barriers[a].Height/2))
						{
							die = true;
							break;
						}
					}
				}

				if (die)
				{
					return true;
				}
			}

			return false;
		}

		public void Restart()
		{
			this.GetStartSquare();
			this.GetStartBarriers();

			this.Deaths = 0;
		}

		#endregion

		#region Event Methods

		private void OnFinish(bool e)
		{
			EventHandler<bool> temp = Volatile.Read(ref this.EventFinish);

			if (temp != null)
			{
				temp(this, e);
			}
		}

		private void FinishRaise(bool finish)
		{
			this.State = States.Stopped;
			this.OnFinish(finish);
		}

		private void OnDie(EngineRectangle e)
		{
			EventHandler<EngineRectangle> temp = Volatile.Read(ref this.EventDie);

			if (temp != null)
			{
				temp(this, e);
			}
		}

		private void DieRaise(EngineRectangle er)
		{
			this.Deaths++;
			this.OnDie(er);
		}

		private void OnMove(EngineRectangle e)
		{
			EventHandler<EngineRectangle> temp = Volatile.Read(ref this.EventMove);

			if (temp != null)
			{
				temp(this, e);
			}
		}

		private void MoveRaise(EngineRectangle er)
		{
			this.OnMove(er);
		}

		//private void TimerElapsed(object sender, ElapsedEventArgs e)
		//{
		//	for (int a = 0; a < this.Barriers.Length; a++)
		//	{
		//		this.MoveRectangle(this.Barriers[a], this.Width * 0.015);
		//	}
		//}

		#endregion
	}
}