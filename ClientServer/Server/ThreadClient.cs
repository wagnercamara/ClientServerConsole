using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server {
    public class ThreadClient {
        private Thread thread { get; set; }
        private Socket clientSocket { get; set; }
        private string mensagem { get; set; }
        private NetworkStream clientStream { get; set; }
        private BinaryReader clientInput = null;
        private SocketServer server { get; set; }
        private BinaryWriter clientOutput { get; set; }
        public ThreadClient (Socket clientSocket, SocketServer server) {
            this.clientSocket = clientSocket;
            this.server = server;
            this.thread = new Thread (this.Run);
        }
        public void Start () {
            this.thread.Start ();
        }
        public void StatusThread (string mensagem, Socket remetente) {
            for (Int32 i = 0; i < this.server.sockets.Count; i++) {
                if (this.server.sockets[i] != remetente) {
                    this.clientStream = new NetworkStream (this.server.sockets[i]);
                    this.clientOutput = new BinaryWriter (clientStream);
                    clientOutput.Write (mensagem);
                }
            }
        }
        public void Run () {
            this.clientStream = new NetworkStream (this.clientSocket);
            this.clientInput = new BinaryReader (clientStream);
            try {
                mensagem = Convert.ToString (clientInput.ReadString ());
                
                IPEndPoint remoteIPEndPoint = this.clientSocket.RemoteEndPoint as IPEndPoint;
                IPEndPoint localIPEndpoint = this.clientSocket.LocalEndPoint as IPEndPoint;

                if (remoteIPEndPoint != null) {
                    mensagem = ($"CLient Port: {remoteIPEndPoint.Port}\n {mensagem}");
                }
                if (mensagem != null) {
                    StatusThread (mensagem, this.clientSocket);
                }
                Run ();
            }
            catch
            {
                this.clientSocket.Close();
                this.server.sockets.Remove(this.clientSocket);
            }
        }

    }
}