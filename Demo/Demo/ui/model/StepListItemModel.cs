using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ui.model
{
    class StepListItemModel
    {
        private int mStepIndex;
        private int mRowIndex;

        public StepListItemModel(int stepIndex)
        {
            mStepIndex = stepIndex;
        }

        public int StepIndex
        {
            get { return mStepIndex; }
            set { mStepIndex = value; }
        }

        public int RowIndex
        {
            get { return mRowIndex; }
            set { mRowIndex = value; }
        }
    }
}
