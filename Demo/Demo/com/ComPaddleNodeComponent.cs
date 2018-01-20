using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComPaddleNodeComponent
    {
        private OpcNode mPosAct;
        private OpcNode mCurPosSp;
        private OpcNode mCurSpeedSp;

        public ComPaddleNodeComponent(byte tubeIndex)
        {
            string sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.LoadAxis.PosAct", tubeIndex);
            mPosAct = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.LoadAxis.CurPosSp", tubeIndex);
            mCurPosSp = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.LoadAxis.CurSpeedSp", tubeIndex);
            mCurSpeedSp = new OpcNode(sNodeId);
        }

        public OpcNode PosAct
        {
            get
            {
                return mPosAct;
            }
        }

        public OpcNode CurPosSp
        {
            get
            {
                return mCurPosSp;
            }
        }

        public OpcNode CurSpeedSp
        {
            get
            {
                return mCurSpeedSp;
            }
        }
    }
}
