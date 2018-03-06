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
    public class SettingsService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void OnSynSettingsComplete(Settings settings);
        public delegate void OnDownloadSettingsComplete(Settings settings);

        public delegate void OnBackupSettingsComplete();
        public delegate void OnConnectComplete();

        private static SettingsService instance;
        private SocketObject mSocketObj;

        private Settings mSettings;
        private byte mTubeIndex;
        private bool mDisconnect;
        Demo.utilities.Properties mSettingsTmpStore;
        Demo.utilities.Properties mSettingsBak;

        Object mLock = new Object();

        private SettingsService()
        {

            mSocketObj = new SocketObject();
            //mSocketObj.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public static SettingsService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SettingsService();
                }
                return instance;
            }
        }

        public Settings LoadSettings(byte tubeIndex)
        {
            if (System.IO.File.Exists(string.Format("settings_tmp{0}.data", tubeIndex)))
            {
                mTubeIndex = tubeIndex;
                if (!ComNodeService.Instance.IsConnected())
                {
                    return null;
                }

                if (mSettings == null)
                {
                    mSettings = new Settings();
                    mSettingsTmpStore = new Demo.utilities.Properties(string.Format("settings_tmp{0}.data", tubeIndex));
                    string strSettingsData = mSettingsTmpStore.get("s");
                    byte[] settingsBytes = new byte[178];
                    byte[] tBytes = Encoding.Default.GetBytes(strSettingsData);
                    Array.Copy(tBytes, 0, settingsBytes, 0, tBytes.Length);
                    mSettings = DecryptSettingsData(settingsBytes);

                }

                return mSettings;
            }
            else
            {
                return null;
            }
        }

        public bool SynSettings(byte tubeIndex, OnSynSettingsComplete callback)
        {
            //read settings from device
            mTubeIndex = tubeIndex;
            if (!ComNodeService.Instance.IsConnected())
            {
                return false;
            }

            ReciveSettingsData(tubeIndex, callback);
            return true;
        }

        public bool DownloadSettings(string fileName, byte tubeIndex, OnDownloadSettingsComplete callback)
        {
            //write settings to device
            mTubeIndex = tubeIndex;
            if (!ComNodeService.Instance.IsConnected())
            {
                return false;
            }
            mSettingsBak = new Demo.utilities.Properties(fileName);
            SendSettingsData(tubeIndex, callback);
            return true;
        }

        public bool CommitSettings(byte tubeIndex, OnDownloadSettingsComplete callback)
        {
            mTubeIndex = tubeIndex;
            if (!ComNodeService.Instance.IsConnected())
            {
                return false;
            }

            SendSettingsData(tubeIndex, callback);
            return true;
        }

        public bool BackupSettings(string fileName, byte tubeIndex, OnBackupSettingsComplete callback)
        {
            mTubeIndex = tubeIndex;
            if (!ComNodeService.Instance.IsConnected())
            {
                return false;
            }
            SaveSettingsData(fileName, callback);

            return true;
        }


        public Settings GetSettings()
        {
            return mSettings;
        }




        private void SaveSettingsData(string fileName, OnBackupSettingsComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                //lock (mLock)
                {
                    mSettingsTmpStore = new Demo.utilities.Properties(string.Format("settings_tmp{0}.data", mTubeIndex));
                    mSettingsBak = new Demo.utilities.Properties(fileName);

                    string strStepData = mSettingsTmpStore.get("s");
                    mSettingsBak.set("s", strStepData);
                    mSettingsBak.Save();

                    callback();
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void ReciveSettingsData(byte tubeIndex, OnSynSettingsComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                //lock (mLock)
                {
                    mSettingsTmpStore = new Demo.utilities.Properties(string.Format("settings_tmp{0}.data", mTubeIndex));
                    ReceiveCompleteSettings(tubeIndex, callback);
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void ReceiveCompleteSettings(byte tubeIndex, OnSynSettingsComplete callback)
        {
            mSocketObj.socket = SocketClient.Instance.GetTcpSocket2(tubeIndex);
            mSocketObj.synSettingsCallback = callback;
            mSocketObj.tubeIndex = tubeIndex;

            ReceiveSettings();
        }

        private void ReceiveSettings()
        {
            byte[] command = { 50, mSocketObj.tubeIndex, 0 };
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
                    mSocketObj.socket.BeginReceive(mSocketObj.buffer, 0, mSocketObj.buffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveSettingsComplete), mSocketObj);
                }
            }
            , null);
        }

        private void OnReceiveSettingsComplete(IAsyncResult ar)
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
                mSettings = DecryptSettingsData(mSocketObj.buffer);
                mSettingsTmpStore.set("s", Encoding.Default.GetString(mSocketObj.buffer));
                mSettingsTmpStore.Save();
            }

            if (mSocketObj.synSettingsCallback != null)
            {
                mSocketObj.synSettingsCallback(mSettings);
            }
        }

        private void SendSettingsData(byte tubeIndex, OnDownloadSettingsComplete callback)
        {
            Thread processRunThread = new Thread(() =>
            {
                //lock (mLock)
                {
                    mSettingsTmpStore = new Demo.utilities.Properties(string.Format("settings_tmp{0}.data", mTubeIndex));
                    SendCompleteSettings(tubeIndex, callback);
                }
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void SendCompleteSettings(byte tubeIndex, OnDownloadSettingsComplete callback)
        {
            mSocketObj.socket = SocketClient.Instance.GetTcpSocket2(tubeIndex);
            mSocketObj.dldSettingsCallback = callback;
            mSocketObj.tubeIndex = tubeIndex;

            SendSettings();
        }


        private void SendSettings()
        {
            byte[] command = { 60, mSocketObj.tubeIndex, 0 };
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
                    byte[] settingsBytes = null;

                    settingsBytes = EncryptSettingsData(mSettings);
                    Array.Copy(settingsBytes, 0, mSocketObj.buffer, 0, settingsBytes.Length);
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
                            mSocketObj.socket.BeginReceive(mSocketObj.endCommand, 0, mSocketObj.endCommand.Length, SocketFlags.None, new AsyncCallback(OnReceiveSendSettingsComplete), null);
                        }
                    }, null);

                }
            }
            , null);
        }

        private void OnReceiveSendSettingsComplete(IAsyncResult ar)
        {
            SocketError errorCode;
            int nBytesRec = mSocketObj.socket.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                nBytesRec = 0;
            }

            if (nBytesRec != 1 && 100 != mSocketObj.endCommand[0])
            {
                //return with error code
                //MessageBox.Show("error");
                log.Error("error:step ");
            }
            else
            {
                //parse settings step data
                byte[] settingsBytes = new byte[178];
                Array.Copy(mSocketObj.buffer, 0, settingsBytes, 0, 178);
                mSettings = DecryptSettingsData(mSocketObj.buffer);
                mSettingsTmpStore.set("s", Encoding.Default.GetString(mSocketObj.buffer));
                mSettingsTmpStore.Save();

            }
            if (mSocketObj.dldSettingsCallback != null)
            {
                mSocketObj.dldSettingsCallback(mSettings);
            }

        }

    

        private byte[] EncryptSettingsData(Settings settings)
        {
            byte[] settingsBytes = new byte[178];
            byte[] cBytes;
            cBytes = BitConverter.GetBytes(settings.Di);
            Array.Copy(cBytes, 0, settingsBytes, 0, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.Gas1MaxValue);
            Array.Copy(cBytes, 0, settingsBytes, 4, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.Gas2MaxValue);
            Array.Copy(cBytes, 0, settingsBytes, 7, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.Gas5MaxValue);
            Array.Copy(cBytes, 0, settingsBytes, 16, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.Gas6MaxValue);
            Array.Copy(cBytes, 0, settingsBytes, 19, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.Gas8MaxValue);
            Array.Copy(cBytes, 0, settingsBytes, 25, cBytes.Length);

            settingsBytes[6] = settings.Gas1Ev;
            settingsBytes[9] = settings.Gas2Ev;
            settingsBytes[18] = settings.Gas5Ev;
            settingsBytes[21] = settings.Gas6Ev;
            settingsBytes[27] = settings.Gas8Ev;

            cBytes = BitConverter.GetBytes(settings.Ana1MaxValue);
            Array.Copy(cBytes, 0, settingsBytes, 28, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.Ana3MaxValue);
            Array.Copy(cBytes, 0, settingsBytes, 32, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.Ana4MaxValue);
            Array.Copy(cBytes, 0, settingsBytes, 34, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.Ana5MaxValue);
            Array.Copy(cBytes, 0, settingsBytes, 36, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.Ana6MaxValue);
            Array.Copy(cBytes, 0, settingsBytes, 38, cBytes.Length);

            cBytes = BitConverter.GetBytes(settings.TemperExtKp1);
            Array.Copy(cBytes, 0, settingsBytes, 45, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTn1);
            Array.Copy(cBytes, 0, settingsBytes, 47, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTd1);
            Array.Copy(cBytes, 0, settingsBytes, 49, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntKp1);
            Array.Copy(cBytes, 0, settingsBytes, 51, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTn1);
            Array.Copy(cBytes, 0, settingsBytes, 53, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTd1);
            Array.Copy(cBytes, 0, settingsBytes, 55, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtKp2);
            Array.Copy(cBytes, 0, settingsBytes, 58, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTn2);
            Array.Copy(cBytes, 0, settingsBytes, 60, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTd2);
            Array.Copy(cBytes, 0, settingsBytes, 62, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntKp2);
            Array.Copy(cBytes, 0, settingsBytes, 64, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTn2);
            Array.Copy(cBytes, 0, settingsBytes, 66, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTd2);
            Array.Copy(cBytes, 0, settingsBytes, 68, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtKp3);
            Array.Copy(cBytes, 0, settingsBytes, 71, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTn3);
            Array.Copy(cBytes, 0, settingsBytes, 73, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTd3);
            Array.Copy(cBytes, 0, settingsBytes, 75, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntKp3);
            Array.Copy(cBytes, 0, settingsBytes, 77, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTn3);
            Array.Copy(cBytes, 0, settingsBytes, 79, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTd3);
            Array.Copy(cBytes, 0, settingsBytes, 81, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtKp4);
            Array.Copy(cBytes, 0, settingsBytes, 84, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTn4);
            Array.Copy(cBytes, 0, settingsBytes, 86, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTd4);
            Array.Copy(cBytes, 0, settingsBytes, 88, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntKp4);
            Array.Copy(cBytes, 0, settingsBytes, 90, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTn4);
            Array.Copy(cBytes, 0, settingsBytes, 92, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTd4);
            Array.Copy(cBytes, 0, settingsBytes, 94, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtKp5);
            Array.Copy(cBytes, 0, settingsBytes, 97, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTn5);
            Array.Copy(cBytes, 0, settingsBytes, 99, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTd5);
            Array.Copy(cBytes, 0, settingsBytes, 101, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntKp5);
            Array.Copy(cBytes, 0, settingsBytes, 103, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTn5);
            Array.Copy(cBytes, 0, settingsBytes, 105, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTd5);
            Array.Copy(cBytes, 0, settingsBytes, 107, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtKp6);
            Array.Copy(cBytes, 0, settingsBytes, 110, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTn6);
            Array.Copy(cBytes, 0, settingsBytes, 112, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperExtTd6);
            Array.Copy(cBytes, 0, settingsBytes, 114, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntKp6);
            Array.Copy(cBytes, 0, settingsBytes, 116, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTn6);
            Array.Copy(cBytes, 0, settingsBytes, 118, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.TemperIntTd6);
            Array.Copy(cBytes, 0, settingsBytes, 120, cBytes.Length);

            cBytes = BitConverter.GetBytes(settings.VacuumKp);
            Array.Copy(cBytes, 0, settingsBytes, 148, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.VacuumTn);
            Array.Copy(cBytes, 0, settingsBytes, 150, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.VacuumTd);
            Array.Copy(cBytes, 0, settingsBytes, 152, cBytes.Length);

            cBytes = BitConverter.GetBytes(settings.MaxPressure);
            Array.Copy(cBytes, 0, settingsBytes, 154, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.MinPressure);
            Array.Copy(cBytes, 0, settingsBytes, 156, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.MaxTemper);
            Array.Copy(cBytes, 0, settingsBytes, 158, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.MaxTemper5);
            Array.Copy(cBytes, 0, settingsBytes, 160, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.MinTemper5);
            Array.Copy(cBytes, 0, settingsBytes, 162, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.MaxPump);
            Array.Copy(cBytes, 0, settingsBytes, 164, cBytes.Length);

            cBytes = BitConverter.GetBytes(settings.Offset);
            Array.Copy(cBytes, 0, settingsBytes, 166, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.PositionDev);
            Array.Copy(cBytes, 0, settingsBytes, 170, cBytes.Length);
            cBytes = BitConverter.GetBytes(settings.ClosePosition);
            Array.Copy(cBytes, 0, settingsBytes, 174, cBytes.Length);
            return settingsBytes;
        }

        private Settings DecryptSettingsData(byte[] settingsBytes)
        {
            Settings settings = new Settings();
            settings.Di = BitConverter.ToInt32(settingsBytes, 0);
            settings.Gas1MaxValue = BitConverter.ToInt16(settingsBytes, 4);
            settings.Gas2MaxValue = BitConverter.ToInt16(settingsBytes, 7);
            settings.Gas5MaxValue = BitConverter.ToInt16(settingsBytes, 16);
            settings.Gas6MaxValue = BitConverter.ToInt16(settingsBytes, 19);
            settings.Gas8MaxValue = BitConverter.ToInt16(settingsBytes, 25);

            settings.Gas1Ev = settingsBytes[6];
            settings.Gas2Ev = settingsBytes[9];
            settings.Gas5Ev = settingsBytes[18];
            settings.Gas6Ev = settingsBytes[21];
            settings.Gas8Ev = settingsBytes[27];

            settings.Ana1MaxValue = BitConverter.ToInt16(settingsBytes, 28);
            settings.Ana3MaxValue = BitConverter.ToInt16(settingsBytes, 32);
            settings.Ana4MaxValue = BitConverter.ToInt16(settingsBytes, 34);
            settings.Ana5MaxValue = BitConverter.ToInt16(settingsBytes, 36);
            settings.Ana6MaxValue = BitConverter.ToInt16(settingsBytes, 38);

            settings.TemperExtKp1 = BitConverter.ToInt16(settingsBytes, 45);
            settings.TemperExtTn1 = BitConverter.ToInt16(settingsBytes, 47);
            settings.TemperExtTd1 = BitConverter.ToInt16(settingsBytes, 49);
            settings.TemperIntKp1 = BitConverter.ToInt16(settingsBytes, 51);
            settings.TemperIntTn1 = BitConverter.ToInt16(settingsBytes, 53);
            settings.TemperIntTd1 = BitConverter.ToInt16(settingsBytes, 55);
            settings.TemperExtKp2 = BitConverter.ToInt16(settingsBytes, 58);
            settings.TemperExtTn2 = BitConverter.ToInt16(settingsBytes, 60);
            settings.TemperExtTd2 = BitConverter.ToInt16(settingsBytes, 62);
            settings.TemperIntKp2 = BitConverter.ToInt16(settingsBytes, 64);
            settings.TemperIntTn2 = BitConverter.ToInt16(settingsBytes, 66);
            settings.TemperIntTd2 = BitConverter.ToInt16(settingsBytes, 68);
            settings.TemperExtKp3 = BitConverter.ToInt16(settingsBytes, 71);
            settings.TemperExtTn3 = BitConverter.ToInt16(settingsBytes, 73);
            settings.TemperExtTd3 = BitConverter.ToInt16(settingsBytes, 75);
            settings.TemperIntKp3 = BitConverter.ToInt16(settingsBytes, 77);
            settings.TemperIntTn3 = BitConverter.ToInt16(settingsBytes, 79);
            settings.TemperIntTd3 = BitConverter.ToInt16(settingsBytes, 81);
            settings.TemperExtKp4 = BitConverter.ToInt16(settingsBytes, 84);
            settings.TemperExtTn4 = BitConverter.ToInt16(settingsBytes, 86);
            settings.TemperExtTd4 = BitConverter.ToInt16(settingsBytes, 88);
            settings.TemperIntKp4 = BitConverter.ToInt16(settingsBytes, 90);
            settings.TemperIntTn4 = BitConverter.ToInt16(settingsBytes, 92);
            settings.TemperIntTd4 = BitConverter.ToInt16(settingsBytes, 94);
            settings.TemperExtKp5 = BitConverter.ToInt16(settingsBytes, 97);
            settings.TemperExtTn5 = BitConverter.ToInt16(settingsBytes, 99);
            settings.TemperExtTd5 = BitConverter.ToInt16(settingsBytes, 101);
            settings.TemperIntKp5 = BitConverter.ToInt16(settingsBytes, 103);
            settings.TemperIntTn5 = BitConverter.ToInt16(settingsBytes, 105);
            settings.TemperIntTd5 = BitConverter.ToInt16(settingsBytes, 107);
            settings.TemperExtKp6 = BitConverter.ToInt16(settingsBytes, 110);
            settings.TemperExtTn6 = BitConverter.ToInt16(settingsBytes, 112);
            settings.TemperExtTd6 = BitConverter.ToInt16(settingsBytes, 114);
            settings.TemperIntKp6 = BitConverter.ToInt16(settingsBytes, 116);
            settings.TemperIntTn6 = BitConverter.ToInt16(settingsBytes, 118);
            settings.TemperIntTd6 = BitConverter.ToInt16(settingsBytes, 120);

            settings.VacuumKp = BitConverter.ToInt16(settingsBytes, 148);
            settings.VacuumTn = BitConverter.ToInt16(settingsBytes, 150);
            settings.VacuumTd = BitConverter.ToInt16(settingsBytes, 152);

            settings.MaxPressure = BitConverter.ToInt16(settingsBytes, 154);
            settings.MinPressure = BitConverter.ToInt16(settingsBytes, 156);
            settings.MaxTemper = BitConverter.ToInt16(settingsBytes, 158);
            settings.MaxTemper5 = BitConverter.ToInt16(settingsBytes, 160);
            settings.MinTemper5 = BitConverter.ToInt16(settingsBytes, 162);
            settings.MaxPump = BitConverter.ToInt16(settingsBytes, 164);

            settings.Offset = BitConverter.ToInt32(settingsBytes, 166);
            settings.PositionDev = BitConverter.ToInt32(settingsBytes, 170);
            settings.ClosePosition = BitConverter.ToInt32(settingsBytes, 174);
            return settings;
        }

        private class SocketObject
        {
            public Socket socket = null;
            public IPEndPoint ipe;
            public const int BUFFER_SIZE = 178;
            public byte[] buffer = new byte[BUFFER_SIZE];
            public byte[] endCommand = new byte[10];
            public IAsyncResult cResult;
            public IAsyncResult sResult;
            public byte tubeIndex;
            public OnSynSettingsComplete synSettingsCallback;
            public OnDownloadSettingsComplete dldSettingsCallback;
            public OnConnectComplete connectCallback;
        }
    }
}
