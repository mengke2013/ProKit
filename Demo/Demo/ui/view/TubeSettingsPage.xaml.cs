using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading;

using log4net;

using Demo.ui.model;
using Demo.controller;

namespace Demo.ui.view
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

        public event Home.ClickHandler CloseClick;
        private ProgressDlg mProgressDlg;

        public TubeSettingsPage()
        {
            InitializeComponent();
            mProgressDlg = new ProgressDlg();
            mProgressDlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            mTubePageStyle = new TubePageStyle();
            mSettingsModel = new TubeSettingsViewModel();
            mController = new SettingsController(this);
        }

        public void LoadPage(byte selectedTube)
        {
            log.Debug("TubeRecipePage:LoadTubePage");
            mSelectedTube = selectedTube;

            Visibility = Visibility.Visible;

            mTubePageStyle.TextBoxWidth = this.ActualWidth / 25;
            mTubePageStyle.TextBoxHeight = this.ActualHeight / 35;

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

        public void UnloadPage(byte selectedTube)
        {
            Visibility = Visibility.Hidden;
            //            ClearValue(EffectProperty);
        }

        public TubeSettingsViewModel SettingsModel
        {
            get { return mSettingsModel; }
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
                Demo.utilities.Properties config = new Demo.utilities.Properties(openFileDialog.FileName);
                LoadConfig(config);
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
                Demo.utilities.Properties config = new Demo.utilities.Properties(saveFileDialog.FileName);
                SaveConfig(config);
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
                mController.ConvertSettingsModel(mSelectedTube);

                bool startDownload = mController.CommitSettings(mSelectedTube, OnDownSettingsComplete);
                if (startDownload)
                {
                    //mProgressDlg.ProgressModel.MaxValue = 64;
                    //mProgressDlg.ProgressModel.Progress = 0;
                    //mProgressDlg.ShowDialog();
                }
            }
        }

        private void OnSynSettingsComplete()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                //StepItems[0].Item_Click(null, null);
                //MessageBox.Show("OnSynSettingsComplete");
                mController.ConvertSettingsPageModel(mSelectedTube);
                //LoadStep(1);
            });
        }

        private void OnDownSettingsComplete()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                // mProgressDlg.Hide();
                // StepItems[0].Item_Click(null, null);
                MessageBox.Show("OnDownSettingsComplete");
            });
        }

        private void LoadConfig(Demo.utilities.Properties config)
        {
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
            mSettingsModel.Gas1Name = config.get("Gas1Name");
            mSettingsModel.Gas2Name = config.get("Gas2Name");
            mSettingsModel.Gas5Name = config.get("Gas5Name");
            mSettingsModel.Gas6Name = config.get("Gas6Name");
            mSettingsModel.Gas8Name = config.get("Gas8Name");
            mSettingsModel.Ana1Name = config.get("Ana1Name");
            mSettingsModel.Ana3Name = config.get("Ana3Name");
            mSettingsModel.Ana4Name = config.get("Ana4Name");
            mSettingsModel.Ana5Name = config.get("Ana5Name");
            mSettingsModel.Ana6Name = config.get("Ana6Name");

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

            mSettingsModel.EvName1 = config.get("EvName1");
            mSettingsModel.EvName2 = config.get("EvName2");
            mSettingsModel.EvName3 = config.get("EvName3");
            mSettingsModel.EvName4 = config.get("EvName4");
            mSettingsModel.EvName5 = config.get("EvName5");
            mSettingsModel.EvName6 = config.get("EvName6");
            mSettingsModel.EvName7 = config.get("EvName7");
            mSettingsModel.EvName8 = config.get("EvName8");
            mSettingsModel.EvName9 = config.get("EvName9");
            mSettingsModel.EvName10 = config.get("EvName10");
            mSettingsModel.EvName11 = config.get("EvName11");
            mSettingsModel.EvName12 = config.get("EvName12");
            mSettingsModel.EvName13 = config.get("EvName13");
            mSettingsModel.EvName14 = config.get("EvName14");
            mSettingsModel.EvName15 = config.get("EvName15");
            mSettingsModel.EvName16 = config.get("EvName16");
            mSettingsModel.EvName17 = config.get("EvName17");
            mSettingsModel.EvName18 = config.get("EvName18");
            mSettingsModel.EvName19 = config.get("EvName19");
            mSettingsModel.EvName20 = config.get("EvName20");
            mSettingsModel.EvName21 = config.get("EvName21");
            mSettingsModel.EvName22 = config.get("EvName22");
            mSettingsModel.EvName23 = config.get("EvName23");
            mSettingsModel.EvName24 = config.get("EvName24");
            mSettingsModel.EvName25 = config.get("EvName25");
            mSettingsModel.EvName26 = config.get("EvName26");
            mSettingsModel.EvName27 = config.get("EvName27");
            mSettingsModel.EvName28 = config.get("EvName28");
            mSettingsModel.EvName29 = config.get("EvName29");
            mSettingsModel.EvName30 = config.get("EvName30");
            mSettingsModel.EvName31 = config.get("EvName31");
            mSettingsModel.EvName32 = config.get("EvName32");
            mSettingsModel.DiName1 = config.get("DiName1");
            mSettingsModel.DiName2 = config.get("DiName2");
            mSettingsModel.DiName3 = config.get("DiName3");
            mSettingsModel.DiName4 = config.get("DiName4");
            mSettingsModel.DiName5 = config.get("DiName5");
            mSettingsModel.DiName6 = config.get("DiName6");
            mSettingsModel.DiName7 = config.get("DiName7");
            mSettingsModel.DiName8 = config.get("DiName8");
            mSettingsModel.DiName9 = config.get("DiName9");
            mSettingsModel.DiName10 = config.get("DiName10");
            mSettingsModel.DiName11 = config.get("DiName11");
            mSettingsModel.DiName12 = config.get("DiName12");
            mSettingsModel.DiName13 = config.get("DiName13");
            mSettingsModel.DiName14 = config.get("DiName14");
            mSettingsModel.DiName15 = config.get("DiName15");
            mSettingsModel.DiName16 = config.get("DiName16");
            mSettingsModel.DiName17 = config.get("DiName17");
            mSettingsModel.DiName18 = config.get("DiName18");
            mSettingsModel.DiName19 = config.get("DiName19");
            mSettingsModel.DiName20 = config.get("DiName20");
            mSettingsModel.DiName21 = config.get("DiName21");
            mSettingsModel.DiName22 = config.get("DiName22");
            mSettingsModel.DiName23 = config.get("DiName23");
            mSettingsModel.DiName24 = config.get("DiName24");
            mSettingsModel.DiName25 = config.get("DiName25");
            mSettingsModel.DiName26 = config.get("DiName26");
            mSettingsModel.DiName27 = config.get("DiName27");
            mSettingsModel.DiName28 = config.get("DiName28");
            mSettingsModel.DiName29 = config.get("DiName29");
            mSettingsModel.DiName30 = config.get("DiName30");
            mSettingsModel.DiName31 = config.get("DiName31");
            mSettingsModel.DiName32 = config.get("DiName32");
            mSettingsModel.DoName1 = config.get("DoName1");
            mSettingsModel.DoName2 = config.get("DoName2");
            mSettingsModel.DoName3 = config.get("DoName3");
            mSettingsModel.DoName4 = config.get("DoName4");
            mSettingsModel.DoName5 = config.get("DoName5");
            mSettingsModel.DoName6 = config.get("DoName6");
            mSettingsModel.DoName7 = config.get("DoName7");
            mSettingsModel.DoName8 = config.get("DoName8");
            mSettingsModel.DoName9 = config.get("DoName9");
            mSettingsModel.DoName10 = config.get("DoName10");
            mSettingsModel.DoName11 = config.get("DoName11");
            mSettingsModel.DoName12 = config.get("DoName12");
            mSettingsModel.DoName13 = config.get("DoName13");
            mSettingsModel.DoName14 = config.get("DoName14");
            mSettingsModel.DoName15 = config.get("DoName15");
            mSettingsModel.DoName16 = config.get("DoName16");

            int value1;
            int.TryParse(config.get("Di"), out value1);
            mSettingsModel.Di = value1;
            int.TryParse(config.get("Offset"), out value1);
            mSettingsModel.Offset = value1;
            int.TryParse(config.get("PositionDev"), out value1);
            mSettingsModel.PositionDev = value1;
            int.TryParse(config.get("ClosePosition"), out value1);
            mSettingsModel.ClosePosition = value1;
        }

        private void SaveConfig(Demo.utilities.Properties config)
        {
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
            config.set("Gas1Name", mSettingsModel.Gas1Name);
            config.set("Gas2Name", mSettingsModel.Gas2Name);
            config.set("Gas5Name", mSettingsModel.Gas5Name);
            config.set("Gas6Name", mSettingsModel.Gas6Name);
            config.set("Gas8Name", mSettingsModel.Gas8Name);
            config.set("Ana1Name", mSettingsModel.Ana1Name);
            config.set("Ana3Name", mSettingsModel.Ana3Name);
            config.set("Ana4Name", mSettingsModel.Ana4Name);
            config.set("Ana5Name", mSettingsModel.Ana5Name);
            config.set("Ana6Name", mSettingsModel.Ana6Name);
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
            config.set("EvName1", mSettingsModel.EvName1);
            config.set("EvName2", mSettingsModel.EvName2);
            config.set("EvName3", mSettingsModel.EvName3);
            config.set("EvName4", mSettingsModel.EvName4);
            config.set("EvName5", mSettingsModel.EvName5);
            config.set("EvName6", mSettingsModel.EvName6);
            config.set("EvName7", mSettingsModel.EvName7);
            config.set("EvName8", mSettingsModel.EvName8);
            config.set("EvName9", mSettingsModel.EvName9);
            config.set("EvName10", mSettingsModel.EvName10);
            config.set("EvName11", mSettingsModel.EvName11);
            config.set("EvName12", mSettingsModel.EvName12);
            config.set("EvName13", mSettingsModel.EvName13);
            config.set("EvName14", mSettingsModel.EvName14);
            config.set("EvName15", mSettingsModel.EvName15);
            config.set("EvName16", mSettingsModel.EvName16);
            config.set("EvName17", mSettingsModel.EvName17);
            config.set("EvName18", mSettingsModel.EvName18);
            config.set("EvName19", mSettingsModel.EvName19);
            config.set("EvName20", mSettingsModel.EvName20);
            config.set("EvName21", mSettingsModel.EvName21);
            config.set("EvName22", mSettingsModel.EvName22);
            config.set("EvName23", mSettingsModel.EvName23);
            config.set("EvName24", mSettingsModel.EvName24);
            config.set("EvName25", mSettingsModel.EvName25);
            config.set("EvName26", mSettingsModel.EvName26);
            config.set("EvName27", mSettingsModel.EvName27);
            config.set("EvName28", mSettingsModel.EvName28);
            config.set("EvName29", mSettingsModel.EvName29);
            config.set("EvName30", mSettingsModel.EvName30);
            config.set("EvName31", mSettingsModel.EvName31);
            config.set("EvName32", mSettingsModel.EvName32);
            config.set("DiName1", mSettingsModel.DiName1);
            config.set("DiName2", mSettingsModel.DiName2);
            config.set("DiName3", mSettingsModel.DiName3);
            config.set("DiName4", mSettingsModel.DiName4);
            config.set("DiName5", mSettingsModel.DiName5);
            config.set("DiName6", mSettingsModel.DiName6);
            config.set("DiName7", mSettingsModel.DiName7);
            config.set("DiName8", mSettingsModel.DiName8);
            config.set("DiName9", mSettingsModel.DiName9);
            config.set("DiName10", mSettingsModel.DiName10);
            config.set("DiName11", mSettingsModel.DiName11);
            config.set("DiName12", mSettingsModel.DiName12);
            config.set("DiName13", mSettingsModel.DiName13);
            config.set("DiName14", mSettingsModel.DiName14);
            config.set("DiName15", mSettingsModel.DiName15);
            config.set("DiName16", mSettingsModel.DiName16);
            config.set("DiName17", mSettingsModel.DiName17);
            config.set("DiName18", mSettingsModel.DiName18);
            config.set("DiName19", mSettingsModel.DiName19);
            config.set("DiName20", mSettingsModel.DiName20);
            config.set("DiName21", mSettingsModel.DiName21);
            config.set("DiName22", mSettingsModel.DiName22);
            config.set("DiName23", mSettingsModel.DiName23);
            config.set("DiName24", mSettingsModel.DiName24);
            config.set("DiName25", mSettingsModel.DiName25);
            config.set("DiName26", mSettingsModel.DiName26);
            config.set("DiName27", mSettingsModel.DiName27);
            config.set("DiName28", mSettingsModel.DiName28);
            config.set("DiName29", mSettingsModel.DiName29);
            config.set("DiName30", mSettingsModel.DiName30);
            config.set("DiName31", mSettingsModel.DiName31);
            config.set("DiName32", mSettingsModel.DiName32);
            config.set("DoName1", mSettingsModel.DoName1);
            config.set("DoName2", mSettingsModel.DoName2);
            config.set("DoName3", mSettingsModel.DoName3);
            config.set("DoName4", mSettingsModel.DoName4);
            config.set("DoName5", mSettingsModel.DoName5);
            config.set("DoName6", mSettingsModel.DoName6);
            config.set("DoName7", mSettingsModel.DoName7);
            config.set("DoName8", mSettingsModel.DoName8);
            config.set("DoName9", mSettingsModel.DoName9);
            config.set("DoName10", mSettingsModel.DoName10);
            config.set("DoName11", mSettingsModel.DoName11);
            config.set("DoName12", mSettingsModel.DoName12);
            config.set("DoName13", mSettingsModel.DoName13);
            config.set("DoName14", mSettingsModel.DoName14);
            config.set("DoName15", mSettingsModel.DoName15);
            config.set("DoName16", mSettingsModel.DoName16);
            config.Save();
        }
    }
}
