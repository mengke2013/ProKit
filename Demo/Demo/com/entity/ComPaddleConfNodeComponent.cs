using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComPaddleConfNodeComponent
    {
        private OpcNode mAxisPosMax;
        private OpcNode mAxisPosDev;
        private OpcNode mAxisOriginOffset;

        public ComPaddleConfNodeComponent(byte tubeGroupIndex)
        {
            mAxisPosMax = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Security.AxisPosMax");
            mAxisPosDev = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Security.AxisPosDev");
            mAxisOriginOffset = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Security.AxisOriginOffset");
        }

        public OpcNode AxisPosMax
        {
            get
            {
                return mAxisPosMax;
            }
        }

        public OpcNode AxisPosDev
        {
            get
            {
                return mAxisPosDev;
            }
        }

        public OpcNode AxisOriginOffset
        {
            get
            {
                return mAxisOriginOffset;
            }
        }
    }
}
