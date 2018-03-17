using Demo.ui.view;

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

        public void UnloadPage(byte selectedTube)
        {
            mTubeMonitorPage.UnloadPage(selectedTube);
        }
    }
}
