using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Rocky.Core.Opc.Ua;
using Demo.com;
using log4net;
using Demo.model;
using System.Net;
using System.Net.Sockets;

namespace Demo.service
{


    class ProcessService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void OnStartProcessComplete();
        public delegate void OnUpdateProcessComplete();

        private static ProcessService instance;

        private Process[] mProcesses;

        private string[] hosts = { "192.168.1.63", "192.168.1.64" };

        Object mLock = new Object();
        SocketObject[] socketObjs;

        private ProcessService()
        {
            mProcesses = new Process[6];
            for (int i = 0; i < 6; ++i)
            {
                mProcesses[i] = new Process();
            }

            socketObjs = new SocketObject[2];
            socketObjs[0] = new SocketObject();
            socketObjs[1] = new SocketObject();
        }

        public static ProcessService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProcessService();
                }
                return instance;
            }
        }

        public void StartProcess(byte tubeIndex, OnStartProcessComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                lock (mLock)
                {
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    bool comCommand = true;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchStart.Value = comCommand;

                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchStart);
                    ComNodeService.Instance.WriteComNodes(tubeIndex, opcWriteNodes);
                    callback();
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        public ProcessStatus GetStatus(byte tubeIndex)
        {
            return (ProcessStatus)mProcesses[tubeIndex-1].Status;
        }

        public string GetProcessName(byte tubeIndex)
        {
            return  mProcesses[tubeIndex-1].ProcessName;
        }

        public string GetProcessStatus(byte tubeIndex)
        {
            return GetStatus(tubeIndex).ToString();
        }

        public int GetProcessTime(byte tubeIndex)
        {
            return mProcesses[tubeIndex-1].ProcessTime;
        }

        public int GetStepNum(byte tubeIndex)
        {
            return mProcesses[tubeIndex-1].StepNum;
        }

        public int GetStepEscapedTime(byte tubeIndex)
        {
            return mProcesses[tubeIndex-1].StepEscapedTime;
        }

        public int GetRemainingTime(byte tubeIndex)
        {
            return mProcesses[tubeIndex-1].ProcessRemainingTime;
        }

        public void StartPullInfoService()
        {
            Thread processRunThread = new Thread(() =>
            {
                //connect(1);
                connect(2);
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();

            //PullProcessInfo(1);
            //PullProcessInfo(2);
        }

        public void EndPullInfoService()
        {
            //disconnect(1);
            disconnect(2);
        }








        private void UpdateProcessInfo(int tubeGroupIndex)
        {
            byte[] command = { 10, 0, 0 };
            socketObjs[tubeGroupIndex-1].socket.BeginSend(command, 0, 3, SocketFlags.None, ar =>
            {
                SocketError errorCode;
                int nBytesSend = socketObjs[tubeGroupIndex - 1].socket.EndSend(ar, out errorCode);
                if (errorCode != SocketError.Success)
                {
                    nBytesSend = 0;
                }

                if (nBytesSend != 3)
                {
                    //return with error code
                    //MessageBox.Show("error");
                    log.Error("error");
                }
                else
                {
                    socketObjs[tubeGroupIndex - 1].sResult = ar;
                    socketObjs[tubeGroupIndex - 1].socket.BeginReceive(socketObjs[tubeGroupIndex - 1].buffer, 0, 1024, SocketFlags.None, new AsyncCallback(OnUpdateComplete), socketObjs[tubeGroupIndex - 1]);
                }

            }
            , null);
        }

        private void OnUpdateComplete(IAsyncResult ar)
        {
            SocketObject socketObj = (SocketObject)ar.AsyncState;
            Socket socket = socketObj.socket;
            SocketError errorCode;
            int nBytesRec = socket.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                nBytesRec = 0;
            }

            if (nBytesRec != 1024)
            {
                log.Error("error");
                socket.EndConnect(socketObj.cResult);
            }
            else
            {
                //parse recipe step data
                byte[] cDataBytes = new byte[1024];
                Array.Copy(socketObj.buffer, 0, cDataBytes, 0, 1024);
                DecryptProcess(socketObj.tubeGroup, cDataBytes);
                //socket.Shutdown(SocketShutdown.Both);
                //socket.EndConnect(socketObj.cResult);
                //socket.Close();
                Thread.Sleep(1000);
             
                //connect(socketObj.tubeGroup);
                UpdateProcessInfo(socketObj.tubeGroup);
            }
        }

        private void connect(int tubeGroup)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            IPAddress ip = IPAddress.Parse(hosts[tubeGroup - 1]);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            socketObjs[tubeGroup - 1].socket = socket;
            socketObjs[tubeGroup - 1].ipe = ipe;
            socketObjs[tubeGroup - 1].tubeGroup = tubeGroup;
            socket.BeginConnect(ipe, new AsyncCallback(OnConnectComplete), socketObjs[tubeGroup - 1]);
        }

        private void disconnect(int tubeGroup)
        {
            if(socketObjs[tubeGroup - 1].socket.Connected)
            {
                socketObjs[tubeGroup - 1].socket.EndConnect(socketObjs[tubeGroup - 1].cResult);
                socketObjs[tubeGroup - 1].socket.Shutdown(SocketShutdown.Both);
                socketObjs[tubeGroup - 1].socket.Close();

                socketObjs[tubeGroup - 1].socket = null;
                socketObjs[tubeGroup - 1].ipe = null;
                socketObjs[tubeGroup - 1].tubeGroup = 0;
                socketObjs[tubeGroup - 1].cResult = null;
                socketObjs[tubeGroup - 1].sResult = null;
            }
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
                UpdateProcessInfo(socketObj.tubeGroup);
            }

        }

        /*
        public void PullProcessInfo(int tubeGroup)
        {
            Thread processRunThread = new Thread(() =>
            {
                lock (mLock)
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    int port = 2000;
                    IPAddress ip = IPAddress.Parse(hosts[tubeGroup - 1]);
                    IPEndPoint ipe = new IPEndPoint(ip, port);
                    StateObject so2 = new StateObject();
                    so2.socket = socket;
                    so2.ipe = ipe;
                    so2.tubeGroup = tubeGroup;
                    socket.BeginConnect(ipe, new AsyncCallback(OnConnectComplete1), so2);
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void OnConnectComplete1(IAsyncResult ar)
        {

            StateObject so2 = (StateObject)ar.AsyncState;
            Socket socket = so2.socket;

            if (!socket.Connected)
            {
                //socket.EndConnect(ar);
                //socket.Disconnect(true);
                log.Error("connection failed, reconnect...");
                Thread.Sleep(5000);
                socket.BeginConnect(so2.ipe, new AsyncCallback(OnConnectComplete1), so2);
            }
            else
            {
                so2.cResult = ar;
                byte[] dataBytes = new byte[1024];
                socket.BeginReceive(dataBytes, 0, 1024, SocketFlags.None,
                           new AsyncCallback(OnReceiveComplete), so2);
            }

        }

        private void OnReceiveComplete(IAsyncResult ar)
        {
            SocketError errorCode;
            StateObject so2 = (StateObject)ar.AsyncState;
            Socket socket = so2.socket;
            int nBytesRec = socket.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                nBytesRec = 0;
            }

            if (nBytesRec != 328)
            {
                log.Error("error");
                socket.EndConnect(so2.cResult);
            }
            else
            {
                //parse recipe step data
                byte[] cDataBytes = new byte[1024];
                Array.Copy(so2.buffer, 0, cDataBytes, 0, 1024);
                DecryptProcess(so2.tubeGroup, cDataBytes);
                socket.BeginReceive(so2.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None,
                    new AsyncCallback(OnReceiveComplete), so2);
            }
        }
        */

        

        private void DecryptProcess(int tubeGroup, byte[] processBytes)
        {
            lock (mLock)
            {
                for (int i = 0; i < 3; ++i)
                {
                    int index = i;
                    //int index = i + 3 * (tubeGroup - 1);
                    mProcesses[index].ProcessName = Encoding.Default.GetString(processBytes, 300 * i + 0, 32).TrimEnd('\0');
                    mProcesses[index].ProcessTime = BitConverter.ToInt32(processBytes, 300 * i + 32);
                    mProcesses[index].StepNum = (sbyte)processBytes[300 * i + 36];
                    mProcesses[index].StepEscapedTime = BitConverter.ToInt32(processBytes, 300 * i + 37);
                    mProcesses[index].Status = (sbyte)processBytes[300 * i + 229];
                    mProcesses[index].ProcessRemainingTime = BitConverter.ToInt32(processBytes, 300 * i + 230);
                    log.Debug("remaining time:" + mProcesses[index].ProcessRemainingTime);
                }
            }
        }

        private class StateObject
        {
            public Socket socket = null;
            public const int BUFFER_SIZE = 1024;
            public byte[] buffer = new byte[BUFFER_SIZE];
            public IAsyncResult cResult;
            public IPEndPoint ipe;
            public int tubeGroup;
            public StringBuilder sb = new StringBuilder();
        }

        private class SocketObject
        {
            public const int BUFFER_SIZE = 1024;
            public byte[] buffer = new byte[BUFFER_SIZE];
            public Socket socket = null;
            public IAsyncResult cResult;
            public IAsyncResult sResult;
            public IPEndPoint ipe;
            public int tubeGroup;
        }
    }
}
