using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client {
    public abstract class ThreadBase {
        private Thread thread { get; set; }
        protected String mensagem; // recabida ou enviada
        protected TcpClient socketClient { get; set; }
        protected NetworkStream clientStream { get; set; }
        protected BinaryReader clientInput { get; set; }
        protected BinaryWriter clientOutput { get; set; }

        public ThreadBase (TcpClient socketClient) {
            this.socketClient = socketClient;
            this.thread = new Thread(this.Run);
        }
        public void Start()
        {
            this.thread.Start();
        }
        public virtual void Run()
        {

        }

    }
}