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
    //0xxx -  finished nimpl
    public static unsafe partial class emu
    {
		static void iInvalidOpcode()
		{
			dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
			DoEvents();
            runsh = false;
        }

        static void iNimp(string info)
        {
            dc.dcon.WriteLine("Warning:Not implemented opcode: "+info+", at pc "+ System.Convert.ToString(pc, 16).ToUpper() + " with code " + System.Convert.ToString(opcode, 16).ToUpper());
            DoEvents();
            //runsh = false;
        }

        //stc SR,<REG_N>                // no implementation
		static void i0000_nnnn_0000_0010()//0002
		{
			uint n = (opcode >> 8) & 0x0F;
            r[n] = sr.reg;
        }

		
		//stc GBR,<REG_N>               // no implementation
		static void i0000_nnnn_0001_0010()
		{
            //iNimp("stc GBR,<REG_N>");
			uint n = (opcode >> 8) & 0xf;
			r[n] = gbr;
		} 
		

		//stc VBR,<REG_N>               // no implementation
		static void i0000_nnnn_0010_0010()
		{
            //iNimp("stc VBR,<REG_N>  ");
			uint n = (opcode >> 8) & 0x0F;
			r[n] = vbr;
        } 
		

		//stc SSR,<REG_N>               // no implementation
		static void i0000_nnnn_0011_0010()
		{
            //iNimp("stc SSR,<REG_N>");
			uint n = (opcode >> 8) & 0xf;
			r[n] = ssr;
        } 
		

		//stc SPC,<REG_N>               // no implementation
		static void i0000_nnnn_0100_0010()
		{
            //iNimp("stc SPC,<REG_N> ");
			uint n = (opcode >> 8) & 0xf;
			r[n] = spc;
        } 
		

		//stc R0_BANK,<REG_N>           // no implementation
		static void i0000_nnnn_1000_0010()
		{
            //iNimp("stc R0_BANK,<REG_N");
			uint n = (opcode >> 8) & 0xf;
			r[n] = r_bank[0];
        } 
		

		//stc R1_BANK,<REG_N>           // no implementation
		static void i0000_nnnn_1001_0010()
		{
            //iNimp("stc R1_BANK,<REG_N>");
			uint n = (opcode >> 8) & 0xf;
			r[n] = r_bank[1];
        } 


		//stc R2_BANK,<REG_N>           // no implementation
		static void i0000_nnnn_1010_0010()
		{
            //iNimp("stc R2_BANK,<REG_N>");
			uint n = (opcode >> 8) & 0xf;
			r[n] = r_bank[2];
        } 


		//stc R3_BANK,<REG_N>           // no implementation
		static void i0000_nnnn_1011_0010()
		{//TODO : !Add this
            //iNimp("stc R3_BANK,<REG_N> ");
			uint n = (opcode >> 8) & 0xf;
			r[n] = r_bank[3];
        } 


		//stc R4_BANK,<REG_N>           // no implementation
		static void i0000_nnnn_1100_0010()
		{
            //iNimp("stc R4_BANK,<REG_N>");
			uint n = (opcode >> 8) & 0xf;
			r[n] = r_bank[4];
        } 


		//stc R5_BANK,<REG_N>           // no implementation
		static void i0000_nnnn_1101_0010()
		{
            //iNimp("stc R5_BANK,<REG_N>");
			uint n = (opcode >> 8) & 0xf;
			r[n] = r_bank[5];
        } 


		//stc R6_BANK,<REG_N>           // no implementation
		static void i0000_nnnn_1110_0010()
		{
            //iNimp("stc R6_BANK,<REG_N>");
			uint n = (opcode >> 8) & 0xf;
			r[n] = r_bank[6];
        } 


		//stc R7_BANK,<REG_N>           // no implementation
		static void i0000_nnnn_1111_0010()
		{
            //iNimp("stc R7_BANK,<REG_N>");
			uint n = (opcode >> 8) & 0xf;
			r[n] = r_bank[7];
        } 


		//braf <REG_N>                  // no implementation
		static void i0000_nnnn_0010_0011()
		{
			uint n = (opcode >> 8) & 0x0F;
            delayslot = r[n] + pc + 4;
            pc_funct = 2;//delay 2
        } 


		//bsrf <REG_N>                  // no implementation
		static void i0000_nnnn_0000_0011()
		{
            //iNimp("bsrf <REG_N>");
			uint n = (opcode >> 8) & 0x0F;
            delayslot = r[n] + pc + 4;

            pr = pc + 4;

			cstAddCall(pc, pr, delayslot, CallType.Normal);
            pc_funct = 2;//delay 2
        } 


		//movca.l R0, @<REG_N>          // no implementation
		static void i0000_nnnn_1100_0011()
		{
            iNimp("movca.l R0, @<REG_N>");
        } 


		//ocbi @<REG_N>                 // no implementation
		static void i0000_nnnn_1001_0011()
		{
            iNimp("ocbi @<REG_N> ");
        } 


		//ocbp @<REG_N>                 // no implementation
		static void i0000_nnnn_1010_0011()
		{
            iNimp("ocbp @<REG_N> ");
        } 


		//ocbwb @<REG_N>                // no implementation
		static void i0000_nnnn_1011_0011()
		{
            iNimp("ocbwb @<REG_N> ");
        } 


		//pref @<REG_N>                 // no implementation
		static void i0000_nnnn_1000_0011()
		{//TODO : !Add this
			//iNimp("pref @<REG_N>");
			uint Dest = r[(opcode >>8)&0xf];

			if ((Dest & 0xFC000000) == 0xE0000000) //Store Queue
			{
				uint* sq;
				uint Address, QACR;
				if (((Dest >> 5) & 0x1) == 0)
				{
					sq = sq0_dw;
					QACR = *QACR0;
				}
				else
				{
					sq = sq1_dw;
					QACR = *QACR1;
				}

				Address = (Dest & 0x03FFFFE0) | (((QACR & 0x1C) >> 2) << 26);//ie:(QACR&0x1c>>2)<<26

				if (Address < 0x11000000)//??
				{

					for (uint i = 0; i < 8; i++)
						write((Address + (i << 2)), sq[i], 4);
					if (((Address >> 26) & 0x7) == 4)
						ProccessTaSQWrite(Address);
				}

			}


		}


		//mov.b <REG_M>,@(R0,<REG_N>)   // no implementation
		static void i0000_nnnn_mmmm_0100()
        {//ToDo : Check This [26/4/05]
            //iNimp("mov.b <REG_M>,@(R0,<REG_N>)");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            //BYTE valor = (BYTE)(R(m) & 0xFF);
            write(r[0] + r[n], r[m] & 0xFF,1);
            //WriteMemoryB(R(0) + R(n), &valor);
        }


		//mov.w <REG_M>,@(R0,<REG_N>)   // no implementation
		static void i0000_nnnn_mmmm_0101()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            write(r[0] + r[n], r[m] & 0xFFFF, 2);
        }


		//mov.l <REG_M>,@(R0,<REG_N>)   // no implementation
		static void i0000_nnnn_mmmm_0110()
        {//TODO : Check This [26/4/05]
            //iNimp("mov.l <REG_M>,@(R0,<REG_N>)");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            write(r[0] + r[n], r[m], 4);
            //WriteMemoryL(R(0) + R(n), (DWORD*)&R(m));
        }


		//mul.l <REG_M>,<REG_N>         // no implementation
		static void i0000_nnnn_mmmm_0111()
		{//TODO : CHECK THIS
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            //macl = (uint)(((int)r[n] * (int)r[m]) & 0xFFFFFFFF);
            macl = (uint)((((long)r[n]) * ((long)r[m])) & 0xFFFFFFFF);
        }


		//clrmac                        // no implementation
		static void i0000_0000_0010_1000()
		{
            iNimp("clrmac");
        } 


		//clrs                          // no implementation
		static void i0000_0000_0100_1000()
		{
            iNimp("clrs");
        } 


		//clrt                          // no implementation
		static void i0000_0000_0000_1000()
		{
            iNimp("clrt");
        } 


		//ldtlb                         // no implementation
		static void i0000_0000_0011_1000()
		{
            iNimp("ldtlb");
        } 


		//sets                          // no implementation
		static void i0000_0000_0101_1000()
		{
            iNimp("sets");
			sr.S = 1;
        } 


		//sett                          // no implementation
		static void i0000_0000_0001_1000()
		{
            //iNimp("sett");
			sr.T = 1;
        } 


		//div0u                         // no implementation
        static void i0000_0000_0001_1001()
        {//ToDo : Check This [26/4/05]
            //iNimp("div0u");
            sr.Q = 0;
            sr.M = 0;
            sr.T = 0;
        }


        //movt <REG_N>                  // no implementation
		static void i0000_nnnn_0010_1001()
		{
            //iNimp("movt <REG_N>");
			uint n = (opcode >> 8) & 0x0F;
            r[n] = sr.T;
        } 


		//nop                           // no implementation
		static void i0000_0000_0000_1001()
		{
            //no operation xD XD .. i just love this opcode ..
		} 


		//sts FPUL,<REG_N>              // no implementation
		static void i0000_nnnn_0101_1010()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			r[n] = fpul;
		} 


		//sts FPSCR,<REG_N>             // no implementation
		static void i0000_nnnn_0110_1010()
		{
            //iNimp("sts FPSCR,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
            r[n] = fpscr.reg;
        } 


		//sts MACH,<REG_N>              // no implementation
		static void i0000_nnnn_0000_1010()
		{
            //iNimp("sts MACH,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;

			r[n] = mach;
        } 


		//sts MACL,<REG_N>              // no implementation
		static void i0000_nnnn_0001_1010()
		{//TODO : CHECK THIS
			uint n = (opcode >> 8) & 0x0F;
			r[n]=macl; 
		} 


		//sts PR,<REG_N>                // no implementation
		static void i0000_nnnn_0010_1010()
		{
            //iNimp("sts PR,<REG_N>");
			uint n = (opcode >> 8) & 0xf;
			r[n] = pr;
        } 


		//rte                           // no implementation
		static void i0000_0000_0010_1011()
		{
            //iNimp("rte");
			sr.reg = ssr;
			delayslot = spc;
			cstRemCall(spc);
			pc_funct = 2;
        } 


		//rts                           // no implementation
		static void i0000_0000_0000_1011()
		{
            delayslot = pr;
			cstRemCall(pr);
            pc_funct = 2;
        } 


		//sleep                         // no implementation
		static void i0000_0000_0001_1011()
		{
            iNimp("sleep");
        } 


		//mov.b @(R0,<REG_M>),<REG_N>   // no implementation
		static void i0000_nnnn_mmmm_1100()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			r[n] = (uint)(sbyte)read(r[0]+r[m],1);
		} 


		//mov.w @(R0,<REG_M>),<REG_N>   // no implementation
		static void i0000_nnnn_mmmm_1101()
        {//ToDo : Check This [26/4/05]
            //iNimp("mov.w @(R0,<REG_M>),<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            r[n] = (uint)(short)read(r[0] + r[m], 2);
        } 


		//mov.l @(R0,<REG_M>),<REG_N>   // no implementation
		static void i0000_nnnn_mmmm_1110()
        {//TODO : Check This [26/4/05]
            //iNimp("mov.l @(R0,<REG_M>),<REG_N>");
			uint n = (opcode >> 8) & 0xf;
			uint m = (opcode >> 4) & 0xf;
			r[n] = read(r[0] + r[m], 4);
        } 


		//mac.l @<REG_M>+,@<REG_N>+     // no implementation
        static void i0000_nnnn_mmmm_1111()
        {//TODO : !Add this
            //iNimp("mac.l @<REG_M>+,@<REG_N>+");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            int rm, rn;
            long mul, mac, result;


            //memread(R(m), &rm, sizeof(DWORD));
            rm = (int)read(r[m], 4);
            //memread(R(n), &rn, sizeof(DWORD));
            rn = (int)read(r[n], 4);


            r[m] += 4;
            r[n] += 4;

            mul = rm * rn;
            mac = (long)(((ulong)mach << 32) + macl);

            result = mac + mul;

            macl = (uint)(result & 0xFFFFFFFF);
            mach = (uint)((result >> 32) & 0xFFFFFFFF);
        }

    }
    //1xxx
    public static unsafe partial class emu 
    {
		//mov.l <REG_M>,@(<disp>,<REG_N>)
		static void i0001_nnnn_mmmm_iiii()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			uint disp = opcode & 0x0F;
            write(r[n] + (disp <<2), r[m], 4);
        }
	}
    //2xxx
    public static unsafe partial class emu 
    {
		//mov.b <REG_M>,@<REG_N>        // no implemetation
		static void i0010_nnnn_mmmm_0000()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			write(r[n],r[m] & 0xFF,1);
		}

		// mov.w <REG_M>,@<REG_N>        // no implemetation
		static void i0010_nnnn_mmmm_0001()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			write (r[n],r[m] & 0xFFFF,2);
		}

		// mov.l <REG_M>,@<REG_N>        // no implemetation
		static void i0010_nnnn_mmmm_0010()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			write(r[n],r[m],4);//at r[n],r[m]
		}

		// mov.b <REG_M>,@-<REG_N>       // no implemetation
		static void i0010_nnnn_mmmm_0100()
		{
            //iNimp("mov.b <REG_M>,@-<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            r[n]--;
            write(r[n],r[m] & 0xFF,1);
            //WriteMemoryB(r[n], R(m) & 0xFF);

        }

		//mov.w <REG_M>,@-<REG_N>       // no implemetation
		static void i0010_nnnn_mmmm_0101 ()
		{
            //iNimp("mov.w <REG_M>,@-<REG_N>");
			uint n = (opcode >> 8) & 0xF;
			uint m = (opcode >> 4) & 0xF;
            r[n] -= 2;
            write(r[n], r[m], 2);
        }

		//mov.l <REG_M>,@-<REG_N>       // no implemetation
		static void i0010_nnnn_mmmm_0110 ()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			r[n] -= 4;
			write (r[n],r[m],4);
		}
		// div0s <REG_M>,<REG_N>         // no implemetation
		static void i0010_nnnn_mmmm_0111()
        {//ToDo : Check This [26/4/05]
            //iNimp("div0s <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

            if ((r[n] & 0x80000000)!=0)
                //SET_BIT(SR, SR_Q);
                sr.Q = 1;
            else
                sr.Q = 0;
                //REMOVE_BIT(SR, SR_Q);

            if ((r[m] & 0x80000000)!=0)
                //SET_BIT(SR, SR_M);
                sr.M = 1;
            else
                sr.M = 0;
                //REMOVE_BIT(SR, SR_M);

            //f ((IS_SR_Q() && IS_SR_M()) || (!IS_SR_Q() && !IS_SR_M()))
            if (sr.Q == sr.M)
                //REMOVE_BIT(SR, SR_T);
                sr.T = 0;
            else
                sr.T = 1;
                //SET_BIT(SR, SR_T);
        }

		// tst <REG_M>,<REG_N>           // no implemetation
		static void i0010_nnnn_mmmm_1000()
        {//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

			if ((r[n] & r[m])!=0)
				sr.T=0;
					else
				sr.T=1;

		}

		//and <REG_M>,<REG_N>           // no implemetation
		static void i0010_nnnn_mmmm_1001 ()
        {//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			r[n] &= r[m];
		}

		//xor <REG_M>,<REG_N>           // no implemetation
		static void i0010_nnnn_mmmm_1010 ()
        {//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			r[n] ^= r[m];
		}

		//or <REG_M>,<REG_N>            // no implemetation
		static void i0010_nnnn_mmmm_1011 ()
        {//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;	
			r[n] |= r[m];
		}

		//cmp/str <REG_M>,<REG_N>       // no implemetation
        static void i0010_nnnn_mmmm_1100()
        {//TODO : Check This [26/4/05]
            //iNimp("cmp/str <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

            uint temp;
            uint HH, HL, LH, LL;

            temp = r[n] ^ r[m];
            HH = (temp & 0xFF000000) >> 12;
            HL = (temp & 0x00FF0000) >> 8;
            LH = (temp & 0x0000FF00) >> 4;
            LL = (temp & 0x000000FF);

            if (!((HH != 0) && (HL != 0) && (LH != 0) && (LL != 0)))
                sr.T = 1;
            else
                sr.T = 0;
        }

        //xtrct <REG_M>,<REG_N>         // no implemetation
		static void i0010_nnnn_mmmm_1101 ()
		{//TODO: unsore of proper emulation [26/4/05]
            //iNimp("xtrct <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

            r[n] = ((r[n] >> 16) & 0xFFFF) | ((r[m] << 16) & 0xFFFF0000);
        }

		//mulu <REG_M>,<REG_N>          // no implemetation
		static void i0010_nnnn_mmmm_1110 ()
		{
            //iNimp("mulu.w <REG_M>,<REG_N>");//check  ++
			uint n = (opcode >> 8) & 0xF;
			uint m = (opcode >> 4) & 0xF;
            macl=((uint)(ushort)r[n])*
                 ((uint)(ushort)r[m]);
        }

		//muls <REG_M>,<REG_N>          // no implemetation
		static void i0010_nnnn_mmmm_1111 ()
        {//TODO : Check This [26/4/05]
            //iNimp("muls <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

            macl = (uint)((int)(r[n] & 0xFFFF) * (int)(r[m] & 0xFFFF));
        }
	}
    //3xxx 
    public static unsafe partial class emu
    {
		// cmp/eq <REG_M>,<REG_N>        // no implemetation
        static void i0011_nnnn_mmmm_0000()
        {
            //iNimp("cmp/eq <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

            if (r[m] == r[n])
                sr.T = 1;
            else
                sr.T = 0;
        }

        // cmp/hs <REG_M>,<REG_N>        // no implemetation
		static void i0011_nnnn_mmmm_0010()
        {//ToDo : Check Me [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			if (r[n] >= r[m])
				sr.T=1;
			else
				sr.T=0;
		}

		//cmp/ge <REG_M>,<REG_N>        // no implemetation
		static void i0011_nnnn_mmmm_0011 ()
        {//TODO : Check This [26/4/05]
            //iNimp("cmp/ge <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            if ((int)r[n] >= (int)r[m])
                sr.T = 1;
            else 
                sr.T = 0;
        }

		//div1 <REG_M>,<REG_N>          // no implemetation
        static void i0011_nnnn_mmmm_0100()
        {//ToDo : Check This [26/4/05]
            //iNimp("div1 <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			uint old_q, tmp0, tmp1;
            #region nasty code ..
            old_q = sr.Q;
            if ((r[n] & 0x80000000)!=0)
                sr.Q = 1;//SET_BIT(SR, SR_Q);
            else
                sr.Q = 0;//REMOVE_BIT(SR, SR_Q);
            //    Q=(unsigned char)((0x80000000 & R[n])!=0);
            r[n] <<= 1;

            //if (sr.T==1)
            r[n] |= sr.T;

            switch (old_q)
            {
                case 0:
                    switch (sr.M)
                    {
                        case 0:
                            tmp0 = r[n];
                            r[n] -= r[m];
                            tmp1 = (r[n] > tmp0) ? (uint)1 : 0;
                            switch (sr.Q)
                            {
                                case 0:
                                    sr.Q = tmp1;
                                    /*if (tmp1)
                                        sr.Q = 1;//(SR, SR_Q);
                                    else
                                        sr.Q = 0;//REMOVE_BIT(SR, SR_Q);
//                Q=tmp1;*/
                                    break;

                                case 1:
//                Q=(unsigned char)(tmp1==0);
                                    if (tmp1 == 0)
                                        sr.Q = 1;//SET_BIT(SR, SR_Q);
                                    else
                                        sr.Q = 0;//REMOVE_BIT(SR, SR_Q);
                                    break;
                            }
                            break;

                        case 1:
                            tmp0 = r[n];
                            r[n] += r[m];
                            tmp1 = (r[n] < tmp0) ? (uint)1 : 0;
                            switch (sr.Q)
                            {
                                case 0:
//                Q=(unsigned char)(tmp1==0);
                                    if (tmp1 == 0)
                                        sr.Q = 1;//SET_BIT(SR, SR_Q);
                                    else
                                        sr.Q = 0;//REMOVE_BIT(SR, SR_Q);
                                    break;

                                case 1:
//                Q=tmp1;
                                    if (tmp1==1)
                                        sr.Q = 1;//SET_BIT(SR, SR_Q);
                                    else
                                        sr.Q = 0;//REMOVE_BIT(SR, SR_Q);
                                    break;
                            }
                            break;
                    }
                    break;

                case 1:
                    switch (sr.M)
                    {
                        case 0:
                            tmp0 = r[n];
                            r[n] += r[m];
                            tmp1 = (r[n] < tmp0) ? (uint)1 : 0;

                            switch (sr.Q)
                            {
                                case 0:
//                Q=tmp1;
                                    if (tmp1==1)
                                        sr.Q = 1;//SET_BIT(SR, SR_Q);
                                    else
                                        sr.Q = 0;//REMOVE_BIT(SR, SR_Q);
                                    break;

                                case 1:
//                Q=(unsigned char)(tmp1==0);
                                    if (tmp1 == 0)
                                        sr.Q = 1;//SET_BIT(SR, SR_Q);
                                    else
                                        sr.Q = 0;//REMOVE_BIT(SR, SR_Q);
                                    break;
                            }
                            break;

                        case 1:
                            tmp0 = r[n];
                            r[n] -= r[m];
                            tmp1 = (r[n] > tmp0) ? (uint)1 : 0;
                            switch (sr.Q)
                            {
                                case 0:
//                Q=(unsigned char)(tmp1==0);
                                    if (tmp1 == 0)
                                        sr.Q = 1;//SET_BIT(SR, SR_Q);
                                    else
                                        sr.Q = 0;//REMOVE_BIT(SR, SR_Q);
                                    break;

                                case 1:
                                    if (tmp1==1)
                                        sr.Q = 1;//SET_BIT(SR, SR_Q);
                                    else
                                        sr.Q = 0;//REMOVE_BIT(SR, SR_Q);
//                Q=tmp1;
                                    break;
                            }
                            break;
                    }
                    break;
            }
            #endregion
            //IS_SR_Q() && IS_SR_M()
            if (sr.Q ==1 && sr.M==1)
                sr.T = 1;//SET_BIT(SR, SR_T);
            else
                sr.T = 0;//REMOVE_BIT(SR, SR_T);
        }

        //dmulu.l <REG_M>,<REG_N>       // no implemetation
        static void i0011_nnnn_mmmm_0101()
        {
            //iNimp("dmulu.l <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            ulong x;

            x = (ulong)r[n] * (ulong)r[m];

            macl = (uint)(x & 0xFFFFFFFF);
            mach = (uint)((x >> 32) & 0xFFFFFFFF);
        }

        // cmp/hi <REG_M>,<REG_N>        // no implemetation
		static void i0011_nnnn_mmmm_0110()
		{
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

			if (r[n] > r[m])
				sr.T=1;
			else
				sr.T=0;
		}

		//cmp/gt <REG_M>,<REG_N>        // no implemetation
		static void i0011_nnnn_mmmm_0111 ()
        {//TODO : Check This [26/4/05]
            //iNimp("cmp/gt <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

            if (((int)r[n]) > ((int)r[m]))
                sr.T = 1;
            else 
                sr.T = 0;
        }

		// sub <REG_M>,<REG_N>           // no implemetation
		static void i0011_nnnn_mmmm_1000()
		{
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			//rn=(int)r[n];
			//rm=(int)r[m];
            r[n] =(uint)(((int)r[n])-((int)r[m]));
            //r[n]=(uint)rn;
		}

		//subc <REG_M>,<REG_N>          // no implemetation
        static void i0011_nnnn_mmmm_1010()
        {//ToDo : Check This [26/4/05]
            //iNimp("subc <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			
            //	R(n) = R(n) - R(m) - (IS_SR_T() ? 1 : 0);
            //	tmp1 = (signed) R(n) - (signed) R(m);
			uint tmp1 = (uint)(((int)r[n]) - ((int)r[m]));
			uint tmp0 = r[n];
            r[n] = tmp1 - sr.T;
            if (tmp0 < tmp1)
            {
                sr.T=1;
            }
            else
            {
                sr.T=0;
            }
            if (tmp1 < r[n])
            {
                sr.T=1;
            }
        }

        //subv <REG_M>,<REG_N>          // no implemetation
		static void i0011_nnnn_mmmm_1011 ()
		{
            iNimp("subv <REG_M>,<REG_N>");
        }

		//add <REG_M>,<REG_N>           // no implemetation
		static void i0011_nnnn_mmmm_1100 ()
		{
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            r[n] =(uint)(((int)r[n]) + ((int)r[m]));
        }

		//dmuls.l <REG_M>,<REG_N>       // no implemetation
        static void i0011_nnnn_mmmm_1101()
        {
            //iNimp("dmuls.l <REG_M>,<REG_N>");//check ++
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            long x;

            x = (long)(int)r[n] * (long)(int)r[m];

            macl = (uint)(x & 0xFFFFFFFF);
            mach = (uint)((x >> 32) & 0xFFFFFFFF);
        }

        //addc <REG_M>,<REG_N>          // no implemetation
		static void i0011_nnnn_mmmm_1110 ()
        {//ToDo : Check This [26/4/05]
            //iNimp("addc <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            uint tmp1 = r[n] + r[m];
            uint tmp0 = r[n];

            r[n] = tmp1 + sr.T;

            if (tmp0 > tmp1)
                sr.T=1;
                //SET_BIT(SR, SR_T);
            else
                sr.T = 0;
               //REMOVE_BIT(SR, SR_T);

            if (tmp1 > r[n])
                sr.T = 1;
                //SET_BIT(SR, SR_T);

        }

		// addv <REG_M>,<REG_N>          // no implemetation
		static void i0011_nnnn_mmmm_1111()
		{
            iNimp("addv <REG_M>,<REG_N>");
        }
	}
    //4xxx  
    public static unsafe partial class emu
    {
		//sts.l FPUL,@-<REG_N>          // no implemetation
		static void i0100_nnnn_0101_0010()
		{
            //iNimp("sts.l FPUL,@-<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
            r[n] -= 4;
            write(r[n], fpul,4);
        }
		

		//sts.l FPSCR,@-<REG_N>         // no implemetation
		static void i0100_nnnn_0110_0010()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			r[n] -= 4;
			write(r[n],fpscr.reg,4);
		}
		

		//sts.l MACH,@-<REG_N>          // no implemetation
		static void i0100_nnnn_0000_0010()
		{
            //iNimp("sts.l MACH,@-<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			r[n] -= 4;
			write(r[n], mach, 4);
        }
		

		//sts.l MACL,@-<REG_N>          // no implemetation
		static void i0100_nnnn_0001_0010()
		{
            //iNimp("sts.l MACL,@-<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			r[n] -= 4;
			write(r[n], macl, 4);
        }
		

		//sts.l PR,@-<REG_N>            // no implemetation
		static void i0100_nnnn_0010_0010()
		{//TODO : fAdd this
			uint n = (opcode >> 8) & 0x0F;
			r[n] -= 4;
			write(r[n],pr,4);
		}
		

		//stc R0_BANK,@-<REG_N>         // no implemetation
		static void i0100_nnnn_1000_0010()
		{
            iNimp("stc R0_BANK,@-<REG_N>");
        }
		

		//stc R1_BANK,@-<REG_N>         // no implemetation
		static void i0100_nnnn_1001_0010()
		{
            iNimp("stc R1_BANK,@-<REG_N>");
        }
		

		//stc R2_BANK,@-<REG_N>         // no implemetation
		static void i0100_nnnn_1010_0010()
		{
            iNimp("stc R2_BANK,@-<REG_N>");
        }
		

		//stc R3_BANK,@-<REG_N>         // no implemetation
		static void i0100_nnnn_1011_0010()
		{
            iNimp("stc R3_BANK,@-<REG_N>");
        }
		

		//stc R4_BANK,@-<REG_N>         // no implemetation
		static void i0100_nnnn_1100_0010()
		{
            iNimp("stc R4_BANK,@-<REG_N>");
        }
		

		//stc R5_BANK,@-<REG_N>         // no implemetation
		static void i0100_nnnn_1101_0010()
		{
            iNimp("stc R5_BANK,@-<REG_N>");
        }
		

		//stc R6_BANK,@-<REG_N>         // no implemetation
		static void i0100_nnnn_1110_0010()
		{
            iNimp("stc R6_BANK,@-<REG_N>");
        }
		

		//stc R7_BANK,@-<REG_N>         // no implemetation
		static void i0100_nnnn_1111_0010()
		{
            iNimp("stc R7_BANK,@-<REG_N>");
        }
		

		//stc.l SR,@-<REG_N>            // no implemetation
		static void i0100_nnnn_0000_0011()
		{
            iNimp("stc.l SR,@-<REG_N>");
        }
		

		//stc.l GBR,@-<REG_N>           // no implemetation
		static void i0100_nnnn_0001_0011()
		{
            iNimp("stc.l GBR,@-<REG_N>");
        }
		

		//stc.l VBR,@-<REG_N>           // no implemetation
		static void i0100_nnnn_0010_0011()
		{
            iNimp("stc.l VBR,@-<REG_N>");
        }
		

		//stc.l SSR,@-<REG_N>           // no implemetation
		static void i0100_nnnn_0011_0011()
		{
            iNimp("stc.l SSR,@-<REG_N>");
        }
		

		//stc.l SPC,@-<REG_N>           // no implemetation
		static void i0100_nnnn_0100_0011()
		{
            iNimp("stc.l SPC,@-<REG_N>");
        }
		

		//lds.l @<REG_N>+,MACH          // no implemetation
		static void i0100_nnnn_0000_0110()
		{
            //iNimp("lds.l @<REG_N>+,MACH");
			uint n = (opcode >> 8) & 0x0F;
			mach = read(r[n], 4);
			r[n] += 4;
        }
		

		//lds.l @<REG_N>+,MACL          // no implemetation
		static void i0100_nnnn_0001_0110()
		{
            //iNimp("lds.l @<REG_N>+,MACL ");
			uint n = (opcode >> 8) & 0x0F;
			macl = read(r[n], 4);
			r[n] += 4;
        }
		

		//lds.l @<REG_N>+,PR            // no implemetation
		static void i0100_nnnn_0010_0110()
		{//TODO : hADD THIS
			uint n = (opcode >> 8) & 0x0F;
			pr = read (r[n],4);
			r[n] += 4;
		}
		

		//lds.l @<REG_N>+,FPUL          // no implemetation
		static void i0100_nnnn_0101_0110()
		{
            //iNimp("lds.l @<REG_N>+,FPUL");
			uint n = (opcode >> 8) & 0x0F;
            fpul = read(r[n], 4);
            r[n] += 4;
        }
		

		//lds.l @<REG_N>+,FPSCR         // no implemetation
		static void i0100_nnnn_0110_0110()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			fpscr.reg =read (r[n],4);
			r[n] += 4;
		}
		

		//ldc.l @<REG_N>+,SR            // no implemetation
		static void i0100_nnnn_0000_0111()
		{
            iNimp("ldc.l @<REG_N>+,SR");
        }
		

		//ldc.l @<REG_N>+,GBR           // no implemetation
		static void i0100_nnnn_0001_0111()
		{
            iNimp("ldc.l @<REG_N>+,GBR");
        }
		

		//ldc.l @<REG_N>+,VBR           // no implemetation
		static void i0100_nnnn_0010_0111()
		{
            iNimp("ldc.l @<REG_N>+,VBR");
        }
		

		//ldc.l @<REG_N>+,SSR           // no implemetation
		static void i0100_nnnn_0011_0111()
		{
            iNimp("ldc.l @<REG_N>+,SSR");
        }
		

		//ldc.l @<REG_N>+,SPC           // no implemetation
		static void i0100_nnnn_0100_0111()
		{
            iNimp("ldc.l @<REG_N>+,SPC");
        }
		

		//ldc.l @<REG_N>+,R0_BANK       // no implemetation
		static void i0100_nnnn_1000_0111()
		{
            iNimp("ldc.l @<REG_N>+,R0_BANK");
        }
		

		//ldc.l @<REG_N>+,R1_BANK       // no implemetation
		static void i0100_nnnn_1001_0111()
		{
            iNimp("ldc.l @<REG_N>+,R1_BANK");
        }
		

		//ldc.l @<REG_N>+,R2_BANK       // no implemetation
		static void i0100_nnnn_1010_0111()
		{
            iNimp("ldc.l @<REG_N>+,R2_BANK");
        }
		

		//ldc.l @<REG_N>+,R3_BANK       // no implemetation
		static void i0100_nnnn_1011_0111()
		{
            iNimp("ldc.l @<REG_N>+,R3_BANK");
        }
		

		//ldc.l @<REG_N>+,R4_BANK       // no implemetation
		static void i0100_nnnn_1100_0111()
		{
            iNimp("ldc.l @<REG_N>+,R4_BANK");
        }
		

		//ldc.l @<REG_N>+,R5_BANK       // no implemetation
		static void i0100_nnnn_1101_0111()
		{
            iNimp("ldc.l @<REG_N>+,R5_BANK ");
        }
		

		//ldc.l @<REG_N>+,R6_BANK       // no implemetation
		static void i0100_nnnn_1110_0111()
		{
            iNimp("ldc.l @<REG_N>+,R6_BANK");
        }
		

		//ldc.l @<REG_N>+,R7_BANK       // no implemetation
		static void i0100_nnnn_1111_0111()
		{
            iNimp("ldc.l @<REG_N>+,R7_BANK");
        }
		

		//lds <REG_N>,MACH              // no implemetation
		static void i0100_nnnn_0000_1010()
		{
            iNimp("lds <REG_N>,MACH");
        }
		

		//lds <REG_N>,MACL              // no implemetation
		static void i0100_nnnn_0001_1010()
		{
            iNimp("lds <REG_N>,MACL");
        }
		

		//lds <REG_N>,PR                // no implemetation
		static void i0100_nnnn_0010_1010()
		{//TODO : check this
			uint n = (opcode >> 8) & 0x0F;
			pr = r[n];
		}
		

		//lds <REG_N>,FPUL              // no implemetation
		static void i0100_nnnn_0101_1010()
		{//TODO : CHECK THIS
			uint n = (opcode >> 8) & 0x0F;
			fpul =r[n];
		}
		

		//lds <REG_N>,FPSCR             // no implemetation
		static void i0100_nnnn_0110_1010()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			fpscr.reg = r[n];
		}
		

		//ldc <REG_N>,SR                // no implemetation
		static void i0100_nnnn_0000_1110()
		{
            //iNimp("ldc <REG_N>,SR");
			uint n = (opcode >> 8) & 0x0F;
            sr.reg = r[n];

        }
		

		//ldc <REG_N>,GBR               // no implemetation
		static void i0100_nnnn_0001_1110()
		{
            //iNimp("ldc <REG_N>,GBR");
			uint n = (opcode >> 8) & 0x0F;

			gbr = r[n];
        }
		

		//ldc <REG_N>,VBR               // no implemetation
		static void i0100_nnnn_0010_1110()
		{
            //iNimp("ldc <REG_N>,VBR");
			uint n = (opcode >> 8) & 0x0F;

			vbr = r[n];
        }
		

		//ldc <REG_N>,SSR               // no implemetation
		static void i0100_nnnn_0011_1110()
		{
            //iNimp("ldc <REG_N>,SSR");
			uint n = (opcode >> 8) & 0x0F;

			ssr = r[n];
        }
		

		//ldc <REG_N>,SPC               // no implemetation
		static void i0100_nnnn_0100_1110()
		{
            //iNimp("ldc <REG_N>,SPC");
			uint n = (opcode >> 8) & 0x0F;

			spc = r[n];
        }
		

		//ldc <REG_N>,R0_BANK           // no implemetation
		static void i0100_nnnn_1000_1110()
		{
            iNimp("ldc <REG_N>,R0_BANK");
        }
		

		//ldc <REG_N>,R1_BANK           // no implemetation
		static void i0100_nnnn_1001_1110()
		{
            iNimp("ldc <REG_N>,R1_BANK");
        }
		

		//ldc <REG_N>,R2_BANK           // no implemetation
		static void i0100_nnnn_1010_1110()
		{
            iNimp("ldc <REG_N>,R2_BANK");
        }
		

		//ldc <REG_N>,R3_BANK           // no implemetation
		static void i0100_nnnn_1011_1110()
		{
            iNimp("ldc <REG_N>,R3_BANK");
        }
		

		//ldc <REG_N>,R4_BANK           // no implemetation
		static void i0100_nnnn_1100_1110()
		{
            iNimp("ldc <REG_N>,R4_BANK");
        }
		

		//ldc <REG_N>,R5_BANK           // no implemetation
		static void i0100_nnnn_1101_1110()
		{
            iNimp("ldc <REG_N>,R5_BANK");
        }
		

		//ldc <REG_N>,R6_BANK           // no implemetation
		static void i0100_nnnn_1110_1110()
		{
            iNimp("ldc <REG_N>,R6_BANK");
        }
		

		//ldc <REG_N>,R7_BANK           // no implemetation
		static void i0100_nnnn_1111_1110()
		{
            iNimp("ldc <REG_N>,R7_BANK");
        }
		

		//shll <REG_N>                  // no implemetation
        static void i0100_nnnn_0000_0000()
        {//ToDo : Check This [26/4/05]
            //iNimp("shll <REG_N>");
			uint n = (opcode >> 8) & 0x0F;

            sr.T = r[n] >> 31;
            /*if (R(n) & 0x80000000)
            {
                SET_T;
            }
            else
            {
                UNSET_T;
            }*/
            r[n] <<= 1;
        }


        //dt <REG_N>                    // no implemetation
		static void i0100_nnnn_0001_0000()
		{
			uint n = (opcode >> 8) & 0x0F;
			//rn=(int)(r[n]);
			//--rn;
            r[n]=(uint)(((int)r[n])-1);
            if (r[n] == 0)
                sr.T=1;
					else
				sr.T=0;
			//r[n]=(uint)rn;
		}
		

		//shal <REG_N>                  // no implemetation
		static void i0100_nnnn_0010_0000()
		{
            iNimp("shal <REG_N>");
        }
		

		//shlr <REG_N>                  // no implemetation
        static void i0100_nnnn_0000_0001()
        {//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
            sr.T = r[n] & 0x1;
            r[n] >>= 1;
        }


        //cmp/pz <REG_N>                // no implemetation
        static void i0100_nnnn_0001_0001()
        {//ToDo : Check This [26/4/05]
            //iNimp("cmp/pz <REG_N>");
			uint n = (opcode >> 8) & 0x0F;

            if (((int)r[n]) >= 0)
                sr.T = 1;
            else
                sr.T = 0;
        }


        //shar <REG_N>                  // no implemetation
		static void i0100_nnnn_0010_0001()
        {//ToDo : Check This [26/4/05] x2
            //iNimp("shar <REG_N>");
			uint n = (opcode >> 8) & 0xF;
			uint t;

			if ((r[n] & 1) == 0) sr.T = 0; else sr.T = 1;
			if ((r[n] & 0x80000000) == 0) t = 0; else t = 1;
			r[n] >>= 1;
			if (t == 1) r[n] |= 0x80000000;
			else r[n] &= 0x7fffffff;

            /*if ((r[n] & 0x00000001) == 0) 
                sr.T = 0;
            else
                sr.T = 1;*/
			/*
			sr.T = r[n] & 0x1;

            if ((r[n] & 0x80000000) == 0)
                stmp1 = 0;
            else
                stmp1 = 1;

            r[n] >>= 1;
            if (stmp1 == 1)
                r[n] |= 0x80000000;
            else 
                r[n] &= 0x7FFFFFFF;*/
            /*short n = (arg >> 8) & 0x0F;
	long temp;

	if (R(n) & 0x01)
		SET_T
	else
		UNSET_T

	if ((R(n) & 0x80000000) == 0)
		temp = 0;
	else
		temp = 1;

	R(n) >>= 1;
	
	if (temp == 1)
		R(n) |= 0x80000000;
	else
		R(n) &= 0x7FFFFFFF;*/
        }
		

		//rotcl <REG_N>                 // no implemetation
        static void i0100_nnnn_0010_0100()
        {//ToDo : Check This [26/4/05]
            //iNimp("rotcl <REG_N>");
			uint n = (opcode >> 8) & 0x0F;
            uint t;

            t = sr.T;

            sr.T = r[n] >> 31;
            //if (R(n) & 0x80000000)
            //    SET_T;
            //else
            //    UNSET_T;

            r[n] <<= 1;

            if (t==1)
                r[n] |= 0x1;
            else
               r[n] &= 0xFFFFFFFE;
        }


        //rotl <REG_N>                  // no implemetation
        static void i0100_nnnn_0000_0100()
        {
            //iNimp("rotl <REG_N>");
			uint n = (opcode >> 8) & 0x0F;

            if ((r[n] & 0x80000000)!=0)
                sr.T=1;
            else
                sr.T = 0;

            r[n] <<= 1;

            if (sr.T!=0)
                r[n] |= 0x00000001;
            else
                r[n] &= 0xFFFFFFFE;
        }


        //cmp/pl <REG_N>                // no implemetation
		static void i0100_nnnn_0001_0101()
		{//TODO : !Add this
            //iNimp("cmp/pl <REG_N>");
			uint n = (opcode >> 8) & 0xF;
            if ((int)r[n] > 0) 
                sr.T = 1;
            else 
                sr.T = 0;
        }
		

		//rotcr <REG_N>                 // no implemetation
		static void i0100_nnnn_0010_0101()
		{
            //iNimp("rotcr <REG_N>");
			uint n = (opcode >> 8) & 0xf;
            uint temp;

            /*if ((R[n] & 0x00000001) == 0) 
                temp = 0;
            else 
                temp = 1;*/

            temp = r[n] & 0x1;

            r[n] >>= 1;
            

            if (sr.T == 1) 
                r[n] |= 0x80000000;
            else 
                r[n] &= 0x7FFFFFFF;

            sr.T = temp;
            /*if (temp == 1) 
                T = 1;
            else 
                T = 0;*/
        }
		

		//rotr <REG_N>                  // no implemetation
		static void i0100_nnnn_0000_0101()
		{
            //iNimp("rotr <REG_N>");//check ++
			uint n = (opcode >> 8) & 0xF;
            sr.T = r[n] & 0x1;
            //if ((r[n] & 0x00000001) == 0) sr.T = 0;
            //else sr.T = 1;
            r[n] >>= 1;
			r[n] |= (sr.T << 31);
            /*if (sr.T == 1) r[n] |= 0x80000000;
            else r[n] &= 0x7FFFFFFF;*/
        }
		

		//shll2 <REG_N>                 // no implemetation
		static void i0100_nnnn_0000_1000()
        {//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			r[n] <<= 2;
		}
		

		//shll8 <REG_N>                 // no implemetation
		static void i0100_nnnn_0001_1000()
        {//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			r[n] <<= 8;
		}
		

		//shll16 <REG_N>                // no implemetation
		static void i0100_nnnn_0010_1000()
        {//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			r[n] <<= 16;
		}
		

		//shlr2 <REG_N>                 // no implemetation
		static void i0100_nnnn_0000_1001()
        {//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			r[n] >>= 2;
		}
		

		//shlr8 <REG_N>                 // no implemetation
		static void i0100_nnnn_0001_1001()
		{
            //iNimp("shlr8 <REG_N>");
			uint n = (opcode >> 8) & 0xF;
            r[n] >>= 8;
        }
		

		//shlr16 <REG_N>                // no implemetation
		static void i0100_nnnn_0010_1001()
		{//TODO : CHECK ME
			uint n = (opcode >> 8) & 0x0F;
			r[n] >>= 16;
		}
		

		//jmp @<REG_N>                  // no implemetation
		static void i0100_nnnn_0010_1011()
		{   //ToDo : Check Me [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			delayslot = r[n];
			pc_funct = 2;//jump with delay 1
		}
		

		//jsr @<REG_N>                  // no implemetation
		static void i0100_nnnn_0000_1011()
		{//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			
			pr = pc + 4;
			delayslot= r[n];

			cstAddCall(pc, pr, delayslot, CallType.Normal);
			pc_funct = 2;//jump with delay
		}
		

		//tas.b @<REG_N>                // no implemetation
        static void i0100_nnnn_0001_1011()
        {
            //iNimp("tas.b @<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
            byte val;

            val=(byte)read(r[n],1);

            if (val == 0)
                sr.T = 1;
            else
                sr.T = 0;

            val |= 0x80;

            write(r[n], val, 1);
            //WriteMemoryB(R(n), &valor);
        }


        //shad <REG_M>,<REG_N>          // no implemetation
		static void i0100_nnnn_mmmm_1100()
		{
            //iNimp("shad <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0xF;
			uint m = (opcode >> 4) & 0xF;
            uint sgn = r[m] & 0x80000000;
            if (sgn == 0)
                r[n] <<= (int)(r[m] & 0x1F);
            else if ((r[m] & 0x1F) == 0)
            {
                if ((r[n] & 0x80000000) == 0)
                    r[n] = 0;
                else
                    r[n] = 0xFFFFFFFF;
            }
            else
                r[n] = r[n] >> (int)((~r[m] & 0x1F) + 1);
            /*short n = (arg >> 8) & 0x0F;
	short m = (arg >> 4) & 0x0F;

	long amount;

	if (R(m) == 0)
		return;

	amount = R(m) & 0x1F;

	if ((signed) R(m) > 0) // left shift
	{
		R(n) <<= amount;
	}
	else // right shift
	if (amount != 0)
	{
		(signed) R(n) >>= (32 - amount); // IMPORTANTE!!!!!!
	}
	else
	if ((signed) R(n) < 0)
	{
		R(n) = -1;
	}
	else
	{
		R(n) = 0;
	}*/
        }
		

		//shld <REG_M>,<REG_N>          // no implemetation
		static void i0100_nnnn_mmmm_1101()
        {//ToDo : Check This [26/4/05] x2
            //iNimp("shld <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0xF;
			uint m = (opcode >> 4) & 0xF;
			int s;

			s =  (int)(r[m] & 0x80000000);
			if (s == 0)
				r[n] <<= (int)(r[m] & 0x1f);
			else if ((r[m] & 0x1f) == 0)
				r[n] = 0;
			else
				r[n] = (uint)r[n] >> (int)((~r[m] & 0x1f) + 1);
			/*
			if ((int)r[m] == 0)
				return;

			if ((int)r[m] > 0)
			{

				r[n] <<= (int)(r[m] & 0x1F);
			}
			else
				if ((int)r[m] < 0)
				{

					r[n] >>= (32 - (int)(r[m] & 0x1F)); // 
				}
				else
					r[n] = 0;*/
        }
		

		//mac.w @<REG_M>+,@<REG_N>+     // no implemetation
		static void i0100_nnnn_mmmm_1111()
		{
            iNimp("mac.w @<REG_M>+,@<REG_N>+");
        }
	}
    //5xxx
    public static unsafe partial class emu 
    {
		//mov.l @(<disp>,<REG_M>),<REG_N>
		static void i0101_nnnn_mmmm_iiii()
		{//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			uint disp = (opcode & 0x0F) << 2;
			r[n]=read(r[m]+disp,4);
		}
	}
    //6xxx
    public static unsafe partial class emu 
    {
		//mov.b @<REG_M>,<REG_N>        // no implemetation
		static void i0110_nnnn_mmmm_0000()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			r[n] = (uint)(int)(sbyte)read(r[m], 1);
		} 
		

		//mov.w @<REG_M>,<REG_N>        // no implemetation
		static void i0110_nnnn_mmmm_0001()
        {//TODO : Check This [26/4/05]
            //iNimp("mov.w @<REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0xF;
			uint m = (opcode >> 4) & 0xF;
            r[n] = (uint)(int)(short)read(r[m],2);
            //if ((r[n] & 0x8000) == 0) 
            //    r[n] &= 0x0000FFFF;
            //else 
            //    r[n] |= 0xFFFF0000;
        } 
		

		//mov.l @<REG_M>,<REG_N>        // no implemetation
		static void i0110_nnnn_mmmm_0010()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			r[n]=read(r[m],4);
		} 
		

		//mov <REG_M>,<REG_N>           // no implemetation
		static void i0110_nnnn_mmmm_0011()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			r[n] = r[m];
		}
		

		//mov.b @<REG_M>+,<REG_N>       // no implemetation
		static void i0110_nnnn_mmmm_0100()
        {//TODO : Check This [26/4/05]
            //iNimp("mov.b @<REG_M>+,<REG_N>");
			uint n = (opcode >> 8) & 0xF;
			uint m = (opcode >> 4) & 0xF;
            r[n] = (uint)(int)(sbyte)read(r[m],1);
            //if ((r[n] & 0x80) == 0) r[n] &= 0x000000FF;
            //else r[n] |= 0xFFFFFF00;
            if (n != m) r[m] += 1;
        } 
		

		//mov.w @<REG_M>+,<REG_N>       // no implemetation
		static void i0110_nnnn_mmmm_0101()
		{
            //iNimp("mov.w @<REG_M>+,<REG_N>");
			uint n = (opcode >> 8) & 0xF;
			uint m = (opcode >> 4) & 0xF;
            r[n] = (uint)(short)(ushort)read(r[m], 2);
            //if ((r[n] & 0x8000) == 0) r[n] &= 0x0000FFFF;
            //else r[n] |= 0xFFFF0000;
            if (n != m) r[m] += 2;
        } 
		

		//mov.l @<REG_M>+,<REG_N>       // no implemetation
		static void i0110_nnnn_mmmm_0110()
		{//TODO : hADD THIS
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

			r[n]=read(r[m],4);
			if (n != m)
				r[m] += 4;

		} 
		

		//not <REG_M>,<REG_N>           // no implemetation
		static void i0110_nnnn_mmmm_0111()
		{
            //iNimp("not <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

            r[n] = ~r[m];
        } 
		

		//swap.b <REG_M>,<REG_N>        // no implemetation
		static void i0110_nnnn_mmmm_1000()
		{
            //iNimp("swap.b <REG_M>,<REG_N>");
			uint m = (opcode >> 4) & 0xF;
			uint n = (opcode >> 8) & 0xF;
			//r_word[];
			r_word[(n << 1) + 1] = r_word[(m << 1) + 1];

			byte tmp = r_byte[m << 2];
			r_byte[n << 2] = r_byte[(m << 2) + 1];
			r_byte[(n << 2) + 1] = tmp;

			//			   r[m<<1]
            //utmp1 = r[m] & 0xFFFF0000;
            //utmp2 = (r[m] & 0x000000FF) << 8;
            //r[n] = (r[m] & 0x0000FF00) >> 8;
            //r[n] = r[n] | utmp2 | utmp1;
        } 
		

		//swap.w <REG_M>,<REG_N>        // no implemetation
		static void i0110_nnnn_mmmm_1001()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

			ushort t = (ushort)(r[m]>>16);
			r[n] = (r[m] << 16) | t;
			//r_word[(n << 1) + 1] = r_word[m << 1];
			//r_word[n << 1] = t;
			//r[n] = ((r[m] >> 16) & 0xFFFF) | ((r[m] << 16) & 0xFFFF0000);
		} 
		

		//negc <REG_M>,<REG_N>          // no implemetation
        static void i0110_nnnn_mmmm_1010()
        {
            //iNimp("negc <REG_M>,<REG_N>");
            //needs check ++
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
            uint temp= (uint)(0 - ((int)r[m]));

            r[n] = temp - sr.T;

            if (0 < temp)
                sr.T = 1;
            else
                sr.T = 0;

            if (temp < r[n])
                sr.T = 1;
        }


        //neg <REG_M>,<REG_N>           // no implemetation
		static void i0110_nnnn_mmmm_1011()
        {//ToDo : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			//rm=(int) r[m];-rm
			r[n] =(uint)(0- ((int)r[m]));
		} 
		

		//extu.b <REG_M>,<REG_N>        // no implemetation
		static void i0110_nnnn_mmmm_1100()
		{//TODO : CHECK THIS
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			r[n] = r[m] & 0xFF;
		} 
		

		//extu.w <REG_M>,<REG_N>        // no implemetation
		static void i0110_nnnn_mmmm_1101()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;
			r[n] = r[m] & 0x0000FFFF;
		} 
		

		//exts.b <REG_M>,<REG_N>        // no implemetation
		static void i0110_nnnn_mmmm_1110()
        {//TODO : Check This [26/4/05]
            //iNimp("exts.b <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

            r[n] = (uint)(sbyte)(byte)(r[m] & 0xFF);

        } 
		

		//exts.w <REG_M>,<REG_N>        // no implemetation
		static void i0110_nnnn_mmmm_1111()
        {//ToDo : Check This [26/4/05]
            //iNimp("exts.w <REG_M>,<REG_N>");
			uint n = (opcode >> 8) & 0x0F;
			uint m = (opcode >> 4) & 0x0F;

            r[n] = (uint)(short)(ushort)(r[m] & 0xFFFF);
        } 
	}
    //7xxx
    public static unsafe partial class emu
    {
		//add #<imm>,<REG_N>
		static void i0111_nnnn_iiii_iiii()
		{//TODO : CHACK THIS
			uint n = (opcode >> 8) & 0x0F;
			int stmp1 = (int)(sbyte)(opcode & 0xFF);
            r[n] = (uint)(((int)r[n])+stmp1);
        }
	}
    //8xxx
    public static unsafe partial class emu
    {
		// bf <bdisp8>                   // no implemetation
		static void i1000_1011_iiii_iiii()
        {//ToDo : Check Me [26/4/05]
            if (sr.T==0)
			{
				delayslot  = (uint)(((sbyte)(opcode  & 0xFF))*2 + 4 + pc );
				pc_funct = 1;//jump , no delay
			}
		}
		

		// bf.s <bdisp8>                 // no implemetation
		static void i1000_1111_iiii_iiii()
        {//TODO : Check This [26/4/05]
            if (sr.T==0)
			{
				delayslot = (uint) ((((sbyte) (opcode & 0xFF))<<1) + pc + 4);
				pc_funct =2;//delay 1 instruction
			}
		}
		

		// bt <bdisp8>                   // no implemetation
		static void i1000_1001_iiii_iiii()
        {//TODO : Check This [26/4/05]
            if (sr.T==1)
			{
				delayslot = (uint)((((sbyte)(opcode & 0xFF))<<1) + pc + 4);
				pc_funct = 1;//direct jump
			}
		}
		

		// bt.s <bdisp8>                 // no implemetation
		static void i1000_1101_iiii_iiii()
		{
            if (sr.T == 1)
            {//TODO : Check This [26/4/05]
                delayslot = (uint) (((sbyte)(opcode & 0xFF)) * 2 + pc + 4); // anti gia disp = ...
                pc_funct = 2;
            }
        }
		

		// cmp/eq #<imm>,R0              // no implemetation
		static void i1000_1000_iiii_iiii()
        {//TODO : Check This [26/4/05]
            uint imm = (uint)(sbyte)(opcode & 0xFF);
			if (r[0] == imm)
				sr.T =1;
					else
				sr.T =0;
		}
		

		// mov.b R0,@(<disp>,<REG_M>)    // no implemetation
		static void i1000_0000_mmmm_iiii()
        {//TODO : Check This [26/4/05]
            //iNimp("mov.b R0,@(<disp>,<REG_M>)");
			uint n = (opcode >> 4) & 0x0F;
			uint disp = opcode & 0x0F;
            write(r[n]+disp, r[0] & 0xFF, 1);
            //WriteMemoryB(R(n) + i, &valor);
        }
		

		// mov.w R0,@(<disp>,<REG_M>)    // no implemetation
		static void i1000_0001_mmmm_iiii()
		{//TODO : !Add this
            //iNimp("mov.w R0,@(<disp>,<REG_M>)");
			uint disp = opcode & 0xF;
			uint m = (opcode >> 4) & 0xF;
            write(r[m] + (disp << 1), r[0], 2);
        }
		

		// mov.b @(<disp>,<REG_M>),R0    // no implemetation
		static void i1000_0100_mmmm_iiii()
        {//TODO : Check This [26/4/05] x2
            //iNimp("mov.b @(<disp>,<REG_M>),R0");
			uint disp = (0xF & opcode);
			uint m = (opcode >> 4) & 0xF;
            r[0] = (uint)(sbyte)read(r[m] + disp,1);
            //if ((R[0] & 0x80) == 0) R[0] &= 0x000000FF;
            //else R[0] |= 0xFFFFFF00;
        }
		

		// mov.w @(<disp>,<REG_M>),R0    // no implemetation
		static void i1000_0101_mmmm_iiii()
        {//TODO : Check This [26/4/05]
            //iNimp("mov.w @(<disp>,<REG_M>),R0");
			uint disp = opcode & 0xF;
			uint m = (opcode >> 4) & 0xF;
            r[0] = (uint)(short)read(r[m] + (disp << 1),2);
            //if ((r[0] & 0x8000) == 0) r[0] &= 0x0000FFFF;
            //else r[0] |= 0xFFFF0000;
        }
	}
    //9xxx
    public static unsafe partial class emu 
    {
		//mov.w @(<disp>,PC),<REG_N>   
		static void i1001_nnnn_iiii_iiii()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint disp = (opcode & 0xFF);
			r[n]=(uint)(int)(short)read((disp<<1) + pc + 4,2);
		}
	}
    //Axxx
    public static unsafe partial class emu 
    {
		// bra <bdisp12>
		static void i1010_iiii_iiii_iiii()
        {//ToDo : Check Me [26/4/05]
            delayslot = (uint) ((  ((short)((opcode & 0x0FFF)<<4)) >>3)  + pc + 4);//(short<<4,>>4(-1*2))
			pc_funct =2;//jump delay 1
		}
	}
    //Bxxx
    public static unsafe partial class emu 
    {
		// bsr <bdisp12>
		static void i1011_iiii_iiii_iiii()
        {//ToDo : Check Me [26/4/05]
            //iNimp("bsr <bdisp12>");
			uint disp = opcode & 0x0FFF;
            pr = pc + 4;
            delayslot = (uint)((((short)(disp<<4)) >> 3) + pc + 4);
			cstAddCall(pc, pr, delayslot, CallType.Normal);
            pc_funct = 2;
        }
	}
    //Cxxx
    public static unsafe partial class emu 
    {
		// mov.b R0,@(<disp>,GBR)        // no implemetation
		static void i1100_0000_iiii_iiii()
		{
            iNimp("mov.b R0,@(<disp>,GBR)");
        }
		

		// mov.w R0,@(<disp>,GBR)        // no implemetation
		static void i1100_0001_iiii_iiii()
		{
            iNimp("mov.w R0,@(<disp>,GBR)");
        }
		

		// mov.l R0,@(<disp>,GBR)        // no implemetation
		static void i1100_0010_iiii_iiii()
		{
            iNimp("mov.l R0,@(<disp>,GBR)");
        }
		

		// trapa #<imm>                  // no implemetation
		static void i1100_0011_iiii_iiii()
		{
            //iNimp("trapa #<imm>");
			/*uint imm = opcode & 0xFF;

			spc = pc + 2;
			ssr = sr.reg;
			*TRA = (imm << 2);
			
			RaiseExecption(sh4_expt.TRAP);
			//sr.MD = 1;
			//sr.RB = 1;
			//sr.BL = 1;
			

			delayslot = vbr + 0x0100;
			pc_funct = 1;		//direct jump
			cstAddCall(pc, spc, delayslot, CallType.Interupt);*/
		}
		

		// mov.b @(<disp>,GBR),R0        // no implemetation
		static void i1100_0100_iiii_iiii()
		{
            iNimp("mov.b @(<disp>,GBR),R0");
        }
		

		// mov.w @(<disp>,GBR),R0        // no implemetation
		static void i1100_0101_iiii_iiii()
		{
            iNimp("mov.w @(<disp>,GBR),R0");
        }
		

		// mov.l @(<disp>,GBR),R0        // no implemetation
		static void i1100_0110_iiii_iiii()
		{
            iNimp("mov.l @(<disp>,GBR),R0");
        }
		

		// mova @(<disp>,PC),R0          // no implemetation
		static void i1100_0111_iiii_iiii()
        {//TODO : Check This [26/4/05]
			uint disp = ((opcode & 0x00FF) << 2) + ((pc + 4) & 0xFFFFFFFC);
			r[0] = disp;
		}
		

		// tst #<imm>,R0                 // no implemetation
		static void i1100_1000_iiii_iiii()
        {//TODO : Check This [26/4/05]
            //iNimp("tst #<imm>,R0");
            uint utmp2 = opcode & 0xFF;
            uint utmp1 = r[0] & utmp2;
            if (utmp1 == 0) 
                sr.T = 1;
            else 
                sr.T = 0;
        }
		

		// and #<imm>,R0                 // no implemetation
		static void i1100_1001_iiii_iiii()
        {//ToDo : Check This [26/4/05]
            //iNimp("and #<imm>,R0");
            uint imm = opcode & 0xFF;
            r[0] &= imm;
        }
		

		// xor #<imm>,R0                 // no implemetation
		static void i1100_1010_iiii_iiii()
		{
            //iNimp("xor #<imm>,R0");
            uint  imm  = opcode & 0xFF;
            r[0] ^= imm;
        }
		

		// or #<imm>,R0                  // no implemetation
		static void i1100_1011_iiii_iiii()
        {//ToDo : Check This [26/4/05]
            //iNimp("or #<imm>,R0");
			uint imm = opcode & 0xFF;
            r[0] |= imm;
        } 
		

		// tst.b #<imm>,@(R0,GBR)        // no implemetation
		static void i1100_1100_iiii_iiii()
		{
            iNimp("tst.b #<imm>,@(R0,GBR)");
        }
		

		// and.b #<imm>,@(R0,GBR)        // no implemetation
		static void i1100_1101_iiii_iiii()
		{
            iNimp("and.b #<imm>,@(R0,GBR)");
        }
		

		// xor.b #<imm>,@(R0,GBR)        // no implemetation
		static void i1100_1110_iiii_iiii()
		{
            iNimp("xor.b #<imm>,@(R0,GBR)");
        }
		

		// or.b #<imm>,@(R0,GBR)         // no implemetation
		static void i1100_1111_iiii_iiii()
		{
            iNimp("or.b #<imm>,@(R0,GBR)");
        }
	}
    //Dxxx
    public static unsafe partial class emu
    {
		// mov.l @(<disp>,PC),<REG_N>    
		static void i1101_nnnn_iiii_iiii()
        {//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			uint disp = (opcode & 0xFF);
			r[n]=read((disp<<2) + (pc & 0xFFFFFFFC) + 4,4);
		}
	}
    //Exxx
    public static unsafe partial class emu 
    {
		// mov #<imm>,<REG_N>
		static void i1110_nnnn_iiii_iiii()
		{//TODO : Check This [26/4/05]
			uint n = (opcode >> 8) & 0x0F;
			r[n] = (uint)(sbyte)(opcode & 0xFF);//(uint)(sbyte)= signextend8 :)
		}
	}
    //Fxxx
    public static unsafe partial class emu 
    {
		//fadd <FREG_M>,<FREG_N>        no implemetation
		static void i1111_nnnn_mmmm_0000()
		{//TODO : CHECK THIS PR FP
			if (fpscr.PR == 0)
			{
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;
				fr[n] += fr[m];
			}
			else
			{
				uint n = (opcode >> 9) & 0x07;
				uint m = (opcode >> 5) & 0x07;
				dr[n] += dr[m];
			}
		}
														   

		//fsub <FREG_M>,<FREG_N>        no implemetation
		static void i1111_nnnn_mmmm_0001(){

			if (fpscr.PR == 0)
			{
				//iNimp("fsub <FREG_M>,<FREG_N>");
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;

				fr[n] -= fr[m];
			}
			else
			{
				uint n = (opcode >> 9) & 0x0F;
				uint m = (opcode >> 5) & 0x0F;

				dr[n] -= dr[m];
			}
        }
																											  

		//fmul <FREG_M>,<FREG_N>        no implemetation
		static void i1111_nnnn_mmmm_0010()
		{
			if (fpscr.PR == 0)
			{
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;
				fr[n] *= fr[m];
			}
			else
			{
				uint n = (opcode >> 9) & 0x07;
				uint m = (opcode >> 5) & 0x07;
				dr[n] *= dr[m];
			}
		}
																																								 

		//fdiv <FREG_M>,<FREG_N>        no implemetation
		static void i1111_nnnn_mmmm_0011()
		{//TODO : CHECK THIS + PRMODE FP
			if (fpscr.PR == 0)
			{
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;
				fr[n] /= fr[m];
			}
			else
			{
				uint n = (opcode >> 9) & 0x07;
				uint m = (opcode >> 5) & 0x07;
				dr[n] /= dr[m];
			}
		}
																																																					

		//fcmp/eq <FREG_M>,<FREG_N>     no implemetation
		static void i1111_nnnn_mmmm_0100()
		{
			if (fpscr.PR == 0)
			{
				//iNimp("fcmp/eq <FREG_M>,<FREG_N>");
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;

				sr.T = (fr[m] == fr[n]) ? (uint)1 : 0;
			}
			else
			{
				//iNimp("fcmp/eq <FREG_M>,<FREG_N>");
				uint n = (opcode >> 9) & 0x07;
				uint m = (opcode >> 5) & 0x07;

				sr.T = (dr[m] == dr[n]) ? (uint)1 : 0;
			}
        }
																																																																	   

		//fcmp/gt <FREG_M>,<FREG_N>     no implemetation
		static void i1111_nnnn_mmmm_0101()
		{
			if (fpscr.PR == 0)
			{
				//iNimp("fcmp/gt <FREG_M>,<FREG_N>");
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;

				if (fr[n] > fr[m])
					sr.T = 1;
				else
					sr.T = 0;
			}
			else
			{
				uint n = (opcode >> 9) & 0x07;
				uint m = (opcode >> 5) & 0x07;

				if (dr[n] > dr[m])
					sr.T = 1;
				else
					sr.T = 0;
			}
        }
																																																																														  

		//fmov.s @(R0,<REG_M>),<FREG_N> no implemetation
		static void i1111_nnnn_mmmm_0110()
		{
			if (fpscr.PR == 0)
			{
				//iNimp("fmov.s @(R0,<REG_M>),<FREG_N>");
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;

				fr_uint[n] = read(r[m] + r[0], 4);
			}
			else
			{
				uint n = (opcode >> 9) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;

				fr_uint[n] = read(r[m] + r[0] + 4, 4);
				fr_uint[n + 1] = read(r[m] + r[0], 4);
			}
        }


		//fmov.s <FREG_M>,@(R0,<REG_N>) no implemetation
		static void i1111_nnnn_mmmm_0111()
		{
			if (fpscr.PR == 0)
			{
				//iNimp("fmov.s <FREG_M>,@(R0,<REG_N>)");
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;

				write(r[0] + r[n], fr_uint[m], 4);
				//WriteMemoryF(R(0) + R(n), &FR(m));
			}
			else
			{

			}
        }


		//fmov.s @<REG_M>,<FREG_N>      no implemetation
		public static unsafe void i1111_nnnn_mmmm_1000()
		{//TODO : CHECK PR SZ FP
			if (fpscr.SZ == 0)
			{
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;
				fr_uint[n] = read(r[m], 4);
			}
			else
			{
				uint n = (opcode >> 8) & 0x0E;
				uint m = (opcode >> 4) & 0x0F;
				fr_uint[n + 1] = read(r[m] + 4, 4);
				fr_uint[n] = read(r[m], 4);
				double* t = &dr[n >> 1];
			}
		}


		//fmov.s @<REG_M>+,<FREG_N>     no implemetation
		static void i1111_nnnn_mmmm_1001()
		{
			if (fpscr.SZ == 0)
			{
				//iNimp("fmov.s @<REG_M>+,<FREG_N>");
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;

				fr_uint[n] = read(r[m], 4);

				r[m] += 4;
			}
			else {
				uint n = (opcode >> 8) & 0x0E;
				uint m = (opcode >> 4) & 0x0F;

				fr_uint[n+1] = read(r[m] + 4, 4);
				fr_uint[n] = read(r[m], 4);

				r[m] += 8;
				//double* t = &dr[n >> 1];
			}
        }


		//fmov.s <FREG_M>,@<REG_N>      no implemetation
		public static unsafe void i1111_nnnn_mmmm_1010()
		{//	TODO : hadd this
			if (fpscr.PR == 0)
			{
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;
				write(r[n], fr_uint[m], 4);
			}
			else
			{
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;
				write(r[n], fr_uint[m], 4);
			}
		}

		//fmov.s <FREG_M>,@-<REG_N>     no implemetation
		static void i1111_nnnn_mmmm_1011()
		{
			if (fpscr.PR == 0)
			{
				//iNimp("fmov.s <FREG_M>,@-<REG_N>");
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;

				r[n] -= 4;

				write(r[n], fr_uint[m], 4);
			}
			else
			{
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;

				r[n] -= 4;

				write(r[n], fr_uint[m], 4);
			}
        }


		//fmov <FREG_M>,<FREG_N>        no implemetation
		static void i1111_nnnn_mmmm_1100()
		{//TODO : checkthis
			if (fpscr.PR == 0)
			{
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;
				fr[n] = fr[m];
			}
			else
			{
				uint n = (opcode >> 9) & 0x07;
				uint m = (opcode >> 5) & 0x07;
				dr[n] = dr[m];
			}
		}


		//fabs <FREG_N>                 no implemetation
		static void i1111_nnnn_0101_1101()
		{
			if (fpscr.PR == 0)
			{
				iNimp("fabs <FREG_N>");
			}
			else
			{
			}
        }

		// FSCA FPUL, DRn//F0FD//1111_nnnn_1111_1101
		static void i1111_nnn0_1111_1101()
		{
			uint n = (opcode >> 9) & 0x07;
			float x = (float) (2 * pi * (float) fpul / 65536.0);
			fr[n*2] = (float)System.Math.Sin(x);
			fr[n*2+1] =(float) System.Math.Cos(x);
		}

		//fcnvds <DR_N>,FPUL            no implemetation
		static void i1111_nnnn_1011_1101()
		{
			
            iNimp("fcnvds <DR_N>,FPUL;mode " + fpscr.PR.ToString());
			uint n = (opcode >> 9) & 0x07;
			fixed (uint*p=&fpul)
				*((float*)p) = (float)dr[n];

        }


		//fcnvsd FPUL,<DR_N>            no implemetation
		static void i1111_nnnn_1010_1101()
		{
			iNimp("fcnvsd FPUL,<DR_N>;mode " + fpscr.PR.ToString());
			uint n = (opcode >> 9) & 0x07;
			fixed (uint* p = &fpul)
				dr[n] = (double)*((float*)p);
        }

		//fipr <FV_M>,<FV_N>            
		static void i1111_nnmm_1110_1101()
		{
            iNimp("fipr <FV_M>,<FV_N>");
        }


		//fldi0 <FREG_N>                no implemetation
		static void i1111_nnnn_1000_1101()
		{
			if (fpscr.PR==0)
			{
				//iNimp("fldi0 <FREG_N>");
				uint n = (opcode >> 8) & 0x0F;

				fr[n] = 0.0f;
			}
			else
			{
				iNimp("fldi0 <Dreg_N>");
			}
        }


		//fldi1 <FREG_N>                no implemetation
		static void i1111_nnnn_1001_1101()
		{
            //iNimp("fldi1 <FREG_N>");
			uint n = (opcode >> 8) & 0x0F;

			fr[n] = (float)1.0;
        }


		//flds <FREG_N>,FPUL            no implemetation
		static void i1111_nnnn_0001_1101()
		{
            //iNimp("flds <FREG_N>,FPUL");
			//teeesxsttt
			uint n = (opcode >> 8) & 0x0F;

			fpul = fr_uint[n];
        }


		//float FPUL,<FREG_N>           no implemetation
		static void i1111_nnnn_0010_1101()
		{//TODO : CHECK THIS (FP)
			if (fpscr.PR == 0)
			{
				uint n = (opcode >> 8) & 0x0F;
				fr[n] = (float)(int)fpul;
			}
			else
			{
				uint n = (opcode >> 9) & 0x07;
				dr[n] = (double)(int)fpul;
			}
		}


		//fneg <FREG_N>                 no implemetation
		static void i1111_nnnn_0100_1101()
		{
            //iNimp("fneg <FREG_N>");
			if (fpscr.PR == 0)
			{
				uint n = (opcode >> 8) & 0x0F;

				fr[n] = -fr[n];
			}
			else
			{
			}

        }


		//frchg                         no implemetation
		static void i1111_1011_1111_1101()
		{
            iNimp("frchg");
        }


		//fschg                         no implemetation
		static void i1111_0011_1111_1101()
		{
            //iNimp("fschg");
			fpscr.SZ = 1 - fpscr.SZ;
			//if (IS_SET(FPSCR, FPSCR_SZ))
		//		REMOVE_BIT(FPSCR, FPSCR_SZ);
		//	else
	//			SET_BIT(FPSCR, FPSCR_SZ);
        }

		//fsqrt <FREG_N>                
		static void i1111_nnnn_0110_1101()
		{
			if (fpscr.PR == 0)
			{
				//iNimp("fsqrt <FREG_N>");
				uint n = (opcode >> 8) & 0x0F;

				fr[n] = (float)Math.Sqrt(fr[n]);
			}
			else { }
        }


		//ftrc <FREG_N>, FPUL           no implemetation
		static void i1111_nnnn_0011_1101()
		{
			if (fpscr.PR == 0)
			{
				uint n = (opcode >> 8) & 0x0F;
				fpul = (uint)(int)fr[n];
			}
			else
			{
				uint n = (opcode >> 9) & 0x07;
				fpul = (uint)(int)dr[n];
			}
		}


		//fsts FPUL,<FREG_N>            no implemetation
		static void i1111_nnnn_0000_1101()
		{
            iNimp("fsts FPUL,<FREG_N>");
        }


		//fmac <FREG_0>,<FREG_M>,<FREG_N> no implemetation
		static void i1111_nnnn_mmmm_1110()
		{
            //iNimp("fmac <FREG_0>,<FREG_M>,<FREG_N>");
			if (emu.fpscr.PR_==0)
			{
				uint n = (opcode >> 8) & 0x0F;
				uint m = (opcode >> 4) & 0x0F;

				fr[n] += fr[0] * fr[m];
			}
			else
			{
				iNimp("fmac <DREG_0>,<DREG_M>,<DREG_N>");
			}
        }


		//ftrv xmtrx,<FV_N>             no implemetation
		static void i1111_nn01_1111_1101()
		{
            //iNimp("ftrv xmtrx,<FV_N>");
			uint n = (opcode >> 10) & 0x3;
			float v1, v2, v3, v4;

			/* matrix:
				XF0		XF4		XF8		XF12
				XF1		XF5		XF9		XF13
				XF2		XF6		XF10	XF14
				XF3		XF7		XF11	XF15 */

			v1 = xr[0] * fr[4 * n + 0] +
						xr[4] * fr[4 * n + 1] +
						xr[8] * fr[4 * n + 2] +
						xr[12] * fr[4 * n + 3];
			v2 = xr[1] * fr[4 * n + 0] +
						xr[5] * fr[4 * n + 1] +
						xr[9] * fr[4 * n + 2] +
						xr[13] * fr[4 * n + 3];
			v3 = xr[2] * fr[4 * n + 0] +
						xr[6] * fr[4 * n + 1] +
						xr[10] * fr[4 * n + 2] +
						xr[14] * fr[4 * n + 3];
			v4 = xr[3] * fr[4 * n + 0] +
						xr[7] * fr[4 * n + 1] +
						xr[11] * fr[4 * n + 2]+
						xr[15] * fr[4 * n + 3];

			fr[4 * n + 0] = v1;
			fr[4 * n + 1] = v2;
			fr[4 * n + 2] = v3;
			fr[4 * n + 3] = v4;
        }
	}
}
