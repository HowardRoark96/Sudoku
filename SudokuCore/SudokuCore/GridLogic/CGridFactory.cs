using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuCore.GridLogic
{
	/// <summary>
	/// TODO remove it. Do it with reflection.
	/// </summary>
	static class CGridFactory
	{
		public static CGrid Create(EGridType type)
		{
			switch (type)
			{
				case EGridType.ClassicGrid:
					{
						return new CGridClassic();
					}
			}
			return null;
		}
	}

	public enum EGridType
	{
		ClassicGrid
	}
}
