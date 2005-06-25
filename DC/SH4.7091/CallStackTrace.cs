using System;
using System.Collections;
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
				return Name_ == null ? (Name_ = sh4_disasm.GetProcedureNameFromAddr(startadr)) : Name_;
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

	public class Stack_callstack_frame_ : Stack
	{
		public new callstack_frame Peek()
		{
			return (callstack_frame)base.Peek ();
		}
		public void Push(callstack_frame obj)
		{
			base.Push (obj);
		}

		public new callstack_frame Pop()
		{
			return (callstack_frame)base.Pop ();
		}



	}
	public unsafe class CallStackTrace
	{
		
		public static Stack_callstack_frame_ cstCallStack = new Stack_callstack_frame_();

		public static void cstAddCall(uint from, uint ret, uint calladdr,CallType calltype)
		{
#if !optimised_b
			if (cstCallStack.Count!=0)
				cstCallStack.Push(new callstack_frame(cstCallStack.Peek().calladr ,from, ret, calladdr, calltype, sh4.gl_cop_cnt));
			else
				cstCallStack.Push(new callstack_frame(sh4.dc_boot_vec, from, ret, calladdr, calltype, sh4.gl_cop_cnt));
#endif
		}
		public static string UintToHex(uint val)
		{
			return "0x"+Convert.ToString(val, 16).ToUpper();
		}
		public static void cstRemCall(uint ret,CallType rettype)
		{
#if !optimised_b
			if (cstCallStack.Count > 0)
			{
				callstack_frame t = cstCallStack.Pop();
				if (t.retadr != ret)
				{
					mem.WriteLine("Call stack wrong Ret address;" +  sh4_disasm.UintToHex(ret) + "!=" + sh4_disasm.UintToHex(t.retadr) + " sub start : " + sh4_disasm.UintToHex(t.startadr));
					dc.dbger.mode = 1;
				}
				if (t.calltype != rettype)
				{
					mem.WriteLine("Call stack wrong TYPE address;" + t.calltype.ToString() + "!=" + rettype.ToString() + " sub start : " + sh4_disasm.UintToHex(t.startadr));
					//cstCallStack.Push(t);
					//dc.dbger.SetBP(t.startadr);
					dc.dbger.mode = 1;
					//sh4.pc = t.retadr;
				}
			}
			else
				mem.WriteLine("Call stack unexpected ret" );
#endif
		}
	}
}

