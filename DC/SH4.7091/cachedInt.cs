using System;

namespace DC4Ever
{
	/// <summary>
	/// sh4 Recompiler.
	/// </summary>
	public class fastint
	{
		const uint maxblocklen = 2048;
		const uint maxblocks = 64;
		static int cache_sort=0;
		static codeblock[] recdata=new codeblock[maxblocks];
		static ushort[] cachet = new ushort[maxblocklen];
		static uint recpc,opcode,blocklen;
		//find opcode block
		static public void RunPC()
		{//get opcodes while not branch /jump or max block len
			recpc=sh4.pc;
			int ch=isincache();

			if (ch!=-1)
			{
				sh4.execblock(recdata[ch]);
				return;
			}

			#region Build Block
			int bp=0;
			for (recpc=sh4.pc;recpc<(sh4.pc +maxblocklen);recpc+=2)
			{
				opcode = mem.read(recpc,2);
				cachet[bp]=(ushort)opcode;bp+=2;
				//jumps
				if  ((opcode == 11) ||	//opcode=0000_0000_0000_1011
					((opcode & 0x00ff)== 0x0023) ||//opcode and 0000_0000_1111_1111=0000_0000_0010_0011
					((opcode & 0xf0ff)== 0x402B) ||//opcode and 1111_0000_1111_1111=0100_0000_0010_1011
					((opcode & 0xff00)== 0x8900) ||	//opcode and 1111_1111_0000_0000=1000_1011_0000_0000
					((opcode & 0xff00)== 0x8b00) ||	//opcode and 1111_1111_0000_0000=1000_1011_0000_0000
					((opcode & 0xff00)== 0x8f00) ||	//opcode and 1111_1111_0000_0000=1000_1111_0000_0000
					((opcode & 0xff00)== 0x8d00) ||	//opcode and 1111_1111_0000_0000=1000_1101_0000_0000
					((opcode & 0xf000)== 0xa000) )	//opcode and 1111_0000_0000_0000=1010_0000_0000_0000
				{
					//end block- if delayslot
					if (((opcode & 0xff00)!=0x8b00))
					{
						recpc+=2;
						cachet[bp]=(ushort)mem.read (recpc,2);bp+=2;
					}
					goto eof;

				}
	
			}
			eof:
			blocklen=recpc-sh4.pc;
			recpc=sh4.pc;
			ch=(int)addtocache();
			sh4.execblock(recdata[ch]);

#endregion
		}
		public struct codeblock
		{
			public uint valid;
			public uint calls;
			public uint rest;
			public uint pc;
			public uint len;
			public System.Delegate code;
			public ushort[] cache;
		}

		static public int isincache()
		{//pc is in cache,corect flags
			cache_sort+=1;
			if (cache_sort>800000) {shortcache();cache_sort=0;}
			for (int i =0;i<maxblocks;i++)
			{
				if (recdata[i].pc==recpc)
				{//we found the block- no flags checks but they must be done
					recdata[i].calls++;
					return i;
				}
			}
		return -1;
		}
		static public uint addtocache()
		{
			//cache_short=1;
			uint smaler=0xffffffff,idx=0;
			for (uint i =0;i<maxblocks;i++)
			{
				if (recdata[i].calls < smaler)
				{//find the smallest block
					smaler=recdata[i].calls;
					idx=i;
				}
				
			}
			//we have the less called 
			recdata[idx].calls=1;
			recdata[idx].pc=recpc;
			recdata[idx].len=blocklen;
			cachet.CopyTo(recdata[idx].cache,0);
			return idx;
		}  
		static public uint showstats()
		{
			uint asize=0,idx=0;
			for (uint i =0;i<maxblocks;i++)
			{
				//dc.dcon.WriteLine (recdata[i].calls.ToString());
				if (recdata[i].calls ==0)
				{//we found the block- no flags checks and they must be done
					idx=i;
					goto nf;
				}
			asize+=recdata[i].len+2;
			}
			nf:
			if (idx==0) idx=1;
			dc.dcon.WriteLine("Number of Blocks : " +idx.ToString());
			dc.dcon.WriteLine("Average Sise of Blocks : " +Convert.ToString(asize/idx));
			return idx;
		}  
		public static void  cancelcache(uint adr)
		{

		}
		public static void  shortcache()
		{
			int ord=0;
			codeblock tmp;
			int v1,v2;
			for (v1=0 ;v1<maxblocks;v1++)
			{
				if (ord==1) return;
				ord=1;
				for (v2=1;v2<maxblocks;v2++)
				{
					if (recdata[v2].calls>recdata[v2-1].calls)// > words(counter2 + 1) Then
					{
						tmp=recdata[v2];//words(0) = words(counter2)
						recdata[v2]=recdata[v2-1];//words(counter2) = words(counter2 + 1)
						recdata[v2-1]=tmp;//words(counter2 + 1) = words(0)
						ord=0;
					}
				}
			}
		}

		public static void init()
		{
			for (uint i =0;i<maxblocks;i++)
			{
			recdata[i].cache = new ushort[maxblocklen];
			}
		}

		public static void disableblock(uint adr)
		{
			for (int i =0;i<maxblocks;i++)
			{
				if ((recdata[i].pc>=adr)&&((recdata[i].len+recdata[i].pc) <=adr))
				{//we found the block- no flags checks but they must be done
					recdata[i].calls=0;
					recdata[i].pc=0;
				}
			}
		}
	}
}
