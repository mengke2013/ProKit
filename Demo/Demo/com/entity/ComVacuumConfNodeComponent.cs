using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComVacuumConfNodeComponent
    {

        private OpcNode mPidKp;
        private OpcNode mPidTn;
        private OpcNode mPidTd;

        private ComAnalogConfNodeComponent[] mAnalogConfNodeComponents;

        public ComVacuumConfNodeComponent(byte tubeGroupIndex)
        {
            mPidKp = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Vacuum.Kp");
            mPidTn = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Vacuum.Tn");
            mPidTd = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.OPC.Config.Vacuum.Td");

            mAnalogConfNodeComponents = new ComAnalogConfNodeComponent[8];
            for (byte analogIndex = 1; analogIndex <= 8; ++analogIndex)
            {
                mAnalogConfNodeComponents[analogIndex - 1] = new ComAnalogConfNodeComponent(tubeGroupIndex, analogIndex);
            }
        }

        public ComAnalogConfNodeComponent[] AnalogConfNodeComponents
        {
            get
            {
                return mAnalogConfNodeComponents;
            }
        }

        public OpcNode PidKp
        {
            get
            {
                return mPidKp;
            }
        }

        public OpcNode PidTn
        {
            get
            {
                return mPidTn;
            }
        }

        public OpcNode PidTd
        {
            get
            {
                return mPidTd;
            }
        }
    }
}
