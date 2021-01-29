using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuCore.GridLogic
{
	class CCommandCellEnterNumber : CCommand
	{
		public CCommandCellEnterNumber(CCell cell, int value)
		{
			m_lValue = value;
			m_Cell = cell;
			m_lOldValue = cell.DisplayValue;
			m_OldMarks = cell.Marks.ToArray();
		}

		public override void Execute()
		{
			m_Cell.Marks.Clear();
			m_Cell.DisplayValue = m_lValue;	
		}
		public override void Undo()
		{
			m_Cell.Marks.AddRange(m_OldMarks);
			m_Cell.DisplayValue = m_lOldValue;
		}

		private CCell m_Cell;
		private int m_lValue;

		private int[] m_OldMarks;
		private int m_lOldValue;
	}
}
