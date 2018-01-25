using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComVacuumNodeComponent
    {
        private ComAnalogNodeComponent[] mAnalogNodeComponents;

        public ComVacuumNodeComponent(byte tubeIndex)
        {
            mAnalogNodeComponents = new ComAnalogNodeComponent[8];
            for (byte analogIndex = 1; analogIndex <= 8; ++analogIndex)
            {
                mAnalogNodeComponents[analogIndex - 1] = new ComAnalogNodeComponent(tubeIndex, analogIndex);
            }
        }

        public ComAnalogNodeComponent[] AnalogNodeComponents
        {
            get
            {
                return mAnalogNodeComponents;
            }
        }
    }
}
