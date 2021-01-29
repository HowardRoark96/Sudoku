using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace SudokuCore
{
    public static class CLogger
    {
        public static Logger Lg
        {
            get
            {
                if (m_Logger != null)
                {
                    return m_Logger;
                }
                LogManager.Configuration.Variables["LogDirrectory"] = Environment.CurrentDirectory + "log.txt";
                m_Logger = LogManager.GetCurrentClassLogger();
                return m_Logger;
            }
        }

        private static Logger m_Logger;
    }
}
