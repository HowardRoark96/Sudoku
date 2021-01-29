using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace SudokuCore.Protocol
{
	public struct SRequest
	{
		public const byte BoundByte = 0x55;

		public ECommands Command;
		public ulong UserId;
		public byte[] DataStream;
	}
	public struct SResponce
	{
		public const byte BoundByte = 0xAA;

		public ECommands Command;
		public ECommandResult CommandResult;
		public byte[] DataStream;
	}

	public struct SGridResponce
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 81)]
		public int[,] Grid;
		public SGridResponce(CGrid grid)
		{
			Grid = new int[grid.Size, grid.Size];
			for (int i = 0; i < grid.Size; i++)
			{
				for (int j = 0; j < grid.Size; j++)
				{
					Grid[i,j] = grid[i, j].DisplayValue;
				}
			}
		}
	}

	public static class ProtocolHelper
	{
		//public static SRequest GetRequest(byte[] data, int length)
		//{
		//	if (data[0] != SRequest.BoundByte && data[length-1] != SRequest.BoundByte)
		//	{
				
		//	}

		//	SRequest result = new SRequest();
		//	result.Command = BitConverter.ToInt32(data, 0);
		//	result.UserId = BitConverter.ToUInt64(data, sizeof(int));
		//	Array.Copy(data, sizeof(byte) + sizeof(int) + sizeof(ulong), result.DataStream, 0, length);
		//}

		public static byte[] GridAnswerToByteArray(SGridResponce answer)
		{
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);

			int size = answer.Grid.GetLength(0);

			writer.Write(size);

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					writer.Write(Convert.ToByte(answer.Grid[i, j]));
				}
			}

			return stream.GetBuffer();
		}
		public static SGridResponce ByteArrayToGridAnswer(byte[] data)
		{
			MemoryStream stream = new MemoryStream(data);
			BinaryReader reader = new BinaryReader(stream);

			int size = reader.ReadInt32();

			SGridResponce answer = new SGridResponce();
			answer.Grid = new int[9, 9];

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					answer.Grid[i, j] = reader.ReadByte();
				}
			}

			return answer;
		}

		public static byte[] StructureToByteArray(object obj)
		{
			int size = Marshal.SizeOf(obj);
			byte[] arr = new byte[size];
			IntPtr ptr = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(obj, ptr, true);
			Marshal.Copy(ptr, arr, 0, size);
			Marshal.FreeHGlobal(ptr);
			return arr;
		}
		public static void ByteArrayToStructure(byte[] byteArray, ref object obj)
		{
			int size = Marshal.SizeOf(obj);
			IntPtr i = Marshal.AllocHGlobal(size);
			Marshal.Copy(byteArray, 0, i, size);
			obj = Marshal.PtrToStructure(i, obj.GetType());
			Marshal.FreeHGlobal(i);
		}
	}
}
