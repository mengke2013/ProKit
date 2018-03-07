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
using System.Windows.Shapes;
using log4net;
using Rocky.Core.Opc.Ua;
using Demo.com;
using System.Threading;
using System.Windows.Media.Effects;
using Demo.ui.model;
using Demo.ui.view;
using System.ComponentModel;
using Demo.service;

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private byte mSelectedTube = 0;

        private byte[] tubePageIndexes = { 1, 1, 1, 1, 1, 1 };
        private string[] tubePageTitleLabels = { "Monitor", "Trend", "Recipe", "Report", "Events" };
        private ITubePage[] tubePages = new ITubePage[5];
        private Button[] TubeTabHeaders = new Button[5];

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
            tubePages[4] = new TubeEventsPageAdapter(tubeEventsPage);
            TubeTabHeaders[0] = TubeTabHeaderMonitor;
            TubeTabHeaders[1] = TubeTabHeaderTrend;
            TubeTabHeaders[2] = TubeTabHeaderRecipe;
            TubeTabHeaders[3] = TubeTabHeaderSettings;
            TubeTabHeaders[4] = TubeTabHeaderEvents;

            //tubeControlBar.CloseClick += new TubeControlBar.ClickHandler(bdMainClose_Click);
            //tubeControlBar.SettingsClick += new TubeControlBar.ClickHandler(bdMainSettings_Click);
            tubeMonitorPage.CloseClick += new TubeControlBar.ClickHandler(bdMainClose_Click);
            tubeMonitorPage.SettingsClick += new TubeControlBar.ClickHandler(bdMainSettings_Click);
            tubeRecipePage.CloseClick += new TubeControlBar.ClickHandler(bdMainClose_Click);
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
                tubeInfoItems[i].StartUpdateUIServer();
                tubeInfoItems[i].ItemMode.FurnaceHeight = (int)(tubeInfoItems[i].ActualHeight - 10) / 5 * 3;
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
                tubeInfoItems[i].ItemMode.FurnaceHeight = (int)(tubeInfoItems[i].ActualHeight - 10) / 10 * 3;
            }
        }

        private void Item_Select_Click(object sender, RoutedEventArgs e, byte tubeIndex)
        {
            log.Debug("Tube Index " + tubeIndex);
            //TubeWindow tubeWindow = new TubeWindow(2);
            //tubeWindow.Show();
            bdMainPanel.Height = this.ActualHeight - 130;
            bdMainPanel.Width = this.ActualWidth - 190;
            
            mSelectedTube = tubeIndex;
            for (byte i = 0; i < tubeInfoItems.Length; ++i)
            {
                if (i != tubeIndex - 1)
                {
                    //tubeInfoItems[i].Background = new SolidColorBrush(Colors.White);
                    tubeInfoItems[i].Background = new SolidColorBrush(Colors.WhiteSmoke);
                    tubeInfoItems[i].ClearValue(EffectProperty);
                    tubeInfoItems[i].Effect = new BlurEffect
                    {
                        Radius = 5
                    };
                }
            }

            bd1.Height = tubeInfoItems[tubeIndex - 1].ActualHeight - 12;
            bd1.Margin = new Thickness(130, 6 + (bd1.Height + 12) * (tubeIndex-1), 0, 0);
            bd1.Visibility = Visibility.Visible;
            tubeInfoItems[tubeIndex - 1].Background = new SolidColorBrush(Colors.White);
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

//            ComNodeHelper.Instance.ReadOpcNodes(2);
            TubeTabHeader.Visibility = Visibility.Visible;
            bd0.Visibility = Visibility.Visible;

            ShowActivedTubePage();
            // MessageBox.Show("Step" + stepIndex);
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

            for (byte i = 0; i < tubeInfoItems.Length; ++i)
            {
                tubeInfoItems[i].ClearValue(EffectProperty);
            }
            
            TubeTabHeader.Visibility = Visibility.Hidden;
            bd0.Visibility = Visibility.Hidden;
            bd1.Visibility = Visibility.Hidden;
        }

        private void bdMainSettings_Click(object sender, RoutedEventArgs e)
        {
            TubeSettingsDialog settingsDlg = new TubeSettingsDialog(mSelectedTube);
            settingsDlg.Height = this.ActualHeight / 3 * 2;
            settingsDlg.Width = this.ActualWidth / 3 * 2;
            settingsDlg.Owner = this;
            settingsDlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settingsDlg.VerticalAlignment = VerticalAlignment.Center;
            settingsDlg.ShowDialog();
        } 

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

        private void btnEventsClick(object sender, RoutedEventArgs e)
        {
            tubePageIndexes[mSelectedTube - 1] = 5;
            ShowActivedTubePage();
        }

        private void ShowActivedTubePage()
        {
            bd0.Margin = new Thickness(381 + (tubePageIndexes[mSelectedTube - 1] - 1) * 85, 42, 0, 0);
            DisableAllTubePages();
            tubePages[tubePageIndexes[mSelectedTube - 1] - 1].UI().Visibility = Visibility.Visible;
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

        private void DisableAllTubePages()
        {
            tubeMonitorPage.Visibility = Visibility.Hidden;
            tubeTrendPage.Visibility = Visibility.Hidden;
            tubeRecipePage.Visibility = Visibility.Hidden;
            tubeSettingsPage.Visibility = Visibility.Hidden;
            tubeEventsPage.Visibility = Visibility.Hidden;
            TubeTabHeaderMonitor.ClearValue(EffectProperty);
            TubeTabHeaderTrend.ClearValue(EffectProperty);
            TubeTabHeaderRecipe.ClearValue(EffectProperty);
            TubeTabHeaderSettings.ClearValue(EffectProperty);
            TubeTabHeaderEvents.ClearValue(EffectProperty);
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
