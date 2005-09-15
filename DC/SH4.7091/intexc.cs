//This is a part of the DC4Ever emulator
//You are not allowed to release modified(or unmodified) versions
//without asking me (drk||Raziel).
//For Suggestions ect please e-mail at : stef_mp@yahoo.gr
//Note : This is just to prove that Fast emulation is possible with 
//	C# /.net ...And yes , this code could be writen at VB.net and 
//	run more or less at the same speed on dynarec mode
//	This code requires C#.net 2.0 (Get the C# epxress 2005 Beta from microsoft)
//
//#define Old_IntCCode
using System;
using System.Collections;

//SET_BIT(reg, bit) (reg |= (bit))
//REMOVE_BIT(reg, bit) (reg &= ~(bit))

namespace DC4Ever
{

	//ok , we have 3 types of interupts on the sh4
	//NMI,RL and Internal Module interupts
	//on the dreamcast the NMI interupts are not used
	//The Interupt input to RL is form the holly sub system (the output of the holly interupt controler)
	//The holly manages all external interupts , and it has 3 types of interupts normal , external and error

	public enum InteruptType
	{
		sh4_int   = 0x00000000,
		sh4_exp   = 0x01000000,
		holly_nrm = 0x20000000,
		holly_ext = 0x21000000,
		holly_err = 0x22000000,
		InteruptTypeMask = 0x7F000000,
		InteruptIDMask=0x00FFFFFF
	}



	public enum sh4_int
	{
		//sh4 internal module interupts
		sh4_in_TMU0=InteruptType.sh4_int|1,
		sh4_in_TMU1=InteruptType.sh4_int|2,
		sh4_in_TMU2=InteruptType.sh4_int|3,

		//sh4 exeptions 
		sh4_ex_USER_BREAK_BEFORE_INSTRUCTION_EXECUTION = InteruptType.sh4_exp | 0x1e0,
		sh4_ex_INSTRUCTION_ADDRESS_ERROR =InteruptType.sh4_exp | 0x0e0,
		sh4_ex_INSTRUCTION_TLB_MISS =InteruptType.sh4_exp | 0x040,
		sh4_ex_INSTRUCTION_TLB_PROTECTION_VIOLATION = InteruptType.sh4_exp |0x0a0,
		sh4_ex_GENERAL_ILLEGAL_INSTRUCTION = InteruptType.sh4_exp |0x180,
		sh4_ex_SLOT_ILLEGAL_INSTRUCTION = InteruptType.sh4_exp |0x1a0,
		sh4_ex_GENERAL_FPU_DISABLE = InteruptType.sh4_exp |0x800,
		sh4_ex_SLOT_FPU_DISABLE = InteruptType.sh4_exp |0x820,
		sh4_ex_DATA_ADDRESS_ERROR_READ =InteruptType.sh4_exp |0x0e0,
		sh4_ex_DATA_ADDRESS_ERROR_WRITE = InteruptType.sh4_exp | 0x100,
		sh4_ex_DATA_TLB_MISS_READ = InteruptType.sh4_exp | 0x040,
		sh4_ex_DATA_TLB_MISS_WRITE = InteruptType.sh4_exp | 0x060,
		sh4_ex_DATA_TLB_PROTECTION_VIOLATION_READ = InteruptType.sh4_exp | 0x0a0,
		sh4_ex_DATA_TLB_PROTECTION_VIOLATION_WRITE = InteruptType.sh4_exp | 0x0c0,
		sh4_ex_FPU = InteruptType.sh4_exp | 0x120,
		sh4_ex_TRAP = InteruptType.sh4_exp | 0x160,
		sh4_ex_INITAL_PAGE_WRITE = InteruptType.sh4_exp | 0x080,

		// asic9a /sh4 external holly normal [internal]
		holly_RENDER_DONE = InteruptType.holly_nrm | 0x02,
		holly_SCANINT1 = InteruptType.holly_nrm | 0x03,
		holly_SCANINT2 = InteruptType.holly_nrm | 0x04,
		holly_VBLank = InteruptType.holly_nrm | 0x05,
		holly_OPAQUE = InteruptType.holly_nrm | 0x07,
		holly_OPAQUEMOD = InteruptType.holly_nrm | 0x08,
		holly_TRANS = InteruptType.holly_nrm | 0x09,
		holly_TRANSMOD = InteruptType.holly_nrm | 0x0a,
		holly_MAPLE_DMA = InteruptType.holly_nrm | 0x0c,
		holly_MAPLE_ERR = InteruptType.holly_nrm | 0x0d,
		holly_GDROM_DMA = InteruptType.holly_nrm | 0x0e,
		holly_SPU_DMA = InteruptType.holly_nrm | 0x0f,
		holly_PVR_DMA = InteruptType.holly_nrm | 0x13,
		holly_PUNCHTHRU = InteruptType.holly_nrm | 0x15,

		// asic9c/sh4 external holly external [EXTERNAL]
		holly_GDROM_CMD = InteruptType.holly_ext | 0x00,
		holly_SPU_IRQ = InteruptType.holly_ext | 0x01,
		holly_EXP_8BIT = InteruptType.holly_ext | 0x02,
		holly_EXP_PCI = InteruptType.holly_ext | 0x03,

		// asic9b/sh4 external holly err only error [error]
		holly_PRIM_NOMEM = InteruptType.holly_err | 0x02,
		holly_MATR_NOMEM = InteruptType.holly_err | 0x03
	}


	public class Queue_sh4_int_:Queue 
	{
		public new sh4_int Dequeue()
		{
			return (sh4_int)base.Dequeue ();
		}
		public new sh4_int Peek()
		{
			return (sh4_int)base.Peek ();
		}
		public void Enqueue(sh4_int obj)
		{
			base.Enqueue (obj);
		}



	}

	/// <summary>
	/// Interups ,Exeptions ,Resets Handling
	/// </summary>
	public unsafe class intc
	{

		
		const int IPr_LVL6 = 0x6;
		const int IPr_LVL4 = 0x4;
		const int IPr_LVL2 = 0x2;

		static Queue_sh4_int_ pending_interups = new Queue_sh4_int_();

		public static bool zleeping = false, awake = false;
		//static int pvr_registered = 0;
		
		public static void UpdateIntExc(uint cycles)
		{
			check_ints();
		}

		public static void RaiseInterupt(sh4_int interupt)
		{
			pending_interups.Enqueue(interupt);
		}

		//static bool inside_int = false;

		static bool check_ints()
		{
			if (sh4.pc_funct != 0)
				return false;

			
			#region new code
			if (pending_interups.Count == 0)
				return false;

			sh4_int interupt=pending_interups.Dequeue();

			InteruptType type = (InteruptType)((uint)interupt & (uint)InteruptType.InteruptTypeMask);

			bool rv=false;

			switch (type)
			{
				case InteruptType.sh4_int:
					rv= HandleSH4_int(interupt);
					break;

				case InteruptType.sh4_exp:
					rv= HandleSH4_exept(interupt);
					break;

				case InteruptType.holly_nrm:
					rv= HandleHolly_nrm(interupt);
					break;

				case InteruptType.holly_ext:
					rv= HandleHolly_ext(interupt);
					break;

				case InteruptType.holly_err:
					rv= HandleHolly_err(interupt);
					break;
			}

			if (!rv)
				pending_interups.Enqueue(interupt);

			return rv;
/*
			if (((interupt >> 24) & 0xFF) != 0)
			{
				WriteLine("WARNING : OLNY INTERNAL INTERUPS ARE SUPORTED");
				return false;
			}

			uint* IST_reg = null, IML6_reg = null, IML4_reg = null, IML2_reg = null;

			uint interupt_type=(interupt >> 24) & 0xFF;

			switch (interupt_type)
			{
				case 0:		//normal
					IST_reg = ISTNRM;
					IML6_reg = IML6NRM;
					IML4_reg = IML4NRM;
					IML2_reg = IML2NRM;
					break;
				case 1:		//external
					IST_reg = ISTEXT;
					IML6_reg = IML6EXT;
					IML4_reg = IML4EXT;
					IML2_reg = IML2EXT;
					break;
				case 2:		//error
					IST_reg = ISTERR;
					IML6_reg = IML6ERR;
					IML4_reg = IML4ERR;
					IML2_reg = IML2ERR;
					break;
			}

			interupt =(uint)( 1 << ((int)(interupt & 0xFF)));
			*IST_reg |= interupt;

			if ((*IML6_reg & interupt) != 0)
			{
				if (intc(IPr_LVL6, interupt_type))
					return true;
			}
			else if ((*IML4_reg & interupt) != 0)
			{
				if (intc(IPr_LVL4, interupt_type))
					return true;
			}
			else if ((*IML2_reg & interupt) != 0)
			{
				if (intc(IPr_LVL2, interupt_type))
					return true;
			}
 * */
			#endregion

			//return false;
		}

		static bool HandleSH4_exept(sh4_int expt)
		{
			return false;
		}

		static bool HandleSH4_int(sh4_int intpt)
		{
			switch(intpt)
			{
				case  sh4_int.sh4_in_TMU0:
					return Do_interupt((uint)(((*mem.IPRA) >> 12)&0xf),0x400,0x600);
				case  sh4_int.sh4_in_TMU1:
					return Do_interupt((uint)(((*mem.IPRA) >> 8)&0xf),0x420,0x600);
				case  sh4_int.sh4_in_TMU2:
					return Do_interupt((uint)(((*mem.IPRA) >> 4)&0xf),0x440,0x600);
			}

			return false;
		}

		static bool HandleHolly_nrm(sh4_int intpt)
		{
			uint interupt = (uint)(1 << (((int)((uint)intpt & (uint)InteruptType.InteruptIDMask))));
			*mem.ISTNRM |= interupt;

			if ((*mem.IML6NRM & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL6, 0x320, 0x600))
					return true;
			}
			else if ((*mem.IML4NRM & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL4, 0x360, 0x600))
					return true;
			}
			else if ((*mem.IML2NRM & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL2, 0x3A0, 0x600))
					return true;
			}
			
			return false;
		}

		static bool HandleHolly_ext(sh4_int intpt)
		{
			uint interupt = (uint)(1 << ((int)((uint)intpt & (uint)InteruptType.InteruptIDMask)));
			*mem.ISTEXT |= interupt;

			if ((*mem.IML6EXT & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL6, 0x320, 0x600))
					return true;
			}
			else if ((*mem.IML4EXT & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL4, 0x360, 0x600))
					return true;
			}
			else if ((*mem.IML2EXT & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL2, 0x3a0, 0x600))
					return true;
			}
			return false;
		}

		static bool HandleHolly_err(sh4_int intpt)
		{
			uint interupt = (uint)(1 << ((int)((uint)intpt & (uint)InteruptType.InteruptIDMask)));
			*mem.ISTERR |= interupt;

			if ((*mem.IML6ERR & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL6, 0x320, 0x600))
					return true;
			}
			else if ((*mem.IML4ERR & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL4, 0x360, 0x600))
					return true;
			}
			else if ((*mem.IML2ERR & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL2, 0x3a0, 0x600))
					return true;
			}
			return false;
		}

		static bool Do_interupt(uint lvl, uint intEvn, uint CallVect)
		{

			if (!zleeping)
			{
				if (sh4.sr.BL == 1 || sh4.vbr == 0)
					return false;
			}

			//interupt disabled :)
			if (sh4.sr.IMASK == 0xf || lvl == 0)
				return false;

			//test interupt level
			if (sh4.sr.IMASK > lvl)
				return false;

			*mem.INTEVT = intEvn;

			if (zleeping)
				awake = true;

			sh4.ssr = sh4.sr.reg;
			sh4.spc = sh4.pc;
			sh4.sgr = sh4.r[15];
			sh4.sr.BL = 1;
			sh4.sr.MD = 1;
			sh4.sr.RB = 1;

			CallStackTrace.cstAddCall(sh4.pc, sh4.pc, sh4.vbr + 0x600, CallType.Interupt);

			sh4.pc = sh4.vbr + CallVect;
			
			return true;
		}

		static bool Do_Exeption(uint lvl, uint expEvn, uint CallVect)
		{

			if (!zleeping)
			{
				if (sh4.sr.BL == 1 || sh4.vbr == 0)
					return false;
			}

			if (sh4.sr.IMASK > lvl)
				return false;

			*mem.EXPEVT = expEvn;

			if (zleeping)
				awake = true;

			sh4.ssr = sh4.sr.reg;
			sh4.spc = sh4.pc;
			sh4.sgr = sh4.r[15];
			sh4.sr.BL = 1;
			sh4.sr.MD = 1;
			sh4.sr.RB = 1;

			CallStackTrace.cstAddCall(sh4.pc, sh4.pc, sh4.vbr + 0x600, CallType.Interupt);

			sh4.pc = sh4.vbr + CallVect;

			return true;
		}

	}
}
