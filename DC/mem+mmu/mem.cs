using System;

namespace DC4Ever
{
	/// <summary>
	/// Manages the mem Reads/Writes and the 
	/// </summary>
    public partial class emu
    {
		public static byte[] ram = new byte[16* dc.mb];//16 megs ram
		public static uint read(uint adr,int len)
		{
			//TODO : Emulate properly the P,ALT and NC bits- this will prob be done on
			//the MMUtranslate , also n*k
			//TODO : RE enalbe the mmu
			adr=mmutrans(adr);//translate using mmu
			uint padr= adr & 0x1FFFFFFF;//get the phisical adress(discard p,alt,nc)
			uint offset=adr&0x3FFFFFF;
			switch (padr>>26)//get the area (upper 3 bits)
			{
				case 0://bios/flashrom/hardaware regs
					#region Bios/Flash/HardWare Registers
					if (offset<0x200000)//bios read
					{
						return readBios(offset,len);
					}
					else if (offset < 0x240000)//flash ram read
					{
						return readBios_falsh(offset-0x200000,len);
					}
					else//register read
					{
						return readhwmmr(offset,len);
					}
					#endregion
				case 1://Video ram
					return readmem(offset,len);// redirect to  readmem
				case 2://???? nothing???? olny mmu???
					dc.dcon.WriteLine("Area2 read  adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16));
					return 0;
				case 3://System Ram
					#region System read(no more sysread sub)
				switch (len)
				{
					case 1:
						return ram[offset];
					case 2:
						//fixed(byte *p=&ram[offset])
						//	return *(ushort*)p;
                        return (uint)(ram[offset] | (ram[offset+1] << 8));
                    case 4:
						//fixed(byte *p=&ram[offset])
						//	return *(uint*)p;
                        return (uint)(ram[offset] | (ram[offset + 1] << 8) | (ram[offset +2] << 16)
                                           | (ram[offset + 3] << 24));
                }
					#endregion
					return 0;
				case 4://Tile acceletator coomand input
					dc.dcon.WriteLine("TA Area4 read adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16));
					return 0;//nothing yet
				case 5://Expansion (modem) port
					dc.dcon.WriteLine("Area5 read adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16));
					return 0;//nothing yet
				case 6://???? nothing???? olny mmu???
					dc.dcon.WriteLine("Area6 read adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16));
					return 0;
				case 7://Internal I.O. regs (same as p4) priv. mode only
					dc.dcon.WriteLine("Interlal I.O. registers read adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16));
					return readinmmr(offset,len);//nothing yet
			}
			return 0;
		}
		public static void write(uint adr,uint data,int len)
		{
			//TODO : Emulate properly the P,ALT and NC bits- this will prob be done on
			//the MMUtranslate ,also n*k access test
			adr=mmutrans(adr);//translate using mmu
			uint padr= adr & 0x1FFFFFFF;//get the phisical adress(discard p,alt,nc)
			uint offset=adr&0x3FFFFFF;//get the area offset
			switch (padr>>26)//get the area (upper 3 bits)
			{
				case 0://bios/flashrom/hardaware regs
					#region Bios/Flash/Hardware Registers
					if (offset<0x200000)//bios Write...heh good idea
					{
						dc.dcon.WriteLine("Bios Write ?!?! (pc="+pc+")");
						return;
					}					
					else if (offset < 0x240000)//flash ram write
					{
						writeBios_flash(offset-0x200000,data,len);
						return;
					}
					else//register write
					{
						writehwmmr(offset,data,len);
						return;
					}
					#endregion
				case 1://Video ram
					writemem(offset,data,len);
					return;// redirect to  writemem
				case 2://???? nothing???? olny mmu???
					dc.dcon.WriteLine("Area2 write adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16));
					return;
				case 3://System Ram
					#region System write(no more syswrite sub)
					//fastint.disableblock(adr);
				switch (len)
				{
					case 1:
						ram[offset]=(byte)data;
						return;
					case 2:
						//fixed(byte *p=&ram[offset])
						//	*(ushort*)p=(ushort)data;
                        ram[offset] = (byte)data;
                        ram[offset + 1] = (byte)(data >> 8);
                        return;
					case 4:
						//fixed(byte *p=&ram[offset])
						//	*(uint*)p=data;
                        ram[offset] = (byte)data;
                        ram[offset + 1] = (byte)(data >> 8);
                        ram[offset + 2] = (byte)(data >> 16);
                        ram[offset + 3] = (byte)(data >> 24);
                        return;
				}
					#endregion
					return;
				case 4://Tile acceletator coomand input
					dc.dcon.WriteLine("TA Area4 write adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16));
					return;//nothing yet
				case 5://Expansion (modem) port
					dc.dcon.WriteLine("Area5 write adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16));
					return;//nothing yet
				case 6://???? nothing???? olny mmu???
					dc.dcon.WriteLine("Area6 write adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16));
					return;
				case 7://Internal I.O. regs (same as p4) priv. mode only
					dc.dcon.WriteLine("Interlal I.O. registers write adr:" + Convert.ToString(adr,16) + " padr:" + Convert.ToString(padr,16) + " size :" +len.ToString() + " value " + Convert.ToString(data ,16)  );
					writehwmmr(offset,data,len);
					return;//nothing yet
			}

		}
		
		//read/write Internal CPU regs (area 7 ,region p4)
		public static uint readinmmr(uint adr,int len)
		{
			if (adr>0x3000000)
			{
				adr-=0x3000000;//get register offset
				switch (adr)
				{
					case 0 ://ccn.PTEH/32
						break;
					case 4 ://ccn.PTEL/32
						break;
					case 8 ://ccn.ttb/32
						break;
					case 0xC ://ccn.tea/32
						break;
					case 0x10://ccn.mmucr/32
						break;
					case 0x14://ccn.basra/8
						break;
					case 0x18://ccn.basrb/8
						break;
					case 0x1C://ccn.ccr/32
						break;
					case 0x20://ccn.tra/32
						break;
					case 0x24://CCN.EXPEVT/32
						break;
					case 0x28://CCNINTEVT/32
						break;
					case 0x34://CCN.PTEA/32
						break;
					case 0x38://CCN.QACR0/32
						break;
					case 0x3C://CCN.QACR1/32
						break;
					case 0x20000://UBC.BARA/32
						break;
					case 0x20004://UBC.BAMRA/8
						break;
				}
			}
			return 0;
		}
		public static void writeinmmr(uint adr,uint data,int len)
		{
            if (adr > 0x3000000)
            {
                adr -= 0x3000000;//get register offset
                switch (adr)
                {
                    case 0://ccn.PTEH/32
                        break;
                    case 4://ccn.PTEL/32
                        break;
                    case 8://ccn.ttb/32
                        break;
                    case 0xC://ccn.tea/32
                        break;
                    case 0x10://ccn.mmucr/32
                        break;
                    case 0x14://ccn.basra/8
                        break;
                    case 0x18://ccn.basrb/8
                        break;
                    case 0x1C://ccn.ccr/32
                        break;
                    case 0x20://ccn.tra/32
                        break;
                    case 0x24://CCN.EXPEVT/32
                        break;
                    case 0x28://CCNINTEVT/32
                        break;
                    case 0x34://CCN.PTEA/32
                        break;
                    case 0x38://CCN.QACR0/32
                        break;
                    case 0x3C://CCN.QACR1/32
                        break;
                    case 0x20000://UBC.BARA/32
                        break;
                    case 0x20004://UBC.BAMRA/8
                        break;
                }
            }

        }
		
		//read/write HW regs (after the bios)- EXTERNAL hardware
		public static uint readhwmmr(uint adr,int len)
		{
			return 0;
		}
		public static void writehwmmr(uint adr,uint data,int len)
		{

		}

		#region not used any more
		// read/write to system ram -  not used anymore 
		public static unsafe uint readsys(uint adr,int len)
		{
			switch (len)
			{
				case 1:
					return ram[adr];
				case 2:
					fixed(byte *p=&ram[adr])
						return *(ushort*)p;
				case 4:
					fixed(byte *p=&ram[adr])
						return *(uint*)p;
			}
			return 0;
		}
		public static unsafe void writesys(uint adr,uint data,int len)
		{
			switch (len)
			{
				case 1:
					ram[adr]=(byte)data;
					return;
				case 2:
					fixed(byte *p=&ram[adr])
						*(ushort*)p=(ushort)data;
					return;
				case 4:
					fixed(byte *p=&ram[adr])
						*(uint*)p=data;
					return;
			}
		}
		
		#endregion
	}
}
