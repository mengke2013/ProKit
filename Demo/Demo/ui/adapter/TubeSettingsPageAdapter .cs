using Demo.ui.view;

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
            mTubeSettingsPage.LoadPage(selectedTube);
        }

        public void UnloadPage(byte selectedTube)
        {
            mTubeSettingsPage.UnloadPage(selectedTube);
        }
    }
}
