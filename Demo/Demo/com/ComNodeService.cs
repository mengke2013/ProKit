using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;
using Demo.com;

namespace Demo.com
{
    
    class ComNodeService
    {
        private OpcClient mClient1;
        private OpcClient mClient2;

        private static ComNodeService instance;

        public static ComNodeService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ComNodeService();
                }
                return instance;
            }
        }

        public void Connect()
        {
            mClient1.Connect();
            mClient2.Connect();
        }

        public void Reconnect()
        {
            mClient1.Reconnect();
            mClient2.Reconnect();
        }

        public void ReadComNodes(byte tubeIndex, List<OpcNode> opcNodes)
        {
            if (tubeIndex < 4 && mClient1 != null)
            {
                mClient1.ReadValue(opcNodes);
            }
            else if(tubeIndex > 3 && mClient2 != null)
            {
                mClient2.ReadValue(opcNodes);
            }
            
        }

        public void WriteComNodes(byte tubeIndex, List<OpcNode> opcNodes)
        {
            if (tubeIndex < 4 && mClient1 != null)
            {
                mClient1.WriteValue(opcNodes);
            }
            else if (tubeIndex > 3 && mClient2 != null)
            {
                mClient2.WriteValue(opcNodes);
            }
        }

        public void SubscriptComNodes(byte tubeIndex, List<OpcNode> opcNodes)
        {
            if (tubeIndex < 4 && mClient1 != null)
            {
                mClient1.SubscriptValue(opcNodes);
            }
            else if (tubeIndex > 3 && mClient2 != null)
            {
                mClient2.SubscriptValue(opcNodes);
            }
        }

        private ComNodeService()
        {
            try
            {
                mClient1 = new OpcClient("192.168.1.64");
                mClient2 = new OpcClient("192.168.1.64");
                //mClient1.Connect();
            }
            catch (Exception e)
            {
                
            }
        }

        public int TubeStatus(byte tubeIndex)
        {
            if (tubeIndex < 4 && mClient1 != null)
            {
                return mClient1.Status;
            }
            else if (tubeIndex > 3 && mClient2 != null)
            {
                return mClient2.Status;
            }
            return 0;
        }

        public bool IsConnected()
        {
            return (mClient1.Status == 1 && mClient2.Status == 1);
        }
    }
}
