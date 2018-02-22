using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComTubeStatusInfoNodeComponent
    {
        private OpcNode mRecipeName;
        private OpcNode mCurStepIndex;
        private OpcNode mCurStepTime;

        public ComTubeStatusInfoNodeComponent(byte tubeIndex)
        {
            string sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.StepNum", tubeIndex);
            mCurStepIndex = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.StepTime", tubeIndex);
            mCurStepTime = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.ProcessName", tubeIndex);
            mRecipeName = new OpcNode(sNodeId);
        }

        public OpcNode CurStepIndex
        {
            get{ return mCurStepIndex; }
            set { mCurStepIndex = value; }
        }

        public OpcNode CurStepTime
        {
            get { return mCurStepTime; }
            set { mCurStepTime = value; }
        }

        public OpcNode RecipeName
        {
            get { return mRecipeName; }
            set { mRecipeName = value; }
        }
    }
}
