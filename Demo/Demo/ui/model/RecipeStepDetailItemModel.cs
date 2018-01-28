using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ui.model
{
    class RecipeStepDetailItemModel
    {
        private int mStepIndex; 

        public RecipeStepDetailItemModel(int stepIndex)
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
