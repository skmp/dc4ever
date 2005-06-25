//This is a part of the DC4Ever emulator
//You are not allowed to release modified(or unmodified) versions
//without asking me (drk||Raziel).
//For Suggestions ect please e-mail at : stef_mp@yahoo.gr
//Note : This is just to prove that Fast emulation is possible with 
//	C# /.net ...And yes , this code could be writen at VB.net and 
//	run more or less at the same speed on dynarec mode
//	This code requires C#.net 2.0 (Get the C# epxress 2005 Beta from microsoft)
//

//#define interpreter
//Define this to get a exe that runs on iterpreter mode

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;

namespace DC4Ever
{
	/// <summary>
	/// Summary description for 
	/// </summary>
    public unsafe static partial  class emu
    {
        public static ulong gl_cop_cnt=0;
		public const uint dc_boot_vec = 0x8C00b800;//0x8C008300;
		const uint dc_bios_vec = 0xA0000000;
		#region sh4 regs and shit declares
		//consts
		const ushort tmu_underflow = 0x0100;
        //sr bits
        const uint sr_T_bit_set = 1;//to set with or
        const uint sr_T_bit_reset = uint.MaxValue - sr_T_bit_set;//to unset with and
        
        //sr,fpscr reg emulation class
        public class sr
		{

			public static uint T,S,IMASK,Q,M,FD,BL;
            public static uint RB_, MD_;
			public static uint cbm = 0;
            public static void srregInit()
			{
				//T = 0; S = 0; IMASK = 1; Q = 0; M = 0; FD = 0; BL = 1; MD = 1; RB = 1;
				reg=0x700000F0;
			}
			public static uint RB// Register bank change..
			{
				get {return RB_;}
				set
				{
					if (MD_ != 0)
					{	//on privilaged mode we can swicth regs
						if (value != RB_)
						{
							RBchange();
							RB_ = value;
						}
					}
					else
					{	//on user mode .. we must use rb=0
						if (RB_ != 0)
						{
							RBchange();
							RB_ = 0;
						}
					}
				}
			}
			public static uint MD// Mode Change..
			{
				get {return MD_;}
				set {
						if (MD_!=value) 
						{
							MD_ = value;
							//RBchange();
							RB = 1-RB_;
							//TODO : Check SR.md=0<sr.rb=1>-->?
						} 
						//MD_=value;
					}
			}
			public static uint reg
			{
				get
				{
					return T|(S<<1)|(IMASK<<4)|(Q<<8)|(M<<9)|
						(FD<<15)|(BL<<28)|(RB<<29)|(MD<<30);
				}
				set
				{
					T=value&0x1;
					S = (value >> 1) & 0x1;
					IMASK = (value >> 4) & 0xF;
					Q = (value >> 8) & 0x1;
					M = (value >> 9) & 0x1;
					FD = (value >> 15) & 0x1;
					BL = (value >> 28) & 0x1;
					MD = (value >> 30) & 0x1;
					RB = (value >> 29) & 0x1;
					if (sr.cbm != sr.RB_)
						sr.RB = sr.RB_;
				}
			}
		}
		public class fpscr
		{
			public static uint RM,finexact,funderflow,foverflow,fdivbyzero,
				 finvalidop,einexact,eunderflow,eoverflow,edivbyzero,
				 einvalidop,cinexact,cunderflow,coverflow,cdivbyzero,
				 cinvalid,cfpuerr,DN,SZ;
            public static uint PR, FR_;
            public static void init()
			{
				reg=0x0004001;
			}
			public static uint reg
			{
				get
				{
					return RM|(finexact<<2)|(funderflow<<3)|(foverflow<<4)|(fdivbyzero<<5)|(
						   finvalidop<<6)|(einexact<<7)|(eunderflow<<8)|(eoverflow<<9)|(edivbyzero<<10)|(
						   einvalidop<<11)|(cinexact<<12)|(cunderflow<<13)|(coverflow<<14)|(cdivbyzero<<15)|(
						   cinvalid<<16)|(cfpuerr<<17)|(DN<<18)|(PR<<19)|(SZ<<20)|(FR<<21);
				}
				set
				{
				RM=value&0x3;
				finexact=(value>>2)&0x1;
				funderflow=(value>>3)&0x1;
				foverflow=(value>>4)&0x1;
				fdivbyzero=(value>>5)&0x1;
				finvalidop=(value>>6)&0x1;
				einexact=(value>>7)&0x1;
				eunderflow=(value>>8)&0x1;
				eoverflow=(value>>9)&0x1;
				edivbyzero=(value>>10)&0x1;
				einvalidop=(value>>11)&0x1;
				cinexact=(value>>12)&0x1;
				cunderflow=(value>>13)&0x1;
				coverflow=(value>>14)&0x1;
				cdivbyzero=(value>>15)&0x1;
				cinvalid=(value>>16)&0x1;
				cfpuerr=(value>>17)&0x1;
				DN=(value>>18)&0x1;
				PR=(value>>19)&0x1;
				SZ=(value>>20)&0x1;
				FR=(value>>21)&0x1;
				}
			}
			/*public static uint PR
			{
				get{return PR_;}
				set{if (value!=PR_){PR_=value; PRchange();}}
			}*/
			public static uint FR
			{
				get{return FR_;}
				set{if (value!=FR_) FRchange();FR_=value;}
			}

		}

        public static uint gbr,ssr,spc,sgr,dbr,vbr;
		public static uint mach,macl,pr,fpul;
		public static uint pc;

		public static uint* r = (uint*)dc.mmgr.AllocMem(16 * sizeof(uint));
		public static byte* r_byte = (byte*)r;
		public static ushort* r_word = (ushort*)r;
		public static uint* r_bank = (uint*)dc.mmgr.AllocMem(8 * sizeof(uint));
		public static float* fr = (float*)dc.mmgr.AllocMem(16 * sizeof(float));//fp regs set 1
		public static float* xr = (float*)dc.mmgr.AllocMem(16 * sizeof(float));//fp regs set 2
        //todo DR/s
		//public static double* dr = (double*)fr;//fp regs set 1, in double form
		static double* dat_dr = (double*)dc.mmgr.AllocMem(8 * sizeof(float));
		public unsafe class dr_regs
		{
			public double this[int i]
			{

				get
				{
					uint* t = (uint*)dat_dr;
					t[0] = fr_uint[(i << 1) | 1];
					t[1] = fr_uint[(i << 1)];
					return *(double*)t;
				}
				set
				{
					double val = value;
					uint* t = (uint*)&val;
					fr_uint[(i << 1) | 1] = t[0];
					fr_uint[(i << 1)] = t[1];
				}
			}
			public double this[uint i]
			{

				get
				{
					uint* t = (uint*)dat_dr;
					t[0] = fr_uint[(i << 1) | 1];
					t[1] = fr_uint[(i << 1)];
					return *(double*)t;
				}
				set
				{
					double val = value;
					uint* t = (uint*)&val;
					fr_uint[(i << 1) | 1] = t[0];
					fr_uint[(i << 1)] = t[1];
				}
			}
		}
		public static dr_regs dr = new dr_regs();
		//public static double* xr_dbl = (double*)xr;//fp regs set 2, in double form

        public static uint* fr_uint = (uint*)fr;//fp regs set 1, in uint form
        public static uint* xr_uint = (uint*)xr;//fp regs set 2, in uint form

		#endregion

		#region Internal vars ect
		const double pi= 3.1415926535;
		public static uint clcount=0;
		public static uint clc=386950;//perhaps boot time.. fix for video refresh sync
		//for use with interpreter
        static uint opcode;
		/*static uint m;
		static uint n;
		static int rm;
		static int rn;
		static uint disp;
        static int stmp1;
        static uint utmp1;
        static int stmp2;
        static uint utmp2;
        static int stmp3;
        static uint utmp3;

        static uint tmp0;
        static uint tmp1;
		static uint tmp2;
		static uint tmp3;
		static uint tmp4;*/
        public static bool runsh;
		static int pc_funct;
		static uint delayslot;
        static uint[] ccount = new uint[65536+100];
		#endregion

		#region Interpreter Main Loop
		public static unsafe void runcpu()
		{
			//System.IO.StreamReader file = new System.IO.StreamReader("c:\\dclog.txt");

            dc.dcon.WriteLine("Runing in Interpreter Mode");

			//uint* lops = stackalloc uint[85535];
            runsh =true;
            uint tc=0;
			//bool log_opts = false;

			//biosmem_w[0x998 >> 1] = 0x9;
			//
			//bios_file[] = 0x9;
			//bios_file[0x38e +1 ] = 0x0;

			//bios_file[0x98a] = 0x9;
			//bios_file[0x98a + 1] = 0x0;
		
            do
			{
				#region dbshit
				//if (gl_cop_cnt > 0x82c741)
				//{
				/*string st = null; 
				if (pc.ToString() != (st=file.ReadLine()))
				{
					dc.dbger.mode = 1;
					pc = Convert.ToUInt32(st);
				}
				if (r[0].ToString() != (st=file.ReadLine()))
				{
					dc.dbger.mode = 1;
					r[0] = Convert.ToUInt32(st);
				}
				if (r[1].ToString() != (st=file.ReadLine()))
				{
					dc.dbger.mode = 1;
					r[1] = Convert.ToUInt32(st);
				}
				if (r[2].ToString() != (st=file.ReadLine()))
				{
					dc.dbger.mode = 1;
					r[2] = Convert.ToUInt32(st);
				}
				if (r[3].ToString() != (st=file.ReadLine()))
				{
					dc.dbger.mode = 1;
					r[3] = Convert.ToUInt32(st);
				}
				if (r[4].ToString() != (st=file.ReadLine()))
				{
					dc.dbger.mode = 1;
					r[4] = Convert.ToUInt32(st);
				}
				if (r[5].ToString() != (st=file.ReadLine()))
				{
					dc.dbger.mode = 1;
					r[5] = Convert.ToUInt32(st);
				}
				if (r[6].ToString() != (st=file.ReadLine()))
				{
					dc.dbger.mode = 1;
					r[6] = Convert.ToUInt32(st);
				}
				if (r[7].ToString() != (st=file.ReadLine()))
				{
					dc.dbger.mode = 1;
					r[7] = Convert.ToUInt32(st);
				}*/
				//file.WriteLine(pc.ToString() + "\n");
				//}
				#endregion

				gl_cop_cnt++;
				//if (pc == 0x8c017880)
				//	log_opts = true;

                #region Interpreter
                opcode=read(pc,3);
				//sh4_opcodes_test.testop(opcode);//check for any switch missmatches
				//if (fpscr.PR==1)
					//WriteLine("Double mode is not emulated;" +  DisasmOpcode(opcode,pc));
				#region debug 
				/*
				if (log_opts)
					lops[opcode]++;
				if (dc.dbger.mode != 0)
				{
					System.Text.StringBuilder sb = new System.Text.StringBuilder();
					for (int i2a = 0; i2a < 16; i2a++)
					{
						for (int i2b = 0; i2b < 16; i2b++)
						{
							for (int i2c = 0; i2c < 256; i2c++)
							{
								int t=(i2a << 12) | (i2b << 0)| (i2c << 4);
								if (lops[t] != 0)
								{
									sb.Append(UintToHex((uint)t));
									sb.Append(' ');
									sb.Append(lops[t]);
									sb.Append(' ');
									sb.Append(DisasmOpcode((uint)t, pc));
									sb.Append("\r\n");


								}
							}
						}
					}
					File.WriteAllText("afile.txt", sb.ToString());
				}*/
				#endregion
				#if !optimised_b
				if (dc.dbger.CpuStep()) goto SkipOpcode;
				#endif
				//file.Close();
                tc = ccount[opcode];
				//if (opcode == 0x40f3)
//					break;
                #region interpreter
                switch (opcode>>12)//proc opcode
                {
                    case 0x0://finished
						#region case 0x0
					    switch (opcode&0xf)
					    {
						    case 0x0://0000
                                iInvalidOpcode();
							    break;
						    case 0x1://0001
                                iInvalidOpcode();
                                break;
						    case 0x2://0010
							    #region case 0x2 multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        i0000_nnnn_0000_0010();
								        break;
							        case 0x1://0001
                                        i0000_nnnn_0001_0010();
                                        break;
							        case 0x2://0010
								        i0000_nnnn_0010_0010();
								        break;
							        case 0x3://0011
								        i0000_nnnn_0011_0010();
								        break;
							        case 0x4://0100
								        i0000_nnnn_0100_0010();
								        break;
							        case 0x8://1000
								        i0000_nnnn_1000_0010();
								        break;
							        case 0x9://1001
								        i0000_nnnn_1001_0010();
								        break;
							        case 0xA://1010
								        i0000_nnnn_1010_0010();
								        break;
							        case 0xB://1011
								        i0000_nnnn_1011_0010();
								        break;
							        case 0xC://1100
								        i0000_nnnn_1100_0010();
								        break;
							        case 0xD://1101
								        i0000_nnnn_1101_0010();
								        break;
							        case 0xE://1110
								        i0000_nnnn_1110_0010();
								        break;
							        case 0xF://1111
								        i0000_nnnn_1111_0010();
								        break;
							        default:
                                        iInvalidOpcode();
                                        break;	
						        }
							    #endregion
							    break;
						    case 0x3://0011
							    #region case 0x3 multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        i0000_nnnn_0000_0011();
								        break;
							        case 0x2://0010
								        i0000_nnnn_0010_0011();
								        break;
							        case 0x8://1000
								        i0000_nnnn_1000_0011();
								        break;
							        case 0x9://1001
								        i0000_nnnn_1001_0011();
								        break;
							        case 0xA://1010
								        i0000_nnnn_1010_0011();
								        break;
							        case 0xB://1011
								        i0000_nnnn_1011_0011();
								        break;
							        case 0xC://1100
								        i0000_nnnn_1100_0011();
								        break;
							        default:
                                        iInvalidOpcode();
                                        break;
						        }
							    #endregion
							    break;
						    case 0x4://0100
							    i0000_nnnn_mmmm_0100();
							    break;
						    case 0x5://0101
							    i0000_nnnn_mmmm_0101();
							    break;
						    case 0x6://0110
							    i0000_nnnn_mmmm_0110();
							    break;
						    case 0x7://0111
							    i0000_nnnn_mmmm_0111();
							    break;
						    case 0x8://1000
							    #region case 0x8 multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        i0000_0000_0000_1000();
								        break;
							        case 0x1://0001
								        i0000_0000_0001_1000();
								        break;
							        case 0x2://0010
								        i0000_0000_0010_1000();
								        break;
							        case 0x3://0011
								        i0000_0000_0011_1000();
								        break;
							        case 0x4://0100
								        i0000_0000_0100_1000();
								        break;
							        case 0x5://0101
								        i0000_0000_0101_1000();
								        break;
							        default:
                                        iInvalidOpcode();
                                        break;
						        }
							    #endregion
							    break;
						    case 0x9://1001
							    #region case 0x9 multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        i0000_0000_0000_1001();
								        break;
							        case 0x1://0001
								        i0000_0000_0001_1001();
								        break;
							        case 0x2://0010
								        i0000_nnnn_0010_1001();
								        break;
							        default:
                                        iInvalidOpcode();
                                        break;
						        }
							    #endregion
							    break;
						    case 0xA://1010
							    #region case 0xA multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        i0000_nnnn_0000_1010();
								        break;
							        case 0x1://0001
								        i0000_nnnn_0001_1010();
								        //n=  (opcode >> 8) & 0x0F;
								        //r[n]=macl; 
								        break;
							        case 0x2://0010
								        i0000_nnnn_0010_1010();
								        break;
							        case 0x5://0101
								        i0000_nnnn_0101_1010();
								        break;
							        case 0x6://0110
								        i0000_nnnn_0110_1010();
								        break;
									case 0xf://1111
										i0000_nnnn_1111_1010();
										break;
							        default:
                                        iInvalidOpcode();
                                        break;
						        }
							    #endregion
							    break;
						    case 0xB://1011
							    #region case 0xB multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        i0000_0000_0000_1011();
								        break;
							        case 0x1://0001
								        i0000_0000_0001_1011();
								        break;
							        case 0x2://0010
								        i0000_0000_0010_1011();
								        break;
							        default:
								        iInvalidOpcode();
								        break;
						        }
							    #endregion
							    break;
						    case 0xC://1100
							    i0000_nnnn_mmmm_1100();
							    break;
						    case 0xD://1101
							    i0000_nnnn_mmmm_1101();
							    break;
						    case 0xE://1110
							    i0000_nnnn_mmmm_1110();
							    break;
						    case 0xF://1111
							    i0000_nnnn_mmmm_1111();
							    break;
					    }
						#endregion
						break;
                    case 0x1://finished
						i0001_nnnn_mmmm_iiii();
						break;
					case 0x2://finished
						#region case 0x2
					switch (opcode&0xf)
					{
						case 0x0://0000
							i0010_nnnn_mmmm_0000();
							break;
						case 0x1://0001
							i0010_nnnn_mmmm_0001();
							break;
						case 0x2://0010
							i0010_nnnn_mmmm_0010();
							break;
						case 0x4://0100
							i0010_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0010_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0010_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0010_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i0010_nnnn_mmmm_1000();
							break;
						case 0x9://1001
							i0010_nnnn_mmmm_1001();
							break;
						case 0xA://1010
							i0010_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i0010_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i0010_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0010_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0010_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0010_nnnn_mmmm_1111();
							break;
						default:
							iInvalidOpcode();
							break;
					}
					#endregion
						break;
					case 0x3://finished
						#region case 0x3
					switch (opcode&0xf)
					{
						case 0x0://0000
							i0011_nnnn_mmmm_0000();
							break;
						case 0x2://0010
							i0011_nnnn_mmmm_0010();
							break;
						case 0x3://0011
							i0011_nnnn_mmmm_0011();
							break;
						case 0x4://0100
							i0011_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0011_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0011_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0011_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i0011_nnnn_mmmm_1000();
							break;
						case 0xA://1010
							i0011_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i0011_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i0011_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0011_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0011_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0011_nnnn_mmmm_1111();
							break;
						default:
							iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0x4://finished
						#region case 0x4
					switch (opcode&0xf)
					{
						case 0x0://0000
							#region 0x0 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0000
								i0100_nnnn_0000_0000();
								break;
							case 0x1://0100_xxxx_0001_0000
								i0100_nnnn_0001_0000();
								break;
							case 0x2://0100_xxxx_0010_0000
								i0100_nnnn_0010_0000();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x1://0001
							#region 0x1 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0001
								i0100_nnnn_0000_0001();
								break;
							case 0x1://0100_xxxx_0001_0001
								i0100_nnnn_0001_0001();
								break;
							case 0x2://0100_xxxx_0010_0001
								i0100_nnnn_0010_0001();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x2://0010
							#region 0x2 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0010
								i0100_nnnn_0000_0010();
								break;
							case 0x1://0100_xxxx_0001_0010
								i0100_nnnn_0001_0010();
								break;
							case 0x2://0100_xxxx_0010_0010
								i0100_nnnn_0010_0010();
								break;
							case 0x5://0100_xxxx_0101_0010
								i0100_nnnn_0101_0010();
								break;
							case 0x6://0100_xxxx_0110_0010
								i0100_nnnn_0110_0010();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x3://0011
							#region 0x3 multi
							if (((opcode >> 7) & 0x1) != 0)
							{
								i0100_nnnn_1mmm_0011();
							}
							else
							{
								switch ((opcode >> 4) & 0xf)
								{
									case 0x0://0100_xxxx_0000_0011
										i0100_nnnn_0000_0011();
										break;
									case 0x1://0100_xxxx_0001_0011
										i0100_nnnn_0001_0011();
										break;
									case 0x2://0100_xxxx_0010_0011
										i0100_nnnn_0010_0011();
										break;
									case 0x3://0100_xxxx_0011_0011
										i0100_nnnn_0011_0011();
										break;
									case 0x4://0100_xxxx_0100_0011
										i0100_nnnn_0100_0011();
										break;
									default:
										iInvalidOpcode();
										break;
								}
							}
							#endregion 
							break;
						case 0x4://0100
							#region 0x4 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0100
								i0100_nnnn_0000_0100();
								break;
							case 0x2://0100_xxxx_0010_0100
								i0100_nnnn_0010_0100();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x5://0101
							#region 0x5 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0101
								i0100_nnnn_0000_0101();
								break;
							case 0x1://0100_xxxx_0001_0101
								i0100_nnnn_0001_0101();
								break;
							case 0x2://0100_xxxx_0010_0101
								i0100_nnnn_0010_0101();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x6://0110
							#region 0x6 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0110
								i0100_nnnn_0000_0110();
								break;
							case 0x1://0100_xxxx_0001_0110
								i0100_nnnn_0001_0110();
								break;
							case 0x2://0100_xxxx_0010_0110
								i0100_nnnn_0010_0110();
								break;
							case 0x5://0100_xxxx_0101_0110
								i0100_nnnn_0101_0110();
								break;
							case 0x6://0100_xxxx_0110_0110
								i0100_nnnn_0110_0110();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x7://0111
							#region 0x7 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0111
								i0100_nnnn_0000_0111();
								break;
							case 0x1://0100_xxxx_0001_0111
								i0100_nnnn_0001_0111();
								break;
							case 0x2://0100_xxxx_0010_0111
								i0100_nnnn_0010_0111();
								break;
							case 0x3://0100_xxxx_0011_0111
								i0100_nnnn_0011_0111();
								break;
							case 0x4://0100_xxxx_0100_0111
								i0100_nnnn_0100_0111();
								break;
							case 0x8://0100_xxxx_1000_0111
								i0100_nnnn_1000_0111();
								break;
							case 0x9://0100_xxxx_1001_0111
								i0100_nnnn_1001_0111();
								break;
							case 0xA://0100_xxxx_1010_0111
								i0100_nnnn_1010_0111();
								break;
							case 0xB://0100_xxxx_1011_0111
								i0100_nnnn_1011_0111();
								break;
							case 0xC://0100_xxxx_1100_0111
								i0100_nnnn_1100_0111();
								break;
							case 0xD://0100_xxxx_1101_0111
								i0100_nnnn_1101_0111();
								break;
							case 0xE://0100_xxxx_1110_0111
								i0100_nnnn_1110_0111();
								break;
							case 0xF://0100_xxxx_1111_0111
								i0100_nnnn_1111_0111();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x8://1000
							#region 0x8 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1000
								i0100_nnnn_0000_1000();
								break;
							case 0x1://0100_xxxx_0001_1000
								i0100_nnnn_0001_1000();
								break;
							case 0x2://0100_xxxx_0010_1000
								i0100_nnnn_0010_1000();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x9://1001
							#region 0x9 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1001
								i0100_nnnn_0000_1001();
								break;
							case 0x1://0100_xxxx_0001_1001
								i0100_nnnn_0001_1001();
								break;
							case 0x2://0100_xxxx_0010_1001
								i0100_nnnn_0010_1001();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;

						case 0xA://1010
							#region 0x9 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1010
								i0100_nnnn_0000_1010();
								break;
							case 0x1://0100_xxxx_0001_1010
								i0100_nnnn_0001_1010();
								break;
							case 0x2://0100_xxxx_0010_1010
								i0100_nnnn_0010_1010();
								break;
							case 0x5://0100_xxxx_0101_1010
								i0100_nnnn_0101_1010();
								break;
							case 0x6://0100_xxxx_0110_1010
								i0100_nnnn_0110_1010();
								break;
							case 0xf://0100_xxxx_0110_1010
								i0100_nnnn_1111_1010();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;

						case 0xB://1011
							#region 0xB multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1011
								i0100_nnnn_0000_1011();
								break;
							case 0x1://0100_xxxx_0001_1011
								i0100_nnnn_0001_1011();
								break;
							case 0x2://0100_xxxx_0010_1011
								i0100_nnnn_0010_1011();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0xC://1100
							i0100_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0100_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							#region 0xE multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1110
								i0100_nnnn_0000_1110();
								break;
							case 0x1://0100_xxxx_0001_1110
								i0100_nnnn_0001_1110();
								break;
							case 0x2://0100_xxxx_0010_1110
								i0100_nnnn_0010_1110();
								break;
							case 0x3://0100_xxxx_0011_1110
								i0100_nnnn_0011_1110();
								break;
							case 0x4://0100_xxxx_0100_1110
								i0100_nnnn_0100_1110();
								break;
							case 0x8://0100_xxxx_1000_1110
								i0100_nnnn_1000_1110();
								break;
							case 0x9://0100_xxxx_1001_1110
								i0100_nnnn_1001_1110();
								break;
							case 0xA://0100_xxxx_1010_1110
								i0100_nnnn_1010_1110();
								break;
							case 0xB://0100_xxxx_1011_1110
								i0100_nnnn_1011_1110();
								break;
							case 0xC://0100_xxxx_1100_1110
								i0100_nnnn_1100_1110();
								break;
							case 0xD://0100_xxxx_1101_1110
								i0100_nnnn_1101_1110();
								break;
							case 0xE://0100_xxxx_1110_1110
								i0100_nnnn_1110_1110();
								break;
							case 0xF://0100_xxxx_1111_1110
								i0100_nnnn_1111_1110();
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0xF://1111
							i0100_nnnn_mmmm_1111();
							break;
						default:
							iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0x5://finished
						i0101_nnnn_mmmm_iiii();
						break;
					case 0x6://finished
						#region case 0x6
					switch (opcode&0xf)
					{
						case 0x0://0000
							i0110_nnnn_mmmm_0000();
							break;
						case 0x1://0001
							i0110_nnnn_mmmm_0001();
							break;
						case 0x2://0010
							i0110_nnnn_mmmm_0010();
							break;
						case 0x3://0011
							i0110_nnnn_mmmm_0011();
							break;
						case 0x4://0100
							i0110_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0110_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0110_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0110_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i0110_nnnn_mmmm_1000();
							break;
						case 0x9://1001
							i0110_nnnn_mmmm_1001();
							break;
						case 0xA://1010
							i0110_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i0110_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i0110_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0110_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0110_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0110_nnnn_mmmm_1111();
							break;
						default:
							iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0x7://finished
						i0111_nnnn_iiii_iiii();
						break;
					case 0x8://finished
						#region case 0x8
					switch ((opcode>>8)&0xf)
					{
						case 0x0://0000
							i1000_0000_mmmm_iiii();
							break;
						case 0x1://0001
							i1000_0001_mmmm_iiii();
							break;
						case 0x4://0100
							i1000_0100_mmmm_iiii();
							break;
						case 0x5://0101
							i1000_0101_mmmm_iiii();
							break;
						case 0x8://1000
							i1000_1000_iiii_iiii();
							break;
						case 0x9://1001
							i1000_1001_iiii_iiii();
							break;
						case 0xB://1011
							i1000_1011_iiii_iiii();
                            break;
						case 0xD://1101
							i1000_1101_iiii_iiii();
							break;
						case 0xF://1111
							i1000_1111_iiii_iiii();
							break;
						default:
							iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0x9://finished
						i1001_nnnn_iiii_iiii();
						break;
					case 0xA://finished
						i1010_iiii_iiii_iiii();
						break;
					case 0xB://finished
						i1011_iiii_iiii_iiii();
						break;
					case 0xC://finished
						#region case 0xC
					switch ((opcode>>8)&0xf)
					{
						case 0x0://0000
							i1100_0000_iiii_iiii();
							break;
						case 0x1://0001
							i1100_0001_iiii_iiii();
							break;
						case 0x2://0010
							i1100_0010_iiii_iiii();
							break;
						case 0x3://0011
							i1100_0011_iiii_iiii();
							break;
						case 0x4://0100
							i1100_0100_iiii_iiii();
							break;
						case 0x5://0101
							i1100_0101_iiii_iiii();
							break;
						case 0x6://0110
							i1100_0110_iiii_iiii();
							break;
						case 0x7://0111
							i1100_0111_iiii_iiii();
							break;
						case 0x8://1000
							i1100_1000_iiii_iiii();
							break;
						case 0x9://1001
							i1100_1001_iiii_iiii();
							break;
						case 0xA://1010
							i1100_1010_iiii_iiii();
							break;
						case 0xB://1011
							i1100_1011_iiii_iiii();
							break;
						case 0xC://1100
							i1100_1100_iiii_iiii();
							break;
						case 0xD://1101
							i1100_1101_iiii_iiii();
							break;
						case 0xE://1110
							i1100_1110_iiii_iiii();
							break;
						case 0xF://1111
							i1100_1111_iiii_iiii();
							break;
						default:
							iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0xD://finished
						i1101_nnnn_iiii_iiii();
						break;
					case 0xE://finished
						i1110_nnnn_iiii_iiii();
						break;
					case 0xF://finished - fix for fsca
						#region case 0xf
					switch (opcode&0xf)
					{
						case 0x0://0000
							i1111_nnnn_mmmm_0000();
							break;
						case 0x1://0001
							i1111_nnnn_mmmm_0001();
							break;
						case 0x2://0010
							i1111_nnnn_mmmm_0010();
							break;
						case 0x3://0011
							i1111_nnnn_mmmm_0011();
							break;
						case 0x4://0100
							i1111_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i1111_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i1111_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i1111_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i1111_nnnn_mmmm_1000();
							break;
						case 0x9://1001
							i1111_nnnn_mmmm_1001();
							break;
						case 0xA://1010
							i1111_nnnn_mmmm_1010();
                            break;
						case 0xB://1011
							i1111_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i1111_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							#region 0xD multi
						switch ((opcode >>4)&0xf)
						{
							case 0x0://0000
								i1111_nnnn_0000_1101(); 
								break;
							case 0x1://0001
								i1111_nnnn_0001_1101(); 
								break;
							case 0x2://0010
								i1111_nnnn_0010_1101();
								break;
							case 0x3://0011
								i1111_nnnn_0011_1101();
								break;
							case 0x4://0100
								i1111_nnnn_0100_1101(); 
								break;
							case 0x5://0101
								i1111_nnnn_0101_1101(); 
								break;
							case 0x6://0110
								i1111_nnnn_0110_1101(); 
								break;
							case 0x8://1000
								i1111_nnnn_1000_1101(); 
								break;
							case 0x9://1001
								i1111_nnnn_1001_1101(); 
								break;
							case 0xA://1010
								i1111_nnnn_1010_1101(); 
								break;
							case 0xB://1011
								i1111_nnnn_1011_1101(); 
								break;
							case 0xF://1111_xxxx_1111_1101
								#region 0xf multi
								//we have :
								//1111_nnn0_1111_1101
								//1111_nn01_1111_1101
								//1111_1011_1111_1101
								//1111_0011_1111_1101
							switch ((opcode>>8)&0x1)
							{
								case 0x0://1111_nnn0_1111_1101 - fsca DC special
									i1111_nnn0_1111_1101();
									break;
								case 0x1://1111_xxy1_1111_1101
									//if (opcode==0xfffd) {iInvalidOpcode();break;}//1111_x111_1111_1101- invalid
									//1111_nn01_1111_1101
									//1111_1011_1111_1101
									//1111_0011_1111_1101
									if (((opcode>>9)&0x1)==0)//1111_xxy1_1111_1101
									{
										i1111_nn01_1111_1101();
										break;
									}
									else//1111_yy11_1111_1101
									{
										if (((opcode>>10)&0x3)==0)//1111_yy11_1111_1101
										{
											i1111_0011_1111_1101();
											break;
										}
										else if(((opcode>>10)&0x3)==2)//1111_yy11_1111_1101
										{
											i1111_1011_1111_1101();
											break;
										}
									}
									//1111_x111_1111_1101- invalid
									iInvalidOpcode();
									break;
								default:
									iInvalidOpcode();
									break;
							}
								#endregion
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion
							break;
						case 0xE://1110
							i1111_nnnn_mmmm_1110();
							break;
                        default:
                            iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0x10://Custom emulation opcodes ;) "just for the fun of it (tm)"
						switch (opcode & 0xFF)
						{
							case 0x1://rts- driect
								pc_funct = 1;
								delayslot = pr;
								cstRemCall(pr, CallType.Normal);
								break;
						}

						break;
					default:
                        iInvalidOpcode();
                        break;
				}
                #endregion

				//if we want to skip the opcode ... ;)
			SkipOpcode:

				#region Proc PC
				switch(pc_funct)	   
				{
					case 0://inc pc
						pc+=2;
						break;
					case 1://jump
						if (delayslot == 0)
						{
							dc.dbger.mode = 1;
							WriteLine("PCF:" + pc_funct.ToString());
						}
						pc=delayslot;
						pc_funct=0;
						break;
					case 2://jump delay
						if (delayslot == 0)
						{
							dc.dbger.mode = 1;
							WriteLine("PCF:" + pc_funct.ToString());
						}
						pc+=2;
						pc_funct=1;
						break;
				}
                #endregion

                #endregion

                UpdateSystem(tc);
            } while (runsh);

		}
		#endregion

        public static void resetsh4()
		{
			pc = dc_boot_vec;//0x8C008300 to boot to sega logo 0x8C00B800 to boot to the bootstrap1  0xA0000000 to boot bios,0x8C010000 to boot directly to the code
            gl_cop_cnt = 0;
            sr.srregInit ();
			fpscr.init();
			InitMaple();
			initOpenGL();
            //reset regs (MMRs and cpu regs)
            ResetMem();
            //init cycle count table
            initclk();
			cstCallStack.Clear();
            //write(0x8c0107e8, 1, 2);
        }
        static void UpdateSh4(uint cycles) 
        {
            clcount += cycles;//cycle count  #1

			#region TMU
			if ((*TSTR & 8)!=8)
			{
				if ((int)(*TCNT2) < 0) // underflow
				{
					*TCNT2 = *TCOR2;
					*TCR2 |= tmu_underflow;
				}
				else
					(*TCNT2)--;
			}
			if ((*TSTR & 2)!=0)
			{
				if ((int)(*TCNT1) < 0) // underflow
				{
					*TCNT1 = *TCOR1;
					*TCR1 |= tmu_underflow;
				}
				else
					(*TCNT1)--;
			}
			if (((*TSTR) & 1)!=0)
			{
				if ((int)(*TCNT0) < 0) // underflow
				{
					(*TCNT0) = *TCOR0;
					(*TCR0) |= tmu_underflow;
				}
				else
				{
					(*TCNT0)--;
				}
			}
			#endregion
		}

        static void initclk()
        {
            for (int i = 0; i < 65536; i++)
            {
                //this is a realy bad approximation
                ccount[i] = 3;//3 is .. heh a bad gues :)
            }
        }
        static void RBchange()
		{//change register bank..
            //somewhat faster with stackalloc and pointers.. actualy , 6.2x faster..
			uint* r_tmp = stackalloc uint[8];
			sr.cbm = 1 - sr.cbm;
            r_tmp[0]=r[0];r_tmp[4]=r[4];
			r_tmp[1]=r[1];r_tmp[5]=r[5];
			r_tmp[2]=r[2];r_tmp[6]=r[6];
			r_tmp[3]=r[3];r_tmp[7]=r[7];

            //r_bank.CopyTo(r,0);
            r[0] = r_bank[0]; r[4] = r_bank[4];
            r[1] = r_bank[1]; r[5] = r_bank[5];
            r[2] = r_bank[2]; r[6] = r_bank[6];
            r[3] = r_bank[3]; r[7] = r_bank[7];

            
			//r_tmp.CopyTo(r_bank,0);
            r_bank[0] = r_tmp[0]; r_bank[4] = r_tmp[4];
            r_bank[1] = r_tmp[1]; r_bank[5] = r_tmp[5];
            r_bank[2] = r_tmp[2]; r_bank[6] = r_tmp[6];
            r_bank[3] = r_tmp[3]; r_bank[7] = r_tmp[7];

        }

        public static bool UpdateSystem(uint nCycles)
        {
            UpdateSh4(nCycles);
            UpdateMem(nCycles);
            UpdatePvr(nCycles);
            UpdateBios(nCycles);
			dma_check();
            UpdateIntExc(nCycles);
            return false;
        }

        /*static void PRchange()
		{//Precision Mode change
			if (fpscr.PR_==0 )//change to single
			{
				//Todo : PR change
				WriteLine("Warning : SINGLE MODE IS SUPORTED FOR NOW , but not changes to double");
			}
			else//change to double
			{
                WriteLine("Warning : DOUBLE MODE IS NOT SUPORTED FOR NOW");
				//fpscr.PR_ = 0;
            }
		}*/
		static void FRchange()
		{//change FP register bank..
            //somewhat faster with pointers.. actualy , 17.4x faster..
            //fr.CopyTo(fp_tmp, 0);
            float* fp_tmp = fr;
			uint* ft_u_t = fr_uint;
            //xr.CopyTo(fr, 0);
            fr = xr;
			fr_uint = xr_uint;

			xr = fp_tmp;
			xr_uint = ft_u_t;
			//dr = (double*)fr;
            //fp_tmp.CopyTo(xr, 0);
		}

		static void dma_check()
		{
			if ((*DMAOR & 1)!=0)
			{
				if ((*CHCR0 & 1) != 0)
				{
					WriteLine("DMA CHANEL 0");
				}
				if ((*CHCR1 & 1)!=0)
				{
					WriteLine("DMA CHANEL 1");
				}
				if ((*CHCR2 & 1)!=0)
				{
					WriteLine("DMA CHANEL 2");
				}
				if ((*CHCR3 & 1) != 0)
				{
					WriteLine("DMA CHANEL 3");
				}
			}
		}
    }
}
