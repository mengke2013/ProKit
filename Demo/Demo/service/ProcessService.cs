﻿using System;
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
using Demo.com;

namespace Demo.service
{


    class ProcessService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void OnStartProcessComplete();
        public delegate void OnHoldProcessComplete();
        public delegate void OnIdleProcessComplete();
        public delegate void OnAbortProcessComplete();
        public delegate void OnUpdateProcessComplete();
        public delegate void OnCommitEditSetpointComplete();

        private static ProcessService instance;

        private Process[] mProcesses;
        private EditProcess mEditProcess;

        private string[] hosts = { "192.168.1.63", "192.168.1.64" };

        Object mLock = new Object();
        SocketObject[] socketObjs;
        SocketObject commitSocketObj;

        private ProcessService()
        {
            mProcesses = new Process[6];
            for (int i = 0; i < 6; ++i)
            {
                mProcesses[i] = new Process();
            }
            mEditProcess = new EditProcess();

            socketObjs = new SocketObject[2];
            socketObjs[0] = new SocketObject();
            socketObjs[1] = new SocketObject();
            commitSocketObj = new SocketObject();
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

        public void CommitChanges(byte tubeIndex, OnCommitEditSetpointComplete callback)
        {
            SendEditSetpointData(tubeIndex, callback);
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

        public void HoldProcess(byte tubeIndex, OnHoldProcessComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                lock (mLock)
                {
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    bool comCommand = true;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchHold.Value = comCommand;

                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchHold);
                    ComNodeService.Instance.WriteComNodes(tubeIndex, opcWriteNodes);
                    callback();
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        public void NextProcess(byte tubeIndex, OnHoldProcessComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                lock (mLock)
                {
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    bool comCommand = true;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchNext.Value = comCommand;

                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchNext);
                    ComNodeService.Instance.WriteComNodes(tubeIndex, opcWriteNodes);
                    callback();
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        public void IdleProcess(byte tubeIndex, OnIdleProcessComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                lock (mLock)
                {
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    bool comCommand = true;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchIdle.Value = comCommand;

                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchIdle);
                    ComNodeService.Instance.WriteComNodes(tubeIndex, opcWriteNodes);
                    callback();
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        public void AbortProcess(byte tubeIndex, OnAbortProcessComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                lock (mLock)
                {
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    bool comCommand = true;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchAbort.Value = comCommand;

                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchAbort);
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

        public string GetStepName(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].StepName;
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

        public int GetGas1Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Gas1Sp;
        }

        public int GetGas1Value(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Gas1CurMeas;
        }

        public int GetGas2Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Gas2Sp;
        }

        public int GetGas2Value(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Gas2CurMeas;
        }

        public int GetGas5Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Gas5Sp;
        }

        public int GetGas5Value(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Gas5CurMeas;
        }

        public int GetGas6Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Gas6Sp;
        }

        public int GetGas6Value(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Gas6CurMeas;
        }

        public int GetGas8Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Gas8Sp;
        }

        public int GetGas8Value(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Gas8CurMeas;
        }

        public int GetAna1Value(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Ana1CurMeas;
        }

        public int GetAna3Value(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Ana3CurMeas;
        }

        public int GetAna4Value(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Ana4CurMeas;
        }

        public int GetAna1Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Ana1Sp;
        }

        public bool GetTemperInt(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].TemperInt;
        }

        public int GetTemper1Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper1Sp;
        }

        public int GetTemper2Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper2Sp;
        }

        public int GetTemper3Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper3Sp;
        }

        public int GetTemper4Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper4Sp;
        }

        public int GetTemper5Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper5Sp;
        }

        public int GetTemper6Sp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper6Sp;
        }

        public int GetTemper1IntValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper1IntValue;
        }

        public int GetTemper2IntValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper2IntValue;
        }

        public int GetTemper3IntValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper3IntValue;
        }

        public int GetTemper4IntValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper4IntValue;
        }

        public int GetTemper5IntValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper5IntValue;
        }

        public int GetTemper6IntValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper6IntValue;
        }

        public int GetTemper1ExtValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper1ExtValue;
        }

        public int GetTemper2ExtValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper2ExtValue;
        }

        public int GetTemper3ExtValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper3ExtValue;
        }

        public int GetTemper4ExtValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper4ExtValue;
        }

        public int GetTemper5ExtValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper5ExtValue;
        }

        public int GetTemper6ExtValue(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Temper6ExtValue;
        }

        public int GetPaddleSpeedSp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].PaddleSpeedSp;
        }

        public int GetPaddlePosSp(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].PaddlePosSp;
        }

        public int GetPaddlePosAct(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].PaddlePosAct;
        }

        public int GetEv(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Ev;
        }

        public int GetDi(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Di;
        }

        public int GetDo(byte tubeIndex)
        {
            return mProcesses[tubeIndex - 1].Do;
        }

        public EditProcess GetEditProcess()
        {
            return mEditProcess; 
        }

        public void StartPullInfoService()
        {
            Thread processRunThread = new Thread(() =>
            {
                connect(1);
                //connect(2);
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();

            //PullProcessInfo(1);
            //PullProcessInfo(2);
        }

        public void EndPullInfoService()
        {
            disconnect(1);
            //disconnect(2);
        }





        private void SendEditSetpointData(byte tubeIndex, OnCommitEditSetpointComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                //lock (mLock)
                {
                    commitSocketObj.socket = SocketClient.Instance.GetTcpSocket2(tubeIndex);
                    commitSocketObj.cesCallback = callback;
                    commitSocketObj.tubeIndex = tubeIndex;
                    SendEditSetpoints(commitSocketObj);
                    
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void SendEditSetpoints(SocketObject so)
        {
            byte[] command = { 40, so.tubeIndex, 0 };
            so.socket.BeginSend(command, 0, 3, SocketFlags.None, ar =>
            {
                SocketError errorCode;
                int nBytesSend = so.socket.EndSend(ar, out errorCode);
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
                    so.sResult = ar;
                    byte[] setpointBytes = new byte[66];
                    byte[] tSetpointData = EncryptEditSetpointData(so.tubeIndex);
                    Array.Copy(tSetpointData, 0, setpointBytes, 0, tSetpointData.Length);
                    so.socket.BeginSend(setpointBytes, 0, setpointBytes.Length, SocketFlags.None, ar1 =>
                    {
                        SocketError errorCode1;
                        int nBytesSend1 = so.socket.EndSend(ar1, out errorCode1);
                        if (errorCode1 != SocketError.Success)
                        {
                            nBytesSend1 = 0;
                        }

                        if (nBytesSend1 != setpointBytes.Length)
                        {
                            //return with error code
                            //MessageBox.Show("error");
                            log.Error("error");
                        }
                        else
                        {
                            so.sResult = ar1;
                            so.socket.BeginReceive(so.endCommand, 0, so.endCommand.Length, SocketFlags.None, new AsyncCallback(OnReceiveSendEditSetpointsComplete), so);
                        }
                    }, null);

                }
            }
            , null);
        }

        private void OnReceiveSendEditSetpointsComplete(IAsyncResult ar)
        {
            SocketError errorCode;
            SocketObject so = (SocketObject)ar.AsyncState;
            int nBytesRec = so.socket.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                nBytesRec = 0;
            }

            if (nBytesRec != 1 && 100 != so.endCommand[0])
            {
                //return with error code
                //MessageBox.Show("error");
                log.Error("error:step ");
            }
            else
            {
                //parse recipe step data

            }




            {


                //finish recipe
                // mSocketObj.socket.EndConnect(mSocketObj.cResult);
                if (so.cesCallback != null)
                {
                    so.cesCallback();
                }

            }
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

        private void connect(byte tubeGroup)
        {
            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //int port = 2000;
            //IPAddress ip = IPAddress.Parse(hosts[tubeGroup - 1]);
            //IPEndPoint ipe = new IPEndPoint(ip, port);
            //socketObjs[tubeGroup - 1].socket = socket;
            socketObjs[tubeGroup - 1].socket = SocketClient.Instance.GetTcpSocket1(tubeGroup);
            //socketObjs[tubeGroup - 1].ipe = ipe;
            socketObjs[tubeGroup - 1].tubeGroup = tubeGroup;
            //socket.BeginConnect(ipe, new AsyncCallback(OnConnectComplete), socketObjs[tubeGroup - 1]);

            UpdateProcessInfo(tubeGroup);
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

        private byte[] EncryptEditSetpointData(int tubeIndex)
        {
            byte[] setpointBytes = new byte[66];
            byte[] cBytes;
            cBytes = BitConverter.GetBytes(mEditProcess.EditGas1Sp);
            Array.Copy(cBytes, 0, setpointBytes, 0, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditGas2Sp);
            Array.Copy(cBytes, 0, setpointBytes, 2, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditGas3Sp);
            Array.Copy(cBytes, 0, setpointBytes, 4, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditGas4Sp);
            Array.Copy(cBytes, 0, setpointBytes, 6, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditGas5Sp);
            Array.Copy(cBytes, 0, setpointBytes, 8, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditGas6Sp);
            Array.Copy(cBytes, 0, setpointBytes, 10, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditGas7Sp);
            Array.Copy(cBytes, 0, setpointBytes, 12, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditGas8Sp);
            Array.Copy(cBytes, 0, setpointBytes, 14, cBytes.Length);
            cBytes = BitConverter.GetBytes((short)mEditProcess.EditAna1Sp);
            Array.Copy(cBytes, 0, setpointBytes, 32, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditTemper1Sp);
            Array.Copy(cBytes, 0, setpointBytes, 16, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditTemper2Sp);
            Array.Copy(cBytes, 0, setpointBytes, 18, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditTemper3Sp);
            Array.Copy(cBytes, 0, setpointBytes, 20, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditTemper4Sp);
            Array.Copy(cBytes, 0, setpointBytes, 22, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditTemper5Sp);
            Array.Copy(cBytes, 0, setpointBytes, 24, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditTemper6Sp);
            Array.Copy(cBytes, 0, setpointBytes, 26, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditTemper7Sp);
            Array.Copy(cBytes, 0, setpointBytes, 28, cBytes.Length);
            cBytes = BitConverter.GetBytes((short)mEditProcess.EditTemper8Sp);
            Array.Copy(cBytes, 0, setpointBytes, 30, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditPaddlePosSp);
            Array.Copy(cBytes, 0, setpointBytes, 48, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditPaddleSpeedSp);
            Array.Copy(cBytes, 0, setpointBytes, 52, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditEvSp);
            Array.Copy(cBytes, 0, setpointBytes, 56, cBytes.Length);
            cBytes = BitConverter.GetBytes(mEditProcess.EditDoSp);
            Array.Copy(cBytes, 0, setpointBytes, 60, cBytes.Length);
            short iTemperIntSp = 0;
            if (mEditProcess.EditTemperIntSp)
            {
                iTemperIntSp = 1;
            }
            cBytes = BitConverter.GetBytes(iTemperIntSp);
            Array.Copy(cBytes, 0, setpointBytes, 64, cBytes.Length);
            return setpointBytes;
        }

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
                    mProcesses[index].StepName = Encoding.Default.GetString(processBytes, 300 * i + 234, 32).TrimEnd('\0');

                    mProcesses[index].Gas1Sp = BitConverter.ToInt16(processBytes, 300 * i + 41);
                    mProcesses[index].Gas1CurMeas = BitConverter.ToInt16(processBytes, 300 * i + 57);
                    mProcesses[index].Gas2Sp = BitConverter.ToInt16(processBytes, 300 * i + 43);
                    mProcesses[index].Gas2CurMeas = BitConverter.ToInt16(processBytes, 300 * i + 59);
                    mProcesses[index].Gas5Sp = BitConverter.ToInt16(processBytes, 300 * i + 49);
                    mProcesses[index].Gas5CurMeas = BitConverter.ToInt16(processBytes, 300 * i + 65);
                    mProcesses[index].Gas6Sp = BitConverter.ToInt16(processBytes, 300 * i + 51);
                    mProcesses[index].Gas6CurMeas = BitConverter.ToInt16(processBytes, 300 * i + 67);
                    mProcesses[index].Gas8Sp = BitConverter.ToInt16(processBytes, 300 * i + 55);
                    mProcesses[index].Gas8CurMeas = BitConverter.ToInt16(processBytes, 300 * i + 71);

                    mProcesses[index].Ana1Sp = BitConverter.ToInt16(processBytes, 300 * i + 81);
                    mProcesses[index].Ana1CurMeas = BitConverter.ToInt16(processBytes, 300 * i + 97);

                    mProcesses[index].Temper1Sp = BitConverter.ToInt16(processBytes, 300 * i + 121);
                    mProcesses[index].Temper2Sp = BitConverter.ToInt16(processBytes, 300 * i + 123);
                    mProcesses[index].Temper3Sp = BitConverter.ToInt16(processBytes, 300 * i + 125);
                    mProcesses[index].Temper4Sp = BitConverter.ToInt16(processBytes, 300 * i + 127);
                    mProcesses[index].Temper5Sp = BitConverter.ToInt16(processBytes, 300 * i + 129);
                    mProcesses[index].Temper6Sp = BitConverter.ToInt16(processBytes, 300 * i + 131);

                    mProcesses[index].PaddlePosSp = BitConverter.ToInt32(processBytes, 300 * i + 201);
                    mProcesses[index].PaddlePosAct = BitConverter.ToInt32(processBytes, 300 * i + 205);
                    mProcesses[index].PaddleSpeedSp = BitConverter.ToInt32(processBytes, 300 * i + 209);
                    mProcesses[index].Ev = BitConverter.ToInt32(processBytes, 300 * i + 221);
                    mProcesses[index].Di = BitConverter.ToInt32(processBytes, 300 * i + 213);
                    mProcesses[index].Do = BitConverter.ToInt32(processBytes, 300 * i + 217);
                    //mProcesses[index].TemperInt = BitConverter.ToBoolean(processBytes, 300 * i + 266);
                    short iTemperInt = BitConverter.ToInt16(processBytes, 300 * i + 266);
                    mProcesses[index].TemperInt = (iTemperInt == 1);

                    mProcesses[index].Status = (sbyte)processBytes[300 * i + 229];
                    mProcesses[index].ProcessRemainingTime = BitConverter.ToInt32(processBytes, 300 * i + 230);
                    log.Debug("remaining time:" + mProcesses[index].ProcessRemainingTime);
                }
            }
        }



        private class SocketObject
        {
            public const int BUFFER_SIZE = 1024;
            public byte[] buffer = new byte[BUFFER_SIZE];
            public byte[] endCommand = new byte[10];
            public Socket socket = null;
            public IAsyncResult cResult;
            public IAsyncResult sResult;
            public IPEndPoint ipe;
            public int tubeGroup;
            public byte tubeIndex;
            public OnCommitEditSetpointComplete cesCallback;
        }
    }
}
