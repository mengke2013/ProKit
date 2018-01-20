using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComTemperNodeComponent
    {
        private OpcNode mIntValue;
        private OpcNode mExtValue;
        private OpcNode mHeatPower;
        private OpcNode mCurSp;

        public ComTemperNodeComponent(byte tubeIndex, byte zoneIndex)
        {
            string sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.Temper{1}.CurIntTemper", tubeIndex, zoneIndex);
            mIntValue = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.Temper{1}.CurExtTemper", tubeIndex, zoneIndex);
            mExtValue = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.Temper{1}.HeatPower", tubeIndex, zoneIndex);
            mHeatPower = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.Temper{1}.CurSp", tubeIndex, zoneIndex);
            mCurSp = new OpcNode(sNodeId);
        }

        public OpcNode IntValue
        {
            get
            {
                return mIntValue;
            }
        }

        public OpcNode ExtValue
        {
            get
            {
                return mExtValue;
            }
        }

        public OpcNode HeatPower
        {
            get
            {
                return mHeatPower;
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
