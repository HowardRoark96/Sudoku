using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuCore.GridLogic
{
	public abstract class CCommand
	{
		public abstract void Execute();
		public abstract void Undo();
	}

	/// <summary>
	/// Universal transaction server.
	/// </summary>
	public class CTransactionServer
	{
		/// <summary>
		/// Are there any commands to undo?
		/// </summary>
		public bool UndoEnabled => m_UndoCommands.Count > 0;
		/// <summary>
		/// Are there any commands to redo?
		/// </summary>
		public bool RedoEnabled => m_RedoCommands.Count > 0;

		/// <summary>
		/// Execute command. Transaction server remembers the command.
		/// </summary>
		/// <param name="command"></param>
		public void Execute(CCommand command)
		{
			if (command != null)
			{
				m_UndoCommands.Push(command);
				m_RedoCommands.Clear();
				command.Execute();
			}
		}
		/// <summary>
		/// Execute last undoing command.
		/// </summary>
		public void Redo()
		{
			if (RedoEnabled)
			{
				var command = m_RedoCommands.Pop();
				command.Execute();
				m_UndoCommands.Push(command);
			}
		}
		/// <summary>
		/// Undo last executed command.
		/// </summary>
		public void Undo()
		{
			if (UndoEnabled)
			{
				var command = m_UndoCommands.Pop();
				command.Undo();
				m_RedoCommands.Push(command);
			}
		}
		/// <summary>
		/// Clear transaction server.
		/// </summary>
		public void Clear()
		{
			m_UndoCommands.Clear();
			m_RedoCommands.Clear();
		}

		private Stack<CCommand> m_UndoCommands = new Stack<CCommand>();
		private Stack<CCommand> m_RedoCommands = new Stack<CCommand>();
	}
}
