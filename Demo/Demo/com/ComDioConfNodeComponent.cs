using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComDioConfNodeComponent
    {
        private OpcNode mDigInputType;

        public ComDioConfNodeComponent(byte tubeGroupIndex)
        {
            mDigInputType = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.DigInputType");
        }

        public OpcNode DigInputType
        {
            get
            {
                return mDigInputType;
            }
        }
    }
}
