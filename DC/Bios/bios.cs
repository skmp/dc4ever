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
namespace DC4Ever
{
	/// <summary>
	/// Emulates the 
	/// HLE Syscall emulation
	/// </summary>
    public unsafe class bios
	{
		public const int kb = 1024;
		public const int mb = kb * 1024;
		public const uint dc_bios_hle_vec_1 = 0x100;
		public const uint dc_bios_hle_vec_2 = 0x100 + 2;
		public const uint dc_bios_hle_vec_3 = 0x100 + 4;
		public const uint dc_bios_hle_vec_4 = 0x100 + 6;
		public const uint dc_bios_hle_vec_5 = 0x100 + 8;

		public const uint dc_bios_syscall_system = 0x8C0000B0;
		public const uint dc_bios_syscall_font = 0x8C0000B4;
		public const uint dc_bios_syscall_flashrom = 0x8C0000B8;
		public const uint dc_bios_syscall_GDrom_misc = 0x8C0000BC;
		public const uint dc_bios_syscall_resets_Misc = 0x8c0000e0;

		public static bool nobios;//no bios so everything has to be emulated(forced full hle)
		public static bool hle;//use hle emulation mixed mode (hle+bios)
		public static bool useBios = false;//use bios if it exists..
		public static byte[] bios_file = new byte[2*mb];// 2mb bios rom
		public static byte[] bios_flash = new byte[256*kb];//256 kb internal flash ram
		public static uint FakeRomFondAddr = 0x8cF82C18;//start of mem
		public static int loadipbin(string name)//load and proc ip.bin/bootfile  -1/-2 on error
		{
			try//load ip.bin
			{
				System.IO.FileInfo fi= new System.IO.FileInfo(name);
				System.IO.FileStream fs = fi.OpenRead();
                byte[] tmp=new byte[32*kb];
				fs.Read(tmp,0,32*kb);
                pointerlib.unmanaged_pointer.ArrCpy(mem.ram_b, tmp, 0, 0x8000, 32 * kb);
                tmp = null; GC.Collect();
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
                byte[] tmp = new byte[fi.Length];
                fs.Read(tmp,0, (int)fi.Length);
                pointerlib.unmanaged_pointer.ArrCpy(mem.ram_b, tmp, 0,32 * kb + 0x8000,(uint) fi.Length);
                tmp = null; GC.Collect();
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
			//
			try
			{
				System.IO.FileInfo fi = new System.IO.FileInfo("dcFont.bin");
				System.IO.FileStream fs = fi.OpenRead();
				byte[] fd = new byte[fi.Length];
				fs.Read(fd, 0, (int)fi.Length);
				for (uint i = 0; i < fi.Length; i++)
				{
					mem.write(FakeRomFondAddr + i, fd[i], 1);
				}
				fs.Close();
			}
			catch { }
			try//try to load the bios file..
			{
				System.IO.FileInfo fi= new System.IO.FileInfo(name);
				System.IO.FileStream fs = fi.OpenRead();
				fs.Read(bios_file,0,2*mb);
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
				fs.Read(bios_flash,0,256*kb);
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
				fs.Write(bios_flash,0,256*kb);
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

			//0x38e
			//0x98a


            //return 0xB;
            //TODO : BIOS HLE Force and Mixed modes
			if (len ==0x3)//olny if this read is from an opcode fetch
			{
				//if ((adr == 0x38e) | (adr == 0x98a))
					//return 0x9;

				#region Bios HLE Mixed
				if (hle==true)
				{
					//return a value according to the addr/register data
				}
				#endregion
				#region Bios HLE Forced
				//if command is not hle and we do not have the bios 
				//we _must_ emulate it..
				if (nobios==true | useBios==false)
				{
					//
					//return a value according to the addr/register data
					switch (sh4.pc)
					{
						case dc_bios_hle_vec_1:
							switch (sh4.r[7])
							{
								case 0:
									//SYSINFO_INIT 
									mem.WriteLine("Bios call : SYSINFO_INIT");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
								case 1:
									//not a valid syscall 
									mem.WriteLine("Bios call : not a valid syscall");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
								case 2:
									//SYSINFO_ICON 
									mem.WriteLine("Bios call : SYSINFO_ICON");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
								case 3:
									//SYSINFO_ID 
									mem.WriteLine("Bios call : SYSINFO_ID");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
							}
							break;
						case dc_bios_hle_vec_2:
							switch (sh4.r[1])
							{
								case 0:
									//ROMFONT_ADDRESS 
									//WriteLine("bios call : ROMFONT_ADDRESS");
									sh4.r[0] = FakeRomFondAddr;
									//WriteLine("Bios call was HLE'd");
									return 65537;//just jump back- no emulation :P
									//break;
								case 1:
									//ROMFONT_LOCK 
									mem.WriteLine("Bios call : ROMFONT_LOCK");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
								case 2:
									//ROMFONT_UNLOCK 
									mem.WriteLine("Bios call : ROMFONT_UNLOCK");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
							}
							break;
						case dc_bios_hle_vec_3:
							switch (sh4.r[7])
							{
								case 0:
									//FLASHROM_INFO  
									mem.WriteLine("Bios call : FLASHROM_INFO");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
								case 1:
									//FLASHROM_READ  
									mem.WriteLine("Bios call : FLASHROM_READ");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
								case 2:
									//FLASHROM_WRITE  
									mem.WriteLine("Bios call : FLASHROM_WRITE");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
								case 3:
									//FLASHROM_DELETE  
									mem.WriteLine("Bios call : FLASHROM_DELETE");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
							}
							break;
						case dc_bios_hle_vec_4:
							switch (sh4.r[6])
							{
								case 0xFFFFFFFF:
									//MISC superfunction 
									mem.WriteLine("Bios call : MISC superfunction");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;
								case 0:
									//GDROM superfunction 
									switch (sh4.r[7])
									{
										case 0:
											mem.WriteLine("GDROM_SEND_COMMAND");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
//											break;

										case 1:
											mem.WriteLine("GDROM_CHECK_COMMAND");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
//											break;

										case 2:
											mem.WriteLine("GDROM_MAINLOOP");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
//											break;

										case 3:
											mem.WriteLine("GDROM_INIT");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
//											break;

										case 4:
											mem.WriteLine("GDROM_CHECK_DRIVE");
											mem.WriteLine("Bios call was FAKED");
											mem.write(sh4.r[4], 2, 4);// standby
											mem.write(sh4.r[4] + 4, 0x80, 4);// GD-ROM

											sh4.r[0] = 0;
											return 65537;//just jump back- no emulation :P
//											break;

										case 5:
											mem.WriteLine("GDROM_?DMA?");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
//											break;

										case 6:
											mem.WriteLine("GDROM_?DMA?");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
//											break;

										case 7:
											mem.WriteLine("GDROM_?DMA?");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
///											break;

										case 8:
											mem.WriteLine("GDROM_ABORT_COMMAND");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
///											break;

										case 9:
											mem.WriteLine("GDROM_RESET");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
///											break;

										case 10:
											mem.WriteLine("GDROM_SECTOR_MODE");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
///											break;

										case 11:
											mem.WriteLine("GDROM_?");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
//											break;

										case 12:
											mem.WriteLine("GDROM_?");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
///											break;

										case 13:
											mem.WriteLine("GDROM_?");
											mem.WriteLine("Bios call was skiped");
											return 65537;//just jump back- no emulation :P
///											break;
									}

									mem.WriteLine("Bios call : GDROM superfunction ");
									mem.WriteLine("Bios call was skiped");
									return 65537;//just jump back- no emulation :P
									//break;

							}
							break;
						case dc_bios_hle_vec_5:
							{
								mem.WriteLine("Bios call : unkoen system call");
								mem.WriteLine("Bios call was skiped");
								return 65537;//just jump back- no emulation :P
							}
					}
					//return 65537;
					mem.WriteLine("ReadBios (" + adr + ") at pc " + sh4.pc);
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
			dc.dcon.WriteLine("Wrong read size in readBios (" + len+") at pc "+sh4.pc);
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
			dc.dcon.WriteLine("Wrong read size in readBios_flash (" + len+") at pc "+sh4.pc);
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
			dc.dcon.WriteLine("Wrong write size in writeBios_flash (" + len+") at pc "+sh4.pc);
		}

        public static void UpdateBios(uint cycles) { }
		public static void InitBios()
		{
			mem.write(dc_bios_syscall_system , dc_bios_hle_vec_1, 4);
			mem.write(dc_bios_syscall_font, dc_bios_hle_vec_2, 4);
			mem.write(dc_bios_syscall_flashrom, dc_bios_hle_vec_3, 4);
			mem.write(dc_bios_syscall_GDrom_misc, dc_bios_hle_vec_4, 4);
			mem.write(dc_bios_syscall_resets_Misc, dc_bios_hle_vec_5, 4);
		}
    }
}
