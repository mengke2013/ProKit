﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.ui
{
    class TubeSettingsPageAdapter : ITubePage
    {
        TubeSettingsPage mTubeSettingsPage;

        public TubeSettingsPageAdapter(TubeSettingsPage tubeSettingsPage)
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