using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client {
    public class Socket {
        private TcpClient socketClient { get; set; }
        private IPAddress ipAddress { get; set; }
        private Int32 Port { get; set; }
        private string mensagem { get; set; }
        private NetworkStream clientStream { get; set; }
        private BinaryWriter clientOutput { get; set; }
        private BinaryReader clientInput { get; set; }

        public void StartClient () {
            this.ipAddress = IPAddress.Parse ("127.0.0.1");
            this.Port = 5000;

            this.socketClient = new TcpClient ();
        }
        public void ConetionServer () {
            try {
                this.socketClient.Connect (this.ipAddress, this.Port);
                Console.WriteLine("Conectado ao Server");
            } catch {
                Console.WriteLine ("Falha na conexão");
            }
        }
        public void AcionarMensagens(string nome)
        {
            ReceberMensagem receber = new ReceberMensagem(this.socketClient);
            receber.Start();
            
            EnviarMensagem enviar = new EnviarMensagem(this.socketClient);
            enviar.DadosRemetente(nome);
            enviar.Start();
        }

    }
}