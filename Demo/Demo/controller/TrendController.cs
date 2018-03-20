using Demo.ui.view;
using Demo.model;
using Demo.service;

namespace Demo.controller
{
    class TrendController
    {
        private TubeTrendPage mPage;

        public TrendController(TubeTrendPage page)
        {
            mPage = page;
        }

        public void ConvertTrendPageModel()
        {
            Trend trend = TrendService.Instance.GetTrend(mPage.ViewModel.TubeIndex, mPage.ViewModel.PlotType);
            mPage.ViewModel.DataPoints = trend.DataPoints;
        }
    }
}
