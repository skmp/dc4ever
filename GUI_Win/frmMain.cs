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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.screen = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
			this.menuItem1.Text = "Main";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Run";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
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
			this.screen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.screen.Location = new System.Drawing.Point(8, 1);
			this.screen.Name = "screen";
			this.screen.Size = new System.Drawing.Size(640, 480);
			this.screen.TabIndex = 0;
			this.screen.TabStop = false;
			this.screen.Resize += new System.EventHandler(this.screen_Resize);
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
			this.ClientSize = new System.Drawing.Size(656, 508);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.screen);
			this.Menu = this.mainMenu1;
			this.Name = "frmmain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Dc4Ever v 1.0 - beta3 - Managed Recompiler - Static Condtional/Uncoditional brach" +
				" inlining";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmmain_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmmain_KeyDown);
			
			this.ResumeLayout(false);

        }

		void screen_Resize(object sender, EventArgs e)
		{
			zezu_pvr.PvrUpdate(4);
		}



		#endregion

		public static ushort kcode = 0xFFFF;
		void frmmain_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Z:
					kcode |= maple.key_CONT_A;
					break;
				case Keys.X:
					kcode |= maple.key_CONT_B;
					break;
				case Keys.C:
					kcode |= maple.key_CONT_C;
					break;
				case Keys.V:
					kcode |= maple.key_CONT_D;
					break;

				case Keys.ShiftKey:
					kcode |= maple.key_CONT_START;
					break;

				case Keys.Up:
					kcode |= maple.key_CONT_DPAD_UP;
					break;
				case Keys.Down:
					kcode |= maple.key_CONT_DPAD_DOWN;
					break;
				case Keys.Left:
					kcode |= maple.key_CONT_DPAD_LEFT;
					break;
				case Keys.Right:
					kcode |= maple.key_CONT_DPAD_RIGHT;
					break;
			}
		}

		void frmmain_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Z:
					kcode &= 0xFFFF - maple.key_CONT_A;
					break;
				case Keys.X:
					kcode &= 0xFFFF - maple.key_CONT_B;
					break;
				case Keys.C:
					kcode &= 0xFFFF - maple.key_CONT_C;
					break;
				case Keys.V:
					kcode &= 0xFFFF - maple.key_CONT_D;
					break;

				case Keys.ShiftKey:
					kcode &= 0xFFFF - maple.key_CONT_START;
					break;

				case Keys.Up:
					kcode &= 0xFFFF - maple.key_CONT_DPAD_UP;
					break;
				case Keys.Down:
					kcode &= 0xFFFF - maple.key_CONT_DPAD_DOWN;
					break;
				case Keys.Left:
					kcode &= 0xFFFF - maple.key_CONT_DPAD_LEFT;
					break;
				case Keys.Right:
					kcode &= 0xFFFF - maple.key_CONT_DPAD_RIGHT;
					break;
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{

		}

        
        System.Threading.Thread thr = new System.Threading.Thread(new System.Threading.ThreadStart(sh4.runcpu));
        private void menuItem2_Click(object sender, System.EventArgs e)
		{
			dc.dcon.WriteLine("Loading ip.bin and Resetting sh4");
            bios.loadipbin("ip.bin");
            sh4.resetsh4();
            dc.dcon.WriteLine("runcpu");
            if (!thr.IsAlive)
                thr.Start();
            //sh4.runcpu();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            this.label1.Text = "Running at " + System.Convert.ToString(((double)sh4.clcount / 1024 / 1024) / ((double)(System.DateTime.Now.Ticks - told) / 10000000)) + " MHz , " + pvr.fps + " fps(not real, just screen refresh) ;vram:" + (pvr.mw).ToString() + " b;pc=" + Convert.ToString(sh4.pc, 16); ;// + ((float)emu.mw / 1024 / 1204).ToString() + " megabyte vram writes per sec " + emu.ch.ToString() + ",cache hits " + emu.cm.ToString() + ",cache misses " + ((emu.ch + 1) / (emu.cm + 1)).ToString() + ":1 cache hit ratio " + ;
            //dc.dcon.WriteLine(emu.pc);
            sh4.clcount = 0;
            pvr.fps = 0;
            //emu.mw = 0;
            told = System.DateTime.Now.Ticks;
            //if (runsh) fastint.showstats();
        }

        private void menuItem3_Click(object sender, System.EventArgs e)
		{
			dc.dcon.WriteLine("Loading ip.bin and Resetting sh4");
            bios.loadipbin("ip.bin");
            sh4.resetsh4();
            dc.dcon.WriteLine("runcpu");
			//runcpuDyna();
		}





    }
}


