using System;
using System.Runtime.InteropServices;
// Ok , here is where we get to .net 2.0 (c# 2.0)
// i realy like the new version .. hehehe mhehehehehe
using System.Collections.Generic;

namespace pointerlib
{

    /// <summary>
	/// A class that implements helper functions
	/// for using unmanaged memomy (allocated not inside the .net enviroment)
	/// with the use of unmanaged pointers (unsafe compilation must be enabled)
	/// </summary>
	public unsafe class unmanaged_pointer:unmanaged_pointer_interface
	{
		#region win32 imports
		[DllImport("kernel32.dll")]
		public static extern bool IsBadReadPtr ( 
			void* lp,
			uint Length);
		[DllImport("kernel32.dll")]
		public static extern bool IsBadWritePtr ( 
			void* lp,
			uint Length);
		[DllImport("kernel32.dll", EntryPoint="RtlFillMemory")]
		public static extern void FillMemory ( 
			void* Destination,
			uint Length,
			byte Fill);
		/*[DllImport("ole32.dll")]
		static extern void* CoTaskMemAlloc ( 
			uint size);
		[DllImport("ole32.dll")]
		static extern void CoTaskMemFree ( 
			void* buffer);
		[DllImport("ole32.dll")]
		static extern void* CoTaskMemRealloc ( 
			void* buffer,
			uint size);*/
		[DllImport("crtdll.dll")]
		static extern void memmove ( 
			void* dVoid,
			void* sVoid,
			uint size);
		[DllImport("kernel32.dll", EntryPoint="RtlMoveMemory")]
		static extern void CopyMemory ( 
			void* Destination,
			void* Source,
			uint Length);
		[DllImport("kernel32.dll", EntryPoint="RtlZeroMemory")]
		public static extern void ZeroMemory ( 
			void* Destination,
			uint Length);
		#endregion

		const uint errval=0xFFFFFFFF;
		byte *ptr=null;					//our pointer
		uint memsz=errval;					//Allocated memory size

		#region contructors
		/// <summary>
		/// Default ctor
		/// </summary>
		public unmanaged_pointer()
		{

		}
		/// <summary>
		/// Creates a unmanaged_pointer object with preallocated memory
		/// </summary>
		/// <param name="size">Size of the memory to allocate</param>
		public unmanaged_pointer(uint size)
		{
			realloc(size);
		}
		/// <summary>
		/// Creates a unmanaged_pointer object from a pointer
		/// </summary>
		/// <param name="from_ptr">Pointer to a memory block</param>
		public unmanaged_pointer(void *from_ptr)
		{
			Ptr=from_ptr;
			memsz=errval;
		}
		/// <summary>
		/// Creates a unmanaged_pointer object from a pointer
		/// that points to data of knowen length
		/// </summary>
		/// <param name="from_ptr">Pointer to a memory block</param>
		/// <param name="Length">Size of the memory block</param>
		public unmanaged_pointer(void *from_ptr,uint Length)
		{
			Ptr=from_ptr;
			memsz=Length;
		}

		/// <summary>
		/// Creates a unmanaged_pointer object , allocates memory
		/// and copies the array data to i'ts memory
		/// </summary>
		/// <param name="fromArr">The array to use as source</param>
		public unmanaged_pointer(byte[] fromArr)
		{
			realloc((uint)fromArr.Length);
			ArrCpy(fromArr,0,0,(uint)fromArr.Length);
		}

		/// <summary>
		/// Default dctor
		/// </summary>
		~unmanaged_pointer()
		{
			//Free any reserved memory to avoid memory leaks..
			free();
		}
		#endregion

		#region Pointer Get/Set props
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as sbyte [sbyte*]
		/// </summary>
		public sbyte*Ptr_sbyte
		{
			get
			{
				return (sbyte*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as short [short*]
		/// </summary>
		public short*Ptr_short
		{
			get
			{
				return (short*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as int [int*]
		/// </summary>
		public int*Ptr_int
		{
			get
			{
				return (int*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as byte [byte*]
		/// </summary>
		public byte*Ptr_byte
		{
			get
			{
				return ptr;
			}
			set
			{
				free();memsz=errval;ptr=value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as ushort [ushort*]
		/// </summary>
		public ushort*Ptr_ushort
		{
			get
			{
				return (ushort*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as uint [uint*]
		/// </summary>
		public uint*Ptr_uint
		{
			get
			{
				return (uint*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as void [void*]
		/// </summary>
		public void*Ptr
		{
			get
			{
				return (void*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		#endregion

		#region Legth;Alloc/realloc/free
		/// <summary>
		/// Gets the length of teh memory block
		/// If the size is unkown (pointer inited by the user using the ptr propertys]
		/// the size is Set.If the size is knowen , the memory block is resized
		/// </summary>
		public uint Length
		{
			get
			{
				if (memsz==errval)
					throw new Exception("Size not suported on user inited pointers");
				else
					return memsz;
			}
			set
			{
				if (memsz!=errval)
				{
					realloc(value);
				}
				else
					memsz=value;
			}
		}
		/// <summary>
		/// Indexer to access the class as a byte array
		/// </summary>
		public byte this [uint index]   // Indexer declaration
		{
			get 
			{
				return ptr[index];
			}
			set 
			{
				ptr[index]=value;
			}
		}

		/// <summary>
		/// Frees the allocated memory block , if allocated
		/// </summary>
		public void free()
		{
			if (ptr!=null)
			{
                Marshal.FreeCoTaskMem(new IntPtr(ptr));
                ptr=null;
				memsz=0;
			}
		}
		/// <summary>
		/// Allocates size bytes, frees teh previus memoryblock if allocated
		/// </summary>
		/// <param name="size">the length in bytes to allocate</param>
		public void alloc(uint size)
		{
			if (ptr!=null)
				free();
			ptr=(byte*)Marshal.AllocCoTaskMem((int)size).ToPointer();
			if (ptr==null)
				throw new OutOfMemoryException("Can't Create Memoby block of size " + size.ToString());
			memsz=size;
		}
		/// <summary>
		/// Reallocates the memoryblock ["resizes"]
		/// you may need to update your pointers after calling this method
		/// </summary>
		/// <param name="newsize">the new size for the memory block</param>
		public void realloc(uint newsize)
		{
			memsz=newsize;
			//do reallocation
			ptr=(byte*)Marshal.ReAllocCoTaskMem(new IntPtr(ptr),(int)newsize).ToPointer();
			if (ptr==null)
				throw new OutOfMemoryException("Can't Create Memoby block of size " + newsize.ToString());
		}

		#endregion 

		#region Array/memcpy's;FromArray
		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size*sizeof(byte) bytes
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">Bytes to copy</param>
		public void ArrCpy(byte[] src,uint sOffset,uint dOffset,uint size)
		{
			fixed (byte*t=&src[0])
			{
				memcpy(t,sOffset,dOffset,size*sizeof(byte));
			}
		}

		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size bytes*sizeof(ushort)
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">ushorts to copy</param>
		public void ArrCpy(ushort[] src,uint sOffset,uint dOffset,uint size)
		{
			fixed (ushort*t=&src[0])
			{
				memcpy(t,sOffset,dOffset,size*sizeof(ushort));
			}
		}

		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size bytes*sizeof(uint)
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">uints to copy</param>
		public void ArrCpy(uint[] src,uint sOffset,uint dOffset,uint size)
		{
			fixed (uint*t=&src[0])
			{
				memcpy(t,sOffset,dOffset,size*sizeof(uint));
			}
		}

		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size bytes*sizeof(ushort)
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">shorts to copy</param>
		public void ArrCpy(short[] src,uint sOffset,uint dOffset,uint size)
		{
			fixed (short*t=&src[0])
			{
				memcpy(t,sOffset,dOffset,size*sizeof(short));
			}
		}

		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size bytes*sizeof(uint)
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">ints to copy</param>
		public void ArrCpy(int[] src,uint sOffset,uint dOffset,uint size)
		{
			fixed (int*t=&src[0])
			{
				memcpy(t,sOffset,dOffset,size*sizeof(int));
			}
		}

		
		#region 32 bit Copy
		#if cpy_32b
		//32 byte struct
		struct bt32
		{
			public uint uint1;
			public uint uint2;
			public uint uint3;
			public uint uint4;
			public uint uint5;
			public uint uint6;
			public uint uint7;
			public uint uint8;
		}
		#endif
		#endregion
		
		/// <summary>
		/// Pointer Memcpy, implemented with C# , as fast as memcpy_crt, 
		/// a bit slower than winapi copymem;Platform independant.
		/// </summary>
		/// <param name="from">Source pointer</param>
		/// <param name="sOffset">Source offset</param>
		/// <param name="dOffset">destination offset</param>
		/// <param name="size">Size (in bytes)</param>
		public void memcpy_cs(void*from,uint sOffset,uint dOffset,uint size)
		{
			byte*frm_b=(byte*)from;
			uint*frm_dw=(uint*)from;
			long*frm_qw=(long*)from;
			#region 32 bit Copy
			#if cpy_32b
			bt32*frm_bt32=(bt32*)from;
			#endif
			#endregion
			uint*ptr_dw=(uint*)ptr;
			long*ptr_qw=(long*)ptr;
			#region 32 bit Copy
			#if cpy_32b
			bt32*ptr_bt32=(bt32*)ptr;

			if (size>32)
			{
				uint mbt32=(dOffset+size)>>5;
				uint csize=(size>>5)<<5;//lol , an & will eb faster but i'm lazy..
				uint sOffset_t=sOffset;
				for (uint i=dOffset>>5;i<mbt32;i++)
				{
					ptr_bt32[i]=frm_bt32[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
			#endif
			#endregion
			if (size>8)
			{
				uint mqw=(dOffset+size)>>3;
				uint csize=(size>>3)<<3;//lol , an & will eb faster but i'm lazy..
				uint sOffset_t=sOffset;
				for (uint i=dOffset>>3;i<mqw;i++)
				{
					ptr_qw[i]=frm_qw[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
			if (size>0x4)
			{
				uint mdw=(dOffset+size)>>2;
				uint csize=(size>>2)<<2;//lol , an & will eb faster but i'm lazy..
				uint sOffset_t=sOffset;
				for (uint i=dOffset>>2;i<mdw;i++)
				{
					ptr_dw[i]=frm_dw[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
			for (uint i=dOffset;i<(dOffset+size);i++)
			{
				ptr[i]=frm_b[sOffset++];
			}
		}
		/// <summary>
		/// Pointer Memcpy, uses CRT (c runtime) memmove , the same speed as memcpy_cs
		/// a bit slower than winapi copymem;Platform dependant(win32).
		/// </summary>
		/// <param name="from">Source pointer</param>
		/// <param name="sOffset">Source offset</param>
		/// <param name="dOffset">destination offset</param>
		/// <param name="size">Size (in bytes)</param>
		public void memcpy_crt(void*from,uint sOffset,uint dOffset,uint size)
		{
			memmove((byte*)ptr+dOffset,(byte*)from+sOffset,size);
		}

		/// <summary>
		/// Pointer Memcpy, uses winapi memcpy , about 20% 
		/// faster that the other methods;Platform dependant(win32).
		/// </summary>
		/// <param name="from">Source pointer</param>
		/// <param name="sOffset">Source offset</param>
		/// <param name="dOffset">destination offset</param>
		/// <param name="size">Size (in bytes)</param>
		public void memcpy(void*from,uint sOffset,uint dOffset,uint size)
		{
			CopyMemory((byte*)ptr+dOffset,(byte*)from+sOffset,size);
		}

		/// <summary>
		/// Copes a part of teh memory buffer to the destination pointer
		/// </summary>
		/// <param name="to">Destination Pointer</param>
		/// <param name="sOffset">Source Offset</param>
		/// <param name="dOffset">Destination Offser</param>
		/// <param name="size">Size (in bytes)</param>
		public void CopyTo(void*to,uint sOffset,uint dOffset,uint size)
		{
			CopyMemory((byte*)to+dOffset,(byte*)ptr+sOffset,size);
		}

		/// <summary>
		/// Allocates memory and copies the contents of the array onto
		/// the allocated memory
		/// </summary>
		/// <param name="fromArr">Source Array</param>
		public void FromArray(byte[] fromArr)
		{
			realloc((uint)fromArr.Length);
			ArrCpy(fromArr,0,0,(uint)fromArr.Length);
		}

		#endregion
	
		#region Read*/Write* methods
		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport uints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		/// <returns></returns>
		public byte ReadByte(uint i)
		{
			return ptr[i];
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport uints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		public void WriteByte(uint i,byte data)
		{
			ptr[i]=data;
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport uints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		public short ReadShort(uint i)
		{
			return ((short*)ptr)[i];
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport uints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		public void WriteShort(uint i,short data)
		{
			((short*)ptr)[i]=data;
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport uints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		public int ReadInt(uint i)
		{
			return ((int*)ptr)[i];
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport uints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		public void WriteInt(uint i,int data)
		{
			((int*)ptr)[i]=data;
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport uints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		public short ReadShort_ptroffset(uint offset)
		{
			return *(short*)(ptr+offset);
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport uints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		public void WriteShort_ptroffset(uint offset,short data)
		{
			*(short*)(ptr+offset)=data;
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport uints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		public int ReadInt_ptroffset(uint offset)
		{
			return *(int*)(ptr+offset);
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport uints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		public void WriteInt_ptroffset(uint offset,int data)
		{
			*(int*)(ptr+offset)=data;
		}


		/// <summary>
		/// Read/Write methods to read/write custom data structures
		/// </summary>
		/// <param name="i">offset inside memoryblock</param>
		/// <param name="data">data pointer to read/write</param>
		/// <param name="len">bytes to read/write</param>
		public void ReadCustom(uint i,void*data,uint len)
		{
			memcpy(data,ptr,i,0,len);
		}

		/// <summary>
		/// Read/Write methods to read/write custom data structures
		/// </summary>
		/// <param name="i">offset inside memoryblock</param>
		/// <param name="data">data pointer to read/write</param>
		/// <param name="len">bytes to read/write</param>
		public void WriteCustom(uint i,void*data,uint len)
		{
			memcpy(ptr,data,0,i,len);
		}

		#endregion

		#region Zero/FreeMem;IsBad*
		/// <summary>
		/// Fill a part of the memory block with zeros
		/// </summary>
		/// <param name="dOffset">Start from here</param>
		/// <param name="Length">How many bytes to fill</param>
		public void ZeroMemory(uint dOffset,uint Length)
		{
			ZeroMemory(ptr+dOffset,Length);
		}
		/// <summary>
		/// Fill teh entire memory block with zeros
		/// </summary>
		public void ZeroMemory()
		{
			ZeroMemory(ptr,this.Length);
		}

		/// <summary>
		/// Fill the a part of the memory block with "value" bytes
		/// </summary>
		/// <param name="dOffset">Start from this offset</param>
		/// <param name="Length">How many bytes to write</param>
		/// <param name="value">Value to write</param>
		public void FillMemory(uint dOffset,uint Length,byte value)
		{
			FillMemory(ptr+dOffset,Length,value);
		}
		/// <summary>
		/// Fill the entire memory block with "value" bytes
		/// </summary>
		/// <param name="value">Value to write</param>
		public void FillMemory(byte value)
		{
			FillMemory(ptr,this.Length,value);
		}

		
		/// <summary>
		/// Return true is you can't read from the memory block
		/// </summary>
		public bool isBadReadPtr
		{
			get{return IsBadReadPtr(ptr,this.Length);}
		}

		/// <summary>
		/// Return true is you can't write to the memory block
		/// </summary>
		public bool isBadWritePtr
		{
			get{return IsBadReadPtr(ptr,this.Length);}
		}

		#endregion

		#region Static implementations
		/// <summary>
		/// Pointer Memcpy, implemented with C# , as fast as memcpy_crt, 
		/// a bit slower than winapi copymem;Platform independant.
		/// </summary>
		/// <param name="ptr">Destination pointer</param>
		/// <param name="from">Source Pointer</param>
		/// <param name="dOffset">Destination Offset</param>
		/// <param name="sOffset">Source Offset</param>
		/// <param name="size">Number of bytes to copy</param>
		public static void memcpy_cs(void* dest,void*from,uint sOffset,uint dOffset,uint size)
		{
			byte*frm_b=(byte*)from;
			uint*frm_dw=(uint*)from;
			long*frm_qw=(long*)from;
			#region 32 bit Copy
			#if cpy_32b
			bt32*frm_bt32=(bt32*)from;
			#endif
			#endregion
			byte*ptr=(byte*)from;
			uint*ptr_dw=(uint*)ptr;
			long*ptr_qw=(long*)ptr;
			#region 32 bit Copy
			#if cpy_32b
			bt32*ptr_bt32=(bt32*)ptr;

			if (size>32)
			{
				uint mbt32=(dOffset+size)>>5;
				uint csize=(size>>5)<<5;//lol , an & will eb faster but i'm lazy..
				uint sOffset_t=sOffset;
				for (uint i=dOffset>>5;i<mbt32;i++)
				{
					ptr_bt32[i]=frm_bt32[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
			#endif
			#endregion

			if (size>8)
			{
				uint mqw=(dOffset+size)>>3;
				uint csize=(size>>3)<<3;//lol , an & will eb faster but i'm lazy..
				uint sOffset_t=sOffset;
				for (uint i=dOffset>>3;i<mqw;i++)
				{
					ptr_qw[i]=frm_qw[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
			if (size>4)
			{
				uint mdw=(dOffset+size)>>2;
				uint csize=(size>>2)<<2;//lol , an & will eb faster but i'm lazy..
				uint sOffset_t=sOffset;
				for (uint i=dOffset>>2;i<mdw;i++)
				{
					ptr_dw[i]=frm_dw[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
			for (uint i=dOffset;i<(dOffset+size);i++)
			{
				ptr[i]=frm_b[sOffset++];
			}
		}

		/// <summary>
		/// Pointer Memcpy, uses CRT (c runtime) memmove , the same speed as memcpy_cs
		/// a bit slower than winapi copymem;Platform dependant(win32).
		/// </summary>
		/// <param name="ptr">Destination pointer</param>
		/// <param name="from">Source Pointer</param>
		/// <param name="dOffset">Destination Offset</param>
		/// <param name="sOffset">Source Offset</param>
		/// <param name="size">Number of bytes to copy</param>
		public static void memcpy_crt(void* ptr,void*from,uint sOffset,uint dOffset,uint size)
		{
			memmove((byte*)ptr+dOffset,(byte*)from+sOffset,size);
		}

		/// <summary>
		/// Pointer Memcpy, uses winapi memcpy , about 20% 
		/// faster that the other methods;Platform dependant(win32).
		/// </summary>
		/// <param name="ptr">Destination pointer</param>
		/// <param name="from">Source Pointer</param>
		/// <param name="dOffset">Destination Offset</param>
		/// <param name="sOffset">Source Offset</param>
		/// <param name="size">Number of bytes to copy</param>
		public static void memcpy(void* ptr,void*from,uint sOffset,uint dOffset,uint size)
		{
			CopyMemory((byte*)ptr+dOffset,(byte*)from+sOffset,size);
		}

		/// <summary>
		/// Read/Write methods to read/write custom data structures
		/// </summary>
		/// <param name="ptr">pointer to memory block</param>
		/// <param name="i">offset inside memoryblock</param>
		/// <param name="data">data pointer to read/write</param>
		/// <param name="len">bytes to read/write</param>
		public static void ReadCustom(byte* ptr,uint i,void*data,uint len)
		{
			memcpy(data,ptr,i,0,len);
		}

		/// <summary>
		/// Read/Write methods to read/write custom data structures
		/// </summary>
		/// <param name="ptr">pointer to memory block</param>
		/// <param name="i">offset inside memoryblock</param>
		/// <param name="data">data pointer to read/write</param>
		/// <param name="len">bytes to read/write</param>
		public static void WriteCustom(byte* ptr,uint i,void*data,uint len)
		{
			memcpy(ptr,data,0,i,len);
		}

        /// <summary>
        /// Copies the contents of source array (starting at offset sOffset)
        /// to the memoryblock (starting at offset dOffset)
        /// Copies size*sizeof(byte) bytes
        /// </summary>
        /// <param name="src">Source Array</param>
        /// <param name="sOffset">Source Array Offser</param>
        /// <param name="dOffset">Destination offset</param>
        /// <param name="size">Bytes to copy</param>
        public static void ArrCpy(void *ptr,byte[] src, uint sOffset, uint dOffset, uint size)
        {
            fixed (byte* t = &src[0])
            {
                memcpy(ptr,t, sOffset, dOffset, size * sizeof(byte));
            }
        }
		#endregion
	}

    #region Com And VB interfaces..
    //for com interop , vb version (uses ints , not uints)
    public unsafe interface unmanaged_pointer_interface_vb
	{

		/// <summary>
		/// Gets/Sets a pointer to the memory block , as sbyte [sbyte*]
		/// </summary>
		sbyte*Ptr_sbyte
		{
			get;
			set;
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as short [short*]
		/// </summary>
		 short*Ptr_short
		{
			get;
			set;
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as int [int*]
		/// </summary>
		 int*Ptr_int
		{
			get;
			set;
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as byte [byte*]
		/// </summary>
		 byte*Ptr_byte
		{
			get;
			set;
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as ushort [ushort*]
		/// </summary>
		 ushort*Ptr_ushort
		{
			get;
			set;
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as int [int*]
		/// </summary>
		 int*Ptr_uint
		{
			get;
			set;
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as void [void*]
		/// </summary>
		 void*Ptr
		{
			get;
			set;
		}


		#region Legth;Alloc/realloc/free
		/// <summary>
		/// Gets the length of teh memory block
		/// If the size is unkown (pointer inited by the user using the ptr propertys]
		/// the size is Set.If the size is knowen , the memory block is resized
		/// </summary>
		int Length
		{
			get;
			set;
		}
		/// <summary>
		/// Indexer to access the class as a byte array
		/// </summary>
		byte this [int index]   // Indexer declaration
		{
			get ;
			set ;
		}

		/// <summary>
		/// Frees the allocated memory block , if allocated
		/// </summary>
		void free();
		/// <summary>
		/// Allocates size bytes, frees teh previus memoryblock if allocated
		/// </summary>
		/// <param name="size">the length in bytes to allocate</param>
		void alloc(int size);
		/// <summary>
		/// Reallocates the memoryblock ["resizes"]
		/// you may need to update your pointers after calling this method
		/// </summary>
		/// <param name="newsize">the new size for the memory block</param>
		void realloc(int newsize);

		#endregion 

		#region Array/memcpy's;FromArray
		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size*sizeof(byte) bytes
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">Bytes to copy</param>
		void ArrCpy(byte[] src,int sOffset,int dOffset,int size);

		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size bytes*sizeof(ushort)
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">ushorts to copy</param>
		void ArrCpy(ushort[] src,int sOffset,int dOffset,int size);

		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size bytes*sizeof(int)
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">ints to copy</param>
		void ArrCpy(int[] src,int sOffset,int dOffset,int size);

		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size bytes*sizeof(ushort)
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">shorts to copy</param>
		void ArrCpy(short[] src,int sOffset,int dOffset,int size);


		
		#region 32 bit Copy
#if cpy_32b
		//32 byte struct
		struct bt32
		{
			 uint uint1;
			 uint uint2;
			 uint uint3;
			 uint uint4;
			 uint uint5;
			 uint uint6;
			 uint uint7;
			 uint uint8;
		}
#endif
		#endregion
		
		/// <summary>
		/// Pointer Memcpy, implemented with C# , as fast as memcpy_crt, 
		/// a bit slower than winapi copymem;Platform independant.
		/// </summary>
		/// <param name="from">Source pointer</param>
		/// <param name="sOffset">Source offset</param>
		/// <param name="dOffset">destination offset</param>
		/// <param name="size">Size (in bytes)</param>
		void memcpy_cs(void*from,int sOffset,int dOffset,int size);
		/// <summary>
		/// Pointer Memcpy, uses CRT (c runtime) memmove , the same speed as memcpy_cs
		/// a bit slower than winapi copymem;Platform dependant(win32).
		/// </summary>
		/// <param name="from">Source pointer</param>
		/// <param name="sOffset">Source offset</param>
		/// <param name="dOffset">destination offset</param>
		/// <param name="size">Size (in bytes)</param>
		void memcpy_crt(void*from,int sOffset,int dOffset,int size);
		/// <summary>
		/// Pointer Memcpy, uses winapi memcpy , about 20% 
		/// faster that the other methods;Platform dependant(win32).
		/// </summary>
		/// <param name="from">Source pointer</param>
		/// <param name="sOffset">Source offset</param>
		/// <param name="dOffset">destination offset</param>
		/// <param name="size">Size (in bytes)</param>
		void memcpy(void*from,int sOffset,int dOffset,int size);
		/// <summary>
		/// Copes a part of teh memory buffer to the destination pointer
		/// </summary>
		/// <param name="to">Destination Pointer</param>
		/// <param name="sOffset">Source Offset</param>
		/// <param name="dOffset">Destination Offser</param>
		/// <param name="size">Size (in bytes)</param>
		void CopyTo(void*to,int sOffset,int dOffset,int size);

		/// <summary>
		/// Allocates memory and copies the contents of the array onto
		/// the allocated memory
		/// </summary>
		/// <param name="fromArr">Source Array</param>
		void FromArray(byte[] fromArr);

		#endregion
	
		#region Read*/Write* methods
		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		/// <returns></returns>
		byte ReadByte(int i);

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		void WriteByte(int i,byte data);
		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		short ReadShort(int i);
		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		void WriteShort(int i,short data);

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		int ReadInt(int i);

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		void WriteInt(int i,int data);

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		short ReadShort_ptroffset(int offset);

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		void WriteShort_ptroffset(int offset,short data);

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		int ReadInt_ptroffset(int offset);

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		void WriteInt_ptroffset(int offset,int data);

		/// <summary>
		/// Read/Write methods to read/write custom data structures
		/// </summary>
		/// <param name="i">offset inside memoryblock</param>
		/// <param name="data">data pointer to read/write</param>
		/// <param name="len">bytes to read/write</param>
		void ReadCustom(int i,void*data,int len);

		/// <summary>
		/// Read/Write methods to read/write custom data structures
		/// </summary>
		/// <param name="i">offset inside memoryblock</param>
		/// <param name="data">data pointer to read/write</param>
		/// <param name="len">bytes to read/write</param>
		void WriteCustom(int i,void*data,int len);
		#endregion

		#region Zero/FreeMem;IsBad*
		/// <summary>
		/// Fill a part of the memory block with zeros
		/// </summary>
		/// <param name="dOffset">Start from here</param>
		/// <param name="Length">How many bytes to fill</param>
		void ZeroMemory(int dOffset,int Length);
		/// <summary>
		/// Fill teh entire memory block with zeros
		/// </summary>
		void ZeroMemory();
		/// <summary>
		/// Fill the a part of the memory block with "value" bytes
		/// </summary>
		/// <param name="dOffset">Start from this offset</param>
		/// <param name="Length">How many bytes to write</param>
		/// <param name="value">Value to write</param>
		void FillMemory(int dOffset,int Length,byte value);
		/// <summary>
		/// Fill the entire memory block with "value" bytes
		/// </summary>
		/// <param name="value">Value to write</param>
		void FillMemory(byte value);

		
		/// <summary>
		/// Return true is you can't read from the memory block
		/// </summary>
		bool isBadReadPtr
		{
			get;
		}

		/// <summary>
		/// Return true is you can't write to the memory block
		/// </summary>
		bool isBadWritePtr
		{
			get;
		}

		#endregion
	}
    
    // for com interop, normal version
    public unsafe interface unmanaged_pointer_interface
    {

        #region Get_Set
        /// <summary>
        /// Gets/Sets a pointer to the memory block , as sbyte [sbyte*]
        /// </summary>
        sbyte* Ptr_sbyte
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/Sets a pointer to the memory block , as short [short*]
        /// </summary>
        short* Ptr_short
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/Sets a pointer to the memory block , as int [int*]
        /// </summary>
        int* Ptr_int
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/Sets a pointer to the memory block , as byte [byte*]
        /// </summary>
        byte* Ptr_byte
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/Sets a pointer to the memory block , as ushort [ushort*]
        /// </summary>
        ushort* Ptr_ushort
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/Sets a pointer to the memory block , as uint [uint*]
        /// </summary>
        uint* Ptr_uint
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/Sets a pointer to the memory block , as void [void*]
        /// </summary>
        void* Ptr
        {
            get;
            set;
        }
        #endregion

        #region Legth;Alloc/realloc/free
        /// <summary>
        /// Gets the length of teh memory block
        /// If the size is unkown (pointer inited by the user using the ptr propertys]
        /// the size is Set.If the size is knowen , the memory block is resized
        /// </summary>
        uint Length
        {
            get;
            set;
        }
        /// <summary>
        /// Indexer to access the class as a byte array
        /// </summary>
        byte this[uint index]   // Indexer declaration
        {
            get;
            set;
        }

        /// <summary>
        /// Frees the allocated memory block , if allocated
        /// </summary>
        void free();
        /// <summary>
        /// Allocates size bytes, frees teh previus memoryblock if allocated
        /// </summary>
        /// <param name="size">the length in bytes to allocate</param>
        void alloc(uint size);
        /// <summary>
        /// Reallocates the memoryblock ["resizes"]
        /// you may need to update your pointers after calling this method
        /// </summary>
        /// <param name="newsize">the new size for the memory block</param>
        void realloc(uint newsize);

        #endregion

        #region Array/memcpy's;FromArray
        /// <summary>
        /// Copies the contents of source array (starting at offset sOffset)
        /// to the memoryblock (starting at offset dOffset)
        /// Copies size*sizeof(byte) bytes
        /// </summary>
        /// <param name="src">Source Array</param>
        /// <param name="sOffset">Source Array Offser</param>
        /// <param name="dOffset">Destination offset</param>
        /// <param name="size">Bytes to copy</param>
        void ArrCpy(byte[] src, uint sOffset, uint dOffset, uint size);

        /// <summary>
        /// Copies the contents of source array (starting at offset sOffset)
        /// to the memoryblock (starting at offset dOffset)
        /// Copies size bytes*sizeof(ushort)
        /// </summary>
        /// <param name="src">Source Array</param>
        /// <param name="sOffset">Source Array Offser</param>
        /// <param name="dOffset">Destination offset</param>
        /// <param name="size">ushorts to copy</param>
        void ArrCpy(ushort[] src, uint sOffset, uint dOffset, uint size);

        /// <summary>
        /// Copies the contents of source array (starting at offset sOffset)
        /// to the memoryblock (starting at offset dOffset)
        /// Copies size bytes*sizeof(uint)
        /// </summary>
        /// <param name="src">Source Array</param>
        /// <param name="sOffset">Source Array Offser</param>
        /// <param name="dOffset">Destination offset</param>
        /// <param name="size">uints to copy</param>
        void ArrCpy(uint[] src, uint sOffset, uint dOffset, uint size);

        /// <summary>
        /// Copies the contents of source array (starting at offset sOffset)
        /// to the memoryblock (starting at offset dOffset)
        /// Copies size bytes*sizeof(ushort)
        /// </summary>
        /// <param name="src">Source Array</param>
        /// <param name="sOffset">Source Array Offser</param>
        /// <param name="dOffset">Destination offset</param>
        /// <param name="size">shorts to copy</param>
        void ArrCpy(short[] src, uint sOffset, uint dOffset, uint size);
        /// <summary>
        /// Copies the contents of source array (starting at offset sOffset)
        /// to the memoryblock (starting at offset dOffset)
        /// Copies size bytes*sizeof(uint)
        /// </summary>
        /// <param name="src">Source Array</param>
        /// <param name="sOffset">Source Array Offser</param>
        /// <param name="dOffset">Destination offset</param>
        /// <param name="size">ints to copy</param>
        void ArrCpy(int[] src, uint sOffset, uint dOffset, uint size);


        #region 32 bit Copy
#if cpy_32b
		//32 byte struct
		struct bt32
		{
			 uint uint1;
			 uint uint2;
			 uint uint3;
			 uint uint4;
			 uint uint5;
			 uint uint6;
			 uint uint7;
			 uint uint8;
		}
#endif
        #endregion

        /// <summary>
        /// Pointer Memcpy, implemented with C# , as fast as memcpy_crt, 
        /// a bit slower than winapi copymem;Platform independant.
        /// </summary>
        /// <param name="from">Source pointer</param>
        /// <param name="sOffset">Source offset</param>
        /// <param name="dOffset">destination offset</param>
        /// <param name="size">Size (in bytes)</param>
        void memcpy_cs(void* from, uint sOffset, uint dOffset, uint size);
        /// <summary>
        /// Pointer Memcpy, uses CRT (c runtime) memmove , the same speed as memcpy_cs
        /// a bit slower than winapi copymem;Platform dependant(win32).
        /// </summary>
        /// <param name="from">Source pointer</param>
        /// <param name="sOffset">Source offset</param>
        /// <param name="dOffset">destination offset</param>
        /// <param name="size">Size (in bytes)</param>
        void memcpy_crt(void* from, uint sOffset, uint dOffset, uint size);
        /// <summary>
        /// Pointer Memcpy, uses winapi memcpy , about 20% 
        /// faster that the other methods;Platform dependant(win32).
        /// </summary>
        /// <param name="from">Source pointer</param>
        /// <param name="sOffset">Source offset</param>
        /// <param name="dOffset">destination offset</param>
        /// <param name="size">Size (in bytes)</param>
        void memcpy(void* from, uint sOffset, uint dOffset, uint size);
        /// <summary>
        /// Copes a part of teh memory buffer to the destination pointer
        /// </summary>
        /// <param name="to">Destination Pointer</param>
        /// <param name="sOffset">Source Offset</param>
        /// <param name="dOffset">Destination Offser</param>
        /// <param name="size">Size (in bytes)</param>
        void CopyTo(void* to, uint sOffset, uint dOffset, uint size);

        /// <summary>
        /// Allocates memory and copies the contents of the array onto
        /// the allocated memory
        /// </summary>
        /// <param name="fromArr">Source Array</param>
        void FromArray(byte[] fromArr);

        #endregion

        #region Read*/Write* methods
        /// <summary>
        /// Read/Write methods to memory block, for use with languages that do not 
        /// suport pointers (but suport uints ;) )
        /// </summary>
        /// <param name="i">target index</param>
        /// <returns></returns>
        byte ReadByte(uint i);

        /// <summary>
        /// Read/Write methods to memory block, for use with languages that do not 
        /// suport pointers (but suport uints ;) )
        /// </summary>
        /// <param name="i">target index</param>
        void WriteByte(uint i, byte data);
        /// <summary>
        /// Read/Write methods to memory block, for use with languages that do not 
        /// suport pointers (but suport uints ;) )
        /// </summary>
        /// <param name="i">target index</param>
        short ReadShort(uint i);
        /// <summary>
        /// Read/Write methods to memory block, for use with languages that do not 
        /// suport pointers (but suport uints ;) )
        /// </summary>
        /// <param name="i">target index</param>
        void WriteShort(uint i, short data);

        /// <summary>
        /// Read/Write methods to memory block, for use with languages that do not 
        /// suport pointers (but suport uints ;) )
        /// </summary>
        /// <param name="i">target index</param>
        int ReadInt(uint i);

        /// <summary>
        /// Read/Write methods to memory block, for use with languages that do not 
        /// suport pointers (but suport uints ;) )
        /// </summary>
        /// <param name="i">target index</param>
        void WriteInt(uint i, int data);

        /// <summary>
        /// Read/Write methods to memory block, for use with languages that do not 
        /// suport pointers (but suport uints ;) )
        /// </summary>
        /// <param name="offset">offset insode memoryblock</param>
        short ReadShort_ptroffset(uint offset);

        /// <summary>
        /// Read/Write methods to memory block, for use with languages that do not 
        /// suport pointers (but suport uints ;) )
        /// </summary>
        /// <param name="offset">offset insode memoryblock</param>
        void WriteShort_ptroffset(uint offset, short data);

        /// <summary>
        /// Read/Write methods to memory block, for use with languages that do not 
        /// suport pointers (but suport uints ;) )
        /// </summary>
        /// <param name="offset">offset insode memoryblock</param>
        int ReadInt_ptroffset(uint offset);

        /// <summary>
        /// Read/Write methods to memory block, for use with languages that do not 
        /// suport pointers (but suport uints ;) )
        /// </summary>
        /// <param name="offset">offset insode memoryblock</param>
        void WriteInt_ptroffset(uint offset, int data);

        /// <summary>
        /// Read/Write methods to read/write custom data structures
        /// </summary>
        /// <param name="i">offset inside memoryblock</param>
        /// <param name="data">data pointer to read/write</param>
        /// <param name="len">bytes to read/write</param>
        void ReadCustom(uint i, void* data, uint len);

        /// <summary>
        /// Read/Write methods to read/write custom data structures
        /// </summary>
        /// <param name="i">offset inside memoryblock</param>
        /// <param name="data">data pointer to read/write</param>
        /// <param name="len">bytes to read/write</param>
        void WriteCustom(uint i, void* data, uint len);
        #endregion

        #region Zero/FreeMem;IsBad*
        /// <summary>
        /// Fill a part of the memory block with zeros
        /// </summary>
        /// <param name="dOffset">Start from here</param>
        /// <param name="Length">How many bytes to fill</param>
        void ZeroMemory(uint dOffset, uint Length);
        /// <summary>
        /// Fill teh entire memory block with zeros
        /// </summary>
        void ZeroMemory();
        /// <summary>
        /// Fill the a part of the memory block with "value" bytes
        /// </summary>
        /// <param name="dOffset">Start from this offset</param>
        /// <param name="Length">How many bytes to write</param>
        /// <param name="value">Value to write</param>
        void FillMemory(uint dOffset, uint Length, byte value);
        /// <summary>
        /// Fill the entire memory block with "value" bytes
        /// </summary>
        /// <param name="value">Value to write</param>
        void FillMemory(byte value);


        /// <summary>
        /// Return true is you can't read from the memory block
        /// </summary>
        bool isBadReadPtr
        {
            get;
        }

        /// <summary>
        /// Return true is you can't write to the memory block
        /// </summary>
        bool isBadWritePtr
        {
            get;
        }

        #endregion
    }
    
	public unsafe class unmanaged_pointer_vb:unmanaged_pointer_interface_vb
	{
		#region win32 imports
		[DllImport("kernel32.dll")]
		public static extern bool IsBadReadPtr ( 
			void* lp,
			int Length);
		[DllImport("kernel32.dll")]
		public static extern bool IsBadWritePtr ( 
			void* lp,
			int Length);
		[DllImport("kernel32.dll", EntryPoint="RtlFillMemory")]
		public static extern void FillMemory ( 
			void* Destination,
			int Length,
			byte Fill);
		[DllImport("ole32.dll")]
		static extern void* CoTaskMemAlloc ( 
			int size);
		[DllImport("ole32.dll")]
		static extern void CoTaskMemFree ( 
			void* buffer);
		[DllImport("ole32.dll")]
		static extern void* CoTaskMemRealloc ( 
			void* buffer,
			int size);
		[DllImport("crtdll.dll")]
		static extern void memmove ( 
			void* dVoid,
			void* sVoid,
			int size);
		[DllImport("kernel32.dll", EntryPoint="RtlMoveMemory")]
		static extern void CopyMemory ( 
			void* Destination,
			void* Source,
			int Length);
		[DllImport("kernel32.dll", EntryPoint="RtlZeroMemory")]
		public static extern void ZeroMemory ( 
			void* Destination,
			int Length);
		#endregion

		const int errval=-1;
		byte *ptr=null;					//our pointer
		int memsz=errval;					//Allocated memory size

		#region contructors
		/// <summary>
		/// Default ctor
		/// </summary>
		public unmanaged_pointer_vb()
		{

		}
		/// <summary>
		/// Creates a unmanaged_pointer object with preallocated memory
		/// </summary>
		/// <param name="size">Size of the memory to allocate</param>
		public unmanaged_pointer_vb(int size)
		{
			realloc(size);
		}
		/// <summary>
		/// Creates a unmanaged_pointer object from a pointer
		/// </summary>
		/// <param name="from_ptr">Pointer to a memory block</param>
		public unmanaged_pointer_vb(void *from_ptr)
		{
			Ptr=from_ptr;
			memsz=errval;
		}

		/// <summary>
		/// Creates a unmanaged_pointer object from a pointer
		/// that points to data of knowen length
		/// </summary>
		/// <param name="from_ptr">Pointer to a memory block</param>
		/// <param name="Length">Size of the memory block</param>
		public unmanaged_pointer_vb(void *from_ptr,int Length)
		{
			Ptr=from_ptr;
			memsz=Length;
		}

		/// <summary>
		/// Creates a unmanaged_pointer object , allocates memory
		/// and copies the array data to i'ts memory
		/// </summary>
		/// <param name="fromArr">The array to use as source</param>
		public unmanaged_pointer_vb(byte[] fromArr)
		{
			realloc((int)fromArr.Length);
			ArrCpy(fromArr,0,0,(int)fromArr.Length);
		}

		/// <summary>
		/// Default dctor
		/// </summary>
		~unmanaged_pointer_vb()
		{
			//Free any reserved memory to avoid memory leaks..
			free();
		}
		#endregion

		#region Pointer Get/Set props
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as sbyte [sbyte*]
		/// </summary>
		public sbyte*Ptr_sbyte
		{
			get
			{
				return (sbyte*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as short [short*]
		/// </summary>
		public short*Ptr_short
		{
			get
			{
				return (short*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as int [int*]
		/// </summary>
		public int*Ptr_int
		{
			get
			{
				return (int*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as byte [byte*]
		/// </summary>
		public byte*Ptr_byte
		{
			get
			{
				return ptr;
			}
			set
			{
				free();memsz=errval;ptr=value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as ushort [ushort*]
		/// </summary>
		public ushort*Ptr_ushort
		{
			get
			{
				return (ushort*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as int [int*]
		/// </summary>
		public int*Ptr_uint
		{
			get
			{
				return (int*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		/// <summary>
		/// Gets/Sets a pointer to the memory block , as void [void*]
		/// </summary>
		public void*Ptr
		{
			get
			{
				return (void*)ptr;
			}
			set
			{
				free();memsz=errval;ptr=(byte*)value;
			}
		}
		#endregion

		#region Legth;Alloc/realloc/free
		/// <summary>
		/// Gets the length of teh memory block
		/// If the size is unkown (pointer inited by the user using the ptr propertys]
		/// the size is Set.If the size is knowen , the memory block is resized
		/// </summary>
		public int Length
		{
			get
			{
				if (memsz==errval)
					throw new Exception("Size not suported on user inited pointers");
				else
					return memsz;
			}
			set
			{
				if (memsz!=errval)
				{
					realloc(value);
				}
				else
					memsz=value;
			}
		}
		/// <summary>
		/// Indexer to access the class as a byte array
		/// </summary>
		public byte this [int index]   // Indexer declaration
		{
			get 
			{
				return ptr[index];
			}
			set 
			{
				ptr[index]=value;
			}
		}

		/// <summary>
		/// Frees the allocated memory block , if allocated
		/// </summary>
		public void free()
		{
			if (ptr!=null)
			{
				CoTaskMemFree(ptr);
				ptr=null;
				memsz=0;
			}
		}
		/// <summary>
		/// Allocates size bytes, frees teh previus memoryblock if allocated
		/// </summary>
		/// <param name="size">the length in bytes to allocate</param>
		public void alloc(int size)
		{
			if (ptr!=null)
				free();
			ptr=(byte*)CoTaskMemAlloc(size);
			memsz=size;
		}
		/// <summary>
		/// Reallocates the memoryblock ["resizes"]
		/// you may need to update your pointers after calling this method
		/// </summary>
		/// <param name="newsize">the new size for the memory block</param>
		public void realloc(int newsize)
		{
			memsz=newsize;
			//do reallocation
			ptr=(byte*)CoTaskMemRealloc(ptr,newsize);
		}

		#endregion 

		#region Array/memcpy's;FromArray
		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size*sizeof(byte) bytes
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">Bytes to copy</param>
		public void ArrCpy(byte[] src,int sOffset,int dOffset,int size)
		{
			fixed (byte*t=&src[0])
			{
				memcpy(t,sOffset,dOffset,size*sizeof(byte));
			}
		}

		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size bytes*sizeof(ushort)
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">ushorts to copy</param>
		public void ArrCpy(ushort[] src,int sOffset,int dOffset,int size)
		{
			fixed (ushort*t=&src[0])
			{
				memcpy(t,sOffset,dOffset,size*sizeof(ushort));
			}
		}

		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size bytes*sizeof(int)
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">ints to copy</param>
		public void ArrCpy(int[] src,int sOffset,int dOffset,int size)
		{
			fixed (int*t=&src[0])
			{
				memcpy(t,sOffset,dOffset,size*sizeof(int));
			}
		}

		/// <summary>
		/// Copies the contents of source array (starting at offset sOffset)
		/// to the memoryblock (starting at offset dOffset)
		/// Copies size bytes*sizeof(ushort)
		/// </summary>
		/// <param name="src">Source Array</param>
		/// <param name="sOffset">Source Array Offser</param>
		/// <param name="dOffset">Destination offset</param>
		/// <param name="size">shorts to copy</param>
		public void ArrCpy(short[] src,int sOffset,int dOffset,int size)
		{
			fixed (short*t=&src[0])
			{
				memcpy(t,sOffset,dOffset,size*sizeof(short));
			}
		}



		
		#region 32 bit Copy
#if cpy_32b
		//32 byte struct
		struct bt32
		{
			public uint uint1;
			public uint uint2;
			public uint uint3;
			public uint uint4;
			public uint uint5;
			public uint uint6;
			public uint uint7;
			public uint uint8;
		}
#endif
		#endregion
		
		/// <summary>
		/// Pointer Memcpy, implemented with C# , as fast as memcpy_crt, 
		/// a bit slower than winapi copymem;Platform independant.
		/// </summary>
		/// <param name="from">Source pointer</param>
		/// <param name="sOffset">Source offset</param>
		/// <param name="dOffset">destination offset</param>
		/// <param name="size">Size (in bytes)</param>
		public void memcpy_cs(void*from,int sOffset,int dOffset,int size)
		{
			byte*frm_b=(byte*)from;
			int*frm_dw=(int*)from;
			long*frm_qw=(long*)from;
			#region 32 bit Copy
#if cpy_32b
			bt32*frm_bt32=(bt32*)from;
#endif
			#endregion
			int*ptr_dw=(int*)ptr;
			long*ptr_qw=(long*)ptr;
			#region 32 bit Copy
#if cpy_32b
			bt32*ptr_bt32=(bt32*)ptr;

			if (size>32)
			{
				uint mbt32=(dOffset+size)>>5;
				uint csize=(size>>5)<<5;//lol , an & will eb faster but i'm lazy..
				uint sOffset_t=sOffset;
				for (uint i=dOffset>>5;i<mbt32;i++)
				{
					ptr_bt32[i]=frm_bt32[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
#endif
			#endregion
			if (size>8)
			{
				int mqw=(dOffset+size)>>3;
				int csize=(size>>3)<<3;//lol , an & will be faster but i'm lazy..
				int sOffset_t=sOffset;
				for (int i=dOffset>>3;i<mqw;i++)
				{
					ptr_qw[i]=frm_qw[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
			if (size>4)
			{
				int mdw=(dOffset+size)>>2;
				int csize=(size>>2)<<2;//lol , an & will eb faster but i'm lazy..
				int sOffset_t=sOffset;
				for (int i=dOffset>>2;i<mdw;i++)
				{
					ptr_dw[i]=frm_dw[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
			for (int i=dOffset;i<(dOffset+size);i++)
			{
				ptr[i]=frm_b[sOffset++];
			}
		}
		/// <summary>
		/// Pointer Memcpy, uses CRT (c runtime) memmove , the same speed as memcpy_cs
		/// a bit slower than winapi copymem;Platform dependant(win32).
		/// </summary>
		/// <param name="from">Source pointer</param>
		/// <param name="sOffset">Source offset</param>
		/// <param name="dOffset">destination offset</param>
		/// <param name="size">Size (in bytes)</param>
		public void memcpy_crt(void*from,int sOffset,int dOffset,int size)
		{
			memmove((byte*)ptr+dOffset,(byte*)from+sOffset,size);
		}

		/// <summary>
		/// Pointer Memcpy, uses winapi memcpy , about 20% 
		/// faster that the other methods;Platform dependant(win32).
		/// </summary>
		/// <param name="from">Source pointer</param>
		/// <param name="sOffset">Source offset</param>
		/// <param name="dOffset">destination offset</param>
		/// <param name="size">Size (in bytes)</param>
		public void memcpy(void*from,int sOffset,int dOffset,int size)
		{
			CopyMemory((byte*)ptr+dOffset,(byte*)from+sOffset,size);
		}

		/// <summary>
		/// Copes a part of teh memory buffer to the destination pointer
		/// </summary>
		/// <param name="to">Destination Pointer</param>
		/// <param name="sOffset">Source Offset</param>
		/// <param name="dOffset">Destination Offser</param>
		/// <param name="size">Size (in bytes)</param>
		public void CopyTo(void*to,int sOffset,int dOffset,int size)
		{
			CopyMemory((byte*)to+dOffset,(byte*)ptr+sOffset,size);
		}

		/// <summary>
		/// Allocates memory and copies the contents of the array onto
		/// the allocated memory
		/// </summary>
		/// <param name="fromArr">Source Array</param>
		public void FromArray(byte[] fromArr)
		{
			realloc((int)fromArr.Length);
			ArrCpy(fromArr,0,0,(int)fromArr.Length);
		}

		#endregion
	
		#region Read*/Write* methods
		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		/// <returns></returns>
		public byte ReadByte(int i)
		{
			return ptr[i];
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		public void WriteByte(int i,byte data)
		{
			ptr[i]=data;
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		public short ReadShort(int i)
		{
			return ((short*)ptr)[i];
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		public void WriteShort(int i,short data)
		{
			((short*)ptr)[i]=data;
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		public int ReadInt(int i)
		{
			return ((int*)ptr)[i];
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="i">target index</param>
		public void WriteInt(int i,int data)
		{
			((int*)ptr)[i]=data;
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		public short ReadShort_ptroffset(int offset)
		{
			return *(short*)(ptr+offset);
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		public void WriteShort_ptroffset(int offset,short data)
		{
			*(short*)(ptr+offset)=data;
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		public int ReadInt_ptroffset(int offset)
		{
			return *(int*)(ptr+offset);
		}

		/// <summary>
		/// Read/Write methods to memory block, for use with languages that do not 
		/// suport pointers (but suport ints ;) )
		/// </summary>
		/// <param name="offset">offset insode memoryblock</param>
		public void WriteInt_ptroffset(int offset,int data)
		{
			*(int*)(ptr+offset)=data;
		}


		/// <summary>
		/// Read/Write methods to read/write custom data structures
		/// </summary>
		/// <param name="i">offset inside memoryblock</param>
		/// <param name="data">data pointer to read/write</param>
		/// <param name="len">bytes to read/write</param>
		public void ReadCustom(int i,void*data,int len)
		{
			memcpy(data,ptr,0,i,len);
		}

		/// <summary>
		/// Read/Write methods to read/write custom data structures
		/// </summary>
		/// <param name="i">offset inside memoryblock</param>
		/// <param name="data">data pointer to read/write</param>
		/// <param name="len">bytes to read/write</param>
		public void WriteCustom(int i,void*data,int len)
		{
			memcpy(ptr,data,i,0,len);
		}
		
		#endregion

		#region Zero/FreeMem;IsBad*
		/// <summary>
		/// Fill a part of the memory block with zeros
		/// </summary>
		/// <param name="dOffset">Start from here</param>
		/// <param name="Length">How many bytes to fill</param>
		public void ZeroMemory(int dOffset,int Length)
		{
			ZeroMemory(ptr+dOffset,Length);
		}
		/// <summary>
		/// Fill teh entire memory block with zeros
		/// </summary>
		public void ZeroMemory()
		{
			ZeroMemory(ptr,this.Length);
		}

		/// <summary>
		/// Fill the a part of the memory block with "value" bytes
		/// </summary>
		/// <param name="dOffset">Start from this offset</param>
		/// <param name="Length">How many bytes to write</param>
		/// <param name="value">Value to write</param>
		public void FillMemory(int dOffset,int Length,byte value)
		{
			FillMemory(ptr+dOffset,Length,value);
		}
		/// <summary>
		/// Fill the entire memory block with "value" bytes
		/// </summary>
		/// <param name="value">Value to write</param>
		public void FillMemory(byte value)
		{
			FillMemory(ptr,this.Length,value);
		}

		
		/// <summary>
		/// Return true is you can't read from the memory block
		/// </summary>
		public bool isBadReadPtr
		{
			get{return IsBadReadPtr(ptr,this.Length);}
		}

		/// <summary>
		/// Return true is you can't write to the memory block
		/// </summary>
		public bool isBadWritePtr
		{
			get{return IsBadReadPtr(ptr,this.Length);}
		}

		#endregion

		#region Static implementations /
		/// <summary>
		/// Pointer Memcpy, implemented with C# , as fast as memcpy_crt, 
		/// a bit slower than winapi copymem;Platform independant.
		/// </summary>
		/// <param name="ptr">Destination pointer</param>
		/// <param name="from">Source Pointer</param>
		/// <param name="dOffset">Destination Offset</param>
		/// <param name="sOffset">Source Offset</param>
		/// <param name="size">Number of bytes to copy</param>
		public static void memcpy_cs(void* dest,void*from,int dOffset,int sOffset,int size)
		{
			byte*frm_b=(byte*)from;
			int*frm_dw=(int*)from;
			long*frm_qw=(long*)from;
			#region 32 bit Copy
#if cpy_32b
			bt32*frm_bt32=(bt32*)from;
#endif
			#endregion
			byte*ptr=(byte*)from;
			int*ptr_dw=(int*)ptr;
			long*ptr_qw=(long*)ptr;
			#region 32 bit Copy
#if cpy_32b
			bt32*ptr_bt32=(bt32*)ptr;

			if (size>32)
			{
				int mbt32=(dOffset+size)>>5;
				int csize=(size>>5)<<5;//lol , an & will eb faster but i'm lazy..
				int sOffset_t=sOffset;
				for (int i=dOffset>>5;i<mbt32;i++)
				{
					ptr_bt32[i]=frm_bt32[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
#endif
			#endregion

			if (size>8)
			{
				int mqw=(dOffset+size)>>3;
				int csize=(size>>3)<<3;//lol , an & will eb faster but i'm lazy..
				int sOffset_t=sOffset;
				for (int i=dOffset>>3;i<mqw;i++)
				{
					ptr_qw[i]=frm_qw[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
			if (size>4)
			{
				int mdw=(dOffset+size)>>2;
				int csize=(size>>2)<<2;//lol , an & will eb faster but i'm lazy..
				int sOffset_t=sOffset;
				for (int i=dOffset>>2;i<mdw;i++)
				{
					ptr_dw[i]=frm_dw[sOffset_t++];
				}
				dOffset+=csize;
				sOffset+=csize;
				size-=csize;
			}
			for (int i=dOffset;i<(dOffset+size);i++)
			{
				ptr[i]=frm_b[sOffset++];
			}
		}

		/// <summary>
		/// Pointer Memcpy, uses CRT (c runtime) memmove , the same speed as memcpy_cs
		/// a bit slower than winapi copymem;Platform dependant(win32).
		/// </summary>
		/// <param name="ptr">Destination pointer</param>
		/// <param name="from">Source Pointer</param>
		/// <param name="dOffset">Destination Offset</param>
		/// <param name="sOffset">Source Offset</param>
		/// <param name="size">Number of bytes to copy</param>
		public static void memcpy_crt(void* ptr,void*from,int dOffset,int sOffset,int size)
		{
			memmove((byte*)ptr+dOffset,(byte*)from+sOffset,size);
		}

		/// <summary>
		/// Pointer Memcpy, uses winapi memcpy , about 20% 
		/// faster that the other methods;Platform dependant(win32).
		/// </summary>
		/// <param name="ptr">Destination pointer</param>
		/// <param name="from">Source Pointer</param>
		/// <param name="dOffset">Destination Offset</param>
		/// <param name="sOffset">Source Offset</param>
		/// <param name="size">Number of bytes to copy</param>
		public static void memcpy(void* ptr,void*from,int dOffset,int sOffset,int size)
		{
			CopyMemory((byte*)ptr+dOffset,(byte*)from+sOffset,size);
		}

		/// <summary>
		/// Read/Write methods to read/write custom data structures
		/// </summary>
		/// <param name="ptr">pointer to memory block</param>
		/// <param name="i">offset inside memoryblock</param>
		/// <param name="data">data pointer to read/write</param>
		/// <param name="len">bytes to read/write</param>
		public static void ReadCustom(byte* ptr,int i,void*data,int len)
		{
			memcpy(data,ptr,0,i,len);
		}

		/// <summary>
		/// Read/Write methods to read/write custom data structures
		/// </summary>
		/// <param name="ptr">pointer to memory block</param>
		/// <param name="i">offset inside memoryblock</param>
		/// <param name="data">data pointer to read/write</param>
		/// <param name="len">bytes to read/write</param>
		public static void WriteCustom(byte* ptr,int i,void*data,int len)
		{
			memcpy(ptr,data,i,0,len);
		}
		
		#endregion
    }
    #endregion

    //A simple class for memory manament
    //it allocates mem and can de allocate it all :)
    public unsafe class MemoryManager
    {
        public List<unmanaged_pointer> pCollection = new List<unmanaged_pointer>();
        ~MemoryManager()
        {
            FreeAll();
        }

        public void* AllocMem(uint MemSize)
        {
            unmanaged_pointer temp = new unmanaged_pointer(MemSize);
            pCollection.Add(temp);
            temp.ZeroMemory();
            return temp.Ptr;
        }
        public void FreeAll()
        {
            for (int i = 0; i < pCollection.Count; i++)
            {
                pCollection[i].free();
                pCollection.RemoveAt(i);
            }
            pCollection.TrimExcess();
            GC.Collect();
        }
    }

	//A simple class for memory manament
	//it allocates mem and can de allocate it all :)
	public static unsafe class MemoryManager_s
	{
		 static public List<unmanaged_pointer> pCollection = new List<unmanaged_pointer>();
		//~MemoryManager_s()
		//{
		//	FreeAll();
		//}

		static public void* AllocMem(uint MemSize)
		{
			unmanaged_pointer temp = new unmanaged_pointer(MemSize);
			pCollection.Add(temp);
			temp.ZeroMemory();
			return temp.Ptr;
		}
		static public void FreeAll()
		{
			for (int i = 0; i < pCollection.Count; i++)
			{
				pCollection[i].free();
				pCollection.RemoveAt(i);
			}
			pCollection.TrimExcess();
			GC.Collect();
		}
	}
}
