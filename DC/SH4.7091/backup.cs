#region oldcode
#if ba
		public static void runcpu()
		{
			
			runsh=true;
			do 
			{
				opcode=read(pc,2);
#region proc opcode
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
								i0000_nnnn_0000_0010();
								break;
							case 0x1://0001
								i0000_nnnn_0001_0010();
								break;
							case 0x2://0010
								i0000_nnnn_0010_0010();
								break;
							case 0x3://0011
								i0000_nnnn_0011_0010();
								break;
							case 0x4://0100
								i0000_nnnn_0100_0010();
								break;
							case 0x8://1000
								i0000_nnnn_1000_0010();
								break;
							case 0x9://1001
								i0000_nnnn_1001_0010();
								break;
							case 0xA://1010
								i0000_nnnn_1010_0010();
								break;
							case 0xB://1011
								i0000_nnnn_1011_0010();
								break;
							case 0xC://1100
								i0000_nnnn_1100_0010();
								break;
							case 0xD://1101
								i0000_nnnn_1101_0010();
								break;
							case 0xE://1110
								i0000_nnnn_1110_0010();
								break;
							case 0xF://1111
								i0000_nnnn_1111_0010();
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
								i0000_nnnn_0000_0011();
								break;
							case 0x2://0010
								i0000_nnnn_0010_0011();
								break;
							case 0x8://1000
								i0000_nnnn_1000_0011();
								break;
							case 0x9://1001
								i0000_nnnn_1001_0011();
								break;
							case 0xA://1010
								i0000_nnnn_1010_0011();
								break;
							case 0xB://1011
								i0000_nnnn_1011_0011();
								break;
							case 0xC://1100
								i0000_nnnn_1100_0011();
								break;
							default:
								iInvalidOpcode();
								break;
						}
#endregion
							break;
						case 0x4://0100
							i0000_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0000_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0000_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0000_nnnn_mmmm_0111();
							break;
						case 0x8://1000
#region case 0x8 multi opcodes
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0000
								i0000_0000_0000_1000();
								break;
							case 0x1://0001
								i0000_0000_0001_1000();
								break;
							case 0x2://0010
								i0000_0000_0010_1000();
								break;
							case 0x3://0011
								i0000_0000_0011_1000();
								break;
							case 0x4://0100
								i0000_0000_0100_1000();
								break;
							case 0x5://0101
								i0000_0000_0101_1000();
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
								i0000_0000_0000_1001();
								break;
							case 0x1://0001
								i0000_0000_0001_1001();
								break;
							case 0x2://0010
								i0000_nnnn_0010_1001();
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
								i0000_nnnn_0000_1010();
								break;
							case 0x1://0001
								i0000_nnnn_0001_1010();
								break;
							case 0x2://0010
								i0000_nnnn_0010_1010();
								break;
							case 0x5://0101
								i0000_nnnn_0101_1010();
								break;
							case 0x6://0110
								i0000_nnnn_0110_1010();
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
								i0000_0000_0000_1011();
								break;
							case 0x1://0001
								i0000_0000_0001_1011();
								break;
							case 0x2://0010
								i0000_0000_0010_1011();
								break;
							default:
								iInvalidOpcode();
								break;
						}
#endregion
							break;
						case 0xC://1100
							i0000_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0000_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0000_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0000_nnnn_mmmm_1111();
							break;
					}
#endregion
						break;
					case 0x1://finished
						i0001_nnnn_mmmm_iiii();
						break;
					case 0x2://finished
#region case 0x2
					switch (opcode&0xf)
					{
						case 0x0://0000
							i0010_nnnn_mmmm_0000();
							break;
						case 0x1://0001
							i0010_nnnn_mmmm_0001();
							break;
						case 0x2://0010
							i0010_nnnn_mmmm_0010();
							break;
						case 0x4://0100
							i0010_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0010_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0010_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0010_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i0010_nnnn_mmmm_1000();
							break;
						case 0x9://1001
							i0010_nnnn_mmmm_1001();
							break;
						case 0xA://1010
							i0010_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i0010_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i0010_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0010_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0010_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0010_nnnn_mmmm_1111();
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
							i0011_nnnn_mmmm_0000();
							break;
						case 0x2://0010
							i0011_nnnn_mmmm_0010();
							break;
						case 0x3://0011
							i0011_nnnn_mmmm_0011();
							break;
						case 0x4://0100
							i0011_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0011_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0011_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0011_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i0011_nnnn_mmmm_1000();
							break;
						case 0xA://1010
							i0011_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i0011_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i0011_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0011_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0011_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0011_nnnn_mmmm_1111();
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
							case 0x0://0100_xxxx_0000_0000
								i0100_nnnn_0000_0000();
								break;
							case 0x1://0100_xxxx_0001_0000
								i0100_nnnn_0001_0000();
								break;
							case 0x2://0100_xxxx_0010_0000
								i0100_nnnn_0010_0000();
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
							case 0x0://0100_xxxx_0000_0001
								i0100_nnnn_0000_0001();
								break;
							case 0x1://0100_xxxx_0001_0001
								i0100_nnnn_0001_0001();
								break;
							case 0x2://0100_xxxx_0010_0001
								i0100_nnnn_0010_0001();
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
							case 0x0://0100_xxxx_0000_0010
								i0100_nnnn_0000_0010();
								break;
							case 0x1://0100_xxxx_0001_0010
								i0100_nnnn_0001_0010();
								break;
							case 0x2://0100_xxxx_0010_0010
								i0100_nnnn_0010_0010();
								break;
							case 0x5://0100_xxxx_0101_0010
								i0100_nnnn_0101_0010();
								break;
							case 0x6://0100_xxxx_0110_0010
								i0100_nnnn_0110_0010();
								break;
							case 0x8://0100_xxxx_1000_0010
								i0100_nnnn_1000_0010();
								break;
							case 0x9://0100_xxxx_1001_0010
								i0100_nnnn_1001_0010();
								break;
							case 0xA://0100_xxxx_1010_0010
								i0100_nnnn_1010_0010();
								break;
							case 0xB://0100_xxxx_1011_0010
								i0100_nnnn_1011_0010();
								break;
							case 0xC://0100_xxxx_1100_0010
								i0100_nnnn_1100_0010();
								break;
							case 0xD://0100_xxxx_1101_0010
								i0100_nnnn_1101_0010();
								break;
							case 0xE://0100_xxxx_1110_0010
								i0100_nnnn_1110_0010();
								break;
							case 0xF://0100_xxxx_1111_0010
								i0100_nnnn_1111_0010();
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
							case 0x0://0100_xxxx_0000_0011
								i0100_nnnn_0000_0011();
								break;
							case 0x1://0100_xxxx_0001_0011
								i0100_nnnn_0001_0011();
								break;
							case 0x2://0100_xxxx_0010_0011
								i0100_nnnn_0010_0011();
								break;
							case 0x3://0100_xxxx_0011_0011
								i0100_nnnn_0011_0011();
								break;
							case 0x4://0100_xxxx_0100_0011
								i0100_nnnn_0100_0011();
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
							case 0x0://0100_xxxx_0000_0100
								i0100_nnnn_0000_0100();
								break;
							case 0x2://0100_xxxx_0010_0100
								i0100_nnnn_0010_0100();
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
							case 0x0://0100_xxxx_0000_0101
								i0100_nnnn_0000_0101();
								break;
							case 0x1://0100_xxxx_0001_0101
								i0100_nnnn_0001_0101();
								break;
							case 0x2://0100_xxxx_0010_0101
								i0100_nnnn_0010_0101();
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
							case 0x0://0100_xxxx_0000_0110
								i0100_nnnn_0000_0110();
								break;
							case 0x1://0100_xxxx_0001_0110
								i0100_nnnn_0001_0110();
								break;
							case 0x2://0100_xxxx_0010_0110
								i0100_nnnn_0010_0110();
								break;
							case 0x5://0100_xxxx_0101_0110
								i0100_nnnn_0101_0110();
								break;
							case 0x6://0100_xxxx_0110_0110
								i0100_nnnn_0110_0110();
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
							case 0x0://0100_xxxx_0000_0111
								i0100_nnnn_0000_0111();
								break;
							case 0x1://0100_xxxx_0001_0111
								i0100_nnnn_0001_0111();
								break;
							case 0x2://0100_xxxx_0010_0111
								i0100_nnnn_0010_0111();
								break;
							case 0x3://0100_xxxx_0011_0111
								i0100_nnnn_0011_0111();
								break;
							case 0x4://0100_xxxx_0100_0111
								i0100_nnnn_0100_0111();
								break;
							case 0x8://0100_xxxx_1000_0111
								i0100_nnnn_1000_0111();
								break;
							case 0x9://0100_xxxx_1001_0111
								i0100_nnnn_1001_0111();
								break;
							case 0xA://0100_xxxx_1010_0111
								i0100_nnnn_1010_0111();
								break;
							case 0xB://0100_xxxx_1011_0111
								i0100_nnnn_1011_0111();
								break;
							case 0xC://0100_xxxx_1100_0111
								i0100_nnnn_1100_0111();
								break;
							case 0xD://0100_xxxx_1101_0111
								i0100_nnnn_1101_0111();
								break;
							case 0xE://0100_xxxx_1110_0111
								i0100_nnnn_1110_0111();
								break;
							case 0xF://0100_xxxx_1111_0111
								i0100_nnnn_1111_0111();
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
							case 0x0://0100_xxxx_0000_1000
								i0100_nnnn_0000_1000();
								break;
							case 0x1://0100_xxxx_0001_1000
								i0100_nnnn_0001_1000();
								break;
							case 0x2://0100_xxxx_0010_1000
								i0100_nnnn_0010_1000();
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
							case 0x0://0100_xxxx_0000_1001
								i0100_nnnn_0000_1001();
								break;
							case 0x1://0100_xxxx_0001_1001
								i0100_nnnn_0001_1001();
								break;
							case 0x2://0100_xxxx_0010_1001
								i0100_nnnn_0010_1001();
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
							case 0x0://0100_xxxx_0000_1010
								i0100_nnnn_0000_1010();
								break;
							case 0x1://0100_xxxx_0001_1010
								i0100_nnnn_0001_1010();
								break;
							case 0x2://0100_xxxx_0010_1010
								i0100_nnnn_0010_1010();
								break;
							case 0x5://0100_xxxx_0101_1010
								i0100_nnnn_0101_1010();
								break;
							case 0x6://0100_xxxx_0110_1010
								i0100_nnnn_0110_1010();
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
							case 0x0://0100_xxxx_0000_1011
								i0100_nnnn_0000_1011();
								break;
							case 0x1://0100_xxxx_0001_1011
								i0100_nnnn_0001_1011();
								break;
							case 0x2://0100_xxxx_0010_1011
								i0100_nnnn_0010_1011();
								break;
							default:
								iInvalidOpcode();
								break;
						}
#endregion 
							break;
						case 0xC://1100
							i0100_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0100_nnnn_mmmm_1101();
							break;
						case 0xE://1110
#region 0xE multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1110
								i0100_nnnn_0000_1110();
								break;
							case 0x1://0100_xxxx_0001_1110
								i0100_nnnn_0001_1110();
								break;
							case 0x2://0100_xxxx_0010_1110
								i0100_nnnn_0010_1110();
								break;
							case 0x3://0100_xxxx_0011_1110
								i0100_nnnn_0011_1110();
								break;
							case 0x4://0100_xxxx_0100_1110
								i0100_nnnn_0100_1110();
								break;
							case 0x8://0100_xxxx_1000_1110
								i0100_nnnn_1000_1110();
								break;
							case 0x9://0100_xxxx_1001_1110
								i0100_nnnn_1001_1110();
								break;
							case 0xA://0100_xxxx_1010_1110
								i0100_nnnn_1010_1110();
								break;
							case 0xB://0100_xxxx_1011_1110
								i0100_nnnn_1011_1110();
								break;
							case 0xC://0100_xxxx_1100_1110
								i0100_nnnn_1100_1110();
								break;
							case 0xD://0100_xxxx_1101_1110
								i0100_nnnn_1101_1110();
								break;
							case 0xE://0100_xxxx_1110_1110
								i0100_nnnn_1110_1110();
								break;
							case 0xF://0100_xxxx_1111_1110
								i0100_nnnn_1111_1110();
								break;
							default:
								iInvalidOpcode();
								break;
						}
#endregion 
							break;
						case 0xF://1111
							i0100_nnnn_mmmm_1111();
							break;
						default:
							iInvalidOpcode();
							break;
					}
#endregion
						break;
					case 0x5://finished
						i0101_nnnn_mmmm_iiii();
						break;
					case 0x6://finished
#region case 0x6
					switch (opcode&0xf)
					{
						case 0x0://0000
							i0110_nnnn_mmmm_0000();
							break;
						case 0x1://0001
							i0110_nnnn_mmmm_0001();
							break;
						case 0x2://0010
							i0110_nnnn_mmmm_0010();
							break;
						case 0x3://0011
							i0110_nnnn_mmmm_0011();
							break;
						case 0x4://0100
							i0110_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0110_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0110_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0110_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i0110_nnnn_mmmm_1000();
							break;
						case 0x9://1001
							i0110_nnnn_mmmm_1001();
							break;
						case 0xA://1010
							i0110_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i0110_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i0110_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0110_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0110_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0110_nnnn_mmmm_1111();
							break;
						default:
							iInvalidOpcode();
							break;
					}
#endregion
						break;
					case 0x7://finished
						i0111_nnnn_iiii_iiii();
						break;
					case 0x8://finished
#region case 0x8
					switch ((opcode>>8)&0xf)
					{
						case 0x0://0000
							i1000_0000_mmmm_iiii();
							break;
						case 0x1://0001
							i1000_0001_mmmm_iiii();
							break;
						case 0x4://0100
							i1000_0100_mmmm_iiii();
							break;
						case 0x5://0101
							i1000_0101_mmmm_iiii();
							break;
						case 0x8://1000
							i1000_1000_iiii_iiii();
							break;
						case 0x9://1001
							i1000_1001_iiii_iiii();
							break;
						case 0xB://1011
							i1000_1011_iiii_iiii();
							break;
						case 0xD://1101
							i1000_1101_iiii_iiii();
							break;
						case 0xF://1111
							i1000_1111_iiii_iiii();
							break;
						default:
							iInvalidOpcode();
							break;
					}
#endregion
						break;
					case 0x9://finished
						i1001_nnnn_iiii_iiii();
						break;
					case 0xA://finished
						i1010_iiii_iiii_iiii();
						break;
					case 0xB://finished
						i1011_iiii_iiii_iiii();
						break;
					case 0xC://finished
#region case 0xC
					switch ((opcode>>8)&0xf)
					{
						case 0x0://0000
							iCxxx.i1100_0000_iiii_iiii();
							break;
						case 0x1://0001
							iCxxx.i1100_0001_iiii_iiii();
							break;
						case 0x2://0010
							iCxxx.i1100_0010_iiii_iiii();
							break;
						case 0x3://0011
							iCxxx.i1100_0011_iiii_iiii();
							break;
						case 0x4://0100
							iCxxx.i1100_0100_iiii_iiii();
							break;
						case 0x5://0101
							iCxxx.i1100_0101_iiii_iiii();
							break;
						case 0x6://0110
							iCxxx.i1100_0110_iiii_iiii();
							break;
						case 0x7://0111
							iCxxx.i1100_0111_iiii_iiii();
							break;
						case 0x8://1000
							iCxxx.i1100_1000_iiii_iiii();
							break;
						case 0x9://1001
							iCxxx.i1100_1001_iiii_iiii();
							break;
						case 0xA://1010
							iCxxx.i1100_1010_iiii_iiii();
							break;
						case 0xB://1011
							iCxxx.i1100_1011_iiii_iiii();
							break;
						case 0xC://1100
							iCxxx.i1100_1100_iiii_iiii();
							break;
						case 0xD://1101
							iCxxx.i1100_1101_iiii_iiii();
							break;
						case 0xE://1110
							iCxxx.i1100_1110_iiii_iiii();
							break;
						case 0xF://1111
							iCxxx.i1100_1111_iiii_iiii();
							break;
						default:
							iCxxx.iInvalidOpcode();
							break;
					}
#endregion
						break;
					case 0xD://finished
						i1101_nnnn_iiii_iiii();
						break;
					case 0xE://finished
						i1110_nnnn_iiii_iiii();
						break;
					case 0xF://finished - fix for fsca
#region case 0xf
					switch (opcode&0xf)
					{
						case 0x0://0000
							i1111_nnnn_mmmm_0000();
							break;
						case 0x1://0001
							i1111_nnnn_mmmm_0001();
							break;
						case 0x2://0010
							i1111_nnnn_mmmm_0010();
							break;
						case 0x3://0011
							i1111_nnnn_mmmm_0011();
							break;
						case 0x4://0100
							i1111_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i1111_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i1111_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i1111_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i1111_nnnn_mmmm_1000();
							break;
						case 0x9://1001
							i1111_nnnn_mmmm_1001();
							break;
						case 0xA://1010
							i1111_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i1111_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i1111_nnnn_mmmm_1100();
							break;
						case 0xD://1101
#region 0xD multi
						switch ((opcode >>4)&0xf)
						{
							case 0x0://0000
								i1111_nnnn_0000_1101(); 
								break;
							case 0x1://0001
								i1111_nnnn_0001_1101(); 
								break;
							case 0x2://0010
								i1111_nnnn_0010_1101(); 
								break;
							case 0x3://0011
								i1111_nnnn_0011_1101(); 
								break;
							case 0x4://0100
								i1111_nnnn_0100_1101(); 
								break;
							case 0x5://0101
								i1111_nnnn_0101_1101(); 
								break;
							case 0x6://0110
								i1111_nnnn_0110_1101(); 
								break;
							/*case 0x7://0111//invalid opcode
								i1111_nnnn_0111_1101(); 
								break;*/
							case 0x8://1000
								i1111_nnnn_1000_1101(); 
								break;
							case 0x9://1001
								i1111_nnnn_1001_1101(); 
								break;
							case 0xA://1010
								i1111_nnnn_1010_1101(); 
								break;
							case 0xB://1011
								i1111_nnnn_1011_1101(); 
								break;
							/*case 0xC://1100// invalid opcodes
								i1111_nnnn_1100_1101(); 
								break;
							case 0xD://1101
								i1111_nnnn_1101_1101(); 
								break;
							case 0xE://1110
								i1111_nnnn_1110_1101(); 
								break;*/
							case 0xF://1111
#region 0xf multi
							switch ((opcode>>8)&0x3)
							{
								case 0x0://1111_xx00_1111_1101 - fsca DC special
									i1111_nnn0_1111_1101();
									break;
								case 0x1://1111_xx01_1111_1101
									i1111_nn01_1111_1101();
									break;
								case 0x2://1111_xx10_1111_1101 - fsca DC special
									i1111_nnn0_1111_1101();
									break;
								case 0x3://1111_xx11_1111_1101
									if (opcode==0xfffd) {iInvalidOpcode();break;}//1111_x111_1111_1101- invalid
									if (((opcode>>11)&0x1)==0)//1111_1011_1111_1101
										i1111_0011_1111_1101();
									else//1111_0011_1111_1101
										i1111_1011_1111_1101();
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
							i1111_nnnn_mmmm_1110();
							break;
						default:
							iInvalidOpcode();
							break;
					}
#endregion
						break;
					default:
						//handle any custom opcodes (>65535)
						//bios hle ect
						break;
				}
#endregion
#region Proc PC
				switch(pc_funct)	   
				{
					case 0://inc pc
						pc+=2;
						break;
					case 1://jump
						pc=delayslot;
						pc_funct=0;
						break;
					case 2://jump delay
						pc+=2;
						pc_funct=1;
						break;
				}
#endregion
				opcount+=1;
				opc+=1;
				if (opc>1461409)
				{present();System.Windows.Forms.Application.DoEvents();opc=0;}
			} while (runsh);

		}
		

		public static void execblock(fastint.codeblock block)
		{
			uint bp=0;
			for (uint i = block.pc;i<=block.pc+block.len;i+=2)
			{
				opcode=block.cache[bp+=2];
				#region proc opcode
				switch (opcode>>12)//proc opcode
				{
					case 0x0://finished
						#region case 0x0
					
					switch (opcode&0xf)
					{
						case 0x0://0000
							dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							break;
						case 0x1://0001
							dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							break;
						case 0x2://0010
							#region case 0x2 multi opcodes
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0000
								i0000_nnnn_0000_0010();
								break;
							case 0x1://0001
								i0000_nnnn_0001_0010();
								break;
							case 0x2://0010
								i0000_nnnn_0010_0010();
								break;
							case 0x3://0011
								i0000_nnnn_0011_0010();
								break;
							case 0x4://0100
								i0000_nnnn_0100_0010();
								break;
							case 0x8://1000
								i0000_nnnn_1000_0010();
								break;
							case 0x9://1001
								i0000_nnnn_1001_0010();
								break;
							case 0xA://1010
								i0000_nnnn_1010_0010();
								break;
							case 0xB://1011
								i0000_nnnn_1011_0010();
								break;
							case 0xC://1100
								i0000_nnnn_1100_0010();
								break;
							case 0xD://1101
								i0000_nnnn_1101_0010();
								break;
							case 0xE://1110
								i0000_nnnn_1110_0010();
								break;
							case 0xF://1111
								i0000_nnnn_1111_0010();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;	
						}
							#endregion
							break;
						case 0x3://0011
							#region case 0x3 multi opcodes
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0000
								i0000_nnnn_0000_0011();
								break;
							case 0x2://0010
								i0000_nnnn_0010_0011();
								break;
							case 0x8://1000
								i0000_nnnn_1000_0011();
								break;
							case 0x9://1001
								i0000_nnnn_1001_0011();
								break;
							case 0xA://1010
								i0000_nnnn_1010_0011();
								break;
							case 0xB://1011
								i0000_nnnn_1011_0011();
								break;
							case 0xC://1100
								i0000_nnnn_1100_0011();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion
							break;
						case 0x4://0100
							i0000_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0000_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0000_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0000_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							#region case 0x8 multi opcodes
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0000
								i0000_0000_0000_1000();
								break;
							case 0x1://0001
								i0000_0000_0001_1000();
								break;
							case 0x2://0010
								i0000_0000_0010_1000();
								break;
							case 0x3://0011
								i0000_0000_0011_1000();
								break;
							case 0x4://0100
								i0000_0000_0100_1000();
								break;
							case 0x5://0101
								i0000_0000_0101_1000();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion
							break;
						case 0x9://1001
							#region case 0x9 multi opcodes
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0000
								i0000_0000_0000_1001();
								break;
							case 0x1://0001
								i0000_0000_0001_1001();
								break;
							case 0x2://0010
								i0000_nnnn_0010_1001();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion
							break;
						case 0xA://1010
							#region case 0xA multi opcodes
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0000
								i0000_nnnn_0000_1010();
								break;
							case 0x1://0001
								i0000_nnnn_0001_1010();
								break;
							case 0x2://0010
								i0000_nnnn_0010_1010();
								break;
							case 0x5://0101
								i0000_nnnn_0101_1010();
								break;
							case 0x6://0110
								i0000_nnnn_0110_1010();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion
							break;
						case 0xB://1011
							#region case 0xB multi opcodes
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0000
								i0000_0000_0000_1011();
								break;
							case 0x1://0001
								i0000_0000_0001_1011();
								break;
							case 0x2://0010
								i0000_0000_0010_1011();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion
							break;
						case 0xC://1100
							i0000_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0000_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0000_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0000_nnnn_mmmm_1111();
							break;
					}
						#endregion
						break;
					case 0x1://finished
						i0001_nnnn_mmmm_iiii();
						break;
					case 0x2://finished
						#region case 0x2
					switch (opcode&0xf)
					{
						case 0x0://0000
							i0010_nnnn_mmmm_0000();
							break;
						case 0x1://0001
							i0010_nnnn_mmmm_0001();
							break;
						case 0x2://0010
							i0010_nnnn_mmmm_0010();
							break;
						case 0x4://0100
							i0010_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0010_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0010_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0010_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i0010_nnnn_mmmm_1000();
							break;
						case 0x9://1001
							i0010_nnnn_mmmm_1001();
							break;
						case 0xA://1010
							i0010_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i0010_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i0010_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0010_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0010_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0010_nnnn_mmmm_1111();
							break;
						default:
							dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							break;
					}
						#endregion
						break;
					case 0x3://finished
						#region case 0x3
					switch (opcode&0xf)
					{
						case 0x0://0000
							i0011_nnnn_mmmm_0000();
							break;
						case 0x2://0010
							i0011_nnnn_mmmm_0010();
							break;
						case 0x3://0011
							i0011_nnnn_mmmm_0011();
							break;
						case 0x4://0100
							i0011_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0011_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0011_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0011_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i0011_nnnn_mmmm_1000();
							break;
						case 0xA://1010
							i0011_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i0011_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i0011_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0011_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0011_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0011_nnnn_mmmm_1111();
							break;
						default:
							dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
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
							case 0x0://0100_xxxx_0000_0000
								i0100_nnnn_0000_0000();
								break;
							case 0x1://0100_xxxx_0001_0000
								i0100_nnnn_0001_0000();
								break;
							case 0x2://0100_xxxx_0010_0000
								i0100_nnnn_0010_0000();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0x1://0001
							#region 0x1 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0001
								i0100_nnnn_0000_0001();
								break;
							case 0x1://0100_xxxx_0001_0001
								i0100_nnnn_0001_0001();
								break;
							case 0x2://0100_xxxx_0010_0001
								i0100_nnnn_0010_0001();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0x2://0010
							#region 0x2 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0010
								i0100_nnnn_0000_0010();
								break;
							case 0x1://0100_xxxx_0001_0010
								i0100_nnnn_0001_0010();
								break;
							case 0x2://0100_xxxx_0010_0010
								i0100_nnnn_0010_0010();
								break;
							case 0x5://0100_xxxx_0101_0010
								i0100_nnnn_0101_0010();
								break;
							case 0x6://0100_xxxx_0110_0010
								i0100_nnnn_0110_0010();
								break;
							case 0x8://0100_xxxx_1000_0010
								i0100_nnnn_1000_0010();
								break;
							case 0x9://0100_xxxx_1001_0010
								i0100_nnnn_1001_0010();
								break;
							case 0xA://0100_xxxx_1010_0010
								i0100_nnnn_1010_0010();
								break;
							case 0xB://0100_xxxx_1011_0010
								i0100_nnnn_1011_0010();
								break;
							case 0xC://0100_xxxx_1100_0010
								i0100_nnnn_1100_0010();
								break;
							case 0xD://0100_xxxx_1101_0010
								i0100_nnnn_1101_0010();
								break;
							case 0xE://0100_xxxx_1110_0010
								i0100_nnnn_1110_0010();
								break;
							case 0xF://0100_xxxx_1111_0010
								i0100_nnnn_1111_0010();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0x3://0011
							#region 0x3 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0011
								i0100_nnnn_0000_0011();
								break;
							case 0x1://0100_xxxx_0001_0011
								i0100_nnnn_0001_0011();
								break;
							case 0x2://0100_xxxx_0010_0011
								i0100_nnnn_0010_0011();
								break;
							case 0x3://0100_xxxx_0011_0011
								i0100_nnnn_0011_0011();
								break;
							case 0x4://0100_xxxx_0100_0011
								i0100_nnnn_0100_0011();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0x4://0100
							#region 0x4 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0100
								i0100_nnnn_0000_0100();
								break;
							case 0x2://0100_xxxx_0010_0100
								i0100_nnnn_0010_0100();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0x5://0101
							#region 0x5 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0101
								i0100_nnnn_0000_0101();
								break;
							case 0x1://0100_xxxx_0001_0101
								i0100_nnnn_0001_0101();
								break;
							case 0x2://0100_xxxx_0010_0101
								i0100_nnnn_0010_0101();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0x6://0110
							#region 0x6 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0110
								i0100_nnnn_0000_0110();
								break;
							case 0x1://0100_xxxx_0001_0110
								i0100_nnnn_0001_0110();
								break;
							case 0x2://0100_xxxx_0010_0110
								i0100_nnnn_0010_0110();
								break;
							case 0x5://0100_xxxx_0101_0110
								i0100_nnnn_0101_0110();
								break;
							case 0x6://0100_xxxx_0110_0110
								i0100_nnnn_0110_0110();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0x7://0111
							#region 0x7 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_0111
								i0100_nnnn_0000_0111();
								break;
							case 0x1://0100_xxxx_0001_0111
								i0100_nnnn_0001_0111();
								break;
							case 0x2://0100_xxxx_0010_0111
								i0100_nnnn_0010_0111();
								break;
							case 0x3://0100_xxxx_0011_0111
								i0100_nnnn_0011_0111();
								break;
							case 0x4://0100_xxxx_0100_0111
								i0100_nnnn_0100_0111();
								break;
							case 0x8://0100_xxxx_1000_0111
								i0100_nnnn_1000_0111();
								break;
							case 0x9://0100_xxxx_1001_0111
								i0100_nnnn_1001_0111();
								break;
							case 0xA://0100_xxxx_1010_0111
								i0100_nnnn_1010_0111();
								break;
							case 0xB://0100_xxxx_1011_0111
								i0100_nnnn_1011_0111();
								break;
							case 0xC://0100_xxxx_1100_0111
								i0100_nnnn_1100_0111();
								break;
							case 0xD://0100_xxxx_1101_0111
								i0100_nnnn_1101_0111();
								break;
							case 0xE://0100_xxxx_1110_0111
								i0100_nnnn_1110_0111();
								break;
							case 0xF://0100_xxxx_1111_0111
								i0100_nnnn_1111_0111();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0x8://1000
							#region 0x8 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1000
								i0100_nnnn_0000_1000();
								break;
							case 0x1://0100_xxxx_0001_1000
								i0100_nnnn_0001_1000();
								break;
							case 0x2://0100_xxxx_0010_1000
								i0100_nnnn_0010_1000();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0x9://1001
							#region 0x9 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1001
								i0100_nnnn_0000_1001();
								break;
							case 0x1://0100_xxxx_0001_1001
								i0100_nnnn_0001_1001();
								break;
							case 0x2://0100_xxxx_0010_1001
								i0100_nnnn_0010_1001();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;

						case 0xA://1010
							#region 0x9 multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1010
								i0100_nnnn_0000_1010();
								break;
							case 0x1://0100_xxxx_0001_1010
								i0100_nnnn_0001_1010();
								break;
							case 0x2://0100_xxxx_0010_1010
								i0100_nnnn_0010_1010();
								break;
							case 0x5://0100_xxxx_0101_1010
								i0100_nnnn_0101_1010();
								break;
							case 0x6://0100_xxxx_0110_1010
								i0100_nnnn_0110_1010();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;

						case 0xB://1011
							#region 0xB multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1011
								i0100_nnnn_0000_1011();
								break;
							case 0x1://0100_xxxx_0001_1011
								i0100_nnnn_0001_1011();
								break;
							case 0x2://0100_xxxx_0010_1011
								i0100_nnnn_0010_1011();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0xC://1100
							i0100_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0100_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							#region 0xE multi
						switch ((opcode>>4)&0xf)
						{
							case 0x0://0100_xxxx_0000_1110
								i0100_nnnn_0000_1110();
								break;
							case 0x1://0100_xxxx_0001_1110
								i0100_nnnn_0001_1110();
								break;
							case 0x2://0100_xxxx_0010_1110
								i0100_nnnn_0010_1110();
								break;
							case 0x3://0100_xxxx_0011_1110
								i0100_nnnn_0011_1110();
								break;
							case 0x4://0100_xxxx_0100_1110
								i0100_nnnn_0100_1110();
								break;
							case 0x8://0100_xxxx_1000_1110
								i0100_nnnn_1000_1110();
								break;
							case 0x9://0100_xxxx_1001_1110
								i0100_nnnn_1001_1110();
								break;
							case 0xA://0100_xxxx_1010_1110
								i0100_nnnn_1010_1110();
								break;
							case 0xB://0100_xxxx_1011_1110
								i0100_nnnn_1011_1110();
								break;
							case 0xC://0100_xxxx_1100_1110
								i0100_nnnn_1100_1110();
								break;
							case 0xD://0100_xxxx_1101_1110
								i0100_nnnn_1101_1110();
								break;
							case 0xE://0100_xxxx_1110_1110
								i0100_nnnn_1110_1110();
								break;
							case 0xF://0100_xxxx_1111_1110
								i0100_nnnn_1111_1110();
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion 
							break;
						case 0xF://1111
							i0100_nnnn_mmmm_1111();
							break;
						default:
							dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							break;
					}
						#endregion
						break;
					case 0x5://finished
						i0101_nnnn_mmmm_iiii();
						break;
					case 0x6://finished
						#region case 0x6
					switch (opcode&0xf)
					{
						case 0x0://0000
							i0110_nnnn_mmmm_0000();
							break;
						case 0x1://0001
							i0110_nnnn_mmmm_0001();
							break;
						case 0x2://0010
							i0110_nnnn_mmmm_0010();
							break;
						case 0x3://0011
							i0110_nnnn_mmmm_0011();
							break;
						case 0x4://0100
							i0110_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i0110_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i0110_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i0110_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i0110_nnnn_mmmm_1000();
							break;
						case 0x9://1001
							i0110_nnnn_mmmm_1001();
							break;
						case 0xA://1010
							i0110_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i0110_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i0110_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							i0110_nnnn_mmmm_1101();
							break;
						case 0xE://1110
							i0110_nnnn_mmmm_1110();
							break;
						case 0xF://1111
							i0110_nnnn_mmmm_1111();
							break;
						default:
							dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							break;
					}
						#endregion
						break;
					case 0x7://finished
						i0111_nnnn_iiii_iiii();
						break;
					case 0x8://finished
						#region case 0x8
					switch ((opcode>>8)&0xf)
					{
						case 0x0://0000
							i1000_0000_mmmm_iiii();
							break;
						case 0x1://0001
							i1000_0001_mmmm_iiii();
							break;
						case 0x4://0100
							i1000_0100_mmmm_iiii();
							break;
						case 0x5://0101
							i1000_0101_mmmm_iiii();
							break;
						case 0x8://1000
							i1000_1000_iiii_iiii();
							break;
						case 0x9://1001
							i1000_1001_iiii_iiii();
							break;
						case 0xB://1011
							i1000_1011_iiii_iiii();
							break;
						case 0xD://1101
							i1000_1101_iiii_iiii();
							break;
						case 0xF://1111
							i1000_1111_iiii_iiii();
							break;
						default:
							dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							break;
					}
						#endregion
						break;
					case 0x9://finished
						i1001_nnnn_iiii_iiii();
						break;
					case 0xA://finished
						i1010_iiii_iiii_iiii();
						break;
					case 0xB://finished
						i1011_iiii_iiii_iiii();
						break;
					case 0xC://finished
						#region case 0xC
					switch ((opcode>>8)&0xf)
					{
						case 0x0://0000
							i1100_0000_iiii_iiii();
							break;
						case 0x1://0001
							i1100_0001_iiii_iiii();
							break;
						case 0x2://0010
							i1100_0010_iiii_iiii();
							break;
						case 0x3://0011
							i1100_0011_iiii_iiii();
							break;
						case 0x4://0100
							i1100_0100_iiii_iiii();
							break;
						case 0x5://0101
							i1100_0101_iiii_iiii();
							break;
						case 0x6://0110
							i1100_0110_iiii_iiii();
							break;
						case 0x7://0111
							i1100_0111_iiii_iiii();
							break;
						case 0x8://1000
							i1100_1000_iiii_iiii();
							break;
						case 0x9://1001
							i1100_1001_iiii_iiii();
							break;
						case 0xA://1010
							i1100_1010_iiii_iiii();
							break;
						case 0xB://1011
							i1100_1011_iiii_iiii();
							break;
						case 0xC://1100
							i1100_1100_iiii_iiii();
							break;
						case 0xD://1101
							i1100_1101_iiii_iiii();
							break;
						case 0xE://1110
							i1100_1110_iiii_iiii();
							break;
						case 0xF://1111
							i1100_1111_iiii_iiii();
							break;
						default:
							dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							break;
					}
						#endregion
						break;
					case 0xD://finished
						i1101_nnnn_iiii_iiii();
						break;
					case 0xE://finished
						i1110_nnnn_iiii_iiii();
						break;
					case 0xF://finished - fix for fsca
						#region case 0xf
					switch (opcode&0xf)
					{
						case 0x0://0000
							i1111_nnnn_mmmm_0000();
							break;
						case 0x1://0001
							i1111_nnnn_mmmm_0001();
							break;
						case 0x2://0010
							i1111_nnnn_mmmm_0010();
							break;
						case 0x3://0011
							i1111_nnnn_mmmm_0011();
							break;
						case 0x4://0100
							i1111_nnnn_mmmm_0100();
							break;
						case 0x5://0101
							i1111_nnnn_mmmm_0101();
							break;
						case 0x6://0110
							i1111_nnnn_mmmm_0110();
							break;
						case 0x7://0111
							i1111_nnnn_mmmm_0111();
							break;
						case 0x8://1000
							i1111_nnnn_mmmm_1000();
							break;
						case 0x9://1001
							i1111_nnnn_mmmm_1001();
							break;
						case 0xA://1010
							i1111_nnnn_mmmm_1010();
							break;
						case 0xB://1011
							i1111_nnnn_mmmm_1011();
							break;
						case 0xC://1100
							i1111_nnnn_mmmm_1100();
							break;
						case 0xD://1101
							#region 0xD multi
						switch ((opcode >>4)&0xf)
						{
							case 0x0://0000
								i1111_nnnn_0000_1101(); 
								break;
							case 0x1://0001
								i1111_nnnn_0001_1101(); 
								break;
							case 0x2://0010
								i1111_nnnn_0010_1101(); 
								break;
							case 0x3://0011
								i1111_nnnn_0011_1101(); 
								break;
							case 0x4://0100
								i1111_nnnn_0100_1101(); 
								break;
							case 0x5://0101
								i1111_nnnn_0101_1101(); 
								break;
							case 0x6://0110
								i1111_nnnn_0110_1101(); 
								break;
								/*case 0x7://0111//invalid opcode
									i1111_nnnn_0111_1101(); 
									break;*/
							case 0x8://1000
								i1111_nnnn_1000_1101(); 
								break;
							case 0x9://1001
								i1111_nnnn_1001_1101(); 
								break;
							case 0xA://1010
								i1111_nnnn_1010_1101(); 
								break;
							case 0xB://1011
								i1111_nnnn_1011_1101(); 
								break;
								/*case 0xC://1100// invalid opcodes
									i1111_nnnn_1100_1101(); 
									break;
								case 0xD://1101
									i1111_nnnn_1101_1101(); 
									break;
								case 0xE://1110
									i1111_nnnn_1110_1101(); 
									break;*/
							case 0xF://1111
								#region 0xf multi
							switch ((opcode>>8)&0x3)
							{
								case 0x0://1111_xx00_1111_1101 - fsca DC special
									i1111_nnn0_1111_1101();
									break;
								case 0x1://1111_xx01_1111_1101
									i1111_nn01_1111_1101();
									break;
								case 0x2://1111_xx10_1111_1101 - fsca DC special
									i1111_nnn0_1111_1101();
									break;
								case 0x3://1111_xx11_1111_1101
									if (opcode==0xfffd) {dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();break;}//1111_x111_1111_1101- invalid
									if (((opcode>>11)&0x1)==0)//1111_1011_1111_1101
										i1111_0011_1111_1101();
									else//1111_0011_1111_1101
										i1111_1011_1111_1101();
									break;
								default:
									dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
									break;
							}
								#endregion
								break;
							default:
								dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
								break;
						}
							#endregion
							break;
						case 0xE://1110
							i1111_nnnn_mmmm_1110();
							break;
						default:
							dc.dcon.WriteLine("Warning Invalid opcode at pc "+System.Convert.ToString(pc,16).ToUpper()+ " with code " +System.Convert.ToString(opcode,16).ToUpper());System.Windows.Forms.Application.DoEvents();
							break;
					}
						#endregion
						break;
					default:
						//handle any custom opcodes (>65535)
						//bios hle ect
						break;
				}
				#endregion
				#region Proc PC
				pc+=2;
				/*switch(pc_funct)	   
				{
					case 0://inc pc
						pc+=2;
						break;
					case 1://jump
						pc=delayslot;
						pc_funct=0;
						break;
					case 2://jump delay
						pc+=2;
						pc_funct=1;
						break;
				}*/
				#endregion
				opcount+=1;
				opc+=1;
				if (opc>1461409)
				{present();System.Windows.Forms.Application.DoEvents();opc=0;}
			} 
			if (pc_funct>0)
			{
				pc=delayslot;
				pc_funct=0;
			}
		}
		public static void runcpuDyna()
		{//runcpu using chase
			runsh=true;
			do 
			{
				fastint.RunPC();
			} while (runsh);
		}
#endif
#endregion