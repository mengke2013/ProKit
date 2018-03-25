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
    public class DataService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static DataService instance;

        private byte mTubeIndex;

        Object mLock = new Object();

        private DataService()
        {
            mTubeIndex = 0;
  
        }

        public static DataService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataService();
                }
                return instance;
            }
        }

        public void StartPullDataService()
        {
            Thread processRunThread = new Thread(() =>
            {
                PullData();
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }



        private void PullData()
        {
            for (int i = 0; i < 6; ++i)
            {

            }
            Thread.Sleep(1000);
            PullData();
        }
    }
}

