using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.com
{
    class ComFurnaceConfNodeComponent
    {
        private ComTemperConfNodeComponent[] mTemperConfNodeComponents;

        public ComFurnaceConfNodeComponent(byte tubeGroupIndex)
        {
            mTemperConfNodeComponents = new ComTemperConfNodeComponent[8];
            for (byte zoneIndex = 1; zoneIndex <= 8; ++zoneIndex)
            {
                mTemperConfNodeComponents[zoneIndex - 1] = new ComTemperConfNodeComponent(tubeGroupIndex, zoneIndex);
            }
        }

        public ComTemperConfNodeComponent[] TemperConfNodeComponents
        {
            get
            {
                return mTemperConfNodeComponents;
            }
        }
    }
}
