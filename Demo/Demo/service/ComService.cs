using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Rocky.Core.Opc.Ua;
using Demo.com;

namespace Demo.service
{
    class ComService
    {
        private static ComService instance;

        //public delegate void OnStartHeartBeatComplete();

        private bool[] mHeartBeats;
        private ushort[] mHeartBeatValues;

        private ComService()
        {
            mHeartBeats = new bool[2];
            mHeartBeatValues = new ushort[2];
        }

        public static ComService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ComService();
                }
                return instance;
            }
        }

        public void StartHeartBeatService()
        {
            //StartHeatBeat(1);
            StartHeatBeat(2);
        }

        public void EndHeartBeatService()
        {
            //EndHeartBeat(1);
            EndHeartBeat(2);
        }

        private void StartHeatBeat(byte tubeGroup)
        {
            Thread processRunThread = new Thread(() =>
            {
                while (mHeartBeats[tubeGroup-1])
                {
                    List<OpcNode> opcWriteNodes = new List<OpcNode>();
                    ComProcessNodeComponent.Instance.TubeGroupHeartBeatNodes[tubeGroup - 1].Value = mHeartBeatValues[tubeGroup - 1];

                    opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeGroupHeartBeatNodes[tubeGroup - 1]);
                    ComNodeService.Instance.WriteComNodesToTubeGroup(tubeGroup, opcWriteNodes);

                    mHeartBeatValues[tubeGroup - 1]++;
                    if (mHeartBeatValues[tubeGroup - 1] > 5000)
                    {
                        mHeartBeatValues[tubeGroup - 1] = 1;
                    }
                    //callback();
                    Thread.Sleep(2000);
                }

            });
            mHeartBeats[tubeGroup - 1] = true;
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        public void EndHeartBeat(byte tubeGroup) {
            mHeartBeats[tubeGroup - 1] = false;
            mHeartBeatValues[tubeGroup - 1] = 0;
        }
    }
}
