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
			frmMain=new frmmain();
			frmAbout= new frmabout();
			Console.WriteLine ("Loaded forms");
			dcon=new DebugConsole();
			Console.WriteLine ("Loaded visual console");
			dcon.WriteLine("Loading Bios and Flash Ram");
            emu.loadbiosfile("bin");
            emu.loadbiosflashfile("bios_flash.bin");
			//fastint.init();
			dcon.WriteLine("Running GUI");
			Console.WriteLine ("Serial output:");
			Application.Run(frmMain);
			emu.runsh=false;//stop cpu if it is running..
		}
		//Startup Code
		[STAThread]
		static void Main() 
		{
			dc.run();
		}
		#endregion
	}
}
