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
using Demo.repository;

namespace Demo.service
{
    public class AlarmService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static AlarmService instance;

        private List<string>[] mAlarms;
        private byte mTubeIndex;
        private IAlarmRepository mAlarmRepository;
        Demo.utilities.Properties mAlarmConfigProperty;

        Object mLock = new Object();

        private AlarmService()
        {
            mAlarmRepository = new AlarmRepository();

            mTubeIndex = 0;

            mAlarms = new List<string>[6];
            for (int i = 0; i < 6; ++i)
            {
                mAlarms[i] = new List<string>();
            }

            mAlarmConfigProperty = new Demo.utilities.Properties("config/alarm.config");
        }

        public static AlarmService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AlarmService();
                }
                return instance;
            }
        }

        public List<Alarm> LoadAlarms(byte tubeIndex)
        {
            if (mTubeIndex != tubeIndex)
            {
                mTubeIndex = tubeIndex;
            }

            //return mAlarms[mTubeIndex-1];
            List<Alarm> alarms = mAlarmRepository.ListAlarms(tubeIndex);
            for (int i = 0; i < alarms.Count; ++i)
            {
                alarms[i].Description = FindDescription(alarms[i].ErrorCode);
            }
            return alarms;
        }


        public bool HasAlarm(byte tubeIndex)
        {
            return mAlarms[tubeIndex - 1].Count > 0 || mAlarmRepository.ListAlarms(tubeIndex).Count>0;
        }

        public void AcknowledgeAlarms(byte tubeIndex)
        {
            mAlarmRepository.UpdateAlarm((int)tubeIndex);
        }

        public void StartPullAlarmService()
        {
            Thread processRunThread = new Thread(() =>
            {
                CheckAlarm();
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        private void CheckAlarm()
        {
            for (int i = 0; i < 6; ++i)
            {
                mAlarms[i].Clear();
                List<string> alarmCodes = ProcessService.Instance.GetAlarms((byte)(i+1));
                Random r = new Random();
                int v = r.Next(0, 10);
                if (v == i)
                {
                    for (int j = 0; j < alarmCodes.Count; ++j)
                    {
                        mAlarms[i].Add(alarmCodes[j]);
                        Alarm alarm = mAlarmRepository.FindAlarm(i + 1, alarmCodes[j]);

                        if (alarm == null)
                        {
                            //add alarm 
                            alarm = new Alarm();
                            alarm.TubeIndex = i + 1;
                            alarm.ErrorCode = alarmCodes[j];
                            mAlarmRepository.CreateAlarm(alarm);
                        }
                    }

                }
            }
            Thread.Sleep(1000);
            CheckAlarm();
        }

        private string FindDescription(string errorCode)
        {
            return mAlarmConfigProperty.get(errorCode);
        }
    }
}

