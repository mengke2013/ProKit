using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;

namespace Demo.ui.model
{
    class TubeRecipePageModel
    {
        private List<RecipeStepDetailItemModel> mStepDetailItemModels;
        public TubeRecipePageModel()
        {
            mStepDetailItemModels = new List<RecipeStepDetailItemModel>();
            for (int i = 0; i < 40; ++i)
            {
                mStepDetailItemModels.Add(new RecipeStepDetailItemModel(i + 1));
            }
        }

        public void LoadData(byte selectedTube)
        {

        }

        public List<RecipeStepDetailItemModel> StepDetailItems
        {
            get
            {
                return mStepDetailItemModels;
            }
        }
    }
}
