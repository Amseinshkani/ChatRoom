using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSocket
{
 
    public class ClientStartUp
    {
        public delegate void ConnectCallBack(string error);

        int _serverPort;
        string _serverIP;
        Socket _ClientSocket;
        public Socket ClientSocket { get { return _ClientSocket; } }
        private event ConnectCallBack _Connect;

        public ClientStartUp(int serverPort,string serverIP, ConnectCallBack connectCallBack)
        {
            _serverPort = serverPort;
                _serverIP = serverIP;
            _Connect = connectCallBack;
        }

        public void InitClient()
        {
            _ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public Task ConnectAsync()
        {
            return Task.Run(() => {
                try
                {
                    _ClientSocket.Connect(new IPEndPoint(IPAddress.Parse(_serverIP), _serverPort));
                    _Connect(null);
                }
                catch (Exception ex)
                {
                    _Connect(ex.Message);
                }
            
            });
        }
        public void Close()
        {
            _ClientSocket.Close();
        }
    }
}
