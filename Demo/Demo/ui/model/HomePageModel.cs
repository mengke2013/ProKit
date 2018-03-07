using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ui.model
{
    class HomePageModel
    {
        private List<TubeInfoItemModel> mTubeInfoItemModels;

        public HomePageModel()
        {
            mTubeInfoItemModels = new List<TubeInfoItemModel>();
            for (byte i = 0; i < 6; ++i)
            {
                TubeInfoItemModel tubeInfoItemModel = new TubeInfoItemModel((byte)(i + 1));
                //stepListItemModel.RowIndex = i;
                mTubeInfoItemModels.Add(tubeInfoItemModel);
            }
        }

        public List<TubeInfoItemModel> TubeInfoItems
        {
            get
            {
                return mTubeInfoItemModels;
            }
        }
    }
}
