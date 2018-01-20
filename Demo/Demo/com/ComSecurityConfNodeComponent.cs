using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComSecurityConfNodeComponent
    {
        private OpcNode mPressureMax;
        private OpcNode mPressureMin;
        private OpcNode mTemperMax;
        private OpcNode mTemper5Max;
        private OpcNode mTemper5Min;
        private OpcNode mPumpExMin;
        private OpcNode mAxisPosMax;
        private OpcNode mAxisPosDev;
        private OpcNode mAxisOriginOffset;

        public ComSecurityConfNodeComponent(byte tubeGroupIndex)
        {
            mPressureMax = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Security.PressureMax");
            mPressureMin = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Security.PressureMin");
            mTemperMax = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Security.TemperMax");
            mTemper5Max = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Security.Temper5Max");
            mTemper5Min = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Security.Temper5Min");
            mPumpExMin = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Security.PumpExMin");
        }

        public OpcNode PressureMax
        {
            get
            {
                return mPressureMax;
            }
        }

        public OpcNode PressureMin
        {
            get
            {
                return mPressureMin;
            }
        }

        public OpcNode TemperMax
        {
            get
            {
                return mTemperMax;
            }
        }

        public OpcNode Temper5Max
        {
            get
            {
                return mTemper5Max;
            }
        }

        public OpcNode Temper5Min
        {
            get
            {
                return mTemper5Min;
            }
        }

        public OpcNode PumpExMin
        {
            get
            {
                return mPumpExMin;
            }
        }
    }
}
