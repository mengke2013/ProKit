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
using log4net;
using Demo.ui.model;
using Demo.ui.view;
using Demo.com;
using Rocky.Core.Opc.Ua;
using Demo.com.entity;
using System.Windows.Threading;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Demo.model;
using Demo.controller;

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for TubeRecipePage.xaml
    /// </summary>
    public partial class TubeSettingsPage : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private byte mSelectedTube;
        private TubeSettingsViewModel mSettingsModel;
        private SettingsController mController;
        private TubePageStyle mTubePageStyle;

        public event TubeControlBar.ClickHandler CloseClick;
        private ProgressDlg mProgressDlg;

        public TubeSettingsPage()
        {
            InitializeComponent();
            mProgressDlg = new ProgressDlg();
            mProgressDlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            mTubePageStyle = new TubePageStyle();
            mSettingsModel = new TubeSettingsViewModel();
            mController = new SettingsController();
        }

        public void LoadPage(byte selectedTube)
        {
            log.Debug("TubeRecipePage:LoadTubePage");
            mSelectedTube = selectedTube;

           // mTubePageStyle.TextBoxWidth = this.ActualWidth / 15;
            mTubePageStyle.TextBoxHeight = this.ActualHeight / 25;

            mSettingsModel.TubePageStyle = mTubePageStyle;
            SettingsView.DataContext = mSettingsModel;


            mController.LoadSettings(mSelectedTube);
            bool startSyn = mController.SynSettings(mSelectedTube, OnSynSettingsComplete);
            if (startSyn)
            {
                mProgressDlg.ProgressModel.MaxValue = 64;
                mProgressDlg.ProgressModel.Progress = 0;
                mProgressDlg.ShowDialog();
            }


        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseClick(sender, e);
            e.Handled = false;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Recipe Data Files (*.config)|*.config"
            };
            openFileDialog.Title = "Open a Configuration File";
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                log.Debug("load configuration file");
                //Demo.utilities.Properties config = new Demo.utilities.Properties("configuration.properties");
                Demo.utilities.Properties config = new Demo.utilities.Properties(openFileDialog.FileName);
                short value;
                short.TryParse(config.get("Gas1MaxValue"), out value);
                mSettingsModel.Gas1MaxValue = value;
                short.TryParse(config.get("Gas2MaxValue"), out value);
                mSettingsModel.Gas2MaxValue = value;
                short.TryParse(config.get("Gas5MaxValue"), out value);
                mSettingsModel.Gas5MaxValue = value;
                short.TryParse(config.get("Gas6MaxValue"), out value);
                mSettingsModel.Gas6MaxValue = value;
                short.TryParse(config.get("Gas8MaxValue"), out value);
                mSettingsModel.Gas8MaxValue = value;
                short.TryParse(config.get("Ana1MaxValue"), out value);
                mSettingsModel.Ana1MaxValue = value;
                short.TryParse(config.get("Ana3MaxValue"), out value);
                mSettingsModel.Ana3MaxValue = value;
                short.TryParse(config.get("Ana4MaxValue"), out value);
                mSettingsModel.Ana4MaxValue = value;
                short.TryParse(config.get("Ana5MaxValue"), out value);
                mSettingsModel.Ana5MaxValue = value;
                short.TryParse(config.get("Ana6MaxValue"), out value);
                mSettingsModel.Ana6MaxValue = value;

                short.TryParse(config.get("VacuumKp"), out value);
                mSettingsModel.VacuumKp = value;
                short.TryParse(config.get("VacuumTn"), out value);
                mSettingsModel.VacuumTn = value;
                short.TryParse(config.get("VacuumTd"), out value);
                mSettingsModel.VacuumTd = value;

                byte value2;
                byte.TryParse(config.get("Gas1Ev"), out value2);
                mSettingsModel.Gas1Ev = value2;
                byte.TryParse(config.get("Gas2Ev"), out value2);
                mSettingsModel.Gas2Ev = value2;
                byte.TryParse(config.get("Gas5Ev"), out value2);
                mSettingsModel.Gas5Ev = value2;
                byte.TryParse(config.get("Gas6Ev"), out value2);
                mSettingsModel.Gas6Ev = value2;
                byte.TryParse(config.get("Gas8Ev"), out value2);
                mSettingsModel.Gas8Ev = value2;

                short.TryParse(config.get("TemperIntKp1"), out value);
                mSettingsModel.TemperIntKp1 = value;
                short.TryParse(config.get("TemperIntKp2"), out value);
                mSettingsModel.TemperIntKp2 = value;
                short.TryParse(config.get("TemperIntKp3"), out value);
                mSettingsModel.TemperIntKp3 = value;
                short.TryParse(config.get("TemperIntKp4"), out value);
                mSettingsModel.TemperIntKp4 = value;
                short.TryParse(config.get("TemperIntKp5"), out value);
                mSettingsModel.TemperIntKp5 = value;
                short.TryParse(config.get("TemperIntKp6"), out value);
                mSettingsModel.TemperIntKp6 = value;
                short.TryParse(config.get("TemperIntTn1"), out value);
                mSettingsModel.TemperIntTn1 = value;
                short.TryParse(config.get("TemperIntTn2"), out value);
                mSettingsModel.TemperIntTn2 = value;
                short.TryParse(config.get("TemperIntTn3"), out value);
                mSettingsModel.TemperIntTn3 = value;
                short.TryParse(config.get("TemperIntTn4"), out value);
                mSettingsModel.TemperIntTn4 = value;
                short.TryParse(config.get("TemperIntTn5"), out value);
                mSettingsModel.TemperIntTn5 = value;
                short.TryParse(config.get("TemperIntTn6"), out value);
                mSettingsModel.TemperIntTn6 = value;
                short.TryParse(config.get("TemperIntTd1"), out value);
                mSettingsModel.TemperIntTd1 = value;
                short.TryParse(config.get("TemperIntTd2"), out value);
                mSettingsModel.TemperIntTd2 = value;
                short.TryParse(config.get("TemperIntTd3"), out value);
                mSettingsModel.TemperIntTd3 = value;
                short.TryParse(config.get("TemperIntTd4"), out value);
                mSettingsModel.TemperIntTd4 = value;
                short.TryParse(config.get("TemperIntTd5"), out value);
                mSettingsModel.TemperIntTd5 = value;
                short.TryParse(config.get("TemperIntTd6"), out value);
                mSettingsModel.TemperIntTd6 = value;
                short.TryParse(config.get("TemperExtKp1"), out value);
                mSettingsModel.TemperExtKp1 = value;
                short.TryParse(config.get("TemperExtKp2"), out value);
                mSettingsModel.TemperExtKp2 = value;
                short.TryParse(config.get("TemperExtKp3"), out value);
                mSettingsModel.TemperExtKp3 = value;
                short.TryParse(config.get("TemperExtKp4"), out value);
                mSettingsModel.TemperExtKp4 = value;
                short.TryParse(config.get("TemperExtKp5"), out value);
                mSettingsModel.TemperExtKp5 = value;
                short.TryParse(config.get("TemperExtKp6"), out value);
                mSettingsModel.TemperExtKp6 = value;
                short.TryParse(config.get("TemperExtTn1"), out value);
                mSettingsModel.TemperExtTn1 = value;
                short.TryParse(config.get("TemperExtTn2"), out value);
                mSettingsModel.TemperExtTn2 = value;
                short.TryParse(config.get("TemperExtTn3"), out value);
                mSettingsModel.TemperExtTn3 = value;
                short.TryParse(config.get("TemperExtTn4"), out value);
                mSettingsModel.TemperExtTn4 = value;
                short.TryParse(config.get("TemperExtTn5"), out value);
                mSettingsModel.TemperExtTn5 = value;
                short.TryParse(config.get("TemperExtTn6"), out value);
                mSettingsModel.TemperExtTn6 = value;
                short.TryParse(config.get("TemperExtTd1"), out value);
                mSettingsModel.TemperExtTd1 = value;
                short.TryParse(config.get("TemperExtTd2"), out value);
                mSettingsModel.TemperExtTd2 = value;
                short.TryParse(config.get("TemperExtTd3"), out value);
                mSettingsModel.TemperExtTd3 = value;
                short.TryParse(config.get("TemperExtTd4"), out value);
                mSettingsModel.TemperExtTd4 = value;
                short.TryParse(config.get("TemperExtTd5"), out value);
                mSettingsModel.TemperExtTd5 = value;
                short.TryParse(config.get("TemperExtTd6"), out value);
                mSettingsModel.TemperExtTd6 = value;
                short.TryParse(config.get("MaxPressure"), out value);
                mSettingsModel.MaxPressure = value;
                short.TryParse(config.get("MinPressure"), out value);
                mSettingsModel.MinPressure = value;
                short.TryParse(config.get("VacuumTd"), out value);
                mSettingsModel.VacuumTd = value;
                short.TryParse(config.get("MaxTemper5"), out value);
                mSettingsModel.MaxTemper5 = value;
                short.TryParse(config.get("MinTemper5"), out value);
                mSettingsModel.MinTemper5 = value;
                short.TryParse(config.get("MaxPump"), out value);
                mSettingsModel.MaxPump = value;

                int value1;
                int.TryParse(config.get("Di"), out value1);
                mSettingsModel.Di = value1;
                int.TryParse(config.get("Offset"), out value1);
                mSettingsModel.Offset = value1;
                int.TryParse(config.get("PositionDev"), out value1);
                mSettingsModel.PositionDev = value1;
                int.TryParse(config.get("ClosePosition"), out value1);
                mSettingsModel.ClosePosition = value1;
                this.DataContext = mSettingsModel;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "Recipe Data Files (*.config)|*.config"
            };
            saveFileDialog.Title = "Save a Configuration File";
            var result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                log.Debug("save configuration file");
                //Demo.utilities.Properties config = new Demo.utilities.Properties("configuration.properties");
                Demo.utilities.Properties config = new Demo.utilities.Properties(saveFileDialog.FileName);
                config.set("Gas1MaxValue", mSettingsModel.Gas1MaxValue);
                config.set("Gas2MaxValue", mSettingsModel.Gas2MaxValue);
                config.set("Gas5MaxValue", mSettingsModel.Gas5MaxValue);
                config.set("Gas6MaxValue", mSettingsModel.Gas6MaxValue);
                config.set("Gas8MaxValue", mSettingsModel.Gas8MaxValue);
                config.set("Ana1MaxValue", mSettingsModel.Ana1MaxValue);
                config.set("Ana3MaxValue", mSettingsModel.Ana3MaxValue);
                config.set("Ana4MaxValue", mSettingsModel.Ana4MaxValue);
                config.set("Ana5MaxValue", mSettingsModel.Ana5MaxValue);
                config.set("Ana6MaxValue", mSettingsModel.Ana6MaxValue);
                config.set("VacuumKp", mSettingsModel.VacuumKp);
                config.set("VacuumTn", mSettingsModel.VacuumTn);
                config.set("VacuumTd", mSettingsModel.VacuumTd);
                config.set("Gas1Ev", mSettingsModel.Gas1Ev);
                config.set("Gas2Ev", mSettingsModel.Gas2Ev);
                config.set("Gas5Ev", mSettingsModel.Gas5Ev);
                config.set("Gas6Ev", mSettingsModel.Gas6Ev);
                config.set("Gas8Ev", mSettingsModel.Gas8Ev);
                config.set("TemperIntKp1", mSettingsModel.TemperIntKp1);
                config.set("TemperIntKp2", mSettingsModel.TemperIntKp2);
                config.set("TemperIntKp3", mSettingsModel.TemperIntKp3);
                config.set("TemperIntKp4", mSettingsModel.TemperIntKp4);
                config.set("TemperIntKp5", mSettingsModel.TemperIntKp5);
                config.set("TemperIntKp6", mSettingsModel.TemperIntKp6);
                config.set("TemperIntTn1", mSettingsModel.TemperIntTn1);
                config.set("TemperIntTn2", mSettingsModel.TemperIntTn2);
                config.set("TemperIntTn3", mSettingsModel.TemperIntTn3);
                config.set("TemperIntTn4", mSettingsModel.TemperIntTn4);
                config.set("TemperIntTn5", mSettingsModel.TemperIntTn5);
                config.set("TemperIntTn6", mSettingsModel.TemperIntTn6);
                config.set("TemperIntTd1", mSettingsModel.TemperIntTd1);
                config.set("TemperIntTd2", mSettingsModel.TemperIntTd2);
                config.set("TemperIntTd3", mSettingsModel.TemperIntTd3);
                config.set("TemperIntTd4", mSettingsModel.TemperIntTd4);
                config.set("TemperIntTd5", mSettingsModel.TemperIntTd5);
                config.set("TemperIntTd6", mSettingsModel.TemperIntTd6);
                config.set("TemperExtKp1", mSettingsModel.TemperExtKp1);
                config.set("TemperExtKp2", mSettingsModel.TemperExtKp2);
                config.set("TemperExtKp3", mSettingsModel.TemperExtKp3);
                config.set("TemperExtKp4", mSettingsModel.TemperExtKp4);
                config.set("TemperExtKp5", mSettingsModel.TemperExtKp5);
                config.set("TemperExtKp6", mSettingsModel.TemperExtKp6);
                config.set("TemperExtTn1", mSettingsModel.TemperExtTn1);
                config.set("TemperExtTn2", mSettingsModel.TemperExtTn2);
                config.set("TemperExtTn3", mSettingsModel.TemperExtTn3);
                config.set("TemperExtTn4", mSettingsModel.TemperExtTn4);
                config.set("TemperExtTn5", mSettingsModel.TemperExtTn5);
                config.set("TemperExtTn6", mSettingsModel.TemperExtTn6);
                config.set("TemperExtTd1", mSettingsModel.TemperExtTd1);
                config.set("TemperExtTd2", mSettingsModel.TemperExtTd2);
                config.set("TemperExtTd3", mSettingsModel.TemperExtTd3);
                config.set("TemperExtTd4", mSettingsModel.TemperExtTd4);
                config.set("TemperExtTd5", mSettingsModel.TemperExtTd5);
                config.set("TemperExtTd6", mSettingsModel.TemperExtTd6);
                config.set("MaxPressure", mSettingsModel.MaxPressure);
                config.set("MinPressure", mSettingsModel.MinPressure);
                config.set("MaxTemper", mSettingsModel.MaxTemper);
                config.set("MaxTemper5", mSettingsModel.MaxTemper5);
                config.set("MinTemper5", mSettingsModel.MinTemper5);
                config.set("MaxPump", mSettingsModel.MaxPump);
                config.set("Di", mSettingsModel.Di);
                config.set("Offset", mSettingsModel.Offset);
                config.set("PositionDev", mSettingsModel.PositionDev);
                config.set("ClosePosition", mSettingsModel.ClosePosition);
                config.Save();
            }
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Would you want to Read configuration from device?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                log.Debug("TubeSettingsDialog:RefreshBtn_Click");
                bool startSyn = mController.SynSettings(mSelectedTube, OnSynSettingsComplete);
                if (startSyn)
                {
                    //mProgressDlg.ProgressModel.MaxValue = 64;
                    //mProgressDlg.ProgressModel.Progress = 0;
                    //mProgressDlg.ShowDialog();
                }
            }

        }

        private void Write_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Would you want to Write configuration to device?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Settings settings = mController.GetSettings(mSelectedTube);
                mController.ConvertSettingsModel(settings, mSettingsModel);

                bool startDownload = mController.CommitSettings(mSelectedTube, OnDownSettingsComplete);
                if (startDownload)
                {
                    //mProgressDlg.ProgressModel.MaxValue = 64;
                    //mProgressDlg.ProgressModel.Progress = 0;
                    //mProgressDlg.ShowDialog();
                }
            }
        }

        private void OnSynSettingsComplete(Settings settings)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                //StepItems[0].Item_Click(null, null);
                //MessageBox.Show("OnSynSettingsComplete");
                mController.ConvertSettingsPageModel(mSettingsModel, settings);
                //LoadStep(1);
            });
        }

        private void OnDownSettingsComplete(Settings settings)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                // mProgressDlg.Hide();
                // StepItems[0].Item_Click(null, null);
                MessageBox.Show("OnDownSettingsComplete");
            });
        }


    }
}
