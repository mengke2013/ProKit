using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;

using log4net;

using Demo.ui.model;
using Demo.controller;
using System;
using System.Windows.Media.Imaging;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for TubeMonitorPage.xaml
    /// </summary>
    public partial class TubeMonitorPage : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event Home.ClickHandler CloseClick;
        public event Home.ClickHandler SettingsClick;

        private TubeMonitorViewModel mTubeMonitorPageModel;
        private TubePageStyle mTubePageStyle;
        private ProgressDlgModel mPgbProcessModel;
        private MonitorController mController;

        Thread mUpdateUIRunThread;
        bool mManual;

        private bool mUpdateUI;
        private bool mHoldingUpdate;

        public TubeMonitorPage()
        {
            InitializeComponent();

            mTubeMonitorPageModel = new TubeMonitorViewModel();
            mTubePageStyle = new TubePageStyle();
            mPgbProcessModel = new ProgressDlgModel();
            mPgbProcessModel.MaxValue = 100;
            pgbProcess.DataContext = mPgbProcessModel;
            mController = new MonitorController(this);

            StartUpdateUIServer();
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseClick(sender, e);
            e.Handled = false;
        }

        public void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.SettingsClick(sender, e);
            e.Handled = false;
        }

        public void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            mManual = !mManual;
            mTubeMonitorPageModel.EditVisible = mManual ? Visibility.Visible : Visibility.Hidden;
            btnManualImage.Source = mManual ? new BitmapImage(new Uri(@System.Environment.CurrentDirectory+"/../../images/auto.png")) : new BitmapImage(new Uri(@System.Environment.CurrentDirectory + "/../../images/maintenance.png"));
            btnCommit.Visibility = mManual ? Visibility.Visible : Visibility.Hidden;

            if (mManual)
            {
                mController.LoadMonitorSetpoints();
            }
        }

        public void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            mController.CommitChanges(mTubeMonitorPageModel.SelectedTube, OnCommitEditSetpointComplete);

        }

        private void OnCommitEditSetpointComplete()
        {
            MessageBox.Show("Done");
        }

        public void StartButton_Click(object sender, RoutedEventArgs e)
        {
            mController.StartProcess(mTubeMonitorPageModel.SelectedTube, OnStartProcessComplete);
            btnStart.IsEnabled = false;
            btnHold.IsEnabled = true;
        }

        private void OnStartProcessComplete()
        {
            MessageBox.Show("OnStartProcessComplete");
        }

        public void HoldButton_Click(object sender, RoutedEventArgs e)
        {
            mController.HoldProcess(mTubeMonitorPageModel.SelectedTube, OnHoldProcessComplete);
            btnHold.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        private void OnHoldProcessComplete()
        {
            MessageBox.Show("OnHoldProcessComplete");
        }

        public void NextButton_Click(object sender, RoutedEventArgs e)
        {
            mController.NextProcess(mTubeMonitorPageModel.SelectedTube, OnNextProcessComplete);
        }

        private void OnNextProcessComplete()
        {
            MessageBox.Show("OnNextProcessComplete");
        }

        public void IdleButton_Click(object sender, RoutedEventArgs e)
        {
            mController.IdleProcess(mTubeMonitorPageModel.SelectedTube, OnIdleProcessComplete);
            btnHold.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        private void OnIdleProcessComplete()
        {
            MessageBox.Show("OnIdleProcessComplete");
        }

        public void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            mController.AbortProcess(mTubeMonitorPageModel.SelectedTube, OnAbortProcessComplete);
            btnHold.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        private void OnAbortProcessComplete()
        {
            MessageBox.Show("OnAbortProcessComplete");
        }

        public void LoadPage(byte selectedTube)
        {
            log.Info("TubeMonitorPage");
            mTubeMonitorPageModel.SelectedTube = selectedTube;

            Visibility = Visibility.Visible;

            mTubePageStyle.TextBoxWidth = this.ActualWidth / 20;
            mTubePageStyle.TextBoxHeight = this.ActualHeight / 25;
            mTubePageStyle.LabelWidth = this.ActualWidth / 20;
            mTubePageStyle.LabelHeight = this.ActualHeight / 25;
            mTubeMonitorPageModel.FurnaceHeight = (int)(MonitorView.FurnaceControl.ActualHeight - 10);

            mTubeMonitorPageModel.TubePageStyle = mTubePageStyle;


            DataContext = mTubeMonitorPageModel;
            mTubeMonitorPageModel.EditVisible = mManual ? Visibility.Visible : Visibility.Hidden;
            if (mManual)
            {
                mController.LoadMonitorSetpoints();
            }

            mHoldingUpdate = false;
        }

        public void UnloadPage(byte selectedTube)
        {
            Visibility = Visibility.Hidden;
            //            ClearValue(EffectProperty);

            mHoldingUpdate = true;
        }

        public TubeMonitorViewModel PageModel
        {
            get { return mTubeMonitorPageModel; }
        }

        private void StartUpdateUIServer()
        {
            mUpdateUIRunThread = new Thread(() =>
            {
                ProcessStatus status = ProcessStatus.UNKNOWN;
                while (mUpdateUI)
                {
                    if (mTubeMonitorPageModel.SelectedTube != 0 && !mHoldingUpdate)
                    {
                        status = mController.GetStatus(mTubeMonitorPageModel.SelectedTube);
                        this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            btnHold.IsEnabled = (status == ProcessStatus.RUNNING);
                            btnStart.IsEnabled = (status == ProcessStatus.INIT || status == ProcessStatus.END || status == ProcessStatus.IDLE || status == ProcessStatus.HOLDING);
                        });

                        mController.UpdateMonitorModel();

                        int processTotalTime = mController.GetProcessEscapedTime(mTubeMonitorPageModel.SelectedTube) + mController.GetRemainingTime(mTubeMonitorPageModel.SelectedTube);
                        //if ((status == ProcessStatus.RUNNING || status == ProcessStatus.HOLDING || status == ProcessStatus.ABORT) && processTotalTime > 0)
                        if (processTotalTime > 0)
                        {
                            mPgbProcessModel.Progress = 100 * (processTotalTime - mController.GetRemainingTime(mTubeMonitorPageModel.SelectedTube)) / processTotalTime;
                            mTubeMonitorPageModel.ProcessRemainingTime = mController.GetRemainingTime(mTubeMonitorPageModel.SelectedTube);
                        }

                    }
                    Thread.Sleep(1000);
                }
            });
            mUpdateUI = true;
            mHoldingUpdate = true;
            mUpdateUIRunThread.IsBackground = true;
            mUpdateUIRunThread.Start();
        }

        private void StopUpdateUIServer()
        {
            mUpdateUI = false;
        }
    }
}
