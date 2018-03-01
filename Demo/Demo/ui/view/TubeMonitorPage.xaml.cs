﻿using System;
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
using Demo.controller;

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
        private MonitorController mController;

        Thread mUpdateUIRunThread;
        bool mUpdateUI;

        public TubeMonitorPage()
        {
            InitializeComponent();

            mTubeMonitorPageModel = new TubeMonitorPageModel();
            mTubePageStyle = new TubePageStyle();
            mPgbProcessModel = new ProgressDlgModel();
            mPgbProcessModel.MaxValue = 100;
            pgbProcess.DataContext = mPgbProcessModel;
            mController = new MonitorController();

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

        private void StartUpdateUIServer()
        {
            mUpdateUIRunThread = new Thread(() =>
            {
                ProcessStatus status = ProcessStatus.UNKNOWN;
                while (mUpdateUI)
                {
                    if (mTubeMonitorPageModel.SelectedTube != 0)
                    {
                        status = mController.GetStatus(mTubeMonitorPageModel.SelectedTube);
                        this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            btnHold.IsEnabled = (status == ProcessStatus.RUNNING);
                            btnStart.IsEnabled = (status == ProcessStatus.INIT || status == ProcessStatus.END || status == ProcessStatus.IDLE || status == ProcessStatus.HOLDING);
                        });

                        mController.UpdateMonitorModel(mTubeMonitorPageModel);

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
            mUpdateUIRunThread.IsBackground = true;
            mUpdateUIRunThread.Start();
        }

        private void StopUpdateUIServer()
        {
            mUpdateUI = false;
        }
    }
}
