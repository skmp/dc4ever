using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DC4Ever
{
	/// <summary>
	/// Summary description for frmMain.
	/// </summary>
	public class frmmain : System.Windows.Forms.Form
	{
		long told;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Timer timer1;
		public System.Windows.Forms.PictureBox screen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.ComponentModel.IContainer components;

		public frmmain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// 
			//
			dx.init(this.screen);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.screen = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
            this.SuspendLayout();
// 
// mainMenu1
// 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                this.menuItem1
            });
            this.mainMenu1.Name = "mainMenu1";
// 
// menuItem1
// 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
            this.menuItem1.Name = "menuItem1";
            this.menuItem1.Text = "Main";
// 
// menuItem2
// 
            this.menuItem2.Index = 0;
            this.menuItem2.Name = "menuItem2";
            this.menuItem2.Text = "Run";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
// 
// menuItem3
// 
            this.menuItem3.Index = 1;
            this.menuItem3.Name = "menuItem3";
            this.menuItem3.Text = "Run Cached";
            this.menuItem3.Visible = false;
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
// 
// timer1
// 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
// 
// screen
// 
            this.screen.Location = new System.Drawing.Point(8, 1);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(640, 480);
            this.screen.TabIndex = 0;
            this.screen.TabStop = false;
// 
// label1
// 
            this.label1.Location = new System.Drawing.Point(8, 488);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(640, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
// 
// frmmain
// 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(656, 513);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.screen);
            this.Menu = this.mainMenu1;
            this.Name = "frmmain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dc4Ever v 1.0 - beta1";
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

		private void menuItem1_Click(object sender, System.EventArgs e)
		{

		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			dc.dcon.WriteLine("Loading ip.bin and Resetting sh4");
            emu.loadipbin("ip.bin");
            emu.resetsh4();
            dc.dcon.WriteLine("runcpu");
            emu.runcpu();
        }

		private void timer1_Tick(object sender, System.EventArgs e)
		{
            this.label1.Text = "Running at " + System.Convert.ToString(((double)emu.opcount / 1024 / 1024) / ((double)(System.DateTime.Now.Ticks - told) / 10000000)) + " mips , " + emu.fps + " fps(not real, just screen refresh)";
            emu.opcount = 0;
            emu.fps = 0;
            told=System.DateTime.Now.Ticks;
			//if (runsh) fastint.showstats();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			dc.dcon.WriteLine("Loading ip.bin and Resetting sh4");
            emu.loadipbin("ip.bin");
            emu.resetsh4();
            dc.dcon.WriteLine("runcpu");
			//runcpuDyna();
		}


	}
}
