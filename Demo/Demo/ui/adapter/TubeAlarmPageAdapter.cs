using Demo.ui.view;

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
            mTubeAlarmPage.LoadPage(selectedTube);
        }

        public void UnloadPage(byte selectedTube)
        {
            mTubeAlarmPage.UnloadPage(selectedTube);
        }
    }
}
