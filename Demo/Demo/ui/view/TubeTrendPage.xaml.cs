using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;

using log4net;

using Demo.controller;
using Demo.ui.model;


namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TubeTrendPage : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event Home.ClickHandler CloseClick;

        private TubeTrendViewModel mTrendModel;
        private TrendController mController;


        bool mUpdatePlot;
        bool mHoldUpdatePlot;

        public TubeTrendPage()
        {
            InitializeComponent();
            mController = new TrendController(this);
            mTrendModel = new TubeTrendViewModel();
            TrendView.ViewModel = mTrendModel;
            this.DataContext = mTrendModel;

            StartUpdatePlotServer();
            HoldUpdatePlot();
        }

        public void LoadPage(byte selectedTube)
        {
            log.Info("TubeTrendPage");
            mTrendModel.TubeIndex = selectedTube;
            mTrendModel.PlotType = TrendPlotType.Temperature;

            Visibility = Visibility.Visible;

            TrendView.LoadView();
            mController.ConvertTrendPageModel();
            TrendView.UpdatePlot();

            ContinueUpdatePlot();
        }

        public void UnloadPage(byte selectedTube)
        {
            Visibility = Visibility.Hidden;
            //            ClearValue(EffectProperty);
        }

        private void HoldUpdatePlot()
        {
            mHoldUpdatePlot = true;
        }

        private void ContinueUpdatePlot()
        {
            mHoldUpdatePlot = false;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseClick(sender, e);
            e.Handled = false;
        }

        private void Temper_Click(object sender, RoutedEventArgs e)
        {
            mTrendModel.PlotType = TrendPlotType.Temperature;
            TrendView.LoadView();
            mController.ConvertTrendPageModel();
            TrendView.UpdatePlot();
        }

        private void Gas_Click(object sender, RoutedEventArgs e)
        {
            mTrendModel.PlotType = TrendPlotType.Gas;
            TrendView.LoadView();
            mController.ConvertTrendPageModel();
            TrendView.UpdatePlot();
        }

        private void Vacuum_Click(object sender, RoutedEventArgs e)
        {
            mTrendModel.PlotType = TrendPlotType.Vacuum;
            TrendView.LoadView();
            mController.ConvertTrendPageModel();
            TrendView.UpdatePlot();
        }

        public void StartUpdatePlotServer()
        {
            Thread mUpdateUIRunThread = new Thread(() =>
            {
                ProcessStatus status = ProcessStatus.UNKNOWN;
                while (mUpdatePlot)
                {
                    if (mTrendModel.TubeIndex != 0)
                    {
                        if (!mHoldUpdatePlot)
                        {
                            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                            {
                                mController.ConvertTrendPageModel();
                                TrendView.UpdatePlot();
                            });
                        }

                    }
                    Thread.Sleep(1000);
                }
            });
            mUpdatePlot = true;
            mUpdateUIRunThread.IsBackground = true;
            mUpdateUIRunThread.Start();
        }

        public TubeTrendViewModel ViewModel
        {
            get { return mTrendModel; }
        }

    }
}
