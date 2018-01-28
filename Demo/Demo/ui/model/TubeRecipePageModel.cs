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

        }

        public void LoadData(byte selectedTube)
        {
            mStepDetailItemModels.Add(new RecipeStepDetailItemModel(1));
            mStepDetailItemModels.Add(new RecipeStepDetailItemModel(2));
            mStepDetailItemModels.Add(new RecipeStepDetailItemModel(3));
            mStepDetailItemModels.Add(new RecipeStepDetailItemModel(4));
            mStepDetailItemModels.Add(new RecipeStepDetailItemModel(5));
            mStepDetailItemModels.Add(new RecipeStepDetailItemModel(6));
            mStepDetailItemModels.Add(new RecipeStepDetailItemModel(7));
            mStepDetailItemModels.Add(new RecipeStepDetailItemModel(8));
            mStepDetailItemModels.Add(new RecipeStepDetailItemModel(9));
            mStepDetailItemModels.Add(new RecipeStepDetailItemModel(10));
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
