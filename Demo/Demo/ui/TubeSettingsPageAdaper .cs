using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.ui
{
    class TubeSettingsPageAdaper : ITubePage
    {
        TubeSettingsPage mTubeSettingsPage;

        public TubeSettingsPageAdaper(TubeSettingsPage tubeSettingsPage)
        {
            mTubeSettingsPage = tubeSettingsPage;
        }

        public void LoadPage(byte selectedTube)
        {
            
        }

        public UserControl UI()
        {
            return mTubeSettingsPage;
        }
    }
}
