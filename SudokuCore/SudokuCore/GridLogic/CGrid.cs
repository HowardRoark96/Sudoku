using System;
using System.Collections.Generic;

namespace SudokuCore
{
	public class CCell
	{
		public CCell(int value)
		{
			Value = value;
		}

		public int Value { get; set; }
		public int DisplayValue { get; set; }
		public List<int> Marks => m_Marks;
		public bool Correct => Value == DisplayValue;

		private readonly List<int> m_Marks = new List<int>();
	}

	public abstract class CGrid : ICloneable
	{
		public CCell this[int rowIndex, int columnIndex]
		{
			get { return Cells[rowIndex, columnIndex]; }
		}
		public abstract int Size { get; }
		public bool Completed
		{
			get
			{
				for (int i = 0; i < Size; i++)
				{
					for (int j = 0; j < Size; j++)
					{
						if (Cells[i, j].DisplayValue != Cells[i, j].Value)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		public Tuple<int,int> CheckInput(int rowIndex, int columnIndex, int value)
		{
			return OnCheckInput(rowIndex, columnIndex, value);
		}
		public void Refresh(EGridDifficulty difficulty)
		{	
			UpdateRealValues(1000);
			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < Size; j++)
				{
					this[i, j].DisplayValue = this[i, j].Value;
					this[i, j].Marks.Clear();
				}
			}
			UpdateValues(difficulty);
		}
		public void ShowResult()
		{
			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < Size; j++)
				{
					Cells[i,j].DisplayValue = Cells[i, j].Value;
				}
			}
		}
		public object Clone()
		{
			return OnClone();
		}

		public void ConsoleOutput()
		{
			CLogger.Lg.Debug("Value");
			for (int i = 0; i < Size; i++)
			{
				string line = "";
				for (int j = 0; j < Size; j++)
				{
					line += Cells[i, j].Value + " ";
				}
				CLogger.Lg.Debug(line);
			}

			CLogger.Lg.Debug("Display Value");
			for (int i = 0; i < Size; i++)
			{
				string line = "";
				for (int j = 0; j < Size; j++)
				{
					line += Cells[i, j].DisplayValue + " ";
				}
				CLogger.Lg.Debug(line);
			}
			Console.WriteLine();
		}

		/// <summary>
		/// Updates grid numbers.
		/// </summary>
		/// <param name="count">Count of diffrent updates. </param>
		protected abstract void UpdateRealValues(int count);
		/// <summary>
		/// Updates visible grid numbers.
		/// </summary>
		/// <param name="difficulty"></param>
		protected abstract void UpdateValues(EGridDifficulty difficulty);
		/// <summary>
		/// Get clone of current grid.
		/// </summary>
		/// <returns></returns>
		protected abstract object OnClone();

		/// <summary>
		/// Checks input of number in cell. 
		/// </summary>
		/// <returns>Adress of cell that alredy has input number. null - input is correct.</returns>
		protected virtual Tuple<int, int> OnCheckInput(int rowIndex, int columnIndex, int value)
		{
			for (int i = 0; i < Size; i++)
			{
				if (this[i, columnIndex].DisplayValue == value)
				{
					return new Tuple<int, int>(i, columnIndex);
				}
				if (this[rowIndex, i].DisplayValue == value)
				{
					return new Tuple<int, int>(rowIndex, i);
				}			
			}
			return null;
		}

		protected void SwapBigRows(int first, int second)
		{
			SwapRows(first * 3, second * 3);
			SwapRows(first * 3 + 1, second * 3 + 1);
			SwapRows(first * 3 + 2, second * 3 + 2);
		}
		protected void SwapBigColumns(int first, int second)
		{
			SwapColumns(first * 3, second * 3);
			SwapColumns(first * 3 + 1, second * 3 + 1);
			SwapColumns(first * 3 + 2, second * 3 + 2);
		}

		protected void SwapRows(int first, int second)
		{
			for (int i = 0; i < Size; i++)
			{
				SwapCells(first, i, second, i);
			}
		}
		protected void SwapColumns(int first, int second)
		{
			for (int i = 0; i < Size; i++)
			{
				SwapCells(i, first, i, second);
			}
		}

		protected void Transpose()
		{
			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < i; j++)
				{
					SwapCells(i, j, j, i);
				}
			}
		}

		private void SwapCells(int row1, int column1, int row2, int column2)
		{
			//Cells[row1, column1] = Cells[row1, column1] ^ Cells[row2, column2];
			//Cells[row2, column2] = Cells[row1, column1] ^ Cells[row2, column2];
			//Cells[row1, column1] = Cells[row1, column1] ^ Cells[row2, column2];

			CCell temp_cell = Cells[row1, column1];
			Cells[row1, column1] = Cells[row2, column2];
			Cells[row2, column2] = temp_cell;
		}

		//protected int[,] Cells = new int[,]
		//{
		//	{1,2,3,4,5,6,7,8,9},
		//	{4,5,6,7,8,9,1,2,3},
		//	{7,8,9,1,2,3,4,5,6},
		//	{2,3,4,5,6,7,8,9,1},
		//	{5,6,7,8,9,1,2,3,4},
		//	{8,9,1,2,3,4,5,6,7},
		//	{3,4,5,6,7,8,9,1,2},
		//	{6,7,8,9,1,2,3,4,5},
		//	{9,1,2,3,4,5,6,7,8},
		//};

		protected CCell[,] Cells;
		protected static Random Random = new Random();
	}

	enum EGridUpdateOpotunities
	{
		Transpose = 1,
		SwapRows = 2,
		SwapColumns = 3,
		SwapBigRows = 4,
		SwapBigColumns = 5
	}
	public enum EGridDifficulty
	{
		Easy = 30,
		Medium = 40,
		Hard = 50,
		VeryHard = 65
	}
}
