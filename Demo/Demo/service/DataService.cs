using System;
using System.Threading;

using log4net;

using Demo.model;
using Demo.repository;

namespace Demo.service
{
    public class DataService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static DataService instance;

        private byte mTubeIndex;
        private ITemperRepository mTemperRepository;
        private IMfcRepository mMfcRepository;
        private IVacuumRepository mVacuumRepository;
        private IPaddleRepository mPaddleRepository;
        private IDioevRepository mDioevRepository;

        private Paddle[] prePaddles;

        Object mLock = new Object();

        private DataService()
        {
            mTubeIndex = 0;
            mTemperRepository = new TemperRepository();
            mMfcRepository = new MfcRepository();
            mVacuumRepository = new VacuumRepository();
            mPaddleRepository = new PaddleRepository();
            mDioevRepository = new DioevRepository();

            prePaddles = new Paddle[6];
            for (int i = 0; i < 6; ++i)
            {
                prePaddles[i] = new Paddle();
            }
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
                Temperature t = new Temperature();
                t.TubeIndex = i + 1;
                mTemperRepository.CreateTemperature(t);
                Gas g = new Gas();
                g.TubeIndex = i + 1;
                mMfcRepository.CreateGas(g);
                Vacuum v = new Vacuum();
                v.TubeIndex = i + 1;
                mVacuumRepository.CreateVacuum(v);
                Dioev d = new Dioev();
                d.TubeIndex = i + 1;
                mDioevRepository.CreateDioev(d);

                Paddle curPaddle = ProcessService.Instance.GetPaddle(i+1);
                if (prePaddles[i] == null)
                {
                    curPaddle.TubeIndex = i + 1;
                    mPaddleRepository.CreatePaddle(curPaddle);
                    prePaddles[i] = curPaddle;
                }
                else
                {
                    if (prePaddles[i].PaddlePosAct != curPaddle.PaddlePosAct || prePaddles[i].PaddlePosSp != curPaddle.PaddlePosSp || prePaddles[i].PaddleSpeedSp != curPaddle.PaddleSpeedSp)
                    {
                        curPaddle.TubeIndex = i + 1;
                        mPaddleRepository.CreatePaddle(curPaddle);
                        prePaddles[i] = curPaddle;
                    }
                }
            }
            Thread.Sleep(1000);
            PullData();
        }
    }
}

