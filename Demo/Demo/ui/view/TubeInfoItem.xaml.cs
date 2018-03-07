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
using System.Threading;
using Demo.controller;
using System.Windows.Threading;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for TubeInfoItem.xaml
    /// </summary>
    public partial class TubeInfoItem : UserControl
    {
        public delegate void ClickHandler(object sender, RoutedEventArgs e, byte tubeIndex);
        public event ClickHandler ItemClick;

        private ProgressDlgModel mPgbProcessModel;
        private TubeInfoItemController mController;
        private TubeInfoItemModel mItemMode;
        bool mUpdateUI;

        public TubeInfoItem()
        {
            InitializeComponent();
            mController = new TubeInfoItemController(this);
            mPgbProcessModel = new ProgressDlgModel();
            mPgbProcessModel.MaxValue = 100;
            pgbProcess.DataContext = mPgbProcessModel;
        }

        public void Item_Click(object sender, MouseButtonEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.Green);
            this.ItemClick(sender, e, mItemMode.TubeIndex);
            if (e != null)
            {
                e.Handled = false;
            }
        }

        public TubeInfoItemModel ItemMode
        {
            get { return mItemMode; }
            set
            {
                mItemMode = value;
                this.DataContext = mItemMode;
            }
        }

        public void StartUpdateUIServer()
        {
            Thread mUpdateUIRunThread = new Thread(() =>
            {
                ProcessStatus status = ProcessStatus.UNKNOWN;
                while (mUpdateUI)
                {
                    if (mItemMode.TubeIndex != 0)
                    {
                        status = mController.GetStatus(mItemMode.TubeIndex);
                        this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            //btnHold.IsEnabled = (status == ProcessStatus.RUNNING);
                            //btnStart.IsEnabled = (status == ProcessStatus.INIT || status == ProcessStatus.END || status == ProcessStatus.IDLE || status == ProcessStatus.HOLDING);
                        });

                        mController.UpdateTubeInfoItemModel(mItemMode);

                        int processTotalTime = mController.GetProcessEscapedTime(mItemMode.TubeIndex) + mController.GetRemainingTime(mItemMode.TubeIndex);
                        //if ((status == ProcessStatus.RUNNING || status == ProcessStatus.HOLDING || status == ProcessStatus.ABORT) && processTotalTime > 0)
                        if (processTotalTime > 0)
                        {
                            mPgbProcessModel.Progress = 100 * (processTotalTime - mController.GetRemainingTime(mItemMode.TubeIndex)) / processTotalTime;
                            mItemMode.ProcessRemainingTime = mController.GetRemainingTime(mItemMode.TubeIndex);
                        }

                    }
                    Thread.Sleep(1000);
                }
            });
            mUpdateUI = true;
            mUpdateUIRunThread.IsBackground = true;
            mUpdateUIRunThread.Start();
        }

        public void EndUpdateUIServer()
        {
            mUpdateUI = false;
        }
    }
}
