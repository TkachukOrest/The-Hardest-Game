using System;
using System.Threading;
using TheHardestGame.GameEngine.Events;
using TheHardestGame.GameEngine.AdditionalClasses;
using TheHardestGame.GameEngine.Enums;

namespace TheHardestGame.GameEngine.Game
{
	public class EngineGame
	{
		#region Fields

		public double Height { get; private set; }
		public double Width { get; private set; }

		public double RectHeight { get; private set; }
		public double RectWidth { get; private set; }

		private readonly EngineRectangle[] _forbidenZones;

		public States State { get; private set; }

		#endregion

		#region Events

		public event EventHandler<FinishEventArgs> EventFinish;
		public event EventHandler<DieEventArgs> EventDie;

		#endregion

		#region Constructors

		public EngineGame(double precision = 10000.0)
		{
			this.Width = precision;
			this.Height = this.Width / 20 * 6;

			this.RectWidth = this.Width / 20;
			this.RectHeight = this.Height / 6;

			this._forbidenZones = this.GetForbiddenZones();

			this.State = States.Running;
		}

		#endregion

		#region Methods

		private EngineRectangle[] GetForbiddenZones()
		{
			EngineRectangle[] zones = new EngineRectangle[4];
			zones[0] = new EngineRectangle(new EnginePoint(this.RectWidth * 4, 0), new EngineSize(this.RectHeight * 5, this.RectWidth));
			zones[1] = new EngineRectangle(new EnginePoint(this.RectWidth * 15, this.RectHeight), new EngineSize(this.RectHeight * 5, this.RectWidth));
			zones[2] = new EngineRectangle(new EnginePoint(this.RectWidth * 5, 0), new EngineSize(this.RectHeight, this.RectWidth * 9));
			zones[3] = new EngineRectangle(new EnginePoint(this.RectWidth * 6, this.RectHeight * 5), new EngineSize(this.RectHeight, this.RectWidth * 9));

			return zones;
		}

		public bool MoveRectangle(ref EngineRectangle er,double offset, bool barrier = false)
		{
			if (this.CanMoveRectangle(er, offset))
			{
				er.Move(offset);
				if (er.X >= this.RectWidth * 16)
				{
					this.FinishRaise(true);
				}
				else
				{
					this.FinishRaise(false);
				}

				return true;
			}

			else if ((er.IsBarrier) && (er.Direction == Directions.Left))
			{
				er.Direction = Directions.Right;
				return this.MoveRectangle(ref er, offset, er.IsBarrier);
			}

			else if ((er.IsBarrier) && (er.Direction == Directions.Right))
			{
				er.Direction = Directions.Left;
				return this.MoveRectangle(ref er, offset, er.IsBarrier);
			}

			return false;
		}

		private bool CanMoveRectangle(EngineRectangle er, double offset)
		{
			EngineRectangle erTemp = er;
			switch (er.Direction)
			{
				case Directions.Up:
					erTemp = new EngineRectangle(new EnginePoint(er.X, er.Y - offset), er.Size);
					break;
				case Directions.Down:
					erTemp = new EngineRectangle(new EnginePoint(er.X, er.Y + offset), er.Size);
					break;
				case Directions.Left:
					erTemp = new EngineRectangle(new EnginePoint(er.X - offset, er.Y), er.Size);
					break;
				case Directions.Right:
					erTemp = new EngineRectangle(new EnginePoint(er.X + offset, er.Y), er.Size);
					break;

			}

			if ((erTemp.X < 0) || (erTemp.Y < 0))
			{
				return false;
			}
			else if (((erTemp.X + erTemp.Width) > this.Width) || ((erTemp.Y + erTemp.Height) > this.Height))
			{
				return false;
			}

			if (this.Overlap(erTemp, this._forbidenZones))
			{
				return false;
			}

			return true;
		}

		public bool Overlap(EngineRectangle er, EngineRectangle[] forbiden)
		{
			EnginePoint[] edges = new EnginePoint[4];
			edges[0] = new EnginePoint(er.X, er.Y);
			edges[1] = new EnginePoint(er.X + er.Width - 1, er.Y);
			edges[2] = new EnginePoint(er.X, er.Y + er.Height - 1);
			edges[3] = new EnginePoint(er.X + er.Width - 1, er.Y + er.Height - 1);

			for (int a = 0; a < forbiden.Length; a++)
			{
				for (int b = 0; b < edges.Length; b++)
				{
					if ((edges[b].X >= forbiden[a].X) && (edges[b].X < (forbiden[a].X + forbiden[a].Width)) && (edges[b].Y >= forbiden[a].Y) && (edges[b].Y < (forbiden[a].Y + forbiden[a].Height)))
					{
						return true;
					}
				}
			}

			return false;
		}


		public EngineRectangle GetStartSquare()
		{
			return new EngineRectangle(new EnginePoint(this.RectWidth * 1.5, this.RectHeight * 2.5), new EngineSize(this.RectHeight * 0.8, this.RectWidth * 0.8));
		}

		public EngineRectangle[] GetStartBarriers()
		{
			EngineRectangle[] barriers = new EngineRectangle[4];
			barriers[0] = new EngineRectangle(new EnginePoint(this.RectWidth * 5 /*+ RectWidth * 0.25*/, this.RectHeight + this.RectHeight * 0.25), new EngineSize(this.RectHeight * 0.5, this.RectWidth * 0.5), Directions.Right, true);
			barriers[1] = new EngineRectangle(new EnginePoint(this.RectWidth * 14 + this.RectWidth * 0.5, this.RectHeight * 2 + this.RectHeight * 0.25), new EngineSize(this.RectHeight * 0.5, this.RectWidth * 0.5), Directions.Left, true);
			barriers[2] = new EngineRectangle(new EnginePoint(this.RectWidth * 5 /*+ RectWidth * 0.25*/, this.RectHeight * 3 + this.RectHeight * 0.25), new EngineSize(this.RectHeight * 0.5, this.RectWidth * 0.5), Directions.Right, true);
			barriers[3] = new EngineRectangle(new EnginePoint(this.RectWidth * 14 + this.RectWidth * 0.5, this.RectHeight * 4 + this.RectHeight * 0.25), new EngineSize(this.RectHeight * 0.5, this.RectWidth * 0.5), Directions.Left, true);

			return barriers;
		}

		public void CheckDeath(EngineRectangle er, EngineRectangle[] barriers)
		{
			EnginePoint[] points = new EnginePoint[barriers.Length];
			
			for (int a = 0; a < points.Length; a++)
			{
				points[a] = new EnginePoint(barriers[a].X + barriers[a].Width / 2, barriers[a].Y + barriers[a].Height / 2);

				for (double b = er.X; b < er.X + er.Width; b += 0.5)
				{
					if ((points[a].GetDistance(b, er.Y) <= barriers[a].Height / 2) ||
						(points[a].GetDistance(b, er.Y + er.Height - 1) <= barriers[a].Height / 2))
					{
						this.DieRaise(true);
						return;
					}
				}

				for (double b = er.Y; b < er.Y + er.Height; b += 0.5)
				{
					if ((points[a].GetDistance(er.X, b) <= barriers[a].Height / 2) ||
						(points[a].GetDistance(er.X + er.Width - 1, b) <= barriers[a].Height / 2))
					{
						this.DieRaise(true);
						return;
					}
				}
			}

			this.DieRaise(false);
		}

		#endregion

		#region Event Methods

		private void OnFinish(FinishEventArgs e)
		{
			EventHandler<FinishEventArgs> temp = Volatile.Read(ref this.EventFinish);

			if (temp != null)
			{
				temp(this, e);
			}
		}

		private void OnDie(DieEventArgs e)
		{
			EventHandler<DieEventArgs> temp = Volatile.Read(ref this.EventDie);

			if (temp != null)
			{
				temp(this, e);
			}
		}
		

		private void FinishRaise(bool finish)
		{
			FinishEventArgs e = new FinishEventArgs(finish);
			this.State = States.Stopped;
			this.OnFinish(e);
		}

		private void DieRaise(bool die)
		{
			DieEventArgs e = new DieEventArgs(die);

			this.OnDie(e);
		}

		#endregion
	}
}
