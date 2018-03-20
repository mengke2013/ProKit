using System;
using System.Threading;

using log4net;

using Demo.model;

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

            return mTrends[(mTubeIndex-1) * 3 + (int)mPlotType-1];
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
            return mTrends[(tubeIndex-1) * 3 + (int)type-1];
        }


        private void UpdateTrendData()
        {
            for (int i = 0; i < 18; ++i)
            {
                mTrends[i].DataPoints.RemoveAt(0);
                Random r = new Random();

                if (i % 3 == 0)
                {
                    double[] values = new double[6];
                    values[0] = r.NextDouble() * 800;
                    values[1] = r.NextDouble() * 500;
                    values[2] = r.NextDouble() * 300;
                    values[3] = r.NextDouble() * 400;
                    values[4] = r.NextDouble() * 100;
                    values[5] = r.NextDouble() * 700;
                    mTrends[i].DataPoints.Add(values);
                }
                else if (i % 3 == 1)
                {
                    double[] values = new double[5];
                    values[0] = r.NextDouble() * 800;
                    values[1] = r.NextDouble() * 500;
                    values[2] = r.NextDouble() * 300;
                    values[3] = r.NextDouble() * 400;
                    values[4] = r.NextDouble() * 100;
                    mTrends[i].DataPoints.Add(values);
                }
                else if (i % 3 == 2)
                {
                    double[] values = new double[3];
                    values[0] = r.NextDouble() * 800;
                    values[1] = r.NextDouble() * 500;
                    values[2] = r.NextDouble() * 300;
                    mTrends[i].DataPoints.Add(values);
                }

            }
            Thread.Sleep(1000);
            UpdateTrendData();

        }
  
    }
}

