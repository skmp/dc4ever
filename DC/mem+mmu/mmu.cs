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

namespace DC4Ever
{
	/// <summary>
	/// MMU is emulated here (Memory managment usint)
	/// Also some simple adress proc may be done here
	/// </summary>
    #if nrt 
    public static partial  class emu 
 #else  
    public partial  class emu 
 #endif
    {
		public static uint mmutrans(uint adr)
		{
			return adr;//no mmu that simple..
		}

	}
}
