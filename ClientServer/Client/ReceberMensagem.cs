using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    public class ReceberMensagem : ThreadBase
    {
        public ReceberMensagem(TcpClient socketClient) : base (socketClient){}

        public override void Run()
        {
            this.clientStream = this.socketClient.GetStream();
            this.clientInput = new BinaryReader (clientStream);
            Console.WriteLine($"\n*{clientInput.ReadString ()}\n >");
            Run();
        }
        
    }
}