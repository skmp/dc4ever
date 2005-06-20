using System;
using System.Collections.Generic;
using System.Text;

namespace DC4Ever
{
	public static unsafe partial class emu
	{
		public const ushort key_CONT_C = (1 << 0);
		public const ushort key_CONT_B = (1 << 1);
		public const ushort key_CONT_A = (1 << 2);
		public const ushort key_CONT_START = (1 << 3);
		public const ushort key_CONT_DPAD_UP = (1 << 4);
		public const ushort key_CONT_DPAD_DOWN = (1 << 5);
		public const ushort key_CONT_DPAD_LEFT = (1 << 6);
		public const ushort key_CONT_DPAD_RIGHT = (1 << 7);
		public const ushort key_CONT_Z = (1 << 8);
		public const ushort key_CONT_Y = (1 << 9);
		public const ushort key_CONT_X = (1 << 10);
		public const ushort key_CONT_D = (1 << 11);
		public const ushort key_CONT_DPAD2_UP = (1 << 12);
		public const ushort key_CONT_DPAD2_DOWN = (1 << 13);
		public const ushort key_CONT_DPAD2_LEFT = (1 << 14);
		public const ushort key_CONT_DPAD2_RIGHT = (1 << 15);

		public static MapleDevice[] MapleDev = new MapleDevice[4];

		static void MapleEnable(uint data)
		{
			*MAPLE_ENABLE = data;//??? what is that ??
			//dc.dbger.mode = 1;
		}

		static void MapleDMAState(uint data)
		{
			//Start Maple DMA transfer
			if ((data & 0x1)!=0)
			{
				uint addr = *MAPLE_DMAADDR;
				bool last = false;
				while (last != true)
				{
					uint header_1 = read(addr, 4);
					uint header_2 = read(addr + 4, 4) &0x1FFFFFE0;
					if (header_1 == 0)//this must not hapen ...
					{
						WriteLine("Maple ; Header1 coruption,ending transfer");
						write(header_2, 0xFFFFFFFF, 4);//not conected;
						break;
					}
					last = (header_1 >> 31) == 1;//is last transfer ?
					uint plen = (header_1 & 0xFF )+1;//transfer lenght
					uint device = (header_1 >> 16) & 0x3;

					if (MapleDev[device] == null)
					{
						write(header_2, 0xFFFFFFFF, 4);//not conected
					}
					else
					{
						uint* p_data = stackalloc uint[(int)plen];

						for (uint i=0;i<plen;i++)
						{
							p_data[i] = read(addr + 8 + (i << 2), 4);
						}
						MapleDev[device].GotData(header_1,header_2,p_data,plen);
					}
					//goto next command
					addr += 2 * 4 + plen * 4;
				}
				*MAPLE_STATE = 0;//finished :P
				RaiseInterupt(sh4_int.holly_MAPLE_DMA);
			}


//			MapleDMAFinished = true;
		}

		static void MapleReset2(uint data)
		{
			//just ingore the write..
		}
		static void MapleReset1(uint data)
		{
			//just ingore the write..
		}
		static void MapleSpeed(uint data)
		{
			//just ingore the write..
		}

		static void InitMaple()
		{
			MapleDev[0] = new DcJoy();
			MapleDev[1] = null;
			MapleDev[2] = null;
			MapleDev[3] = null;
		}
	}
	
	public unsafe interface MapleDevice
	{
		void GotData(uint header1, uint header2, uint* data, uint datalen);
	}

	public unsafe class DcJoy : MapleDevice
	{
		static string strName = "Dreamcast Emulated Controler\0";
		static string strBrand = "A fake brand (tm)\0";
		public void GotData(uint header1,uint header2,uint*data,uint datalen)
		{
			uint command = data[0] & 0xFF;
			uint recadr = (data[0] >> 8) & 0xFF;
			uint sendadr = (data[0] >> 16) & 0xFF;
			uint ptr_out;
			if (recadr != 0x20)
			{
				emu.write(header2, 0xFFFFFFFF, 4);//not conected
				return;
			}
			switch (command)
			{
				/*typedef struct {
					DWORD		func;//4
					DWORD		function_data[3];//3*4
					BYTE		area_code;//1
					BYTE		connector_direction;//1
					char		product_name[30];//30*1
					char		product_license[60];//60*1
					WORD		standby_power;//2
					WORD		max_power;//2
				} maple_devinfo_t;*/
				case 1:
					ptr_out = header2;
					//header
					emu.write(ptr_out,(uint)(0x05 | //response
								(((ushort)sendadr << 8) & 0xFF00) |
								((((recadr == 0x20) ? 0x20 : 0) << 16) & 0xFF0000) |
								(((112/*size*//4) << 24) & 0xFF000000)),4); ptr_out += 4;
					//caps
					//4
					emu.write(ptr_out, (1 << 24), 4); ptr_out += 4;

					//struct data
					//3*4
					emu.write(ptr_out, 0, 4); ptr_out += 4;
					emu.write(ptr_out, 0, 4); ptr_out += 4;
					emu.write(ptr_out, 0, 4); ptr_out += 4;
					//1
					emu.write(ptr_out, 0, 1); ptr_out += 1;
					//1
					emu.write(ptr_out, 0, 1); ptr_out += 1;
					//30
					for (uint i = 0; i < 30; i++)
					{
						if (i<strName.Length)
						{
							emu.write(ptr_out + i, (byte)strName[(int)i], 1);
						}
					}
					ptr_out += 30;

					//60
					for (uint i = 0; i < 30; i++)
					{
						if (i < strBrand.Length)
						{
							emu.write(ptr_out + i, (byte)strBrand[(int)i], 1);
						}
					}
					ptr_out += 60;

					//2
					emu.write(ptr_out, 0, 2); ptr_out += 2;

					//2
					emu.write(ptr_out, 0, 2); ptr_out += 2;
					break;

				/* controller condition structure 
				typedef struct {//8 bytes
				WORD buttons;			///* buttons bitfield	/2
				BYTE rtrig;			///* right trigger			/1
				BYTE ltrig;			///* left trigger 			/1
				BYTE joyx;			////* joystick X 			/1
				BYTE joyy;			///* joystick Y				/1
				BYTE joy2x;			///* second joystick X 		/1
				BYTE joy2y;			///* second joystick Y 		/1
				} cont_cond_t;*/
				case 9:
					ptr_out = header2;

					//header
					emu.write(ptr_out, (uint)(0x08 | // data transfer (response)
								(((ushort)sendadr << 8) & 0xFF00) |
								((((recadr == 0x20) ? 0x20 : 1) << 16) & 0xFF0000) |
								(((12/*size*/ / 4 ) << 24) & 0xFF000000)), 4); ptr_out += 4;
					//caps
					//4
					emu.write(ptr_out, (1 << 24), 4); ptr_out += 4;

					//struct data
					//2
					emu.write(ptr_out, frmmain.kcode, 4); ptr_out += 2;
					
					//1
					emu.write(ptr_out, 0, 1); ptr_out += 1;
					//1
					emu.write(ptr_out, 0, 1); ptr_out += 1;
					//1
					emu.write(ptr_out, 0, 1); ptr_out += 1;
					//1
					emu.write(ptr_out, 0, 1); ptr_out += 1;

					//1
					emu.write(ptr_out, 0, 1); ptr_out += 1;
					//1
					emu.write(ptr_out, 0, 1); ptr_out += 1;
					//1
					emu.write(ptr_out, 0, 1); ptr_out += 1;
					//1
					emu.write(ptr_out, 0, 1); ptr_out += 1;

					break;

				default:

					break;
			}
		}
	}
}
