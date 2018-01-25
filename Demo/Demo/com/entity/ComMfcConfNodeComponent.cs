using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.com
{
    class ComMfcConfNodeComponent
    {
        private ComGasConfNodeComponent[] mGasConfNodeComponents;

        public ComMfcConfNodeComponent(byte tubeIndex)
        {
            mGasConfNodeComponents = new ComGasConfNodeComponent[8];
            for (byte gasIndex = 1; gasIndex <= 8; ++gasIndex)
            {
                mGasConfNodeComponents[gasIndex - 1] = new ComGasConfNodeComponent(tubeIndex, gasIndex);
            }
        }

        public ComGasConfNodeComponent[] GasConfNodeComponents
        {
            get
            {
                return mGasConfNodeComponents;
            }
        }
    }
}
