namespace TheHardestGame.DesktopUI
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.LabelDeaths = new System.Windows.Forms.Label();
			this.PictureBoxLevel = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxLevel)).BeginInit();
			this.SuspendLayout();
			// 
			// LabelDeaths
			// 
			this.LabelDeaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LabelDeaths.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.LabelDeaths.Location = new System.Drawing.Point(10, 9);
			this.LabelDeaths.Name = "LabelDeaths";
			this.LabelDeaths.Size = new System.Drawing.Size(712, 34);
			this.LabelDeaths.TabIndex = 0;
			this.LabelDeaths.Text = "Deaths: 0";
			this.LabelDeaths.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PictureBoxLevel
			// 
			this.PictureBoxLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PictureBoxLevel.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.PictureBoxLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PictureBoxLevel.Location = new System.Drawing.Point(10, 50);
			this.PictureBoxLevel.Name = "PictureBoxLevel";
			this.PictureBoxLevel.Size = new System.Drawing.Size(712, 249);
			this.PictureBoxLevel.TabIndex = 1;
			this.PictureBoxLevel.TabStop = false;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 311);
			this.Controls.Add(this.PictureBoxLevel);
			this.Controls.Add(this.LabelDeaths);
			this.MinimumSize = new System.Drawing.Size(750, 350);
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "The Hardest Game";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WindowKeyDown);
			this.Resize += new System.EventHandler(this.WindowResize);
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxLevel)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label LabelDeaths;
		private System.Windows.Forms.PictureBox PictureBoxLevel;
	}
}

