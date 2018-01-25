using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComGasConfNodeComponent
    {
        private OpcNode mMaxValue;
        private OpcNode mEvA;

        public ComGasConfNodeComponent(byte tubeGroupIndex, byte gasIndex)
        {
            string sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Gas{0}.MaxValue", gasIndex);
            mMaxValue = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Gas{0}.EvA", gasIndex);
            mEvA = new OpcNode(sNodeId);
        }

        public OpcNode MaxValue
        {
            get
            {
                return mMaxValue;
            }
        }

        public OpcNode EvA
        {
            get
            {
                return mEvA;
            }
        }
    }
}
