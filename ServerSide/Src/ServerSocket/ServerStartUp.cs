using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocket
{
    //Init Server

    public delegate void AcceptedCallback(Socket AcceptedSocket);
    public delegate void AcceptedErrorCallback(string Error);
    public class ServerStartUp
    {
        int _Port;
        string _Ip;
        Socket _ServerSocket;
        private event AcceptedCallback _AcceptedCallback;
        private event AcceptedErrorCallback _AcceptedErrorCallback;
        public ServerStartUp(int port, string ip, AcceptedCallback acceptedCallback ,AcceptedErrorCallback acceptedErrorCallback)
        {
            _Port = port;
            _Ip = ip;
            _AcceptedCallback = acceptedCallback;
            _AcceptedErrorCallback = acceptedErrorCallback;
        }

        public void InitServer(int ListenCount = 1) 
        {
            _ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(_Ip), _Port));
            _ServerSocket.Listen(ListenCount);
        }

        public Task AcceptAsync() 
        {

            return Task.Run(() =>
            {
                try
                {
                    Socket AcceptedSocket = _ServerSocket.Accept();
                    _AcceptedCallback(AcceptedSocket);
                }
                catch (Exception ex)
                {
                    _AcceptedErrorCallback(ex.Message);
                }

            });
        }

        public void CloseClient (Socket Client)
        {
            Client.Close();
        }


        public void CloseServer()
        {
            _ServerSocket.Close();
        }
    }
}
