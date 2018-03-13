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
    public class TrendService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static TrendService instance;

        private Trend[] mTrends;
        private byte mTubeIndex;
        private TrendPlotType mPlotType;

        Object mLock = new Object();

        private TrendService()
        {
            mTubeIndex = 0;
            mPlotType = TrendPlotType.UNKNOWN;
            mTrends = new Trend[18];
            for (int i = 0; i < 18; ++i)
            {
                mTrends[i] = new Trend();
            }           
        }

        public static TrendService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TrendService();
                }
                return instance;
            }
        }

        public Trend LoadTrends(byte tubeIndex, TrendPlotType type)
        {
            if (mTubeIndex != tubeIndex || mPlotType != type)
            {
                mTubeIndex = tubeIndex;
                mPlotType = type;
            }

            return mTrends[(mTubeIndex-1) * 3 + (int)mPlotType];
        }

        public void StartPullTrendDataService()
        {
            Thread processRunThread = new Thread(() =>
            {
                UpdateTrendData();
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        public Trend GetTrend(byte tubeIndex, TrendPlotType type)
        {
            return mTrends[(tubeIndex-1) * 3 + (int)type];
        }


        private void UpdateTrendData()
        {
            for (int i = 0; i < 18; ++i)
            {
                mTrends[i].DataPoints.RemoveAt(0);
                Random r = new Random();
                mTrends[i].DataPoints.Add(r.NextDouble()*800);
            }
            Thread.Sleep(1000);
            UpdateTrendData();

        }

  
    }
}

