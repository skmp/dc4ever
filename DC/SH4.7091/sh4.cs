//#define interpreter
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace DC4Ever
{
	/// <summary>
	/// Summary description for 
	/// </summary>
    public partial class emu
    {

		#region sh4 regs and shit declares
        //sr bits
        public const uint sr_T_bit_set = 1;//to set with or
        public const uint sr_T_bit_reset = uint.MaxValue - sr_T_bit_set;//to unset with and
        //sr,fpscr reg emulation class
		public class sr
		{

			public static uint T,S,IMASK,Q,M,FD,BL,RB_,MD;
			public static void srregInit()
			{
				T=0;S=0;IMASK=1;Q=0;M=0;FD=0;BL=1;RB=1;MD=1;                                                                       
			}
			public static uint RB// Register bank change..
			{
				get {return RB_;}
				set {
						if (value!=RB_) RBchange();
						RB_=value;
					}
			}
			public static uint _MD// Mode Change..
			{
				get {return _MD;}
				set {
						if (_MD==0) 
						{
							//if (RB_=1) RBchange();
							//TODO : Check SR.md=0<sr.rb=1>-->?
						} 
						_MD=value;
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
					S=(value>>1)&0x1;
					IMASK=(value>>4)&0xF;
					Q=(value>>8)&0x1;
					M=(value>>9)&0x1;
					FD=(value>>15)&0x1;
					BL=(value>>28)&0x1;
					RB=(value>>29)&0x1;
					MD=(value>>30)&0x1;
				}
			}
		}
		
		public class fpscr
		{
			public static uint RM,finexact,funderflow,foverflow,fdivbyzero,
				 finvalidop,einexact,eunderflow,eoverflow,edivbyzero,
				 einvalidop,cinexact,cunderflow,coverflow,cdivbyzero,
				 cinvalid,cfpuerr,DN,PR_,SZ,FR_;
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

			public static uint PR
			{
				get{return PR_;}
				set{if (value!=PR_){PR_=value; PRchange();}}
			}
			public static uint FR
			{
				get{return FR_;}
				set{if (value!=FR_) FRchange();FR_=value;}
			}

		}
		public static uint gbr,ssr,spc,sgr,dbr,vbr;
		public static uint mach,macl,pr,fpul;
		public static uint pc;
		public static uint[] r = new uint[16];
		public static uint[] r_bank = new uint[8];
		public static float[] fr=new float[16];//fp regs set 1
		public static float[] xr=new float[16];//fp regs set 2
		#endregion

		#region Internal vars ect
		const double pi= 3.1415926535;
		public static uint opcount=0;
		public static uint opc=386950;//perhaps boot time.. fix for video refresh sync
		public static uint opcode;
		public static uint m;
		public static uint n;
		public static int rm;
		public static int rn;
		public static uint disp;
		public static uint tmp1;
		public static uint tmp2;
		public static uint tmp3;
		public static uint tmp4;
		public static bool runsh;
		public static int pc_funct;
		public static uint delayslot;
        public static uint[] ccount = new uint[65536];
		#endregion

		#region Interpreter/Dynarec-Main Loop
		public static unsafe void runcpu()
		{
            #if interpreter
                dc.dcon.WriteLine("Runing in Interpreter Mode");
            #else
                dc.dcon.WriteLine("Runing in Dynarec Mode");
            #endif
            runsh=true;
            uint tc=0;
            do
            {
                #region Interpreter-Dynarec
                #if !interpreter
                
                tc=RecExecuteBlock();
                
                #else
                                opcode=read(pc,2);
                                tc = ccount[opcode];
                                switch (opcode>>12)//proc opcode
				                {
					                case 0x0://finished
						                #region case 0x0
                					
					                switch (opcode&0xf)
					                {
						                case 0x0://0000
							                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							                break;
						                case 0x1://0001
							                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							                break;
						                case 0x2://0010
							                #region case 0x2 multi opcodes
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0000
								                //i0000_nnnn_0000_0010();
								                n = (opcode >> 8) & 0x0F;
								                r[n] = sr.reg;
								                break;
							                case 0x1://0001
								                //i0000_nnnn_0001_0010();
								                break;
							                case 0x2://0010
								                //i0000_nnnn_0010_0010();
								                break;
							                case 0x3://0011
								                //i0000_nnnn_0011_0010();
								                break;
							                case 0x4://0100
								                //i0000_nnnn_0100_0010();
								                break;
							                case 0x8://1000
								                //i0000_nnnn_1000_0010();
								                break;
							                case 0x9://1001
								                //i0000_nnnn_1001_0010();
								                break;
							                case 0xA://1010
								                //i0000_nnnn_1010_0010();
								                break;
							                case 0xB://1011
								                //i0000_nnnn_1011_0010();
								                break;
							                case 0xC://1100
								                //i0000_nnnn_1100_0010();
								                break;
							                case 0xD://1101
								                //i0000_nnnn_1101_0010();
								                break;
							                case 0xE://1110
								                //i0000_nnnn_1110_0010();
								                break;
							                case 0xF://1111
								                //i0000_nnnn_1111_0010();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;	
						                }
							                #endregion
							                break;
						                case 0x3://0011
							                #region case 0x3 multi opcodes
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0000
								                //i0000_nnnn_0000_0011();
								                break;
							                case 0x2://0010
								                //i0000_nnnn_0010_0011();
								                n = (opcode >> 8) & 0x0F;
								                delayslot = r[n] + pc + 4;
								                pc_funct =2;//delay 2
								                break;
							                case 0x8://1000
								                //i0000_nnnn_1000_0011();
								                break;
							                case 0x9://1001
								                //i0000_nnnn_1001_0011();
								                break;
							                case 0xA://1010
								                //i0000_nnnn_1010_0011();
								                break;
							                case 0xB://1011
								                //i0000_nnnn_1011_0011();
								                break;
							                case 0xC://1100
								                //i0000_nnnn_1100_0011();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion
							                break;
						                case 0x4://0100
							                //i0000_nnnn_mmmm_0100();
							                break;
						                case 0x5://0101
							                //i0000_nnnn_mmmm_0101();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                write(r[0]+r[n],r[m]& 0xFFFF,2);
							                break;
						                case 0x6://0110
							                //i0000_nnnn_mmmm_0110();
							                break;
						                case 0x7://0111
							                //i0000_nnnn_mmmm_0111();
							                n = (opcode>> 8) & 0x0F;
							                m = (opcode>> 4) & 0x0F;
							                macl= (uint) (((int)r[n] * (int) r[m]) & 0xFFFFFFFF);
							                break;
						                case 0x8://1000
							                #region case 0x8 multi opcodes
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0000
								                ////i0000_0000_0000_1000();
								                break;
							                case 0x1://0001
								                ////i0000_0000_0001_1000();
								                break;
							                case 0x2://0010
								                ////i0000_0000_0010_1000();
								                break;
							                case 0x3://0011
								                //i0000_0000_0011_1000();
								                break;
							                case 0x4://0100
								                //i0000_0000_0100_1000();
								                break;
							                case 0x5://0101
								                //i0000_0000_0101_1000();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion
							                break;
						                case 0x9://1001
							                #region case 0x9 multi opcodes
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0000
								                //i0000_0000_0000_1001();
								                break;
							                case 0x1://0001
								                //i0000_0000_0001_1001();
								                break;
							                case 0x2://0010
								                //i0000_nnnn_0010_1001();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion
							                break;
						                case 0xA://1010
							                #region case 0xA multi opcodes
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0000
								                //i0000_nnnn_0000_1010();
								                break;
							                case 0x1://0001
								                //i0000_nnnn_0001_1010();
								                n=  (opcode >> 8) & 0x0F;
								                r[n]=macl; 
								                break;
							                case 0x2://0010
								                //i0000_nnnn_0010_1010();
								                break;
							                case 0x5://0101
								                //i0000_nnnn_0101_1010();
								                n = (opcode >> 8) & 0x0F;
								                r[n] = (uint)(int)fpul;
								                break;
							                case 0x6://0110
								                //i0000_nnnn_0110_1010();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion
							                break;
						                case 0xB://1011
							                #region case 0xB multi opcodes
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0000
								                //i0000_0000_0000_1011();
								                delayslot  = pr;
								                pc_funct = 2;
								                break;
							                case 0x1://0001
								                //i0000_0000_0001_1011();
								                break;
							                case 0x2://0010
								                //i0000_0000_0010_1011();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion
							                break;
						                case 0xC://1100
							                //i0000_nnnn_mmmm_1100();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                r[n] = (uint)(sbyte)read(r[0]+r[m],1);
							                break;
						                case 0xD://1101
							                //i0000_nnnn_mmmm_1101();
							                break;
						                case 0xE://1110
							                //i0000_nnnn_mmmm_1110();
							                break;
						                case 0xF://1111
							                //i0000_nnnn_mmmm_1111();
							                break;
					                }
						                #endregion
						                break;
					                case 0x1://finished
						                //i0001_nnnn_mmmm_iiii();
						                n = (opcode >> 8) & 0x0F;
						                m = (opcode >> 4) & 0x0F;
						                disp = opcode & 0x0F;
						                write(r[n] + disp * 4,r[m],4);
						                break;
					                case 0x2://finished
						                #region case 0x2
					                switch (opcode&0xf)
					                {
						                case 0x0://0000
							                //i0010_nnnn_mmmm_0000();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                write(r[n],r[m] & 0xFF,1);
							                break;
						                case 0x1://0001
							                //i0010_nnnn_mmmm_0001();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                write(r[n],r[m] & 0xFFFF,2);
							                break;
						                case 0x2://0010
							                //i0010_nnnn_mmmm_0010();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                write(r[n],r[m],4);//at r[n],r[m]
							                break;
						                case 0x4://0100
							                //i0010_nnnn_mmmm_0100();
							                break;
						                case 0x5://0101
							                //i0010_nnnn_mmmm_0101();
							                break;
						                case 0x6://0110
							                //i0010_nnnn_mmmm_0110();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                r[n] -= 4;
							                write (r[n],r[m],4);
							                break;
						                case 0x7://0111
							                //i0010_nnnn_mmmm_0111();
							                break;
						                case 0x8://1000
							                //i0010_nnnn_mmmm_1000();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;

							                if ((r[n] & r[m])>0)
								                sr.T=0;
							                else
								                sr.T=1;

							                break;
						                case 0x9://1001
							                //i0010_nnnn_mmmm_1001();
							                n = (opcode>> 8) & 0x0F;
							                m = (opcode>> 4) & 0x0F;
							                r[n] &= r[m];
							                break;
						                case 0xA://1010
							                //i0010_nnnn_mmmm_1010();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                r[n] ^= r[m];
							                break;
						                case 0xB://1011
							                //i0010_nnnn_mmmm_1011();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;	
							                r[n] |= r[m];
							                break;
						                case 0xC://1100
							                //i0010_nnnn_mmmm_1100();
							                break;
						                case 0xD://1101
							                //i0010_nnnn_mmmm_1101();
							                break;
						                case 0xE://1110
							                //i0010_nnnn_mmmm_1110();
							                break;
						                case 0xF://1111
							                //i0010_nnnn_mmmm_1111();
							                break;
						                default:
							                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							                break;
					                }
					                #endregion
						                break;
					                case 0x3://finished
						                #region case 0x3
					                switch (opcode&0xf)
					                {
						                case 0x0://0000
							                //i0011_nnnn_mmmm_0000();
							                break;
						                case 0x2://0010
							                //i0011_nnnn_mmmm_0010();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                if (r[n] >= r[m])
								                sr.T=1;
							                else
								                sr.T=0;
							                break;
						                case 0x3://0011
							                //i0011_nnnn_mmmm_0011();
							                break;
						                case 0x4://0100
							                //i0011_nnnn_mmmm_0100();
							                break;
						                case 0x5://0101
							                //i0011_nnnn_mmmm_0101();
							                break;
						                case 0x6://0110
							                //i0011_nnnn_mmmm_0110();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;

							                if (r[n] > r[m])
								                sr.T=1;
							                else
								                sr.T=0;
							                break;
						                case 0x7://0111
							                //i0011_nnnn_mmmm_0111();
							                break;
						                case 0x8://1000
							                //i0011_nnnn_mmmm_1000();
							                n = (opcode>> 8) & 0x0F;
							                m = (opcode>> 4) & 0x0F;
							                rn=(int)r[n];
							                rm=(int)r[m];
							                rn -= rm;
                	
							                r[n]=(uint)rn;
							                break;
						                case 0xA://1010
							                //i0011_nnnn_mmmm_1010();
							                break;
						                case 0xB://1011
							                //i0011_nnnn_mmmm_1011();
							                break;
						                case 0xC://1100
							                //i0011_nnnn_mmmm_1100();
							                n = (opcode>> 8) & 0x0F;
							                m = (opcode>> 4) & 0x0F;
							                rm=(int)r[m];
							                rn=(int)r[n];	
							                rn += rm;
							                r[n]=(uint)rn;
							                break;
						                case 0xD://1101
							                //i0011_nnnn_mmmm_1101();
							                break;
						                case 0xE://1110
							                //i0011_nnnn_mmmm_1110();
							                break;
						                case 0xF://1111
							                //i0011_nnnn_mmmm_1111();
							                break;
						                default:
							                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
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
								                //i0100_nnnn_0000_0000();
								                break;
							                case 0x1://0100_xxxx_0001_0000
								                //i0100_nnnn_0001_0000();
								                n = (opcode >> 8) & 0x0F;
								                rn=(int)(r[n]);
								                --rn;
								                if (rn==0)
									                sr.T=1;
								                else
									                sr.T=0;
								                r[n]=(uint)rn;
								                break;
							                case 0x2://0100_xxxx_0010_0000
								                //i0100_nnnn_0010_0000();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0x1://0001
							                #region 0x1 multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_0001
								                //i0100_nnnn_0000_0001();
								                n = (opcode>> 8) & 0x0F;
								                sr.T = r[n] & 0x1;
								                r[n] >>= 1;
								                break;
							                case 0x1://0100_xxxx_0001_0001
								                //i0100_nnnn_0001_0001();
								                break;
							                case 0x2://0100_xxxx_0010_0001
								                //i0100_nnnn_0010_0001();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0x2://0010
							                #region 0x2 multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_0010
								                //i0100_nnnn_0000_0010();
								                break;
							                case 0x1://0100_xxxx_0001_0010
								                //i0100_nnnn_0001_0010();
								                break;
							                case 0x2://0100_xxxx_0010_0010
								                //i0100_nnnn_0010_0010();
								                n = (opcode >> 8) & 0x0F;
								                r[n] -= 4;
								                write(r[n],pr,4);
								                break;
							                case 0x5://0100_xxxx_0101_0010
								                //i0100_nnnn_0101_0010();
								                break;
							                case 0x6://0100_xxxx_0110_0010
								                //i0100_nnnn_0110_0010();
								                n = (opcode >> 8) & 0x0F;
								                r[n] -= 4;
								                write(r[n],fpscr.reg,4);
								                break;
							                case 0x8://0100_xxxx_1000_0010
								                //i0100_nnnn_1000_0010();
								                break;
							                case 0x9://0100_xxxx_1001_0010
								                //i0100_nnnn_1001_0010();
								                break;
							                case 0xA://0100_xxxx_1010_0010
								                //i0100_nnnn_1010_0010();
								                break;
							                case 0xB://0100_xxxx_1011_0010
								                //i0100_nnnn_1011_0010();
								                break;
							                case 0xC://0100_xxxx_1100_0010
								                //i0100_nnnn_1100_0010();
								                break;
							                case 0xD://0100_xxxx_1101_0010
								                //i0100_nnnn_1101_0010();
								                break;
							                case 0xE://0100_xxxx_1110_0010
								                //i0100_nnnn_1110_0010();
								                break;
							                case 0xF://0100_xxxx_1111_0010
								                //i0100_nnnn_1111_0010();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0x3://0011
							                #region 0x3 multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_0011
								                //i0100_nnnn_0000_0011();
								                break;
							                case 0x1://0100_xxxx_0001_0011
								                //i0100_nnnn_0001_0011();
								                break;
							                case 0x2://0100_xxxx_0010_0011
								                //i0100_nnnn_0010_0011();
								                break;
							                case 0x3://0100_xxxx_0011_0011
								                //i0100_nnnn_0011_0011();
								                break;
							                case 0x4://0100_xxxx_0100_0011
								                //i0100_nnnn_0100_0011();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0x4://0100
							                #region 0x4 multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_0100
								                //i0100_nnnn_0000_0100();
								                break;
							                case 0x2://0100_xxxx_0010_0100
								                //i0100_nnnn_0010_0100();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0x5://0101
							                #region 0x5 multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_0101
								                //i0100_nnnn_0000_0101();
								                break;
							                case 0x1://0100_xxxx_0001_0101
								                //i0100_nnnn_0001_0101();
								                break;
							                case 0x2://0100_xxxx_0010_0101
								                //i0100_nnnn_0010_0101();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0x6://0110
							                #region 0x6 multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_0110
								                //i0100_nnnn_0000_0110();
								                break;
							                case 0x1://0100_xxxx_0001_0110
								                //i0100_nnnn_0001_0110();
								                break;
							                case 0x2://0100_xxxx_0010_0110
								                //i0100_nnnn_0010_0110();
								                m = (opcode >> 8) & 0x0F;
								                pr = read (r[m],4);
								                r[m] += 4;
								                break;
							                case 0x5://0100_xxxx_0101_0110
								                //i0100_nnnn_0101_0110();
								                break;
							                case 0x6://0100_xxxx_0110_0110
								                //i0100_nnnn_0110_0110();
								                m = (opcode >> 8) & 0x0F;
								                fpscr.reg =read (r[m],4);
								                r[m] += 4;
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0x7://0111
							                #region 0x7 multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_0111
								                //i0100_nnnn_0000_0111();
								                break;
							                case 0x1://0100_xxxx_0001_0111
								                //i0100_nnnn_0001_0111();
								                break;
							                case 0x2://0100_xxxx_0010_0111
								                //i0100_nnnn_0010_0111();
								                break;
							                case 0x3://0100_xxxx_0011_0111
								                //i0100_nnnn_0011_0111();
								                break;
							                case 0x4://0100_xxxx_0100_0111
								                //i0100_nnnn_0100_0111();
								                break;
							                case 0x8://0100_xxxx_1000_0111
								                //i0100_nnnn_1000_0111();
								                break;
							                case 0x9://0100_xxxx_1001_0111
								                //i0100_nnnn_1001_0111();
								                break;
							                case 0xA://0100_xxxx_1010_0111
								                //i0100_nnnn_1010_0111();
								                break;
							                case 0xB://0100_xxxx_1011_0111
								                //i0100_nnnn_1011_0111();
								                break;
							                case 0xC://0100_xxxx_1100_0111
								                //i0100_nnnn_1100_0111();
								                break;
							                case 0xD://0100_xxxx_1101_0111
								                //i0100_nnnn_1101_0111();
								                break;
							                case 0xE://0100_xxxx_1110_0111
								                //i0100_nnnn_1110_0111();
								                break;
							                case 0xF://0100_xxxx_1111_0111
								                //i0100_nnnn_1111_0111();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0x8://1000
							                #region 0x8 multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_1000
								                //i0100_nnnn_0000_1000();
								                n = (opcode  >> 8) & 0x0F;
								                r[n] <<= 2;
								                break;
							                case 0x1://0100_xxxx_0001_1000
								                //i0100_nnnn_0001_1000();
								                n = (opcode >> 8) & 0x0F;
								                r[n] <<= 8;
								                break;
							                case 0x2://0100_xxxx_0010_1000
								                //i0100_nnnn_0010_1000();
								                n = (opcode >> 8) & 0x0F;
								                r[n] <<= 16;
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0x9://1001
							                #region 0x9 multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_1001
								                //i0100_nnnn_0000_1001();
								                n = (opcode >> 8) & 0x0F;
								                r[n] >>= 2;
								                break;
							                case 0x1://0100_xxxx_0001_1001
								                //i0100_nnnn_0001_1001();
								                break;
							                case 0x2://0100_xxxx_0010_1001
								                //i0100_nnnn_0010_1001();
								                n = (opcode >> 8) & 0x0F;
								                r[n] >>= 16;
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;

						                case 0xA://1010
							                #region 0x9 multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_1010
								                //i0100_nnnn_0000_1010();
								                break;
							                case 0x1://0100_xxxx_0001_1010
								                //i0100_nnnn_0001_1010();
								                break;
							                case 0x2://0100_xxxx_0010_1010
								                //i0100_nnnn_0010_1010();
								                m = (opcode >> 8) & 0x0F;
								                pr = r[m];
								                break;
							                case 0x5://0100_xxxx_0101_1010
								                //i0100_nnnn_0101_1010();
								                m = (opcode >> 8) & 0x0F;
								                fpul =r[m];
								                break;
							                case 0x6://0100_xxxx_0110_1010
								                //i0100_nnnn_0110_1010();
								                m = (opcode>> 8) & 0x0F;
								                fpscr.reg = r[m];
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;

						                case 0xB://1011
							                #region 0xB multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_1011
								                //i0100_nnnn_0000_1011();
								                n = (opcode >> 8) & 0x0F;
								                pr = pc + 4;
								                delayslot= r[n];
								                pc_funct = 2;//jump with delay
								                break;
							                case 0x1://0100_xxxx_0001_1011
								                //i0100_nnnn_0001_1011();
								                break;
							                case 0x2://0100_xxxx_0010_1011
								                //i0100_nnnn_0010_1011();
								                n = (opcode >> 8) & 0x0F;
								                delayslot = r[n];
								                pc_funct = 2;//jump with delay 1
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0xC://1100
							                //i0100_nnnn_mmmm_1100();
							                break;
						                case 0xD://1101
							                //i0100_nnnn_mmmm_1101();
							                break;
						                case 0xE://1110
							                #region 0xE multi
						                switch ((opcode>>4)&0xf)
						                {
							                case 0x0://0100_xxxx_0000_1110
								                //i0100_nnnn_0000_1110();
								                break;
							                case 0x1://0100_xxxx_0001_1110
								                //i0100_nnnn_0001_1110();
								                break;
							                case 0x2://0100_xxxx_0010_1110
								                //i0100_nnnn_0010_1110();
								                break;
							                case 0x3://0100_xxxx_0011_1110
								                //i0100_nnnn_0011_1110();
								                break;
							                case 0x4://0100_xxxx_0100_1110
								                //i0100_nnnn_0100_1110();
								                break;
							                case 0x8://0100_xxxx_1000_1110
								                //i0100_nnnn_1000_1110();
								                break;
							                case 0x9://0100_xxxx_1001_1110
								                //i0100_nnnn_1001_1110();
								                break;
							                case 0xA://0100_xxxx_1010_1110
								                //i0100_nnnn_1010_1110();
								                break;
							                case 0xB://0100_xxxx_1011_1110
								                //i0100_nnnn_1011_1110();
								                break;
							                case 0xC://0100_xxxx_1100_1110
								                //i0100_nnnn_1100_1110();
								                break;
							                case 0xD://0100_xxxx_1101_1110
								                //i0100_nnnn_1101_1110();
								                break;
							                case 0xE://0100_xxxx_1110_1110
								                //i0100_nnnn_1110_1110();
								                break;
							                case 0xF://0100_xxxx_1111_1110
								                //i0100_nnnn_1111_1110();
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion 
							                break;
						                case 0xF://1111
							                //i0100_nnnn_mmmm_1111();
							                break;
						                default:
							                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							                break;
					                }
						                #endregion
						                break;
					                case 0x5://finished
						                //i0101_nnnn_mmmm_iiii();
						                n = (opcode >> 8) & 0x0F;
						                m = (opcode >> 4) & 0x0F;
						                disp = (opcode & 0x0F)*4;
						                r[n]=read(r[m]+disp,4);
						                break;
					                case 0x6://finished
						                #region case 0x6
					                switch (opcode&0xf)
					                {
						                case 0x0://0000
							                //i0110_nnnn_mmmm_0000();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                r[n] = (uint)(sbyte) read(r[m], 1);
							                break;
						                case 0x1://0001
							                //i0110_nnnn_mmmm_0001();
							                break;
						                case 0x2://0010
							                //i0110_nnnn_mmmm_0010();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                r[n]=read (r[m],4);
							                break;
						                case 0x3://0011
							                //i0110_nnnn_mmmm_0011();
							                n = (opcode>> 8) & 0x0F;
							                m = (opcode>> 4) & 0x0F;
							                r[n] = r[m];
							                break;
						                case 0x4://0100
							                //i0110_nnnn_mmmm_0100();
							                break;
						                case 0x5://0101
							                //i0110_nnnn_mmmm_0101();
							                break;
						                case 0x6://0110
							                //i0110_nnnn_mmmm_0110();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;

							                r[n]=read(r[m],4);
							                if (n != m)
								                r[m] += 4;
							                break;
						                case 0x7://0111
							                //i0110_nnnn_mmmm_0111();
							                break;
						                case 0x8://1000
							                //i0110_nnnn_mmmm_1000();
							                break;
						                case 0x9://1001
							                //i0110_nnnn_mmmm_1001();
							                n = (opcode>> 8) & 0x0F;
							                m = (opcode>> 4) & 0x0F;
							                r[n] = ((r[m] >> 16) & 0xFFFF) | ((r[m] << 16) & 0xFFFF0000);
							                break;
						                case 0xA://1010
							                //i0110_nnnn_mmmm_1010();
							                break;
						                case 0xB://1011
							                //i0110_nnnn_mmmm_1011();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                rm=(int) r[m];
							                //rn=(int) r[n];
							                r[n] =(uint) -rm;
							                break;
						                case 0xC://1100
							                //i0110_nnnn_mmmm_1100();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                r[n] = r[m] & 0xFF;
							                break;
						                case 0xD://1101
							                //i0110_nnnn_mmmm_1101();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode  >> 4) & 0x0F;
							                r[n] = r[m] & 0x0000FFFF;
							                break;
						                case 0xE://1110
							                //i0110_nnnn_mmmm_1110();
							                break;
						                case 0xF://1111
							                //i0110_nnnn_mmmm_1111();
							                break;
						                default:
							                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							                break;
					                }
						                #endregion
						                break;
					                case 0x7://finished
						                //i0111_nnnn_iiii_iiii();
						                n = (opcode>> 8) & 0x0F;
						                rm = (int)(sbyte)(opcode & 0xFF);
						                rn=((int)r[n])+rm;
						                r[n]=(uint)rn;
						                break;
					                case 0x8://finished
						                #region case 0x8
					                switch ((opcode>>8)&0xf)
					                {
						                case 0x0://0000
							                //i1000_0000_mmmm_iiii();
							                break;
						                case 0x1://0001
							                //i1000_0001_mmmm_iiii();
							                break;
						                case 0x4://0100
							                //i1000_0100_mmmm_iiii();
							                break;
						                case 0x5://0101
							                //i1000_0101_mmmm_iiii();
							                break;
						                case 0x8://1000
							                //i1000_1000_iiii_iiii();
							                m = (uint)(sbyte)(opcode & 0xFF);
							                if (r[0] == m)
								                sr.T =1;
							                else
								                sr.T =0;
							                break;
						                case 0x9://1001
							                //i1000_1001_iiii_iiii();
							                if (sr.T==1)
							                {
								                delayslot = (uint)((sbyte)(opcode & 0xFF))*2 + pc + 4;
								                pc_funct = 1;//direct jump
							                }
							                break;
						                case 0xB://1011
							                //i1000_1011_iiii_iiii();
							                if (sr.T==0)
							                {
								                delayslot  = (uint)((sbyte)(opcode  & 0xFF))*2 + 4 + pc ;
								                pc_funct = 1;//jump , no delay
							                }
							                break;
						                case 0xD://1101
							                //i1000_1101_iiii_iiii();
							                delayslot =(uint) ((sbyte)(opcode & 0xFF))*2 + pc + 4; // antes era disp = ...
							                pc_funct = 2;
							                break;
						                case 0xF://1111
							                //i1000_1111_iiii_iiii();
							                if (sr.T==0)
							                {
								                delayslot = (uint)((sbyte) (opcode & 0xFF))*2 + pc + 4;
								                pc_funct =2;//delay 1 instruction
							                }
							                break;
						                default:
							                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							                break;
					                }
						                #endregion
						                break;
					                case 0x9://finished
						                //i1001_nnnn_iiii_iiii();
						                n = (opcode >> 8) & 0x0F;
						                disp = (opcode & 0x00FF);
						                r[n]=(uint)(short)read(disp*2 + pc + 4,2);
						                break;
					                case 0xA://finished
						                //i1010_iiii_iiii_iiii();
						                delayslot = (uint)(((short)((opcode & 0x0FFF)<<4) )>>3)  + pc + 4;//(short<<4,>>4(-1*2))
						                pc_funct =2;//jump delay 1
						                break;
					                case 0xB://finished
						                //i1011_iiii_iiii_iiii();
						                break;
					                case 0xC://finished
						                #region case 0xC
					                switch ((opcode>>8)&0xf)
					                {
						                case 0x0://0000
							                //i1100_0000_iiii_iiii();
							                break;
						                case 0x1://0001
							                //i1100_0001_iiii_iiii();
							                break;
						                case 0x2://0010
							                //i1100_0010_iiii_iiii();
							                break;
						                case 0x3://0011
							                //i1100_0011_iiii_iiii();
							                break;
						                case 0x4://0100
							                //i1100_0100_iiii_iiii();
							                break;
						                case 0x5://0101
							                //i1100_0101_iiii_iiii();
							                break;
						                case 0x6://0110
							                //i1100_0110_iiii_iiii();
							                break;
						                case 0x7://0111
							                //i1100_0111_iiii_iiii();
							                disp = (opcode & 0x00FF)*4 +  ((pc + 4) & 0xFFFFFFFC);
							                r[0] = disp;
							                break;
						                case 0x8://1000
							                //i1100_1000_iiii_iiii();
							                break;
						                case 0x9://1001
							                //i1100_1001_iiii_iiii();
							                break;
						                case 0xA://1010
							                //i1100_1010_iiii_iiii();
							                break;
						                case 0xB://1011
							                //i1100_1011_iiii_iiii();
							                break;
						                case 0xC://1100
							                //i1100_1100_iiii_iiii();
							                break;
						                case 0xD://1101
							                //i1100_1101_iiii_iiii();
							                break;
						                case 0xE://1110
							                //i1100_1110_iiii_iiii();
							                break;
						                case 0xF://1111
							                //i1100_1111_iiii_iiii();
							                break;
						                default:
							                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							                break;
					                }
						                #endregion
						                break;
					                case 0xD://finished
						                //i1101_nnnn_iiii_iiii();
						                n = (opcode >> 8) & 0x0F;
						                disp = (opcode & 0xFF)<<2;
						                r[n]=read ( disp + (pc & 0xFFFFFFFC) + 4,4);
						                break;
					                case 0xE://finished
						                //i1110_nnnn_iiii_iiii();
						                n = (opcode >> 8) & 0x0F;
						                r[n] = (uint)(sbyte)(opcode & 0xFF);//(uint)(sbyte)= signextend8 :)
						                break;
					                case 0xF://finished - fix for fsca
						                #region case 0xf
					                switch (opcode&0xf)
					                {
						                case 0x0://0000
							                //i1111_nnnn_mmmm_0000();
							                n = (opcode>> 8) & 0x0F;
							                m = (opcode>> 4) & 0x0F;
							                fr[n] += fr[m];
							                break;
						                case 0x1://0001
							                //i1111_nnnn_mmmm_0001();
							                break;
						                case 0x2://0010
							                //i1111_nnnn_mmmm_0010();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                fr[n] *= fr[m];
							                break;
						                case 0x3://0011
							                //i1111_nnnn_mmmm_0011();
							                n = (opcode>> 8) & 0x0F;
							                m = (opcode>> 4) & 0x0F;
							                fr[n] /= fr[m];
							                break;
						                case 0x4://0100
							                //i1111_nnnn_mmmm_0100();
							                break;
						                case 0x5://0101
							                //i1111_nnnn_mmmm_0101();
							                break;
						                case 0x6://0110
							                //i1111_nnnn_mmmm_0110();
							                break;
						                case 0x7://0111
							                //i1111_nnnn_mmmm_0111();
							                break;
						                case 0x8://1000
							                //i1111_nnnn_mmmm_1000();
						                n = (opcode>> 8) & 0x0F;
						                m = (opcode>> 4) & 0x0F;
						                uint tmp=read(r[m],4);
						                fr[n]=*(float*)&tmp;
							                break;
						                case 0x9://1001
							                //i1111_nnnn_mmmm_1001();
							                break;
						                case 0xA://1010
							                //i1111_nnnn_mmmm_1010();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                fixed (float*p=&fr[m]){write(r[n],*(uint*)p,4);}
							                break;
						                case 0xB://1011
							                //i1111_nnnn_mmmm_1011();
							                break;
						                case 0xC://1100
							                //i1111_nnnn_mmmm_1100();
							                n = (opcode >> 8) & 0x0F;
							                m = (opcode >> 4) & 0x0F;
							                fr[n] = fr[m];
							                break;
						                case 0xD://1101
							                #region 0xD multi
						                switch ((opcode >>4)&0xf)
						                {
							                case 0x0://0000
								                //i1111_nnnn_0000_1101(); 
								                break;
							                case 0x1://0001
								                //i1111_nnnn_0001_1101(); 
								                break;
							                case 0x2://0010
								                //i1111_nnnn_0010_1101(); 
								                n = (opcode >> 8) & 0x0F;
								                fr[n] = (float) (int)fpul; 
								                break;
							                case 0x3://0011
								                //i1111_nnnn_0011_1101(); 
								                m = (opcode >> 8) & 0x0F;
								                fpul =  (uint)(int)fr[m];
								                break;
							                case 0x4://0100
								                //i1111_nnnn_0100_1101(); 
								                break;
							                case 0x5://0101
								                //i1111_nnnn_0101_1101(); 
								                break;
							                case 0x6://0110
								                //i1111_nnnn_0110_1101(); 
								                break;
							                case 0x8://1000
								                //i1111_nnnn_1000_1101(); 
								                break;
							                case 0x9://1001
								                //i1111_nnnn_1001_1101(); 
								                break;
							                case 0xA://1010
								                //i1111_nnnn_1010_1101(); 
								                break;
							                case 0xB://1011
								                //i1111_nnnn_1011_1101(); 
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
									                //i1111_nnn0_1111_1101();
									                n = (opcode>>9) & 0x07;
									                float x = (float) (2 * pi * (float) fpul / 65536.0);
									                fr[n*2] = (float)System.Math.Sin(x);
									                fr[n*2+1] =(float) System.Math.Cos(x);
									                break;
								                case 0x1://1111_xxy1_1111_1101
									                //if (opcode==0xfffd) {dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();break;}//1111_x111_1111_1101- invalid
									                //1111_nn01_1111_1101
									                //1111_1011_1111_1101
									                //1111_0011_1111_1101
									                if (((opcode>>9)&0x1)==0)//1111_xxy1_1111_1101
									                {
										                break;//i1111_nn01_1111_1101();
									                }
									                else//1111_yy11_1111_1101
									                {
										                if (((opcode>>10)&0x3)==0)//1111_yy11_1111_1101
										                {
											                break;//i1111_0011_1111_1101();
										                }
										                else if(((opcode>>10)&0x3)==2)//1111_yy11_1111_1101
										                {
											                break;//i1111_1011_1111_1101();
										                }
									                }
									                //1111_x111_1111_1101- invalid
									                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
									                break;
								                default:
									                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
									                break;
							                }
								                #endregion
								                break;
							                default:
								                dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								                break;
						                }
							                #endregion
							                break;
						                case 0xE://1110
							                //i1111_nnnn_mmmm_1110();
							                break;
						                default:
							                //dc.dcon.WriteLine("Warning:Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());
							                //System.Windows.Forms.Application.DoEvents();
							                break;
					                }
						                #endregion
						                break;
					                default:
						                //handle any custom opcodes (>65535)
						                //bios hle ect
						                break;
				                }
				                #region Proc PC
				                switch(pc_funct)	   
				                {
					                case 0://inc pc
						                pc+=2;
						                break;
					                case 1://jump
						                pc=delayslot;
						                pc_funct=0;
						                break;
					                case 2://jump delay
						                pc+=2;
						                pc_funct=1;
						                break;
				                }
                                #endregion
                #endif
                #endregion
                opcount += tc;//opcode count - inacurate on recompiler
                opc += tc;   //cycle count - if 1 opcode takes 1 cycle to execute then this is corect :P
                if (opc > (3495253))//60 ~herz = 200 mhz / 60=3495253 cycles per screen refresh
                {present();System.Windows.Forms.Application.DoEvents();opc=0;}
			} while (runsh);

		}
		#endregion


		public static void resetsh4()
		{//boot directly to the bootfile(no boot strap :P)
			pc=0x8C010000;//0x8C008300 to boot to sega logo 0x8C00B800 to boot to the bootstrap1  A0000000 to boot bios,0x8C0010000 to boot directly to the code
			sr.srregInit ();
			fpscr.init();
			//reset regs (MMRs and cpu regs)
            //init cycle count table
            initclk();
        }

        public static void initclk()
        {
            for (int i = 0; i < 65536; i++)
            {
                //this is a realy bad approximation
                ccount[i] = 3;//3 is the average case for the best conditions [no cache miss ect]
            }
        }
        public static void RBchange()
		{//change register bank..
			uint[] r_tmp = new uint[8];
			r_tmp[0]=r[0];r_tmp[4]=r[4];
			r_tmp[1]=r[1];r_tmp[5]=r[5];
			r_tmp[2]=r[2];r_tmp[6]=r[6];
			r_tmp[3]=r[3];r_tmp[7]=r[7];
			r_bank.CopyTo(r,0);
			r_tmp.CopyTo(r_bank,0);
		}

		public static void PRchange()
		{//Precision Mode change
			if (fpscr.FR_==0 )//change to single
			{
				//Todo : PR change
			}
			else//change to double
			{
				//Todo : PR change
			}
		}
		public static void FRchange()
		{//change FP register bank..
			float[] fp_tmp = new float[16];
			fr.CopyTo(fp_tmp,0);
			xr.CopyTo(fr,0);
			fp_tmp.CopyTo(xr,0);
		}

        private static void GetCodeBuffer()
        {
            throw new NotImplementedException();
        }
    }
}
