using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.ui
{
    class TubeMonitorPageAdaper : ITubePage
    {
        TubeMonitorPage mTubeMonitorPage;

        public TubeMonitorPageAdaper(TubeMonitorPage tubeMonitorPage)
        {
            mTubeMonitorPage = tubeMonitorPage;
        }

        public void LoadPage(byte selectedTube)
        {
            
        }

        public UserControl UI()
        {
            return mTubeMonitorPage;
        }
    }
}
