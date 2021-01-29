using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuCore.Protocol
{
	public enum ECommandResult
	{
		/// <summary>
		/// Error sending request. Read SErrorRequestStructure for the details.
		/// </summary>
		Error = -1,
		Success = 0,
	}

	public enum EParseResult
	{
		Success,
		UnknownCommand,
		WrongRequest,
		WrongResponce,
	}

	public enum ECommands
	{
		Ping,
		Connect,

		MakeLobby,
		LobbyList,
		ConnectToLobby,

		GetLobbyInfo,
		GetPlayerInfo,

		GetGrid,
		GetMarks,
		UpdateGrid,

		GetServerTime,
		
		EnterNumber,
		EnterMark,

		CheckWin,

		GiveUp,

		Reconnect
	}
}
