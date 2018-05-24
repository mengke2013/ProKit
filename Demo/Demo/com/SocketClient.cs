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
    public class SocketClient
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public delegate void OnConnectEnd();

        private delegate void OnConnectSocketEnd();
        private static SocketClient instance;


        private SocketObject mSocket1;
        private SocketObject mSocket2;
        private SocketObject mSocket3;
        private SocketObject mSocket4;
        private OnConnectEnd mEndCallBack;

        private string[] hosts = { "192.168.1.63", "192.168.1.64" };

        public static SocketClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SocketClient();
                }
                return instance;
            }
        }

        private SocketClient()
        {
            //Connect();
            mSocket1 = new SocketObject();
            mSocket1.callback = OnConnectSocket1End;
            mSocket2 = new SocketObject();
            mSocket2.callback = OnConnectSocket2End;
            mSocket3 = new SocketObject();
            mSocket3.callback = OnConnectSocket3End;
            mSocket4 = new SocketObject();
            mSocket4.callback = OnConnectSocket4End;
        }

        public Socket GetTcpSocket1(byte tubeGroup)
        {
            if (tubeGroup == 1)
            {
                return mSocket1.socket;
            }
            else if (tubeGroup == 2)
            {
                return mSocket3.socket;
            }
            else
            {
                return null;
            }
        }

        public Socket GetTcpSocket2(byte tubeIndex)
        {
            if (tubeIndex < 4 && tubeIndex > 0)
            {
                return mSocket2.socket;
            }
            else if (tubeIndex > 3 && tubeIndex < 7)
            {
                return mSocket4.socket;
            }
            else
            {
                return null;
            }
        }

        public void StartTcpService(OnConnectEnd endCallback)
        {
            mEndCallBack = endCallback;
            Connect(mSocket1, 1); 
        }

        private void OnConnectSocket1End()
        {
            Connect(mSocket2, 1);
        }

        private void OnConnectSocket2End()
        {
            //connect(mSocket3, 2);
            mEndCallBack();
        }

        private void OnConnectSocket3End()
        {
            //connect(mSocket4, 2);
        }

        private void OnConnectSocket4End()
        {
            mEndCallBack();
        }

        private void Connect(SocketObject so, byte tubeGroup)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            IPAddress ip = IPAddress.Parse(hosts[tubeGroup - 1]);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            so.socket = socket;
            so.ipe = ipe;
            so.tubeGroup = tubeGroup;
            socket.BeginConnect(ipe, new AsyncCallback(OnConnectComplete), so);
        }

        private void OnConnectComplete(IAsyncResult ar)
        {

            SocketObject socketObj = (SocketObject)ar.AsyncState;
            Socket socket = socketObj.socket;

            if (!socket.Connected)
            {
                //socket.EndConnect(ar);
                //socket.Disconnect(true);
                log.Error("connection failed, reconnect..." + socketObj.ipe.Address);
                socket.BeginConnect(socketObj.ipe, new AsyncCallback(OnConnectComplete), socketObj);
            }
            else
            {
                socketObj.cResult = ar;
                log.Info("connected " + socketObj.ipe.Address + " successfully");
                socketObj.callback();
            }

        }

        private class SocketObject
        {
            public Socket socket = null;
            public IAsyncResult cResult;
            public IPEndPoint ipe;
            public int tubeGroup;
            public OnConnectSocketEnd callback;
        }
    }
}
