using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.ui
{
    class TubeRecipePageAdaper : ITubePage
    {
        TubeRecipePage mTubeRecipePage;

        public TubeRecipePageAdaper(TubeRecipePage tubeRecipePage)
        {
            mTubeRecipePage = tubeRecipePage;
        }

        public void LoadPage(byte selectedTube)
        {
            
        }

        public UserControl UI()
        {
            return mTubeRecipePage;
        }
    }
}
