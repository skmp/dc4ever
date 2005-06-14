using System;
using System.Collections.Generic;
using System.Text;

namespace DC4Ever
{
	static unsafe partial class emu
	{
		public static string DisasmOpcode(uint opcode, uint pc)
		{
			#region Dissasm - switch
			switch (opcode >> 12)//proc opcode
			{
				case 0x0://finished
					#region case 0x0
					switch (opcode & 0xf)
					{
						case 0x0://0000
							return "Invalid opcode";
						//break;
						case 0x1://0001
							return "Invalid opcode";
						//break;
						case 0x2://0010
							#region case 0x2 multi opcodes
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0000
									return d0000_nnnn_0000_0010(opcode, pc);
								//return "stc SR{" + sr.reg.ToString() + "}," + RegToString(Get2N(opcode));
								//break;
								case 0x1://0001
									return d0000_nnnn_0001_0010(opcode, pc);
								//return "stc GBR{" + gbr.ToString() + "}," + RegToString(Get2N(opcode)) + "//nimp";
								//break;
								case 0x2://0010
									return d0000_nnnn_0010_0010(opcode, pc);
								//break;
								case 0x3://0011
									return d0000_nnnn_0011_0010(opcode, pc);
								//break;
								case 0x4://0100
									return d0000_nnnn_0100_0010(opcode, pc);
								//break;
								case 0x8://1000
									return d0000_nnnn_1000_0010(opcode, pc);
								//break;
								case 0x9://1001
									return d0000_nnnn_1001_0010(opcode, pc);
								//break;
								case 0xA://1010
									return d0000_nnnn_1010_0010(opcode, pc);
								//break;
								case 0xB://1011
									return d0000_nnnn_1011_0010(opcode, pc);
								//break;
								case 0xC://1100
									return d0000_nnnn_1100_0010(opcode, pc);
								//break;
								case 0xD://1101
									return d0000_nnnn_1101_0010(opcode, pc);
								//break;
								case 0xE://1110
									return d0000_nnnn_1110_0010(opcode, pc);
								//break;
								case 0xF://1111
									return d0000_nnnn_1111_0010(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x3://0011
							#region case 0x3 multi opcodes
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0000
									return d0000_nnnn_0000_0011(opcode, pc);
								//break;
								case 0x2://0010
									return d0000_nnnn_0010_0011(opcode, pc);
								//break;
								case 0x8://1000
									return d0000_nnnn_1000_0011(opcode, pc);
								//break;
								case 0x9://1001
									return d0000_nnnn_1001_0011(opcode, pc);
								//break;
								case 0xA://1010
									return d0000_nnnn_1010_0011(opcode, pc);
								//break;
								case 0xB://1011
									return d0000_nnnn_1011_0011(opcode, pc);
								//break;
								case 0xC://1100
									return d0000_nnnn_1100_0011(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x4://0100
							return d0000_nnnn_mmmm_0100(opcode, pc);
						//break;
						case 0x5://0101
							return d0000_nnnn_mmmm_0101(opcode, pc);
						//break;
						case 0x6://0110
							return d0000_nnnn_mmmm_0110(opcode, pc);
						//break;
						case 0x7://0111
							return d0000_nnnn_mmmm_0111(opcode, pc);
						//break;
						case 0x8://1000
							#region case 0x8 multi opcodes
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0000
									return d0000_0000_0000_1000(opcode, pc);
								//break;
								case 0x1://0001
									return d0000_0000_0001_1000(opcode, pc);
								//break;
								case 0x2://0010
									return d0000_0000_0010_1000(opcode, pc);
								//break;
								case 0x3://0011
									return d0000_0000_0011_1000(opcode, pc);
								//break;
								case 0x4://0100
									return d0000_0000_0100_1000(opcode, pc);
								//break;
								case 0x5://0101
									return d0000_0000_0101_1000(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x9://1001
							#region case 0x9 multi opcodes
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0000
									return d0000_0000_0000_1001(opcode, pc);
								//break;
								case 0x1://0001
									return d0000_0000_0001_1001(opcode, pc);
								//break;
								case 0x2://0010
									return d0000_nnnn_0010_1001(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0xA://1010
							#region case 0xA multi opcodes
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0000
									return d0000_nnnn_0000_1010(opcode, pc);
								//break;
								case 0x1://0001
									return d0000_nnnn_0001_1010(opcode, pc);
								//break;
								case 0x2://0010
									return d0000_nnnn_0010_1010(opcode, pc);
								//break;
								case 0x5://0101
									return d0000_nnnn_0101_1010(opcode, pc);
								//break;
								case 0x6://0110
									return d0000_nnnn_0110_1010(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0xB://1011
							#region case 0xB multi opcodes
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0000
									return d0000_0000_0000_1011(opcode, pc);
								//break;
								case 0x1://0001
									return d0000_0000_0001_1011(opcode, pc);
								//break;
								case 0x2://0010
									return d0000_0000_0010_1011(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0xC://1100
							return d0000_nnnn_mmmm_1100(opcode, pc);
						//break;
						case 0xD://1101
							return d0000_nnnn_mmmm_1101(opcode, pc);
						//break;
						case 0xE://1110
							return d0000_nnnn_mmmm_1110(opcode, pc);
						//break;
						case 0xF://1111
							return d0000_nnnn_mmmm_1111(opcode, pc);
						//break;
					}
					#endregion
				break;
				case 0x1://finished
					return d0001_nnnn_mmmm_iiii(opcode, pc);
				//return "mov.l " + RegToString(Get3N(opcode)) + ",@(" + Get4N(opcode) + "," + RegToString(Get2N(opcode)) + ")";
				//break;
				case 0x2://finished
					#region case 0x2
					switch (opcode & 0xf)
					{
						case 0x0://0000
							return d0010_nnnn_mmmm_0000(opcode, pc);
						//break;
						case 0x1://0001
							return d0010_nnnn_mmmm_0001(opcode, pc);
						//break;
						case 0x2://0010
							return d0010_nnnn_mmmm_0010(opcode, pc);
						//break;
						case 0x4://0100
							return d0010_nnnn_mmmm_0100(opcode, pc);
						//break;
						case 0x5://0101
							return d0010_nnnn_mmmm_0101(opcode, pc);
						//break;
						case 0x6://0110
							return d0010_nnnn_mmmm_0110(opcode, pc);
						//break;
						case 0x7://0111
							return d0010_nnnn_mmmm_0111(opcode, pc);
						//break;
						case 0x8://1000
							return d0010_nnnn_mmmm_1000(opcode, pc);
						//break;
						case 0x9://1001
							return d0010_nnnn_mmmm_1001(opcode, pc);
						//break;
						case 0xA://1010
							return d0010_nnnn_mmmm_1010(opcode, pc);
						//break;
						case 0xB://1011
							return d0010_nnnn_mmmm_1011(opcode, pc);
						//break;
						case 0xC://1100
							return d0010_nnnn_mmmm_1100(opcode, pc);
						//break;
						case 0xD://1101
							return d0010_nnnn_mmmm_1101(opcode, pc);
						//break;
						case 0xE://1110
							return d0010_nnnn_mmmm_1110(opcode, pc);
						//break;
						case 0xF://1111
							return d0010_nnnn_mmmm_1111(opcode, pc);
						//break;
						default:
							return "Invalid opcode";
						//break;
					}
					#endregion
//				break;
				case 0x3://finished
					#region case 0x3
					switch (opcode & 0xf)
					{
						case 0x0://0000
							return d0011_nnnn_mmmm_0000(opcode, pc);
						//break;
						case 0x2://0010
							return d0011_nnnn_mmmm_0010(opcode, pc);
						//break;
						case 0x3://0011
							return d0011_nnnn_mmmm_0011(opcode, pc);
						//break;
						case 0x4://0100
							return d0011_nnnn_mmmm_0100(opcode, pc);
						//break;
						case 0x5://0101
							return d0011_nnnn_mmmm_0101(opcode, pc);
						//break;
						case 0x6://0110
							return d0011_nnnn_mmmm_0110(opcode, pc);
						//break;
						case 0x7://0111
							return d0011_nnnn_mmmm_0111(opcode, pc);
						//break;
						case 0x8://1000
							return d0011_nnnn_mmmm_1000(opcode, pc);
						//break;
						case 0xA://1010
							return d0011_nnnn_mmmm_1010(opcode, pc);
						//break;
						case 0xB://1011
							return d0011_nnnn_mmmm_1011(opcode, pc);
						//break;
						case 0xC://1100
							return d0011_nnnn_mmmm_1100(opcode, pc);
						//break;
						case 0xD://1101
							return d0011_nnnn_mmmm_1101(opcode, pc);
						//break;
						case 0xE://1110
							return d0011_nnnn_mmmm_1110(opcode, pc);
						//break;
						case 0xF://1111
							return d0011_nnnn_mmmm_1111(opcode, pc);
						//break;
						default:
							return "Invalid opcode";
						//break;
					}
					#endregion
				//break;
				case 0x4://finished
					#region case 0x4
					switch (opcode & 0xf)
					{
						case 0x0://0000
							#region 0x0 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_0000
									return d0100_nnnn_0000_0000(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_0000
									return d0100_nnnn_0001_0000(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_0000
									return d0100_nnnn_0010_0000(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x1://0001
							#region 0x1 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_0001
									return d0100_nnnn_0000_0001(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_0001
									return d0100_nnnn_0001_0001(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_0001
									return d0100_nnnn_0010_0001(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x2://0010
							#region 0x2 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_0010
									return d0100_nnnn_0000_0010(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_0010
									return d0100_nnnn_0001_0010(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_0010
									return d0100_nnnn_0010_0010(opcode, pc);
								//break;
								case 0x5://0100_xxxx_0101_0010
									return d0100_nnnn_0101_0010(opcode, pc);
								//break;
								case 0x6://0100_xxxx_0110_0010
									return d0100_nnnn_0110_0010(opcode, pc);
								//break;
								case 0x8://0100_xxxx_1000_0010
									return d0100_nnnn_1000_0010(opcode, pc);
								//break;
								case 0x9://0100_xxxx_1001_0010
									return d0100_nnnn_1001_0010(opcode, pc);
								//break;
								case 0xA://0100_xxxx_1010_0010
									return d0100_nnnn_1010_0010(opcode, pc);
								//break;
								case 0xB://0100_xxxx_1011_0010
									return d0100_nnnn_1011_0010(opcode, pc);
								//break;
								case 0xC://0100_xxxx_1100_0010
									return d0100_nnnn_1100_0010(opcode, pc);
								//break;
								case 0xD://0100_xxxx_1101_0010
									return d0100_nnnn_1101_0010(opcode, pc);
								//break;
								case 0xE://0100_xxxx_1110_0010
									return d0100_nnnn_1110_0010(opcode, pc);
								//break;
								case 0xF://0100_xxxx_1111_0010
									return d0100_nnnn_1111_0010(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x3://0011
							#region 0x3 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_0011
									return d0100_nnnn_0000_0011(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_0011
									return d0100_nnnn_0001_0011(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_0011
									return d0100_nnnn_0010_0011(opcode, pc);
								//break;
								case 0x3://0100_xxxx_0011_0011
									return d0100_nnnn_0011_0011(opcode, pc);
								//break;
								case 0x4://0100_xxxx_0100_0011
									return d0100_nnnn_0100_0011(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x4://0100
							#region 0x4 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_0100
									return d0100_nnnn_0000_0100(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_0100
									return d0100_nnnn_0010_0100(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x5://0101
							#region 0x5 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_0101
									return d0100_nnnn_0000_0101(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_0101
									return d0100_nnnn_0001_0101(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_0101
									return d0100_nnnn_0010_0101(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x6://0110
							#region 0x6 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_0110
									return d0100_nnnn_0000_0110(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_0110
									return d0100_nnnn_0001_0110(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_0110
									return d0100_nnnn_0010_0110(opcode, pc);
								//break;
								case 0x5://0100_xxxx_0101_0110
									return d0100_nnnn_0101_0110(opcode, pc);
								//break;
								case 0x6://0100_xxxx_0110_0110
									return d0100_nnnn_0110_0110(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x7://0111
							#region 0x7 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_0111
									return d0100_nnnn_0000_0111(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_0111
									return d0100_nnnn_0001_0111(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_0111
									return d0100_nnnn_0010_0111(opcode, pc);
								//break;
								case 0x3://0100_xxxx_0011_0111
									return d0100_nnnn_0011_0111(opcode, pc);
								//break;
								case 0x4://0100_xxxx_0100_0111
									return d0100_nnnn_0100_0111(opcode, pc);
								//break;
								case 0x8://0100_xxxx_1000_0111
									return d0100_nnnn_1000_0111(opcode, pc);
								//break;
								case 0x9://0100_xxxx_1001_0111
									return d0100_nnnn_1001_0111(opcode, pc);
								//break;
								case 0xA://0100_xxxx_1010_0111
									return d0100_nnnn_1010_0111(opcode, pc);
								//break;
								case 0xB://0100_xxxx_1011_0111
									return d0100_nnnn_1011_0111(opcode, pc);
								//break;
								case 0xC://0100_xxxx_1100_0111
									return d0100_nnnn_1100_0111(opcode, pc);
								//break;
								case 0xD://0100_xxxx_1101_0111
									return d0100_nnnn_1101_0111(opcode, pc);
								//break;
								case 0xE://0100_xxxx_1110_0111
									return d0100_nnnn_1110_0111(opcode, pc);
								//break;
								case 0xF://0100_xxxx_1111_0111
									return d0100_nnnn_1111_0111(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x8://1000
							#region 0x8 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_1000
									return d0100_nnnn_0000_1000(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_1000
									return d0100_nnnn_0001_1000(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_1000
									return d0100_nnnn_0010_1000(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0x9://1001
							#region 0x9 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_1001
									return d0100_nnnn_0000_1001(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_1001
									return d0100_nnnn_0001_1001(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_1001
									return d0100_nnnn_0010_1001(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;

						case 0xA://1010
							#region 0x9 multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_1010
									return d0100_nnnn_0000_1010(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_1010
									return d0100_nnnn_0001_1010(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_1010
									return d0100_nnnn_0010_1010(opcode, pc);
								//break;
								case 0x5://0100_xxxx_0101_1010
									return d0100_nnnn_0101_1010(opcode, pc);
								//break;
								case 0x6://0100_xxxx_0110_1010
									return d0100_nnnn_0110_1010(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;

						case 0xB://1011
							#region 0xB multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_1011
									return d0100_nnnn_0000_1011(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_1011
									return d0100_nnnn_0001_1011(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_1011
									return d0100_nnnn_0010_1011(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0xC://1100
							return d0100_nnnn_mmmm_1100(opcode, pc);
						//break;
						case 0xD://1101
							return d0100_nnnn_mmmm_1101(opcode, pc);
						//break;
						case 0xE://1110
							#region 0xE multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0100_xxxx_0000_1110
									return d0100_nnnn_0000_1110(opcode, pc);
								//break;
								case 0x1://0100_xxxx_0001_1110
									return d0100_nnnn_0001_1110(opcode, pc);
								//break;
								case 0x2://0100_xxxx_0010_1110
									return d0100_nnnn_0010_1110(opcode, pc);
								//break;
								case 0x3://0100_xxxx_0011_1110
									return d0100_nnnn_0011_1110(opcode, pc);
								//break;
								case 0x4://0100_xxxx_0100_1110
									return d0100_nnnn_0100_1110(opcode, pc);
								//break;
								case 0x8://0100_xxxx_1000_1110
									return d0100_nnnn_1000_1110(opcode, pc);
								//break;
								case 0x9://0100_xxxx_1001_1110
									return d0100_nnnn_1001_1110(opcode, pc);
								//break;
								case 0xA://0100_xxxx_1010_1110
									return d0100_nnnn_1010_1110(opcode, pc);
								//break;
								case 0xB://0100_xxxx_1011_1110
									return d0100_nnnn_1011_1110(opcode, pc);
								//break;
								case 0xC://0100_xxxx_1100_1110
									return d0100_nnnn_1100_1110(opcode, pc);
								//break;
								case 0xD://0100_xxxx_1101_1110
									return d0100_nnnn_1101_1110(opcode, pc);
								//break;
								case 0xE://0100_xxxx_1110_1110
									return d0100_nnnn_1110_1110(opcode, pc);
								//break;
								case 0xF://0100_xxxx_1111_1110
									return d0100_nnnn_1111_1110(opcode, pc);
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0xF://1111
							return d0100_nnnn_mmmm_1111(opcode, pc);
						//break;
						default:
							return "Invalid opcode";
						//break;
					}
					#endregion
				//break;
				case 0x5://finished
					return d0101_nnnn_mmmm_iiii(opcode, pc);
				//break;
				case 0x6://finished
					#region case 0x6
					switch (opcode & 0xf)
					{
						case 0x0://0000
							return d0110_nnnn_mmmm_0000(opcode, pc);
						//break;
						case 0x1://0001
							return d0110_nnnn_mmmm_0001(opcode, pc);
						//break;
						case 0x2://0010
							return d0110_nnnn_mmmm_0010(opcode, pc);
						//break;
						case 0x3://0011
							return d0110_nnnn_mmmm_0011(opcode, pc);
						//break;
						case 0x4://0100
							return d0110_nnnn_mmmm_0100(opcode, pc);
						//break;
						case 0x5://0101
							return d0110_nnnn_mmmm_0101(opcode, pc);
						//break;
						case 0x6://0110
							return d0110_nnnn_mmmm_0110(opcode, pc);
						//break;
						case 0x7://0111
							return d0110_nnnn_mmmm_0111(opcode, pc);
						//break;
						case 0x8://1000
							return d0110_nnnn_mmmm_1000(opcode, pc);
						//break;
						case 0x9://1001
							return d0110_nnnn_mmmm_1001(opcode, pc);
						//break;
						case 0xA://1010
							return d0110_nnnn_mmmm_1010(opcode, pc);
						//break;
						case 0xB://1011
							return d0110_nnnn_mmmm_1011(opcode, pc);
						//break;
						case 0xC://1100
							return d0110_nnnn_mmmm_1100(opcode, pc);
						//break;
						case 0xD://1101
							return d0110_nnnn_mmmm_1101(opcode, pc);
						//break;
						case 0xE://1110
							return d0110_nnnn_mmmm_1110(opcode, pc);
						//break;
						case 0xF://1111
							return d0110_nnnn_mmmm_1111(opcode, pc);
						//break;
						default:
							return "Invalid opcode";
						//break;
					}
					#endregion
				//break;
				case 0x7://finished
					return d0111_nnnn_iiii_iiii(opcode, pc);
				//break;
				case 0x8://finished
					#region case 0x8
					switch ((opcode >> 8) & 0xf)
					{
						case 0x0://0000
							return d1000_0000_mmmm_iiii(opcode, pc);
						//break;
						case 0x1://0001
							return d1000_0001_mmmm_iiii(opcode, pc);
						//break;
						case 0x4://0100
							return d1000_0100_mmmm_iiii(opcode, pc);
						//break;
						case 0x5://0101
							return d1000_0101_mmmm_iiii(opcode, pc);
						//break;
						case 0x8://1000
							return d1000_1000_iiii_iiii(opcode, pc);
						//break;
						case 0x9://1001
							return d1000_1001_iiii_iiii(opcode, pc);
						//break;
						case 0xB://1011
							return d1000_1011_iiii_iiii(opcode, pc);
						//break;
						case 0xD://1101
							return d1000_1101_iiii_iiii(opcode, pc);
						//break;
						case 0xF://1111
							return d1000_1111_iiii_iiii(opcode, pc);
						//break;
						default:
							return "Invalid opcode";
						//break;
					}
					#endregion
				//break;
				case 0x9://finished
					return d1001_nnnn_iiii_iiii(opcode, pc);
				//break;
				case 0xA://finished
					return d1010_iiii_iiii_iiii(opcode, pc);
				//break;
				case 0xB://finished
					return d1011_iiii_iiii_iiii(opcode, pc);
				//break;
				case 0xC://finished
					#region case 0xC
					switch ((opcode >> 8) & 0xf)
					{
						case 0x0://0000
							return d1100_0000_iiii_iiii(opcode, pc);
						//break;
						case 0x1://0001
							return d1100_0001_iiii_iiii(opcode, pc);
						//break;
						case 0x2://0010
							return d1100_0010_iiii_iiii(opcode, pc);
						//break;
						case 0x3://0011
							return d1100_0011_iiii_iiii(opcode, pc);
						//break;
						case 0x4://0100
							return d1100_0100_iiii_iiii(opcode, pc);
						//break;
						case 0x5://0101
							return d1100_0101_iiii_iiii(opcode, pc);
						//break;
						case 0x6://0110
							return d1100_0110_iiii_iiii(opcode, pc);
						//break;
						case 0x7://0111
							return d1100_0111_iiii_iiii(opcode, pc);
						//break;
						case 0x8://1000
							return d1100_1000_iiii_iiii(opcode, pc);
						//break;
						case 0x9://1001
							return d1100_1001_iiii_iiii(opcode, pc);
						//break;
						case 0xA://1010
							return d1100_1010_iiii_iiii(opcode, pc);
						//break;
						case 0xB://1011
							return d1100_1011_iiii_iiii(opcode, pc);
						//break;
						case 0xC://1100
							return d1100_1100_iiii_iiii(opcode, pc);
						//break;
						case 0xD://1101
							return d1100_1101_iiii_iiii(opcode, pc);
						//break;
						case 0xE://1110
							return d1100_1110_iiii_iiii(opcode, pc);
						//break;
						case 0xF://1111
							return d1100_1111_iiii_iiii(opcode, pc);
						//break;
						default:
							return "Invalid opcode";
						//break;
					}
					#endregion
				//break;
				case 0xD://finished
					return d1101_nnnn_iiii_iiii(opcode, pc);
				//break;
				case 0xE://finished
					return d1110_nnnn_iiii_iiii(opcode, pc);
				//break;
				case 0xF://finished - fix for fsca
					#region case 0xf
					switch (opcode & 0xf)
					{
						case 0x0://0000
							return d1111_nnnn_mmmm_0000(opcode, pc);
						//break;
						case 0x1://0001
							return d1111_nnnn_mmmm_0001(opcode, pc);
						//break;
						case 0x2://0010
							return d1111_nnnn_mmmm_0010(opcode, pc);
						//break;
						case 0x3://0011
							return d1111_nnnn_mmmm_0011(opcode, pc);
						//break;
						case 0x4://0100
							return d1111_nnnn_mmmm_0100(opcode, pc);
						//break;
						case 0x5://0101
							return d1111_nnnn_mmmm_0101(opcode, pc);
						//break;
						case 0x6://0110
							return d1111_nnnn_mmmm_0110(opcode, pc);
						//break;
						case 0x7://0111
							return d1111_nnnn_mmmm_0111(opcode, pc);
						//break;
						case 0x8://1000
							return d1111_nnnn_mmmm_1000(opcode, pc);
						//break;
						case 0x9://1001
							return d1111_nnnn_mmmm_1001(opcode, pc);
						//break;
						case 0xA://1010
							return d1111_nnnn_mmmm_1010(opcode, pc);
						//break;
						case 0xB://1011
							return d1111_nnnn_mmmm_1011(opcode, pc);
						//break;
						case 0xC://1100
							return d1111_nnnn_mmmm_1100(opcode, pc);
						//break;
						case 0xD://1101
							#region 0xD multi
							switch ((opcode >> 4) & 0xf)
							{
								case 0x0://0000
									return d1111_nnnn_0000_1101(opcode, pc);
								//break;
								case 0x1://0001
									return d1111_nnnn_0001_1101(opcode, pc);
								//break;
								case 0x2://0010
									return d1111_nnnn_0010_1101(opcode, pc);
								//break;
								case 0x3://0011
									return d1111_nnnn_0011_1101(opcode, pc);
								//break;
								case 0x4://0100
									return d1111_nnnn_0100_1101(opcode, pc);
								//break;
								case 0x5://0101
									return d1111_nnnn_0101_1101(opcode, pc);
								//break;
								case 0x6://0110
									return d1111_nnnn_0110_1101(opcode, pc);
								//break;
								case 0x8://1000
									return d1111_nnnn_1000_1101(opcode, pc);
								//break;
								case 0x9://1001
									return d1111_nnnn_1001_1101(opcode, pc);
								//break;
								case 0xA://1010
									return d1111_nnnn_1010_1101(opcode, pc);
								//break;
								case 0xB://1011
									return d1111_nnnn_1011_1101(opcode, pc);
								//break;
								case 0xF://1111_xxxx_1111_1101
									#region 0xf multi
									//we have :
									//1111_nnn0_1111_1101
									//1111_nn01_1111_1101
									//1111_1011_1111_1101
									//1111_0011_1111_1101
									switch ((opcode >> 8) & 0x1)
									{
										case 0x0://1111_nnn0_1111_1101 - fsca DC special
											return d1111_nnn0_1111_1101(opcode, pc);
										//break;
										case 0x1://1111_xxy1_1111_1101
											//if (opcode==0xfffd) {return "Invalid opcode";//break;}//1111_x111_1111_1101- invalid
											//1111_nn01_1111_1101
											//1111_1011_1111_1101
											//1111_0011_1111_1101
											if (((opcode >> 9) & 0x1) == 0)//1111_xxy1_1111_1101
											{
												return d1111_nn01_1111_1101(opcode, pc);
											}
											else//1111_yy11_1111_1101
											{
												if (((opcode >> 10) & 0x3) == 0)//1111_yy11_1111_1101
												{
													return d1111_0011_1111_1101(opcode, pc);
												}
												else if (((opcode >> 10) & 0x3) == 2)//1111_yy11_1111_1101
												{
													return d1111_1011_1111_1101(opcode, pc);
												}
											}
											//1111_x111_1111_1101- invalid
											return "Invalid opcode";
										//break;
										default:
											return "Invalid opcode";
										//break;
									}
									#endregion
								//break;
								default:
									return "Invalid opcode";
								//break;
							}
							#endregion
						//break;
						case 0xE://1110
							return d1111_nnnn_mmmm_1110(opcode, pc);
						//break;
						default:
							return "Invalid opcode";
						//break;
					}
					#endregion
				//break;
				case 0x10://Custom emulation opcodes ;) "just for the fun of it (tm)"
					switch (opcode & 0xFF)
					{
						case 0x1://rts- driect
							return "Custom ; rts direct (" + emu.pr.ToString() + ")";
						//break;
					}

				break;
				default:
					return "Invalid opcode";
				//break;
			}
			#endregion
			return "Invalid opcode";
		}


		static string RegToString(uint reg)
		{
			return "R" + reg.ToString() + "{" + UintToHex(r[reg]) + "}";
		}
		static string SrToStr()
		{
			return "Sr{" + UintToHex(sr.reg) + "}";
		}

		static string RegBankToString(uint reg)
		{
			return "R_bank" + reg.ToString() + "{" + UintToHex(r_bank[reg]) + "}";
		}
		static string UintToHex(uint val)
		{
			return "0x"+Convert.ToString(val, 16).ToUpper();
		}

		static uint Get1N(uint value)
		{
			return (value >> 12) & 0xf;
		}
		static uint Get2N(uint value)
		{
			return (value >> 8) & 0xf;
		}
		static uint Get3N(uint value)
		{
			return (value >> 4) & 0xf;
		}
		static uint Get4N(uint value)
		{
			return value & 0xf;
		}
		static uint Get1B(uint value)
		{
			return (value >> 8) & 0xff;
		}
		static uint Get2B(uint value)
		{
			return (value >> 0) & 0xff;
		}
		static uint Get12bit(uint value)
		{
			return value & 0xfff;
		}

		static int se8(uint value)
		{
			return (sbyte)value;
		}
		static int se12(uint value)
		{
			uint s = value >> 11;//top bit = sign
			return (int)((value & 0x7FF) | s << 31);
		}
		static int se16(ushort value)
		{
			return (sbyte)value;
		}

		//
		public static string GetProcedureNameFromAddr(uint addr)
		{

			addr &= 0x1FFFFFFF;
			if (addr == (dc_bios_vec & 0x1FFFFFFF))
				return "Dreamcast Bios Boot Address";
			else if (addr == (dc_boot_vec & 0x1FFFFFFF))
				return "Dreamcast Boot Address";
			else
				return "Unknown function name[0x" + Convert.ToString(addr, 16) + "]";
		}


	}


	#region disasm handlers

	//0xxx -  finished nimpl
	public static unsafe partial class emu
	{

		//stc SR," + RegToString(Get2N(opcode)) + "                // no implementation
		static string d0000_nnnn_0000_0010(uint opcode, uint pc)//0002
		{
			return "stc " + SrToStr() + "," + RegToString(Get2N(opcode));
		}


		//stc GBR," + RegToString(Get2N(opcode)) + "               // no implementation
		static string d0000_nnnn_0001_0010(uint opcode, uint pc)
		{
			return "stc GBR," + RegToString(Get2N(opcode)) + "		//nimp";
		}


		//stc VBR," + RegToString(Get2N(opcode)) + "               // no implementation
		static string d0000_nnnn_0010_0010(uint opcode, uint pc)
		{
			return "stc VBR," + RegToString(Get2N(opcode)) + "		//nimp";
		}


		//stc SSR," + RegToString(Get2N(opcode)) + "               // no implementation
		static string d0000_nnnn_0011_0010(uint opcode, uint pc)
		{
			return ("stc SSR," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc SPC," + RegToString(Get2N(opcode)) + "               // no implementation
		static string d0000_nnnn_0100_0010(uint opcode, uint pc)
		{
			return ("stc SPC," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R0_BANK," + RegToString(Get2N(opcode)) + "           // no implementation
		static string d0000_nnnn_1000_0010(uint opcode, uint pc)
		{
			return ("stc R0_BANK," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R1_BANK," + RegToString(Get2N(opcode)) + "           // no implementation
		static string d0000_nnnn_1001_0010(uint opcode, uint pc)
		{
			return ("stc R1_BANK," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R2_BANK," + RegToString(Get2N(opcode)) + "           // no implementation
		static string d0000_nnnn_1010_0010(uint opcode, uint pc)
		{
			return ("stc R2_BANK," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R3_BANK," + RegToString(Get2N(opcode)) + "           // no implementation
		static string d0000_nnnn_1011_0010(uint opcode, uint pc)
		{//TODO : !Add this
			return ("stc R3_BANK," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R4_BANK," + RegToString(Get2N(opcode)) + "           // no implementation
		static string d0000_nnnn_1100_0010(uint opcode, uint pc)
		{
			return ("stc R4_BANK," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R5_BANK," + RegToString(Get2N(opcode)) + "           // no implementation
		static string d0000_nnnn_1101_0010(uint opcode, uint pc)
		{
			return ("stc R5_BANK," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R6_BANK," + RegToString(Get2N(opcode)) + "           // no implementation
		static string d0000_nnnn_1110_0010(uint opcode, uint pc)
		{
			return ("stc R6_BANK," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R7_BANK," + RegToString(Get2N(opcode)) + "           // no implementation
		static string d0000_nnnn_1111_0010(uint opcode, uint pc)
		{
			return ("stc R7_BANK," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//braf " + RegToString(Get2N(opcode)) + "                  // no implementation
		static string d0000_nnnn_0010_0011(uint opcode, uint pc)
		{
			return "braf " + RegToString(Get2N(opcode)) + "";
		}


		//bsrf " + RegToString(Get2N(opcode)) + "                  // no implementation
		static string d0000_nnnn_0000_0011(uint opcode, uint pc)
		{
			return "bsrf " + RegToString(Get2N(opcode)) + "";
		}


		//movca.l R0, @" + RegToString(Get2N(opcode)) + "          // no implementation
		static string d0000_nnnn_1100_0011(uint opcode, uint pc)
		{
			return ("movca.l R0, @" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//ocbi @" + RegToString(Get2N(opcode)) + "                 // no implementation
		static string d0000_nnnn_1001_0011(uint opcode, uint pc)
		{
			return ("ocbi @" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//ocbp @" + RegToString(Get2N(opcode)) + "                 // no implementation
		static string d0000_nnnn_1010_0011(uint opcode, uint pc)
		{
			return ("ocbp @" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//ocbwb @" + RegToString(Get2N(opcode)) + "                // no implementation
		static string d0000_nnnn_1011_0011(uint opcode, uint pc)
		{
			return ("ocbwb @" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//pref @" + RegToString(Get2N(opcode)) + "                 // no implementation
		static string d0000_nnnn_1000_0011(uint opcode, uint pc)
		{//TODO : !Add this
			return ("pref @" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//mov.b " + RegToString(Get3N(opcode)) + ",@(R0," + RegToString(Get2N(opcode)) + ")   // no implementation
		static string d0000_nnnn_mmmm_0100(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "mov.b " + RegToString(Get3N(opcode)) + ",@(R0," + RegToString(Get2N(opcode)) + ")";
		}


		//mov.w " + RegToString(Get3N(opcode)) + ",@(R0," + RegToString(Get2N(opcode)) + ")   // no implementation
		static string d0000_nnnn_mmmm_0101(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.w " + RegToString(Get3N(opcode)) + ",@(R0," + RegToString(Get2N(opcode)) + ")";
		}


		//mov.l " + RegToString(Get3N(opcode)) + ",@(R0," + RegToString(Get2N(opcode)) + ")   // no implementation
		static string d0000_nnnn_mmmm_0110(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.l " + RegToString(Get3N(opcode)) + ",@(R0," + RegToString(Get2N(opcode)) + ")";
		}


		//mul.l " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "         // no implementation
		static string d0000_nnnn_mmmm_0111(uint opcode, uint pc)
		{//TODO : CHECK THIS
			return "mul.l " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}


		//clrmac                        // no implementation
		static string d0000_0000_0010_1000(uint opcode, uint pc)
		{
			return ("clrmac		//nimp");
		}


		//clrs                          // no implementation
		static string d0000_0000_0100_1000(uint opcode, uint pc)
		{
			return ("clrs		//nimp");
		}


		//clrt                          // no implementation
		static string d0000_0000_0000_1000(uint opcode, uint pc)
		{
			return ("clrt		//nimp");
		}


		//ldtlb                         // no implementation
		static string d0000_0000_0011_1000(uint opcode, uint pc)
		{
			return ("ldtlb		//nimp");
		}


		//sets                          // no implementation
		static string d0000_0000_0101_1000(uint opcode, uint pc)
		{
			return ("sets		//nimp");
		}


		//sett                          // no implementation
		static string d0000_0000_0001_1000(uint opcode, uint pc)
		{
			return ("sett		//nimp");
		}


		//div0u                         // no implementation
		static string d0000_0000_0001_1001(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "div0u";
		}


		//movt " + RegToString(Get2N(opcode)) + "                  // no implementation
		static string d0000_nnnn_0010_1001(uint opcode, uint pc)
		{
			return "movt " + RegToString(Get2N(opcode)) + "";
		}


		//nop                           // no implementation
		static string d0000_0000_0000_1001(uint opcode, uint pc)
		{
			//no operation xD XD .. i just love this opcode ..
			return "nop .. my favorite..";
		}


		//sts FPUL," + RegToString(Get2N(opcode)) + "              // no implementation
		static string d0000_nnnn_0101_1010(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "sts FPUL," + RegToString(Get2N(opcode)) + "";
		}


		//sts FPSCR," + RegToString(Get2N(opcode)) + "             // no implementation
		static string d0000_nnnn_0110_1010(uint opcode, uint pc)
		{
			return "sts FPSCR," + RegToString(Get2N(opcode)) + "";
		}


		//sts MACH," + RegToString(Get2N(opcode)) + "              // no implementation
		static string d0000_nnnn_0000_1010(uint opcode, uint pc)
		{
			return ("sts MACH," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//sts MACL," + RegToString(Get2N(opcode)) + "              // no implementation
		static string d0000_nnnn_0001_1010(uint opcode, uint pc)
		{//TODO : CHECK THIS
			return "sts MACL," + RegToString(Get2N(opcode)) + "";
		}


		//sts PR," + RegToString(Get2N(opcode)) + "                // no implementation
		static string d0000_nnnn_0010_1010(uint opcode, uint pc)
		{
			return ("sts PR," + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//rte                           // no implementation
		static string d0000_0000_0010_1011(uint opcode, uint pc)
		{
			return ("rte		//nimp");
		}


		//rts                           // no implementation
		static string d0000_0000_0000_1011(uint opcode, uint pc)
		{
			return "rts";
		}


		//sleep                         // no implementation
		static string d0000_0000_0001_1011(uint opcode, uint pc)
		{
			return ("sleep		//nimp");
		}
		

		//mov.b @(R0," + RegToString(Get3N(opcode)) + ")," + RegToString(Get2N(opcode)) + "   // no implementation
		static string d0000_nnnn_mmmm_1100(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.b @(R0," + RegToString(Get3N(opcode)) + ")," + RegToString(Get2N(opcode)) + " ";
		}


		//mov.w @(R0," + RegToString(Get3N(opcode)) + ")," + RegToString(Get2N(opcode)) + "   // no implementation
		static string d0000_nnnn_mmmm_1101(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "mov.w @(R0," + RegToString(Get3N(opcode)) + ")," + RegToString(Get2N(opcode)) + "";
		}


		//mov.l @(R0," + RegToString(Get3N(opcode)) + ")," + RegToString(Get2N(opcode)) + "   // no implementation
		static string d0000_nnnn_mmmm_1110(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.l @(R0," + RegToString(Get3N(opcode)) + ")," + RegToString(Get2N(opcode)) + "";
		}


		//mac.l @" + RegToString(Get3N(opcode)) + "+,@" + RegToString(Get2N(opcode)) + "+     // no implementation
		static string d0000_nnnn_mmmm_1111(uint opcode, uint pc)
		{//TODO : !Add this
			return "mac.l @" + RegToString(Get3N(opcode)) + "+,@" + RegToString(Get2N(opcode)) + "+";
		}

	}
	//1xxx
	public static unsafe partial class emu
	{
		//mov.l " + RegToString(Get3N(opcode)) + ",@(" + Get4N(opcode) + "," + RegToString(Get2N(opcode)) + ")
		static string d0001_nnnn_mmmm_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.l " + RegToString(Get3N(opcode)) + ",@(" + Get4N(opcode) + "," + RegToString(Get2N(opcode)) + ")";
		}
	}
	//2xxx
	public static unsafe partial class emu
	{
		//mov.b " + RegToString(Get3N(opcode)) + ",@" + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0010_nnnn_mmmm_0000(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.b " + RegToString(Get3N(opcode)) + ",@" + RegToString(Get2N(opcode)) + " ";
		}

		// mov.w " + RegToString(Get3N(opcode)) + ",@" + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0010_nnnn_mmmm_0001(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.w " + RegToString(Get3N(opcode)) + ",@" + RegToString(Get2N(opcode)) + "";
		}

		// mov.l " + RegToString(Get3N(opcode)) + ",@" + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0010_nnnn_mmmm_0010(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.l " + RegToString(Get3N(opcode)) + ",@" + RegToString(Get2N(opcode)) + " ";
		}
		// mov.b " + RegToString(Get3N(opcode)) + ",@-" + RegToString(Get2N(opcode)) + "       // no implemetation
		static string d0010_nnnn_mmmm_0100(uint opcode, uint pc)
		{
			return "mov.b " + RegToString(Get3N(opcode)) + ",@-" + RegToString(Get2N(opcode)) + "";

		}

		//mov.w " + RegToString(Get3N(opcode)) + ",@-" + RegToString(Get2N(opcode)) + "       // no implemetation
		static string d0010_nnnn_mmmm_0101(uint opcode, uint pc)
		{
			return "mov.w " + RegToString(Get3N(opcode)) + ",@-" + RegToString(Get2N(opcode)) + "";
		}

		//mov.l " + RegToString(Get3N(opcode)) + ",@-" + RegToString(Get2N(opcode)) + "       // no implemetation
		static string d0010_nnnn_mmmm_0110(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.l " + RegToString(Get3N(opcode)) + ",@-" + RegToString(Get2N(opcode)) + "";
		}
		// div0s " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0010_nnnn_mmmm_0111(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "div0s " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}

		// tst " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0010_nnnn_mmmm_1000(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "tst " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}

		//and " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0010_nnnn_mmmm_1001(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "and " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + " ";
		}

		//xor " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0010_nnnn_mmmm_1010(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "xor " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}

		//or " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "            // no implemetation
		static string d0010_nnnn_mmmm_1011(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "or " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}

		//cmp/str " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "       // no implemetation
		static string d0010_nnnn_mmmm_1100(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "cmp/str " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}

		//xtrct " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0010_nnnn_mmmm_1101(uint opcode, uint pc)
		{//TODO: unsore of proper emulation [26/4/05]
			return "xtrct " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}

		//mulu " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0010_nnnn_mmmm_1110(uint opcode, uint pc)
		{
			return ("mulu.w " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}

		//muls " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0010_nnnn_mmmm_1111(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return ("muls " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}
	}
	//3xxx 
	public static unsafe partial class emu
	{
		// cmp/eq " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0011_nnnn_mmmm_0000(uint opcode, uint pc)
		{
			return ("cmp/eq " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}

		// cmp/hs " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0011_nnnn_mmmm_0010(uint opcode, uint pc)
		{//ToDo : Check Me [26/4/05]
			return "cmp/hs " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + " ";
		}

		//cmp/ge " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0011_nnnn_mmmm_0011(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return ("cmp/ge " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}

		//div1 " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0011_nnnn_mmmm_0100(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return ("div1 " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}

		//dmulu.l " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "       // no implemetation
		static string d0011_nnnn_mmmm_0101(uint opcode, uint pc)
		{
			return ("dmulu.l " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}

		// cmp/hi " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0011_nnnn_mmmm_0110(uint opcode, uint pc)
		{
			return "cmp/hi " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}

		//cmp/gt " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0011_nnnn_mmmm_0111(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return ("cmp/gt " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}

		// sub " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0011_nnnn_mmmm_1000(uint opcode, uint pc)
		{
			return "sub " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}

		//subc " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0011_nnnn_mmmm_1010(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return ("subc " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}

		//subv " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0011_nnnn_mmmm_1011(uint opcode, uint pc)
		{
			return ("subv " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "		//nimp");
		}

		//add " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0011_nnnn_mmmm_1100(uint opcode, uint pc)
		{
			return "add " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}

		//dmuls.l " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "       // no implemetation
		static string d0011_nnnn_mmmm_1101(uint opcode, uint pc)
		{
			return ("dmuls.l " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}

		//addc " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0011_nnnn_mmmm_1110(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return ("addc " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}

		// addv " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0011_nnnn_mmmm_1111(uint opcode, uint pc)
		{
			return ("addv " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "		//nimp");
		}
	}
	//4xxx  
	public static unsafe partial class emu
	{
		//sts.l FPUL,@-" + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0100_nnnn_0101_0010(uint opcode, uint pc)
		{
			return ("sts.l FPUL,@-" + RegToString(Get2N(opcode)) + "");
		}


		//sts.l FPSCR,@-" + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0100_nnnn_0110_0010(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "sts.l FPSCR,@-" + RegToString(Get2N(opcode)) + "";
		}


		//sts.l MACH,@-" + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0100_nnnn_0000_0010(uint opcode, uint pc)
		{
			return ("sts.l MACH,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//sts.l MACL,@-" + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0100_nnnn_0001_0010(uint opcode, uint pc)
		{
			return ("sts.l MACL,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//sts.l PR,@-" + RegToString(Get2N(opcode)) + "            // no implemetation
		static string d0100_nnnn_0010_0010(uint opcode, uint pc)
		{//TODO : fAdd this
			return "sts.l PR,@-" + RegToString(Get2N(opcode)) + "";
		}


		//stc R0_BANK,@-" + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0100_nnnn_1000_0010(uint opcode, uint pc)
		{
			return ("stc R0_BANK,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R1_BANK,@-" + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0100_nnnn_1001_0010(uint opcode, uint pc)
		{
			return ("stc R1_BANK,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R2_BANK,@-" + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0100_nnnn_1010_0010(uint opcode, uint pc)
		{
			return ("stc R2_BANK,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R3_BANK,@-" + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0100_nnnn_1011_0010(uint opcode, uint pc)
		{
			return ("stc R3_BANK,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R4_BANK,@-" + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0100_nnnn_1100_0010(uint opcode, uint pc)
		{
			return ("stc R4_BANK,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R5_BANK,@-" + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0100_nnnn_1101_0010(uint opcode, uint pc)
		{
			return ("stc R5_BANK,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R6_BANK,@-" + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0100_nnnn_1110_0010(uint opcode, uint pc)
		{
			return ("stc R6_BANK,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc R7_BANK,@-" + RegToString(Get2N(opcode)) + "         // no implemetation
		static string d0100_nnnn_1111_0010(uint opcode, uint pc)
		{
			return ("stc R7_BANK,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc.l SR,@-" + RegToString(Get2N(opcode)) + "            // no implemetation
		static string d0100_nnnn_0000_0011(uint opcode, uint pc)
		{
			return ("stc.l SR,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc.l GBR,@-" + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0100_nnnn_0001_0011(uint opcode, uint pc)
		{
			return ("stc.l GBR,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc.l VBR,@-" + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0100_nnnn_0010_0011(uint opcode, uint pc)
		{
			return ("stc.l VBR,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc.l SSR,@-" + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0100_nnnn_0011_0011(uint opcode, uint pc)
		{
			return ("stc.l SSR,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//stc.l SPC,@-" + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0100_nnnn_0100_0011(uint opcode, uint pc)
		{
			return ("stc.l SPC,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//lds.l @" + RegToString(Get2N(opcode)) + "+,MACH          // no implemetation
		static string d0100_nnnn_0000_0110(uint opcode, uint pc)
		{
			return ("lds.l @" + RegToString(Get2N(opcode)) + "+,MACH		//nimp");
		}


		//lds.l @" + RegToString(Get2N(opcode)) + "+,MACL          // no implemetation
		static string d0100_nnnn_0001_0110(uint opcode, uint pc)
		{
			return ("lds.l @" + RegToString(Get2N(opcode)) + "+,MACL		//nimp");
		}


		//lds.l @" + RegToString(Get2N(opcode)) + "+,PR            // no implemetation
		static string d0100_nnnn_0010_0110(uint opcode, uint pc)
		{//TODO : hADD THIS
			return "lds.l @" + RegToString(Get2N(opcode)) + "+,PR";
		}


		//lds.l @" + RegToString(Get2N(opcode)) + "+,FPUL          // no implemetation
		static string d0100_nnnn_0101_0110(uint opcode, uint pc)
		{
			return ("lds.l @" + RegToString(Get2N(opcode)) + "+,FPUL");
		}


		//lds.l @" + RegToString(Get2N(opcode)) + "+,FPSCR         // no implemetation
		static string d0100_nnnn_0110_0110(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "lds.l @" + RegToString(Get2N(opcode)) + "+,FPSCR ";
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,SR            // no implemetation
		static string d0100_nnnn_0000_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,SR		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,GBR           // no implemetation
		static string d0100_nnnn_0001_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,GBR		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,VBR           // no implemetation
		static string d0100_nnnn_0010_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,VBR		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,SSR           // no implemetation
		static string d0100_nnnn_0011_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,SSR		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,SPC           // no implemetation
		static string d0100_nnnn_0100_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,SPC		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,R0_BANK       // no implemetation
		static string d0100_nnnn_1000_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,R0_BANK		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,R1_BANK       // no implemetation
		static string d0100_nnnn_1001_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,R1_BANK		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,R2_BANK       // no implemetation
		static string d0100_nnnn_1010_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,R2_BANK		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,R3_BANK       // no implemetation
		static string d0100_nnnn_1011_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,R3_BANK		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,R4_BANK       // no implemetation
		static string d0100_nnnn_1100_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,R4_BANK		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,R5_BANK       // no implemetation
		static string d0100_nnnn_1101_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,R5_BANK		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,R6_BANK       // no implemetation
		static string d0100_nnnn_1110_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,R6_BANK		//nimp");
		}


		//ldc.l @" + RegToString(Get2N(opcode)) + "+,R7_BANK       // no implemetation
		static string d0100_nnnn_1111_0111(uint opcode, uint pc)
		{
			return ("ldc.l @" + RegToString(Get2N(opcode)) + "+,R7_BANK		//nimp");
		}


		//lds " + RegToString(Get2N(opcode)) + ",MACH              // no implemetation
		static string d0100_nnnn_0000_1010(uint opcode, uint pc)
		{
			return ("lds " + RegToString(Get2N(opcode)) + ",MACH		//nimp");
		}


		//lds " + RegToString(Get2N(opcode)) + ",MACL              // no implemetation
		static string d0100_nnnn_0001_1010(uint opcode, uint pc)
		{
			return ("lds " + RegToString(Get2N(opcode)) + ",MACL		//nimp");
		}


		//lds " + RegToString(Get2N(opcode)) + ",PR                // no implemetation
		static string d0100_nnnn_0010_1010(uint opcode, uint pc)
		{//TODO : check this
			return "lds " + RegToString(Get2N(opcode)) + ",PR";
		}


		//lds " + RegToString(Get2N(opcode)) + ",FPUL              // no implemetation
		static string d0100_nnnn_0101_1010(uint opcode, uint pc)
		{//TODO : CHECK THIS
			return "lds " + RegToString(Get2N(opcode)) + ",FPUL";
		}


		//lds " + RegToString(Get2N(opcode)) + ",FPSCR             // no implemetation
		static string d0100_nnnn_0110_1010(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "lds " + RegToString(Get2N(opcode)) + ",FPSCR";
		}


		//ldc " + RegToString(Get2N(opcode)) + ",SR                // no implemetation
		static string d0100_nnnn_0000_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",SR");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",GBR               // no implemetation
		static string d0100_nnnn_0001_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",GBR		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",VBR               // no implemetation
		static string d0100_nnnn_0010_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",VBR		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",SSR               // no implemetation
		static string d0100_nnnn_0011_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",SSR		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",SPC               // no implemetation
		static string d0100_nnnn_0100_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",SPC		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",R0_BANK           // no implemetation
		static string d0100_nnnn_1000_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",R0_BANK		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",R1_BANK           // no implemetation
		static string d0100_nnnn_1001_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",R1_BANK		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",R2_BANK           // no implemetation
		static string d0100_nnnn_1010_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",R2_BANK		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",R3_BANK           // no implemetation
		static string d0100_nnnn_1011_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",R3_BANK		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",R4_BANK           // no implemetation
		static string d0100_nnnn_1100_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",R4_BANK		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",R5_BANK           // no implemetation
		static string d0100_nnnn_1101_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",R5_BANK		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",R6_BANK           // no implemetation
		static string d0100_nnnn_1110_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",R6_BANK		//nimp");
		}


		//ldc " + RegToString(Get2N(opcode)) + ",R7_BANK           // no implemetation
		static string d0100_nnnn_1111_1110(uint opcode, uint pc)
		{
			return ("ldc " + RegToString(Get2N(opcode)) + ",R7_BANK		//nimp");
		}


		//shll " + RegToString(Get2N(opcode)) + "                  // no implemetation
		static string d0100_nnnn_0000_0000(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return ("shll " + RegToString(Get2N(opcode)) + "");
		}


		//dt " + RegToString(Get2N(opcode)) + "                    // no implemetation
		static string d0100_nnnn_0001_0000(uint opcode, uint pc)
		{
			return "dt " + RegToString(Get2N(opcode)) + "";
		}


		//shal " + RegToString(Get2N(opcode)) + "                  // no implemetation
		static string d0100_nnnn_0010_0000(uint opcode, uint pc)
		{
			return ("shal " + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//shlr " + RegToString(Get2N(opcode)) + "                  // no implemetation
		static string d0100_nnnn_0000_0001(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "shlr " + RegToString(Get2N(opcode)) + "";
		}


		//cmp/pz " + RegToString(Get2N(opcode)) + "                // no implemetation
		static string d0100_nnnn_0001_0001(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return ("cmp/pz " + RegToString(Get2N(opcode)) + "");
		}


		//shar " + RegToString(Get2N(opcode)) + "                  // no implemetation
		static string d0100_nnnn_0010_0001(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05] x2
			return ("shar " + RegToString(Get2N(opcode)) + "");
		}


		//rotcl " + RegToString(Get2N(opcode)) + "                 // no implemetation
		static string d0100_nnnn_0010_0100(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return ("rotcl " + RegToString(Get2N(opcode)) + "");
		}


		//rotl " + RegToString(Get2N(opcode)) + "                  // no implemetation
		static string d0100_nnnn_0000_0100(uint opcode, uint pc)
		{
			return ("rotl " + RegToString(Get2N(opcode)) + "");
		}


		//cmp/pl " + RegToString(Get2N(opcode)) + "                // no implemetation
		static string d0100_nnnn_0001_0101(uint opcode, uint pc)
		{//TODO : !Add this
			return ("cmp/pl " + RegToString(Get2N(opcode)) + "");
		}


		//rotcr " + RegToString(Get2N(opcode)) + "                 // no implemetation
		static string d0100_nnnn_0010_0101(uint opcode, uint pc)
		{
			return ("rotcr " + RegToString(Get2N(opcode)) + "");
		}


		//rotr " + RegToString(Get2N(opcode)) + "                  // no implemetation
		static string d0100_nnnn_0000_0101(uint opcode, uint pc)
		{
			return ("rotr " + RegToString(Get2N(opcode)) + "");
		}


		//shll2 " + RegToString(Get2N(opcode)) + "                 // no implemetation
		static string d0100_nnnn_0000_1000(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "shll2 " + RegToString(Get2N(opcode)) + "";
		}


		//shll8 " + RegToString(Get2N(opcode)) + "                 // no implemetation
		static string d0100_nnnn_0001_1000(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "shll8 " + RegToString(Get2N(opcode)) + "";
		}


		//shll16 " + RegToString(Get2N(opcode)) + "                // no implemetation
		static string d0100_nnnn_0010_1000(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "shll16 " + RegToString(Get2N(opcode)) + "";
		}


		//shlr2 " + RegToString(Get2N(opcode)) + "                 // no implemetation
		static string d0100_nnnn_0000_1001(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "shlr2 " + RegToString(Get2N(opcode)) + "";
		}


		//shlr8 " + RegToString(Get2N(opcode)) + "                 // no implemetation
		static string d0100_nnnn_0001_1001(uint opcode, uint pc)
		{
			return ("shlr8 " + RegToString(Get2N(opcode)) + "");
		}


		//shlr16 " + RegToString(Get2N(opcode)) + "                // no implemetation
		static string d0100_nnnn_0010_1001(uint opcode, uint pc)
		{//TODO : CHECK ME
			return "shlr16 " + RegToString(Get2N(opcode)) + "";
		}


		//jmp @" + RegToString(Get2N(opcode)) + "                  // no implemetation
		static string d0100_nnnn_0010_1011(uint opcode, uint pc)
		{   //ToDo : Check Me [26/4/05]
			return "jmp @" + RegToString(Get2N(opcode)) + "";
		}


		//jsr @" + RegToString(Get2N(opcode)) + "                  // no implemetation
		static string d0100_nnnn_0000_1011(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "jsr @" + RegToString(Get2N(opcode)) + "";
		}


		//tas.b @" + RegToString(Get2N(opcode)) + "                // no implemetation
		static string d0100_nnnn_0001_1011(uint opcode, uint pc)
		{
			return ("tas.b @" + RegToString(Get2N(opcode)) + "");
		}


		//shad " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0100_nnnn_mmmm_1100(uint opcode, uint pc)
		{
			return ("shad " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}


		//shld " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0100_nnnn_mmmm_1101(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05] x2
			return ("shld " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}


		//mac.w @" + RegToString(Get3N(opcode)) + "+,@" + RegToString(Get2N(opcode)) + "+     // no implemetation
		static string d0100_nnnn_mmmm_1111(uint opcode, uint pc)
		{
			return ("mac.w @" + RegToString(Get3N(opcode)) + "+,@" + RegToString(Get2N(opcode)) + "+ 		//nimp");
		}
	}
	//5xxx
	public static unsafe partial class emu
	{
		//mov.l @(" + Get4N(opcode) + "," + RegToString(Get3N(opcode)) + ")," + RegToString(Get2N(opcode)) + "
		static string d0101_nnnn_mmmm_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.l @(" + Get4N(opcode) + "," + RegToString(Get3N(opcode)) + ")," + RegToString(Get2N(opcode)) + "";
		}
	}
	//6xxx
	public static unsafe partial class emu
	{
		//mov.b @" + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0110_nnnn_mmmm_0000(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.b @" + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}


		//mov.w @" + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0110_nnnn_mmmm_0001(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return ("mov.w @" + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}


		//mov.l @" + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0110_nnnn_mmmm_0010(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.l @" + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}


		//mov " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0110_nnnn_mmmm_0011(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}


		//mov.b @" + RegToString(Get3N(opcode)) + "+," + RegToString(Get2N(opcode)) + "       // no implemetation
		static string d0110_nnnn_mmmm_0100(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return ("mov.b @" + RegToString(Get3N(opcode)) + "+," + RegToString(Get2N(opcode)) + "");
		}


		//mov.w @" + RegToString(Get3N(opcode)) + "+," + RegToString(Get2N(opcode)) + "       // no implemetation
		static string d0110_nnnn_mmmm_0101(uint opcode, uint pc)
		{
			return ("mov.w @" + RegToString(Get3N(opcode)) + "+," + RegToString(Get2N(opcode)) + "");
		}


		//mov.l @" + RegToString(Get3N(opcode)) + "+," + RegToString(Get2N(opcode)) + "       // no implemetation
		static string d0110_nnnn_mmmm_0110(uint opcode, uint pc)
		{//TODO : hADD THIS
			return "mov.l @" + RegToString(Get3N(opcode)) + "+," + RegToString(Get2N(opcode)) + "";
		}


		//not " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0110_nnnn_mmmm_0111(uint opcode, uint pc)
		{
			return ("not " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}


		//swap.b " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0110_nnnn_mmmm_1000(uint opcode, uint pc)
		{
			return ("swap.b " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}


		//swap.w " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0110_nnnn_mmmm_1001(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "swap.w " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}


		//negc " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "          // no implemetation
		static string d0110_nnnn_mmmm_1010(uint opcode, uint pc)
		{
			return ("negc " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}


		//neg " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "           // no implemetation
		static string d0110_nnnn_mmmm_1011(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return "neg " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}


		//extu.b " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0110_nnnn_mmmm_1100(uint opcode, uint pc)
		{//TODO : CHECK THIS
			return "extu.b " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}


		//extu.w " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0110_nnnn_mmmm_1101(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "extu.w " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "";
		}


		//exts.b " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0110_nnnn_mmmm_1110(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return ("exts.b " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}


		//exts.w " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "        // no implemetation
		static string d0110_nnnn_mmmm_1111(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return ("exts.w " + RegToString(Get3N(opcode)) + "," + RegToString(Get2N(opcode)) + "");
		}
	}
	//7xxx
	public static unsafe partial class emu
	{
		//add #" + Get2B(opcode) + "," + RegToString(Get2N(opcode)) + "
		static string d0111_nnnn_iiii_iiii(uint opcode, uint pc)
		{//TODO : CHACK THIS
			return "add #" + Get2B(opcode) + "," + RegToString(Get2N(opcode)) + "";
		}
	}
	//8xxx
	public static unsafe partial class emu
	{
		// bf " + Get2B(opcode) + "                   // no implemetation
		static string d1000_1011_iiii_iiii(uint opcode, uint pc)
		{//ToDo : Check Me [26/4/05]
			return "bf " + UintToHex((uint)(((sbyte)(opcode  & 0xFF))*2 + 4 + pc )) ;
		}


		// bf.s " + Get2B(opcode) + "                 // no implemetation
		static string d1000_1111_iiii_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "bf.s " + UintToHex((uint)((((sbyte)(opcode & 0xFF)) << 1) + pc + 4));
		}


		// bt " + Get2B(opcode) + "                   // no implemetation
		static string d1000_1001_iiii_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "bt " + UintToHex((uint)((((sbyte)(opcode & 0xFF)) << 1) + pc + 4)) + "	;T=" + sr.T.ToString();
		}


		// bt.s " + Get2B(opcode) + "                 // no implemetation
		static string d1000_1101_iiii_iiii(uint opcode, uint pc)
		{
			return "bt.s " + UintToHex((uint)( (uint) (((sbyte)(opcode & 0xFF)) * 2 + pc + 4))) + "	;T=" + sr.T.ToString();
		}


		// cmp/eq #" + Get2B(opcode) + ",R0              // no implemetation
		static string d1000_1000_iiii_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "cmp/eq #" + Get2B(opcode) + ",R0";
		}


		// mov.b R0,@(" + Get4N(opcode) + "," + RegToString(Get3N(opcode)) + ")    // no implemetation
		static string d1000_0000_mmmm_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return ("mov.b R0,@(" + Get4N(opcode) + "," + RegToString(Get3N(opcode)) + ")");
		}


		// mov.w R0,@(" + Get4N(opcode) + "," + RegToString(Get3N(opcode)) + ")    // no implemetation
		static string d1000_0001_mmmm_iiii(uint opcode, uint pc)
		{//TODO : !Add this
			return ("mov.w R0,@(" + Get4N(opcode) + "," + RegToString(Get3N(opcode)) + ")");
		}


		// mov.b @(" + Get4N(opcode) + "," + RegToString(Get3N(opcode)) + "),R0    // no implemetation
		static string d1000_0100_mmmm_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05] x2
			return ("mov.b @(" + Get4N(opcode) + "," + RegToString(Get3N(opcode)) + "),R0");
		}


		// mov.w @(" + Get4N(opcode) + "," + RegToString(Get3N(opcode)) + "),R0    // no implemetation
		static string d1000_0101_mmmm_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return ("mov.w @(" + Get4N(opcode) + "," + RegToString(Get3N(opcode)) + "),R0");
		}
	}
	//9xxx
	public static unsafe partial class emu
	{
		//mov.w @(" + Get4N(opcode) + ",PC)," + RegToString(Get2N(opcode)) + "   
		static string d1001_nnnn_iiii_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.w @(" + (Get2B(opcode)<<1) + ",PC)," + RegToString(Get2N(opcode)) + "";
		}
	}
	//Axxx
	public static unsafe partial class emu
	{
		// bra " +Get12bit(opcode) + "
		static string d1010_iiii_iiii_iiii(uint opcode, uint pc)
		{//ToDo : Check Me [26/4/05]
			uint disp = Get12bit(opcode);
			return "bra " + UintToHex((uint)((((short)((opcode & 0x0FFF) << 4)) >> 3) + pc + 4));
		}
	}
	//Bxxx
	public static unsafe partial class emu
	{
		// bsr " +Get12bit(opcode) + "
		static string d1011_iiii_iiii_iiii(uint opcode, uint pc)
		{//ToDo : Check Me [26/4/05]
			uint disp = Get12bit(opcode);
			return "bsr " + UintToHex((uint)((((short)(disp << 4)) >> 3) + pc + 4));
		}
	}
	//Cxxx
	public static unsafe partial class emu
	{
		// mov.b R0,@(" + Get4N(opcode) + ",GBR)        // no implemetation
		static string d1100_0000_iiii_iiii(uint opcode, uint pc)
		{
			return ("mov.b R0,@(" + Get4N(opcode) + ",GBR)		//nimp");
		}


		// mov.w R0,@(" + Get4N(opcode) + ",GBR)        // no implemetation
		static string d1100_0001_iiii_iiii(uint opcode, uint pc)
		{
			return ("mov.w R0,@(" + Get4N(opcode) + ",GBR)		//nimp");
		}


		// mov.l R0,@(" + Get4N(opcode) + ",GBR)        // no implemetation
		static string d1100_0010_iiii_iiii(uint opcode, uint pc)
		{
			return ("mov.l R0,@(" + Get4N(opcode) + ",GBR)		//nimp");
		}


		// trapa #" + Get2B(opcode) + "                  // no implemetation
		static string d1100_0011_iiii_iiii(uint opcode, uint pc)
		{
			return ("trapa #" + Get2B(opcode) + "		//nimp");
		}


		// mov.b @(" + Get4N(opcode) + ",GBR),R0        // no implemetation
		static string d1100_0100_iiii_iiii(uint opcode, uint pc)
		{
			return ("mov.b @(" + Get4N(opcode) + ",GBR),R0		//nimp");
		}


		// mov.w @(" + Get4N(opcode) + ",GBR),R0        // no implemetation
		static string d1100_0101_iiii_iiii(uint opcode, uint pc)
		{
			return ("mov.w @(" + Get4N(opcode) + ",GBR),R0		//nimp");
		}


		// mov.l @(" + Get4N(opcode) + ",GBR),R0        // no implemetation
		static string d1100_0110_iiii_iiii(uint opcode, uint pc)
		{
			return ("mov.l @(" + Get4N(opcode) + ",GBR),R0		//nimp");
		}


		// mova @(" + Get4N(opcode) + ",PC),R0          // no implemetation
		static string d1100_0111_iiii_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mova @(" + Get4N(opcode) + ",PC),R0";
		}


		// tst #" + Get2B(opcode) + ",R0                 // no implemetation
		static string d1100_1000_iiii_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return ("tst #" + Get2B(opcode) + ",R0");
		}


		// and #" + Get2B(opcode) + ",R0                 // no implemetation
		static string d1100_1001_iiii_iiii(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return ("and #" + Get2B(opcode) + ",R0");
		}


		// xor #" + Get2B(opcode) + ",R0                 // no implemetation
		static string d1100_1010_iiii_iiii(uint opcode, uint pc)
		{
			return ("xor #" + Get2B(opcode) + ",R0");
		}


		// or #" + Get2B(opcode) + ",R0                  // no implemetation
		static string d1100_1011_iiii_iiii(uint opcode, uint pc)
		{//ToDo : Check This [26/4/05]
			return ("or #" + Get2B(opcode) + ",R0");
		}


		// tst.b #" + Get2B(opcode) + ",@(R0,GBR)        // no implemetation
		static string d1100_1100_iiii_iiii(uint opcode, uint pc)
		{
			return ("tst.b #" + Get2B(opcode) + ",@(R0,GBR)		//nimp");
		}


		// and.b #" + Get2B(opcode) + ",@(R0,GBR)        // no implemetation
		static string d1100_1101_iiii_iiii(uint opcode, uint pc)
		{
			return ("and.b #" + Get2B(opcode) + ",@(R0,GBR)		//nimp");
		}


		// xor.b #" + Get2B(opcode) + ",@(R0,GBR)        // no implemetation
		static string d1100_1110_iiii_iiii(uint opcode, uint pc)
		{
			return ("xor.b #" + Get2B(opcode) + ",@(R0,GBR)		//nimp");
		}


		// or.b #" + Get2B(opcode) + ",@(R0,GBR)         // no implemetation
		static string d1100_1111_iiii_iiii(uint opcode, uint pc)
		{
			return ("or.b #" + Get2B(opcode) + ",@(R0,GBR)		//nimp");
		}
	}
	//Dxxx
	public static unsafe partial class emu
	{
		// mov.l @(" + Get4N(opcode) + ",PC)," + RegToString(Get2N(opcode)) + "    
		static string d1101_nnnn_iiii_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov.l @(" +( Get2B(opcode)<<2) + ",PC)," + RegToString(Get2N(opcode)) + "";
		}
	}
	//Exxx
	public static unsafe partial class emu
	{
		// mov #" + Get2B(opcode) + "," + RegToString(Get2N(opcode)) + "
		static string d1110_nnnn_iiii_iiii(uint opcode, uint pc)
		{//TODO : Check This [26/4/05]
			return "mov #" + Get2B(opcode) + "," + RegToString(Get2N(opcode)) + "";
		}
	}
	//Fxxx
	public static unsafe partial class emu
	{
		//fadd <FREG_M>,<FREG_N>        no implemetation
		static string d1111_nnnn_mmmm_0000(uint opcode, uint pc)
		{//TODO : CHECK THIS PR FP
			return "fadd <FREG_M>,<FREG_N>";
		}


		//fsub <FREG_M>,<FREG_N>        no implemetation
		static string d1111_nnnn_mmmm_0001(uint opcode, uint pc)
		{
			return ("fsub <FREG_M>,<FREG_N>		//nimp");
		}


		//fmul <FREG_M>,<FREG_N>        no implemetation
		static string d1111_nnnn_mmmm_0010(uint opcode, uint pc)
		{
			return "fmul <FREG_M>,<FREG_N>";
		}


		//fdiv <FREG_M>,<FREG_N>        no implemetation
		static string d1111_nnnn_mmmm_0011(uint opcode, uint pc)
		{//TODO : CHECK THIS + PRMODE FP
			return "fdiv <FREG_M>,<FREG_N>";

		}


		//fcmp/eq <FREG_M>,<FREG_N>     no implemetation
		static string d1111_nnnn_mmmm_0100(uint opcode, uint pc)
		{
			return ("fcmp/eq <FREG_M>,<FREG_N>		//nimp");
		}


		//fcmp/gt <FREG_M>,<FREG_N>     no implemetation
		static string d1111_nnnn_mmmm_0101(uint opcode, uint pc)
		{
			return ("fcmp/gt <FREG_M>,<FREG_N>		//nimp");
		}


		//fmov.s @(R0," + RegToString(Get3N(opcode)) + "),<FREG_N> no implemetation
		static string d1111_nnnn_mmmm_0110(uint opcode, uint pc)
		{
			return ("fmov.s @(R0," + RegToString(Get3N(opcode)) + "),<FREG_N>		//nimp");
		}


		//fmov.s <FREG_M>,@(R0," + RegToString(Get2N(opcode)) + ") no implemetation
		static string d1111_nnnn_mmmm_0111(uint opcode, uint pc)
		{
			return ("fmov.s <FREG_M>,@(R0," + RegToString(Get2N(opcode)) + ")		//nimp");
		}


		//fmov.s @" + RegToString(Get3N(opcode)) + ",<FREG_N>      no implemetation
		public static unsafe string d1111_nnnn_mmmm_1000(uint opcode, uint pc)
		{//TODO : CHECK PR SZ FP
			return "fmov.s @" + RegToString(Get3N(opcode)) + ",<FREG_N>";
		}


		//fmov.s @" + RegToString(Get3N(opcode)) + "+,<FREG_N>     no implemetation
		static string d1111_nnnn_mmmm_1001(uint opcode, uint pc)
		{
			return ("fmov.s @" + RegToString(Get3N(opcode)) + "+,<FREG_N>		//nimp");
		}


		//fmov.s <FREG_M>,@" + RegToString(Get2N(opcode)) + "      no implemetation
		public static unsafe string d1111_nnnn_mmmm_1010(uint opcode, uint pc)
		{//	TODO : hadd this
			return "fmov.s <FREG_M>,@" + RegToString(Get2N(opcode)) + "";
		}

		//fmov.s <FREG_M>,@-" + RegToString(Get2N(opcode)) + "     no implemetation
		static string d1111_nnnn_mmmm_1011(uint opcode, uint pc)
		{
			return ("fmov.s <FREG_M>,@-" + RegToString(Get2N(opcode)) + "		//nimp");
		}


		//fmov <FREG_M>,<FREG_N>        no implemetation
		static string d1111_nnnn_mmmm_1100(uint opcode, uint pc)
		{//TODO : checkthis
			return "fmov <FREG_M>,<FREG_N>";
		}


		//fabs <FREG_N>                 no implemetation
		static string d1111_nnnn_0101_1101(uint opcode, uint pc)
		{
			return ("fabs <FREG_N>		//nimp");
		}

		// FSCA FPUL, DRn//F0FD//1111_nnnn_1111_1101
		static string d1111_nnn0_1111_1101(uint opcode, uint pc)
		{
			return "FSCA FPUL, <DR_N>";
		}

		//fcnvds <DR_N>,FPUL            no implemetation
		static string d1111_nnnn_1011_1101(uint opcode, uint pc)
		{
			return ("fcnvds <DR_N>,FPUL		//nimp");
		}


		//fcnvsd FPUL,<DR_N>            no implemetation
		static string d1111_nnnn_1010_1101(uint opcode, uint pc)
		{
			return ("fcnvsd FPUL,<DR_N>		//nimp");
		}

		//fipr <FV_M>,<FV_N>            
		static string d1111_nnmm_1110_1101(uint opcode, uint pc)
		{
			return ("fipr <FV_M>,<FV_N>		//nimp");
		}


		//fldi0 <FREG_N>                no implemetation
		static string d1111_nnnn_1000_1101(uint opcode, uint pc)
		{
			return ("fldi0 <FREG_N>		//nimp");
		}


		//fldi1 <FREG_N>                no implemetation
		static string d1111_nnnn_1001_1101(uint opcode, uint pc)
		{
			return ("fldi1 <FREG_N>		//nimp");
		}


		//flds <FREG_N>,FPUL            no implemetation
		static string d1111_nnnn_0001_1101(uint opcode, uint pc)
		{
			return ("flds <FREG_N>,FPUL		//nimp");
		}


		//float FPUL,<FREG_N>           no implemetation
		static string d1111_nnnn_0010_1101(uint opcode, uint pc)
		{//TODO : CHECK THIS (FP)
			return "float FPUL,<FREG_N>";
		}


		//fneg <FREG_N>                 no implemetation
		static string d1111_nnnn_0100_1101(uint opcode, uint pc)
		{
			return ("fneg <FREG_N>		//nimp");
		}


		//frchg                         no implemetation
		static string d1111_1011_1111_1101(uint opcode, uint pc)
		{
			return ("frchg		//nimp");
		}


		//fschg                         no implemetation
		static string d1111_0011_1111_1101(uint opcode, uint pc)
		{
			return ("fschg		//nimp");
		}

		//fsqrt <FREG_N>                
		static string d1111_nnnn_0110_1101(uint opcode, uint pc)
		{
			return ("fsqrt <FREG_N>		//nimp");
		}


		//ftrc <FREG_N>, FPUL           no implemetation
		static string d1111_nnnn_0011_1101(uint opcode, uint pc)
		{
			return "ftrc <FREG_N>, FPUL";
		}


		//fsts FPUL,<FREG_N>            no implemetation
		static string d1111_nnnn_0000_1101(uint opcode, uint pc)
		{
			return ("fsts FPUL,<FREG_N>		//nimp");
		}


		//fmac <FREG_0>,<FREG_M>,<FREG_N> no implemetation
		static string d1111_nnnn_mmmm_1110(uint opcode, uint pc)
		{
			return ("fmac <FREG_0>,<FREG_M>,<FREG_N>		//nimp");
		}


		//ftrv xmtrx,<FV_N>             no implemetation
		static string d1111_nn01_1111_1101(uint opcode, uint pc)
		{
			return ("ftrv xmtrx,<FV_N>		//nimp");
		}
	}

	#endregion
}
