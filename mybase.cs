//This is a part of the DC4Ever emulator
//You are not allowed to release modified(or unmodified) versions
//without asking me (drk||Raziel).
//For Suggestions ect please e-mail at : stef_mp@yahoo.gr
//Note : This is just to prove that Fast emulation is possible with 
//	C# /.net ...And yes , this code could be writen at VB.net and 
//	run more or less at the same speed on dynarec mode
//	This code requires C#.net 2.0 (Get the C# epxress 2005 Beta from microsoft)
//


using System;
using System.Windows.Forms;

namespace DC4Ever
{
	/// <summary>
	/// This is The base Class for the project.
	/// </summary>
	public class dc
	{
		public const int mb = 1024*1024;
		public const int kb = 1024;
		public static frmmain frmMain;
		public static frmabout frmAbout;
		public static DebugConsole dcon;//debug console

		#region Init and Startup code
		public static void run()//run the gui
		{
			Console.WriteLine ("Starting");
#if nrt
			frmMain=new frmmain();

			frmAbout= new frmabout();
#endif
			Console.WriteLine ("Loaded forms");
			dcon=new DebugConsole();
			Console.WriteLine ("Loaded visual console");
			dcon.WriteLine("Loading Bios and Flash Ram");
            emu.loadbiosfile("bin");
            emu.loadbiosflashfile("bios_flash.bin");
			//fastint.init();
			dcon.WriteLine("Running GUI");
			Console.WriteLine ("Serial output:");
#if nrt
			Application.Run(frmMain);
#else
            Timer t = new Timer();
            t.Tick+=new EventHandler(t_Tick);
            t.Interval = 1000;
            t.Enabled = true;
            dc.dcon.WriteLine("Loading ip.bin and Resetting sh4");
            emu.loadipbin("ip.bin");
            emu.resetsh4();
            dc.dcon.WriteLine("runcpu");
            emu.runcpu();
#endif
			emu.runsh=false;//stop cpu if it is running..
		}
		//Startup Code
		[STAThread]
		static void Main() 
		{
			dc.run();
		}
		#endregion
#if !nrt
        static long told = System.DateTime.Now.Ticks;
        static void t_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Running at " + System.Convert.ToString(((double)emu.opcount / 1024 / 1024) / ((double)(System.DateTime.Now.Ticks - told) / 10000000)) + " MHz , " + emu.fps + " fps(not real, just screen refresh) ");// + ((float)emu.mw / 1024 / 1204).ToString() + " megabyte vram writes per sec " + emu.ch.ToString() + ",cache hits " + emu.cm.ToString() + ",cache misses " + ((emu.ch + 1) / (emu.cm + 1)).ToString() + ":1 cache hit ratio ");
            emu.opcount = 0;
            emu.fps = 0;
            emu.mw = 0;
            told = System.DateTime.Now.Ticks;
        }
#endif
    }
}
