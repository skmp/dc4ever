using System;
namespace DC4Ever
{
	/// <summary>
	/// Emulates the 
	/// HLE Syscall emulation
	/// </summary>
	public partial class emu
	{
		public static bool nobios;//no bios so everything has to be emulated(forced full hle)
		public static bool hle;//use hle emulation mixed mode (hle+bios)
		public static byte[] bios_file = new byte[2*dc.mb];// 2mb bios rom
		public static byte[] bios_flash = new byte[256*dc.kb];//256 kb internal flash ram

		public static int loadipbin(string name)//load and proc ip.bin/bootfile  -1/-2 on error
		{
			try//load ip.bin
			{
				System.IO.FileInfo fi= new System.IO.FileInfo(name);
				System.IO.FileStream fs = fi.OpenRead();
				fs.Read(ram,0x8000,32*dc.kb);
				fs.Close();
			}
			catch 
			{
				return -1;//no ip.bin found
			}

			try
			{//load 1st_read.bin
				System.IO.FileInfo fi= new System.IO.FileInfo("1st_read.bin");
				System.IO.FileStream fs = fi.OpenRead();
				fs.Read(ram,32*dc.kb+0x8000, (int) fi.Length);
				fs.Close();
				return 0;
			}
			catch //no 1st_read.bin found
			{	
				return -2;
			}

		}
		public static void loadbiosfile(string name)//load bios file
		{
			try//try to load the bios file..
			{
				System.IO.FileInfo fi= new System.IO.FileInfo(name);
				System.IO.FileStream fs = fi.OpenRead();
				fs.Read(bios_file,0,2*dc.mb);
				fs.Close();
				dc.dcon.WriteLine("Bios File \""+ name + "\" loaded");
			}
			catch// we do not have the bios :(..Emulate it :)
			{
				dc.dcon.WriteLine("Bios File \""+ name + "\" is missing");
				nobios=true;//force full hle emulation
				dc.dcon.WriteLine("Using Full bios HLE emulation");
				return;
			}
			hle=true;//Enable mixed mode (hle+bios)
			dc.dcon.WriteLine("Using mixed Mode bios emulation (Bios+HLE)");
		}
		public static void loadbiosflashfile(string name)//load flash ram file
		{
			try
			{
				System.IO.FileInfo fi= new System.IO.FileInfo(name);
				System.IO.FileStream fs = fi.OpenRead();
				fs.Read(bios_flash,0,256*dc.kb);
				fs.Close();
				dc.dcon.WriteLine("Flash ram readed from \"" + name + "\"");
			}
			catch//well the file does not exist so... create it :)
			{
				dc.dcon.WriteLine("Flash ram \"" + name + "\" not found. Creating one");
				savebiosflashfile(name);
			}
		}
		public static void savebiosflashfile(string name)//save flash ram file
		{
			try
			{
				System.IO.FileInfo fi= new System.IO.FileInfo(name);
				System.IO.FileStream fs = fi.OpenWrite();
				fs.Write(bios_flash,0,256*dc.kb);
				fs.Close();
				dc.dcon.WriteLine("Flash ram saved to \"" + name + "\"");
			}
			catch
			{
				dc.dcon.WriteLine("Flash ram cannot be saved to \"" + name + "\"");
			}
		}
		public static unsafe uint readBios(uint adr,int len)//read from bios mem - no write (rom)
		{
			//TODO : BIOS HLE Force and Mixed modes
			if (len ==0x3)//olny if this read is from an opcode fetch
			{
				#region Bios HLE Mixed
				if (hle==true)
				{
					//return a value according to the addr/register data
				}
				#endregion
				#region Bios HLE Forced
				//if command is not hle and we do not have the bios 
				//we _must_ emulate it..
				if (nobios==true)
				{
					//return a value according to the addr/register data
					return 65535+100;//just jump back- no emulation :P
				}
				#endregion
			}
			switch (len)
			{
				case 0x1://1 byte read
					return bios_file[adr];
				case 0x2://2 byte read
					fixed(byte *p=&bios_file[adr])
						return *(ushort*)p;
				case 0x3://2 byte read//WARNING THIS IS NOT AN error
					//0x3 is a 0x2 read but using mHLE /fHLE- when the hle sys failed..
					fixed(byte *p=&bios_file[adr])
						return *(ushort*)p;
				case 0x4://4 byte read
					fixed(byte *p=&bios_file[adr])
						return *(uint*)p;
			}
			dc.dcon.WriteLine("Wrong read size in readBios (" + len+") at pc "+pc);
			return 0;
		}
		public static unsafe uint readBios_falsh(uint adr,int len)
		{
			switch (len)
			{
				case 0x1://1 byte read
					return bios_flash[adr];
				case 0x2://2 byte read
					fixed(byte *p=&bios_flash[adr])
						return *(ushort*)p;
				case 0x4://4 byte read
					fixed(byte *p=&bios_flash[adr])
						return *(uint*)p;
			}
			dc.dcon.WriteLine("Wrong read size in readBios_flash (" + len+") at pc "+pc);
			return 0;
		}
		public static unsafe void writeBios_flash(uint adr,uint data,int len)//write to the flash
		{
			switch (len)
			{
				case 0x1://1 byte write
					bios_flash[adr]=(byte)data;
					return;
				case 0x2://2 byte write
					fixed(byte *p=&bios_flash[adr])
						*(ushort*)p=(ushort)data;
					return;
				case 0x4://4 byte write
					fixed(byte *p=&bios_flash[adr])
						*(uint*)p=data;
					return; 
			}
			dc.dcon.WriteLine("Wrong write size in writeBios_flash (" + len+") at pc "+pc);
		}
	}
}
