using System;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			SudokuCore.CGameSession session = new SudokuCore.CGameSession(SudokuCore.EGridDifficulty.VeryHard);
			session.NewGame();
			session.ConsoleOutput();
			//session.EnterNumber(8, 8, 9);
			//session.ConsoleOutput();
			//session.Undo();
			//session.ConsoleOutput();
		}
	}
}
