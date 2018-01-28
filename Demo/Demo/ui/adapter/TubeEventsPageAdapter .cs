using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.ui
{
    class TubeEventsPageAdapter : ITubePage
    {
        TubeEventsPage mTubeEventsPage;

        public TubeEventsPageAdapter(TubeEventsPage tubeEventsPage)
        {
            mTubeEventsPage = tubeEventsPage;
        }

        public void LoadPage(byte selectedTube)
        {
            mTubeEventsPage.LoadTubePage(selectedTube);
            mTubeEventsPage.DescriptionColumn.Width = mTubeEventsPage.dataGrid.ActualWidth - 890;
        }

        public UserControl UI()
        {
            return mTubeEventsPage;
        }
    }
}
