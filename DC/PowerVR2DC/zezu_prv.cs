using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Softec;
using System.Reflection;
using System.Reflection.Emit;

namespace DC4Ever
{
	

	unsafe delegate void NullCallBack();

	unsafe delegate void interCB(uint type);
	unsafe delegate void Save_ConfigCB(byte* RegStr, uint Config);
	unsafe delegate void Load_ConfigCB(byte* RegStr, uint* Config);
	unsafe delegate void Dbg_outCallBack(uint dwDebugFlags, byte* szFormat);
	unsafe delegate void CPU_HaltCB(byte * szReason);

	static unsafe class zezu_pvr
	{

		/* Plugin types */
		public const ushort PLUGIN_TYPE_GFX = 1;
		public const ushort PLUGIN_TYLE_LLE = 0x1000;
		public const ushort PLUGIN_TYPE_PVR = 0x1001;

		//#define EXPORT						__declspec(dllexport)
		//#define CALL						_cdecl

		/* Interrupt defines */
		public const ushort RENDER_VIDEO = 1;
		public const ushort OPAQUE_LIST = 6;
		public const ushort OPAQUE_MOD_LIST = 7;
		public const ushort TRANS_LIST = 8;
		public const ushort TRANS_MOD_LIST = 9;
		public const ushort PUNCH_THRU_LIST = 18;

		/* Debug */
		public const uint DEBUG_DP_GFX = (1 << 24);

		/***** Structures *****/
		[StructLayout(LayoutKind.Sequential)]
		public struct PLUGIN_INFO
		{
			public ushort Version;        /* Should be set to 1 */
			public ushort Type;           /* Set to PLUGIN_TYPE_GFX */
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
			public string name;//char[100]      /* Name of the DLL */
		};


		/********** PowerVR2 Plugin Specific **
****************************************/

		[StructLayout(LayoutKind.Sequential)]
		public struct GFX_INFO
		{
			public IntPtr hWnd;			// Render window 
			public IntPtr hStatusBar;    // if render window has no status bar, this is NULL 

			public byte* VRAM;			// Access to 32-bit video ram (0xa5000000 following) 
			public uint* PVR_REGS;		// Access to PVR registers (0xa05f8000 following) 

			//void (*Enqueue_IRQ)		(uint type);
			public interCB Enqueue_IRQ;
			
			//void (*Debug_Printf)	(uint dwDebugFlags, byte* szFormat, ... );
			public Dbg_outCallBack /*Dbg_outCallBack*/ Debug_Printf;

			//void (*CPU_Halt)		(byte * szReason);
			public CPU_HaltCB CPU_Halt;

			// Silly stats for debug console 
			//void (*StatsFrame)		();
			public NullCallBack StatsFrame;

			//void (*StatsVtxStrip)	();
			public NullCallBack StatsVtxStrip;

			// This can be used to easily save/load a 32-bit config struct for each gfx plugin 
			//void (*Save_Config)		(byte *RegStr, uint Config);
			public Save_ConfigCB Save_Config;
			//void (*Load_Config)		(byte *RegStr, uint *Config);
			public Load_ConfigCB Load_Config;

		}

		[StructLayout(LayoutKind.Sequential)]
		public struct GFX_INFO_st
		{
			public IntPtr hWnd;			// Render window 
			public IntPtr hStatusBar;    // if render window has no status bar, this is NULL 

			public byte* VRAM;			// Access to 32-bit video ram (0xa5000000 following) 
			public uint* PVR_REGS;		// Access to PVR registers (0xa05f8000 following) 

			//void (*Enqueue_IRQ)		(uint type);
			public uint Enqueue_IRQ;

			//void (*Debug_Printf)	(uint dwDebugFlags, byte* szFormat, ... );
			public uint /*Dbg_outCallBack*/ Debug_Printf;

			//void (*CPU_Halt)		(byte * szReason);
			public uint CPU_Halt;

			// Silly stats for debug console 
			//void (*StatsFrame)		();
			public uint StatsFrame;

			//void (*StatsVtxStrip)	();
			public uint StatsVtxStrip;

			// This can be used to easily save/load a 32-bit config struct for each gfx plugin 
			//void (*Save_Config)		(byte *RegStr, uint Config);
			public uint Save_Config;
			//void (*Load_Config)		(byte *RegStr, uint *Config);
			public uint Load_Config;

		}
		
		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern bool PvrInit(GFX_INFO_st GfxInfo);	// InitiateGFX
		
		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void PvrTerm();	// GFXCloseDLL
		
		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void PvrOpen();	// GfxGameOpen
		
		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void PvrClose();	// GfxGameClosed

		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void PvrCommand(uint* pCmd, uint dwQW_Len);	// ProcessDList

		//EXPORT void (__cdecl *PvrRead)	( DWORD dwAddr );
		//EXPORT void (__cdecl *PvrWrite8)	( DWORD dwAddr, DWORD dwValue );
		//EXPORT void (__cdecl *PvrWrite16)	( DWORD dwAddr, DWORD dwValue );
		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void PvrWrite32(uint dwAddr, ref uint dwValue);

		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void PvrUpdate(uint dwReason);	// SizeScreen | DrawScreen(FB) etc ..

		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void PvrDllTest(IntPtr hParent);	// GFXDllTest

		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void PvrDllAbout(IntPtr hParent);	// GFXDllAbout

		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void PvrDllConfig(IntPtr hParent);	// GFXDllConfig

		[DllImport("IcGfxDX.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void GetDllInfo(ref PLUGIN_INFO PluginInfo);

		[DllImport("icPl.dll", CallingConvention = CallingConvention.Cdecl)]
		[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
		public static extern void Redirect(ref GFX_INFO_st newi, GFX_INFO oldi);

		public static void FrameDone()
		{
			emu.WriteLine("DLL:FrameDone");
			//emu.fps++;
		}

		public static void VtxStripDone()
		{
			emu.WriteLine("DLL:VtxStrip");
			//emu.fps++;
		}


		public static void Save_Config(byte* RegStr, uint Config)
		{
			emu.WriteLine("DLL:Save_Config");
		}

		public static void Load_Config(byte* RegStr, uint* Config)
		{
			emu.WriteLine("DLL:Load_Config");
		}

		public static void InteruptHandler(uint type)
		{
			switch (type)
			{
				case  RENDER_VIDEO:
					emu.RaiseInterupt(emu.sh4_int.holly_RENDER_DONE);
					emu.Is3DOn = true;
					emu.fps++;
					emu.DoEvents();
					break;
				case OPAQUE_LIST :
					emu.RaiseInterupt(emu.sh4_int.holly_OPAQUE);
					break;
				case OPAQUE_MOD_LIST :
					emu.RaiseInterupt(emu.sh4_int.holly_OPAQUEMOD);
					break;
				case TRANS_LIST :
					emu.RaiseInterupt(emu.sh4_int.holly_TRANS);
					break;
				case TRANS_MOD_LIST :
					emu.RaiseInterupt(emu.sh4_int.holly_TRANSMOD);
					break;
				case PUNCH_THRU_LIST :
					emu.RaiseInterupt(emu.sh4_int.holly_PUNCHTHRU);
					break;
				default:
					dc.dcon.WriteLine("DLL:InteruptHandler");
					break;
			}
		}

		public static void CPU_Halt(byte * szReason)
		{
			string t = Marshal.PtrToStringAnsi(new IntPtr(szReason));
			dc.dcon.WriteLine("DLL:CPU HALT" + t);
		}

		public static void LogHandler(uint dwDebugFlags, byte* szFormat)//(uint dwDebugFlags, byte* szFormat)
		{
			string t = Marshal.PtrToStringAnsi(new IntPtr(szFormat));
			dc.dcon.WriteLine("DLL:" + t);
		}

		static PLUGIN_INFO plinfo = new PLUGIN_INFO();
		static GFX_INFO ginfo = new GFX_INFO();
		static GFX_INFO_st ginfo_2 = new GFX_INFO_st();

		public static void PvrInit()
		{
			GetDllInfo(ref plinfo);


			ginfo.hWnd = dc.frmMain.screen.Handle;
			ginfo.PVR_REGS = (uint*)&emu.biosmem_b[0x5f8000];
			ginfo.VRAM = emu.vram_b;

			ginfo.StatsFrame=FrameDone;
			ginfo.StatsVtxStrip=VtxStripDone;
			ginfo.Save_Config=Save_Config;

			ginfo.Load_Config=Load_Config;
			ginfo.CPU_Halt=CPU_Halt;
			ginfo.Debug_Printf = LogHandler;
			ginfo.Enqueue_IRQ = InteruptHandler;
			

			Redirect(ref ginfo_2, ginfo);


			PvrInit(ginfo_2);
			PvrOpen();
		}

	}
}
