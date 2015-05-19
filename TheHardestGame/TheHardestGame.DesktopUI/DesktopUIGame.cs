using System;
using System.Drawing;
using System.Windows.Forms;

using TheHardestGame.GameEngine.AdditionalClasses;
using TheHardestGame.GameEngine.Enums;
using TheHardestGame.GameEngine.Events;
using TheHardestGame.GameEngine.Game;


namespace TheHardestGame.DesktopUI
{
	internal class DesktopUIGame
	{
		/*private EngineLogic _el;
		private DesktopDrawing _dd;

		private double _engineToDesktopMultiplier;
		
		private EngineRectangle _square;
		private EngineRectangle[] _barriers;

		private Timer _timer;

		private Graphics _graphics;
		private PictureBox _pb;

		public int Deaths { get; private set; }


		public void TransformEngineRectangle(ref EngineRectangle er)
		{
			er = new EngineRectangle(er.X / _engineToDesktopMultiplier, er.Y / _engineToDesktopMultiplier, er.Height / _engineToDesktopMultiplier, er.Width / _engineToDesktopMultiplier, er.IsBarrier, er.Direction);
		}

		public void TransformDesktopRectangle(ref EngineRectangle er)
		{
			er = new EngineRectangle(er.X * _engineToDesktopMultiplier, er.Y * _engineToDesktopMultiplier, er.Height * _engineToDesktopMultiplier, er.Width * _engineToDesktopMultiplier, er.IsBarrier, er.Direction);
		}


		public void StartGame(Graphics graphics, PictureBox pb, int areaHeight, int areaWidth)
		{
			_graphics = graphics;
			_pb = pb;

			Deaths = 0;

			_el = new EngineLogic();
			RegisterEvents();
			_engineToDesktopMultiplier = Math.Max(_el.Width/areaWidth, _el.Height/areaHeight);
			_square = _el.GetStartSquare();
			_barriers = _el.GetStartBarriers();

			_dd = new DesktopDrawing();
			_dd.DrawLevelBackground(_graphics, _pb, areaHeight, areaWidth, (_el.RectHeight / _engineToDesktopMultiplier),
				(_el.RectWidth / _engineToDesktopMultiplier));

			TransformEngineRectangle(ref _square);
			for (int a = 0; a < _barriers.Length; a++)
			{
				TransformEngineRectangle(ref _barriers[a]);
				_dd.DrawRectangle(_graphics, _pb, _barriers[a], Brushes.Blue, true);
			}

			_dd.DrawRectangle(_graphics, _pb, _square, Brushes.Red);

			_timer = new Timer();
			_timer.Interval = 50;
			_timer.Tick += ((sender, e) =>
			{
				for (int a = 0; a < _barriers.Length; a++)
				{
					if (a % 2 == 0)
					{
						MoveRectangle(ref _barriers[a], Brushes.Blue, Directions.Right, _pb.Width * 0.005);
					}
					else
					{
						MoveRectangle(ref _barriers[a], Brushes.Blue, Directions.Left, _pb.Width * 0.005);
					}
				}
				_timer.Interval = 50;
				_el.CheckDeath(_square, _barriers);
			});

			_timer.Start();
		}

		public void MoveRectangle(ref EngineRectangle er, Brush brush, Directions direction, double offset)
		{
			TransformDesktopRectangle(ref er);
			if (!er.IsBarrier)
			{
				er.Direction = direction;
			}
			bool moved = _el.MoveRectangle(ref er, offset * _engineToDesktopMultiplier);
			TransformEngineRectangle(ref er);
			if (moved)
			{
				//_dd.DrawAfterMoving(er, 1, _colors);
			}
			_dd.DrawRectangle(_graphics, _pb, er, brush);
		}

		private void RegisterEvents()
		{
			_el.EventFinish += FinishRaise;
			_el.EventDie += DieRaise;

			MainWindow.EventMove += MoveRaise;
		}

		private void UnregisterEvents()
		{
			_el.EventFinish -= FinishRaise;
			_el.EventDie -= DieRaise;

			MainWindow.EventMove -= MoveRaise;
		}

		private void FinishRaise(Object sender, FinishEventArgs e)
		{
			if (e.Finish)
			{
				_timer.Stop();
				MessageBox.Show("CONGRATULATION!!! You have finished level", "Finished", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				UnregisterEvents();
			}
		}

		private void DieRaise(Object sender, DieEventArgs e)
		{
			if (e.Die)
			{
				Deaths++;
				//_dd.Redraw(_square, _colors);
				_square = _el.GetStartSquare();
				TransformEngineRectangle(ref _square);
				_dd.DrawRectangle(_graphics, _pb, _square, Brushes.Red);
			}
		}

		private void MoveRaise(Object sender, MoveEventArgs e)
		{
			MoveRectangle(ref _square ,Brushes.Red, e.Direction, _pb.Width * 0.0075);
		}

		public void Renew(Graphics graphics, int areaHeight, int areaWidth)
		{
			_graphics = graphics;
			TransformDesktopRectangle(ref _square);
			for (int a = 0; a < _barriers.Length; a++)
			{
				TransformDesktopRectangle(ref _barriers[a]);
			}

			_engineToDesktopMultiplier = Math.Max(_el.Width / areaWidth, _el.Height / areaHeight);

			_dd.DrawLevelBackground(_graphics, _pb, areaHeight, areaWidth, (_el.RectHeight / _engineToDesktopMultiplier),
				(_el.RectWidth / _engineToDesktopMultiplier));
			

			TransformEngineRectangle(ref _square);
			for (int a = 0; a < _barriers.Length; a++)
			{
				TransformEngineRectangle(ref _barriers[a]);
				_dd.DrawRectangle(_graphics, _pb, _barriers[a], Brushes.Blue, true);
			}

			_dd.DrawRectangle(_graphics, _pb, _square, Brushes.Red);

		}*/
	}
}