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
			dddev = new Microsoft.DirectX.DirectDraw.Device();
			dddev.SetCooperativeLevel(targ,Microsoft.DirectX.DirectDraw.CooperativeLevelFlags.Normal);
			didr.SurfaceCaps caps=new Microsoft.DirectX.DirectDraw.SurfaceCaps();
			caps.PrimarySurface=true;
			didr.SurfaceDescription desc=new Microsoft.DirectX.DirectDraw.SurfaceDescription(caps);
			fb=new didr.Surface(desc,dddev);
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

	}
}
