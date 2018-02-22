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
    class RecipeService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void OnSynRecipeComplete(Recipe recipe);
        public delegate void OnSynStepComplete(RecipeStep step);
        public delegate void OnDownloadRecipeComplete(Recipe recipe);
        public delegate void OnDownloadStepComplete(RecipeStep step);
        public delegate void OnCommitStepComplete(byte stepIndex);
        public delegate void OnBackupRecipeComplete();

        private static RecipeService instance;

        private Recipe mRecipe;
        private byte mTubeIndex;
        Demo.utilities.Properties mRecipeTmpStore;
        Demo.utilities.Properties mRecipeBak = new Demo.utilities.Properties("recipe_bak.data");

        Object mLock = new Object();

        private RecipeService()
        {
            //mRecipe = new Recipe();
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
                if (mTubeIndex == tubeIndex && mRecipe != null)
                {
                }
                else
                {
                    mTubeIndex = tubeIndex;
                    if (!ComNodeService.Instance.IsConnected())
                    {
                        return null;
                    }

                    if (mRecipe == null)
                    {
                        mRecipe = new Recipe();
                    }
                    mRecipeTmpStore = new Demo.utilities.Properties(string.Format("recipe_tmp{0}.data", tubeIndex));
                    for (int i = 0; i < mRecipe.Steps.Length - 1; ++i)
                    {
                        string strStepData = mRecipeBak.get(String.Format("{0}", i + 1));
                        byte[] stepBytes = Encoding.Unicode.GetBytes(strStepData);
                        mRecipe.Steps[i] = DecryptStepData(stepBytes);
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

            ReadRecipeData(rCallback, sCallback);
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
            WriteRecipeData(rCallback, sCallback);
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

        public bool CommitStep(byte tubeIndex, byte stepIndex, OnCommitStepComplete callback)
        {
            mTubeIndex = tubeIndex;
            if (!ComNodeService.Instance.IsConnected())
            {
                return false;
            }

            WriteStepeData(stepIndex, callback);
            return true;
        }

        public Recipe GetRecipeModel(byte tubeIndex)
        {
            return mRecipe;
        }

        private void SaveRecipeData(string fileName, OnBackupRecipeComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                lock (mLock)
                {
                    mRecipeTmpStore = new Demo.utilities.Properties(string.Format("recipe_tmp{0}.data", mTubeIndex));
                    mRecipeBak = new Demo.utilities.Properties(fileName);
                    for (int i = 0; i < 63; ++i)
                    {
                        string strStepData = mRecipeBak.get(String.Format("{0}", i + 1));
                        mRecipeBak.set(String.Format("{0}", i + 1), strStepData);
                        mRecipeBak.Save();
                    }
                    callback();
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void WriteStepeData(byte stepIndex, OnCommitStepComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                lock (mLock)
                {
                    RecipeStep step = mRecipe.Steps[stepIndex - 1];
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    sbyte comCommand = 22;
                    ComProcessNodeComponent.Instance.TubeNodeComponents[mTubeIndex - 1].CommandNodeComponent.ControlWord.Value = stepIndex;
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
                                socket.EndConnect(asyncResult);

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
                                    mRecipeTmpStore.set(String.Format("{0}", stepIndex), recipeBytes);
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
            so2.buffer = Encoding.Unicode.GetBytes(strRecipeData);
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
                    so2.buffer = Encoding.Unicode.GetBytes(strRecipeData);
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
                mRecipeTmpStore.set(String.Format("{0}", so2.stepIndex), Encoding.Unicode.GetString(so2.buffer));
                mRecipeTmpStore.Save();
            }

            if (so2.stepIndex < 63)
            {
                //mProgressDlg.ProgressModel.Progress = so2.stepIndex;
                //go next step 
                RecipeStep step = new RecipeStep();
                step.StepIndex = so2.stepIndex;
                so2.synStepCallback(step);

                so2.stepIndex = (byte)(so2.stepIndex + 1);
                socket.BeginReceive(so2.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None,
                    new AsyncCallback(ReadCompleteRecipeCallback), so2);
            }
            else
            {
                //finish recipe
                socket.EndConnect(so2.aResult);
                Recipe recipe = null;
                so2.synRecipeCallback(recipe);
                return;
            }
        }

        private byte[] EncryptStepData(RecipeStep step)
        {
            byte[] recipeBytes = new byte[328];
            byte[] cBytes;
            cBytes = System.Text.Encoding.ASCII.GetBytes(step.StepName);
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
            cBytes = BitConverter.GetBytes(step.TemperRegulInt);
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

            step.StepName = Encoding.ASCII.GetString(recipeBytes, 0, 32).TrimEnd('\0');
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

            step.TemperRegulInt = BitConverter.ToInt16(recipeBytes, 301);
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
    }
}
