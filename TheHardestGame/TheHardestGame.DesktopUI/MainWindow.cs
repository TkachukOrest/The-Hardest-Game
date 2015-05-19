using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace TheHardestGame.DesktopUI
{
	public partial class MainWindow : Form
	{
		//private Graphics _graphics;

		//public static event EventHandler<MoveEventArgs> EventMove;

		//DesktopUIGame _dUIG;

		//private System.Windows.Forms.Timer _timerBarriers;

		//private DesktopUIGame DUIG;

		public MainWindow()
		{
			InitializeComponent();

			//Initialize();

			//_dUIG = new DesktopUIGame();
			//_dUIG.StartGame(_graphics, PictureBoxLevel, PictureBoxLevel.Height, PictureBoxLevel.Width);
		}

		//private void Initialize()
		//{
		//	Bitmap DrawArea = new Bitmap(PictureBoxLevel.Width, PictureBoxLevel.Height);
		//	PictureBoxLevel.Image = DrawArea;
		//	_graphics = Graphics.FromImage(DrawArea);

		//}

		private void WindowResize(object sender, EventArgs e)
		{
			//ReinitalizeDrawArea();
			//_dUIG.Renew(_graphics, PictureBoxLevel.Height, PictureBoxLevel.Width);
		}

		//public void ReinitalizeDrawArea()
		//{
		//	Bitmap DrawArea = new Bitmap(PictureBoxLevel.Width, PictureBoxLevel.Height);
		//	PictureBoxLevel.Image = DrawArea;
		//	_graphics = Graphics.FromImage(DrawArea);
		//}

		private void WindowKeyDown(object sender, KeyEventArgs e)
		{
			//if (e.KeyCode == Keys.Up)
			//{
			//	MoveRaise(0);
			//}
			//else if (e.KeyCode == Keys.Down)
			//{
			//	MoveRaise(1);
			//}
			//else if (e.KeyCode == Keys.Left)
			//{
			//	MoveRaise(2);
			//}
			//else if (e.KeyCode == Keys.Right)
			//{
			//	MoveRaise(3);
			//}
		}

		//private void OnMove(MoveEventArgs e)
		//{
		//	EventHandler<MoveEventArgs> temp = Volatile.Read(ref EventMove);

		//	if (temp != null)
		//	{
		//		temp(this, e);
		//	}
		//}

		//public void MoveRaise(int direction)
		//{
		//	MoveEventArgs e = new MoveEventArgs(direction);

		//	OnMove(e);
		//}
	}
}
