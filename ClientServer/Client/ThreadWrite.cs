using System;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace Exercicio03Client
{
	public class ThreadWrite
	{
		private NetworkStream clientStream;
		private string message;
		private Thread thread;
		private bool isRunning;

		public ThreadWrite(NetworkStream clientStream)
		{
			this.message = null;
			this.isRunning = false;
			this.clientStream = clientStream;
			this.thread = new Thread(this.Run);
		}

		public void Start()
		{
			this.isRunning = true;
			this.thread.Start();
		}

		public void Write(string message)
		{
			this.message = message;
		}

		public void Run()
		{
			BinaryWriter clientOutput = new BinaryWriter(this.clientStream);

			while (this.isRunning)
			{
				if (this.message != null)
				{
					clientOutput.Write(this.message);//send()
					this.message = null;
				}
			}
		}
	}
}
