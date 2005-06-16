
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
using System.Drawing;
#if nrt
using System.Runtime.InteropServices;
#endif
using Tao.OpenGl;
using Tao.Sdl;
using SdlDotNet;

namespace DC4Ever
{
	/// <summary>
	/// Summary description for 
	/// </summary>
    public unsafe static partial  class emu
    {

		#region WinApi
		public struct BITMAPINFOHEADER
		{
			public int biSize;
			public int biWidth;
			public int biHeight;
			public short biPlanes;
			public short biBitCount;
			public int biCompression;
			public int biSizeImage;
			public int biXPelsPerMeter;
			public int biYPelsPerMeter;
			public int biClrUsed;
			public int biClrImportant;
		}
		[DllImport("gdi32.dll")]
		static unsafe extern int StretchDIBits ( 
			System.IntPtr hdc,
			int x,
			int y,
			int dx,
			int dy,
			int SrcX,
			int SrcY,
			int wSrcWidth,
			int wSrcHeight,
			byte* lpBits,
			BITMAPINFOHEADER* lpBitsInfo,
			int wUsage,
			int dwRop);
		public static BITMAPINFOHEADER bitinfo;
		#endregion
        public static uint mw=0;
        public static uint f;
		public static uint fps;
		static int clc_pvr_frame = 0;
		public static int clc_pvr_renderdone = 0;
		//public static byte[] vram= new byte[8*mb];
        static byte* vram_b = (byte*)dc.mmgr.AllocMem(8 * mb);
        static ushort* vram_w = (ushort*)vram_b;
        static uint* vram_dw = (uint*)vram_b;
        static unsafe void writePvr(uint adr,uint data,int len)
		{
            mw += (uint)len;
			#region Address translation
			if (adr<0x800000)//(adr>>24)&0x1//using 64 bit interface
			{
				//Translate address to offset
				//if bit 2(0x4) is set then read from rambank2(4mb+>)
				//get rid of bit 2(0x4) and >> by 1 to fix the pos 
				//01111111111111111111100->0x3FFFFC
				//00000000000000000000011->0x3
				adr=((adr>>1)&0x3FFFFC)+(adr&0x3)+0x3FFFFF*((adr>>2)&0x1);
			}
			else if ((adr > 0xFFFFFF)&& (adr<0x1800000))//using 32 bit interface
			{
				adr=adr-0x1000000;//translate to vram offset
			}
			else 
			{
				dc.dcon.WriteLine("Address read out of Vram on write (pc="+pc+")");
				return;
			}
			#endregion
            switch (len)
			{
				case 0x1://1 byte write
					vram_b[adr]=(byte)data;
					return;
				case 0x2://2 byte write
					//fixed(byte *p=&vram[adr])
					//	*(ushort*)p=(ushort)data;
                    vram_w[adr >> 1] = (ushort)data;
					//dc.dbger.mode = 1;
                    return;
				case 0x4://4 byte write
					//fixed(byte *p=&vram[adr])
					//	*(uint*)p=data;
                    vram_dw[adr >> 2] = data;
                    return; 
			}
			dc.dcon.WriteLine("Wrong write size in write (" + len+") at pc "+pc);
		}
		static unsafe uint readPvr(uint adr, int len)
		{
			#region Address translation
			if ((adr > 0xFFFFFF)&& (adr<0x1800000))//using 32 bit interface
			{
				adr=adr-0x1000000;//translate to vram offset
			}
			else if (adr<0x800000)//(adr>>24)&0x1//using 64 bit interface
			{
				//Translate address to offset
				//if bit 2(0x4) is set then read from rambank2(4mb+>)
				//get rid of bit 2(0x4) and >> by 1 to fix the pos 
				//01111111111111111111100->0x3FFFFC
				//00000000000000000000011->0x3
					adr=((adr>>1)&0x3FFFFC)+(adr&0x3)+0x3FFFFF*((adr>>2)&0x1);
			}
			else 
			{
				dc.dcon.WriteLine("Address read out of Vram on read (pc="+pc+")");
				return 0;
			}
			#endregion
			switch (len)
			{
				case 0x1://1 byte read
						return vram_b[adr];
				case 0x2://2 byte read
					//fixed(byte *p=&vram[adr])
					//	return *(ushort*)p;
                    return vram_w[adr>>1];
                case 0x4://4 byte read
					//fixed(byte *p=&vram[adr])
					//	return *(uint*)p;
                    return vram_dw[adr>>2];
            }
			dc.dcon.WriteLine("Wrong read size in read (" + len+") at pc "+pc);
			return 0;
		}
		static unsafe  void present()// draw the framebuffer(640*480*16 bit)
		{
            #if nrt
                System.IntPtr hdc = dx.bb.GetDc();
                fixed (BITMAPINFOHEADER* bi = &bitinfo)
                    StretchDIBits(hdc, 0, 0, 640, 480, 0, 0, 640, 480,vram_b+*FB_R_SOF1 , bi, 0, 13369376);
                dx.bb.ReleaseDc(hdc);
                dx.fb.Draw(new Rectangle(dc.frmMain.PointToScreen(new Point(dc.frmMain.ClientRectangle.X + 8, dc.frmMain.ClientRectangle.Y + 8))
                    , new Size(dc.frmMain.screen.Width, dc.frmMain.screen.Height)), dx.bb, Microsoft.DirectX.DirectDraw.DrawFlags.DoNotWait | Microsoft.DirectX.DirectDraw.DrawFlags.Async);
            #endif
            fps+=1;
		}
        static void UpdatePvr(uint cycles)
        {
			clc_pvr_frame += (int)cycles;    //cycle count  #2
			if (clc_pvr_frame > (3495253))//60 ~herz = 200 mhz / 60=3495253 cycles per screen refresh
			{
				//ok .. here , after much effort , we reached a full screen redraw :P
				//now , we will copy everything onto the screen (meh) and raise a vblank interupt
				RaiseInterupt(sh4_int.VBLank);//weeeee
				present();
				DoEvents();
				clc_pvr_frame -= 3495253;
			}

			if (clc_pvr_renderdone > 0)
			{
				clc_pvr_renderdone -= (int)cycles;
				if (clc_pvr_renderdone <= 0)
				{
					//render done interupt :P
					RaiseInterupt(sh4_int.RENDER_DONE);
					//I MUST FIX THAT .. SOMEDAY
					if ((pvr_registered & (1 << 0))!=0)
						RaiseInterupt(sh4_int.OPAQUE);	// ASIC_EVT_PVR_OPAQUEDONE

					if ((pvr_registered & (1 << 1)) != 0)
						RaiseInterupt(sh4_int.OPAQUEMOD); 	// ASIC_EVT_PVR_OPAQUEMODDONE

					if ((pvr_registered & (1 << 2)) != 0)
						RaiseInterupt(sh4_int.TRANS); 	// ASIC_EVT_PVR_TRANSDONE

					if ((pvr_registered & (1 << 3)) != 0)
						RaiseInterupt(sh4_int.TRANSMOD);	// ASIC_EVT_PVR_TRANSMODDONE

					if ((pvr_registered & (1 << 4)) != 0)
						RaiseInterupt(sh4_int.PUNCHTHRU); 	// ASIC_EVT_PVR_PTDONE

					Video.GLSwapBuffers();
					//*ACK_A |= 0x80; // end of rendering

					Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
					DoEvents();
				}
			}
		}
    }
}
