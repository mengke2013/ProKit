using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using log4net;
using System.Threading;


namespace Demo.com
{
    class TcpClient
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private static TcpClient instance;

        private Socket mSocket;

        public static TcpClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TcpClient();
                }
                return instance;
            }
        }

        private TcpClient()
        {
            
        }

        public void Connect()
        {
            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            string host = "192.168.1.64";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            try
            {
                mSocket.Connect(ipe);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public void Close()
        {
            mSocket.Disconnect(false);
            mSocket.Close();
        }

        public string test()
        {
            string recvStr = "";
            try
            {

                //mSocket.BeginConnect(host, port, new AsyncCallback(ConnectCallback1), mSocket);

                string sendStr = "hello!This is a socket test";
                byte[] bytesSendStr = Encoding.ASCII.GetBytes(sendStr);
                //byte[] bytesSendStr = new byte[1024];
                mSocket.Send(bytesSendStr, bytesSendStr.Length, 0);

                byte[] recvBytes = new byte[1024];
                int bytes = 0;
                while (true)
                {
                    bytes = mSocket.Receive(recvBytes, recvBytes.Length, 0);
                    recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
                    if (bytes <= 0)
                    {
                        break;
                    }
                }
                mSocket.Shutdown(SocketShutdown.Both);
                return recvStr;
            }
            catch (SocketException ee)
            {
                //Shutdown(StatusCodes.Bad);
                //mSocket.Shutdown(SocketShutdown.Both);

                return "";
                //throw e;
            }

        }

        public void GetRecipe(int stepIndex, byte[] recipeData)
        {
            string recvStr = "";

            try
            {

                //mSocket.BeginConnect(host, port, new AsyncCallback(ConnectCallback1), mSocket);

               // string sendStr = "R";
               // byte[] bytesSendStr = Encoding.ASCII.GetBytes(sendStr);
               // mSocket.Send(bytesSendStr);
                Thread.Sleep(200);
                byte[] recvBytes = new byte[328];
                int bytes = 0;
                while (mSocket.Available > 0)
                {
                    bytes = mSocket.Receive(recipeData);
                    //recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);

                    /*
                    bytes = mSocket.Receive(recvBytes, recvBytes.Length, 0);
                    recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
                    if (bytes <= 0)
                    {
                        break;
                    }
                    */
                }
                mSocket.Shutdown(SocketShutdown.Both);
            }
            catch (SocketException ee)
            {
                //Shutdown(StatusCodes.Bad);
                //mSocket.Shutdown(SocketShutdown.Both);

                //throw e;
            }
        }
    }
}
