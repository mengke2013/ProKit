using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Demo.ui.model;
using Rocky.Core.Opc.Ua;
using Demo.com;
using log4net;
using Demo.ui.view;
using System.Threading;
using System.Windows.Threading;
using Demo.service;

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for TubeMonitorPage.xaml
    /// </summary>
    public partial class TubeMonitorPage : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //public delegate void ClickHandler(object sender, RoutedEventArgs e);
        public event TubeControlBar.ClickHandler CloseClick;
        public event TubeControlBar.ClickHandler SettingsClick;

        private TubeMonitorPageModel mTubeMonitorPageModel;
        private TubePageStyle mTubePageStyle;
        private ProgressDlgModel mPgbProcessModel;

        public TubeMonitorPage()
        {
            InitializeComponent();

            mTubeMonitorPageModel = new TubeMonitorPageModel();
            mTubePageStyle = new TubePageStyle();
            mPgbProcessModel = new ProgressDlgModel();
            pgbProcess.DataContext = mPgbProcessModel;

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

        public void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessService.Instance.StartProcess(mTubeMonitorPageModel.SelectedTube);
            btnStart.IsEnabled = false;
            btnHold.IsEnabled = true;
            mPgbProcessModel.MaxValue = 100;
            mPgbProcessModel.Progress = 0;
            Thread processRunThread = new Thread(() => {
                while (mPgbProcessModel.Progress != mPgbProcessModel.MaxValue)
                {
                    //The invoke only needs to be used when updating GUI Elements
                    Thread.Sleep(500);
                    mPgbProcessModel.Progress += 1;
                    mTubeMonitorPageModel.ProcessRemainingTime = mPgbProcessModel.MaxValue - mPgbProcessModel.Progress;
                }
              
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    MessageBox.Show("Done");
                    btnHold.IsEnabled = false;
                    btnStart.IsEnabled = true;
                });
     
            });
            processRunThread.IsBackground = true;
            processRunThread.Start();
        }

        public void HoldButton_Click(object sender, RoutedEventArgs e)
        {
            btnHold.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        public void IdleButton_Click(object sender, RoutedEventArgs e)
        {
            btnHold.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        public void LoadPage(byte selectedTube)
        {
            log.Info("TubeMonitorPage");
            mTubeMonitorPageModel.SelectedTube = selectedTube;

            mTubePageStyle.TextBoxWidth = this.ActualWidth / 20;
            mTubePageStyle.TextBoxHeight = this.ActualHeight / 25;

            mTubeMonitorPageModel.TubePageStyle = mTubePageStyle;

            DataContext = mTubeMonitorPageModel;
            mTubeMonitorPageModel.UpdateDataSource();
        }
    }
}
