using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using TheHardestGame.GameEngine.AdditionalClasses;
using TheHardestGame.GameEngine.Enums;
using TheHardestGame.GameEngine.Game;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;

namespace TheHardestGame.DesktopUI
{
	internal class DesktopUiGame
	{
		private object _sync = new object();

		private readonly MainWindow _mainWindow;

		private readonly EngineGame _game;
		private readonly DesktopDrawing _dd;

		private double _engineToDesktopMultiplier;

		private Timer _timer;

		private Graphics _graphics;
		private readonly PictureBox _pb;
		private readonly Label _labelDeath;

		private double _horizontalOffset;
		private double _verticalOffset;


		private MediaPlayer _mediaPlayerEvents;
		private readonly MediaPlayer _mediaPlayerMain;

		public DesktopUiGame()
		{
			this.UnpackResources();

			this._mediaPlayerMain = new MediaPlayer();
			this._mediaPlayerMain.Volume = 0.3;
			this._mediaPlayerMain.MediaEnded += ((sender, e) =>
			{
				this._mediaPlayerMain.Position = new TimeSpan(0);
				this._mediaPlayerMain.Play();
			});

			try
			{
				this._mediaPlayerMain.Open(new Uri(Application.StartupPath + "\\Sounds\\Main.mp3"));
			}
			catch (FileNotFoundException e)
			{
				this._mediaPlayerMain = null;
			}
			

			this._game = new EngineGame();

			StartForm startForm = new StartForm();
			startForm.ShowDialog();
			if (startForm.Command == Commands.Exit)
			{
				this._game.State = States.Stopped;
			}
			else
			{
				this._game.State = States.ReadyToStart;
			}

			if (this._game.State == States.ReadyToStart)
			{
				this._mainWindow = new MainWindow();
				this._pb = this._mainWindow.PictureBoxDrawing;
				this._engineToDesktopMultiplier = Math.Max(this._game.Width/this._pb.Width, this._game.Height/this._pb.Height);

				this._horizontalOffset = (this._pb.Width*this._engineToDesktopMultiplier - this._game.Width)/2/
				                         this._engineToDesktopMultiplier;
				this._verticalOffset = (this._pb.Height*this._engineToDesktopMultiplier - this._game.Height)/2/
				                       this._engineToDesktopMultiplier;

				this.GetGraphics();

				this._labelDeath = this._mainWindow.LabelDeathQ;
				this.RegisterEvents();

				this._dd = new DesktopDrawing();
			}
		}

		private void UnpackResources()
		{
			string path = Application.StartupPath + "\\Sounds";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			Byte[] file = Properties.Resources.Die;
			this.WriteFile(file, path + "\\Die.mp3");
			file = Properties.Resources.Finish;
			this.WriteFile(file, path + "\\Finish.mp3");
			file = Properties.Resources.Restart;
			this.WriteFile(file, path + "\\Restart.mp3");
			file = Properties.Resources.Main;
			this.WriteFile(file, path + "\\Main.mp3");
		}

		private void WriteFile(Byte[] data, string path)
		{
			if (!File.Exists(path))
			{
				File.WriteAllBytes(path, data);
			}
		}
		
		private void GetGraphics()
		{
			Bitmap drawArea = new Bitmap(this._pb.Width, this._pb.Height);
			this._pb.Image = drawArea;
			this._graphics = Graphics.FromImage(drawArea);
		}

		public void StartGame()
		{
			if (this._game.State == States.ReadyToStart)
			{
				this._game.State = States.Running;

				this._dd.DrawLevelBackground(this._graphics, this._pb, (this._game.RectHeight/this._engineToDesktopMultiplier),
					(this._game.RectWidth/this._engineToDesktopMultiplier), this._horizontalOffset, this._verticalOffset);

				this._dd.DrawRectangle(this._graphics, this._pb, this.TransformEngineRectangle(this._game.Square), Brushes.Red);

				for (int a = 0; a < this._game.Barriers.Length; a++)
				{
					this._dd.DrawRectangle(this._graphics, this._pb, this.TransformEngineRectangle(this._game.Barriers[a]),
						Brushes.Blue);
				}

				this._timer = new Timer();
				this._timer.Interval = 50;
				this._timer.Tick += ((sender, e) =>
				{
					double offset = Math.Floor(this._pb.Width*0.015)*this._engineToDesktopMultiplier;
					double copyOffset = offset/this._engineToDesktopMultiplier;
					for (int a = 0; a < this._game.Barriers.Length; a++)
					{
						this._game.MoveRectangle(this._game.Barriers[a], offset);
						this._dd.DrawAfterMoving(this._pb, this.TransformEngineRectangle(this._game.Barriers[a]));
						this._dd.DrawRectangle(this._graphics, this._pb, this.TransformEngineRectangle(this._game.Barriers[a]),
							Brushes.Blue);
					}
				});

				this._timer.Start();

				//this._game.EngineTimer.Start();

				if (this._mediaPlayerMain != null)
				{
					this._mediaPlayerMain.Play();
				}

				this.ShowWindow();
			}
		}

		public EngineRectangle TransformEngineRectangle(EngineRectangle er)
		{
			return new EngineRectangle(er.X/this._engineToDesktopMultiplier + this._horizontalOffset,
				er.Y/this._engineToDesktopMultiplier + this._verticalOffset, er.Height/this._engineToDesktopMultiplier,
				er.Width/this._engineToDesktopMultiplier, er.LastOffset/this._engineToDesktopMultiplier, er.Direction, er.IsBarrier);
		}

		public void ShowWindow()
		{
			Application.EnableVisualStyles();
			Application.Run(this._mainWindow);
			this.UnregisterEvents();
		}

		private void RegisterEvents()
		{
			this._game.EventFinish += this.FinishRaise;
			this._game.EventDie += this.DieRaise;
			this._game.EventMove += this.MoveRaise;

			this._mainWindow.EventMove += this.WindowMoveRaise;
			this._mainWindow.EventRestart += this.RestartRaise;
			this._mainWindow.EventResize += this.ResizeRaise;
		}

		private void UnregisterEvents()
		{
			this._game.EventFinish -= this.FinishRaise;
			this._game.EventDie -= this.DieRaise;
			this._game.EventMove -= this.MoveRaise;

			this._mainWindow.EventMove -= this.WindowMoveRaise;
			this._mainWindow.EventRestart -= this.RestartRaise;
			this._mainWindow.EventResize -= this.ResizeRaise;
		}

		private void SetLabelText()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Deaths: {0}", this._game.Deaths);
			this._labelDeath.Text = sb.ToString();
		}

		private void FinishRaise(Object sender, bool e)
		{
			if (e)
			{
				if (this._timer.Enabled)
				{
					this._timer.Stop();
				}
				
				if (this._mediaPlayerEvents == null)
				{
					this._mediaPlayerEvents = new MediaPlayer();
					this._mediaPlayerEvents.Volume = 1.0;
				}

				try
				{
					this._mediaPlayerEvents.Open(new Uri(Application.StartupPath + "\\Sounds\\Finish.mp3"));
				}
				catch (FileNotFoundException ee)
				{

					this._mediaPlayerEvents = null;
				}

				if (this._mediaPlayerEvents != null)
				{
					this._mediaPlayerEvents.Play();
				}

				if (MessageBox.Show(@"CONGRATULATION!!! You have finished the game", @"Finished", MessageBoxButtons.YesNo,
					MessageBoxIcon.Information) == DialogResult.Yes)
				{
					this._timer.Start();
				}

				else
				{
					this.UnregisterEvents();
					Application.Exit();
				}

				this._dd.Redraw(this._pb, this.TransformEngineRectangle(this._game.Square));
				this._game.GetStartSquare();
				this._dd.DrawRectangle(this._graphics, this._pb, this.TransformEngineRectangle(this._game.Square), Brushes.Red);
			}
		}

		private void DieRaise(Object sender, EngineRectangle e)
		{
			if (this._mediaPlayerEvents == null)
			{
				this._mediaPlayerEvents = new MediaPlayer();
				this._mediaPlayerEvents.Volume = 1.0;
			}
			try
			{
				this._mediaPlayerEvents.Open(new Uri(Application.StartupPath + "\\Sounds\\Die.mp3"));
			}
			catch (FileNotFoundException ee)
			{
				this._mediaPlayerEvents = null;
			}

			if (this._mediaPlayerEvents != null)
			{
				this._mediaPlayerEvents.Play();
			}
			
			this.SetLabelText();
			this._dd.DrawAfterMoving(this._pb, this.TransformEngineRectangle(e));
			this._dd.Redraw(this._pb, this.TransformEngineRectangle(e));
			this._dd.DrawRectangle(this._graphics, this._pb, this.TransformEngineRectangle(this._game.Square), Brushes.Red);
		}

		private void MoveRaise(Object sender, EngineRectangle e)
		{
			this._dd.DrawAfterMoving(this._pb, this.TransformEngineRectangle(e));
			Brush brush = e.IsBarrier ? Brushes.Blue : Brushes.Red;
			this._dd.DrawRectangle(this._graphics, this._pb, this.TransformEngineRectangle(e), brush);
		}

		private void WindowMoveRaise(Object sender, Directions e)
		{
			this._game.Square.Direction = e;
			double offset = Math.Floor(this._pb.Width*0.018)*this._engineToDesktopMultiplier;
			this._game.MoveRectangle(this._game.Square, offset);
		}

		private void RestartRaise(Object sender, EventArgs e)
		{
			if (this._mediaPlayerEvents == null)
			{
				this._mediaPlayerEvents = new MediaPlayer();
				this._mediaPlayerEvents.Volume = 1.0;
			}
			try
			{
				this._mediaPlayerEvents.Open(new Uri(Application.StartupPath + "\\Sounds\\Restart.mp3"));
			}
			catch (FileNotFoundException ee)
			{

				this._mediaPlayerEvents = null;
			}

			if (this._mediaPlayerEvents != null)
			{
				this._mediaPlayerEvents.Play();
			}
			
			this._game.Deaths = 0;
			this.SetLabelText();
			this._dd.Redraw(this._pb, this.TransformEngineRectangle(this._game.Square));
			for (int a = 0; a < this._game.Barriers.Length; a++)
			{
				this._dd.Redraw(this._pb, this.TransformEngineRectangle(this._game.Barriers[a]));
			}

			this._game.Restart();
			this._dd.DrawRectangle(this._graphics, this._pb, this.TransformEngineRectangle(this._game.Square), Brushes.Red);
			for (int a = 0; a < this._game.Barriers.Length; a++)
			{
				this._dd.DrawRectangle(this._graphics, this._pb, this.TransformEngineRectangle(this._game.Barriers[a]), Brushes.Blue);
			}

			if (!this._timer.Enabled)
			{
				this._timer.Start();
			}
		}

		private void ResizeRaise(Object sender, EventArgs e)
		{
			if (this._timer.Enabled)
			{
				this._timer.Stop();
			}

			this._engineToDesktopMultiplier = Math.Max(this._game.Width/this._pb.Width, this._game.Height/this._pb.Height);
			this.GetGraphics();

			this._horizontalOffset = (this._pb.Width*this._engineToDesktopMultiplier - this._game.Width)/2/
			                         this._engineToDesktopMultiplier;
			this._verticalOffset = (this._pb.Height*this._engineToDesktopMultiplier - this._game.Height)/2/
			                       this._engineToDesktopMultiplier;


			this._dd.DrawLevelBackground(this._graphics, this._pb, (this._game.RectHeight/this._engineToDesktopMultiplier),
				(this._game.RectWidth/this._engineToDesktopMultiplier), this._horizontalOffset, this._verticalOffset);

			this._dd.DrawRectangle(this._graphics, this._pb, this.TransformEngineRectangle(this._game.Square), Brushes.Red);

			for (int a = 0; a < this._game.Barriers.Length; a++)
			{
				this._dd.DrawRectangle(this._graphics, this._pb, this.TransformEngineRectangle(this._game.Barriers[a]), Brushes.Blue);
			}

			this._timer.Start();
		}
	}
}