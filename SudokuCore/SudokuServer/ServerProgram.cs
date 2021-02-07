using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using SudokuCore.Protocol;
using SudokuCore;

namespace SudokuServer
{
    class Program
    {
        private static int Port = 8005;
        private static string Ip = "127.0.0.1";
        private static int ListenCount = 100;
        private const int PacketSize = 256;

        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(Ip), Port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(ListenCount);

                Console.WriteLine("Server started " + DateTime.Now.ToLongDateString());

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    int bytes_count = 0;
                    byte[] data = new byte[PacketSize];

                    do
                    {
                        bytes_count = handler.Receive(data);
                    }
                    while (handler.Available > 0);

                    handler.Send(ProcessRequest(data));

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static byte[] ProcessRequest(byte[] data)
		{
            short request_value = BitConverter.ToInt16(data, 0);
            var request_name = Enum.GetName(typeof(ECommands), request_value);
            if (request_name == null)
			{
                //TODO Log.
                Console.WriteLine("Error");
                return new byte[3];
			}

            //TODO Log.
            byte[] result = new byte[3];

			switch ((ECommands)request_value)
			{
                case ECommands.Ping:
					{
                        break;
					}
                case ECommands.Connect:
                    {
                        break;
                    }

                case ECommands.MakeLobby:
					{
                        break;
					}
                case ECommands.LobbyList:
					{
                        break;
					}
                case ECommands.ConnectToLobby:
					{
                        break;
					}

                case ECommands.GetLobbyInfo:
                    {
                        break;
                    }

                case ECommands.GetPlayerInfo:
                    {
                        break;
                    }

                case ECommands.GetGrid:
					{
                        SGridResponce answer = new SudokuCore.Protocol.SGridResponce(grid);
                        return ProtocolHelper.GridAnswerToByteArray(answer);
                        break;
					}
                case ECommands.GetMarks:
                    {
                        break;
                    }
                case ECommands.UpdateGrid:
                    {
                        break;
                    }

                case ECommands.GetServerTime:
					{
                        break;
					}

                case ECommands.EnterNumber:
					{
                        break;
					}
                case ECommands.EnterMark:
                    {
                        break;
                    }

                case ECommands.CheckWin:
                    {
                        break;
                    }
                case ECommands.GiveUp:
                    {
                        break;
                    }
			}

            return result;
		}

        private static CGridClassic grid = new CGridClassic();
    }
}
