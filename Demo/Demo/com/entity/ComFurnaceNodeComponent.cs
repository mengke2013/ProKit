using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.com
{
    class ComFurnaceNodeComponent
    {
        private ComTemperNodeComponent[] mTemperNodeComponents;

        public ComFurnaceNodeComponent(byte tubeIndex)
        {
            mTemperNodeComponents = new ComTemperNodeComponent[8];
            for (byte zoneIndex = 1; zoneIndex <= 8; ++zoneIndex)
            {
                mTemperNodeComponents[zoneIndex - 1] = new ComTemperNodeComponent(tubeIndex, zoneIndex);
            }
        }

        public ComTemperNodeComponent[] TemperNodeComponents
        {
            get
            {
                return mTemperNodeComponents;
            }
        }
    }
}
