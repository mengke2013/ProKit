using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.ui;
using Demo.model;
using Demo.ui.model;
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
