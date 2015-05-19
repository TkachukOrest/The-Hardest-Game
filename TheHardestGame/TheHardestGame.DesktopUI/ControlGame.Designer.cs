using System.ComponentModel;
using System.Windows.Forms;

namespace TheHardestGame.DesktopUI
{
	partial class ControlGame
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonRestart = new System.Windows.Forms.Button();
			this.buttonHelp = new System.Windows.Forms.Button();
			this.labelDeaths = new System.Windows.Forms.Label();
			this.pictureBoxLevel = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLevel)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonRestart
			// 
			this.buttonRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonRestart.Location = new System.Drawing.Point(12, 13);
			this.buttonRestart.Name = "buttonRestart";
			this.buttonRestart.Size = new System.Drawing.Size(100, 31);
			this.buttonRestart.TabIndex = 0;
			this.buttonRestart.Text = "Restart";
			this.buttonRestart.UseVisualStyleBackColor = true;
			// 
			// buttonHelp
			// 
			this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonHelp.Location = new System.Drawing.Point(734, 13);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new System.Drawing.Size(100, 31);
			this.buttonHelp.TabIndex = 1;
			this.buttonHelp.Text = "Help";
			this.buttonHelp.UseVisualStyleBackColor = true;
			// 
			// labelDeaths
			// 
			this.labelDeaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDeaths.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelDeaths.Location = new System.Drawing.Point(118, 13);
			this.labelDeaths.Name = "labelDeaths";
			this.labelDeaths.Size = new System.Drawing.Size(610, 31);
			this.labelDeaths.TabIndex = 2;
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
			this.pictureBoxLevel.Location = new System.Drawing.Point(12, 50);
			this.pictureBoxLevel.Name = "pictureBoxLevel";
			this.pictureBoxLevel.Size = new System.Drawing.Size(822, 249);
			this.pictureBoxLevel.TabIndex = 3;
			this.pictureBoxLevel.TabStop = false;
			// 
			// ControlGame
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pictureBoxLevel);
			this.Controls.Add(this.labelDeaths);
			this.Controls.Add(this.buttonHelp);
			this.Controls.Add(this.buttonRestart);
			this.Name = "ControlGame";
			this.Size = new System.Drawing.Size(848, 313);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLevel)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Button buttonRestart;
		private Button buttonHelp;
		private Label labelDeaths;
		private PictureBox pictureBoxLevel;
	}
}
