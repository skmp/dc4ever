using System;
using System.Collections.Generic;
using System.Text;

namespace DC4Ever
{
	class sh4_opcodes_test
	{
		public static void testop(uint opcode)
		{
			#region interpreter
                switch (opcode>>12)//proc opcode
                {
                    case 0x0://finished
						#region case 0x0
					    switch (opcode&0xf)
					    {
						    case 0x0://0000
                                iInvalidOpcode();
							    break;
						    case 0x1://0001
                                iInvalidOpcode();
                                break;
						    case 0x2://0010
							    #region case 0x2 multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        check_str("0","nnnn","0","2",opcode);
										//check_n(opcode,0x002);
								        break;
							        case 0x1://0001
                                        check_str("0","nnnn","1","2",opcode);
										//check_n(opcode,0x0012);
                                        break;
							        case 0x2://0010
								        check_str("0","nnnn","2","2",opcode);
										//check_n(opcode,0x0022);
								        break;
							        case 0x3://0011
								        check_str("0","nnnn","3","2",opcode);
										//check_n(opcode,0x0032);
								        break;
							        case 0x4://0100
								        check_str("0","nnnn","4","2",opcode);
										//check_n(opcode,0x0042);
								        break;
							        case 0x8://1000
								        check_str("0","nnnn","8","2",opcode);
										//check_n(opcode,0x0082);
								        break;
							        case 0x9://1001
								        check_str("0","nnnn","9","2",opcode);
										//check_n(opcode,0x0092);
								        break;
							        case 0xA://1010
								        check_str("0","nnnn","A","2",opcode);
										//check_n(opcode,0x00A2);
								        break;
							        case 0xB://1011
								        check_str("0","nnnn","B","2",opcode);
										//check_n(opcode,0x00B2);
								        break;
							        case 0xC://1100
								        check_str("0","nnnn","C","2",opcode);
										//check_n(opcode,0x00C2);
								        break;
							        case 0xD://1101
								        check_str("0","nnnn","D","2",opcode);
										//check_n(opcode,0x00D2);
								        break;
							        case 0xE://1110
								        check_str("0","nnnn","E","2",opcode);
										//check_n(opcode,0x00E2);
								        break;
							        case 0xF://1111
								        check_str("0","nnnn","F","2",opcode);
										//check_n(opcode,0x00F2);
								        break;
							        default:
                                        iInvalidOpcode();
                                        break;	
						        }
							    #endregion
							    break;
						    case 0x3://0011
							    #region case 0x3 multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        check_str("0","nnnn","0","3",opcode);
										//check_n(opcode,0x0003);
								        break;
							        case 0x2://0010
								        check_str("0","nnnn","2","3",opcode);
										//check_n(opcode,0x0023);
								        break;
							        case 0x8://1000
								        check_str("0","nnnn","8","3",opcode);
										//check_n(opcode,0x0083);
								        break;
							        case 0x9://1001
								        check_str("0","nnnn","9","3",opcode);
										//check_n(opcode,0x0093);
								        break;
							        case 0xA://1010
								        check_str("0","nnnn","A","3",opcode);
										//check_n(opcode,0x00A3);
								        break;
							        case 0xB://1011
								        check_str("0","nnnn","B","3",opcode);
										//check_n(opcode,0x00B3);
								        break;
							        case 0xC://1100
								        check_str("0","nnnn","C","3",opcode);
										//check_n(opcode,0x00C3);
								        break;
							        default:
                                        iInvalidOpcode();
                                        break;
						        }
							    #endregion
							    break;
						    case 0x4://0100
							    check_str("0","nnnn","mmmm","4",opcode);
								//check_nm(opcode,0x0004);
							    break;
						    case 0x5://0101
							    check_str("0","nnnn","mmmm","5",opcode);
								//check_nm(opcode,0x0005);
							    break;
						    case 0x6://0110
							    check_str("0","nnnn","mmmm","6",opcode);
								//check_nm(opcode,0x0006);
							    break;
						    case 0x7://0111
							    check_str("0","nnnn","mmmm","7",opcode);
								//check_nm(opcode,0x0007);
							    break;
						    case 0x8://1000
							    #region case 0x8 multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        check_str("0","0","0","1000",opcode);
										//check_(opcode,0x0008);
								        break;
							        case 0x1://0001
								        check_str("0","0","1","1000",opcode);
										//check_(opcode,0x0018);
								        break;
							        case 0x2://0010
								        check_str("0","0","2","1000",opcode);
										//check_(opcode,0x0028);
								        break;
							        case 0x3://0011
								        check_str("0","0","3","1000",opcode);
										//check_(opcode,0x0038);
								        break;
							        case 0x4://0100
								        check_str("0","0","4","1000",opcode);
										//check_(opcode,0x0048);
								        break;
							        case 0x5://0101
								        check_str("0","0","5","1000",opcode);
										//check_(opcode,0x0058);
								        break;
							        default:
                                        iInvalidOpcode();
                                        break;
						        }
							    #endregion
							    break;
						    case 0x9://1001
							    #region case 0x9 multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        check_str("0","0","0","1001",opcode);
										//check_(opcode,0x0006);
								        break;
							        case 0x1://0001
								        check_str("0","0","1","1001",opcode);
										//check_(opcode,0x0016);
								        break;
							        case 0x2://0010
								        check_str("0","nnnn","2","1001",opcode);
										//check_(opcode,0x0026);
								        break;
							        default:
                                        iInvalidOpcode();
                                        break;
						        }
							    #endregion
							    break;
						    case 0xA://1010
							    #region case 0xA multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        check_str("0","nnnn","0","1010",opcode);
								        break;
							        case 0x1://0001
								        check_str("0","nnnn","1","1010",opcode);
								        break;
							        case 0x2://0010
								        check_str("0","nnnn","2","1010",opcode);
								        break;
							        case 0x5://0101
								        check_str("0","nnnn","5","1010",opcode);
								        break;
							        case 0x6://0110
								        check_str("0","nnnn","6","1010",opcode);
								        break;
							        default:
                                        iInvalidOpcode();
                                        break;
						        }
							    #endregion
							    break;
						    case 0xB://1011
							    #region case 0xB multi opcodes
						        switch ((opcode>>4)&0xf)
						        {
							        case 0x0://0000
								        check_str("0","0","0","1011",opcode);
								        break;
							        case 0x1://0001
								        check_str("0","0","1","1011",opcode);
								        break;
							        case 0x2://0010
								        check_str("0","0","2","1011",opcode);
								        break;
							        default:
								        iInvalidOpcode();
								        break;
						        }
							    #endregion
							    break;
						    case 0xC://1100
							    check_str("0","nnnn","mmmm","1100",opcode);
							    break;
						    case 0xD://1101
							    check_str("0","nnnn","mmmm","1101",opcode);
							    break;
						    case 0xE://1110
							    check_str("0","nnnn","mmmm","1110",opcode);
							    break;
						    case 0xF://1111
							    check_str("0","nnnn","mmmm","1111",opcode);
							    break;
					    }
						#endregion
						break;
                    case 0x1://finished
						check_str("1","nnnn","mmmm","iiii",opcode);
						break;
					case 0x2://finished
						#region case 0x2
					switch (opcode&0xf)
					{
						case 0x0://0000
							check_str("2","nnnn","mmmm","0",opcode);
							break;
						case 0x1://0001
							check_str("2","nnnn","mmmm","1",opcode);
							break;
						case 0x2://0010
							check_str("2","nnnn","mmmm","2",opcode);
							break;
						case 0x4://0100
							check_str("2","nnnn","mmmm","4",opcode);
							break;
						case 0x5://0101
							check_str("2","nnnn","mmmm","5",opcode);
							break;
						case 0x6://0110
							check_str("2","nnnn","mmmm","6",opcode);
							break;
						case 0x7://0111
							check_str("2","nnnn","mmmm","7",opcode);
							break;
						case 0x8://1000
							check_str("2","nnnn","mmmm","1000",opcode);
							break;
						case 0x9://1001
							check_str("2","nnnn","mmmm","1001",opcode);
							break;
						case 0xA://1010
							check_str("2","nnnn","mmmm","1010",opcode);
							break;
						case 0xB://1011
							check_str("2","nnnn","mmmm","1011",opcode);
							break;
						case 0xC://1100
							check_str("2","nnnn","mmmm","1100",opcode);
							break;
						case 0xD://1101
							check_str("2","nnnn","mmmm","1101",opcode);
							break;
						case 0xE://1110
							check_str("2","nnnn","mmmm","1110",opcode);
							break;
						case 0xF://1111
							check_str("2","nnnn","mmmm","1111",opcode);
							break;
						default:
							iInvalidOpcode();
							break;
					}
					#endregion
						break;
					case 0x3://finished
						#region case 0x3
					switch (opcode&0xf)
					{
						case 0x0://0000
							check_str("3","nnnn","mmmm","0",opcode);
							break;
						case 0x2://0010
							check_str("3","nnnn","mmmm","2",opcode);
							break;
						case 0x3://0011
							check_str("3","nnnn","mmmm","3",opcode);
							break;
						case 0x4://0100
							check_str("3","nnnn","mmmm","4",opcode);
							break;
						case 0x5://0101
							check_str("3","nnnn","mmmm","5",opcode);
							break;
						case 0x6://0110
							check_str("3","nnnn","mmmm","6",opcode);
							break;
						case 0x7://0111
							check_str("3","nnnn","mmmm","7",opcode);
							break;
						case 0x8://1000
							check_str("3","nnnn","mmmm","1000",opcode);
							break;
						case 0xA://1010
							check_str("3","nnnn","mmmm","1010",opcode);
							break;
						case 0xB://1011
							check_str("3","nnnn","mmmm","1011",opcode);
							break;
						case 0xC://1100
							check_str("3","nnnn","mmmm","1100",opcode);
							break;
						case 0xD://1101
							check_str("3","nnnn","mmmm","1101",opcode);
							break;
						case 0xE://1110
							check_str("3","nnnn","mmmm","1110",opcode);
							break;
						case 0xF://1111
							check_str("3","nnnn","mmmm","1111",opcode);
							break;
						default:
							iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0x4://finished
						#region case 0x4
					switch (opcode&0xf)
					{
						case 0x0://0000
							#region 0x0 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_0000
								check_str("4","nnnn","0","0",opcode);
								break;
							case 0x1://4_xxxx_1_0000
								check_str("4","nnnn","1","0",opcode);
								break;
							case 0x2://4_xxxx_2_0000
								check_str("4","nnnn","2","0",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x1://0001
							#region 0x1 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_0001
								check_str("4","nnnn","0","1",opcode);
								break;
							case 0x1://4_xxxx_1_0001
								check_str("4","nnnn","1","1",opcode);
								break;
							case 0x2://4_xxxx_2_0001
								check_str("4","nnnn","2","1",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x2://0010
							#region 0x2 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_0010
								check_str("4","nnnn","0","2",opcode);
								break;
							case 0x1://4_xxxx_1_0010
								check_str("4","nnnn","1","2",opcode);
								break;
							case 0x2://4_xxxx_2_0010
								check_str("4","nnnn","2","2",opcode);
								break;
							case 0x5://4_xxxx_5_0010
								check_str("4","nnnn","5","2",opcode);
								break;
							case 0x6://4_xxxx_6_0010
								check_str("4","nnnn","6","2",opcode);
								break;
							case 0x8://4_xxxx_8_0010
								check_str("4","nnnn","8","2",opcode);
								break;
							case 0x9://4_xxxx_9_0010
								check_str("4","nnnn","9","2",opcode);
								break;
							case 0xA://4_xxxx_A_0010
								check_str("4","nnnn","A","2",opcode);
								break;
							case 0xB://4_xxxx_B_0010
								check_str("4","nnnn","B","2",opcode);
								break;
							case 0xC://4_xxxx_C_0010
								check_str("4","nnnn","C","2",opcode);
								break;
							case 0xD://4_xxxx_D_0010
								check_str("4","nnnn","D","2",opcode);
								break;
							case 0xE://4_xxxx_E_0010
								check_str("4","nnnn","E","2",opcode);
								break;
							case 0xF://4_xxxx_F_0010
								check_str("4","nnnn","F","2",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x3://0011
							#region 0x3 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_0011
								check_str("4","nnnn","0","3",opcode);
								break;
							case 0x1://4_xxxx_1_0011
								check_str("4","nnnn","1","3",opcode);
								break;
							case 0x2://4_xxxx_2_0011
								check_str("4","nnnn","2","3",opcode);
								break;
							case 0x3://4_xxxx_3_0011
								check_str("4","nnnn","3","3",opcode);
								break;
							case 0x4://4_xxxx_4_0011
								check_str("4","nnnn","4","3",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x4://0100
							#region 0x4 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_0100
								check_str("4","nnnn","0","4",opcode);
								break;
							case 0x2://4_xxxx_2_0100
								check_str("4","nnnn","2","4",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x5://0101
							#region 0x5 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_0101
								check_str("4","nnnn","0","5",opcode);
								break;
							case 0x1://4_xxxx_1_0101
								check_str("4","nnnn","1","5",opcode);
								break;
							case 0x2://4_xxxx_2_0101
								check_str("4","nnnn","2","5",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x6://0110
							#region 0x6 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_0110
								check_str("4","nnnn","0","6",opcode);
								break;
							case 0x1://4_xxxx_1_0110
								check_str("4","nnnn","1","6",opcode);
								break;
							case 0x2://4_xxxx_2_0110
								check_str("4","nnnn","2","6",opcode);
								break;
							case 0x5://4_xxxx_5_0110
								check_str("4","nnnn","5","6",opcode);
								break;
							case 0x6://4_xxxx_6_0110
								check_str("4","nnnn","6","6",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x7://0111
							#region 0x7 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_0111
								check_str("4","nnnn","0","7",opcode);
								break;
							case 0x1://4_xxxx_1_0111
								check_str("4","nnnn","1","7",opcode);
								break;
							case 0x2://4_xxxx_2_0111
								check_str("4","nnnn","2","7",opcode);
								break;
							case 0x3://4_xxxx_3_0111
								check_str("4","nnnn","3","7",opcode);
								break;
							case 0x4://4_xxxx_4_0111
								check_str("4","nnnn","4","7",opcode);
								break;
							case 0x8://4_xxxx_8_0111
								check_str("4","nnnn","8","7",opcode);
								break;
							case 0x9://4_xxxx_9_0111
								check_str("4","nnnn","9","7",opcode);
								break;
							case 0xA://4_xxxx_A_0111
								check_str("4","nnnn","A","7",opcode);
								break;
							case 0xB://4_xxxx_B_0111
								check_str("4","nnnn","B","7",opcode);
								break;
							case 0xC://4_xxxx_C_0111
								check_str("4","nnnn","C","7",opcode);
								break;
							case 0xD://4_xxxx_D_0111
								check_str("4","nnnn","D","7",opcode);
								break;
							case 0xE://4_xxxx_E_0111
								check_str("4","nnnn","E","7",opcode);
								break;
							case 0xF://4_xxxx_F_0111
								check_str("4","nnnn","F","7",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x8://1000
							#region 0x8 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_1000
								check_str("4","nnnn","0","1000",opcode);
								break;
							case 0x1://4_xxxx_1_1000
								check_str("4","nnnn","1","1000",opcode);
								break;
							case 0x2://4_xxxx_2_1000
								check_str("4","nnnn","2","1000",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0x9://1001
							#region 0x9 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_1001
								check_str("4","nnnn","0","1001",opcode);
								break;
							case 0x1://4_xxxx_1_1001
								check_str("4","nnnn","1","1001",opcode);
								break;
							case 0x2://4_xxxx_2_1001
								check_str("4","nnnn","2","1001",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;

						case 0xA://1010
							#region 0x9 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_1010
								check_str("4","nnnn","0","1010",opcode);
								break;
							case 0x1://4_xxxx_1_1010
								check_str("4","nnnn","1","1010",opcode);
								break;
							case 0x2://4_xxxx_2_1010
								check_str("4","nnnn","2","1010",opcode);
								break;
							case 0x5://4_xxxx_5_1010
								check_str("4","nnnn","5","1010",opcode);
								break;
							case 0x6://4_xxxx_6_1010
								check_str("4","nnnn","6","1010",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;

						case 0xB://1011
							#region 0xB multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_1011
								check_str("4","nnnn","0","1011",opcode);
								break;
							case 0x1://4_xxxx_1_1011
								check_str("4","nnnn","1","1011",opcode);
								break;
							case 0x2://4_xxxx_2_1011
								check_str("4","nnnn","2","1011",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0xC://1100
							check_str("4","nnnn","mmmm","1100",opcode);
							break;
						case 0xD://1101
							check_str("4","nnnn","mmmm","1101",opcode);
							break;
						case 0xE://1110
							#region 0xE multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://4_xxxx_0_1110
								check_str("4","nnnn","0","1110",opcode);
								break;
							case 0x1://4_xxxx_1_1110
								check_str("4","nnnn","1","1110",opcode);
								break;
							case 0x2://4_xxxx_2_1110
								check_str("4","nnnn","2","1110",opcode);
								break;
							case 0x3://4_xxxx_3_1110
								check_str("4","nnnn","3","1110",opcode);
								break;
							case 0x4://4_xxxx_4_1110
								check_str("4","nnnn","4","1110",opcode);
								break;
							case 0x8://4_xxxx_8_1110
								check_str("4","nnnn","8","1110",opcode);
								break;
							case 0x9://4_xxxx_9_1110
								check_str("4","nnnn","9","1110",opcode);
								break;
							case 0xA://4_xxxx_A_1110
								check_str("4","nnnn","A","1110",opcode);
								break;
							case 0xB://4_xxxx_B_1110
								check_str("4","nnnn","B","1110",opcode);
								break;
							case 0xC://4_xxxx_C_1110
								check_str("4","nnnn","C","1110",opcode);
								break;
							case 0xD://4_xxxx_D_1110
								check_str("4","nnnn","D","1110",opcode);
								break;
							case 0xE://4_xxxx_E_1110
								check_str("4","nnnn","E","1110",opcode);
								break;
							case 0xF://4_xxxx_F_1110
								check_str("4","nnnn","F","1110",opcode);
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion 
							break;
						case 0xF://1111
							check_str("4","nnnn","mmmm","1111",opcode);
							break;
						default:
							iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0x5://finished
						check_str("5","nnnn","mmmm","iiii",opcode);
						break;
					case 0x6://finished
						#region case 0x6
					switch (opcode&0xf)
					{
						case 0x0://0000
							check_str("6","nnnn","mmmm","0",opcode);
							break;
						case 0x1://0001
							check_str("6","nnnn","mmmm","1",opcode);
							break;
						case 0x2://0010
							check_str("6","nnnn","mmmm","2",opcode);
							break;
						case 0x3://0011
							check_str("6","nnnn","mmmm","3",opcode);
							break;
						case 0x4://0100
							check_str("6","nnnn","mmmm","4",opcode);
							break;
						case 0x5://0101
							check_str("6","nnnn","mmmm","5",opcode);
							break;
						case 0x6://0110
							check_str("6","nnnn","mmmm","6",opcode);
							break;
						case 0x7://0111
							check_str("6","nnnn","mmmm","7",opcode);
							break;
						case 0x8://1000
							check_str("6","nnnn","mmmm","1000",opcode);
							break;
						case 0x9://1001
							check_str("6","nnnn","mmmm","1001",opcode);
							break;
						case 0xA://1010
							check_str("6","nnnn","mmmm","1010",opcode);
							break;
						case 0xB://1011
							check_str("6","nnnn","mmmm","1011",opcode);
							break;
						case 0xC://1100
							check_str("6","nnnn","mmmm","1100",opcode);
							break;
						case 0xD://1101
							check_str("6","nnnn","mmmm","1101",opcode);
							break;
						case 0xE://1110
							check_str("6","nnnn","mmmm","1110",opcode);
							break;
						case 0xF://1111
							check_str("6","nnnn","mmmm","1111",opcode);
							break;
						default:
							iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0x7://finished
						check_str("7","nnnn","iiii","iiii",opcode);
						break;
					case 0x8://finished
						#region case 0x8
					switch ((opcode>>8)&0xf)
					{
						case 0x0://0000
							check_str("8","0","mmmm","iiii",opcode);
							break;
						case 0x1://0001
							check_str("8","1","mmmm","iiii",opcode);
							break;
						case 0x4://0100
							check_str("8","4","mmmm","iiii",opcode);
							break;
						case 0x5://0101
							check_str("8","5","mmmm","iiii",opcode);
							break;
						case 0x8://1000
							check_str("8","8","iiii","iiii",opcode);
							break;
						case 0x9://1001
							check_str("8","9","iiii","iiii",opcode);
							break;
						case 0xB://1011
							check_str("8","B","iiii","iiii",opcode);
                            break;
						case 0xD://1101
							check_str("8","D","iiii","iiii",opcode);
							break;
						case 0xF://1111
							check_str("8","F","iiii","iiii",opcode);
							break;
						default:
							iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0x9://finished
						check_str("9","nnnn","iiii","iiii",opcode);
						break;
					case 0xA://finished
						check_str("A","iiii","iiii","iiii",opcode);
						break;
					case 0xB://finished
						check_str("B","iiii","iiii","iiii",opcode);
						break;
					case 0xC://finished
						#region case 0xC
					switch ((opcode>>8)&0xf)
					{
						case 0x0://0000
							check_str("C","0","iiii","iiii",opcode);
							break;
						case 0x1://0001
							check_str("C","1","iiii","iiii",opcode);
							break;
						case 0x2://0010
							check_str("C","2","iiii","iiii",opcode);
							break;
						case 0x3://0011
							check_str("C","3","iiii","iiii",opcode);
							break;
						case 0x4://0100
							check_str("C","4","iiii","iiii",opcode);
							break;
						case 0x5://0101
							check_str("C","5","iiii","iiii",opcode);
							break;
						case 0x6://0110
							check_str("C","6","iiii","iiii",opcode);
							break;
						case 0x7://0111
							check_str("C","7","iiii","iiii",opcode);
							break;
						case 0x8://1000
							check_str("C","8","iiii","iiii",opcode);
							break;
						case 0x9://1001
							check_str("C","9","iiii","iiii",opcode);
							break;
						case 0xA://1010
							check_str("C","A","iiii","iiii",opcode);
							break;
						case 0xB://1011
							check_str("C","B","iiii","iiii",opcode);
							break;
						case 0xC://1100
							check_str("C","C","iiii","iiii",opcode);
							break;
						case 0xD://1101
							check_str("C","D","iiii","iiii",opcode);
							break;
						case 0xE://1110
							check_str("C","E","iiii","iiii",opcode);
							break;
						case 0xF://1111
							check_str("C","F","iiii","iiii",opcode);
							break;
						default:
							iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0xD://finished
						check_str("D","nnnn","iiii","iiii",opcode);
						break;
					case 0xE://finished
						check_str("E","nnnn","iiii","iiii",opcode);
						break;
					case 0xF://finished - fix for fsca
						#region case 0xf
					switch (opcode&0xf)
					{
						case 0x0://0000
							check_str("F","nnnn","mmmm","0",opcode);
							break;
						case 0x1://0001
							check_str("F","nnnn","mmmm","1",opcode);
							break;
						case 0x2://0010
							check_str("F","nnnn","mmmm","2",opcode);
							break;
						case 0x3://0011
							check_str("F","nnnn","mmmm","3",opcode);
							break;
						case 0x4://0100
							check_str("F","nnnn","mmmm","4",opcode);
							break;
						case 0x5://0101
							check_str("F","nnnn","mmmm","5",opcode);
							break;
						case 0x6://0110
							check_str("F","nnnn","mmmm","6",opcode);
							break;
						case 0x7://0111
							check_str("F","nnnn","mmmm","7",opcode);
							break;
						case 0x8://1000
							check_str("F","nnnn","mmmm","1000",opcode);
							break;
						case 0x9://1001
							check_str("F","nnnn","mmmm","1001",opcode);
							break;
						case 0xA://1010
							check_str("F","nnnn","mmmm","1010",opcode);
                            break;
						case 0xB://1011
							check_str("F","nnnn","mmmm","1011",opcode);
							break;
						case 0xC://1100
							check_str("F","nnnn","mmmm","1100",opcode);
							break;
						case 0xD://1101
							#region 0xD multi
						switch ((opcode >>4)&0xf)
						{
							case 0x0://0000
								check_str("F","nnnn","0","1101",opcode); 
								break;
							case 0x1://0001
								check_str("F","nnnn","1","1101",opcode); 
								break;
							case 0x2://0010
								check_str("F","nnnn","2","1101",opcode);
								break;
							case 0x3://0011
								check_str("F","nnnn","3","1101",opcode);
								break;
							case 0x4://0100
								check_str("F","nnnn","4","1101",opcode); 
								break;
							case 0x5://0101
								check_str("F","nnnn","5","1101",opcode); 
								break;
							case 0x6://0110
								check_str("F","nnnn","6","1101",opcode); 
								break;
							case 0x8://1000
								check_str("F","nnnn","8","1101",opcode); 
								break;
							case 0x9://1001
								check_str("F","nnnn","9","1101",opcode); 
								break;
							case 0xA://1010
								check_str("F","nnnn","A","1101",opcode); 
								break;
							case 0xB://1011
								check_str("F","nnnn","B","1101",opcode); 
								break;
							case 0xF://F_xxxx_F_1101
								#region 0xf multi
								//we have :
								//F_nnn0_F_1101
								//F_nn01_F_1101
								//F_B_F_1101
								//F_3_F_1101
							switch ((opcode>>8)&0x1)
							{
								case 0x0://F_nnn0_F_1101 - fsca DC special
									check_str("F","nnn0","F","1101",opcode);
									break;
								case 0x1://F_xxy1_F_1101
									//if (opcode==0xfffd) {iInvalidOpcode();break;}//F_x111_F_1101- invalid
									//F_nn01_F_1101
									//F_B_F_1101
									//F_3_F_1101
									if (((opcode>>9)&0x1)==0)//F_xxy1_F_1101
									{
										check_str("F","nn01","F","1101",opcode);
										break;
									}
									else//F_yy11_F_1101
									{
										if (((opcode>>10)&0x3)==0)//F_yy11_F_1101
										{
											check_str("F","3","F","1101",opcode);
											break;
										}
										else if(((opcode>>10)&0x3)==2)//F_yy11_F_1101
										{
											check_str("F","B","F","1101",opcode);
											break;
										}
									}
									//F_x111_F_1101- invalid
									iInvalidOpcode();
									break;
								default:
									iInvalidOpcode();
									break;
							}
								#endregion
								break;
							default:
								iInvalidOpcode();
								break;
						}
							#endregion
							break;
						case 0xE://1110
							check_str("F","nnnn","mmmm","1110",opcode);
							break;
                        default:
                            iInvalidOpcode();
							break;
					}
						#endregion
						break;
					case 0x10://Custom emulation opcodes ;) "just for the fun of it (tm)"
						
						break;
					default:
                        iInvalidOpcode();
                        break;
				}
                #endregion
		}
		static void iInvalidOpcode()
		{
			//check_(0, 1000);
		}

		static void check_str(string p1,string p2,string p3,string p4,uint opcode)
		{
			uint mask=0xFFFF;
			uint desired=0;

			if (p1.Length==4)
				desired|=Convert.ToUInt32(p1,2)<<12;
			else
				desired|=Convert.ToUInt32(p1,16)<<12;

			if (p2[0]=='n')
				mask&=0xF0FF;
			else if (p2[0]=='i')
				mask&=0xF0FF;
			else
			{
			if (p2.Length==4)
				desired|=Convert.ToUInt32(p2,2)<<8;
			else
				desired|=Convert.ToUInt32(p2,16)<<8;
			}

			if (p3[0]=='m')
				mask&=0xFF0F;
			else if (p3[0]=='i')
				mask&=0xFF0F;
			else
			{
			if (p3.Length==4)
				desired|=Convert.ToUInt32(p3,2)<<4;
			else
				desired|=Convert.ToUInt32(p3,16)<<4;
			}

			if (p4[0]=='i')
				mask&=0xFFF0;
			else
			{
			if (p4.Length==4)
				desired|=Convert.ToUInt32(p4,2);
			else
				desired|=Convert.ToUInt32(p4,16);
			}
			
			check_mask(opcode,mask,desired);
		}
		static void check_mask(uint opcode ,uint mask, uint desired)
		{
			if ((opcode & mask)!=desired)
			{
				dc.dcon.WriteLine("OPCODE ERROR : OPCODE MISSMATCH;" + hex(opcode) + " & " + hex(mask) + " != " + hex(desired));
			}
		}
		static void check_(uint opcode,uint desired)
		{
			check_mask(opcode,0xffff,desired);
		}
		static void check_m(uint opcode,uint desired)
		{
			check_mask(opcode,0xff0f,desired);
		}
		static void check_n(uint opcode,uint desired)
		{
			check_mask(opcode,0xf0ff,desired);
		}
		static void check_nm(uint opcode,uint desired)
		{
			check_mask(opcode,0xf00f,desired);
		}

		static void check_imm8(uint opcode,uint desired)
		{
			check_mask(opcode,0xff00,desired);
		}
		static void check_imm4(uint opcode,uint desired)
		{
			check_mask(opcode,0xfff0,desired);
		}
		static void check_imm12(uint opcode,uint desired)
		{
			check_mask(opcode,0xf000,desired);
		}
		static void check_nmimm4(uint opcode,uint desired)
		{
			check_mask(opcode,0xf000,desired);
		}
		static void check_nimm4(uint opcode,uint desired)
		{
			check_mask(opcode,0xf0f0,desired);
		}
		static void check_nimm8(uint opcode,uint desired)
		{
			check_mask(opcode,0xf000,desired);
		}
		static string hex(uint val)
		{
			return "0x" + Convert.ToString(val, 16).ToLower();
		}
	}
}
