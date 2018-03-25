using Demo.ui.view;

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
            mTubeEventsPage.LoadPage(selectedTube);
            mTubeEventsPage.DescriptionColumn.Width = mTubeEventsPage.dataGrid.ActualWidth - 890;
        }

        public void UnloadPage(byte selectedTube)
        {
            mTubeEventsPage.UnloadPage(selectedTube);
        }
    }
}
