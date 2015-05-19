using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TheHardestGame.GameEngine.Enums;

namespace TheHardestGame.DesktopUI
{
	public partial class MainWindow : Form
	{
		public event EventHandler<Directions> EventMove;
		public event EventHandler EventRestart;
		public event EventHandler EventResize;

		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void WindowResize(object sender, EventArgs e)
		{
			if (this.WindowState != FormWindowState.Minimized)
			{
				this.ResizetRaise();
			}
		}

		private void OnMove(Directions e)
		{
			EventHandler<Directions> temp = Volatile.Read(ref this.EventMove);

			if (temp != null)
			{
				temp(this, e);
			}
		}

		public void MoveRaise(Directions direction)
		{
			this.OnMove(direction);
		}

		private void OnRestart()
		{
			EventHandler temp = Volatile.Read(ref this.EventRestart);

			if (temp != null)
			{
				temp(this, null);
			}
		}

		public void RestartRaise()
		{
			this.OnRestart();
		}

		private void OnResize()
		{
			EventHandler temp = Volatile.Read(ref this.EventResize);

			if (temp != null)
			{
				temp(this, null);
			}
		}

		public void ResizetRaise()
		{
			this.OnResize();
		}

		public PictureBox PictureBoxDrawing
		{
			get { return this.pictureBoxLevel; }
		}

		public Label LabelDeathQ
		{
			get { return this.labelDeaths; }
		}

		private void ButtonRestartClick(object sender, EventArgs e)
		{
			this.RestartRaise();
		}

		private void ButtonHelpClick(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("GAME DESCRIPTION\r\n\r\n");
			sb.Append(
				"The aim of this game is to move red square from left green zone to right one avoiding blue barriers.\r\n\r\n");
			sb.Append("Use arrow keys to move red square.\r\n\r\n");
			sb.Append("If red square steps on blue barrier, it will be returned to its start position.\r\n\r\n");
			sb.Append("ENJOY)");

			MessageBox.Show(sb.ToString(), "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Up:
					this.MoveRaise(Directions.Up);
					break;
				case Keys.Down:
					this.MoveRaise(Directions.Down);
					break;
				case Keys.Left:
					this.MoveRaise(Directions.Left);
					break;
				case Keys.Right:
					this.MoveRaise(Directions.Right);
					break;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}