using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.ComponentModel;

using log4net;

using Demo.ui.model;
using Demo.ui.view;
using Demo.com;

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void ClickHandler(object sender, RoutedEventArgs e);

        private byte mSelectedTube = 0;

        private byte[] tubePageIndexes = { 1, 1, 1, 1, 1, 1 };
        private string[] tubePageTitleLabels = { "Monitor", "Trend", "Recipe", "Config", "Alarm", "Events" };
        private ITubePage[] tubePages = new ITubePage[6];
        private Button[] TubeTabHeaders = new Button[6];

        private HomePageModel mViewModel;
        private TubeInfoItem[] tubeInfoItems;

        public Home()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            RegestDataContext();

            tubePages[0] = new TubeMonitorPageAdapter(tubeMonitorPage);
            tubePages[1] = new TubeTrendPageAdapter(tubeTrendPage);
            tubePages[2] = new TubeRecipePageAdapter(tubeRecipePage);
            tubePages[3] = new TubeSettingsPageAdapter(tubeSettingsPage);
            tubePages[4] = new TubeAlarmPageAdapter(tubeAlarmPage);
            tubePages[5] = new TubeEventsPageAdapter(tubeEventsPage);
            TubeTabHeaders[0] = TubeTabHeaderMonitor;
            TubeTabHeaders[1] = TubeTabHeaderTrend;
            TubeTabHeaders[2] = TubeTabHeaderRecipe;
            TubeTabHeaders[3] = TubeTabHeaderSettings;
            TubeTabHeaders[4] = TubeTabHeaderAlarm;
            TubeTabHeaders[5] = TubeTabHeaderEvents;

            //tubeControlBar.CloseClick += new TubeControlBar.ClickHandler(bdMainClose_Click);
            //tubeControlBar.SettingsClick += new TubeControlBar.ClickHandler(bdMainSettings_Click);
            //tubeMonitorPage.SettingsClick += new TubeControlBar.ClickHandler(bdMainSettings_Click);
            tubeMonitorPage.CloseClick += new ClickHandler(bdMainClose_Click);
            tubeTrendPage.CloseClick += new ClickHandler(bdMainClose_Click);
            tubeRecipePage.CloseClick += new ClickHandler(bdMainClose_Click);
            tubeSettingsPage.CloseClick += new ClickHandler(bdMainClose_Click);
            tubeAlarmPage.CloseClick += new ClickHandler(bdMainClose_Click);
            this.DataContext = this;

            mViewModel = new HomePageModel();
            tubeInfoItems = new TubeInfoItem[6];
            tubeInfoItems[0] = tubeInfoItem1;
            tubeInfoItems[1] = tubeInfoItem2;
            tubeInfoItems[2] = tubeInfoItem3;
            tubeInfoItems[3] = tubeInfoItem4;
            tubeInfoItems[4] = tubeInfoItem5;
            tubeInfoItems[5] = tubeInfoItem6;

            for (byte i = 0; i < tubeInfoItems.Length; ++i)
            {
                tubeInfoItems[i].ItemMode = mViewModel.TubeInfoItems[i];
                tubeInfoItems[i].ItemClick += new TubeInfoItem.ClickHandler(Item_Select_Click);
                tubeInfoItems[i].WarningClick += new TubeInfoItem.ClickHandler(Item_Warning_Click);
                tubeInfoItems[i].StartUpdateUIServer();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            for (byte i = 0; i < tubeInfoItems.Length; ++i)
            {
                tubeInfoItems[i].SetViewVisible(true);
                tubeInfoItems[i].ItemMode.FurnaceHeight = (int)(tubeInfoItems[i].ActualHeight - 10) / 10 * 3;
            }
        }

        private void Item_Select_Click(object sender, RoutedEventArgs e, byte tubeIndex)
        {
            log.Debug("Tube Index " + tubeIndex);
            //TubeWindow tubeWindow = new TubeWindow(2);
            //tubeWindow.Show();
            bdMainPanel.Height = this.ActualHeight - 130;
            bdMainPanel.Width = this.ActualWidth -190;
            this.UpdateLayout();
            
            mSelectedTube = tubeIndex;
            for (byte i = 0; i < tubeInfoItems.Length; ++i)
            {
                tubeInfoItems[i].SetViewVisible(false);
                if (i != tubeIndex - 1)
                {
                    tubeInfoItems[i].ClearValue(EffectProperty);
                    tubeInfoItems[i].ItemMode.TabBackground = "#FFD3C7C7";
                }
            }

            bd1.Height = tubeInfoItems[tubeIndex - 1].ActualHeight - 12;
            bd1.Margin = new Thickness(130, 6 + (bd1.Height + 12) * (tubeIndex-1), 0, 0);
            bd1.Visibility = Visibility.Visible;
            tubeInfoItems[tubeIndex - 1].ItemMode.TabBackground = "white";
            tubeInfoItems[tubeIndex - 1].Effect = new DropShadowEffect
            {
                Color = new Color { A = 255, R = 0, G = 0, B = 0 },
                Direction = 315,
                ShadowDepth = 5,
                BlurRadius = 5,
                RenderingBias = RenderingBias.Performance,
                Opacity = 100
            };
            bdMainPanel.Visibility = Visibility.Visible;

            TubeTabHeader.Visibility = Visibility.Visible;
            bd0.Visibility = Visibility.Visible;

            ShowActivedTubePage();
        }

        private void Item_Warning_Click(object sender, RoutedEventArgs e, byte tubeIndex)
        {
            log.Debug("Tube Index " + tubeIndex);
            //TubeWindow tubeWindow = new TubeWindow(2);
            //tubeWindow.Show();
            //MessageBox.Show("Warning popup");
            tubePageIndexes[tubeIndex - 1] = 5;

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("Would you want to exit?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //ProcessService.Instance.EndPullInfoService();
            }
            else
            {
                // if you want to stop it, set e.Cancel = true
                e.Cancel = true;
            }

        }

        protected override void OnClosed(EventArgs e)
        {
            //Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            //if (this.IsAfreshLogin == true) return;

            base.OnClosed(e);
            Application.Current.Shutdown();

        }

        public object TextBoxWidth
        {
            get { return 20; }
        }

        private void RegestDataContext()
        {
       
        }

        private void bdMainClose_Click(object sender, RoutedEventArgs e)
        {
            bdMainPanel.Visibility = Visibility.Hidden;

            tubeInfoItems[mSelectedTube-1].ClearValue(EffectProperty);
            for (byte i = 0; i < tubeInfoItems.Length; ++i)
            {
                tubeInfoItems[i].SetViewVisible(true);
                tubeInfoItems[i].ItemMode.TabBackground = "white";
            }
            
            TubeTabHeader.Visibility = Visibility.Hidden;
            bd0.Visibility = Visibility.Hidden;
            bd1.Visibility = Visibility.Hidden;
        }

        /*private void bdMainSettings_Click(object sender, RoutedEventArgs e)
        {
            TubeSettingsDialog settingsDlg = new TubeSettingsDialog(mSelectedTube);
            settingsDlg.Height = this.ActualHeight / 3 * 2;
            settingsDlg.Width = this.ActualWidth / 3 * 2;
            settingsDlg.Owner = this;
            settingsDlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settingsDlg.VerticalAlignment = VerticalAlignment.Center;
            settingsDlg.ShowDialog();
        } */

        private void btnMonitorClick(object sender, RoutedEventArgs e)
        {
            tubePageIndexes[mSelectedTube - 1] = 1;
            ShowActivedTubePage();
        }

        private void btnTrendClick(object sender, RoutedEventArgs e)
        {
            tubePageIndexes[mSelectedTube - 1] = 2;
            ShowActivedTubePage();
        }

        private void btnRecipeClick(object sender, RoutedEventArgs e)
        {
            tubePageIndexes[mSelectedTube - 1] = 3;
            ShowActivedTubePage();
        }

        private void btnSettingsClick(object sender, RoutedEventArgs e)
        {
            tubePageIndexes[mSelectedTube - 1] = 4;
            ShowActivedTubePage();
        }

        private void btnAlarmClick(object sender, RoutedEventArgs e)
        {
            tubePageIndexes[mSelectedTube - 1] = 5;
            ShowActivedTubePage();
        }

        private void btnEventsClick(object sender, RoutedEventArgs e)
        {
            tubePageIndexes[mSelectedTube - 1] = 6;
            ShowActivedTubePage();
        }

        private void ShowActivedTubePage()
        {
            bd0.Margin = new Thickness(381 + (tubePageIndexes[mSelectedTube - 1] - 1) * 85, 42, 0, 0);

            for (int i = 0; i < tubePages.Length; ++i)
            {
                if (tubePageIndexes[mSelectedTube - 1] != i + 1)
                {
                    tubePages[i].UnloadPage((byte)(i+1));
                }
            }

            tubePageTitle.Text = tubePageTitleLabels[tubePageIndexes[mSelectedTube - 1] - 1];
            TubeTabHeaders[tubePageIndexes[mSelectedTube - 1] - 1].Effect = new DropShadowEffect
            {
                Color = new Color { A = 255, R = 0, G = 0, B = 0 },
                Direction = 315,
                ShadowDepth = 5,
                BlurRadius = 5,
                RenderingBias = RenderingBias.Performance,
                Opacity = 100
            };
            tubePages[tubePageIndexes[mSelectedTube - 1] - 1].LoadPage(mSelectedTube);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            ComNodeService.Instance.Connect();
        }

        private void btnReconnect_Click(object sender, RoutedEventArgs e)
        {
            ComNodeService.Instance.Reconnect();
        }
    }
}
