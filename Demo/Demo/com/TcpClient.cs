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

        public delegate void OnReadRecipeComplete(byte[] recipeBytes, byte stepIndex);
        public delegate void OnSendRecipeComplete(byte stepIndex);

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
            //Connect();
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

        public void ReadCompleteRecipe(byte[] recipeData)
        {
            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            string host = "192.168.1.64";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            mSocket.BeginConnect(ipe, asyncResult =>
            {
                mSocket.BeginReceive(recipeData, 0, recipeData.Length, SocketFlags.None,
                          new AsyncCallback(ReadCompleteRecipeCallback), mSocket);
            }, null);
        }

        private void ReadCompleteRecipeCallback(IAsyncResult ar)
        {
            SocketError errorCode;
            int nBytesRec = mSocket.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                nBytesRec = 0;
            }
            else if (nBytesRec > 0)
            {

            }
            else
            {

            }
            //callback(recipeData, stepIndex);
        }

        public void GetRecipeStep(byte stepIndex, byte[] recipeData, OnReadRecipeComplete callback)
        {
            mSocket.BeginReceive(recipeData, 0, recipeData.Length, SocketFlags.None, asyncResult1 =>
            {
                SocketError errorCode;
                int nBytesRec = mSocket.EndReceive(asyncResult1, out errorCode);
                if (errorCode != SocketError.Success)
                {
                    nBytesRec = 0;
                }
                else if (nBytesRec > 0)
                {

                }
                else
                {

                }
                callback(recipeData, stepIndex);
               
            }, null);
        }



        public void GetRecipe(byte stepIndex, byte[] recipeData, OnReadRecipeComplete callback)
        {
            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            string host = "192.168.1.64";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            mSocket.BeginConnect(ipe, asyncResult =>
            {
                //Socket s = (Socket)asyncResult.AsyncState;
                if (asyncResult.IsCompleted)
                {
                    mSocket.BeginReceive(recipeData, 0, recipeData.Length, SocketFlags.None, asyncResult1 =>
                    {
                        //   int length = mSocket.EndReceive(asyncResult1);
                        //Socket s1 = (Socket)asyncResult.AsyncState;
                        SocketError errorCode;
                        int nBytesRec = mSocket.EndReceive(asyncResult1, out errorCode);
                        if (errorCode != SocketError.Success)
                        {
                            nBytesRec = 0;
                        }
                        mSocket.EndConnect(asyncResult);
                        callback(recipeData, stepIndex);
                        //Console.WriteLine(string.Format("客户端发送消息:{0}", Encoding.UTF8.GetString(data)));
                    }, null);
                }
                else
                {
                    mSocket.EndConnect(asyncResult);
                    callback(recipeData, stepIndex);
                }
            },null);

        }
        public void GetRecipe1(int stepIndex, byte[] recipeData)
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

        public void SendRecipe(byte stepIndex, byte[] recipeData, OnSendRecipeComplete callback)
        {
            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            string host = "192.168.1.64";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            mSocket.BeginConnect(ipe, asyncResult =>
            {
                mSocket.BeginSend(recipeData, 0, recipeData.Length, SocketFlags.None, asyncResult1 =>
                {
                    int length = mSocket.EndReceive(asyncResult1);
                    mSocket.EndConnect(asyncResult);
                    callback(stepIndex);
                    //Console.WriteLine(string.Format("客户端发送消息:{0}", Encoding.UTF8.GetString(data)));
                }, null);
            }, null);

        }

        public void SendRecipe1(int stepIndex, byte[] recipeData)
        {
            string recvStr = "";

            try
            {

                //mSocket.BeginConnect(host, port, new AsyncCallback(ConnectCallback1), mSocket);

                // string sendStr = "R";
                // byte[] bytesSendStr = Encoding.ASCII.GetBytes(sendStr);
                mSocket.Send(recipeData);
                Thread.Sleep(200);
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
