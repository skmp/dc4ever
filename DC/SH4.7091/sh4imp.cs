//This is a part of the DC4Ever emulator
//You are not allowed to release modified(or unmodified) versions
//without asking me (drk||Raziel).
//For Suggestions ect please e-mail at : stef_mp@yahoo.gr
//Note : This is just to prove that Fast emulation is possible with 
//	C# /.net ...And yes , this code could be writen at VB.net and 
//	run more or less at the same speed on dynarec mode
//	This code requires C#.net 2.0 (Get the C# epxress 2005 Beta from microsoft)
//
//not used anymore
using System;
namespace DC4Ever
{
    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		public static void iInvalidOpcode()
		{
			dc.dcon.WriteLine("Warning Invalid opcode (0xxx class) at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
			System.Windows.Forms.Application.DoEvents();
		}
		//stc SR,<REG_N>                // no implementation
		public static void i0000_nnnn_0000_0010()//0002
		{
			n = (opcode >> 8) & 0x0F;
			r[n] = sr.reg;
		}

		
		//stc GBR,<REG_N>               // no implementation
		public static void i0000_nnnn_0001_0010()
		{

		} 
		

		//stc VBR,<REG_N>               // no implementation
		public static void i0000_nnnn_0010_0010()
		{

		} 
		

		//stc SSR,<REG_N>               // no implementation
		public static void i0000_nnnn_0011_0010()
		{

		} 
		

		//stc SPC,<REG_N>               // no implementation
		public static void i0000_nnnn_0100_0010()
		{

		} 
		

		//stc R0_BANK,<REG_N>           // no implementation
		public static void i0000_nnnn_1000_0010()
		{

		} 
		

		//stc R1_BANK,<REG_N>           // no implementation
		public static void i0000_nnnn_1001_0010()
		{

		} 


		//stc R2_BANK,<REG_N>           // no implementation
		public static void i0000_nnnn_1010_0010()
		{

		} 


		//stc R3_BANK,<REG_N>           // no implementation
		public static void i0000_nnnn_1011_0010()
		{//TODO : !Add this

		} 


		//stc R4_BANK,<REG_N>           // no implementation
		public static void i0000_nnnn_1100_0010()
		{

		} 


		//stc R5_BANK,<REG_N>           // no implementation
		public static void i0000_nnnn_1101_0010()
		{

		} 


		//stc R6_BANK,<REG_N>           // no implementation
		public static void i0000_nnnn_1110_0010()
		{

		} 


		//stc R7_BANK,<REG_N>           // no implementation
		public static void i0000_nnnn_1111_0010()
		{

		} 


		//braf <REG_N>                  // no implementation
		public static void i0000_nnnn_0010_0011()
		{//TODO : done!ADD THIS
			n = (opcode >> 8) & 0x0F;
			delayslot = r[n] + pc + 4;
			pc_funct =2;//delay 2
		} 


		//bsrf <REG_N>                  // no implementation
		public static void i0000_nnnn_0000_0011()
		{

		} 


		//movca.l R0, @<REG_N>          // no implementation
		public static void i0000_nnnn_1100_0011()
		{

		} 


		//ocbi @<REG_N>                 // no implementation
		public static void i0000_nnnn_1001_0011()
		{

		} 


		//ocbp @<REG_N>                 // no implementation
		public static void i0000_nnnn_1010_0011()
		{

		} 


		//ocbwb @<REG_N>                // no implementation
		public static void i0000_nnnn_1011_0011()
		{

		} 


		//pref @<REG_N>                 // no implementation
		public static void i0000_nnnn_1000_0011()
		{//TODO : !Add this

		} 


		//mov.b <REG_M>,@(R0,<REG_N>)   // no implementation
		public static void i0000_nnnn_mmmm_0100()
		{

		}


		//mov.w <REG_M>,@(R0,<REG_N>)   // no implementation
		public static void i0000_nnnn_mmmm_0101()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			write(r[0]+r[n],r[m]& 0xFFFF,2);
		}


		//mov.l <REG_M>,@(R0,<REG_N>)   // no implementation
		public static void i0000_nnnn_mmmm_0110()
		{

		}


		//mul.l <REG_M>,<REG_N>         // no implementation
		public static void i0000_nnnn_mmmm_0111()
		{//TODO : CHECK THIS
			n = (opcode>> 8) & 0x0F;
			m = (opcode>> 4) & 0x0F;
			macl= (uint) (((int)r[n] * (int) r[m]) & 0xFFFFFFFF);
		}


		//clrmac                        // no implementation
		public static void i0000_0000_0010_1000()
		{

		} 


		//clrs                          // no implementation
		public static void i0000_0000_0100_1000()
		{

		} 


		//clrt                          // no implementation
		public static void i0000_0000_0000_1000()
		{

		} 


		//ldtlb                         // no implementation
		public static void i0000_0000_0011_1000()
		{

		} 


		//sets                          // no implementation
		public static void i0000_0000_0101_1000()
		{

		} 


		//sett                          // no implementation
		public static void i0000_0000_0001_1000()
		{

		} 


		//div0u                         // no implementation
		public static void i0000_0000_0001_1001()
		{

		} 


		//movt <REG_N>                  // no implementation
		public static void i0000_nnnn_0010_1001()
		{

		} 


		//nop                           // no implementation
		public static void i0000_0000_0000_1001()
		{//TODO : Nop :P

		} 


		//sts FPUL,<REG_N>              // no implementation
		public static void i0000_nnnn_0101_1010()
		{//TODO : CHECK THIS  WRONG FPUL ?!?!
			n = (opcode >> 8) & 0x0F;
			r[n] = (uint)(int)fpul;
		} 


		//sts FPSCR,<REG_N>             // no implementation
		public static void i0000_nnnn_0110_1010()
		{

		} 


		//sts MACH,<REG_N>              // no implementation
		public static void i0000_nnnn_0000_1010()
		{

		} 


		//sts MACL,<REG_N>              // no implementation
		public static void i0000_nnnn_0001_1010()
		{//TODO : CHECK THIS
			n=  (opcode >> 8) & 0x0F;
			r[n]=macl; 
		} 


		//sts PR,<REG_N>                // no implementation
		public static void i0000_nnnn_0010_1010()
		{

		} 


		//rte                           // no implementation
		public static void i0000_0000_0010_1011()
		{

		} 


		//rts                           // no implementation
		public static void i0000_0000_0000_1011()
		{//TODO : fADD THIS
			delayslot  = pr;
			pc_funct = 2;
		} 


		//sleep                         // no implementation
		public static void i0000_0000_0001_1011()
		{

		} 


		//mov.b @(R0,<REG_M>),<REG_N>   // no implementation
		public static void i0000_nnnn_mmmm_1100()
		{//TODO : done!add this
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			r[n] = (uint)(sbyte)read(r[0]+r[m],1);
		} 


		//mov.w @(R0,<REG_M>),<REG_N>   // no implementation
		public static void i0000_nnnn_mmmm_1101()
		{

		} 


		//mov.l @(R0,<REG_M>),<REG_N>   // no implementation
		public static void i0000_nnnn_mmmm_1110()
		{

		} 


		//mac.l @<REG_M>+,@<REG_N>+     // no implementation
		public static void i0000_nnnn_mmmm_1111()
		{//TODO : !Add this

		} 

	}

    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		//mov.l <REG_M>,@(<disp>,<REG_N>)
		public static void i0001_nnnn_mmmm_iiii()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			disp = opcode & 0x0F;
			write(r[n] + disp * 4,r[m],4);
		}
	}

    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		public static void iInvalidOpcode()
		{
			dc.dcon.WriteLine("Warning Invalid opcode (2xxx class) at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
			System.Windows.Forms.Application.DoEvents();
		}
		//mov.b <REG_M>,@<REG_N>        // no implemetation
		public static void i0010_nnnn_mmmm_0000()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			write(r[n],r[m] & 0xFF,1);
		}

		// mov.w <REG_M>,@<REG_N>        // no implemetation
		public static void i0010_nnnn_mmmm_0001()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			write (r[n],r[m] & 0xFFFF,2);
		}

		// mov.l <REG_M>,@<REG_N>        // no implemetation
		public static void i0010_nnnn_mmmm_0010()
		{//TODO : chack this
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			write(r[n],r[m],4);//at r[n],r[m]
		}

		// mov.b <REG_M>,@-<REG_N>       // no implemetation
		public static void i0010_nnnn_mmmm_0100()
		{

		}

		//mov.w <REG_M>,@-<REG_N>       // no implemetation
		public static void i0010_nnnn_mmmm_0101 ()
		{

		}

		//mov.l <REG_M>,@-<REG_N>       // no implemetation
		public static void i0010_nnnn_mmmm_0110 ()
		{//TODO : fADD THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			r[n] -= 4;
			write (r[n],r[m],4);
		}
		// div0s <REG_M>,<REG_N>         // no implemetation
		public static void i0010_nnnn_mmmm_0111()
		{

		}

		// tst <REG_M>,<REG_N>           // no implemetation
		public static void i0010_nnnn_mmmm_1000()
		{//TODO : hADD THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;

			if ((r[n] & r[m])>0)
				sr.T=0;
					else
				sr.T=1;

		}

		//and <REG_M>,<REG_N>           // no implemetation
		public static void i0010_nnnn_mmmm_1001 ()
		{
			n = (opcode>> 8) & 0x0F;
			m = (opcode>> 4) & 0x0F;
			r[n] &= r[m];
		}

		//xor <REG_M>,<REG_N>           // no implemetation
		public static void i0010_nnnn_mmmm_1010 ()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			r[n] ^= r[m];
		}

		//or <REG_M>,<REG_N>            // no implemetation
		public static void i0010_nnnn_mmmm_1011 ()
		{//TODO : fadd this
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;	
			r[n] |= r[m];
		}

		//cmp/str <REG_M>,<REG_N>       // no implemetation
		public static void i0010_nnnn_mmmm_1100 ()
		{

		}

		//xtrct <REG_M>,<REG_N>         // no implemetation
		public static void i0010_nnnn_mmmm_1101 ()
		{

		}

		//mulu <REG_M>,<REG_N>          // no implemetation
		public static void i0010_nnnn_mmmm_1110 ()
		{//TODO : !Add this

		}

		//muls <REG_M>,<REG_N>          // no implemetation
		public static void i0010_nnnn_mmmm_1111 ()
		{

		}
	}

    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		static int rm;
		static int rn;
		public static void iInvalidOpcode()
		{
			dc.dcon.WriteLine("Warning Invalid opcode (3xxx class) at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
			System.Windows.Forms.Application.DoEvents();
		}
		// cmp/eq <REG_M>,<REG_N>        // no implemetation
		public static void i0011_nnnn_mmmm_0000()
		{

		}

		// cmp/hs <REG_M>,<REG_N>        // no implemetation
		public static void i0011_nnnn_mmmm_0010()
		{//TODO : done!Add this
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			if (r[n] >= r[m])
				sr.T=1;
			else
				sr.T=0;
		}

		//cmp/ge <REG_M>,<REG_N>        // no implemetation
		public static void i0011_nnnn_mmmm_0011 ()
		{

		}

		//div1 <REG_M>,<REG_N>          // no implemetation
		public static void i0011_nnnn_mmmm_0100 ()
		{

		}

		//dmulu.l <REG_M>,<REG_N>       // no implemetation
		public static void i0011_nnnn_mmmm_0101 ()
		{

		}

		// cmp/hi <REG_M>,<REG_N>        // no implemetation
		public static void i0011_nnnn_mmmm_0110()
		{//TODO : done!ADD THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;

			if (r[n] > r[m])
				sr.T=1;
			else
				sr.T=0;
		}

		//cmp/gt <REG_M>,<REG_N>        // no implemetation
		public static void i0011_nnnn_mmmm_0111 ()
		{

		}

		// sub <REG_M>,<REG_N>           // no implemetation
		public static void i0011_nnnn_mmmm_1000()
		{//TODO : CHECK THYIS
			n = (opcode>> 8) & 0x0F;
			m = (opcode>> 4) & 0x0F;
			rn=(int)r[n];
			rm=(int)r[m];
			rn -= rm;
	
			r[n]=(uint)rn;
		}

		//subc <REG_M>,<REG_N>          // no implemetation
		public static void i0011_nnnn_mmmm_1010 ()
		{

		}

		//subv <REG_M>,<REG_N>          // no implemetation
		public static void i0011_nnnn_mmmm_1011 ()
		{

		}

		//add <REG_M>,<REG_N>           // no implemetation
		public static void i0011_nnnn_mmmm_1100 ()
		{//TODO : CHECk this
			n = (opcode>> 8) & 0x0F;
			m = (opcode>> 4) & 0x0F;
			rm=(int)r[m];
			rn=(int)r[n];	
			rn += rm;
			r[n]=(uint)rn;
		}

		//dmuls.l <REG_M>,<REG_N>       // no implemetation
		public static void i0011_nnnn_mmmm_1101 ()
		{

		}

		//addc <REG_M>,<REG_N>          // no implemetation
		public static void i0011_nnnn_mmmm_1110 ()
		{

		}

		// addv <REG_M>,<REG_N>          // no implemetation
		public static void i0011_nnnn_mmmm_1111()
		{

		}
	}
    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		static int rn;
		public static void iInvalidOpcode()
		{
			dc.dcon.WriteLine("Warning Invalid opcode (4xxx class) at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
			System.Windows.Forms.Application.DoEvents();
		}

		//sts.l FPUL,@-<REG_N>          // no implemetation
		public static void i0100_nnnn_0101_0010()
		{

		}
		

		//sts.l FPSCR,@-<REG_N>         // no implemetation
		public static void i0100_nnnn_0110_0010()
		{//TODO : fADD THIS
			n = (opcode >> 8) & 0x0F;
			r[n] -= 4;
			write(r[n],fpscr.reg,4);
		}
		

		//sts.l MACH,@-<REG_N>          // no implemetation
		public static void i0100_nnnn_0000_0010()
		{

		}
		

		//sts.l MACL,@-<REG_N>          // no implemetation
		public static void i0100_nnnn_0001_0010()
		{

		}
		

		//sts.l PR,@-<REG_N>            // no implemetation
		public static void i0100_nnnn_0010_0010()
		{//TODO : fAdd this
			n = (opcode >> 8) & 0x0F;
			r[n] -= 4;
			write(r[n],pr,4);
		}
		

		//stc R0_BANK,@-<REG_N>         // no implemetation
		public static void i0100_nnnn_1000_0010()
		{

		}
		

		//stc R1_BANK,@-<REG_N>         // no implemetation
		public static void i0100_nnnn_1001_0010()
		{

		}
		

		//stc R2_BANK,@-<REG_N>         // no implemetation
		public static void i0100_nnnn_1010_0010()
		{

		}
		

		//stc R3_BANK,@-<REG_N>         // no implemetation
		public static void i0100_nnnn_1011_0010()
		{

		}
		

		//stc R4_BANK,@-<REG_N>         // no implemetation
		public static void i0100_nnnn_1100_0010()
		{

		}
		

		//stc R5_BANK,@-<REG_N>         // no implemetation
		public static void i0100_nnnn_1101_0010()
		{

		}
		

		//stc R6_BANK,@-<REG_N>         // no implemetation
		public static void i0100_nnnn_1110_0010()
		{

		}
		

		//stc R7_BANK,@-<REG_N>         // no implemetation
		public static void i0100_nnnn_1111_0010()
		{

		}
		

		//stc.l SR,@-<REG_N>            // no implemetation
		public static void i0100_nnnn_0000_0011()
		{

		}
		

		//stc.l GBR,@-<REG_N>           // no implemetation
		public static void i0100_nnnn_0001_0011()
		{

		}
		

		//stc.l VBR,@-<REG_N>           // no implemetation
		public static void i0100_nnnn_0010_0011()
		{

		}
		

		//stc.l SSR,@-<REG_N>           // no implemetation
		public static void i0100_nnnn_0011_0011()
		{

		}
		

		//stc.l SPC,@-<REG_N>           // no implemetation
		public static void i0100_nnnn_0100_0011()
		{

		}
		

		//lds.l @<REG_N>+,MACH          // no implemetation
		public static void i0100_nnnn_0000_0110()
		{

		}
		

		//lds.l @<REG_N>+,MACL          // no implemetation
		public static void i0100_nnnn_0001_0110()
		{

		}
		

		//lds.l @<REG_N>+,PR            // no implemetation
		public static void i0100_nnnn_0010_0110()
		{//TODO : hADD THIS
			m = (opcode >> 8) & 0x0F;
			pr = read (r[m],4);
			r[m] += 4;
		}
		

		//lds.l @<REG_N>+,FPUL          // no implemetation
		public static void i0100_nnnn_0101_0110()
		{

		}
		

		//lds.l @<REG_N>+,FPSCR         // no implemetation
		public static void i0100_nnnn_0110_0110()
		{
			m = (opcode >> 8) & 0x0F;
			fpscr.reg =read (r[m],4);
			r[m] += 4;
		}
		

		//ldc.l @<REG_N>+,SR            // no implemetation
		public static void i0100_nnnn_0000_0111()
		{

		}
		

		//ldc.l @<REG_N>+,GBR           // no implemetation
		public static void i0100_nnnn_0001_0111()
		{

		}
		

		//ldc.l @<REG_N>+,VBR           // no implemetation
		public static void i0100_nnnn_0010_0111()
		{

		}
		

		//ldc.l @<REG_N>+,SSR           // no implemetation
		public static void i0100_nnnn_0011_0111()
		{

		}
		

		//ldc.l @<REG_N>+,SPC           // no implemetation
		public static void i0100_nnnn_0100_0111()
		{

		}
		

		//ldc.l @<REG_N>+,R0_BANK       // no implemetation
		public static void i0100_nnnn_1000_0111()
		{

		}
		

		//ldc.l @<REG_N>+,R1_BANK       // no implemetation
		public static void i0100_nnnn_1001_0111()
		{

		}
		

		//ldc.l @<REG_N>+,R2_BANK       // no implemetation
		public static void i0100_nnnn_1010_0111()
		{

		}
		

		//ldc.l @<REG_N>+,R3_BANK       // no implemetation
		public static void i0100_nnnn_1011_0111()
		{

		}
		

		//ldc.l @<REG_N>+,R4_BANK       // no implemetation
		public static void i0100_nnnn_1100_0111()
		{

		}
		

		//ldc.l @<REG_N>+,R5_BANK       // no implemetation
		public static void i0100_nnnn_1101_0111()
		{

		}
		

		//ldc.l @<REG_N>+,R6_BANK       // no implemetation
		public static void i0100_nnnn_1110_0111()
		{

		}
		

		//ldc.l @<REG_N>+,R7_BANK       // no implemetation
		public static void i0100_nnnn_1111_0111()
		{

		}
		

		//lds <REG_N>,MACH              // no implemetation
		public static void i0100_nnnn_0000_1010()
		{

		}
		

		//lds <REG_N>,MACL              // no implemetation
		public static void i0100_nnnn_0001_1010()
		{

		}
		

		//lds <REG_N>,PR                // no implemetation
		public static void i0100_nnnn_0010_1010()
		{//TODO : check this
			m = (opcode >> 8) & 0x0F;
			pr = r[m];
		}
		

		//lds <REG_N>,FPUL              // no implemetation
		public static void i0100_nnnn_0101_1010()
		{//TODO : CHECK THIS
			m = (opcode >> 8) & 0x0F;
			fpul =r[m];
		}
		

		//lds <REG_N>,FPSCR             // no implemetation
		public static void i0100_nnnn_0110_1010()
		{//TODO : fADD THIS
			m = (opcode>> 8) & 0x0F;
			fpscr.reg = r[m];
		}
		

		//ldc <REG_N>,SR                // no implemetation
		public static void i0100_nnnn_0000_1110()
		{

		}
		

		//ldc <REG_N>,GBR               // no implemetation
		public static void i0100_nnnn_0001_1110()
		{

		}
		

		//ldc <REG_N>,VBR               // no implemetation
		public static void i0100_nnnn_0010_1110()
		{

		}
		

		//ldc <REG_N>,SSR               // no implemetation
		public static void i0100_nnnn_0011_1110()
		{

		}
		

		//ldc <REG_N>,SPC               // no implemetation
		public static void i0100_nnnn_0100_1110()
		{

		}
		

		//ldc <REG_N>,R0_BANK           // no implemetation
		public static void i0100_nnnn_1000_1110()
		{

		}
		

		//ldc <REG_N>,R1_BANK           // no implemetation
		public static void i0100_nnnn_1001_1110()
		{

		}
		

		//ldc <REG_N>,R2_BANK           // no implemetation
		public static void i0100_nnnn_1010_1110()
		{

		}
		

		//ldc <REG_N>,R3_BANK           // no implemetation
		public static void i0100_nnnn_1011_1110()
		{

		}
		

		//ldc <REG_N>,R4_BANK           // no implemetation
		public static void i0100_nnnn_1100_1110()
		{

		}
		

		//ldc <REG_N>,R5_BANK           // no implemetation
		public static void i0100_nnnn_1101_1110()
		{

		}
		

		//ldc <REG_N>,R6_BANK           // no implemetation
		public static void i0100_nnnn_1110_1110()
		{
//TODO : UPTOWRERE
		}
		

		//ldc <REG_N>,R7_BANK           // no implemetation
		public static void i0100_nnnn_1111_1110()
		{

		}
		

		//shll <REG_N>                  // no implemetation
		public static void i0100_nnnn_0000_0000()
		{

		}
		

		//dt <REG_N>                    // no implemetation
		public static void i0100_nnnn_0001_0000()
		{//TODO : Check this
			n = (opcode >> 8) & 0x0F;
			rn=(int)(r[n]);
			--rn;
			if (rn==0)
				sr.T=1;
					else
				sr.T=0;
			r[n]=(uint)rn;
			

		}
		

		//shal <REG_N>                  // no implemetation
		public static void i0100_nnnn_0010_0000()
		{

		}
		

		//shlr <REG_N>                  // no implemetation
		public static void i0100_nnnn_0000_0001()
		{//TODO : CHECK THIS
		n = (opcode>> 8) & 0x0F;
		sr.T = r[n] & 0x1;
		r[n] >>= 1;
		}
		

		//cmp/pz <REG_N>                // no implemetation
		public static void i0100_nnnn_0001_0001()
		{

		}
		

		//shar <REG_N>                  // no implemetation
		public static void i0100_nnnn_0010_0001()
		{//TODO : !Add this

		}
		

		//rotcl <REG_N>                 // no implemetation
		public static void i0100_nnnn_0010_0100()
		{

		}
		

		//rotl <REG_N>                  // no implemetation
		public static void i0100_nnnn_0000_0100()
		{

		}
		

		//cmp/pl <REG_N>                // no implemetation
		public static void i0100_nnnn_0001_0101()
		{//TODO : !Add this

		}
		

		//rotcr <REG_N>                 // no implemetation
		public static void i0100_nnnn_0010_0101()
		{

		}
		

		//rotr <REG_N>                  // no implemetation
		public static void i0100_nnnn_0000_0101()
		{

		}
		

		//shll2 <REG_N>                 // no implemetation
		public static void i0100_nnnn_0000_1000()
		{//TODO : Check This
			n = (opcode  >> 8) & 0x0F;
			r[n] <<= 2;
		}
		

		//shll8 <REG_N>                 // no implemetation
		public static void i0100_nnnn_0001_1000()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			r[n] <<= 8;
		}
		

		//shll16 <REG_N>                // no implemetation
		public static void i0100_nnnn_0010_1000()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			r[n] <<= 16;
		}
		

		//shlr2 <REG_N>                 // no implemetation
		public static void i0100_nnnn_0000_1001()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			r[n] >>= 2;
		}
		

		//shlr8 <REG_N>                 // no implemetation
		public static void i0100_nnnn_0001_1001()
		{

		}
		

		//shlr16 <REG_N>                // no implemetation
		public static void i0100_nnnn_0010_1001()
		{//TODO : CHECK ME
			n = (opcode >> 8) & 0x0F;
			r[n] >>= 16;
		}
		

		//jmp @<REG_N>                  // no implemetation
		public static void i0100_nnnn_0010_1011()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			delayslot = r[n];
			pc_funct = 2;//jump with delay 1
		}
		

		//jsr @<REG_N>                  // no implemetation
		public static void i0100_nnnn_0000_1011()
		{//TODO : fadd this
			n = (opcode >> 8) & 0x0F;
			pr = pc + 4;
			delayslot= r[n];
			pc_funct = 2;//jump with delay
		}
		

		//tas.b @<REG_N>                // no implemetation
		public static void i0100_nnnn_0001_1011()
		{

		}
		

		//shad <REG_M>,<REG_N>          // no implemetation
		public static void i0100_nnnn_mmmm_1100()
		{

		}
		

		//shld <REG_M>,<REG_N>          // no implemetation
		public static void i0100_nnnn_mmmm_1101()
		{

		}
		

		//mac.w @<REG_M>+,@<REG_N>+     // no implemetation
		public static void i0100_nnnn_mmmm_1111()
		{

		}
	}

    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		//mov.l @(<disp>,<REG_M>),<REG_N>
		public static void i0101_nnnn_mmmm_iiii()
		{//TODO : done!ADD THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			disp = (opcode & 0x0F)*4;
			r[n]=read(r[m]+disp,4);
		}
	}

    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		static int rn;
		static int rm;
		public static void iInvalidOpcode()
		{
			dc.dcon.WriteLine("Warning Invalid opcode (6xxx class) at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
			System.Windows.Forms.Application.DoEvents();
		}
		//mov.b @<REG_M>,<REG_N>        // no implemetation
		public static void i0110_nnnn_mmmm_0000()
		{//TODO : CHEK ME
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			r[n] = (uint)(sbyte) read(r[m], 1);
		} 
		

		//mov.w @<REG_M>,<REG_N>        // no implemetation
		public static void i0110_nnnn_mmmm_0001()
		{//TODO : !Add this

		} 
		

		//mov.l @<REG_M>,<REG_N>        // no implemetation
		public static void i0110_nnnn_mmmm_0010()
		{//TODO : fADD THIS 
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			r[n]=read (r[m],4);
		} 
		

		//mov <REG_M>,<REG_N>           // no implemetation
		public static void i0110_nnnn_mmmm_0011()
		{//CHeck THIS
			n = (opcode>> 8) & 0x0F;
			m = (opcode>> 4) & 0x0F;
			r[n] = r[m];
		}
		

		//mov.b @<REG_M>+,<REG_N>       // no implemetation
		public static void i0110_nnnn_mmmm_0100()
		{//TODO : !Add this

		} 
		

		//mov.w @<REG_M>+,<REG_N>       // no implemetation
		public static void i0110_nnnn_mmmm_0101()
		{

		} 
		

		//mov.l @<REG_M>+,<REG_N>       // no implemetation
		public static void i0110_nnnn_mmmm_0110()
		{//TODO : hADD THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;

			r[n]=read(r[m],4);
			if (n != m)
				r[m] += 4;

		} 
		

		//not <REG_M>,<REG_N>           // no implemetation
		public static void i0110_nnnn_mmmm_0111()
		{

		} 
		

		//swap.b <REG_M>,<REG_N>        // no implemetation
		public static void i0110_nnnn_mmmm_1000()
		{

		} 
		

		//swap.w <REG_M>,<REG_N>        // no implemetation
		public static void i0110_nnnn_mmmm_1001()
		{//TODO : fADD THIS
			n = (opcode>> 8) & 0x0F;
			m = (opcode>> 4) & 0x0F;


			r[n] = ((r[m] >> 16) & 0xFFFF) | ((r[m] << 16) & 0xFFFF0000);
		} 
		

		//negc <REG_M>,<REG_N>          // no implemetation
		public static void i0110_nnnn_mmmm_1010()
		{

		} 
		

		//neg <REG_M>,<REG_N>           // no implemetation
		public static void i0110_nnnn_mmmm_1011()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			rm=(int) r[m];
			rn=(int) r[n];
			r[n] =(uint) -rm;
		} 
		

		//extu.b <REG_M>,<REG_N>        // no implemetation
		public static void i0110_nnnn_mmmm_1100()
		{//TODO : CHECK THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			r[n] = r[m] & 0xFF;
		} 
		

		//extu.w <REG_M>,<REG_N>        // no implemetation
		public static void i0110_nnnn_mmmm_1101()
		{//TODO : fADD THIS
			n = (opcode >> 8) & 0x0F;
			m = (opcode  >> 4) & 0x0F;
			r[n] = r[m] & 0x0000FFFF;
		} 
		

		//exts.b <REG_M>,<REG_N>        // no implemetation
		public static void i0110_nnnn_mmmm_1110()
		{

		} 
		

		//exts.w <REG_M>,<REG_N>        // no implemetation
		public static void i0110_nnnn_mmmm_1111()
		{//TODO : !Add this

		} 
	}
    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		static int s;
		static int rn;
		//add #<imm>,<REG_N>
		public static void i0111_nnnn_iiii_iiii()
		{//TODO : CHACK THIS
			n = (opcode>> 8) & 0x0F;
			s = (int)(sbyte)(opcode & 0xFF);
			rn=((int)r[n])+s;
			r[n]=(uint)rn;
		}
	}

    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		
		public static void iInvalidOpcode()
		{
			dc.dcon.WriteLine("Warning Invalid opcode (8xxx class) at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
			System.Windows.Forms.Application.DoEvents();
		}
		// bf <bdisp8>                   // no implemetation
		public static void i1000_1011_iiii_iiii()
		{//CHeck THIS
			if (sr.T==0)
			{
				delayslot  = (uint)((sbyte)(opcode  & 0xFF))*2 + 4 + pc ;
				pc_funct = 1;//jump , no delay
			}
		}
		

		// bf.s <bdisp8>                 // no implemetation
		public static void i1000_1111_iiii_iiii()
		{//TODO : CHECK THIS
			if (sr.T==0)
			{
				delayslot = (uint)((sbyte) (opcode & 0xFF))*2 + pc + 4;
				pc_funct =2;//delay 1 instruction
			}
		}
		

		// bt <bdisp8>                   // no implemetation
		public static void i1000_1001_iiii_iiii()
		{//TODO done!ADD THIS
			if (sr.T==1)
			{
				delayslot = (uint)((sbyte)(opcode & 0xFF))*2 + pc + 4;
				pc_funct = 1;//direct jump
			}
		}
		

		// bt.s <bdisp8>                 // no implemetation
		public static void i1000_1101_iiii_iiii()
		{//TODO : fADD THIS
			delayslot =(uint) ((sbyte)(opcode & 0xFF))*2 + pc + 4; // antes era disp = ...
			pc_funct = 2;
		}
		

		// cmp/eq #<imm>,R0              // no implemetation
		public static void i1000_1000_iiii_iiii()
		{//TODO : hADD THIS
			m = (uint)(sbyte)(opcode & 0xFF);
			if (r[0] == m)
				sr.T =1;
					else
				sr.T =0;
		}
		

		// mov.b R0,@(<disp>,<REG_M>)    // no implemetation
		public static void i1000_0000_mmmm_iiii()
		{

		}
		

		// mov.w R0,@(<disp>,<REG_M>)    // no implemetation
		public static void i1000_0001_mmmm_iiii()
		{//TODO : !Add this

		}
		

		// mov.b @(<disp>,<REG_M>),R0    // no implemetation
		public static void i1000_0100_mmmm_iiii()
		{

		}
		

		// mov.w @(<disp>,<REG_M>),R0    // no implemetation
		public static void i1000_0101_mmmm_iiii()
		{

		}
	}
    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		//mov.w @(<disp>,PC),<REG_N>   
		public static void i1001_nnnn_iiii_iiii()
		{//TODO : fadd this
			n = (opcode >> 8) & 0x0F;
			disp = (opcode & 0x00FF);
			r[n]=(uint)(short)read(disp*2 + pc + 4,2);
		}
	}
    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		// bra <bdisp12>
		public static void i1010_iiii_iiii_iiii()
		{//TODO CHeck this + singE12
			delayslot = (uint)(((short)((opcode & 0x0FFF)<<4) )>>3)  + pc + 4;//(short<<4,>>4(-1*2))
			pc_funct =2;//jump delay 1
		}
	}

    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		// bsr <bdisp12>
		public static void i1011_iiii_iiii_iiii()
		{//TODO : !Add this

		}
	}
    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;

		public static void iInvalidOpcode()
		{
			dc.dcon.WriteLine("Warning Invalid opcode (Cxxx class) at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
			System.Windows.Forms.Application.DoEvents();
		}
		// mov.b R0,@(<disp>,GBR)        // no implemetation
		public static void i1100_0000_iiii_iiii()
		{

		}
		

		// mov.w R0,@(<disp>,GBR)        // no implemetation
		public static void i1100_0001_iiii_iiii()
		{

		}
		

		// mov.l R0,@(<disp>,GBR)        // no implemetation
		public static void i1100_0010_iiii_iiii()
		{

		}
		

		// trapa #<imm>                  // no implemetation
		public static void i1100_0011_iiii_iiii()
		{

		}
		

		// mov.b @(<disp>,GBR),R0        // no implemetation
		public static void i1100_0100_iiii_iiii()
		{

		}
		

		// mov.w @(<disp>,GBR),R0        // no implemetation
		public static void i1100_0101_iiii_iiii()
		{

		}
		

		// mov.l @(<disp>,GBR),R0        // no implemetation
		public static void i1100_0110_iiii_iiii()
		{

		}
		

		// mova @(<disp>,PC),R0          // no implemetation
		public static void i1100_0111_iiii_iiii()
		{//TODO : CHECK THIS
			disp = (opcode & 0x00FF)*4 +  ((pc + 4) & 0xFFFFFFFC);
			r[0] = disp;
		}
		

		// tst #<imm>,R0                 // no implemetation
		public static void i1100_1000_iiii_iiii()
		{//TODO : !Add this

		}
		

		// and #<imm>,R0                 // no implemetation
		public static void i1100_1001_iiii_iiii()
		{//TODO : !Add this

		}
		

		// xor #<imm>,R0                 // no implemetation
		public static void i1100_1010_iiii_iiii()
		{

		}
		

		// or #<imm>,R0                  // no implemetation
		public static void i1100_1011_iiii_iiii()
		{//TODO : !Add this

		}
		

		// tst.b #<imm>,@(R0,GBR)        // no implemetation
		public static void i1100_1100_iiii_iiii()
		{

		}
		

		// and.b #<imm>,@(R0,GBR)        // no implemetation
		public static void i1100_1101_iiii_iiii()
		{

		}
		

		// xor.b #<imm>,@(R0,GBR)        // no implemetation
		public static void i1100_1110_iiii_iiii()
		{

		}
		

		// or.b #<imm>,@(R0,GBR)         // no implemetation
		public static void i1100_1111_iiii_iiii()
		{

		}
	}
    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		// mov.l @(<disp>,PC),<REG_N>    
		public static void i1101_nnnn_iiii_iiii()
		{//TODO : CHeck it
			n = (opcode >> 8) & 0x0F;
			disp = (opcode & 0xFF);
			r[n]=read(disp*4 + (pc & 0xFFFFFFFC) + 4,4);
		}
	}

    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		// mov #<imm>,<REG_N>
		public static void i1110_nnnn_iiii_iiii()
		{//TODO : check this
			n = (opcode >> 8) & 0x0F;
			r[n] = (uint)(sbyte)(opcode & 0xFF);//(uint)(sbyte)= signextend8 :)
		}
	}

    #if nrt 
    public static partial  class emu r
 #else  
    public partial  class emu 
 #endif
    {
		static uint n;
		static uint m;
		static uint disp;
		const double pi= 3.1415926535;
		public static void iInvalidOpcode()
		{
			dc.dcon.WriteLine("Warning Invalid opcode (Fxxx class) at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
			System.Windows.Forms.Application.DoEvents();
		}

		//fadd <FREG_M>,<FREG_N>        no implemetation
		public static void i1111_nnnn_mmmm_0000()
		{//TODO : CHECK THIS PR FP
			n = (opcode>> 8) & 0x0F;
			m = (opcode>> 4) & 0x0F;
			fr[n] += fr[m];
		}
														   

		//fsub <FREG_M>,<FREG_N>        no implemetation
		public static void i1111_nnnn_mmmm_0001()
		{

		}
																											  

		//fmul <FREG_M>,<FREG_N>        no implemetation
		public static void i1111_nnnn_mmmm_0010()
		{
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			fr[n] *= fr[m];
		}
																																								 

		//fdiv <FREG_M>,<FREG_N>        no implemetation
		public static void i1111_nnnn_mmmm_0011()
		{//TODO : CHECK THIS + PRMODE FP
			n = (opcode>> 8) & 0x0F;
			m = (opcode>> 4) & 0x0F;
			fr[n] /= fr[m];

		}
																																																					

		//fcmp/eq <FREG_M>,<FREG_N>     no implemetation
		public static void i1111_nnnn_mmmm_0100()
		{

		}
																																																																	   

		//fcmp/gt <FREG_M>,<FREG_N>     no implemetation
		public static void i1111_nnnn_mmmm_0101()
		{

		}
																																																																														  

		//fmov.s @(R0,<REG_M>),<FREG_N> no implemetation
		public static void i1111_nnnn_mmmm_0110()
		{

		}


		//fmov.s <FREG_M>,@(R0,<REG_N>) no implemetation
		public static void i1111_nnnn_mmmm_0111()
		{

		}


		//fmov.s @<REG_M>,<FREG_N>      no implemetation
		public static unsafe void i1111_nnnn_mmmm_1000()
		{//TODO : CHECK PR SZ FP
			n = (opcode>> 8) & 0x0F;
			m = (opcode>> 4) & 0x0F;
			uint tmp=read(r[m],4);
			fr[n]=*(float*)&tmp;
		}


		//fmov.s @<REG_M>+,<FREG_N>     no implemetation
		public static void i1111_nnnn_mmmm_1001()
		{

		}


		//fmov.s <FREG_M>,@<REG_N>      no implemetation
		public static unsafe void i1111_nnnn_mmmm_1010()
		{//	TODO : hadd this
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			fixed (float*p=&fr[m]){write(r[n],*(uint*)p,4);}
		}


		//fmov.s <FREG_M>,@-<REG_N>     no implemetation
		public static void i1111_nnnn_mmmm_1011()
		{

		}


		//fmov <FREG_M>,<FREG_N>        no implemetation
		public static void i1111_nnnn_mmmm_1100()
		{//TODO : checkthis
			n = (opcode >> 8) & 0x0F;
			m = (opcode >> 4) & 0x0F;
			fr[n] = fr[m];
		}


		//fabs <FREG_N>                 no implemetation
		public static void i1111_nnnn_0101_1101()
		{

		}

		// FSCA FPUL, DRn//F0FD//1111_nnnn_1111_1101
		public static void i1111_nnn0_1111_1101()
		{
			n = (opcode>>9) & 0x07;
			float x = (float) (2 * pi * (float) fpul / 65536.0);
			fr[n*2] = (float)System.Math.Sin(x);
			fr[n*2+1] =(float) System.Math.Cos(x);
		}

		//fcnvds <DR_N>,FPUL            no implemetation
		public static void i1111_nnnn_1011_1101()
		{

		}


		//fcnvsd FPUL,<DR_N>            no implemetation
		public static void i1111_nnnn_1010_1101()
		{

		}

		//fipr <FV_M>,<FV_N>            
		public static void i1111_nnmm_1110_1101()
		{

		}


		//fldi0 <FREG_N>                no implemetation
		public static void i1111_nnnn_1000_1101()
		{

		}


		//fldi1 <FREG_N>                no implemetation
		public static void i1111_nnnn_1001_1101()
		{

		}


		//flds <FREG_N>,FPUL            no implemetation
		public static void i1111_nnnn_0001_1101()
		{

		}


		//float FPUL,<FREG_N>           no implemetation
		public static void i1111_nnnn_0010_1101()
		{//TODO : CHECK THIS (FP)
			n = (opcode >> 8) & 0x0F;
			fr[n] = (float) (int)fpul; 
		}


		//fneg <FREG_N>                 no implemetation
		public static void i1111_nnnn_0100_1101()
		{

		}


		//frchg                         no implemetation
		public static void i1111_1011_1111_1101()
		{

		}


		//fschg                         no implemetation
		public static void i1111_0011_1111_1101()
		{

		}

		//fsqrt <FREG_N>                
		public static void i1111_nnnn_0110_1101()
		{

		}


		//ftrc <FREG_N>, FPUL           no implemetation
		public static void i1111_nnnn_0011_1101()
		{
			m = (opcode >> 8) & 0x0F;
			fpul =  (uint)(int)fr[m];
		}


		//fsts FPUL,<FREG_N>            no implemetation
		public static void i1111_nnnn_0000_1101()
		{

		}


		//fmac <FREG_0>,<FREG_M>,<FREG_N> no implemetation
		public static void i1111_nnnn_mmmm_1110()
		{

		}


		//ftrv xmtrx,<FV_N>             no implemetation
		public static void i1111_nn01_1111_1101()
		{

		}
	}
}
