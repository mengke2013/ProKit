using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.ui
{
    class TubeMonitorPageAdapter : ITubePage
    {
        TubeMonitorPage mTubeMonitorPage;

        public TubeMonitorPageAdapter(TubeMonitorPage tubeMonitorPage)
        {
            mTubeMonitorPage = tubeMonitorPage;
        }

        public void LoadPage(byte selectedTube)
        {
            mTubeMonitorPage.LoadPage(selectedTube);
        }

        public UserControl UI()
        {
            return mTubeMonitorPage;
        }
    }
}
