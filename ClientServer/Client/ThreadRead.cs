using System;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace Exercicio03Client
{
	public class ThreadRead
	{
		private NetworkStream clientStream;
		private Thread thread;
		private bool isRunning;

		public ThreadRead(NetworkStream clientStream)
		{
			this.isRunning = false;
			this.clientStream = clientStream;
			this.thread = new Thread(this.Run);
		}

		public void Start()
		{
			this.isRunning = true;
			this.thread.Start();
		}

		public void Run()
		{
			BinaryReader clientInput = new BinaryReader(this.clientStream);

			while (this.isRunning)
			{
				Console.WriteLine(clientInput.ReadString());//recv()
			}
		}
	}
}
