using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server {
    public class SocketServer {
        public List<Socket> sockets = new List<Socket> ();
        private TcpListener serverSocket { get; set; }
        private ThreadClient thread;
        private NetworkStream clientStream { get; set; }
        private BinaryWriter clientOutput { get; set; }
        SocketServer server { get; set; }
        public void StartServer () {
            IPAddress ipAddress = IPAddress.Parse ("127.0.0.1");
            Int32 port = 5000;

            this.serverSocket = new TcpListener (ipAddress, port);

            serverSocket.Start ();
        }
        public void ConectionServer (SocketServer server) {
            this.server = server;
            try {
                Socket clientSocket = this.serverSocket.AcceptSocket ();
                AdicioneList(clientSocket);
                TatamentoCliente (this.server,clientSocket);
            } catch {
                Console.WriteLine ("Erro de conexão");
            }
            ConectionServer (server);

        }
        private void TatamentoCliente (SocketServer server, Socket clientSocket) {
                this.thread = new ThreadClient (clientSocket,server);
                thread.Start ();
        }
        private void AdicioneList(Socket clientSocket)
        {
            this.sockets.Add(clientSocket);
            Console.WriteLine(sockets.Count);
        }
    }

}