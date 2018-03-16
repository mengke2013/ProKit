using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.ui
{
    class TubeAlarmPageAdapter : ITubePage
    {
        TubeAlarmPage mTubeAlarmPage;

        public TubeAlarmPageAdapter(TubeAlarmPage tubeAlarmPage)
        {
            mTubeAlarmPage = tubeAlarmPage;
        }

        public void LoadPage(byte selectedTube)
        {
            mTubeAlarmPage.LoadTubePage(selectedTube);
        }

        public UserControl UI()
        {
            return mTubeAlarmPage;
        }
    }
}
