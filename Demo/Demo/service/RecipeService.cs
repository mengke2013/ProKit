using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

using log4net;

using Rocky.Core.Opc.Ua;

using Demo.model;
using Demo.com;

namespace Demo.service
{
    public class RecipeService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void OnSynRecipeComplete();
        public delegate void OnSynStepComplete(byte stepIndex);
        public delegate void OnDownloadRecipeComplete();
        public delegate void OnDownloadStepComplete(byte stepIndex);
        public delegate void OnCommitStepComplete(byte stepIndex);
        public delegate void OnBackupRecipeComplete();
        public delegate void OnConnectComplete();

        private static RecipeService instance;
        private SocketObject mSocketObj;
        private string[] hosts = { "192.168.1.64", "192.168.1.64" };

        private Recipe mRecipe;
        private byte mTubeIndex;
        private bool mDisconnect;
        Demo.utilities.Properties mRecipeTmpStore;
        Demo.utilities.Properties mRecipeBak = new Demo.utilities.Properties("recipe_bak.data");

        Object mLock = new Object();

        private RecipeService()
        {

            mSocketObj = new SocketObject();
            //mSocketObj.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public static RecipeService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RecipeService();
                }
                return instance;
            }
        }

        public void LoadRecipe(byte tubeIndex)
        {
            mTubeIndex = tubeIndex;

            if (System.IO.File.Exists(string.Format("recipe_tmp{0}.data", tubeIndex)))
            {
                mRecipe = new Recipe();
                mRecipeTmpStore = new Demo.utilities.Properties(string.Format("recipe_tmp{0}.data", tubeIndex));
                for (int i = 0; i < mRecipe.Steps.Length - 1; ++i)
                {
                    string strStepData = mRecipeTmpStore.get(String.Format("{0}", i + 1));
                    if (strStepData != null)
                    {
                        byte[] stepBytes = new byte[328];
                        byte[] tBytes = Encoding.Default.GetBytes(strStepData);
                        Array.Copy(tBytes, 0, stepBytes, 0, tBytes.Length);
                        mRecipe.Steps[i] = DecryptStepData(stepBytes);
                    }
                    else
                    {
                        mRecipe.Steps[i] = new RecipeStep();
                    }
                    mRecipe.Steps[i].StepIndex = i + 1;
                }
            }
            else
            {
                mRecipe = new Recipe();
                for (int i = 0; i < mRecipe.Steps.Length - 1; ++i)
                {
                    mRecipe.Steps[i] = new RecipeStep();
                    mRecipe.Steps[i].StepIndex = i + 1;
                }
            }
        }

        public bool SynRecipe(byte tubeIndex, OnSynRecipeComplete rCallback, OnSynStepComplete sCallback)
        {
            //read recipe from device
            mTubeIndex = tubeIndex;
            if (!ComNodeService.Instance.IsConnected())
            {
                return false;
            }

            ReciveRecipeData(tubeIndex, 0, rCallback, sCallback);
            return true;
        }

        public bool SynStep(byte tubeIndex, byte stepIndex, OnSynRecipeComplete rCallback, OnSynStepComplete sCallback)
        {
            //read recipe from device
            mTubeIndex = tubeIndex;
            if (!ComNodeService.Instance.IsConnected())
            {
                return false;
            }

            ReciveRecipeData(tubeIndex, stepIndex, rCallback, sCallback);
            return true;
        }

        public bool DownloadRecipe(string fileName, byte tubeIndex, OnDownloadRecipeComplete rCallback, OnDownloadStepComplete sCallback)
        {
            //write recipe to device
            mTubeIndex = tubeIndex;
            if (!ComNodeService.Instance.IsConnected())
            {
                return false;
            }
            mRecipeBak = new Demo.utilities.Properties(fileName);
            SendRecipeData(tubeIndex, 0, rCallback, sCallback);
            return true;
        }

        public bool BackupRecipe(string fileName, byte tubeIndex, OnBackupRecipeComplete callback)
        {
            mTubeIndex = tubeIndex;
            if (!ComNodeService.Instance.IsConnected())
            {
                return false;
            }
            SaveRecipeData(fileName, callback);

            return true;
        }

        public bool CommitStep(byte tubeIndex, int stepIndex, OnDownloadRecipeComplete rCallback, OnDownloadStepComplete sCallback)
        {
            mTubeIndex = tubeIndex;
            if (!ComNodeService.Instance.IsConnected())
            {
                return false;
            }

            SendRecipeData(tubeIndex, (byte)stepIndex, rCallback, sCallback);
            return true;
        }

        public RecipeStep GetRecipeStep(int stepIndex)
        {
            return mRecipe == null ? null : mRecipe.Steps[stepIndex - 1];
        }




        private void SaveRecipeData(string fileName, OnBackupRecipeComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                //lock (mLock)
                {
                    mRecipeTmpStore = new Demo.utilities.Properties(string.Format("recipe_tmp{0}.data", mTubeIndex));
                    mRecipeBak = new Demo.utilities.Properties(fileName);
                    for (int i = 0; i < 63; ++i)
                    {
                        string strStepData = mRecipeTmpStore.get(String.Format("{0}", i + 1));
                        mRecipeBak.set(String.Format("{0}", i + 1), strStepData);
                        mRecipeBak.Save();
                    }
                    string strRecipeName = mRecipeTmpStore.get("rn");
                    mRecipeBak.set("rn", strRecipeName);
                    mRecipeBak.Save();
                    callback();
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void ReciveRecipeData(byte tubeIndex, byte stepIndex, OnSynRecipeComplete rCallback, OnSynStepComplete sCallback)
        {
            Thread processRunThread = new Thread(() =>
            {
                //lock (mLock)
                {
                    mRecipeTmpStore = new Demo.utilities.Properties(string.Format("recipe_tmp{0}.data", mTubeIndex));
                    ReceiveCompleteRecipe(tubeIndex, stepIndex, rCallback, sCallback);
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void ReceiveCompleteRecipe(byte tubeIndex, byte stepIndex, OnSynRecipeComplete rCallback, OnSynStepComplete sCallback)
        {
            mSocketObj.socket = SocketClient.Instance.GetTcpSocket2(tubeIndex);
            mSocketObj.synStepCallback = sCallback;
            mSocketObj.synRecipeCallback = rCallback;
            mSocketObj.tubeIndex = tubeIndex;
            if (stepIndex == 0)
            {
                mSocketObj.receiveRecipe = true;
                mSocketObj.stepIndex = (byte)1;
            }
            else if (stepIndex > 0 && stepIndex < 65)
            {
                mSocketObj.receiveRecipe = false;
                mSocketObj.stepIndex = stepIndex;
            }

            if (!mSocketObj.socket.Connected)
            {
                mSocketObj.connectCallback = new OnConnectComplete(ReceiveCompleteRecipeAferConnect);
                mSocketObj.tubeIndex = tubeIndex;
                if (tubeIndex < 4)
                {
                    connect(1);
                }
                else if (tubeIndex > 3)
                {
                    connect(2);
                }

            }
            else
            {
                ReceiveRecipeStep();
            }
        }

        private void ReceiveCompleteRecipeAferConnect()
        {
            ReceiveRecipeStep();
        }

        private void ReceiveRecipeStep()
        {
            byte[] command = { 30, mSocketObj.tubeIndex, mSocketObj.stepIndex };
            mSocketObj.socket.BeginSend(command, 0, 3, SocketFlags.None, ar =>
            {
                SocketError errorCode;
                int nBytesSend = mSocketObj.socket.EndSend(ar, out errorCode);
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
                    mSocketObj.sResult = ar;
                    //string strRecipeData = mRecipeBak.get(String.Format("{0}", so.stepIndex));
                    //so.buffer = Encoding.Default.GetBytes(strRecipeData);
                    mSocketObj.socket.BeginReceive(mSocketObj.buffer, 0, mSocketObj.buffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveRecipeStepComplete), mSocketObj);
                }
            }
            , null);
        }

        private void OnReceiveRecipeStepComplete(IAsyncResult ar)
        {
            SocketError errorCode;
            int nBytesSend = mSocketObj.socket.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                nBytesSend = 0;
            }

            if (nBytesSend != mSocketObj.buffer.Length)
            {
                //return with error code
                //MessageBox.Show("error");
                log.Error("error");
            }
            else
            {
                //byte[] recipeBytes = new byte[328];
                //Array.Copy(so.buffer, 0, recipeBytes, 0, 328);
                mRecipe.Steps[mSocketObj.stepIndex - 1] = DecryptStepData(mSocketObj.buffer);
                mRecipe.Steps[mSocketObj.stepIndex - 1].StepIndex = mSocketObj.stepIndex;
                mRecipeTmpStore.set(String.Format("{0}", mSocketObj.stepIndex), Encoding.Default.GetString(mSocketObj.buffer));
                mRecipeTmpStore.Save();
            }

            if (mSocketObj.synStepCallback != null)
            {
                mSocketObj.synStepCallback(mSocketObj.stepIndex);
            }


            if (mSocketObj.receiveRecipe && mSocketObj.stepIndex < 63)
            {
                //mProgressDlg.ProgressModel.Progress = so2.stepIndex;
                //go next step 
                mSocketObj.stepIndex = (byte)(mSocketObj.stepIndex + 1);
                ReceiveRecipeStep();
            }
            else
            {
                List<OpcNode> opcReadNodes = new List<OpcNode>();
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].StatusInfoNodeComponent.RecipeName);
                ComNodeService.Instance.ReadComNodes((byte)(mTubeIndex + 1), opcReadNodes);
                string recipename = (string)ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].StatusInfoNodeComponent.RecipeName.Value;
                mRecipeTmpStore.set("rn", recipename);
                mRecipeTmpStore.Save();

                //finish recipe
                //mSocketObj.socket.EndConnect(so.cResult);
                if (mSocketObj.synRecipeCallback != null)
                {
                    mSocketObj.synRecipeCallback();
                }
                if (mDisconnect)
                {
                    mSocketObj.socket.EndConnect(mSocketObj.cResult);
                    mSocketObj.socket.Shutdown(SocketShutdown.Both);
                    mSocketObj.socket.Close();
                }
            }
        }

        private void SendRecipeData(byte tubeIndex, byte stepIndex, OnDownloadRecipeComplete rCallback, OnDownloadStepComplete sCallback)
        {
            Thread processRunThread = new Thread(() =>
            {
                //lock (mLock)
                {
                    mRecipeTmpStore = new Demo.utilities.Properties(string.Format("recipe_tmp{0}.data", mTubeIndex));
                    SendCompleteRecipe(tubeIndex, stepIndex, rCallback, sCallback);
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void SendCompleteRecipe(byte tubeIndex, byte stepIndex, OnDownloadRecipeComplete rCallback, OnDownloadStepComplete sCallback)
        {
            mSocketObj.socket = SocketClient.Instance.GetTcpSocket2(tubeIndex);
            mSocketObj.dldStepCallback = sCallback;
            mSocketObj.dldRecipeCallback = rCallback;
            mSocketObj.tubeIndex = tubeIndex;
            if (stepIndex == 0)
            {
                mSocketObj.sendRecipe = true;
                mSocketObj.stepIndex = (byte)1;
            }
            else if (stepIndex > 0 && stepIndex < 65)
            {
                mSocketObj.sendRecipe = false;
                mSocketObj.stepIndex = stepIndex;
            }

            if (!mSocketObj.socket.Connected)
            {
                mSocketObj.connectCallback = new OnConnectComplete(SendCompleteRecipeAferConnect);
                mSocketObj.tubeIndex = tubeIndex;
                if (tubeIndex < 4)
                {
                    connect(1);
                }
                else if (tubeIndex > 3)
                {
                    connect(2);
                }

            }
            else
            {
                SendRecipeStep();
            }
        }

        private void SendCompleteRecipeAferConnect()
        {
            SendRecipeStep();
        }

        private void connect(int tubeGroup)
        {

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            IPAddress ip = IPAddress.Parse(hosts[tubeGroup - 1]);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            mSocketObj.ipe = ipe;
            mSocketObj.socket.BeginConnect(ipe, new AsyncCallback(OnSocketConnectComplete), mSocketObj);
        }

        private void disconnect(int tubeGroup)
        {
            if (mSocketObj.socket.Connected)
            {
                mDisconnect = true;
            }
        }

        private void OnSocketConnectComplete(IAsyncResult ar)
        {
            if (!mSocketObj.socket.Connected)
            {
                //socket.EndConnect(ar);
                //socket.Disconnect(true);
                log.Error("connection failed, reconnect..." + mSocketObj.ipe.Address);
                mSocketObj.socket.BeginConnect(mSocketObj.ipe, new AsyncCallback(OnSocketConnectComplete), mSocketObj);
            }
            else
            {
                mSocketObj.cResult = ar;
                log.Info("connected " + mSocketObj.ipe.Address + " successfully");
                mDisconnect = false;
                mSocketObj.connectCallback();
            }

        }

        private void SendRecipeStep()
        {
            byte[] command = { 20, mSocketObj.tubeIndex, mSocketObj.stepIndex };
            mSocketObj.socket.BeginSend(command, 0, 3, SocketFlags.None, ar =>
            {
                SocketError errorCode;
                int nBytesSend = mSocketObj.socket.EndSend(ar, out errorCode);
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
                    mSocketObj.sResult = ar;
                    byte[] recipeBytes = null;
                    if (mSocketObj.sendRecipe)
                    {
                        string strRecipeData = mRecipeBak.get(String.Format("{0}", mSocketObj.stepIndex));
                        recipeBytes = Encoding.Default.GetBytes(strRecipeData);
                    }
                    else
                    {
                        recipeBytes = EncryptStepData(mRecipe.Steps[mSocketObj.stepIndex - 1]);
                    }
                    //string strRecipeData = mRecipeBak.get(String.Format("{0}", mSocketObj.stepIndex));
                    //byte[] recipeBytes = Encoding.Default.GetBytes(strRecipeData);
                    //byte[] recipeBytes = EncryptStepData(mRecipe.Steps[mSocketObj.stepIndex - 1]);
                    Array.Copy(recipeBytes, 0, mSocketObj.buffer, 0, recipeBytes.Length);
                    mSocketObj.socket.BeginSend(mSocketObj.buffer, 0, mSocketObj.buffer.Length, SocketFlags.None, ar1 =>
                    {
                        SocketError errorCode1;
                        int nBytesSend1 = mSocketObj.socket.EndSend(ar1, out errorCode1);
                        if (errorCode1 != SocketError.Success)
                        {
                            nBytesSend1 = 0;
                        }

                        if (nBytesSend1 != mSocketObj.buffer.Length)
                        {
                            //return with error code
                            //MessageBox.Show("error");
                            log.Error("error");
                        }
                        else
                        {
                            mSocketObj.sResult = ar1;
                            mSocketObj.socket.BeginReceive(mSocketObj.endCommand, 0, mSocketObj.endCommand.Length, SocketFlags.None, new AsyncCallback(OnReceiveSendRecipeStepComplete), null);
                        }
                    }, null);

                }
            }
            , null);
        }

        private void OnReceiveSendRecipeStepComplete(IAsyncResult ar)
        {
            SocketError errorCode;
            int nBytesRec = mSocketObj.socket.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                nBytesRec = 0;
            }

            if (nBytesRec != 1 && mSocketObj.stepIndex != mSocketObj.endCommand[0])
            {
                //return with error code
                //MessageBox.Show("error");
                log.Error("error:step " + mSocketObj.stepIndex);
            }
            else
            {
                //parse recipe step data
                byte[] recipeBytes = new byte[328];
                Array.Copy(mSocketObj.buffer, 0, recipeBytes, 0, 328);
                mRecipe.Steps[mSocketObj.stepIndex - 1] = DecryptStepData(mSocketObj.buffer);
                mRecipe.Steps[mSocketObj.stepIndex - 1].StepIndex = mSocketObj.stepIndex;
                mRecipeTmpStore.set(String.Format("{0}", mSocketObj.stepIndex), Encoding.Default.GetString(mSocketObj.buffer));
                mRecipeTmpStore.Save();

            }
            if (mSocketObj.dldStepCallback != null)
            {
                mSocketObj.dldStepCallback(mSocketObj.stepIndex);
            }


            if (mSocketObj.sendRecipe && mSocketObj.stepIndex < 63)
            {
                //mProgressDlg.ProgressModel.Progress = so2.stepIndex;
                //go next step 
                mSocketObj.stepIndex = (byte)(mSocketObj.stepIndex + 1);
                SendRecipeStep();
            }
            else
            {
                if (mSocketObj.sendRecipe)
                {
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    // string recipeName = "Test AAA";
                    string recipeName = mRecipeBak.get("rn").TrimEnd('\0');
                    byte[] recipeBytes = new byte[32];
                    Array.Copy(Encoding.Default.GetBytes(recipeName), 0, recipeBytes, 0, recipeName.Length);
                    ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].StatusInfoNodeComponent.RecipeName.Value = Encoding.Default.GetString(recipeBytes);
                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].StatusInfoNodeComponent.RecipeName);
                    ComNodeService.Instance.WriteComNodes(mTubeIndex, opcWriteNodes);
                }

                //finish recipe
                // mSocketObj.socket.EndConnect(mSocketObj.cResult);
                if (mSocketObj.dldRecipeCallback != null)
                {
                    mSocketObj.dldRecipeCallback();
                }
                if (mDisconnect)
                {
                    mSocketObj.socket.EndConnect(mSocketObj.cResult);
                    mSocketObj.socket.Shutdown(SocketShutdown.Both);
                    mSocketObj.socket.Close();
                }
            }
        }

        /*
        private void WriteStepeData(int stepIndex, OnCommitStepComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                //lock (mLock)
                {
                    RecipeStep step = mRecipe.Steps[stepIndex - 1];
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    sbyte comCommand = 22;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.ControlWord.Value = (byte)stepIndex;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.TchLoad.Value = comCommand;
                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.ControlWord);
                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.TchLoad);
                    ComNodeService.Instance.WriteComNodes(mTubeIndex, opcWriteNodes);

                    byte[] recipeBytes = EncryptStepData(step);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        int port = 2000;
                        string host = "192.168.1.64";
                        IPAddress ip = IPAddress.Parse(host);
                        IPEndPoint ipe = new IPEndPoint(ip, port);
                        socket.BeginConnect(ipe, asyncResult =>
                        {
                            socket.BeginSend(recipeBytes, 0, recipeBytes.Length, SocketFlags.None, asyncResult1 =>
                            {
                                SocketError errorCode;
                                int nBytesSend = socket.EndSend(asyncResult1, out errorCode);

                                if (errorCode != SocketError.Success)
                                {
                                    nBytesSend = 0;
                                }

                                if (nBytesSend != 328)
                                {
                                    //return with error code
                                    //MessageBox.Show("error");
                                    log.Error("error:step " + stepIndex);
                                }
                                else
                                {
                                    mRecipeTmpStore.set(String.Format("{0}", stepIndex), Encoding.Default.GetString(recipeBytes));
                                    mRecipeTmpStore.Save();
                                    //finish recipe
                                    socket.EndConnect(asyncResult);
                                    callback(stepIndex);
                                    //SynRecipeStep(1);
                                    return;
                                }
                            }, null);
                        }, null);
                    }
                    catch (Exception ee)
                    {
                        socket.Disconnect(false);
                        socket.Close();
                    }
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void WriteRecipeData(OnDownloadRecipeComplete rCallback, OnDownloadStepComplete sCallback)
        {
            Thread processRunThread = new Thread(() =>
            {
                lock (mLock)
                {
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    sbyte comCommand = 22;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.ControlWord.Value = (byte)70;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.TchLoad.Value = comCommand;

                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.ControlWord);
                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.TchLoad);
                    ComNodeService.Instance.WriteComNodes(mTubeIndex, opcWriteNodes);

                    mRecipeTmpStore = new Demo.utilities.Properties(string.Format("recipe_tmp{0}.data", mTubeIndex));
                    WriteCompleteRecipe(rCallback, sCallback);
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void ReadRecipeData(OnSynRecipeComplete rCallback, OnSynStepComplete sCallback)
        {
            Thread processRunThread = new Thread(() =>
            {
                lock (mLock)
                {
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    sbyte comCommand = 21;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.ControlWord.Value = (byte)70;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.TchLoad.Value = comCommand;

                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.ControlWord);
                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.TchLoad);
                    ComNodeService.Instance.WriteComNodes(mTubeIndex, opcWriteNodes);

                    mRecipeTmpStore = new Demo.utilities.Properties(string.Format("recipe_tmp{0}.data", mTubeIndex));
                    ReadCompleteRecipe(rCallback, sCallback);
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void WriteCompleteRecipe(OnDownloadRecipeComplete rCallback, OnDownloadStepComplete sCallback)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            string host = "192.168.1.64";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);

            StateObject so2 = new StateObject();
            so2.socket = socket;
            so2.dldStepCallback = sCallback;
            so2.dldRecipeCallback = rCallback;
            so2.stepIndex = (byte)1;
            string strRecipeData = mRecipeBak.get(String.Format("{0}", so2.stepIndex));
            so2.buffer = Encoding.Default.GetBytes(strRecipeData);
            socket.BeginConnect(ipe, ar =>
            {
                so2.aResult = ar;
                socket.BeginSend(so2.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None,
                      new AsyncCallback(WriteCompleteRecipeCallback), so2);
            }, null);
        }

        private void ReadCompleteRecipe(OnSynRecipeComplete rCallback, OnSynStepComplete sCallback)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            string host = "192.168.1.64";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            StateObject so2 = new StateObject();
            so2.socket = socket;
            so2.synStepCallback = sCallback;
            so2.synRecipeCallback = rCallback;
            so2.stepIndex = (byte)1;
            socket.BeginConnect(ipe, asyncResult =>
            {
                so2.aResult = asyncResult;
                socket.BeginReceive(so2.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None,
                          new AsyncCallback(ReadCompleteRecipeCallback), so2);
            }, null);
        }

        private void WriteCompleteRecipeCallback(IAsyncResult ar)
        {
            SocketError errorCode;
            StateObject so2 = (StateObject)ar.AsyncState;
            Socket sSocket = so2.socket;
            int nBytesSend = sSocket.EndSend(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                nBytesSend = 0;
            }

            if (nBytesSend != 328)
            {
                //return with error code
                //MessageBox.Show("error");
                log.Error("error:step " + so2.stepIndex);
            }
            else
            {
                if (so2.stepIndex < 63)
                {
                    RecipeStep step = new RecipeStep();
                    step.StepIndex = so2.stepIndex;
                    so2.dldStepCallback(step);
                    Thread.Sleep(200);
                    //go next step 
                    so2.stepIndex = (byte)(so2.stepIndex + 1);
                    string strRecipeData = mRecipeBak.get(String.Format("{0}", so2.stepIndex));
                    mRecipeTmpStore.set(String.Format("{0}", so2.stepIndex), strRecipeData);
                    mRecipeTmpStore.Save();
                    so2.buffer = Encoding.Default.GetBytes(strRecipeData);
                    sSocket.BeginSend(so2.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None,
                        new AsyncCallback(WriteCompleteRecipeCallback), so2);
                }
                else
                {
                    //finish recipe
                    sSocket.EndConnect(so2.aResult);
                    Recipe recipe = null;
                    so2.dldRecipeCallback(recipe);
                    //SynRecipeStep(1);
                    return;
                }
            }
        }

        private void ReadCompleteRecipeCallback(IAsyncResult ar)
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
                //return with error code
                //MessageBox.Show("error");
                log.Error("error:step " + so2.stepIndex);
            }
            else
            {
                //parse recipe step data
                byte[] recipeBytes = new byte[328];
                Array.Copy(so2.buffer, 0, recipeBytes, 0, 328);
                mRecipe.Steps[so2.stepIndex-1] = DecryptStepData(so2.buffer);
                mRecipe.Steps[so2.stepIndex-1].StepIndex = so2.stepIndex;
                mRecipeTmpStore.set(String.Format("{0}", so2.stepIndex), Encoding.Default.GetString(so2.buffer));
                mRecipeTmpStore.Save();
            }

            if (so2.stepIndex < 63)
            {
                //mProgressDlg.ProgressModel.Progress = so2.stepIndex;
                //go next step 
                so2.synStepCallback(mRecipe.Steps[so2.stepIndex - 1]);

                so2.stepIndex = (byte)(so2.stepIndex + 1);
                socket.BeginReceive(so2.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None,
                    new AsyncCallback(ReadCompleteRecipeCallback), so2);
            }
            else
            {
                //finish recipe
                socket.EndConnect(so2.aResult);
                so2.synRecipeCallback(mRecipe);
                return;
            }
        }
        */

        private byte[] EncryptStepData(RecipeStep step)
        {
            byte[] recipeBytes = new byte[328];
            byte[] cBytes;
            cBytes = System.Text.Encoding.Default.GetBytes(step.StepName);
            Array.Copy(cBytes, 0, recipeBytes, 0, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.StepTime);
            Array.Copy(cBytes, 0, recipeBytes, 32, cBytes.Length);
            recipeBytes[36] = (byte)step.StepType;
            cBytes = BitConverter.GetBytes(step.Gas1Sp);
            Array.Copy(cBytes, 0, recipeBytes, 77, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Gas2Sp);
            Array.Copy(cBytes, 0, recipeBytes, 83, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Gas5Sp);
            Array.Copy(cBytes, 0, recipeBytes, 101, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Gas6Sp);
            Array.Copy(cBytes, 0, recipeBytes, 107, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Gas8Sp);
            Array.Copy(cBytes, 0, recipeBytes, 119, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Ana1Sp);
            Array.Copy(cBytes, 0, recipeBytes, 125, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Temper1Sp);
            Array.Copy(cBytes, 0, recipeBytes, 173, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Temper2Sp);
            Array.Copy(cBytes, 0, recipeBytes, 189, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Temper3Sp);
            Array.Copy(cBytes, 0, recipeBytes, 205, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Temper4Sp);
            Array.Copy(cBytes, 0, recipeBytes, 221, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Temper5Sp);
            Array.Copy(cBytes, 0, recipeBytes, 237, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Temper6Sp);
            Array.Copy(cBytes, 0, recipeBytes, 253, cBytes.Length);
            short iTemperInt = 0;
            if (step.TemperRegulInt)
            {
                iTemperInt = 1;
            }
            cBytes = BitConverter.GetBytes(iTemperInt);
            Array.Copy(cBytes, 0, recipeBytes, 301, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.AxisPosSp);
            Array.Copy(cBytes, 0, recipeBytes, 303, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.AxisSpeedSp);
            Array.Copy(cBytes, 0, recipeBytes, 307, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Ramp);
            Array.Copy(cBytes, 0, recipeBytes, 311, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.DigOutput);
            Array.Copy(cBytes, 0, recipeBytes, 315, cBytes.Length);
            cBytes = BitConverter.GetBytes(step.Ev);
            Array.Copy(cBytes, 0, recipeBytes, 319, cBytes.Length);
            recipeBytes[323] = (byte)step.Num;
            cBytes = BitConverter.GetBytes(step.CheckSum);
            Array.Copy(cBytes, 0, recipeBytes, 324, cBytes.Length);

            recipeBytes[38] = (byte)step.AnalogAbort;
            recipeBytes[39] = (byte)step.DigitalAbort;
            recipeBytes[40] = (byte)step.TemperAbort;
            recipeBytes[41] = (byte)step.ManualAbort;
            recipeBytes[42] = (byte)step.PowerAbort;
            recipeBytes[43] = (byte)step.AnalogDelay;
            recipeBytes[44] = (byte)step.MfcDelay;

            recipeBytes[79] = (byte)step.Gas1Abort;
            recipeBytes[85] = (byte)step.Gas2Abort;
            recipeBytes[103] = (byte)step.Gas5Abort;
            recipeBytes[109] = (byte)step.Gas6Abort;
            recipeBytes[121] = (byte)step.Gas8Abort;
            recipeBytes[127] = (byte)step.Ana1Abort;
            recipeBytes[80] = (byte)step.Gas1Hold;
            recipeBytes[86] = (byte)step.Gas2Hold;
            recipeBytes[104] = (byte)step.Gas5Hold;
            recipeBytes[110] = (byte)step.Gas6Hold;
            recipeBytes[122] = (byte)step.Gas8Hold;
            recipeBytes[128] = (byte)step.Ana1Hold;
            recipeBytes[81] = (byte)step.Gas1Alarm;
            recipeBytes[87] = (byte)step.Gas2Alarm;
            recipeBytes[105] = (byte)step.Gas5Alarm;
            recipeBytes[111] = (byte)step.Gas6Alarm;
            recipeBytes[123] = (byte)step.Gas8Alarm;
            recipeBytes[129] = (byte)step.Ana1Alarm;
            recipeBytes[82] = (byte)step.Gas1Next;
            recipeBytes[88] = (byte)step.Gas2Next;
            recipeBytes[106] = (byte)step.Gas5Next;
            recipeBytes[112] = (byte)step.Gas6Next;
            recipeBytes[124] = (byte)step.Gas8Next;
            recipeBytes[130] = (byte)step.Ana1Next;

            if (step.TemperRegulInt)
            {
                recipeBytes[181] = (byte)step.Temper1Abort;
                recipeBytes[197] = (byte)step.Temper2Abort;
                recipeBytes[213] = (byte)step.Temper3Abort;
                recipeBytes[229] = (byte)step.Temper4Abort;
                recipeBytes[245] = (byte)step.Temper5Abort;
                recipeBytes[261] = (byte)step.Temper6Abort;
                recipeBytes[182] = (byte)step.Temper1Hold;
                recipeBytes[198] = (byte)step.Temper2Hold;
                recipeBytes[214] = (byte)step.Temper3Hold;
                recipeBytes[230] = (byte)step.Temper4Hold;
                recipeBytes[246] = (byte)step.Temper5Hold;
                recipeBytes[262] = (byte)step.Temper6Hold;
                recipeBytes[183] = (byte)step.Temper1Alarm;
                recipeBytes[199] = (byte)step.Temper2Alarm;
                recipeBytes[215] = (byte)step.Temper3Alarm;
                recipeBytes[231] = (byte)step.Temper4Alarm;
                recipeBytes[247] = (byte)step.Temper5Alarm;
                recipeBytes[263] = (byte)step.Temper6Alarm;
                recipeBytes[184] = (byte)step.Temper1Next;
                recipeBytes[200] = (byte)step.Temper2Next;
                recipeBytes[216] = (byte)step.Temper3Next;
                recipeBytes[232] = (byte)step.Temper4Next;
                recipeBytes[248] = (byte)step.Temper5Next;
                recipeBytes[264] = (byte)step.Temper6Next;
            }
            else
            {
                recipeBytes[185] = (byte)step.Temper1Abort;
                recipeBytes[201] = (byte)step.Temper2Abort;
                recipeBytes[217] = (byte)step.Temper3Abort;
                recipeBytes[233] = (byte)step.Temper4Abort;
                recipeBytes[249] = (byte)step.Temper5Abort;
                recipeBytes[265] = (byte)step.Temper6Abort;
                recipeBytes[186] = (byte)step.Temper1Hold;
                recipeBytes[202] = (byte)step.Temper2Hold;
                recipeBytes[218] = (byte)step.Temper3Hold;
                recipeBytes[234] = (byte)step.Temper4Hold;
                recipeBytes[250] = (byte)step.Temper5Hold;
                recipeBytes[266] = (byte)step.Temper6Hold;
                recipeBytes[187] = (byte)step.Temper1Alarm;
                recipeBytes[203] = (byte)step.Temper2Alarm;
                recipeBytes[219] = (byte)step.Temper3Alarm;
                recipeBytes[235] = (byte)step.Temper4Alarm;
                recipeBytes[251] = (byte)step.Temper5Alarm;
                recipeBytes[267] = (byte)step.Temper6Alarm;
                recipeBytes[188] = (byte)step.Temper1Next;
                recipeBytes[204] = (byte)step.Temper2Next;
                recipeBytes[220] = (byte)step.Temper3Next;
                recipeBytes[236] = (byte)step.Temper4Next;
                recipeBytes[252] = (byte)step.Temper5Next;
                recipeBytes[268] = (byte)step.Temper6Next;
            }
            

            cBytes = step.AlrmDigIns;
            Array.Copy(cBytes, 0, recipeBytes, 45, 32);
            return recipeBytes;
        }

        private RecipeStep DecryptStepData(byte[] recipeBytes)
        {
            RecipeStep step = new RecipeStep();

            step.StepName = Encoding.Default.GetString(recipeBytes, 0, 32).TrimEnd('\0');
            step.StepType = (sbyte)recipeBytes[36];
            step.StepTime = BitConverter.ToInt32(recipeBytes, 32);

            step.Gas1Sp = BitConverter.ToInt16(recipeBytes, 77);
            step.Gas2Sp = BitConverter.ToInt16(recipeBytes, 83);
            step.Gas5Sp = BitConverter.ToInt16(recipeBytes, 101);
            step.Gas6Sp = BitConverter.ToInt16(recipeBytes, 107);
            step.Gas8Sp = BitConverter.ToInt16(recipeBytes, 119);
            step.Ana1Sp = BitConverter.ToInt16(recipeBytes, 125);
            step.Temper1Sp = BitConverter.ToInt16(recipeBytes, 173);
            step.Temper2Sp = BitConverter.ToInt16(recipeBytes, 189);
            step.Temper3Sp = BitConverter.ToInt16(recipeBytes, 205);
            step.Temper4Sp = BitConverter.ToInt16(recipeBytes, 221);
            step.Temper5Sp = BitConverter.ToInt16(recipeBytes, 237);
            step.Temper6Sp = BitConverter.ToInt16(recipeBytes, 253);
            short iTemperInt = BitConverter.ToInt16(recipeBytes, 301);
            step.TemperRegulInt = (iTemperInt == 1);
            step.AxisPosSp = BitConverter.ToInt32(recipeBytes, 303);
            step.AxisSpeedSp = BitConverter.ToInt32(recipeBytes, 307);
            step.Ramp = BitConverter.ToInt32(recipeBytes, 311);
            step.DigOutput = BitConverter.ToInt32(recipeBytes, 315);
            step.Ev = BitConverter.ToInt32(recipeBytes, 319);
            step.Num = recipeBytes[323];
            step.CheckSum = BitConverter.ToInt32(recipeBytes, 324);

            step.AnalogAbort = recipeBytes[38];
            step.DigitalAbort = recipeBytes[39];
            step.TemperAbort = recipeBytes[40];
            step.ManualAbort = recipeBytes[41];
            step.PowerAbort = recipeBytes[42];
            step.AnalogDelay = recipeBytes[43];
            step.MfcDelay = recipeBytes[44];

            step.Gas1Abort = recipeBytes[79];
            step.Gas2Abort = recipeBytes[85];
            step.Gas5Abort = recipeBytes[103];
            step.Gas6Abort = recipeBytes[109];
            step.Gas8Abort = recipeBytes[121];
            step.Ana1Abort = recipeBytes[127];
            step.Gas1Hold = recipeBytes[80];
            step.Gas2Hold = recipeBytes[86];
            step.Gas5Hold = recipeBytes[104];
            step.Gas6Hold = recipeBytes[110];
            step.Gas8Hold = recipeBytes[122];
            step.Ana1Hold = recipeBytes[128];
            step.Gas1Alarm = recipeBytes[81];
            step.Gas2Alarm = recipeBytes[87];
            step.Gas5Alarm = recipeBytes[105];
            step.Gas6Alarm = recipeBytes[111];
            step.Gas8Alarm = recipeBytes[123];
            step.Ana1Alarm = recipeBytes[129];
            step.Gas1Next = recipeBytes[82];
            step.Gas2Next = recipeBytes[88];
            step.Gas5Next = recipeBytes[106];
            step.Gas6Next = recipeBytes[112];
            step.Gas8Next = recipeBytes[124];
            step.Ana1Next = recipeBytes[130];

            if (step.TemperRegulInt)
            {
                step.Temper1Abort = recipeBytes[181];
                step.Temper2Abort = recipeBytes[197];
                step.Temper3Abort = recipeBytes[213];
                step.Temper4Abort = recipeBytes[229];
                step.Temper5Abort = recipeBytes[245];
                step.Temper6Abort = recipeBytes[261];
                step.Temper1Hold = recipeBytes[182];
                step.Temper2Hold = recipeBytes[198];
                step.Temper3Hold = recipeBytes[214];
                step.Temper4Hold = recipeBytes[230];
                step.Temper5Hold = recipeBytes[246];
                step.Temper6Hold = recipeBytes[262];
                step.Temper1Alarm = recipeBytes[183];
                step.Temper2Alarm = recipeBytes[199];
                step.Temper3Alarm = recipeBytes[215];
                step.Temper4Alarm = recipeBytes[231];
                step.Temper5Alarm = recipeBytes[247];
                step.Temper6Alarm = recipeBytes[263];
                step.Temper1Next = recipeBytes[184];
                step.Temper2Next = recipeBytes[200];
                step.Temper3Next = recipeBytes[216];
                step.Temper4Next = recipeBytes[232];
                step.Temper5Next = recipeBytes[248];
                step.Temper6Next = recipeBytes[264];
            }
            else
            {
                step.Temper1Abort = recipeBytes[185];
                step.Temper2Abort = recipeBytes[201];
                step.Temper3Abort = recipeBytes[217];
                step.Temper4Abort = recipeBytes[233];
                step.Temper5Abort = recipeBytes[249];
                step.Temper6Abort = recipeBytes[265];
                step.Temper1Hold = recipeBytes[186];
                step.Temper2Hold = recipeBytes[203];
                step.Temper3Hold = recipeBytes[218];
                step.Temper4Hold = recipeBytes[234];
                step.Temper5Hold = recipeBytes[250];
                step.Temper6Hold = recipeBytes[266];
                step.Temper1Alarm = recipeBytes[187];
                step.Temper2Alarm = recipeBytes[204];
                step.Temper3Alarm = recipeBytes[219];
                step.Temper4Alarm = recipeBytes[235];
                step.Temper5Alarm = recipeBytes[251];
                step.Temper6Alarm = recipeBytes[267];
                step.Temper1Next = recipeBytes[188];
                step.Temper2Next = recipeBytes[205];
                step.Temper3Next = recipeBytes[220];
                step.Temper4Next = recipeBytes[236];
                step.Temper5Next = recipeBytes[252];
                step.Temper6Next = recipeBytes[268];
            }

            byte[] alrmDigIns = new byte[32];
            Array.Copy(recipeBytes, 45, alrmDigIns, 0, 32);
            step.AlrmDigIns = alrmDigIns;

            return step;
        }

        private class StateObject
        {
            public Socket socket = null;
            public const int BUFFER_SIZE = 328;
            public byte[] buffer = new byte[BUFFER_SIZE];
            public byte stepIndex;
            public OnSynStepComplete synStepCallback;
            public OnSynRecipeComplete synRecipeCallback;
            public OnDownloadStepComplete dldStepCallback;
            public OnDownloadRecipeComplete dldRecipeCallback;
            public IAsyncResult aResult;
            public StringBuilder sb = new StringBuilder();
        }

        private class SocketObject
        {
            public Socket socket = null;
            public IPEndPoint ipe;
            public const int BUFFER_SIZE = 328;
            public byte[] buffer = new byte[BUFFER_SIZE];
            public byte[] endCommand = new byte[10];
            public IAsyncResult cResult;
            public IAsyncResult sResult;
            public byte tubeIndex;
            public byte stepIndex;
            public bool sendRecipe;
            public bool receiveRecipe;
            public OnSynStepComplete synStepCallback;
            public OnSynRecipeComplete synRecipeCallback;
            public OnDownloadStepComplete dldStepCallback;
            public OnDownloadRecipeComplete dldRecipeCallback;
            public OnConnectComplete connectCallback;
        }
    }
}
