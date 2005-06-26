
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
using System.Runtime.InteropServices;
using Tao.OpenGl;
using Tao.Sdl;
using SdlDotNet;

namespace DC4Ever
{
	/// <summary>
	/// Summary description for 
	/// </summary>
    public unsafe class pvr
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
		public static bool Is3DOn = false;
        public static uint mw=0;
        public static uint f;
		public static uint fps;
		static int clc_pvr_frame = 0, clc_pvr_scanline=0;
		public static uint prv_cur_scanline = 0;
		public static int clc_pvr_renderdone = 0;
		//public static byte[] vram= new byte[8*mb];
        public static byte* vram_b = (byte*)dc.mmgr.AllocMem(8 * mem.mb);
        static ushort* vram_w = (ushort*)vram_b;
        static uint* vram_dw = (uint*)vram_b;
        public static unsafe void writePvr(uint adr,uint data,int len)
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
				dc.dcon.WriteLine("Address read out of Vram on write (pc="+sh4.pc+")");
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
			dc.dcon.WriteLine("Wrong write size in write (" + len+") at pc "+sh4.pc);
		}
		public static unsafe uint readPvr(uint adr, int len)
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
				dc.dcon.WriteLine("Address read out of Vram on read (pc="+sh4.pc+")");
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
			dc.dcon.WriteLine("Wrong read size in read (" + len+") at pc "+sh4.pc);
			return 0;
		}
		public static unsafe  void present()// draw the framebuffer(640*480*16 bit)
		{
           #if nrt
                System.IntPtr hdc = dx.bb.GetDc();
                fixed (BITMAPINFOHEADER* bi = &bitinfo)
                    StretchDIBits(hdc, 0, 0, 640, 480, 0, 0, 640, 480,vram_b+*mem.FB_R_SOF1 , bi, 0, 13369376);
                dx.bb.ReleaseDc(hdc);
                dx.fb.Draw(new Rectangle(dc.frmMain.PointToScreen(new Point(dc.frmMain.ClientRectangle.X + 8, dc.frmMain.ClientRectangle.Y + 8))
                    , new Size(dc.frmMain.screen.Width, dc.frmMain.screen.Height)), dx.bb, Microsoft.DirectX.DirectDraw.DrawFlags.DoNotWait | Microsoft.DirectX.DirectDraw.DrawFlags.Async);
            #endif
            fps+=1;
		}
        public static void UpdatePvr(uint cycles)
        {
			clc_pvr_frame += (int)cycles;    //cycle count  #2
			
			if (clc_pvr_frame > (3495253))//60 ~herz = 200 mhz / 60=3495253 cycles per screen refresh
			{
//#if !zezuExt
				//ok .. here , after much effort , we reached a full screen redraw :P
				//now , we will copy everything onto the screen (meh) and raise a vblank interupt
				intc.RaiseInterupt(sh4_int.holly_VBLank);//weeeee
				//zezu_pvr.PvrUpdate(2);
				if (!Is3DOn)
					present();

				ta.DoEvents();
				//zezu_pvr.PvrUpdate(0xFFFFFF);
				//zezu_pvr.PvrUpdate(zezu_pvr.
//#else
//				//zezu_pvr
//#endif
				clc_pvr_frame -= 3495253;
			}

			clc_pvr_scanline += (int)cycles;
			if (clc_pvr_scanline > (3495253 / 480))//60 ~herz = 200 mhz / 60=3495253 cycles per screen refresh
			{
				//ok .. here , after much effort , we reached a full screen redraw :P
				//now , we will copy everything onto the screen (meh) and raise a vblank interupt
				prv_cur_scanline=(prv_cur_scanline+1)%480;

				uint data = *mem.SPG_VBLANK_INT;
				if ((data & 0x3FFF) == prv_cur_scanline)
					intc.RaiseInterupt(sh4_int.holly_SCANINT1);
				if (((data >> 16) & 0x3FFF) == prv_cur_scanline)
					intc.RaiseInterupt(sh4_int.holly_SCANINT2);
				
				clc_pvr_scanline -= (3495253 / 480);
			}

			if (clc_pvr_renderdone > 0)
			{
				clc_pvr_renderdone -= (int)cycles;
				if (clc_pvr_renderdone <= 0)
				{
					//render done interupt :P
					//I MUST FIX THAT .. SOMEDAY
#if!zezuExt
					if (ta.curListheader.vertex_count != 0)
					{
						ta.curListheader.vertex_count = 0;
						Gl.glEnd();
					}
					Video.GLSwapBuffers();

					Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

					ta.DoEvents();
#endif
				}
			}
			//else
			//{
			//	clc_pvr_renderdone =10000000;
			//}
		}

		public static uint HblankInfo()
		{
			return 0;
		}
		
		public static uint VblankInfo()
		{
			uint data = *mem.SPG_VBLANK_INT;
			if (((data & 0x3FFF) <= prv_cur_scanline) && (((data >> 16) & 0x3FFF) >= prv_cur_scanline))
				return 1;
			else
				return 0;
		}
    }
}
