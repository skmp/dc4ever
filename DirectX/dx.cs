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

using didr = Microsoft.DirectX.DirectDraw;

namespace DC4Ever
{
	/// <summary>
	/// Interfaces with managed DirectX
	/// </summary>
	public class dx
	{
        
		public static Microsoft.DirectX.DirectDraw.Device dddev; 
		public static Microsoft.DirectX.DirectDraw.Surface fb;//frontbuffer
		public static Microsoft.DirectX.DirectDraw.Surface bb;//backbuffer
		public static void init(System.Windows.Forms.Control targ)
		{
			//#if!zezuExt
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
//#endif
            pvr.bitinfo = new DC4Ever.pvr.BITMAPINFOHEADER();
            pvr.bitinfo.biSize = 40;
            pvr.bitinfo.biWidth = 640;
            pvr.bitinfo.biHeight = -480;
            pvr.bitinfo.biPlanes = 1;
            pvr.bitinfo.biBitCount = 16;
            pvr.bitinfo.biSizeImage = 640 * 480 * 2;
        }
        
    }
}
