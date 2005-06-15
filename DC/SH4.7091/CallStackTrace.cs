using System;
using System.Collections.Generic;
using System.Text;

namespace DC4Ever
{
	public enum CallType
	{
		Normal,
		Interupt
	}

	public struct callstack_frame
	{
		public uint startadr;
		public uint fromadr;
		public uint retadr;
		public uint calladr;
		public CallType calltype;
		public ulong cycletag;
		string Name_ ;
		public string Name
		{
			get
			{
				return Name_ == null ? (Name_ = emu.GetProcedureNameFromAddr(startadr)) : Name_;
			}
		}

		public callstack_frame(uint startadr,uint fromadr, uint retadr, uint calladr, CallType calltype, ulong cycletag)
		{
			this.startadr = startadr;
			this.fromadr = fromadr;
			this.retadr = retadr;
			this.calladr = calladr;
			this.calltype = calltype;
			this.cycletag = cycletag;
			Name_ = null;
		}

		public override string ToString()
		{
			return ((Name == null) ? "0x" + Convert.ToString(retadr, 16) : Name) + "+0x" + Convert.ToString((fromadr & 0x1FFFFFFF) - (startadr & 0x1FFFFFFF), 16) + " ;call type :" + calltype.ToString();
		}
	}

	public static unsafe partial class emu
	{
		
		public static Stack<callstack_frame> cstCallStack = new Stack<callstack_frame>();

		static void cstAddCall(uint from, uint ret, uint calladdr,CallType calltype)
		{
#if !optimised_b
			if (cstCallStack.Count!=0)
				cstCallStack.Push(new callstack_frame(cstCallStack.Peek().calladr ,from, ret, calladdr, calltype, gl_cop_cnt));
			else
				cstCallStack.Push(new callstack_frame(dc_boot_vec, from, ret, calladdr, calltype, gl_cop_cnt));
#endif
		}

		static void cstRemCall(uint ret)
		{
#if !optimised_b
			if (cstCallStack.Count > 0)
			{
				callstack_frame t = cstCallStack.Pop();
				if (t.retadr != ret)
				{
					WriteLine("Call stack wrong Ret address;" + UintToHex(ret) + "!=" + UintToHex(t.retadr));
					delayslot = t.retadr;
				}
			}
			else
				WriteLine("Call stack unexpected ret" );
#endif
		}
	}
}
