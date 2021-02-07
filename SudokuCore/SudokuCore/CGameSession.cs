using System;
using System.Collections.Generic;
using System.Text;
using SudokuCore.GridLogic;

namespace SudokuCore
{
	//TODO make options load.
	public class CGameSession
	{
		/// <summary>
		/// Constructor for lobby. To make simmilar grid for few players.
		/// </summary>
		/// <param name="grid"></param>
		internal CGameSession(CGrid grid)
		{
			m_Grid = grid;
		}
		/// <summary>
		/// Constructor for single player.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="difficulty"></param>
		internal CGameSession(EGridType type, EGridDifficulty difficulty)
		{
			m_Grid = CGridFactory.Create(type);
			m_eDicfficulty = difficulty;
		}

		/// <summary>
		/// Debug constructor. Use until nothing is made.
		/// </summary>
		public CGameSession(EGridDifficulty difficulty)
		{
			m_Grid = CGridFactory.Create(EGridType.ClassicGrid);
			m_eDicfficulty = difficulty;
		}

		public bool UndoEnabled { get; set; }

		/// <summary>
		/// Creating new game.
		/// </summary>
		public void NewGame()
		{
			m_Grid.Refresh(m_eDicfficulty);
		}
		/// <summary>
		/// Starts the timer and game actions.
		/// </summary>
		public void StartGame()
		{
			//TODO make timer.
			throw new NotImplementedException();
		}
		/// <summary>
		/// Pause game and timer.
		/// </summary>
		public void StopGame()
		{
			//TODO make timer.
			throw new NotImplementedException();
		}
		/// <summary>
		/// Saving game. //TODO make CGameSession serialization.
		/// </summary>
		public void SaveGame()
		{
			//TODO make serialization.
			throw new NotImplementedException();
		}
		/// <summary>
		/// Fill display values of list with real values and stop the game.
		/// </summary>
		public void GiveUp() 
		{
			m_Grid.ShowResult();
		}
		/// <summary>
		/// Returns is grid completed.
		/// </summary>
		/// <returns></returns>
		public bool CheckDone()
		{
			return m_Grid.Completed;
		}

		/// <summary>
		/// Debug option.
		/// </summary>
		public void ConsoleOutput()
		{
			m_Grid.ConsoleOutput();
		}

		/// <summary>
		/// Get number of some grid cell. 
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <param name="columnIndex"></param>
		/// <returns></returns>
		public int GetNumber(int rowIndex, int columnIndex)
		{
			return m_Grid[rowIndex, columnIndex].DisplayValue;
		}
		/// <summary>
		/// Get marks of some grid cell.
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <param name="columnIndex"></param>
		/// <returns></returns>
		public List<int> GetMarks(int rowIndex, int columnIndex)
		{
			return m_Grid[rowIndex, columnIndex].Marks;
		}	

		/// <summary>
		/// Enter a number to some grid cell.
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <param name="columnIndex"></param>
		/// <param name="value"></param>
		/// <returns>Returns the adress of cell that has the same number.</returns>
		public Tuple<int, int> EnterNumber(int rowIndex, int columnIndex, int value)
		{
			var same_number_adress = m_Grid.CheckInput(rowIndex, columnIndex, value);
			if (same_number_adress == null)
			{
				var command = new CCommandCellEnterNumber(m_Grid[rowIndex, columnIndex], value);
				m_TransactServer.Execute(command);
			}

			if (m_Grid.Completed)
			{
				StopGame();
			}

			return same_number_adress;
		}
		/// <summary>
		/// Enter a mark to some grid cell.
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <param name="columnIndex"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool EnterMark(int rowIndex, int columnIndex, int value)
		{
			throw new NotImplementedException();
		}		
		/// <summary>
		/// Clear grid cell.
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <param name="columnIndex"></param>
		public void ClearCell(int rowIndex, int columnIndex)
		{
			throw new NotImplementedException();
		}

		public Tuple<int, int, int> GetAdvise()
		{
			throw new NotImplementedException();
		}
		public int GetErrorsCount()
		{
			throw new NotImplementedException();
		}
		public DateTime GetTime()
		{
			//TODO make timer.
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Cancel last player action.
		/// </summary>
		public void Undo()
		{
			if (m_TransactServer.UndoEnabled)
			{
				m_TransactServer.Undo();
			}
		}
		/// <summary>
		/// Execute the last canceled action.
		/// </summary>
		public void Redo()
		{
			if (m_TransactServer.RedoEnabled)
			{
				m_TransactServer.Undo();
			}
		}
		
		protected EGridDifficulty m_eDicfficulty;
		protected int m_lErrorsCount;

		protected CGrid m_Grid;
		protected  readonly CTransactionServer m_TransactServer = new CTransactionServer();
	}

	//TODO make options load.
	public class CMultiplayerGameSession
	{
		
	}
}
