using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuCore
{
	/// <summary>
	/// Database manager.
	/// That will probably be some cash of real data base.
	/// TODO need to do connection to real database (may be SQL).
	/// </summary>
	public class CDBManager
	{
		public bool registerPlayer(string userName, string password)
		{
			if (!ExistsPlayerName(userName))
			{
				m_ulMaxUserId++;
				m_dictUsers.Add(userName, m_ulMaxUserId);
				m_dictUserIds.Add(m_ulMaxUserId, password);
				return true;
			}
			return false;
		}

		public bool ExistsPlayerName(string userName)
		{
			return m_dictUsers.ContainsKey(userName);
		}
		public bool ExistsPlayerId(ulong userId)
		{
			return m_dictUserIds.ContainsKey(userId);
		}

		public ulong GetPlayerId(string userName, string password)
		{
			if (ExistsPlayerName(userName))
			{
				ulong player_id = m_dictUsers[userName];
				return m_dictUserIds[player_id] == password ? player_id : 0;
			}
			return 0;
		}

		private Dictionary<string, ulong> m_dictUsers = new Dictionary<string, ulong>();
		private Dictionary<ulong, string> m_dictUserIds = new Dictionary<ulong, string>();
		private ulong m_ulMaxUserId = 0;
	}
}
