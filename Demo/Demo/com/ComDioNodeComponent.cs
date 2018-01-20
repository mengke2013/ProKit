using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComDioNodeComponent
    {
        private OpcNode mDigInput;
        private OpcNode mDigOutput;
        private OpcNode mEv;

        public ComDioNodeComponent(byte tubeIndex)
        {
            string sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.DigInput", tubeIndex);
            mDigInput = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.DigOutput", tubeIndex);
            mDigOutput = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.Ev", tubeIndex);
            mEv = new OpcNode(sNodeId);
        }

        public OpcNode DigInput
        {
            get
            {
                return mDigInput;
            }
        }

        public OpcNode DigOutput
        {
            get
            {
                return mDigOutput;
            }
        }

        public OpcNode Ev
        {
            get
            {
                return mEv;
            }
        }
    }
}
