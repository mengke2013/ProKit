using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.com
{
    class ComTubeGroupConfNodeComponent
    {
        private ComFurnaceConfNodeComponent mFurnaceConfNodeComponent;
        private ComVacuumConfNodeComponent mVacuumConfNodeComponent;
        private ComMfcConfNodeComponent mMfcConfNodeComponent;
        private ComPaddleConfNodeComponent mPaddleConfNodeComponent;
        private ComSecurityConfNodeComponent mSecurityConfNodeComponent;
        private ComDioConfNodeComponent mDiConfNodeComponent;

        public ComTubeGroupConfNodeComponent(byte tubeGroupIndex)
        {
            mFurnaceConfNodeComponent = new ComFurnaceConfNodeComponent(tubeGroupIndex);
            mVacuumConfNodeComponent = new ComVacuumConfNodeComponent(tubeGroupIndex);
            mMfcConfNodeComponent = new ComMfcConfNodeComponent(tubeGroupIndex);
            mPaddleConfNodeComponent = new ComPaddleConfNodeComponent(tubeGroupIndex);
            mSecurityConfNodeComponent = new ComSecurityConfNodeComponent(tubeGroupIndex);
            mDiConfNodeComponent = new ComDioConfNodeComponent(tubeGroupIndex);
        }
    }
}
