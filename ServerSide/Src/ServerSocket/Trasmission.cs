using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocket
{
    public delegate void RecieveCallback(string RecieveMassage);
    public delegate void RecieveErrorCallback(string Error);
    public class Trasmission
    {
        private readonly Socket _Socket;

        private event RecieveCallback _RecieveCallback;
        public Socket socket { get { return _Socket; } }
        private event RecieveErrorCallback _RecieveErrorCallback;

        public Trasmission(Socket Socket , RecieveCallback RecieveCallback, RecieveErrorCallback recieveErrorCallback)
        {
            _Socket = Socket;
            _RecieveCallback = RecieveCallback;
            _RecieveErrorCallback = recieveErrorCallback;
        }
        
        public Task RecieveAsync()
        {
            return Task.Run(() => {
                try
                {
                    byte[] Buffer = new byte[1024];
                    _Socket.Receive(Buffer);
                    _RecieveCallback(Encoding.UTF8.GetString(Buffer));
                    RecieveAsync();
                }
                catch (Exception ex)
                {
                    _RecieveErrorCallback(ex.Message);
                }
            });
                
        }

        public void Send(string Message) 
        {
            if (!string.IsNullOrEmpty(Message))
            {
                _Socket.Send(Encoding.UTF8.GetBytes(Message)); 
            }
        }
    }
}
