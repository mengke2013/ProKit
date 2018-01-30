﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.ui
{
    class TubeRecipePageAdapter : ITubePage
    {
        TubeRecipePage mTubeRecipePage;

        public TubeRecipePageAdapter(TubeRecipePage tubeRecipePage)
        {
            mTubeRecipePage = tubeRecipePage;
        }

        public void LoadPage(byte selectedTube)
        {
            mTubeRecipePage.LoadTubePage(selectedTube);
        }

        public UserControl UI()
        {
            return mTubeRecipePage;
        }
    }
}