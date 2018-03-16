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
    public class AlarmService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static AlarmService instance;

        private List<Alarm>[] mAlarms;
        private byte mTubeIndex;

        Object mLock = new Object();

        private AlarmService()
        {
            mTubeIndex = 0;

            mAlarms = new List<Alarm>[6];
            for (int i = 0; i < 6; ++i)
            {
                mAlarms[i] = new List<Alarm>();
            }     
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

            return mAlarms[mTubeIndex-1];
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

        public List<Alarm> GetAlarms(byte tubeIndex)
        {
            return mAlarms[tubeIndex-1];
        }


        private void CheckAlarm()
        {
            for (int i = 0; i < 6; ++i)
            {
                mAlarms[i].Clear();
                Random r = new Random();
                int v = r.Next(0, 10);
                if (v == i)
                {
                    mAlarms[i].Add(new Alarm());
                }
            }
            Thread.Sleep(1000);
            CheckAlarm();
        }
    }
}

