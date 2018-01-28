using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.ui
{
    class TubeTrendPageAdapter : ITubePage
    {
        TubeTrendPage mTubeTrendPage;

        public TubeTrendPageAdapter(TubeTrendPage tubeTrendPage)
        {
            mTubeTrendPage = tubeTrendPage;
        }

        public void LoadPage(byte selectedTube)
        {
            
        }

        public UserControl UI()
        {
            return mTubeTrendPage;
        }
    }
}
