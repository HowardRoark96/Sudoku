using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using SudokuCore.Protocol;

namespace SudokuConsoleClient
{
    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 8005; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        static void Main(string[] args)
        {
            Console.WriteLine(System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName);

            IPHostEntry host1 = Dns.GetHostEntry(System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName);
			Console.WriteLine(host1.HostName);
			foreach (IPAddress ip in host1.AddressList)
				Console.WriteLine(ip.ToString());

			Console.WriteLine();

			IPHostEntry host2 = Dns.GetHostEntry("127.0.0.1");
			Console.WriteLine(host2.HostName);
			foreach (IPAddress ip in host2.AddressList)
				Console.WriteLine(ip.ToString());

			Console.ReadLine();
			return;

			while (true)
            { 
                try
                {
                    Console.Write("Введите номер команды:");
                    string message = Console.ReadLine();

                    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // подключаемся к удаленному хосту
                    socket.Connect(ipPoint);

                    byte[] data = Encoding.Unicode.GetBytes(message);
                    //socket.Send(data);
                    socket.Send(new byte[] { Convert.ToByte(message) });

                    // получаем ответ
                    data = new byte[256]; // буфер для ответа
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байт

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (socket.Available > 0);

                    SGridResponce answer = ProtocolHelper.ByteArrayToGridAnswer(data);

                    for (int i = 0; i< answer.Grid.GetLength(0); i++)
					{
                        for (int j = 0; j < answer.Grid.GetLength(0); j++)
                        {
                            Console.Write(answer.Grid[i,j] + "  ");
                        }
                        Console.WriteLine();
                    }

                    //Console.WriteLine("ответ сервера: " + builder.ToString());

                    // закрываем сокет
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.Read();
        }
    }
}
