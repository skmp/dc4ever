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
using System.Collections.Generic;

//SET_BIT(reg, bit) (reg |= (bit))
//REMOVE_BIT(reg, bit) (reg &= ~(bit))

namespace DC4Ever
{

	//ok , we have 3 types of interupts on the sh4
	//NMI,RL and Internal Module interupts
	//on the dreamcast the NMI interupts are not used
	//The Interupt input to RL is form the holly sub system (the output of the holly interupt controler)
	//The holly manages all external interupts , and it has 3 types of interupts normal , external and error


	/// <summary>
	/// Interups ,Exeptions ,Resets Handling
	/// </summary>
	public static unsafe partial class emu
	{

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

		const int IPr_LVL6 = 0x5;
		const int IPr_LVL4 = 0x3;
		const int IPr_LVL2 = 0x1;

		static Queue<sh4_int> pending_interups = new Queue<sh4_int>();

		static bool zleeping = false, awake = false;
		static int pvr_registered = 0;
		
		static void UpdateIntExc(uint cycles)
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
			if (pc_funct != 0)
				return false;

			
			#region new code
			if (pending_interups.Count == 0)
				return false;

			sh4_int interupt=pending_interups.Dequeue();

			InteruptType type = (InteruptType)((uint)interupt & (uint)InteruptType.InteruptTypeMask);



			switch (type)
			{
				case InteruptType.sh4_int:
					return HandleSH4_int((sh4_int)((uint)interupt));

				case InteruptType.sh4_exp:
					return HandleSH4_exept((sh4_int)((uint)interupt ));

				case InteruptType.holly_nrm:
					return HandleHolly_nrm((sh4_int)((uint)interupt));

				case InteruptType.holly_ext:
					return HandleHolly_ext((sh4_int)((uint)interupt));

				case InteruptType.holly_err:
					return HandleHolly_err((sh4_int)((uint)interupt ));
			}

			return false;
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
			return false;
		}

		static bool HandleHolly_nrm(sh4_int intpt)
		{
			uint interupt = (uint)(1 << (((int)((uint)intpt & (uint)InteruptType.InteruptIDMask))));
			*ISTNRM |= interupt;

			if ((*IML6NRM & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL6, 0x320, 0x600))
					return true;
			}
			else if ((*IML4NRM & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL4, 0x360, 0x600))
					return true;
			}
			else if ((*IML2NRM & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL2, 0x3A0, 0x600))
					return true;
			}
			
			return false;
		}

		static bool HandleHolly_ext(sh4_int intpt)
		{
			uint interupt = (uint)(1 << ((int)((uint)intpt & (uint)InteruptType.InteruptIDMask)));
			*ISTEXT |= interupt;

			if ((*IML6EXT & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL6, 0x320, 0x600))
					return true;
			}
			else if ((*IML4EXT & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL4, 0x360, 0x600))
					return true;
			}
			else if ((*IML2EXT & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL2, 0x3a0, 0x600))
					return true;
			}
			return false;
		}

		static bool HandleHolly_err(sh4_int intpt)
		{
			uint interupt = (uint)(1 << ((int)((uint)intpt & (uint)InteruptType.InteruptIDMask)));
			*ISTERR |= interupt;

			if ((*IML6ERR & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL6, 0x320, 0x600))
					return true;
			}
			else if ((*IML4ERR & interupt) != 0)
			{
				if (Do_interupt(IPr_LVL4, 0x360, 0x600))
					return true;
			}
			else if ((*IML2ERR & interupt) != 0)
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
				if (sr.BL == 1 || vbr == 0)
					return false;
			}

			//interupt disabled :)
			if (sr.IMASK == 0xf || lvl == 0)
				return false;

			//test interupt level
			if (sr.IMASK > lvl)
				return false;

			*INTEVT = intEvn;

			if (zleeping)
				awake = true;

			ssr = sr.reg;
			spc = pc;
			sgr = r[15];
			sr.BL = 1;
			sr.MD = 1;
			sr.RB = 1;

			cstAddCall(pc, pc, vbr + 0x600, CallType.Interupt);

			pc = vbr + CallVect;
			
			return true;
		}

		static bool Do_Exeption(uint lvl, uint expEvn, uint CallVect)
		{

			if (!zleeping)
			{
				if (sr.BL == 1 || vbr == 0)
					return false;
			}

			if (sr.IMASK > lvl)
				return false;

			*EXPEVT = expEvn;

			if (zleeping)
				awake = true;

			ssr = sr.reg;
			spc = pc;
			sgr = r[15];
			sr.BL = 1;
			sr.MD = 1;
			sr.RB = 1;

			cstAddCall(pc, pc, vbr + 0x600, CallType.Interupt);

			pc = vbr + CallVect;

			return true;
		}

	}
}
