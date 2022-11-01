using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSocket
{
    public delegate void RecieveCallback(string RecievedMessage,string error);
    public class Trasmission
    {
        private Socket _Socket;
        public Socket socket { get { return _Socket; } }
        private event RecieveCallback _RecieveCallback;
        public Trasmission(Socket Socket, RecieveCallback recieveCallback)
        {
            _Socket = Socket;
            _RecieveCallback = recieveCallback;
        }

        public Task RecieveAsync() 
        {
            return Task.Run(() => {
                try
                {
                    byte[] Buffer = new byte[1024];
                    _Socket.Receive(Buffer);
                    _RecieveCallback(Encoding.UTF8.GetString(Buffer),null);
                    RecieveAsync();
                }
                catch (Exception ex)
                {
                    _RecieveCallback(null,ex.Message);
                }
            });
        }
        public void Send (string Message) 
        {
            if (!string.IsNullOrEmpty(Message))
            {
                _Socket.Send(Encoding.UTF8.GetBytes(Message));
            }
            else
            {
                throw new Exception("Message can not be Null or Empty");
            }
        }
    }
}
