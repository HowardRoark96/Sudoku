using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuCore
{
	public class CServer
	{
		#region Common user functions. //TODO We have to make some database requests here.
		/// <summary>
		/// User registration function. 
		/// </summary>
		/// <returns></returns>
		public bool Registrate(string userName, string password)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// User log in function.
		/// </summary>
		/// <returns>returns user playerId</returns>
		public ulong LogIn(string userName, string password)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Disconnection from the server.
		/// </summary>
		/// <returns></returns>
		public bool LogOut()
		{
			throw new NotImplementedException();
		}
		public bool ChangePassword()
		{
			throw new NotImplementedException();
		}
		public void RecoverPassword()
		{
			throw new NotImplementedException();
		}

		public void SendMessage(ulong senderId, ulong reciverId)
		{
			throw new NotImplementedException();
		}
		public void GetPlayerInfo(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public void GetVersion()
		{
			throw new NotImplementedException();
		}
		public void GetUpdates()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Lobby functions. //TODO Nothing is made.
		public ulong CreateLobby(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public bool ConnectToLobby(ulong playerId, uint lobbyId)
		{
			throw new NotImplementedException();
		}
		public bool ReconnectToLobby(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public bool LeaveLobby(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public List<int> GetLobbyesList()
		{
			throw new NotImplementedException();
		}
		public object GetLobbyInfo()
		{
			throw new NotImplementedException();
		}

		public void PrintMessage(ulong senderId, string message)
		{
			throw new NotImplementedException();
		}
		public void TellMessage(ulong senderId, byte[] message)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Game functions. //TODO Nothing is made.
		public void NewGame(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public void StartGame(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public void StopGame(ulong playerId)
		{
			throw new NotImplementedException();
		}

		public void GiveUp(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public int GetNumber(ulong playerId, int rowIndex, int columnIndex)
		{
			throw new NotImplementedException();
		}
		public List<int> GetMarks(ulong playerId, int rowIndex, int columnIndex)
		{
			throw new NotImplementedException();
		}
		public Tuple<int, int> EnterNumber(ulong playerId, int rowIndex, int columnIndex, int value)
		{
			throw new NotImplementedException();
		}
		public bool EnterMark(ulong playerId, int rowIndex, int columnIndex, int value)
		{
			throw new NotImplementedException();
		}
		public void ClearCell(ulong playerId, int rowIndex, int columnIndex)
		{
			throw new NotImplementedException();
		}
		public Tuple<int, int, int> GetAdvise(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public int GetErrorsCount(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public DateTime GetTime(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public void Undo(ulong playerId)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Settings and achivments functions //TODO need to add them.
		#endregion



		private CDBManager m_DbManager = new CDBManager();
	}

	class CLobby
	{
		public CLobby(ulong playerId)
		{
			m_ulHostId = playerId;
			m_dictPlayersTeam.Add(playerId, 0);

			GridLogic.CGridFactory.Create(GridLogic.EGridType.ClassicGrid);
			m_eDicfficulty = EGridDifficulty.Hard;
		}

		/// <summary>
		/// Add some player to lobby.
		/// </summary>
		/// <param name="playerId"></param>
		public bool AddPlayer(ulong playerId)
		{
			if (m_dictPlayersTeam.ContainsKey(playerId))
			{
				m_dictPlayersTeam.Add(playerId, 0);
				return true;
			}
			return false;
		}
		public void RemovePlayer(ulong playerId)
		{
			if (m_dictPlayersTeam.ContainsKey(playerId))
			{
				m_dictPlayersTeam.Remove(playerId);
				if (playerId == m_ulHostId)
				{
					if (m_dictPlayersTeam.Count > 0)
					{
						IEnumerator<ulong> enumerator = m_dictPlayersTeam.Keys.GetEnumerator();
						enumerator.MoveNext();
						m_ulHostId = enumerator.Current;
					}
					else
					{
						//TODO close lobby.
					}
				}
			}
		}
		public void SetPlayerTeam(ulong playerId, int playerTeam)
		{
			if (m_dictPlayersTeam.ContainsKey(playerId))
			{
				m_dictPlayersTeam[playerId] = playerTeam;
			}
		}

		public void PrintMessage(ulong playerId, string message)
		{
			throw new NotImplementedException();
		}
		public void TellMessage(ulong playerId, byte[] stream)
		{
			throw new NotImplementedException();
		}

		public ulong GetHostId()
		{
			return m_ulHostId;
		}

		public void NewGame(ulong playerId)
		{
			if (playerId == m_ulHostId)
			{
				m_Grid.Refresh(EGridDifficulty.Hard);
				m_dictPlayersSessions.Clear();

				Dictionary<int, CGrid> team_grids = new Dictionary<int, CGrid>();

				foreach (var id in m_dictPlayersTeam.Keys)
				{
					int team = m_dictPlayersTeam[id];

					if (!team_grids.ContainsKey(team))
					{
						team_grids.Add(team, (CGrid)m_Grid.Clone());			
					}
					m_dictPlayersSessions.Add(id, new CGameSession(team_grids[team]));
				}
			}

			//TODO CreateTimer.
		}
		public void StartGame(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public void StopGame(ulong playerId)
		{
			throw new NotImplementedException();
		}

		public void GiveUp(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public int GetNumber(ulong playerId, int rowIndex, int columnIndex)
		{
			throw new NotImplementedException();
		}
		public List<int> GetMarks(ulong playerId, int rowIndex, int columnIndex)
		{
			throw new NotImplementedException();
		}
		public Tuple<int, int> EnterNumber(ulong playerId, int rowIndex, int columnIndex, int value)
		{
			throw new NotImplementedException();
		}
		public bool EnterMark(ulong playerId, int rowIndex, int columnIndex, int value)
		{
			throw new NotImplementedException();
		}
		public void ClearCell(ulong playerId, int rowIndex, int columnIndex)
		{
			throw new NotImplementedException();
		}
		public Tuple<int, int, int> GetAdvise(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public int GetErrorsCount(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public DateTime GetTime(ulong playerId)
		{
			throw new NotImplementedException();
		}
		public void Undo(ulong playerId)
		{
			throw new NotImplementedException();
		}

		private readonly CGrid m_Grid;
		private EGridDifficulty m_eDicfficulty;

		private ulong m_ulHostId;
		//protected readonly Dictionary<int, CGrid> m_dictTeamGrids = new Dictionary<int, CGrid>();
		private readonly Dictionary<ulong, CGameSession> m_dictPlayersSessions = new Dictionary<ulong, CGameSession>();
		private readonly Dictionary<ulong, int> m_dictPlayersTeam = new Dictionary<ulong, int>();
	}
}
