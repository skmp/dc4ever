//This is a part of the DC4Ever emulator
//You are not allowed to release modified(or unmodified) versions
//without asking me (drk||Raziel).
//For Suggestions ect please e-mail at : stef_mp@yahoo.gr
//Note : This is just to prove that Fast emulation is possible with 
//	C# /.net ...And yes , this code could be writen at VB.net and 
//	run more or less at the same speed on dynarec mode .. see , i like to use pointers all around ..even if not neede xD
//  just to get 1 0.00001% performace increase .. (Well , actualy with good use i get much more .. but on dynarec this does not count..)
//	This code requires C#.net 2.0 (Get the C# epxress 2005 Beta from microsoft)
//

using System;

namespace DC4Ever
{
	/// <summary>
	/// Manages the mem Reads/Writes and the 
	/// </summary>
    public unsafe partial class emu
    {
        public const int kb = 1024;
        public const int mb = kb * 1024;
        //public static byte[] ram = new byte[16* dc.mb];//16 megs ram
		static byte* ram_b = (byte*)dc.mmgr.AllocMem(16 * mb);//= new byte[16* dc.mb];//16 megs ram
        static ushort* ram_w =(ushort*)ram_b;//in words
        static uint* ram_dw = (uint*)ram_b;//in dwords
		static uint* regmem_dw = (uint*)dc.mmgr.AllocMem(16 * mb);
        static ushort* regmem_w = (ushort*)regmem_dw;
        static byte* regmem_b = (byte*)regmem_dw;

		static uint* biosmem_bw = (uint*)dc.mmgr.AllocMem(16 * mb);
        static ushort* biosmem_w = (ushort*)biosmem_bw;
        static byte* biosmem_b = (byte*)biosmem_bw;

		static uint* sq0_dw = (uint*)dc.mmgr.AllocMem(8 * 4);

		static uint* sq1_dw = (uint*)dc.mmgr.AllocMem(8 * 4);


        #region mmrs- ptr's

        #region memregs-1
        static uint* PTEL;
        static uint* CCR;
        static uint* EXPEVT;
        static uint* INTEVT;
        static uint* TRA;
        static uint* QACR0;
        static uint* QACR1;

        static uint* PCTRA;
        static ushort* PDTRA;
        static uint* PCTRB;
        static ushort* PDTRB;

        #region DMA
        static uint* SAR0;
        static uint* DAR0;
        static uint* DMATCR0;
        static uint* CHCR0;

        static uint* SAR1;
        static uint* DAR1;
        static uint* DMATCR1;
        static uint* CHCR1;

        static uint* SAR2;
        static uint* DAR2;
        static uint* DMATCR2;
        static uint* CHCR2;

        static uint* SAR3;
        static uint* DAR3;
        static uint* DMATCR3;
        static uint* CHCR3;

        static uint* DMAOR;
        #endregion

        static ushort* ICR;
        static ushort* IPRA;
        static ushort* IPRB;
        static ushort* IPRC;

        #region TMU Registers
        static byte* TOCR;
        static byte* TSTR;
        static uint* TCOR0;
        static uint* TCNT0;
        static ushort* TCR0;
        static uint* TCOR1;
        static uint* TCNT1;
        static ushort* TCR1;
        static uint* TCOR2;
        static uint* TCNT2;
        static ushort* TCR2;
        static uint* TCPR2;
        #endregion

        static ushort* SCSMR2;
        static byte* SCBRR2;
        static ushort* SCSCR2;
        static byte* SCFTDR2;
        static ushort* SCFSR2;
        static ushort* SCFCR2;
        static ushort* SCSPTR2;
        static ushort* SCLSR2;
        #endregion

        #region biosmem [mmr's on bios / flash area] pointer setup

        static uint* snd_dbg;
        static uint* g2_fifo;
        
        #region interups
		static uint* ACK_A = (uint*)(biosmem_b + 0x5F6900); // Pending Interrupts 1
		static uint* ACK_B = (uint*)(biosmem_b + 0x5f6904); // Pending Interrupts 2
		static uint* ACK_C = (uint*)(biosmem_b + 0x5f6908); // Pending Interrupts 3
        static uint* IRQD_A = (uint*)(biosmem_b + 0x5f6910); // Enabled Interrupts IRQD_A
        static uint* IRQD_B = (uint*)(biosmem_b + 0x5f6914); // Enabled Interrupts IRQD_B
        static uint* IRQD_C = (uint*)(biosmem_b + 0x5f6918); // Enabled Interrupts IRQD_C
        static uint* IRQB_A = (uint*)(biosmem_b + 0x5f6920); // Enabled Interrupts IRQB_A
        static uint* IRQB_B = (uint*)(biosmem_b + 0x5f6924); // Enabled Interrupts IRQB_B
        static uint* IRQB_C = (uint*)(biosmem_b + 0x5f6928); // Enabled Interrupts IRQB_C
        static uint* IRQ9_A = (uint*)(biosmem_b + 0x5f6930); // Enabled Interrupts IRQ9_A
        static uint* IRQ9_B = (uint*)(biosmem_b + 0x5f6934); // Enabled Interrupts IRQ9_B
        static uint* IRQ9_C = (uint*)(biosmem_b + 0x5f6938); // Enabled Interrupts IRQ9_C
        static uint* SB_PDTNRM = (uint*)(biosmem_b + 0x5f6940); // SB_PDTNRM	PVR-DMA trigger select from normal interrupt
        static uint* SB_PDTEXT = (uint*)(biosmem_b + 0x5f6944); // SB_PDTEXT	PVR-DMA trigger select from external interrupt
        #endregion

        #region mapple
        static uint* MAPLE_DMAADDR = (uint*)(biosmem_b + 0x5f6c04); // MAPLE_DMAADDR
        static uint* MAPLE_RESET2 = (uint*)(biosmem_b + 0x5f6c10); // MAPLE_RESET2
        static uint* MAPLE_ENABLE = (uint*)(biosmem_b + 0x5f6c14); // MAPLE_ENABLE
        static uint* MAPLE_STATE = (uint*)(biosmem_b + 0x5f6c18); // MAPLE_STATE
        static uint* MAPLE_SPEED = (uint*)(biosmem_b + 0x5f6c80); // MAPLE_SPEED
        static uint* MAPLE_RESET1 = (uint*)(biosmem_b + 0x5f6c8c); // MAPLE_RESET1
        #endregion 

        #region  PVR Registers
        // PVR Registers

        static uint* COREID = (uint*)(biosmem_b + 0x5f8000 + 0x00 * 4);// COREID [ID] (PVR2 Core ID)
        static uint* CORETYPE = (uint*)(biosmem_b + 0x5f8000 + 0x01 * 4);// CORETYPE [REV] (PVR2 Core version)
        static uint* COREDISABLE = (uint*)(biosmem_b + 0x5f8000 + 0x02 * 4);//COREDISABLE [CORERESET] (PVR2) (Enable/disable the submodules of the PVR2 Core)
        static uint* RENDERFORMAT = (uint*)(biosmem_b + 0x5f8000 + 0x03 * 4); // RENDERFORMAT, "alpha config"
        static uint* RENDERSTART = (uint*)(biosmem_b + 0x5f8000 + 0x05 * 4);//RENDERSTART (3D) (Start render strobe)
        static uint* TESTSELECT = (uint*)(biosmem_b + 0x5f8000 + 0x06 * 4);// [TESTSELECT]
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x07 * 4);// Unknown
        static uint* PARAM_BASE = (uint*)(biosmem_b + 0x5f8000 + 0x08 * 4);// PRIMALLOCBASE [PARAM_BASE] (3D) (Primitive allocation base)
        static uint* SPANSORTCFG = (uint*)(biosmem_b + 0x5f8000 + 0x09 * 4);// [SPANSORTCFG]
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x0a * 4);// ??
        static uint* TILEARRAY = (uint*)(biosmem_b + 0x5f8000 + 0x0b * 4);// TILEARRAY (Tile Array base address) [REGION_BASE]
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x0c * 4);// Unknown
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x0d * 4);// Unknown
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x0e * 4);// Unknown
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x0f * 4);// Unknown
        static uint* BORDERCOLOR = (uint*)(biosmem_b + 0x5f8000 + 0x10 * 4);// BORDERCOLOR (2D) (Border colour)
        static uint* BITMAPTYPE = (uint*)(biosmem_b + 0x5f8000 + 0x11 * 4);		// BITMAPTYPE (bitmap display settings)
        static uint* FB_W_CTRL = (uint*)(biosmem_b + 0x5f8000 + 0x12 * 4);		// RENDERFORMAT [FB_W_CTRL] (3D) (Render output format)
        static uint* FB_W_LINESTRIDE = (uint*)(biosmem_b + 0x5f8000 + 0x13 * 4);		// RENDERPITCH [FB_W_LINESTRIDE] (3D) (Render target pitch)
        static uint* FB_R_SOF1 = (uint*)(biosmem_b + 0x5f8000 + 0x14 * 4);		// FRAMEBUF [FB_R_SOF1] (2D) (Framebuffer address)
        static uint* FB_R_SOF2 = (uint*)(biosmem_b + 0x5f8000 + 0x15 * 4);		// FRAMEBUF [FB_R_SOF2] (2D) (Framebuffer address, short field)
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x16 * 4);		// Unknown
        static uint* DIWSIZE = (uint*)(biosmem_b + 0x5f8000 + 0x17 * 4);		// DIWSIZE (2D) (Display window size)
        static uint* FB_W_SOF1 = (uint*)(biosmem_b + 0x5f8000 + 0x18 * 4);		//RENDERBASE [FB_W_SOF1](3D) (Render target base address)
        static uint* FB_W_SOF2 = (uint*)(biosmem_b + 0x5f8000 + 0x19 * 4);		//[FB_W_SOF2]
        static uint* FB_X_CLIP = (uint*)(biosmem_b + 0x5f8000 + 0x1a * 4);		//RENDERWINDOWX [FB_X_CLIP] (3D) (Render output window X-start and X-stop)
        static uint* FB_Y_CLIP = (uint*)(biosmem_b + 0x5f8000 + 0x1b * 4);		//RENDERWINDOWY [FB_Y_CLIP] (3D) (Render output window Y-start and Y-stop)
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x1c * 4);		//Unknown
        static uint* FPU_SHAD_SCALE = (uint*)(biosmem_b + 0x5f8000 + 0x1d * 4);		//CHEAPSHADOWS [FPU_SHAD_SCALE] (3D) (Cheap shadow enable + strength)
        static uint* FPU_CULL_VAL = (uint*)(biosmem_b + 0x5f8000 + 0x1e * 4);		//CULLINGVALUE [FPU_CULL_VAL] (3D) (Minimum allowed polygon area)
        static uint* FPU_PARAM_CFG = (uint*)(biosmem_b + 0x5f8000 + 0x1f * 4);		//[FPU_PARAM_CFG] (3D) (Something to do with rendering)
        static uint* HALF_OFFSET = (uint*)(biosmem_b + 0x5f8000 + 0x20 * 4);		//[HALF_OFFSET]
        static uint* FPU_PERP_VAL = (uint*)(biosmem_b + 0x5f8000 + 0x21 * 4);		//[FPU_PERP_VAL] (3D) (Something to do with rendering)
        static uint* ISP_BACKGND_D = (uint*)(biosmem_b + 0x5f8000 + 0x22 * 4);		//[ISP_BACKGND_D]
        static uint* ISP_BACKGND_T = (uint*)(biosmem_b + 0x5f8000 + 0x23 * 4);		//BGPLANE [ISP_BACKGND_T] (3D) (Background plane location)   
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x24 * 4);		// Unknown
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x25 * 4);     // Unknown
        static uint* ISP_FEED_CFG = (uint*)(biosmem_b + 0x5f8000 + 0x26 * 4);     // [ISP_FEED_CFG]
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x27 * 4);     // Unknown
        static uint* SDRAM_REFRESH = (uint*)(biosmem_b + 0x5f8000 + 0x28 * 4);		// [SDRAM_REFRESH]
        static uint* SDRAM_ARB_CFG = (uint*)(biosmem_b + 0x5f8000 + 0x29 * 4);		// [SDRAM_ARB_CFG]
        static uint* SDRAM_CFG = (uint*)(biosmem_b + 0x5f8000 + 0x2a * 4);		// SDRAM_CFG (PVR) (Graphics memory control)
        //=(uint*)(biosmem_b+ 0x5f8000 + 0x2b * 4);		// Unknown
        static uint* FOG_COL_RAM = (uint*)(biosmem_b + 0x5f8000 + 0x2c * 4);		// FOGTABLECOLOR [FOG_COL_RAM] (3D) (Fogging colour when using table fog)
        static uint* FOG_COL_VERT = (uint*)(biosmem_b + 0x5f8000 + 0x2d * 4);		// [FOG_COL_VERT]
        static uint* FOG_DENSITY = (uint*)(biosmem_b + 0x5f8000 + 0x2e * 4);		// [FOG_DENSITY]
        static uint* FOG_CLAMP_MAX = (uint*)(biosmem_b + 0x5f8000 + 0x2f * 4);		//[FOG_CLAMP_MAX]
        static uint* FOG_CLAMP_MIN = (uint*)(biosmem_b + 0x5f8000 + 0x30 * 4);		// [FOG_CLAMP_MIN]
        static uint* SPG_TRIGGER_POS = (uint*)(biosmem_b + 0x5f8000 + 0x31 * 4);		// [SPG_TRIGGER_POS]
        static uint* SPG_HBLANK_INT = (uint*)(biosmem_b + (0xa05f8000 + 0x32 * 4)); // 0xa05f80c8, SPG_HBLANK_INT
        static uint* SPG_VBLANK_INT = (uint*)(biosmem_b + (0xa05f8000 + 0x33 * 4)); // 0xa05f80cc, SPG_VBLANK_INT
        static uint* SPG_CONTROL = (uint*)(biosmem_b + (0xa05f8000 + 0x34 * 4)); // 0xa05f80d0, SPG_CONTROL
        static uint* SPG_HBLANK = (uint*)(biosmem_b + 0x5f8000 + 0x35 * 4); // 0xa05f80d4, SPG_HBLANK
        static uint* SPG_LOAD = (uint*)(biosmem_b + 0x5f8000 + 0x36 * 4); // 0xa05f80d8, SPG_LOAD
        static uint* SPG_WIDTH = (uint*)(biosmem_b + 0x5f8000 + 0x39 * 4); // TEXTURESTRIDE [SPG_WIDTH] (3D) (Width of rectangular texture)
        static uint* TEXT_CONTROL = (uint*)(biosmem_b + 0x5f8000 + 0x3a * 4); // [TEXT_CONTROL]
        static uint* VO_CONTROL = (uint*)(biosmem_b + 0x5f8000 + 0x3b * 4); // [VO_CONTROL]
        static uint* VO_STARTX = (uint*)(biosmem_b + 0x5f8000 + 0x3c * 4); // [VO_STARTX]
        static uint* VO_STARTY = (uint*)(biosmem_b + 0x5f8000 + 0x3d * 4); // [VO_STARTY]
        static uint* SCALER_CTL = (uint*)(biosmem_b + 0x5f8000 + 0x3e * 4); // [SCALER_CTL]
        static uint* PAL_RAM_CTRL = (uint*)(biosmem_b + 0x5f8000 + 0x42 * 4); // [PAL_RAM_CTRL]
        static uint* SYNC_STAT = (uint*)(biosmem_b + 0x5f8000 + 0x43 * 4); // [SYNC_STAT]
        static uint* FB_BURSTCTRL = (uint*)(biosmem_b + 0x5f8000 + 0x44 * 4); // [FB_BURSTCTRL]
        static uint* FB_C_SOF = (uint*)(biosmem_b + 0x5f8000 + 0x45 * 4); // [FB_C_SOF]
        static uint* Y_COEFF = (uint*)(biosmem_b + 0x5f8000 + 0x46 * 4); // [Y_COEFF]
        static uint*PT_ALPHA_REF=(uint*)(biosmem_b+ 0x5f8000 + 0x47 * 4); //[PT_ALPHA_REF]
        static uint* TA_OL_BASE = (uint*)(biosmem_b + 0x5f8000 + 0x49 * 4); // PPMATRIXBASE [TA_OL_BASE] (TA) (Root PP-block matrices base address)
        static uint* TA_ISP_BASE = (uint*)(biosmem_b + 0x5f8000 + 0x4a * 4); // PRIMALLOCSTART [TA_ISP_BASE] (TA) (Primitive allocation area start)
        static uint* TA_OL_LIMIT = (uint*)(biosmem_b + 0x5f8000 + 0x4b * 4); // PPALLOCSTART [TA_OL_LIMIT] (TA) (PP-block allocation area start)
        static uint* TA_ISP_LIMIT = (uint*)(biosmem_b + 0x5f8000 + 0x4c * 4); // PRIMALLOCEND [TA_ISP_LIMIT] (TA) (Primitive allocation area end)
        static uint* TA_NEXT_OPB = (uint*)(biosmem_b + 0x5f8000 + 0x4d * 4); // PPALLOCPOS [TA_NEXT_OPB] (TA) (Current PP-block allocation position)
        static uint* TA_ITP_CURRENT = (uint*)(biosmem_b + 0x5f8000 + 0x4e * 4); // PRIMALLOCPOS [TA_ITP_CURRENT] (TA) (Current primitive allocation position)
        static uint* TA_GLOB_TILE_CLIP = (uint*)(biosmem_b + 0x5f8000 + 0x4f * 4); // TILEARRAYSIZE [TA_GLOB_TILE_CLIP] (TA) (Tile Array dimensions)
        static uint* TA_ALLOC_CTRL = (uint*)(biosmem_b + 0x5f8000 + 0x50 * 4); // PPBLOCKSIZE [TA_ALLOC_CTRL] (TA) (PP-block sizes)
        static uint* TA_LIST_INIT = (uint*)(biosmem_b + 0x5f8000 + 0x51 * 4); // TASTART [TA_LIST_INIT] (TA) (Start vertex enqueueing strobe)
        static uint* TA_YUV_TEX_BASE = (uint*)(biosmem_b + 0x5f8000 + 0x52 * 4); // [TA_YUV_TEX_BASE]
        static uint* TA_YUV_TEX_CTRL = (uint*)(biosmem_b + 0x5f8000 + 0x53 * 4); // [TA_YUV_TEX_CTRL]
        static uint* TA_YUV_TEX_CNT = (uint*)(biosmem_b + 0x5f8000 + 0x54 * 4); // [TA_YUV_TEX_CNT]
        static uint* TA_LIST_CONT = (uint*)(biosmem_b + 0x5f8000 + 0x58 * 4); // [TA_LIST_CONT]
        static uint* TA_NEXT_OPB_INIT = (uint*)(biosmem_b + 0x5f8000 + 0x59 * 4); // PPALLOCEND [TA_NEXT_OPB_INIT] (TA) (PP-block allocation area end)
        #endregion

        static uint* dc_rtc1 = (uint*)(biosmem_b + 0x710000); // Dreamcast RTC, reg 1
        static uint* dc_rtc2 = (uint*)(biosmem_b + 0x710004); // Dreamcast RTC, reg 2

        #endregion

        #endregion

        public static uint read(uint adr,int len)
		{
			//Emulate properly the P,ALT and NC bits- this will prob be done on
			//the MMUtranslate , also n*k
			adr=mmutrans(adr,len);//translate using mmu
			uint padr= adr & 0x1FFFFFFF;//get the phisical adress(discard p,alt,nc)
			uint offset=adr&0x3FFFFFF;
			switch (padr>>26)//get the area (upper 3 bits)
			{
				case 0://bios/flashrom/hardaware regs
					#region Bios/Flash/HardWare Registers
					if (offset<0x200000)//bios read
					{            
						return readBios(offset,len);
					}
					else if (offset < 0x240000)//flash ram read
					{
						return readBios_falsh(offset-0x200000,len);
					}
					else//register read
					{
						return readhwmmr(offset,len);
					}
					#endregion
				case 1://Video ram
					return readPvr(offset, len);// redirect to  readmem
				case 2://???? nothing???? olny mmu???
					WriteLine("Area2 read  adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16));
					return 0;
				case 3://System Ram
					#region System read
				switch (len)
				{
					case 1:
						return ram_b[offset];
					case 2:
						//fixed(byte *p=&ram[offset])
						//	return *(ushort*)p;
                        return ram_w[offset >> 1];
					case 3:
						//fixed(byte *p=&ram[offset])
						//	return *(ushort*)p;
						return ram_w[offset >> 1];
                    case 4:
						//fixed(byte *p=&ram[offset])
						//	return *(uint*)p;
                        return ram_dw[offset >> 2];
                }
					#endregion
					return 0;
				case 4://Tile acceletator coomand input
                    //WriteLine("TA Area4 read adr:" + Convert.ToString(adr, 16) + " padr:" + Convert.ToString(padr, 16));
					return TaRead(offset, len); ;//nothing yet
				case 5://Expansion (modem) port
                    WriteLine("Area5 read adr:" + Convert.ToString(adr, 16) + " padr:" + Convert.ToString(padr, 16));
                    return 0;//nothing yet
				case 6://???? nothing???? olny mmu???
                    WriteLine("Area6 read adr:" + Convert.ToString(adr, 16) + " padr:" + Convert.ToString(padr, 16));
                    return 0;
				case 7://Internal I.O. regs (same as p4) priv. mode only
                    return readInternalmmr(offset, len);//nothing yet
            }
			return 0;
		}
        public static void write(uint adr, uint data, int len)
        {

			//Emulate properly the P,ALT and NC bits- this will prob be done on
			//the MMUtranslate ,also n*k access test
			adr=mmutrans(adr,len);//translate using mmu
			uint padr= adr & 0x1FFFFFFF;//get the phisical adress(discard p,alt,nc)
			uint offset=adr&0x3FFFFFF;//get the area offset

			//if ((adr & 0x1FFFFFFF) == (0xa081fffc & 0x1FFFFFFF))
				//return;

			if (((adr >> 24) & 0xF8) == 0xE0)
			{
				
				offset = (adr >> 2) & 7; // 3 bits

				if ((adr & 0x20)!=0) // 0: SQ0, 1: SQ1
				{
					sq1_dw[offset] = data;
					//WriteLine("SQ1 WRITE" + offset.ToString());
				}
				else
				{
					sq0_dw[offset] = data;
					//WriteLine((sq0_dw[offset] ).ToString());
					//WriteLine("SQ0 WRITE ;" + offset.ToString());
				}
				return;
			}
			//if (offset == (0xac0000e0 & 0x3FFFFF))
			//	offset=100;
			switch (padr>>26)//get the area (upper 3 bits)
			{
				case 0://bios/flashrom/hardaware regs
					#region Bios/Flash/Hardware Registers
					if (offset<0x200000)//bios Write...heh good idea
					{
                        //WriteLine("Bios Write ?!?! (pc=" + pc + ") ," + offset.ToString());
                        return;
					}					
					else if (offset < 0x240000)//flash ram write
					{
						writeBios_flash(offset-0x200000,data,len);
						return;
					}
					else//register write
					{
						writehwmmr(offset,data,len);
						return;
					}
					#endregion
				case 1://Video ram
					writePvr(offset, data, len);
					return;// redirect to  writemem
				case 2://???? nothing???? olny mmu???
                    WriteLine("Area2 write adr:" + Convert.ToString(adr, 16) + " padr:" + Convert.ToString(padr, 16));
                    return;
				case 3://System Ram
					#region System write
					//fastint.disableblock(adr);
                    //Console.WriteLine(offset.ToString() + " pc: " + pc.ToString());
                    switch (len)
                    {
                        case 1:
                            ram_b[offset] = (byte)data;
                            return;
                        case 2:
                            //fixed(byte *p=&ram[offset])
                            //	*(ushort*)p=(ushort)data;
                            //ram[offset] = (byte)data;
                            //ram[offset + 1] = (byte)(data >> 8);
                            ram_w[offset >> 1] = (ushort)data;
                            return;
                        case 4:
                            //fixed(byte *p=&ram[offset])
                            //	*(uint*)p=data;
                            //ram[offset] = (byte)data;
                            //ram[offset + 1] = (byte)(data >> 8);
                            //ram[offset + 2] = (byte)(data >> 16);
                            //ram[offset + 3] = (byte)(data >> 24);
                            ram_dw[offset >> 2] = (uint)data;
                            return;
                    }
					#endregion
					return;
				case 4://Tile acceletator coomand input
                    //WriteLine("TA Area4 write adr:" + Convert.ToString(adr, 16) + " padr:" + Convert.ToString(padr, 16));
					TaWrite(offset, data, len);
                    return;//nothing yet
				case 5://Expansion (modem) port
                    WriteLine("Area5 write adr:" + Convert.ToString(adr, 16) + " padr:" + Convert.ToString(padr, 16));
                    return;//nothing yet
				case 6://???? nothing???? olny mmu???
                    WriteLine("Area6 write adr:" + Convert.ToString(adr, 16) + " padr:" + Convert.ToString(padr, 16));
                    return;
				case 7://Internal I.O. regs (same as p4) priv. mode only
                    writeInternalmmr(offset, data, len);
                    return;//nothing yet
			}

		}
        
        static void UpdateMem(uint cycles) { }

        //read/write Internal CPU regs (area 7 ,region p4)
		static uint readInternalmmr(uint adr,int len)
		{
			/*if (adr>0x3000000)
			{*/
                adr &= 0xFFFFFF;//get register offset
				switch (adr)
				{
					#region old
#if !optimised_b
					case 0://ccn.PTEH/32
						break;
					case 4://ccn.PTEL/32
						break;
					case 8://ccn.ttb/32
						break;
					case 0xC://ccn.tea/32
						break;
					case 0x10://ccn.mmucr/32
						break;
					case 0x14://ccn.basra/8
						break;
					case 0x18://ccn.basrb/8
						break;
					case 0x1C://ccn.ccr/32
						break;
					case 0x20://ccn.tra/32
						break;
					case 0x24://CCN.EXPEVT/32
						break;
					case 0x28://CCNINTEVT/32
						break;
					case 0x34://CCN.PTEA/32
						break;
					case 0x38://CCN.QACR0/32
						break;
					case 0x3C://CCN.QACR1/32
						break;
					case 0x20000://UBC.BARA/32
						break;
					case 0x20004://UBC.BAMRA/8
						break;
#endif
					case 0xe80010:// SCFSR2/16, Serial Status Register
						return *SCFSR2;
					#endregion
						/*
					case 0x000004:
						//PTEL = (uint*)&regmem_b[0x000004]; *PTEL = 0;
						break;
					case 0x00001C:
						//CCR = (uint*)&regmem_b[0x00001C]; *CCR = 0;
						break;
					case 0x000024:
						//EXPEVT = (uint*)&regmem_b[0x000024]; *EXPEVT = 0;
						break;
					case 0x000028:
						//INTEVT = (uint*)&regmem_b[0x000028]; *INTEVT = 0;
						break;
					case 0x000020:
						//TRA = (uint*)&regmem_b[0x000020];
						break;
					case 0x000038:
						//QACR0 = (uint*)&regmem_b[0x000038]; *QACR0 = 0;
						break;
					case 0x00003C:
						//QACR1 = (uint*)&regmem_b[0x00003C]; *QACR1 = 0;
						break;

					case 0x80002C:
						//PCTRA = (uint*)&regmem_b[0x80002C]; *PCTRA = 0;
						break;
					case 0x800030:
						//PDTRA = (ushort*)&regmem_b[0x800030]; *PDTRA = 0;
						break;
					case 0x800040:
						PCTRB = (uint*)&regmem_b[0x800040]; *PCTRB = 0;
						break;
					case 0x800044:
						//PDTRB = (ushort*)&regmem_b[0x800044]; *PDTRB = 0;
						break;

					#region DMA

					case 0xA00000:
						//SAR0 = (uint*)&regmem_b[0xA00000];
						break;
					case 0xA00004:
						//DAR0 = (uint*)&regmem_b[0xA00004];
						break;
					case 0xA00008:
						DMATCR0 = (uint*)&regmem_b[0xA00008];
						break;
					case 0xA0000C:
						//CHCR0 = (uint*)&regmem_b[0xA0000C]; *CHCR0 = 0;
						break;
					case 0xA00010:
						//SAR1 = (uint*)&regmem_b[0xA00010];
						break;
					case 0xA00014:
						DAR1 = (uint*)&regmem_b[0xA00014];
						break;
					case 0xA00018:
						//DMATCR1 = (uint*)&regmem_b[0xA00018];
						break;
					case 0xA0001C:
						//CHCR1 = (uint*)&regmem_b[0xA0001C]; *CHCR1 = 0;
						break;
					case 0xA00020:
						SAR2 = (uint*)&regmem_b[0xA00020];
						break;
					case 0xA00024:
						//DAR2 = (uint*)&regmem_b[0xA00024];
						break;
					case 0xA00028:
						//DMATCR2 = (uint*)&regmem_b[0xA00028];
						break;
					case 0xA0002C:
						//CHCR2 = (uint*)&regmem_b[0xA0002C]; *CHCR2 = 0;
						break;
					case 0xA00030:
						//SAR3 = (uint*)&regmem_b[0xA00030];
						break;
					case 0xA00034:
						//DAR3 = (uint*)&regmem_b[0xA00034];
						break;
					case 0xA00038:
						//DMATCR3 = (uint*)&regmem_b[0xA00038];
						break;
					case 0xA0003C:
						//CHCR3 = (uint*)&regmem_b[0xA0003C]; *CHCR3 = 0;
						break;
					case 0xA00040:
						//DMAOR = (uint*)&regmem_b[0xA00040]; *DMAOR = 0;
						break;

					#endregion

					case 0xD00000:
						ICR = (ushort*)&regmem_b[0xD00000]; *ICR = 0;
						break;
					case 0xD00000:
						IPRA = (ushort*)&regmem_b[0xD00000]; *IPRA = 0;
						break;
					case 0xD00008:
						IPRB = (ushort*)&regmem_b[0xD00008]; *IPRB = 0;
						break;
					case 0xD0000C:
						IPRC = (ushort*)&regmem_b[0xD0000C]; *IPRC = 0;
						break;


					#region TMU Registers

					case 0xD80000:
						TOCR = (byte*)&regmem_b[0xD80000]; *TOCR = 0;
						break;
					case 0xD80004:
						TSTR = (byte*)&regmem_b[0xD80004]; *TSTR = 0;
						break;
					case 0xD80008:
						TCOR0 = (uint*)&regmem_b[0xD80008]; *TCOR0 = 0xFFFFFFFF;
						break;
					case 0xD8000C:
						TCNT0 = (uint*)&regmem_b[0xD8000C]; *TCNT0 = 0xFFFFFFFF;
						break;
					case 0xD80010:
						TCR0 = (ushort*)&regmem_b[0xD80010]; *TCR0 = 0;
						break;
					case 0xD80014:
						TCOR1 = (uint*)&regmem_b[0xD80014]; *TCOR1 = 0xFFFFFFFF;
						break;
					case 0xD80018:
						TCNT1 = (uint*)&regmem_b[0xD80018]; *TCNT1 = 0xFFFFFFFF;
						break;
					case 0xD8001C:
						TCR1 = (ushort*)&regmem_b[0xD8001C]; *TCR1 = 0;
						break;
					case 0xD80020:
						TCOR2 = (uint*)&regmem_b[0xD80020]; *TCOR2 = 0xFFFFFFFF;
						break;
					case 0xD80024:
						TCNT2 = (uint*)&regmem_b[0xD80024]; *TCNT2 = 0xFFFFFFFF;
						break;
					case 0xD80028:
						TCR2 = (ushort*)&regmem_b[0xD80028]; *TCR2 = 0;
						break;
					case 0xD8002C:
						TCPR2 = (uint*)&regmem_b[0xD8002C];
						break;

					#endregion

					case 0xE80000:
						SCSMR2 = (ushort*)&regmem_b[0xE80000]; *SCSMR2 = 0;
						break;
					case 0xE80004:
						SCBRR2 = (byte*)&regmem_b[0xE80004]; *SCBRR2 = 0xFF;
						break;
					case 0xE80008:
						SCSCR2 = (ushort*)&regmem_b[0xE80008]; *SCSCR2 = 0;
						break;
					case 0xE8000C:
						SCFTDR2 = (byte*)&regmem_b[0xE8000C]; *SCFTDR2 = 0;
						break;
					case 0xE80010:
						SCFSR2 = (ushort*)&regmem_b[0xE80010]; *SCFSR2 = 0x60;
						break;
					case 0xE80018:
						SCFCR2 = (ushort*)&regmem_b[0xE80018]; *SCFCR2 = 0;
						break;
					case 0xE80020:
						SCSPTR2 = (ushort*)&regmem_b[0xE80020]; *SCSPTR2 = 0;
						break;
					case 0xE80024:
						SCLSR2 = (ushort*)&regmem_b[0xE80024]; *SCLSR2 = 0;
						break;
*/
#if !optimised_b
					default:
						//WriteLine("P4:Interlal I.O. registers read adr:" + Convert.ToString(adr, 16));
						break;
#endif
				}
                switch(len)
                {
                    case 1:
                        return regmem_b[adr];
                    case 2:
                        return regmem_w[adr>>1];
                    case 4:
                        return regmem_dw[adr>>2];
                }
                
            /*}
            adr &= 0xFFFFFF;
            switch (len)
            {
                case 1:
                    return regmem_b[adr];
                case 2:
                    return regmem_w[adr >> 1];
                case 4:
                    return regmem_dw[adr >> 2];
            }*/
            return 0;
		}
        static void writeInternalmmr(uint adr, uint data, int len)
        {
            /*if (adr > 0x3000000)
            {*/
                adr &= 0xFFFFFF;//get register offset
                switch (adr)
                {
	#if !optimised_b
                    case 0://ccn.PTEH/32
                        WriteLine("ccn.PTEH/32 write ; size=" + len.ToString());
                        break;
                    case 4://ccn.PTEL/32
                        WriteLine("ccn.PTEL/32 write ; size=" + len.ToString());
                        break;
                    case 8://ccn.ttb/32
                        WriteLine("ccn.ttb/32 write ; size=" + len.ToString());
                        break;
                    case 0xC://ccn.tea/32
                        WriteLine("ccn.tea/32 write ; size=" + len.ToString());
                        break;
                    case 0x10://ccn.mmucr/32
                        WriteLine("ccn.mmucr/32 write ; size=" + len.ToString());
                        break;
                    case 0x14://ccn.basra/8
                        WriteLine("ccn.basra/8 write ; size=" + len.ToString());
                        break;
                    case 0x18://ccn.basrb/8
                        WriteLine("ccn.basrb/8 write ; size=" + len.ToString());
                        break;
                    case 0x1C://ccn.ccr/32
                        WriteLine("ccn.ccr/32 write ; size=" + len.ToString());
                        break;
                    case 0x20://ccn.tra/32
                        WriteLine("ccn.tra/32 write ; size=" + len.ToString());
                        break;
                    case 0x24://CCN.EXPEVT/32
                        WriteLine("CCN.EXPEVT/32 write ; size=" + len.ToString());
                        break;
                    case 0x28://CCNINTEVT/32
                        WriteLine("CCNINTEVT/32 write ; size=" + len.ToString());
                        break;
                    case 0x34://CCN.PTEA/32
                        WriteLine("CCN.PTEA/32 write ; size=" + len.ToString());
                        break;
                    case 0x38://CCN.QACR0/32
                        //WriteLine("CCN.QACR0/32 write ; size=" + len.ToString());
                        break;
                    case 0x3C://CCN.QACR1/32
                        //WriteLine("CCN.QACR1/32 write ; size=" + len.ToString());
                        break;
                    case 0x20000://UBC.BARA/32
                        WriteLine("UBC.BARA/32 write ; size=" + len.ToString());
                        break;
                    case 0x20004://UBC.BAMRA/8
                        WriteLine("UBC.BAMRA/8 write ; size=" + len.ToString());
                        break;
#endif
                    case 0xe8000c:// SCFTDR2, Transmit FIFO Data Register
                        Console.Write((char)(byte)(data));
						if (((char)(byte)(data)) == '*')
							dc.dbger.mode = 1;
                        break;
                    case 0xe80010:// SCFSR2/16, Serial Status Register
                        //WriteLine("SCFSR2/16 Serial Status Register write ; size=" + len.ToString());
                        *SCFSR2 = 0x60;
                        return;//just a hack
                    default :
                        //WriteLine("P4:Interlal I.O. registers write adr:" + Convert.ToString(adr, 16) + " padr;" + " size :" + len.ToString() + " value " + Convert.ToString(data, 16));
                        break;
                }
                switch (len)
                {
                    case 1:
                        regmem_b[adr]=(byte)data;
                        return;
                    case 2:
                        regmem_w[adr >> 1] = (ushort)data;
                        return;
                    case 4:
                        regmem_dw[adr >> 2] = (uint)data;
                        return;
                }/*
            }
            adr &= 0xFFFFFF;
            switch (len)
            {
                case 1:
                    regmem_b[adr] = (byte)data;
                    return;
                case 2:
                    regmem_w[adr >> 1] = (ushort)data;
                    return;
                case 4:
                    regmem_dw[adr >> 2] = (uint)data;
                    return;
            }*/
        }
		


		//read/write HW regs (after the bios)- EXTERNAL hardware
        static uint readhwmmr(uint adr, int len)
        {
            //
            uint data = regmem_dw[adr >> 2];
            switch (adr & 0xFFFFFF)
            {
                case 0x80FFC0: // snd_dbg
                    WriteLinehw("HW Read snd_dbg :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					*snd_dbg = 3;//hack
                    break;

                case 0x5F688C: // G2 FIFO
                    //WriteLinehw("HW Read  G2 FIFO :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					*g2_fifo = 1 - *g2_fifo;//hacked
					//uint* p = g2_fifo;
					//*p = 1 - *p;
					//return 0;
					//return 1;//hack
                    break;
#if !optimised_b
                #region interups control registers
                case 0x5f6900: // Pending Interrupts 1
                    WriteLinehw("HW Read Pending Interrupts 1 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6904: // Pending Interrupts 2
                    WriteLinehw("HW Read  Pending Interrupts 2 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6908: // Pending Interrupts 3
                    WriteLinehw("HW Read Pending Interrupts 3 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6910: // Enabled Interrupts IRQD_A
                    WriteLinehw("HW Read Enabled Interrupts IRQD_A :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6914: // Enabled Interrupts IRQD_B
                    WriteLinehw("HW Read Enabled Interrupts IRQD_B :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6918: // Enabled Interrupts IRQD_C
                    WriteLinehw("HW Read Enabled Interrupts IRQD_C :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6920: // Enabled Interrupts IRQB_A
                    WriteLinehw("HW Read Enabled Interrupts IRQB_A :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6924: // Enabled Interrupts IRQB_B
                    WriteLinehw("HW Read Enabled Interrupts IRQB_B :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6928: // Enabled Interrupts IRQB_C
                    WriteLinehw("HW Read Enabled Interrupts IRQB_C :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6930: // Enabled Interrupts IRQ9_A
                    WriteLinehw("HW Read Enabled Interrupts IRQ9_A :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6934: // Enabled Interrupts IRQ9_B
                    WriteLinehw("HW Read Enabled Interrupts IRQ9_B :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6938: // Enabled Interrupts IRQ9_C
                    WriteLinehw("HW Read Enabled Interrupts IRQ9_C :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6940: // SB_PDTNRM	PVR-DMA trigger select from normal interrupt
                    WriteLinehw("HW Read SB_PDTNRM	PVR-DMA trigger select from normal interrupt :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6944: // SB_PDTEXT	PVR-DMA trigger select from external interrupt
                    WriteLinehw("HW Read SB_PDTEXT	PVR-DMA trigger select from external interrupt :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;
                #endregion

                #region MAPLE
                case 0x5f6c04: // MAPLE_DMAADDR
                    //WriteLinehw("HW Read MAPLE_DMAADDR :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6c10: // MAPLE_RESET2
                    WriteLinehw("HW Read MAPLE_RESET2 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6c14: // MAPLE_ENABLE
                    //WriteLinehw("HW Read MAPLE_ENABLE :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6c18: // MAPLE_STATE
                    //WriteLinehw("HW Read MAPLE_STATE :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6c80: // MAPLE_SPEED
                    WriteLinehw("HW Read MAPLE_SPEED :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6c8c: // MAPLE_RESET1
                    WriteLinehw("HW Read MAPLE_RESET1 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;
                #endregion

                #region PVR Registers

                case 0x5f8012: // RENDERFORMAT, "alpha config"
                    WriteLinehw("HW Read RENDERFORMAT, alpha config :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x00 * 4:// COREID [ID] (PVR2 Core ID)
                    WriteLinehw("HW Read COREID [ID] (PVR2 Core ID) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					return 0x17fd11db;//fixed value
                    //break;

                case 0x5f8000 + 0x01 * 4:// CORETYPE [REV] (PVR2 Core version)
                    WriteLinehw("HW Read CORETYPE [REV] (PVR2 Core version) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x02 * 4://COREDISABLE [CORERESET] (PVR2) (Enable/disable the submodules of the PVR2 Core)
                    WriteLinehw("HW Read COREDISABLE [CORERESET] (PVR2) (Enable/disable the submodules of the PVR2 Core) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x05 * 4://RENDERSTART (3D) (Start render strobe)
                    WriteLinehw("HW Read RENDERSTART (3D) (Start render strobe) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x06 * 4:// [TESTSELECT]
                    WriteLinehw("HW Read TESTSELECT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x07 * 4:// Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x08 * 4:// PRIMALLOCBASE [PARAM_BASE] (3D) (Primitive allocation base)
                    WriteLinehw("HW Read PRIMALLOCBASE [PARAM_BASE] (3D) (Primitive allocation base) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x09 * 4:// [SPANSORTCFG]
                    WriteLinehw("HW Read SPANSORTCFG :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0a * 4:// Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0b * 4:// TILEARRAY (Tile Array base address) [REGION_BASE]
                    WriteLinehw("HW Read TILEARRAY (Tile Array base address) [REGION_BASE] :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0c * 4:// Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0d * 4:// Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0e * 4:// Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0f * 4:// Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x10 * 4:// BORDERCOLOR (2D) (Border colour)
                    WriteLinehw("HW Read BORDERCOLOR (2D) (Border colour) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x11 * 4:		// BITMAPTYPE (bitmap display settings)
                    WriteLinehw("HW Read BITMAPTYPE (bitmap display settings) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					//return 0x0000000d;//hack
                    break;

                case 0x5f8000 + 0x12 * 4:		// RENDERFORMAT [FB_W_CTRL] (3D) (Render output format)
                    WriteLinehw("HW Read RENDERFORMAT [FB_W_CTRL] (3D) (Render output format) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x13 * 4:		// RENDERPITCH [FB_W_LINESTRIDE] (3D) (Render target pitch)
                    WriteLinehw("HW Read RENDERPITCH [FB_W_LINESTRIDE] (3D) (Render target pitch) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x14 * 4:		// FRAMEBUF [FB_R_SOF1] (2D) (Framebuffer address)
                    WriteLinehw("HW Read FRAMEBUF [FB_R_SOF1] (2D) (Framebuffer address) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x15 * 4:		// FRAMEBUF [FB_R_SOF2] (2D) (Framebuffer address, short field)
                    WriteLinehw("HW Read FRAMEBUF [FB_R_SOF2] (2D) (Framebuffer address, short field) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x16 * 4:		// Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x17 * 4:		// DIWSIZE (2D) (Display window size)
                    WriteLinehw("HW Read DIWSIZE (2D) (Display window size) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x18 * 4:		//RENDERBASE [FB_W_SOF1](3D) (Render target base address)
                    WriteLinehw("HW Read RENDERBASE [FB_W_SOF1](3D) (Render target base address) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x19 * 4:		//[FB_W_SOF2]
                    WriteLinehw("HW Read FB_W_SOF2 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1a * 4:		//RENDERWINDOWX [FB_X_CLIP] (3D) (Render output window X-start and X-stop)
                    WriteLinehw("HW Read RENDERWINDOWX [FB_X_CLIP] (3D) (Render output window X-start and X-stop) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1b * 4:		//RENDERWINDOWY [FB_Y_CLIP] (3D) (Render output window Y-start and Y-stop)
                    WriteLinehw("HW Read RENDERWINDOWY [FB_Y_CLIP] (3D) (Render output window Y-start and Y-stop) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1c * 4:		//Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1d * 4:		//CHEAPSHADOWS [FPU_SHAD_SCALE] (3D) (Cheap shadow enable + strength)
                    WriteLinehw("HW Read CHEAPSHADOWS [FPU_SHAD_SCALE] (3D) (Cheap shadow enable + strength) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1e * 4:		//CULLINGVALUE [FPU_CULL_VAL] (3D) (Minimum allowed polygon area)
                    WriteLinehw("HW Read CULLINGVALUE [FPU_CULL_VAL] (3D) (Minimum allowed polygon area) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1f * 4:		//[FPU_PARAM_CFG] (3D) (Something to do with rendering)
                    WriteLinehw("HW Read [FPU_PARAM_CFG] (3D) (Something to do with rendering) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x20 * 4:		//[HALF_OFFSET]
                    WriteLinehw("HW Read HALF_OFFSET :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x21 * 4:		//[FPU_PERP_VAL] (3D) (Something to do with rendering)
                    WriteLinehw("HW Read [FPU_PERP_VAL] (3D) (Something to do with rendering) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x22 * 4:		//[ISP_BACKGND_D]
                    WriteLinehw("HW Read ISP_BACKGND_D :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x23 * 4:		//BGPLANE [ISP_BACKGND_T] (3D) (Background plane location)   
                    WriteLinehw("HW Read BGPLANE [ISP_BACKGND_T] (3D) (Background plane location)    :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x24 * 4:		// Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x25 * 4:     // Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x26 * 4:     // [ISP_FEED_CFG]
                    WriteLinehw("HW Read ISP_FEED_CFG :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x27 * 4:     // Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x28 * 4:		// pvr_write: [SDRAM_REFRESH]
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x29 * 4:		// pvr_write: [SDRAM_ARB_CFG]
                    WriteLinehw("HW Read pvr_write: [SDRAM_ARB_CFG] :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2a * 4:		// SDRAM_CFG (PVR) (Graphics memory control)
                    WriteLinehw("HW Read SDRAM_CFG (PVR) (Graphics memory control) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2b * 4:		// Unknown
                    WriteLinehw("HW Read Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2c * 4:		// FOGTABLECOLOR [FOG_COL_RAM] (3D) (Fogging colour when using table fog)
                    WriteLinehw("HW Read FOGTABLECOLOR [FOG_COL_RAM] (3D) (Fogging colour when using table fog) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2d * 4:		// [FOG_COL_VERT]
                    WriteLinehw("HW Read FOG_COL_VERT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2e * 4:		// [FOG_DENSITY]
                    WriteLinehw("HW Read FOG_DENSITY :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2f * 4:		//[FOG_CLAMP_MAX]
                    WriteLinehw("HW Read FOG_CLAMP_MAX :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x30 * 4:		// [FOG_CLAMP_MIN]
                    WriteLinehw("HW Read FOG_CLAMP_MIN :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x31 * 4:		// [SPG_TRIGGER_POS]
                    WriteLinehw("HW Read SPG_TRIGGER_POS :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case (0xa05f8000 + 0x32 * 4): // 0xa05f80c8, SPG_HBLANK_INT
                    WriteLinehw("HW Read  0xa05f80c8, SPG_HBLANK_INT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case (0xa05f8000 + 0x33 * 4): // 0xa05f80cc, SPG_VBLANK_INT
                    WriteLinehw("HW Read 0xa05f80cc, SPG_VBLANK_INT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case (0xa05f8000 + 0x34 * 4): // 0xa05f80d0, SPG_CONTROL
                    WriteLinehw("HW Read  0xa05f80d0, SPG_CONTROL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x35 * 4: // 0xa05f80d4, SPG_HBLANK
                    WriteLinehw("HW Read 0xa05f80d4, SPG_HBLANK :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x36 * 4: // 0xa05f80d8, SPG_LOAD
                    WriteLinehw("HW Read 0xa05f80d8, SPG_LOAD :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x39 * 4: // TEXTURESTRIDE [SPG_WIDTH] (3D) (Width of rectangular texture)
                    WriteLinehw("HW Read TEXTURESTRIDE [SPG_WIDTH] (3D) (Width of rectangular texture) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x3a * 4: // [TEXT_CONTROL]
                    WriteLinehw("HW Read TEXT_CONTROL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x3b * 4: // [VO_CONTROL]
                    WriteLinehw("HW Read VO_CONTROL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x3c * 4: // [VO_STARTX]
                    WriteLinehw("HW Read VO_STARTX :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x3d * 4: // [VO_STARTY]
                    WriteLinehw("HW Read VO_STARTY :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x3e * 4: // [SCALER_CTL]
                    WriteLinehw("HW Read SCALER_CTL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x42 * 4: // [PAL_RAM_CTRL]
                    WriteLinehw("HW Read PAL_RAM_CTRL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x43 * 4: // [SYNC_STAT]
                    //WriteLinehw("HW Read SYNC_STAT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					return  (uint)((emu.gl_cop_cnt / 10) % 0x1FF);
                    //break;

                case 0x5f8000 + 0x44 * 4: // [FB_BURSTCTRL]
                    WriteLinehw("HW Read FB_BURSTCTRL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x45 * 4: // [FB_C_SOF]
                    WriteLinehw("HW Read FB_C_SOF :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x46 * 4: // [Y_COEFF]
                    WriteLinehw("HW Read Y_COEFF :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x47 * 4: //[PT_ALPHA_REF]
                    WriteLinehw("HW Read PT_ALPHA_REF :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x49 * 4: // PPMATRIXBASE [TA_OL_BASE] (TA) (Root PP-block matrices base address)
                    WriteLinehw("HW Read PPMATRIXBASE [TA_OL_BASE] (TA) (Root PP-block matrices base address) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4a * 4: // PRIMALLOCSTART [TA_ISP_BASE] (TA) (Primitive allocation area start)
                    WriteLinehw("HW Read PRIMALLOCSTART [TA_ISP_BASE] (TA) (Primitive allocation area start) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4b * 4: // PPALLOCSTART [TA_OL_LIMIT] (TA) (PP-block allocation area start)
                    WriteLinehw("HW Read PPALLOCSTART [TA_OL_LIMIT] (TA) (PP-block allocation area start) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4c * 4: // PRIMALLOCEND [TA_ISP_LIMIT] (TA) (Primitive allocation area end)
                    WriteLinehw("HW Read  PRIMALLOCEND [TA_ISP_LIMIT] (TA) (Primitive allocation area end) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4d * 4: // PPALLOCPOS [TA_NEXT_OPB] (TA) (Current PP-block allocation position)
                    WriteLinehw("HW Read PPALLOCPOS [TA_NEXT_OPB] (TA) (Current PP-block allocation position) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4e * 4: // PRIMALLOCPOS [TA_ITP_CURRENT] (TA) (Current primitive allocation position)
                    WriteLinehw("HW Read PRIMALLOCPOS [TA_ITP_CURRENT] (TA) (Current primitive allocation position) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4f * 4: // TILEARRAYSIZE [TA_GLOB_TILE_CLIP] (TA) (Tile Array dimensions)
                    WriteLinehw("HW Read TILEARRAYSIZE [TA_GLOB_TILE_CLIP] (TA) (Tile Array dimensions) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x50 * 4: // PPBLOCKSIZE [TA_ALLOC_CTRL] (TA) (PP-block sizes)
                    WriteLinehw("HW Read PPBLOCKSIZE [TA_ALLOC_CTRL] (TA) (PP-block sizes) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x51 * 4: // TASTART [TA_LIST_INIT] (TA) (Start vertex enqueueing strobe)
                    WriteLinehw("HW Read TASTART [TA_LIST_INIT] (TA) (Start vertex enqueueing strobe) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x52 * 4: // [TA_YUV_TEX_BASE]
                    WriteLinehw("HW Read TA_YUV_TEX_BASE :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x53 * 4: // [TA_YUV_TEX_CTRL]
                    WriteLinehw("HW Read TA_YUV_TEX_CTRL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x54 * 4: // [TA_YUV_TEX_CNT]
                    WriteLinehw("HW Read TA_YUV_TEX_CNT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x58 * 4: // [TA_LIST_CONT]
                    WriteLinehw("HW Read TA_LIST_CONT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x59 * 4: // PPALLOCEND [TA_NEXT_OPB_INIT] (TA) (PP-block allocation area end)
                    WriteLinehw("HW Read PPALLOCEND [TA_NEXT_OPB_INIT] (TA) (PP-block allocation area end) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                #endregion

                case 0x710000: // Dreamcast RTC, reg 1
                    WriteLinehw("HW Read Dreamcast RTC, reg 1 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x710004: // Dreamcast RTC, reg 2
                    WriteLinehw("HW Read Dreamcast RTC, reg 2 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                default:
                    //WriteLine("WH I.O. registers Read adr:" + Convert.ToString(adr, 16) + " size :" + len.ToString() + " value " + Convert.ToString(data, 16));
                    break;
#endif
            }
            switch (len)
            {
                case 1:
					return biosmem_b[adr];
                case 2:
					return biosmem_w[adr >> 1];
                case 4:
					return biosmem_bw[adr >> 2];
            }
            return 0;
        }
        static void writehwmmr(uint adr,uint data,int len)
        {
            switch (adr & 0xFFFFFF)
            {
#if !optimised_b
                case 0x80FFC0: // snd_dbg
                    WriteLinehw("HW Write snd_dbg :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5F688C: // G2 FIFO
                    WriteLinehw("HW Write  G2 FIFO :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                #region interups control registers
                case 0x5f6900: // Pending Interrupts 1
                    WriteLinehw("HW Write Pending Interrupts 1 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6904: // Pending Interrupts 2
                    WriteLinehw("HW Write  Pending Interrupts 2 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6908: // Pending Interrupts 3
                    WriteLinehw("HW Write Pending Interrupts 3 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6910: // Enabled Interrupts IRQD_A
                    WriteLinehw("HW Write Enabled Interrupts IRQD_A :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6914: // Enabled Interrupts IRQD_B
                    WriteLinehw("HW Write Enabled Interrupts IRQD_B :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6918: // Enabled Interrupts IRQD_C
                    WriteLinehw("HW Write Enabled Interrupts IRQD_C :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6920: // Enabled Interrupts IRQB_A
                    WriteLinehw("HW Write Enabled Interrupts IRQB_A :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6924: // Enabled Interrupts IRQB_B
                    WriteLinehw("HW Write Enabled Interrupts IRQB_B :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6928: // Enabled Interrupts IRQB_C
                    WriteLinehw("HW Write Enabled Interrupts IRQB_C :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6930: // Enabled Interrupts IRQ9_A
                    WriteLinehw("HW Write Enabled Interrupts IRQ9_A :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6934: // Enabled Interrupts IRQ9_B
                    WriteLinehw("HW Write Enabled Interrupts IRQ9_B :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6938: // Enabled Interrupts IRQ9_C
                    WriteLinehw("HW Write Enabled Interrupts IRQ9_C :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6940: // SB_PDTNRM	PVR-DMA trigger select from normal interrupt
                    WriteLinehw("HW Write SB_PDTNRM	PVR-DMA trigger select from normal interrupt :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f6944: // SB_PDTEXT	PVR-DMA trigger select from external interrupt
                    WriteLinehw("HW Write SB_PDTEXT	PVR-DMA trigger select from external interrupt :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;
                #endregion
#endif
                #region MAPLE
#if !optimised_b
                case 0x5f6c04: // MAPLE_DMAADDR
                    //WriteLinehw("HW Write MAPLE_DMAADDR :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;
#endif
                case 0x5f6c10: // MAPLE_RESET2
                    //WriteLinehw("HW Write MAPLE_RESET2 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					MapleReset2(data);
                    break;

                case 0x5f6c14: // MAPLE_ENABLE
                    //WriteLinehw("HW Write MAPLE_ENABLE :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					MapleEnable(data);
					return;    
					//break;
					
                case 0x5f6c18: // MAPLE_STATE
                    //WriteLinehw("HW Write MAPLE_STATE :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					MapleDMAState(data);
					return;    
                    //break;

                case 0x5f6c80: // MAPLE_SPEED
                    //WriteLinehw("HW Write MAPLE_SPEED :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					MapleSpeed(data);
					return;
                    //break;

                case 0x5f6c8c: // MAPLE_RESET1
                    //WriteLinehw("HW Write MAPLE_RESET1 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					MapleReset1(data);
					return;
                    //break;
                #endregion

                #region PVR Registers
#if !optimised_b
                case 0x5f8012: // RENDERFORMAT, "alpha config"
                    WriteLinehw("HW Write RENDERFORMAT, alpha config :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x00 * 4:// COREID [ID] (PVR2 Core ID)
                    WriteLinehw("HW Write COREID [ID] (PVR2 Core ID) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x01 * 4:// CORETYPE [REV] (PVR2 Core version)
                    WriteLinehw("HW Write CORETYPE [REV] (PVR2 Core version) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x02 * 4://COREDISABLE [CORERESET] (PVR2) (Enable/disable the submodules of the PVR2 Core)
                    WriteLinehw("HW Write COREDISABLE [CORERESET] (PVR2) (Enable/disable the submodules of the PVR2 Core) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;
#endif
                case 0x5f8000 + 0x05 * 4://RENDERSTART (3D) (Start render strobe)
                    //WriteLinehw("HW Write RENDERSTART (3D) (Start render strobe) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					RenderStart();
                    break;
#if !optimised_b
                case 0x5f8000 + 0x06 * 4:// [TESTSELECT]
                    WriteLinehw("HW Write TESTSELECT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x07 * 4:// Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x08 * 4:// PRIMALLOCBASE [PARAM_BASE] (3D) (Primitive allocation base)
                    WriteLinehw("HW Write PRIMALLOCBASE [PARAM_BASE] (3D) (Primitive allocation base) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x09 * 4:// [SPANSORTCFG]
                    WriteLinehw("HW Write SPANSORTCFG :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0a * 4:// Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0b * 4:// TILEARRAY (Tile Array base address) [REGION_BASE]
                    WriteLinehw("HW Write TILEARRAY (Tile Array base address) [REGION_BASE] :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0c * 4:// Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0d * 4:// Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0e * 4:// Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x0f * 4:// Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x10 * 4:// BORDERCOLOR (2D) (Border colour)
                    WriteLinehw("HW Write BORDERCOLOR (2D) (Border colour) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;
#endif
                case 0x5f8000 + 0x11 * 4:		// BITMAPTYPE (bitmap display settings)
                    WriteLinehw("HW Write BITMAPTYPE (bitmap display settings) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					if ((data & 0x01)!=0)
						WriteLinehw("bitmap display enable");
					if ((data & 0x02)!=0)
						WriteLinehw("line doubling enable");
					switch ((data >> 2) & 0x3)
					{
						case 0x00:
							WriteLinehw("ARGB1555");
							bitinfo.biBitCount = 16;
							//screenbits = 16;
							break;

						case 0x01:
							WriteLinehw("RGB565");
							bitinfo.biBitCount = 16;
							//screenbits = 16;
							break;

						case 0x02:
							WriteLinehw("RGB888");
							bitinfo.biBitCount = 24;
							//screenbits = 24;
							break;

						case 0x03:
							WriteLinehw("ARGB8888");
							bitinfo.biBitCount = 32;
							//screenbits = 32;
							break;
					}
					if ((data & 0x00800000)!=0)
						WriteLinehw("pixel clock double enable");
                    break;
#if !optimised_b
                case 0x5f8000 + 0x12 * 4:		// RENDERFORMAT [FB_W_CTRL] (3D) (Render output format)
                    WriteLinehw("HW Write RENDERFORMAT [FB_W_CTRL] (3D) (Render output format) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x13 * 4:		// RENDERPITCH [FB_W_LINESTRIDE] (3D) (Render target pitch)
                    WriteLinehw("HW Write RENDERPITCH [FB_W_LINESTRIDE] (3D) (Render target pitch) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x14 * 4:		// FRAMEBUF [FB_R_SOF1] (2D) (Framebuffer address)
                    WriteLinehw("HW Write FRAMEBUF [FB_R_SOF1] (2D) (Framebuffer address) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x15 * 4:		// FRAMEBUF [FB_R_SOF2] (2D) (Framebuffer address, short field)
                    WriteLinehw("HW Write FRAMEBUF [FB_R_SOF2] (2D) (Framebuffer address, short field) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x16 * 4:		// Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x17 * 4:		// DIWSIZE (2D) (Display window size)
                    WriteLinehw("HW Write DIWSIZE (2D) (Display window size) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x18 * 4:		//RENDERBASE [FB_W_SOF1](3D) (Render target base address)
                    WriteLinehw("HW Write RENDERBASE [FB_W_SOF1](3D) (Render target base address) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x19 * 4:		//[FB_W_SOF2]
                    WriteLinehw("HW Write FB_W_SOF2 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1a * 4:		//RENDERWINDOWX [FB_X_CLIP] (3D) (Render output window X-start and X-stop)
                    WriteLinehw("HW Write RENDERWINDOWX [FB_X_CLIP] (3D) (Render output window X-start and X-stop) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1b * 4:		//RENDERWINDOWY [FB_Y_CLIP] (3D) (Render output window Y-start and Y-stop)
                    WriteLinehw("HW Write RENDERWINDOWY [FB_Y_CLIP] (3D) (Render output window Y-start and Y-stop) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1c * 4:		//Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1d * 4:		//CHEAPSHADOWS [FPU_SHAD_SCALE] (3D) (Cheap shadow enable + strength)
                    WriteLinehw("HW Write CHEAPSHADOWS [FPU_SHAD_SCALE] (3D) (Cheap shadow enable + strength) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1e * 4:		//CULLINGVALUE [FPU_CULL_VAL] (3D) (Minimum allowed polygon area)
                    WriteLinehw("HW Write CULLINGVALUE [FPU_CULL_VAL] (3D) (Minimum allowed polygon area) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x1f * 4:		//[FPU_PARAM_CFG] (3D) (Something to do with rendering)
                    WriteLinehw("HW Write [FPU_PARAM_CFG] (3D) (Something to do with rendering) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x20 * 4:		//[HALF_OFFSET]
                    WriteLinehw("HW Write HALF_OFFSET :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x21 * 4:		//[FPU_PERP_VAL] (3D) (Something to do with rendering)
                    WriteLinehw("HW Write [FPU_PERP_VAL] (3D) (Something to do with rendering) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x22 * 4:		//[ISP_BACKGND_D]
                    WriteLinehw("HW Write ISP_BACKGND_D :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x23 * 4:		//BGPLANE [ISP_BACKGND_T] (3D) (Background plane location)   
                    WriteLinehw("HW Write BGPLANE [ISP_BACKGND_T] (3D) (Background plane location)    :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x24 * 4:		// Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x25 * 4:     // Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x26 * 4:     // [ISP_FEED_CFG]
                    WriteLinehw("HW Write ISP_FEED_CFG :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x27 * 4:     // Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x28 * 4:		// pvr_write: [SDRAM_REFRESH]
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x29 * 4:		// pvr_write: [SDRAM_ARB_CFG]
                    WriteLinehw("HW Write pvr_write: [SDRAM_ARB_CFG] :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2a * 4:		// SDRAM_CFG (PVR) (Graphics memory control)
                    WriteLinehw("HW Write SDRAM_CFG (PVR) (Graphics memory control) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2b * 4:		// Unknown
                    WriteLinehw("HW Write Unknown :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2c * 4:		// FOGTABLECOLOR [FOG_COL_RAM] (3D) (Fogging colour when using table fog)
                    WriteLinehw("HW Write FOGTABLECOLOR [FOG_COL_RAM] (3D) (Fogging colour when using table fog) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2d * 4:		// [FOG_COL_VERT]
                    WriteLinehw("HW Write FOG_COL_VERT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2e * 4:		// [FOG_DENSITY]
                    WriteLinehw("HW Write FOG_DENSITY :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x2f * 4:		//[FOG_CLAMP_MAX]
                    WriteLinehw("HW Write FOG_CLAMP_MAX :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x30 * 4:		// [FOG_CLAMP_MIN]
                    WriteLinehw("HW Write FOG_CLAMP_MIN :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x31 * 4:		// [SPG_TRIGGER_POS]
                    WriteLinehw("HW Write SPG_TRIGGER_POS :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case (0xa05f8000 + 0x32 * 4): // 0xa05f80c8, SPG_HBLANK_INT
                    WriteLinehw("HW Write  0xa05f80c8, SPG_HBLANK_INT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case (0xa05f8000 + 0x33 * 4): // 0xa05f80cc, SPG_VBLANK_INT
                    WriteLinehw("HW Write 0xa05f80cc, SPG_VBLANK_INT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case (0xa05f8000 + 0x34 * 4): // 0xa05f80d0, SPG_CONTROL
                    WriteLinehw("HW Write  0xa05f80d0, SPG_CONTROL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x35 * 4: // 0xa05f80d4, SPG_HBLANK
                    WriteLinehw("HW Write 0xa05f80d4, SPG_HBLANK :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x36 * 4: // 0xa05f80d8, SPG_LOAD
                    WriteLinehw("HW Write 0xa05f80d8, SPG_LOAD :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x39 * 4: // TEXTURESTRIDE [SPG_WIDTH] (3D) (Width of rectangular texture)
                    WriteLinehw("HW Write TEXTURESTRIDE [SPG_WIDTH] (3D) (Width of rectangular texture) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x3a * 4: // [TEXT_CONTROL]
                    WriteLinehw("HW Write TEXT_CONTROL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x3b * 4: // [VO_CONTROL]
                    WriteLinehw("HW Write VO_CONTROL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x3c * 4: // [VO_STARTX]
                    WriteLinehw("HW Write VO_STARTX :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x3d * 4: // [VO_STARTY]
                    WriteLinehw("HW Write VO_STARTY :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;
   
                case 0x5f8000 + 0x3e * 4: // [SCALER_CTL]
                    WriteLinehw("HW Write SCALER_CTL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x42 * 4: // [PAL_RAM_CTRL]
                    WriteLinehw("HW Write PAL_RAM_CTRL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x43 * 4: // [SYNC_STAT]
                    WriteLinehw("HW Write SYNC_STAT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;
  
                case 0x5f8000 + 0x44 * 4: // [FB_BURSTCTRL]
                    WriteLinehw("HW Write FB_BURSTCTRL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x45 * 4: // [FB_C_SOF]
                    WriteLinehw("HW Write FB_C_SOF :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x46 * 4: // [Y_COEFF]
                    WriteLinehw("HW Write Y_COEFF :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x47 * 4: //[PT_ALPHA_REF]
                    WriteLinehw("HW Write PT_ALPHA_REF :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x49 * 4: // PPMATRIXBASE [TA_OL_BASE] (TA) (Root PP-block matrices base address)
                    WriteLinehw("HW Write PPMATRIXBASE [TA_OL_BASE] (TA) (Root PP-block matrices base address) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4a * 4: // PRIMALLOCSTART [TA_ISP_BASE] (TA) (Primitive allocation area start)
                    WriteLinehw("HW Write PRIMALLOCSTART [TA_ISP_BASE] (TA) (Primitive allocation area start) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4b * 4: // PPALLOCSTART [TA_OL_LIMIT] (TA) (PP-block allocation area start)
                    WriteLinehw("HW Write PPALLOCSTART [TA_OL_LIMIT] (TA) (PP-block allocation area start) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4c * 4: // PRIMALLOCEND [TA_ISP_LIMIT] (TA) (Primitive allocation area end)
                    WriteLinehw("HW Write  PRIMALLOCEND [TA_ISP_LIMIT] (TA) (Primitive allocation area end) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4d * 4: // PPALLOCPOS [TA_NEXT_OPB] (TA) (Current PP-block allocation position)
                    WriteLinehw("HW Write PPALLOCPOS [TA_NEXT_OPB] (TA) (Current PP-block allocation position) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4e * 4: // PRIMALLOCPOS [TA_ITP_CURRENT] (TA) (Current primitive allocation position)
                    WriteLinehw("HW Write PRIMALLOCPOS [TA_ITP_CURRENT] (TA) (Current primitive allocation position) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x4f * 4: // TILEARRAYSIZE [TA_GLOB_TILE_CLIP] (TA) (Tile Array dimensions)
                    WriteLinehw("HW Write TILEARRAYSIZE [TA_GLOB_TILE_CLIP] (TA) (Tile Array dimensions) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;
#endif
				case 0x5f8000 + 0x50 * 4: // PPBLOCKSIZE [TA_ALLOC_CTRL] (TA) (PP-block sizes) /ta_opb_cfg
					//WriteLinehw("HW Write PPBLOCKSIZE [TA_ALLOC_CTRL] (TA) (PP-block sizes) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
					{
						uint punch_through = (data >> 16) & 0x3;
						uint transmod = (data >> 12) & 0x3;
						uint transpoly = (data >> 8) & 0x3;
						uint opaquemod = (data >> 4) & 0x3;
						uint opaquepoly = (data >> 0) & 0x3;

						pvr_registered = 0;

						if (punch_through > 0)
							pvr_registered |= 1 << 4;

						if (transmod > 0)
							pvr_registered |= 1 << 3;

						if (transpoly > 0)
							pvr_registered |= 1 << 2;

						if (opaquemod > 0)
							pvr_registered |= 1 << 1;

						if (opaquepoly > 0)
							pvr_registered |= 1 << 0;
					}
					break;
#if !optimised_b
                case 0x5f8000 + 0x51 * 4: // TASTART [TA_LIST_INIT] (TA) (Start vertex enqueueing strobe)
                    WriteLinehw("HW Write TASTART [TA_LIST_INIT] (TA) (Start vertex enqueueing strobe) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x52 * 4: // [TA_YUV_TEX_BASE]
                    WriteLinehw("HW Write TA_YUV_TEX_BASE :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x53 * 4: // [TA_YUV_TEX_CTRL]
                    WriteLinehw("HW Write TA_YUV_TEX_CTRL :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x54 * 4: // [TA_YUV_TEX_CNT]
                    WriteLinehw("HW Write TA_YUV_TEX_CNT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x58 * 4: // [TA_LIST_CONT]
                    WriteLinehw("HW Write TA_LIST_CONT :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x5f8000 + 0x59 * 4: // PPALLOCEND [TA_NEXT_OPB_INIT] (TA) (PP-block allocation area end)
                    WriteLinehw("HW Write PPALLOCEND [TA_NEXT_OPB_INIT] (TA) (PP-block allocation area end) :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;
#endif
                #endregion
#if !optimised_b
                case 0x710000: // Dreamcast RTC, reg 1
                    WriteLinehw("HW Write Dreamcast RTC, reg 1 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                case 0x710004: // Dreamcast RTC, reg 2
                    WriteLinehw("HW Write Dreamcast RTC, reg 2 :" + Convert.ToString(data, 16) + " ;size :" + len.ToString());
                    break;

                default :
                    //WriteLine("WH I.O. registers write adr:" + Convert.ToString(adr, 16) + " size :" + len.ToString() + " value " + Convert.ToString(data, 16));
                    break;
#endif
            }
            switch (len)
            {
                case 1:
					biosmem_b[adr] = (byte)data;
                    return;
                case 2:
					biosmem_w[adr >> 1] = (ushort)data;
                    return;
                case 4:
					biosmem_bw[adr >> 2] = (uint)data;
                    return;
            }
        }

        public static void Init()
        {
            /*malloc_lib.mallocClass c=new malloc_lib.mallocClass();
            mal = c.isMalloc();
            c = null;
            DeInit();
            ram_b = (byte*)mal.Alloc(16 * dc.mb);
            ram_w = (ushort*)ram_b;
            ram_dw =(uint*)ram_b;

            vram_b = (byte*)mal.Alloc(8 * dc.mb);
            vram_w = (ushort*)vram_b;
            vram_dw = (uint*)vram_b;*/
            //pointerlib.unmanaged_pointer t=new pointerlib.unmanaged_pointer(100);
            //vram_b = t.Ptr_byte;
        }
        public static void DeInit()
        {
           /* mal.Free((int)ram_b);
            ram_b = null;
            ram_w = null;
            ram_dw = null;

            mal.Free((int)vram_b);
            vram_b = null;
            vram_w = null;
            vram_dw = null;*/
        }
        static void ResetMem()
        {

            #region regmem [internal registers] pointers setup ;)
            PTEL = (uint*)&regmem_b[0x000004]; *PTEL = 0;
            CCR = (uint*)&regmem_b[0x00001C]; *CCR = 0;
            EXPEVT = (uint*)&regmem_b[0x000024]; *EXPEVT = 0;
            INTEVT = (uint*)&regmem_b[0x000028]; *INTEVT = 0;
            TRA = (uint*)&regmem_b[0x000020];
            QACR0 = (uint*)&regmem_b[0x000038]; *QACR0 = 0;
            QACR1 = (uint*)&regmem_b[0x00003C]; *QACR1 = 0;


            PCTRA = (uint*)&regmem_b[0x80002C]; *PCTRA = 0;
            PDTRA = (ushort*)&regmem_b[0x800030]; *PDTRA = 0;
            PCTRB = (uint*)&regmem_b[0x800040]; *PCTRB = 0;
            PDTRB = (ushort*)&regmem_b[0x800044]; *PDTRB = 0;

            #region DMA
            SAR0 = (uint*)&regmem_b[0xA00000];
            DAR0 = (uint*)&regmem_b[0xA00004];
            DMATCR0 = (uint*)&regmem_b[0xA00008];
            CHCR0 = (uint*)&regmem_b[0xA0000C]; *CHCR0 = 0;

            SAR1 = (uint*)&regmem_b[0xA00010];
            DAR1 = (uint*)&regmem_b[0xA00014];
            DMATCR1 = (uint*)&regmem_b[0xA00018];
            CHCR1 = (uint*)&regmem_b[0xA0001C]; *CHCR1 = 0;

            SAR2 = (uint*)&regmem_b[0xA00020];
            DAR2 = (uint*)&regmem_b[0xA00024];
            DMATCR2 = (uint*)&regmem_b[0xA00028];
            CHCR2 = (uint*)&regmem_b[0xA0002C]; *CHCR2 = 0;

            SAR3 = (uint*)&regmem_b[0xA00030];
            DAR3 = (uint*)&regmem_b[0xA00034];
            DMATCR3 = (uint*)&regmem_b[0xA00038];
            CHCR3 = (uint*)&regmem_b[0xA0003C]; *CHCR3 = 0;

            DMAOR = (uint*)&regmem_b[0xA00040]; *DMAOR = 0;
            #endregion

            ICR = (ushort*)&regmem_b[0xD00000]; *ICR = 0;
            IPRA = (ushort*)&regmem_b[0xD00004]; *IPRA = 0;
            IPRB = (ushort*)&regmem_b[0xD00008]; *IPRB = 0;
            IPRC = (ushort*)&regmem_b[0xD0000C]; *IPRC = 0;

            #region TMU Registers
            TOCR = (byte*)&regmem_b[0xD80000]; *TOCR = 0;
            TSTR = (byte*)&regmem_b[0xD80004]; *TSTR = 0;
            TCOR0 = (uint*)&regmem_b[0xD80008]; *TCOR0 = 0xFFFFFFFF;
            TCNT0 = (uint*)&regmem_b[0xD8000C]; *TCNT0 = 0xFFFFFFFF;
            TCR0 = (ushort*)&regmem_b[0xD80010]; *TCR0 = 0;
            TCOR1 = (uint*)&regmem_b[0xD80014]; *TCOR1 = 0xFFFFFFFF;
            TCNT1 = (uint*)&regmem_b[0xD80018]; *TCNT1 = 0xFFFFFFFF;
            TCR1 = (ushort*)&regmem_b[0xD8001C]; *TCR1 = 0;
            TCOR2 = (uint*)&regmem_b[0xD80020]; *TCOR2 = 0xFFFFFFFF;
            TCNT2 = (uint*)&regmem_b[0xD80024]; *TCNT2 = 0xFFFFFFFF;
            TCR2 = (ushort*)&regmem_b[0xD80028]; *TCR2 = 0;
            TCPR2 = (uint*)&regmem_b[0xD8002C];
            #endregion

            SCSMR2 = (ushort*)&regmem_b[0xE80000]; *SCSMR2 = 0;
            SCBRR2 = (byte*)&regmem_b[0xE80004]; *SCBRR2 = 0xFF;
            SCSCR2 = (ushort*)&regmem_b[0xE80008]; *SCSCR2 = 0;
            SCFTDR2 = (byte*)&regmem_b[0xE8000C]; *SCFTDR2 = 0;
            SCFSR2 = (ushort*)&regmem_b[0xE80010]; *SCFSR2 = 0x60;
            SCFCR2 = (ushort*)&regmem_b[0xE80018]; *SCFCR2 = 0;
            SCSPTR2 = (ushort*)&regmem_b[0xE80020]; *SCSPTR2 = 0;
            SCLSR2 = (ushort*)&regmem_b[0xE80024]; *SCLSR2 = 0;

            #endregion

            #region biosmem [mmr's on bios / flash area] pointer setup
                
                snd_dbg=(uint*)(biosmem_b+ 0x80FFC0); // snd_dbg
                g2_fifo=(uint*)(biosmem_b+ 0x5F688C); // G2 FIFO
                
                #region interups
				ACK_A = (uint*)(biosmem_b + 0x5F6900); // Pending Interrupts 1
				ACK_B= (uint*)(biosmem_b + 0x5f6904); // Pending Interrupts 2
				ACK_C = (uint*)(biosmem_b + 0x5f6908); // Pending Interrupts 3
                IRQD_A=(uint*)(biosmem_b+ 0x5f6910); // Enabled Interrupts IRQD_A
                IRQD_B=(uint*)(biosmem_b+ 0x5f6914); // Enabled Interrupts IRQD_B
                IRQD_C=(uint*)(biosmem_b+ 0x5f6918); // Enabled Interrupts IRQD_C
                IRQB_A=(uint*)(biosmem_b+ 0x5f6920); // Enabled Interrupts IRQB_A
                IRQB_B=(uint*)(biosmem_b+ 0x5f6924); // Enabled Interrupts IRQB_B
                IRQB_C=(uint*)(biosmem_b+ 0x5f6928); // Enabled Interrupts IRQB_C
                IRQ9_A=(uint*)(biosmem_b+ 0x5f6930); // Enabled Interrupts IRQ9_A
                IRQ9_B=(uint*)(biosmem_b+ 0x5f6934); // Enabled Interrupts IRQ9_B
                IRQ9_C=(uint*)(biosmem_b+ 0x5f6938); // Enabled Interrupts IRQ9_C
                SB_PDTNRM=(uint*)(biosmem_b+ 0x5f6940); // SB_PDTNRM	PVR-DMA trigger select from normal interrupt
                SB_PDTEXT=(uint*)(biosmem_b+ 0x5f6944); // SB_PDTEXT	PVR-DMA trigger select from external interrupt
                #endregion

                #region mapple
                MAPLE_DMAADDR=(uint*)&biosmem_b[0x5f6c04]; // MAPLE_DMAADDR
                MAPLE_RESET2=(uint*)(biosmem_b+ 0x5f6c10); // MAPLE_RESET2
                MAPLE_ENABLE=(uint*)(biosmem_b+ 0x5f6c14); // MAPLE_ENABLE
                MAPLE_STATE=(uint*)(biosmem_b+ 0x5f6c18); // MAPLE_STATE
                MAPLE_SPEED=(uint*)(biosmem_b+ 0x5f6c80); // MAPLE_SPEED
                MAPLE_RESET1=(uint*)(biosmem_b+ 0x5f6c8c); // MAPLE_RESET1
                #endregion 

                #region  PVR Registers
                // PVR Registers
                
                COREID=(uint*)(biosmem_b+ 0x5f8000 + 0x00 * 4);// COREID [ID] (PVR2 Core ID)
                CORETYPE=(uint*)(biosmem_b+ 0x5f8000 + 0x01 * 4);// CORETYPE [REV] (PVR2 Core version)
                COREDISABLE=(uint*)(biosmem_b+ 0x5f8000 + 0x02 * 4);//COREDISABLE [CORERESET] (PVR2) (Enable/disable the submodules of the PVR2 Core)
                RENDERFORMAT=(uint*)(biosmem_b+  0x5f8000 + 0x03 * 4); // RENDERFORMAT, "alpha config"
                RENDERSTART=(uint*)(biosmem_b+ 0x5f8000 + 0x05 * 4);//RENDERSTART (3D) (Start render strobe)
                TESTSELECT=(uint*)(biosmem_b+ 0x5f8000 + 0x06 * 4);// [TESTSELECT]
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x07 * 4);// Unknown
                PARAM_BASE=(uint*)(biosmem_b+ 0x5f8000 + 0x08 * 4);// PRIMALLOCBASE [PARAM_BASE] (3D) (Primitive allocation base)
                SPANSORTCFG=(uint*)(biosmem_b+ 0x5f8000 + 0x09 * 4);// [SPANSORTCFG]
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x0a * 4);// ??
                TILEARRAY=(uint*)(biosmem_b+ 0x5f8000 + 0x0b * 4);// TILEARRAY (Tile Array base address) [REGION_BASE]
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x0c * 4);// Unknown
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x0d * 4);// Unknown
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x0e * 4);// Unknown
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x0f * 4);// Unknown
                BORDERCOLOR=(uint*)(biosmem_b+ 0x5f8000 + 0x10 * 4);// BORDERCOLOR (2D) (Border colour)
                BITMAPTYPE=(uint*)(biosmem_b+ 0x5f8000 + 0x11 * 4);		// BITMAPTYPE (bitmap display settings)
                FB_W_CTRL = (uint*)(biosmem_b + 0x5f8000 + 0x12 * 4);		// RENDERFORMAT [FB_W_CTRL] (3D) (Render output format)
                FB_W_LINESTRIDE = (uint*)(biosmem_b + 0x5f8000 + 0x13 * 4);		// RENDERPITCH [FB_W_LINESTRIDE] (3D) (Render target pitch)
                FB_R_SOF1 = (uint*)(biosmem_b + 0x5f8000 + 0x14 * 4);		// FRAMEBUF [FB_R_SOF1] (2D) (Framebuffer address)
                FB_R_SOF2 = (uint*)(biosmem_b + 0x5f8000 + 0x15 * 4);		// FRAMEBUF [FB_R_SOF2] (2D) (Framebuffer address, short field)
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x16 * 4);		// Unknown
                DIWSIZE=(uint*)(biosmem_b+ 0x5f8000 + 0x17 * 4);		// DIWSIZE (2D) (Display window size)
                FB_W_SOF1 = (uint*)(biosmem_b + 0x5f8000 + 0x18 * 4);		//RENDERBASE [FB_W_SOF1](3D) (Render target base address)
                FB_W_SOF2=(uint*)(biosmem_b+ 0x5f8000 + 0x19 * 4);		//[FB_W_SOF2]
                FB_X_CLIP = (uint*)(biosmem_b + 0x5f8000 + 0x1a * 4);		//RENDERWINDOWX [FB_X_CLIP] (3D) (Render output window X-start and X-stop)
                FB_Y_CLIP = (uint*)(biosmem_b + 0x5f8000 + 0x1b * 4);		//RENDERWINDOWY [FB_Y_CLIP] (3D) (Render output window Y-start and Y-stop)
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x1c * 4);		//Unknown
                FPU_SHAD_SCALE = (uint*)(biosmem_b + 0x5f8000 + 0x1d * 4);		//CHEAPSHADOWS [FPU_SHAD_SCALE] (3D) (Cheap shadow enable + strength)
                FPU_CULL_VAL = (uint*)(biosmem_b + 0x5f8000 + 0x1e * 4);		//CULLINGVALUE [FPU_CULL_VAL] (3D) (Minimum allowed polygon area)
                FPU_PARAM_CFG=(uint*)(biosmem_b+ 0x5f8000 + 0x1f * 4);		//[FPU_PARAM_CFG] (3D) (Something to do with rendering)
                HALF_OFFSET=(uint*)(biosmem_b+ 0x5f8000 + 0x20 * 4);		//[HALF_OFFSET]
                FPU_PERP_VAL=(uint*)(biosmem_b+ 0x5f8000 + 0x21 * 4);		//[FPU_PERP_VAL] (3D) (Something to do with rendering)
                ISP_BACKGND_D=(uint*)(biosmem_b+ 0x5f8000 + 0x22 * 4);		//[ISP_BACKGND_D]
                ISP_BACKGND_T=(uint*)(biosmem_b+ 0x5f8000 + 0x23 * 4);		//BGPLANE [ISP_BACKGND_T] (3D) (Background plane location)   
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x24 * 4);		// Unknown
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x25 * 4);     // Unknown
                ISP_FEED_CFG=(uint*)(biosmem_b+ 0x5f8000 + 0x26 * 4);     // [ISP_FEED_CFG]
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x27 * 4);     // Unknown
                SDRAM_REFRESH=(uint*)(biosmem_b+ 0x5f8000 + 0x28 * 4);		// [SDRAM_REFRESH]
                SDRAM_ARB_CFG=(uint*)(biosmem_b+ 0x5f8000 + 0x29 * 4);		// [SDRAM_ARB_CFG]
                SDRAM_CFG=(uint*)(biosmem_b+ 0x5f8000 + 0x2a * 4);		// SDRAM_CFG (PVR) (Graphics memory control)
                //=(uint*)(biosmem_b+ 0x5f8000 + 0x2b * 4);		// Unknown
                FOG_COL_RAM = (uint*)(biosmem_b + 0x5f8000 + 0x2c * 4);		// FOGTABLECOLOR [FOG_COL_RAM] (3D) (Fogging colour when using table fog)
                FOG_COL_VERT=(uint*)(biosmem_b+ 0x5f8000 + 0x2d * 4);		// [FOG_COL_VERT]
                FOG_DENSITY=(uint*)(biosmem_b+ 0x5f8000 + 0x2e * 4);		// [FOG_DENSITY]
                FOG_CLAMP_MAX=(uint*)(biosmem_b+ 0x5f8000 + 0x2f * 4);		//[FOG_CLAMP_MAX]
                FOG_CLAMP_MIN=(uint*)(biosmem_b+ 0x5f8000 + 0x30 * 4);		// [FOG_CLAMP_MIN]
                SPG_TRIGGER_POS=(uint*)(biosmem_b+ 0x5f8000 + 0x31 * 4);		// [SPG_TRIGGER_POS]
                SPG_HBLANK_INT=(uint*)(biosmem_b+ (0xa05f8000 + 0x32 * 4)); // 0xa05f80c8, SPG_HBLANK_INT
                SPG_VBLANK_INT=(uint*)(biosmem_b+ (0xa05f8000 + 0x33 * 4)); // 0xa05f80cc, SPG_VBLANK_INT
                SPG_CONTROL=(uint*)(biosmem_b+ (0xa05f8000 + 0x34 * 4)); // 0xa05f80d0, SPG_CONTROL
                SPG_HBLANK=(uint*)(biosmem_b+ 0x5f8000 + 0x35 * 4); // 0xa05f80d4, SPG_HBLANK
                SPG_LOAD=(uint*)(biosmem_b+ 0x5f8000 + 0x36 * 4); // 0xa05f80d8, SPG_LOAD
                SPG_WIDTH=(uint*)(biosmem_b+ 0x5f8000 + 0x39 * 4); // TEXTURESTRIDE [SPG_WIDTH] (3D) (Width of rectangular texture)
                TEXT_CONTROL=(uint*)(biosmem_b+ 0x5f8000 + 0x3a * 4); // [TEXT_CONTROL]
                VO_CONTROL=(uint*)(biosmem_b+ 0x5f8000 + 0x3b * 4); // [VO_CONTROL]
                VO_STARTX=(uint*)(biosmem_b+ 0x5f8000 + 0x3c * 4); // [VO_STARTX]
                VO_STARTY=(uint*)(biosmem_b+ 0x5f8000 + 0x3d * 4); // [VO_STARTY]
                SCALER_CTL=(uint*)(biosmem_b+ 0x5f8000 + 0x3e * 4); // [SCALER_CTL]
                PAL_RAM_CTRL=(uint*)(biosmem_b+ 0x5f8000 + 0x42 * 4); // [PAL_RAM_CTRL]
                SYNC_STAT=(uint*)(biosmem_b+ 0x5f8000 + 0x43 * 4); // [SYNC_STAT]
                FB_BURSTCTRL=(uint*)(biosmem_b+ 0x5f8000 + 0x44 * 4); // [FB_BURSTCTRL]
                FB_C_SOF=(uint*)(biosmem_b+ 0x5f8000 + 0x45 * 4); // [FB_C_SOF]
                Y_COEFF=(uint*)(biosmem_b+ 0x5f8000 + 0x46 * 4); // [Y_COEFF]
                PT_ALPHA_REF=(uint*)(biosmem_b+ 0x5f8000 + 0x47 * 4); //[PT_ALPHA_REF]
                TA_OL_BASE = (uint*)(biosmem_b + 0x5f8000 + 0x49 * 4); // PPMATRIXBASE [TA_OL_BASE] (TA) (Root PP-block matrices base address)
                TA_ISP_BASE = (uint*)(biosmem_b + 0x5f8000 + 0x4a * 4); // PRIMALLOCSTART [TA_ISP_BASE] (TA) (Primitive allocation area start)
                TA_OL_LIMIT = (uint*)(biosmem_b + 0x5f8000 + 0x4b * 4); // PPALLOCSTART [TA_OL_LIMIT] (TA) (PP-block allocation area start)
                TA_NEXT_OPB = (uint*)(biosmem_b + 0x5f8000 + 0x4c * 4); // PRIMALLOCEND [TA_ISP_LIMIT] (TA) (Primitive allocation area end)
                TA_NEXT_OPB = (uint*)(biosmem_b + 0x5f8000 + 0x4d * 4); // PPALLOCPOS [TA_NEXT_OPB] (TA) (Current PP-block allocation position)
                TA_ITP_CURRENT = (uint*)(biosmem_b + 0x5f8000 + 0x4e * 4); // PRIMALLOCPOS [TA_ITP_CURRENT] (TA) (Current primitive allocation position)
                TA_GLOB_TILE_CLIP = (uint*)(biosmem_b + 0x5f8000 + 0x4f * 4); // TILEARRAYSIZE [TA_GLOB_TILE_CLIP] (TA) (Tile Array dimensions)
                TA_ALLOC_CTRL = (uint*)(biosmem_b + 0x5f8000 + 0x50 * 4); // PPBLOCKSIZE [TA_ALLOC_CTRL] (TA) (PP-block sizes)
                TA_LIST_INIT = (uint*)(biosmem_b + 0x5f8000 + 0x51 * 4); // TASTART [TA_LIST_INIT] (TA) (Start vertex enqueueing strobe)
                TA_YUV_TEX_BASE=(uint*)(biosmem_b+ 0x5f8000 + 0x52 * 4); // [TA_YUV_TEX_BASE]
                TA_YUV_TEX_CTRL=(uint*)(biosmem_b+ 0x5f8000 + 0x53 * 4); // [TA_YUV_TEX_CTRL]
                TA_YUV_TEX_CNT=(uint*)(biosmem_b+ 0x5f8000 + 0x54 * 4); // [TA_YUV_TEX_CNT]
                TA_LIST_CONT=(uint*)(biosmem_b+ 0x5f8000 + 0x58 * 4); // [TA_LIST_CONT]
                TA_NEXT_OPB_INIT = (uint*)(biosmem_b + 0x5f8000 + 0x59 * 4); // PPALLOCEND [TA_NEXT_OPB_INIT] (TA) (PP-block allocation area end)
                #endregion

                dc_rtc1=(uint*)(biosmem_b+ 0x710000); // Dreamcast RTC, reg 1
                dc_rtc2=(uint*)(biosmem_b+ 0x710004); // Dreamcast RTC, reg 2

            #endregion

			InitBios();//hle and shit
        }
		#region not used any more
		// read/write to system ram -  not used anymore 
		/*
        public static unsafe uint readsys(uint adr,int len)
		{
			switch (len)
			{
				case 1:
					return ram[adr];
				case 2:
					fixed(byte *p=&ram[adr])
						return *(ushort*)p;
				case 4:
					fixed(byte *p=&ram[adr])
						return *(uint*)p;
			}
			return 0;
		}
		public static unsafe void writesys(uint adr,uint data,int len)
		{
			switch (len)
			{
				case 1:
					ram[adr]=(byte)data;
					return;
				case 2:
					fixed(byte *p=&ram[adr])
						*(ushort*)p=(ushort)data;
					return;
				case 4:
					fixed(byte *p=&ram[adr])
						*(uint*)p=data;
					return;
			}
		}
		*/
		#endregion
		public static bool stop_textout = false;
        static void WriteLine(string ln)
        {
#if !optimised_b
			if (!stop_textout)
				dc.dcon.WriteLine(ln);
#endif
        }

        static void WriteLinehw(string ln)
        {
#if !optimised_b
			if (!stop_textout)
				dc.dcon.WriteLine(ln);
#endif
		}
    }
}
