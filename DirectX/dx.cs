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
#if nrt
using didr = Microsoft.DirectX.DirectDraw;
#endif
namespace DC4Ever
{
	/// <summary>
	/// Interfaces with managed DirectX
	/// </summary>
	public static class dx
	{
        #if nrt
		public static Microsoft.DirectX.DirectDraw.Device dddev; 
		public static Microsoft.DirectX.DirectDraw.Surface fb;//frontbuffer
		public static Microsoft.DirectX.DirectDraw.Surface bb;//backbuffer
		public static void init(System.Windows.Forms.Control targ)
		{
			dddev = new Microsoft.DirectX.DirectDraw.Device();
			dddev.SetCooperativeLevel(targ,Microsoft.DirectX.DirectDraw.CooperativeLevelFlags.Normal);
			didr.SurfaceCaps caps=new Microsoft.DirectX.DirectDraw.SurfaceCaps();
			caps.PrimarySurface=true;
			didr.SurfaceDescription desc=new Microsoft.DirectX.DirectDraw.SurfaceDescription(caps);
			fb=new didr.Surface(desc,dddev);
            fb.Clipper = new Microsoft.DirectX.DirectDraw.Clipper(dddev);
            fb.Clipper.Window = targ;
            caps.PrimarySurface=false;
			desc.Width = 640;
			desc.Height = 480;
			//desc.PixelFormatStructure.RgbBitCount= 16;
			bb=new didr.Surface(desc,dddev);
			bb.ColorFill (0);
            emu.bitinfo = new DC4Ever.emu.BITMAPINFOHEADER();
            emu.bitinfo.biSize = 40;
            emu.bitinfo.biWidth = 640;
            emu.bitinfo.biHeight = -480;
            emu.bitinfo.biPlanes = 1;
            emu.bitinfo.biBitCount = 16;
            emu.bitinfo.biSizeImage = 640 * 480 * 2;
        }
        #endif
    }
}
