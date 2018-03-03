using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComCommandNodeComponent
    {
        private OpcNode mTchLoad;
        private OpcNode mControlWord;
        private OpcNode mTchStart;
        private OpcNode mTchHold;
        private OpcNode mTchNext;
        private OpcNode mTchIdle;
        private OpcNode mTchAbort;

        public ComCommandNodeComponent(byte tubeIndex)
        {
            string sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.TchLoad", tubeIndex);
            mTchLoad = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.SynControlWord", tubeIndex);
            mControlWord = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.TchStart", tubeIndex);
            mTchStart = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.TchHold", tubeIndex);
            mTchHold = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.TchNext", tubeIndex);
            mTchNext = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.TchIdle", tubeIndex);
            mTchIdle = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.TchAbort", tubeIndex);
            mTchAbort = new OpcNode(sNodeId);
        }

        public OpcNode TchLoad
        {
            get{ return mTchLoad;}
            set { mTchLoad = value; }
        }

        public OpcNode ControlWord
        {
            get { return mControlWord; }
            set { mControlWord = value; }
        }

        public OpcNode TchStart
        {
            get { return mTchStart; }
            set { mTchStart = value; }
        }

        public OpcNode TchHold
        {
            get { return mTchHold; }
            set { mTchHold = value; }
        }

        public OpcNode TchNext
        {
            get { return mTchNext; }
            set { mTchNext = value; }
        }

        public OpcNode TchIdle
        {
            get { return mTchIdle; }
            set { mTchIdle = value; }
        }

        public OpcNode TchAbort
        {
            get { return mTchAbort; }
            set { mTchAbort = value; }
        }
    }
}
