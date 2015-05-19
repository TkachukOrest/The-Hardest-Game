using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TheHardestGame.DesktopUI
{
	public enum Commands
	{
		Start,
		Exit
	}

	public partial class StartForm : Form
	{
		public Commands Command;

		public StartForm()
		{
			this.InitializeComponent();
			this.Command = Commands.Exit;
		}

		private void ButtonInfoClick(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("GAME DESCRIPTION\r\n\r\n");
			sb.Append(
				"The aim of this game is to move red square from left green zone to right one avoiding blue barriers.\r\n\r\n");
			sb.Append("If red square steps on blue barrier, it will be returned to its start position.\r\n\r\n");
			sb.Append("ENJOY)");

			MessageBox.Show(sb.ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void ButtonControlsClick(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("GAME CONTROLS\r\n\r\n");
			sb.Append("Use \"Up Arrow\" to move red square up.\r\n\r\n");
			sb.Append("Use \"Down Arrow\" to move red square down.\r\n\r\n");
			sb.Append("Use \"Left Arrow\" to move red square left.\r\n\r\n");
			sb.Append("Use \"Right Arrow\" to move red square right.\r\n\r\n");
			sb.Append("ENJOY)");

			MessageBox.Show(sb.ToString(), "Controls", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void ButtonStartClick(object sender, EventArgs e)
		{
			this.Command = Commands.Start;
			this.Close();
		}

		private void ButtonExitClick(object sender, EventArgs e)
		{
			this.Command = Commands.Exit;
			this.Close();
		}

		private void WindowClosing(object sender, FormClosingEventArgs e)
		{
			if (Command != Commands.Start)
			{
				this.Command = Commands.Exit;
			}
		}
	}
}