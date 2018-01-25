using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.com
{
    class ComMfcNodeComponent
    {
        private ComGasNodeComponent[] mGasNodeComponents;

        public ComMfcNodeComponent(byte tubeIndex)
        {
            mGasNodeComponents = new ComGasNodeComponent[8];
            for (byte gasIndex = 1; gasIndex <= 8; ++gasIndex)
            {
                mGasNodeComponents[gasIndex - 1] = new ComGasNodeComponent(tubeIndex, gasIndex);
            }
        }

        public ComGasNodeComponent[] GasNodeComponents
        {
            get
            {
                return mGasNodeComponents;
            }
        }
    }
}
