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
#if nrt
using System.Windows.Forms;
#else
namespace System.Windows.Forms
{
    public class Application
    {
        public static void DoEvents()
        {

        }
    }
}
#endif

namespace DC4Ever
{
	/// <summary>
	/// This is The base Class for the project.
	/// </summary>
	public class dc
	{
#if nrt
		public static frmmain frmMain;
		public static frmabout frmAbout;
#endif
        public static DebugConsole dcon;//debug console

		#region Init and Startup code
		public static void run()//run the gui
		{
			Console.WriteLine ("Starting");
            emu.Init();
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
            dc.dcon.WriteLine("Loading ip.bin and Resetting sh4");
#if !interpreter
            Console.WriteLine("Please give inlining level settings..");
            emu.br_8_b_level_max = Convert.ToUInt32(Console.ReadLine());
            emu.br_8_f_level_max = Convert.ToUInt32(Console.ReadLine());
#endif
            emu.loadipbin("ip.bin");
            emu.resetsh4();
            dc.dcon.WriteLine("runcpu");
            emu.runcpu();
#endif
			emu.runsh=false;//stop cpu if it is running..
            emu.DeInit();
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
        public static void t_Tick(object sender, EventArgs e)
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
