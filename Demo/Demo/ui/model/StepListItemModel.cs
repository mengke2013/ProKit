using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ui.model
{
    public class StepListItemModel
    {
        private int mStepIndex;
        private string mStepName;
        private sbyte mStepType;
        private int mStepTime;
        private int mRowIndex;


        public StepListItemModel(byte stepIndex)
        {
            mStepIndex = stepIndex;
        }

        public int StepIndex
        {
            get { return mStepIndex; }
            set { mStepIndex = value; }
        }

        public string StepName
        {
            get { return mStepName; }
            set { mStepName = value;}
        }

        public sbyte StepType
        {
            get { return mStepType; }
            set { mStepType = value; }
        }

        public int StepTime
        {
            get { return mStepTime; }
            set { mStepTime = value; }
        }

        public int RowIndex
        {
            get { return mRowIndex; }
            set { mRowIndex = value; }
        }
    }
}
