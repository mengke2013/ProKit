using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComAnalogConfNodeComponent
    {
        private OpcNode mMaxValue;

        public ComAnalogConfNodeComponent(byte tubeGroupIndex, byte analogIndex)
        {
            string sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Analog{0}.MaxValue", analogIndex);
            mMaxValue = new OpcNode(sNodeId);
        }

        public OpcNode MaxValue
        {
            get
            {
                return mMaxValue;
            }
        }
    }
}
