using System;

namespace DC4Ever
{
	/// <summary>
	/// MMU is emulated here (Memory managment usint)
	/// Also some simple adress proc may be done here
	/// </summary>
    public partial class emu
    {
		public static uint mmutrans(uint adr)
		{
			return adr;//no mmu that simple..
		}

	}
}
