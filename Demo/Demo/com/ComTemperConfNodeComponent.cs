using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{

    class ComTemperConfNodeComponent
    {
        private OpcNode mIntKpFixedValue;
        private OpcNode mIntTnFixedValue;
        private OpcNode mIntTdFixedValue;
        private OpcNode mExtKpFixedValue;
        private OpcNode mExtTnFixedValue;
        private OpcNode mExtTdFixedValue;

        public ComTemperConfNodeComponent(byte tubeGroupIndex, byte zoneIndex)
        {
            string sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Temper{0}.IntKpFixedValue", zoneIndex);
            mIntKpFixedValue = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Temper{0}.IntTnFixedValue", zoneIndex);
            mIntTnFixedValue = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Temper{0}.IntTdFixedValue", zoneIndex);
            mIntTdFixedValue = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Temper{0}.ExtKpFixedValue", zoneIndex);
            mExtKpFixedValue = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Temper{0}.ExtTnFixedValue", zoneIndex);
            mExtTnFixedValue = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Temper{0}.ExtTdFixedValue", zoneIndex);
            mExtTdFixedValue = new OpcNode(sNodeId);
        }

        public OpcNode IntKpFixedValue
        {
            get
            {
                return mIntKpFixedValue;
            }
        }

        public OpcNode IntTnFixedValue
        {
            get
            {
                return mIntTnFixedValue;
            }
        }

        public OpcNode IntTdFixedValue
        {
            get
            {
                return mIntTdFixedValue;
            }
        }

        public OpcNode ExtKpFixedValue
        {
            get
            {
                return mExtKpFixedValue;
            }
        }

        public OpcNode ExtTnFixedValue
        {
            get
            {
                return mExtTnFixedValue;
            }
        }

        public OpcNode ExtTdFixedValue
        {
            get
            {
                return mExtTdFixedValue;
            }
        }
    }
}
