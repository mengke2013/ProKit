using Demo.ui.view;

namespace Demo.ui
{
    class TubeRecipePageAdapter : ITubePage
    {
        TubeRecipePage mTubeRecipePage;

        public TubeRecipePageAdapter(TubeRecipePage tubeRecipePage)
        {
            mTubeRecipePage = tubeRecipePage;
        }

        public void LoadPage(byte selectedTube)
        {
            mTubeRecipePage.LoadPage(selectedTube);
        }

        public void UnloadPage(byte selectedTube)
        {
            mTubeRecipePage.UnloadPage(selectedTube);
        }
    }
}
