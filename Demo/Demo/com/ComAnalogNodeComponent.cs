using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComAnalogNodeComponent
    {
        private OpcNode mCurMeas;
        private OpcNode mCurSp;

        public ComAnalogNodeComponent(byte tubeIndex, byte analogIndex)
        {
            string sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.Analog{1}.CurMeas", tubeIndex, analogIndex);
            mCurMeas = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.Analog{1}.CurSp", tubeIndex, analogIndex);
            mCurSp = new OpcNode(sNodeId);
        }

        public OpcNode CurMeas
        {
            get
            {
                return mCurMeas;
            }
        }

        public OpcNode CurSp
        {
            get
            {
                return mCurSp;
            }
        }
    }
}
