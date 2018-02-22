using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.model
{
    public class Recipe
    {
        private string mName;
        private RecipeStep[] mSteps;

        public Recipe()
        {
            mSteps = new RecipeStep[64];
        }

        public RecipeStep[] Steps
        {
            get { return mSteps; }
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
    }
}
