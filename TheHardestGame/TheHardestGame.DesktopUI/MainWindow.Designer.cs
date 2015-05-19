using System.ComponentModel;
using System.Windows.Forms;

namespace TheHardestGame.DesktopUI
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.labelDeaths = new System.Windows.Forms.Label();
			this.pictureBoxLevel = new System.Windows.Forms.PictureBox();
			this.buttonRestart = new System.Windows.Forms.Button();
			this.buttonHelp = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLevel)).BeginInit();
			this.SuspendLayout();
			// 
			// labelDeaths
			// 
			this.labelDeaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDeaths.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelDeaths.Location = new System.Drawing.Point(116, 9);
			this.labelDeaths.Name = "labelDeaths";
			this.labelDeaths.Size = new System.Drawing.Size(610, 34);
			this.labelDeaths.TabIndex = 0;
			this.labelDeaths.Text = "Deaths: 0";
			this.labelDeaths.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBoxLevel
			// 
			this.pictureBoxLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxLevel.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.pictureBoxLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxLevel.Location = new System.Drawing.Point(10, 50);
			this.pictureBoxLevel.Name = "pictureBoxLevel";
			this.pictureBoxLevel.Size = new System.Drawing.Size(822, 249);
			this.pictureBoxLevel.TabIndex = 1;
			this.pictureBoxLevel.TabStop = false;
			// 
			// buttonRestart
			// 
			this.buttonRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonRestart.Location = new System.Drawing.Point(10, 12);
			this.buttonRestart.Name = "buttonRestart";
			this.buttonRestart.Size = new System.Drawing.Size(100, 32);
			this.buttonRestart.TabIndex = 2;
			this.buttonRestart.TabStop = false;
			this.buttonRestart.Text = "Restart";
			this.buttonRestart.UseVisualStyleBackColor = true;
			this.buttonRestart.Click += new System.EventHandler(this.ButtonRestartClick);
			// 
			// buttonHelp
			// 
			this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonHelp.Location = new System.Drawing.Point(732, 11);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new System.Drawing.Size(100, 32);
			this.buttonHelp.TabIndex = 3;
			this.buttonHelp.TabStop = false;
			this.buttonHelp.Text = "Help";
			this.buttonHelp.UseVisualStyleBackColor = true;
			this.buttonHelp.Click += new System.EventHandler(this.ButtonHelpClick);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(844, 311);
			this.Controls.Add(this.buttonHelp);
			this.Controls.Add(this.buttonRestart);
			this.Controls.Add(this.pictureBoxLevel);
			this.Controls.Add(this.labelDeaths);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(860, 350);
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "The Hardest Game";
			this.Resize += new System.EventHandler(this.WindowResize);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLevel)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Label labelDeaths;
		private PictureBox pictureBoxLevel;
		private Button buttonRestart;
		private Button buttonHelp;
	}
}

