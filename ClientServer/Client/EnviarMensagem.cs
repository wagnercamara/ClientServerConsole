using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace Client
{
    public class EnviarMensagem : ThreadBase
    {
        private string nome{get; set;}
        public EnviarMensagem(TcpClient socketClient) : base (socketClient){}

        public void DadosRemetente(string nome)
        {
            this.nome = nome;
        }
        public override void Run()
        {
            DateTime localDate = DateTime.Now;
            this.clientStream = this.socketClient.GetStream();
            this.clientOutput = new BinaryWriter (clientStream);
            Console.Write($"@{nome}> ");
            String mensagem = Console.ReadLine();
            clientOutput.Write($"{localDate}\n @{nome} Diz: {mensagem}");
            if(mensagem == "fim")
            {
                this.socketClient.Close();
            }
            mensagem = null;
            Run();
        }
        
    }
}