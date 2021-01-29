using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuCore
{
	public class CGridClassic : CGrid
	{
		public override int Size => 9;
		public virtual int BlockSize => 3;

		public CGridClassic()
		{
			Cells = new CCell[,]
			{
				{new CCell(1),new CCell(2),new CCell(3),new CCell(4),new CCell(5),new CCell(6),new CCell(7),new CCell(8),new CCell(9)},
				{new CCell(4),new CCell(5),new CCell(6),new CCell(7),new CCell(8),new CCell(9),new CCell(1),new CCell(2),new CCell(3)},
				{new CCell(7),new CCell(8),new CCell(9),new CCell(1),new CCell(2),new CCell(3),new CCell(4),new CCell(5),new CCell(6)},
				{new CCell(2),new CCell(3),new CCell(4),new CCell(5),new CCell(6),new CCell(7),new CCell(8),new CCell(9),new CCell(1)},
				{new CCell(5),new CCell(6),new CCell(7),new CCell(8),new CCell(9),new CCell(1),new CCell(2),new CCell(3),new CCell(4)},
				{new CCell(8),new CCell(9),new CCell(1),new CCell(2),new CCell(3),new CCell(4),new CCell(5),new CCell(6),new CCell(7)},
				{new CCell(3),new CCell(4),new CCell(5),new CCell(6),new CCell(7),new CCell(8),new CCell(9),new CCell(1),new CCell(2)},
				{new CCell(6),new CCell(7),new CCell(8),new CCell(9),new CCell(1),new CCell(2),new CCell(3),new CCell(4),new CCell(5)},
				{new CCell(9),new CCell(1),new CCell(2),new CCell(3),new CCell(4),new CCell(5),new CCell(6),new CCell(7),new CCell(8)},
			};
		}
	
		protected override void UpdateRealValues(int count)
		{
			for (int i = 0; i < count; i++)
			{
				switch ((EGridUpdateOpotunities)Random.Next(1, Enum.GetValues(typeof(EGridUpdateOpotunities)).Length))
				{
					case EGridUpdateOpotunities.Transpose:
						{
							Transpose();
							break;
						}
					case EGridUpdateOpotunities.SwapRows:
						{
							int big_row = Random.Next(0, BlockSize);
							int first = Random.Next(0, BlockSize);
							int second = 0;
							do
							{
								second = Random.Next(0, BlockSize);
							} while (second == first);
							SwapRows(first + big_row * BlockSize, second + big_row * BlockSize);
							break;
						}
					case EGridUpdateOpotunities.SwapColumns:
						{
							int big_column = Random.Next(0, BlockSize);
							int first = Random.Next(0, BlockSize);
							int second = 0;
							do
							{
								second = Random.Next(0, BlockSize);
							} while (second == first);
							SwapColumns(first + big_column * BlockSize, second + big_column * BlockSize);
							break;
						}
					case EGridUpdateOpotunities.SwapBigRows:
						{
							int first = Random.Next(0, BlockSize);
							int second = 0;
							do
							{
								second = Random.Next(0, BlockSize);
							} while (second == first);
							SwapBigRows(first, second);
							break;
						}
					case EGridUpdateOpotunities.SwapBigColumns:
						{
							int first = Random.Next(0, BlockSize);
							int second = 0;
							do
							{
								second = Random.Next(0, BlockSize);
							} while (second == first);
							SwapBigColumns(first, second);
							break;
						}
				}
			}
		}
		protected override void UpdateValues(EGridDifficulty difficulty)
		{
			//TODO remake the function. Now it is not working algorithm.
			int count = (int)difficulty;

			while (true)
			{
				for (int i = 0; i < Size; i++)
				{
					for (int j = 0; j < Size; j++)
					{
						bool errase = Random.Next(0, 30) == 0;
						if (errase)
						{
							Cells[i, j].DisplayValue = 0;
							count--;
							if (count == 0)
							{
								return;
							}
						}
					}
				}
			}
		}
		protected override object OnClone()
		{
			return new CGridClassic()
			{
				Cells = this.Cells
			};
		}
		protected override Tuple<int, int> OnCheckInput(int rowIndex, int columnIndex, int value)
		{
			var result = base.OnCheckInput(rowIndex, columnIndex, value);
			if (result != null)
			{
				return result;
			}

			int big_row = rowIndex / BlockSize;
			int big_column = columnIndex / BlockSize;

			for (int i = 0; i < BlockSize; i++)
			{
				for (int j = 0; j < BlockSize; j++)
				{
					if (this[big_row * BlockSize + i, big_column*BlockSize + j].DisplayValue == value)
					{
						return new Tuple<int, int>(big_row * BlockSize + i, big_column * BlockSize + i);
					}
				}
			}

			return null;
		}
	}
}
