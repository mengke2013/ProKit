using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ui.model
{
    public class StepListItemModel
    {
        private byte mStepIndex;
        private int mRowIndex;

        public StepListItemModel(byte stepIndex)
        {
            mStepIndex = stepIndex;
        }

        public byte StepIndex
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
