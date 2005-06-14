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
		public static frmmain frmMain;
		public static frmabout frmAbout;
        public static DebugConsole dcon;//debug console
		public static Debugger dbger;
		
		public static pointerlib.MemoryManager mmgr = new pointerlib.MemoryManager();

		#region Init and Startup code
		public static void run()//run the gui
		{
			Console.WriteLine ("Starting");
            emu.Init();
			frmMain=new frmmain();
			frmAbout= new frmabout();
			dbger = new Debugger();
			Console.WriteLine ("Loaded forms");
			dcon=new DebugConsole();
			Console.WriteLine ("Loaded visual console");
			dcon.WriteLine("Loading Bios and Flash Ram");
            emu.loadbiosfile("bios.bin");
            emu.loadbiosflashfile("bios_flash.bin");
			//fastint.init();
			dcon.WriteLine("Running GUI");
			Console.WriteLine ("Serial output:");
			dbger.Show();
			Application.Run(frmMain);
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
