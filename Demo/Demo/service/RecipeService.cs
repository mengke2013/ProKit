using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Demo.model;
using Rocky.Core.Opc.Ua;
using Demo.com;
using System.Net;
using System.Net.Sockets;
using log4net;

namespace Demo.service
{
    public class RecipeService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void OnSynRecipeComplete(Recipe recipe);
        public delegate void OnSynStepComplete(RecipeStep step);
        public delegate void OnDownloadRecipeComplete(Recipe recipe);
        public delegate void OnDownloadStepComplete(RecipeStep step);
        public delegate void OnCommitStepComplete(int stepIndex);
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

        public Recipe LoadRecipe(byte tubeIndex)
        {
            if (System.IO.File.Exists(string.Format("recipe_tmp{0}.data", tubeIndex)))
            {
                mTubeIndex = tubeIndex;
                if (!ComNodeService.Instance.IsConnected())
                {
                    return null;
                }

                if (mRecipe == null)
                {
                    mRecipe = new Recipe();
                    mRecipeTmpStore = new Demo.utilities.Properties(string.Format("recipe_tmp{0}.data", tubeIndex));
                    for (int i = 0; i < mRecipe.Steps.Length - 1; ++i)
                    {
                        string strStepData = mRecipeTmpStore.get(String.Format("{0}", i + 1));
                        byte[] stepBytes = new byte[328];
                        byte[] tBytes = Encoding.Default.GetBytes(strStepData);
                        Array.Copy(tBytes, 0, stepBytes, 0, tBytes.Length);
                        mRecipe.Steps[i] = DecryptStepData(stepBytes);
                        mRecipe.Steps[i].StepIndex = i + 1;
                    }
                }

                return mRecipe;
            }
            else
            {
                return null;
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
                mSocketObj.synStepCallback(mRecipe.Steps[mSocketObj.stepIndex - 1]);
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
                    mSocketObj.synRecipeCallback(mRecipe);
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
                mSocketObj.dldStepCallback(mRecipe.Steps[mSocketObj.stepIndex - 1]);
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
                    mSocketObj.dldRecipeCallback(mRecipe);
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
