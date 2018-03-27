using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

using log4net;

using Demo.model;
using Demo.com;

namespace Demo.service
{
    public class SettingsService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void OnSynSettingsComplete();
        public delegate void OnDownloadSettingsComplete();

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
            mTubeIndex = tubeIndex;
            if (System.IO.File.Exists(string.Format("settings_tmp{0}.data", tubeIndex)))
            {
                mSettings = new Settings();
                mSettingsTmpStore = new Demo.utilities.Properties(string.Format("settings_tmp{0}.data", tubeIndex));
                string strSettingsData = mSettingsTmpStore.get("s");
                byte[] settingsBytes = new byte[178];
                byte[] tBytes = Encoding.Default.GetBytes(strSettingsData);
                Array.Copy(tBytes, 0, settingsBytes, 0, tBytes.Length);
                DecryptSettingsData(settingsBytes);
                LoadNameSettings(mSettings, mSettingsTmpStore);
                return mSettings;
            }
            else
            {
                return new Settings();
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
                DecryptSettingsData(mSocketObj.buffer);
                mSettingsTmpStore.set("s", Encoding.Default.GetString(mSocketObj.buffer));
                mSettingsTmpStore.Save();
            }

            if (mSocketObj.synSettingsCallback != null)
            {
                mSocketObj.synSettingsCallback();
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
                DecryptSettingsData(mSocketObj.buffer);
                mSettingsTmpStore.set("s", Encoding.Default.GetString(mSocketObj.buffer));
                SaveNameSettings(mSettingsTmpStore);
                mSettingsTmpStore.Save();

            }
            if (mSocketObj.dldSettingsCallback != null)
            {
                mSocketObj.dldSettingsCallback();
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

        private void DecryptSettingsData(byte[] settingsBytes)
        {
            Settings settings = mSettings;
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
        }

        private void LoadNameSettings(Settings setting, Demo.utilities.Properties config)
        {
            setting.Gas1Name = config.get("Gas1Name");
            setting.Gas2Name = config.get("Gas2Name");
            setting.Gas5Name = config.get("Gas5Name");
            setting.Gas6Name = config.get("Gas6Name");
            setting.Gas8Name = config.get("Gas8Name");
            setting.Ana1Name = config.get("Ana1Name");
            setting.Ana3Name = config.get("Ana3Name");
            setting.Ana4Name = config.get("Ana4Name");
            setting.Ana5Name = config.get("Ana5Name");
            setting.Ana6Name = config.get("Ana6Name");
            setting.EvNames[0] = config.get("EvName1");
            setting.EvNames[1] = config.get("EvName2");
            setting.EvNames[2] = config.get("EvName3");
            setting.EvNames[3] = config.get("EvName4");
            setting.EvNames[4] = config.get("EvName5");
            setting.EvNames[5] = config.get("EvName6");
            setting.EvNames[6] = config.get("EvName7");
            setting.EvNames[7] = config.get("EvName8");
            setting.EvNames[8] = config.get("EvName9");
            setting.EvNames[9] = config.get("EvName10");
            setting.EvNames[10] = config.get("EvName11");
            setting.EvNames[11] = config.get("EvName12");
            setting.EvNames[12] = config.get("EvName13");
            setting.EvNames[13] = config.get("EvName14");
            setting.EvNames[14] = config.get("EvName15");
            setting.EvNames[15] = config.get("EvName16");
            setting.EvNames[16] = config.get("EvName17");
            setting.EvNames[17] = config.get("EvName18");
            setting.EvNames[18] = config.get("EvName19");
            setting.EvNames[19] = config.get("EvName20");
            setting.EvNames[20] = config.get("EvName21");
            setting.EvNames[21] = config.get("EvName22");
            setting.EvNames[22] = config.get("EvName23");
            setting.EvNames[23] = config.get("EvName24");
            setting.EvNames[24] = config.get("EvName25");
            setting.EvNames[25] = config.get("EvName26");
            setting.EvNames[26] = config.get("EvName27");
            setting.EvNames[27] = config.get("EvName28");
            setting.EvNames[28] = config.get("EvName29");
            setting.EvNames[29] = config.get("EvName30");
            setting.EvNames[30] = config.get("EvName31");
            setting.EvNames[31] = config.get("EvName32");
            setting.DiNames[0] = config.get("DiName1");
            setting.DiNames[1] = config.get("DiName2");
            setting.DiNames[2] = config.get("DiName3");
            setting.DiNames[3] = config.get("DiName4");
            setting.DiNames[4] = config.get("DiName5");
            setting.DiNames[5] = config.get("DiName6");
            setting.DiNames[6] = config.get("DiName7");
            setting.DiNames[7] = config.get("DiName8");
            setting.DiNames[8] = config.get("DiName9");
            setting.DiNames[9] = config.get("DiName10");
            setting.DiNames[10] = config.get("DiName11");
            setting.DiNames[11] = config.get("DiName12");
            setting.DiNames[12] = config.get("DiName13");
            setting.DiNames[13] = config.get("DiName14");
            setting.DiNames[14] = config.get("DiName15");
            setting.DiNames[15] = config.get("DiName16");
            setting.DiNames[16] = config.get("DiName17");
            setting.DiNames[17] = config.get("DiName18");
            setting.DiNames[18] = config.get("DiName19");
            setting.DiNames[19] = config.get("DiName20");
            setting.DiNames[20] = config.get("DiName21");
            setting.DiNames[21] = config.get("DiName22");
            setting.DiNames[22] = config.get("DiName23");
            setting.DiNames[23] = config.get("DiName24");
            setting.DiNames[24] = config.get("DiName25");
            setting.DiNames[25] = config.get("DiName26");
            setting.DiNames[26] = config.get("DiName27");
            setting.DiNames[27] = config.get("DiName28");
            setting.DiNames[28] = config.get("DiName29");
            setting.DiNames[29] = config.get("DiName30");
            setting.DiNames[30] = config.get("DiName31");
            setting.DiNames[31] = config.get("DiName32");
            setting.DoNames[0] = config.get("DoName1");
            setting.DoNames[1] = config.get("DoName2");
            setting.DoNames[2] = config.get("DoName3");
            setting.DoNames[3] = config.get("DoName4");
            setting.DoNames[4] = config.get("DoName5");
            setting.DoNames[5] = config.get("DoName6");
            setting.DoNames[6] = config.get("DoName7");
            setting.DoNames[7] = config.get("DoName8");
            setting.DoNames[8] = config.get("DoName9");
            setting.DoNames[9] = config.get("DoName10");
            setting.DoNames[10] = config.get("DoName11");
            setting.DoNames[11] = config.get("DoName12");
            setting.DoNames[12] = config.get("DoName13");
            setting.DoNames[13] = config.get("DoName14");
            setting.DoNames[14] = config.get("DoName15");
            setting.DoNames[15] = config.get("DoName16");
        }

        private void SaveNameSettings(Demo.utilities.Properties config)
        {
            Settings setting = mSettings;
            config.set("Gas1Name", setting.Gas1Name);
            config.set("Gas2Name", setting.Gas2Name);
            config.set("Gas5Name", setting.Gas5Name);
            config.set("Gas6Name", setting.Gas6Name);
            config.set("Gas8Name", setting.Gas8Name);
            config.set("Ana1Name", setting.Ana1Name);
            config.set("Ana3Name", setting.Ana3Name);
            config.set("Ana4Name", setting.Ana4Name);
            config.set("Ana5Name", setting.Ana5Name);
            config.set("Ana6Name", setting.Ana6Name);
            config.set("EvName1", setting.EvNames[0]);
            config.set("EvName2", setting.EvNames[1]);
            config.set("EvName3", setting.EvNames[2]);
            config.set("EvName4", setting.EvNames[3]);
            config.set("EvName5", setting.EvNames[4]);
            config.set("EvName6", setting.EvNames[5]);
            config.set("EvName7", setting.EvNames[6]);
            config.set("EvName8", setting.EvNames[7]);
            config.set("EvName9", setting.EvNames[8]);
            config.set("EvName10", setting.EvNames[9]);
            config.set("EvName11", setting.EvNames[10]);
            config.set("EvName12", setting.EvNames[11]);
            config.set("EvName13", setting.EvNames[12]);
            config.set("EvName14", setting.EvNames[13]);
            config.set("EvName15", setting.EvNames[14]);
            config.set("EvName16", setting.EvNames[15]);
            config.set("EvName17", setting.EvNames[16]);
            config.set("EvName18", setting.EvNames[17]);
            config.set("EvName19", setting.EvNames[18]);
            config.set("EvName20", setting.EvNames[19]);
            config.set("EvName21", setting.EvNames[20]);
            config.set("EvName22", setting.EvNames[21]);
            config.set("EvName23", setting.EvNames[22]);
            config.set("EvName24", setting.EvNames[23]);
            config.set("EvName25", setting.EvNames[24]);
            config.set("EvName26", setting.EvNames[25]);
            config.set("EvName27", setting.EvNames[26]);
            config.set("EvName28", setting.EvNames[27]);
            config.set("EvName29", setting.EvNames[28]);
            config.set("EvName30", setting.EvNames[29]);
            config.set("EvName31", setting.EvNames[30]);
            config.set("EvName32", setting.EvNames[31]);
            config.set("DiName1", setting.DiNames[0]);
            config.set("DiName2", setting.DiNames[1]);
            config.set("DiName3", setting.DiNames[2]);
            config.set("DiName4", setting.DiNames[3]);
            config.set("DiName5", setting.DiNames[4]);
            config.set("DiName6", setting.DiNames[5]);
            config.set("DiName7", setting.DiNames[6]);
            config.set("DiName8", setting.DiNames[7]);
            config.set("DiName9", setting.DiNames[8]);
            config.set("DiName10", setting.DiNames[9]);
            config.set("DiName11", setting.DiNames[10]);
            config.set("DiName12", setting.DiNames[11]);
            config.set("DiName13", setting.DiNames[12]);
            config.set("DiName14", setting.DiNames[13]);
            config.set("DiName15", setting.DiNames[14]);
            config.set("DiName16", setting.DiNames[15]);
            config.set("DiName17", setting.DiNames[16]);
            config.set("DiName18", setting.DiNames[17]);
            config.set("DiName19", setting.DiNames[18]);
            config.set("DiName20", setting.DiNames[19]);
            config.set("DiName21", setting.DiNames[20]);
            config.set("DiName22", setting.DiNames[21]);
            config.set("DiName23", setting.DiNames[22]);
            config.set("DiName24", setting.DiNames[23]);
            config.set("DiName25", setting.DiNames[24]);
            config.set("DiName26", setting.DiNames[25]);
            config.set("DiName27", setting.DiNames[26]);
            config.set("DiName28", setting.DiNames[27]);
            config.set("DiName29", setting.DiNames[28]);
            config.set("DiName30", setting.DiNames[29]);
            config.set("DiName31", setting.DiNames[30]);
            config.set("DiName32", setting.DiNames[31]);
            config.set("DoName1", setting.DoNames[0]);
            config.set("DoName2", setting.DoNames[1]);
            config.set("DoName3", setting.DoNames[2]);
            config.set("DoName4", setting.DoNames[3]);
            config.set("DoName5", setting.DoNames[4]);
            config.set("DoName6", setting.DoNames[5]);
            config.set("DoName7", setting.DoNames[6]);
            config.set("DoName8", setting.DoNames[7]);
            config.set("DoName9", setting.DoNames[8]);
            config.set("DoName10", setting.DoNames[9]);
            config.set("DoName11", setting.DoNames[10]);
            config.set("DoName12", setting.DoNames[11]);
            config.set("DoName13", setting.DoNames[12]);
            config.set("DoName14", setting.DoNames[13]);
            config.set("DoName15", setting.DoNames[14]);
            config.set("DoName16", setting.DoNames[15]);
            config.Save();
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
