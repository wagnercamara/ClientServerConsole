using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Exercicio03Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Informe o seu nome:");

			String nome = Console.ReadLine();

			bool isRunning = true;

			IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
			int port = 5000;

			TcpClient client = new TcpClient();//socket()

			client.Connect(ipAddress, port);//connect()

			NetworkStream clientStream = client.GetStream();

			ThreadRead threadRead = new ThreadRead(clientStream);
			ThreadWrite threadWrite = new ThreadWrite(clientStream);

			threadRead.Start();
			threadWrite.Start();

			while (isRunning)
			{
				threadWrite.Write($"{nome}: {Console.ReadLine()}");
			}
		}
	}
}
