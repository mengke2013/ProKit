using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ui.model
{
    class StepDetailModel
    {
        private int mStepIndex; 

        public StepDetailModel(int stepIndex)
        {
            mStepIndex = stepIndex;
        }

        public int StepIndex
        {
            get { return mStepIndex; }
            set { mStepIndex = value; }
        }
    }
}
