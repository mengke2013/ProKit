using Demo.ui.view;

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
            mTubeTrendPage.LoadPage(selectedTube);
        }

        public void UnloadPage(byte selectedTube)
        {
            mTubeTrendPage.UnloadPage(selectedTube);
        }
    }
}
