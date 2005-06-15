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

//SET_BIT(reg, bit) (reg |= (bit))
//REMOVE_BIT(reg, bit) (reg &= ~(bit))

namespace DC4Ever
{
	/// <summary>
	/// Interups ,Exeptions ,Resets Handling
	/// </summary>
	public static unsafe partial class emu
	{
		enum sh4_expt
		{
			USER_BREAK_BEFORE_INSTRUCTION_EXECUTION = 0x1e0,
			INSTRUCTION_ADDRESS_ERROR = 0x0e0,
			INSTRUCTION_TLB_MISS = 0x040,
			INSTRUCTION_TLB_PROTECTION_VIOLATION = 0x0a0,
			GENERAL_ILLEGAL_INSTRUCTION = 0x180,
			SLOT_ILLEGAL_INSTRUCTION = 0x1a0,
			GENERAL_FPU_DISABLE = 0x800,
			SLOT_FPU_DISABLE = 0x820,
			DATA_ADDRESS_ERROR_READ = 0x0e0,
			DATA_ADDRESS_ERROR_WRITE = 0x100,
			DATA_TLB_MISS_READ = 0x040,
			DATA_TLB_MISS_WRITE = 0x060,
			DATA_TLB_PROTECTION_VIOLATION_READ = 0x0a0,
			DATA_TLB_PROTECTION_VIOLATION_WRITE = 0x0c0,
			FPU = 0x120,
			TRAP = 0x160,
			INITAL_PAGE_WRITE = 0x080
		}

		enum sh4_int
		{
			// for asic9a only
			RENDER_DONE = 1 << 0x02,
			SCANINT1 = 1 << 0x03,

			SCANINT2 = 1 << 0x04,
			VBLank = 1 << 0x05,
			OPAQUE = 1 << 0x07,
			OPAQUEMOD = 1 << 0x08,
			TRANS = 1 << 0x09,
			TRANSMOD = 1 << 0x0a,
			MAPLE_DMA = 1 << 0x0c,
			MAPLE_ERR = 1 << 0x0d,
			GDROM_DMA = 1 << 0x0e,
			SPU_DMA = 1 << 0x0f,
			PVR_DMA = 1 << 0x13,
			PUNCHTHRU = 1 << 0x15

			//// for asic9b only
			//PRIM_NOMEM = 0x0100 | 0x02,
			//MATR_NOMEM = 0x0100 | 0x03,

			//// for asic9c only
			//GDROM_CMD = 0x0200 | 0x00,
			//SPU_IRQ = 0x0200 | 0x01,
			//EXP_8BIT = 0x0200 | 0x02,
			//EXP_PCI = 0x0200 | 0x03
		}

		const int ASIC_IRQ9 = 1;
		const int ASIC_IRQB = 2;
		const int ASIC_IRQD = 3;

		//static int intdelay = 0;
		//static int intcnt = 0;
		static int pvr_registered = 0;
		static uint pending_interups=0;
		static void UpdateIntExc(uint cycles)
		{
			check_ints();
		}

		static void RaiseExecption(sh4_expt exept)
		{}
		static void RaiseInterupt(sh4_int interupt)
		{
			pending_interups |= (uint)interupt;
		}

		static bool inside_int = false;

		static bool check_ints()
		{
			if (pc_funct != 0)
				/* 	||  IS_SET(SR, SR_BL)
					||  VBR == 0) */
				return false;

				#region old code
/*
			//intdelay++;
				
			if (intdelay == 150000)
			{
				uint dw = 0;

				if (intcnt > 9)
					intcnt = 0;

				switch (intcnt)
				{
					case 0:
						dw = 1 << 3;	// ASIC_EVT_PVR_SCANINT1
						break;

					case 1:
						dw = 1 << 4;	// ASIC_EVT_PVR_SCANINT2
						break;

					case 2:
						if ((pvr_registered & (1 << 0))!=0)
							dw = 1 << 7;	// ASIC_EVT_PVR_OPAQUEDONE
						break;

					case 3:
						if ((pvr_registered & (1 << 1)) != 0)
							dw = 1 << 8;	// ASIC_EVT_PVR_OPAQUEMODDONE
						break;

					case 4:
						if ((pvr_registered & (1 << 2)) != 0)
							dw = 1 << 9;	// ASIC_EVT_PVR_TRANSDONE
						break;

					case 5:
						if ((pvr_registered & (1 << 3)) != 0)
							dw = 1 << 10;	// ASIC_EVT_PVR_TRANSMODDONE
						break;

					case 6:
						if ((pvr_registered & (1 << 4)) != 0)
							dw = 1 << 21;	// ASIC_EVT_PVR_PTDONE
						break;

					case 7:
						//			if (pvr_registering == -1)
						dw = 1 << 2;	// ASIC_EVT_PVR_RENDERDONE
						break;

					case 8:
						dw = 1 << 5;	// ASIC_EVT_PVR_VBLINT
						break;

					case 9:
						if (MapleDMAFinished)
						{
							dw = 1 << 12;				// maple dma complete
							MapleDMAFinished = false;	//interupt raised , clear the flag
						}
						break;
				}

				
				//intcnt++;
				//intdelay = 0;

				if (dw == 0)
					return;
				*/
				#endregion

			 if (pending_interups==0)
				 return false;

				//logxmsg(LOG_INTC, "seteando bit %x\n", dw);
				*ACK_A|= pending_interups;
				
				if ((*IRQ9_A & pending_interups)!=0)
				{
					//logxmsg(LOG_INTC, "intc: asic_irq9, %d\n", intcnt);
					if (intc(ASIC_IRQ9))
					{
						return true;
					}
				}
				if ((*IRQD_A & pending_interups)!=0)
				{
					//logxmsg(LOG_INTC, "intc: asic_irqd, %d\n", intcnt);
					if (intc(ASIC_IRQD))
					{
						return true;
					}
				}
				if ((*IRQB_A & pending_interups)!=0)
				{
					//logxmsg(LOG_INTC, "intc: asic_irqb, %d\n", intcnt);
					if (intc(ASIC_IRQB))
					{
						return true;
					}
				}

				pending_interups = 0;
				return false;
			}
		

		static bool intc(uint irq)
		{
			byte v=0;

			if (sr.BL==0 || vbr == 0)
				return false;

			if (inside_int == true)
			{
				//logmsg("llamando intc mientras se procesa otra int.\n");
				//		return;
			}

			/*	logmsg("antes:");
				dump_registers(); */

			// a revisar por int's pendientes
			//logmsg("imask: %x, VBR: %x\r\n", SR_GET_IMASK(), VBR);

			switch (irq)
			{
				case ASIC_IRQ9: v = 0x9; break;
				case ASIC_IRQB: v = 0xb; break;
				case ASIC_IRQD: v = 0xd; break;
				default: /*logmsg("ERROR: IRQ NO RECONOCIDA!"); abort();*/ break;
			}

			if (sr.IMASK > v)
			{
				//logmsg("imask > irq, retornando.\n");
				return false;
			}

			switch (irq)
			{
				case ASIC_IRQ9: *INTEVT = 0x320; break;
				case ASIC_IRQB: *INTEVT = 0x360; break;
				case ASIC_IRQD: *INTEVT = 0x3a0; break;
			}

			ssr = sr.reg;
			spc = pc;
			sgr = r[15];
			//SET_BIT(SR, SR_BL);
			sr.BL = 1;
			//SET_BIT(SR, SR_MD);
			sr.MD = 1;
			//SET_BIT(SR, SR_RB);
			sr.RB = 1;

			cstAddCall(pc, pc, vbr + 0x600, CallType.Interupt);

			pc = vbr + 0x600;
			
			//logmsg("intc: saltando a %x, con registros:\n", PC);
			//dump_registers();
			inside_int = true;
			//	filelogging = FILELOG_CALLS;
			return true;
		}


	}
}
