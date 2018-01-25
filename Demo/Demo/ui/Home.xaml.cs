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

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private TubeMonitorPageModel mTubeMonitorPageModel;
        public Home()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            mTubeMonitorPageModel = new TubeMonitorPageModel();
            mTubeMonitorPageModel.SelectedTube = 3;
            tubeMonitorPage.DataContext = mTubeMonitorPageModel;

            this.bdMainPanel.Height = this.Height-130;
            this.bdMainPanel.Width = this.Width - 90;
            RegestDataContext();
        }

        private void RegestDataContext()
        {
            txtBlkTube1Gas1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].MfcNodeComponent.GasNodeComponents[0].CurMeas);
            txtBlkTube1Gas2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].MfcNodeComponent.GasNodeComponents[1].CurMeas);
            txtBlkTube1Gas5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].MfcNodeComponent.GasNodeComponents[3].CurMeas);
            txtBlkTube1Gas6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].MfcNodeComponent.GasNodeComponents[4].CurMeas);
            txtBlkTube1Gas8Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].MfcNodeComponent.GasNodeComponents[7].CurMeas);
            txtBlkTube2Gas1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].MfcNodeComponent.GasNodeComponents[0].CurMeas);
            txtBlkTube2Gas2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].MfcNodeComponent.GasNodeComponents[1].CurMeas);
            txtBlkTube2Gas5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].MfcNodeComponent.GasNodeComponents[3].CurMeas);
            txtBlkTube2Gas6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].MfcNodeComponent.GasNodeComponents[4].CurMeas);
            txtBlkTube2Gas8Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].MfcNodeComponent.GasNodeComponents[7].CurMeas);
            txtBlkTube3Gas1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].MfcNodeComponent.GasNodeComponents[0].CurMeas);
            txtBlkTube3Gas2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].MfcNodeComponent.GasNodeComponents[1].CurMeas);
            txtBlkTube3Gas5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].MfcNodeComponent.GasNodeComponents[3].CurMeas);
            txtBlkTube3Gas6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].MfcNodeComponent.GasNodeComponents[4].CurMeas);
            txtBlkTube3Gas8Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].MfcNodeComponent.GasNodeComponents[7].CurMeas);

            txtBlkTube1Ana1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas);
            txtBlkTube1Ana3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas);
            txtBlkTube1Ana4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas);
            txtBlkTube1Ana5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].VacuumNodeComponent.AnalogNodeComponents[4].CurMeas);
            txtBlkTube1Ana6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].VacuumNodeComponent.AnalogNodeComponents[5].CurMeas);
            txtBlkTube2Ana1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas);
            txtBlkTube2Ana3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas);
            txtBlkTube2Ana4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas);
            txtBlkTube2Ana5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].VacuumNodeComponent.AnalogNodeComponents[4].CurMeas);
            txtBlkTube2Ana6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].VacuumNodeComponent.AnalogNodeComponents[5].CurMeas);
            txtBlkTube3Ana1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas);
            txtBlkTube3Ana3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas);
            txtBlkTube3Ana4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas);
            txtBlkTube3Ana5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].VacuumNodeComponent.AnalogNodeComponents[4].CurMeas);
            txtBlkTube3Ana6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].VacuumNodeComponent.AnalogNodeComponents[5].CurMeas);

            txtBlkTube1Zone1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].FurnaceNodeComponent.TemperNodeComponents[0].IntValue);
            txtBlkTube1Zone2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].FurnaceNodeComponent.TemperNodeComponents[1].IntValue);
            txtBlkTube1Zone3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].FurnaceNodeComponent.TemperNodeComponents[2].IntValue);
            txtBlkTube1Zone4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].FurnaceNodeComponent.TemperNodeComponents[3].IntValue);
            txtBlkTube1Zone5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].FurnaceNodeComponent.TemperNodeComponents[4].IntValue);
            txtBlkTube1Zone6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[0].FurnaceNodeComponent.TemperNodeComponents[5].IntValue);
            txtBlkTube2Zone1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].FurnaceNodeComponent.TemperNodeComponents[0].IntValue);
            txtBlkTube2Zone2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].FurnaceNodeComponent.TemperNodeComponents[1].IntValue);
            txtBlkTube2Zone3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].FurnaceNodeComponent.TemperNodeComponents[2].IntValue);
            txtBlkTube2Zone4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].FurnaceNodeComponent.TemperNodeComponents[3].IntValue);
            txtBlkTube2Zone5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].FurnaceNodeComponent.TemperNodeComponents[4].IntValue);
            txtBlkTube2Zone6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[1].FurnaceNodeComponent.TemperNodeComponents[5].IntValue);
            txtBlkTube3Zone1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].FurnaceNodeComponent.TemperNodeComponents[0].IntValue);
            txtBlkTube3Zone2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].FurnaceNodeComponent.TemperNodeComponents[1].IntValue);
            txtBlkTube3Zone3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].FurnaceNodeComponent.TemperNodeComponents[2].IntValue);
            txtBlkTube3Zone4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].FurnaceNodeComponent.TemperNodeComponents[3].IntValue);
            txtBlkTube3Zone5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].FurnaceNodeComponent.TemperNodeComponents[4].IntValue);
            txtBlkTube3Zone6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[2].FurnaceNodeComponent.TemperNodeComponents[5].IntValue);
            
            txtBlkTube4Gas1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].MfcNodeComponent.GasNodeComponents[0].CurMeas);
            txtBlkTube4Gas2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].MfcNodeComponent.GasNodeComponents[1].CurMeas);
            txtBlkTube4Gas5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].MfcNodeComponent.GasNodeComponents[3].CurMeas);
            txtBlkTube4Gas6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].MfcNodeComponent.GasNodeComponents[4].CurMeas);
            txtBlkTube4Gas8Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].MfcNodeComponent.GasNodeComponents[7].CurMeas);
            txtBlkTube5Gas1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].MfcNodeComponent.GasNodeComponents[0].CurMeas);
            txtBlkTube5Gas2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].MfcNodeComponent.GasNodeComponents[1].CurMeas);
            txtBlkTube5Gas5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].MfcNodeComponent.GasNodeComponents[3].CurMeas);
            txtBlkTube5Gas6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].MfcNodeComponent.GasNodeComponents[4].CurMeas);
            txtBlkTube5Gas8Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].MfcNodeComponent.GasNodeComponents[7].CurMeas);
            txtBlkTube6Gas1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].MfcNodeComponent.GasNodeComponents[0].CurMeas);
            txtBlkTube6Gas2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].MfcNodeComponent.GasNodeComponents[1].CurMeas);
            txtBlkTube6Gas5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].MfcNodeComponent.GasNodeComponents[3].CurMeas);
            txtBlkTube6Gas6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].MfcNodeComponent.GasNodeComponents[4].CurMeas);
            txtBlkTube6Gas8Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].MfcNodeComponent.GasNodeComponents[7].CurMeas);

            txtBlkTube4Ana1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas);
            txtBlkTube4Ana3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas);
            txtBlkTube4Ana4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas);
            txtBlkTube4Ana5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].VacuumNodeComponent.AnalogNodeComponents[4].CurMeas);
            txtBlkTube4Ana6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].VacuumNodeComponent.AnalogNodeComponents[5].CurMeas);
            txtBlkTube5Ana1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas);
            txtBlkTube5Ana3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas);
            txtBlkTube5Ana4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas);
            txtBlkTube5Ana5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].VacuumNodeComponent.AnalogNodeComponents[4].CurMeas);
            txtBlkTube5Ana6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].VacuumNodeComponent.AnalogNodeComponents[5].CurMeas);
            txtBlkTube6Ana1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas);
            txtBlkTube6Ana3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas);
            txtBlkTube6Ana4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas);
            txtBlkTube6Ana5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].VacuumNodeComponent.AnalogNodeComponents[4].CurMeas);
            txtBlkTube6Ana6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].VacuumNodeComponent.AnalogNodeComponents[5].CurMeas);

            txtBlkTube4Zone1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].FurnaceNodeComponent.TemperNodeComponents[0].IntValue);
            txtBlkTube4Zone2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].FurnaceNodeComponent.TemperNodeComponents[1].IntValue);
            txtBlkTube4Zone3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].FurnaceNodeComponent.TemperNodeComponents[2].IntValue);
            txtBlkTube4Zone4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].FurnaceNodeComponent.TemperNodeComponents[3].IntValue);
            txtBlkTube4Zone5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].FurnaceNodeComponent.TemperNodeComponents[4].IntValue);
            txtBlkTube4Zone6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[3].FurnaceNodeComponent.TemperNodeComponents[5].IntValue);
            txtBlkTube5Zone1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].FurnaceNodeComponent.TemperNodeComponents[0].IntValue);
            txtBlkTube5Zone2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].FurnaceNodeComponent.TemperNodeComponents[1].IntValue);
            txtBlkTube5Zone3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].FurnaceNodeComponent.TemperNodeComponents[2].IntValue);
            txtBlkTube5Zone4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].FurnaceNodeComponent.TemperNodeComponents[3].IntValue);
            txtBlkTube5Zone5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].FurnaceNodeComponent.TemperNodeComponents[4].IntValue);
            txtBlkTube5Zone6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[4].FurnaceNodeComponent.TemperNodeComponents[5].IntValue);
            txtBlkTube6Zone1Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].FurnaceNodeComponent.TemperNodeComponents[0].IntValue);
            txtBlkTube6Zone2Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].FurnaceNodeComponent.TemperNodeComponents[1].IntValue);
            txtBlkTube6Zone3Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].FurnaceNodeComponent.TemperNodeComponents[2].IntValue);
            txtBlkTube6Zone4Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].FurnaceNodeComponent.TemperNodeComponents[3].IntValue);
            txtBlkTube6Zone5Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].FurnaceNodeComponent.TemperNodeComponents[4].IntValue);
            txtBlkTube6Zone6Int.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[5].FurnaceNodeComponent.TemperNodeComponents[5].IntValue);
        }

        private void bdMainClose_Click(object sender, RoutedEventArgs e)
        {
            bdMainPanel.Visibility = Visibility.Hidden;
            DisableAllTabs();
            borderTube1.ClearValue(EffectProperty);
            borderTube2.ClearValue(EffectProperty);
            borderTube3.ClearValue(EffectProperty);
            borderTube4.ClearValue(EffectProperty);
            borderTube5.ClearValue(EffectProperty);
            borderTube6.ClearValue(EffectProperty);
            TubeTabHeader.Visibility = Visibility.Hidden;
            bd0.Visibility = Visibility.Hidden;
        }

        private void btnTube1_Click(object sender, RoutedEventArgs e)
        {
            //TubeWindow tubeWindow = new TubeWindow(1);
           // tubeWindow.Show();
            //TestControls testControls = new TestControls();
            //testControls.Show();
            this.bd1.Margin = new Thickness(130, 1, 0, 0);

            DisableAllTabs();
            borderTube1.Background = new SolidColorBrush(Colors.White);
            borderTube1.Effect = new DropShadowEffect
            {
                Color = new Color { A = 255, R = 0, G = 0, B = 0 },
                Direction = 315,
                ShadowDepth = 5,
                BlurRadius = 5,
                RenderingBias = RenderingBias.Performance,
                Opacity = 100
            };
            bdMainPanel.Visibility = Visibility.Visible;

            mTubeMonitorPageModel.SelectedTube = 1;
            mTubeMonitorPageModel.UpdateDataSource();
            ComNodeHelper.Instance.ReadOpcNodes(1);
            TubeTabHeader.Visibility = Visibility.Visible;
            bd0.Visibility = Visibility.Visible;
        }

        private void btnTube2_Click(object sender, RoutedEventArgs e)
        {
            //TubeWindow tubeWindow = new TubeWindow(2);
            //tubeWindow.Show();
            this.bd1.Margin = new Thickness(130,123,0,0);
            DisableAllTabs();
            borderTube2.Background = new SolidColorBrush(Colors.White);
            borderTube2.Effect = new DropShadowEffect
            {
                Color = new Color { A = 255, R = 0, G = 0, B = 0 },
                Direction = 315,
                ShadowDepth = 5,
                BlurRadius = 5,
                RenderingBias = RenderingBias.Performance,
                Opacity = 100
            };
            bdMainPanel.Visibility = Visibility.Visible;

            mTubeMonitorPageModel.SelectedTube = 2;
            mTubeMonitorPageModel.UpdateDataSource();
            ComNodeHelper.Instance.ReadOpcNodes(2);
            TubeTabHeader.Visibility = Visibility.Visible;
            bd0.Visibility = Visibility.Visible;
        }

        private void btnTube3_Click(object sender, RoutedEventArgs e)
        {
            //TubeWindow tubeWindow = new TubeWindow(3);
            //tubeWindow.Show();
            this.bd1.Margin = new Thickness(130, 245, 0, 0);
            DisableAllTabs();
            borderTube3.Background = new SolidColorBrush(Colors.White);
            borderTube3.Effect = new DropShadowEffect
            {
                Color = new Color { A = 255, R = 0, G = 0, B = 0 },
                Direction = 315,
                ShadowDepth = 5,
                BlurRadius = 5,
                RenderingBias = RenderingBias.Performance,
                Opacity = 100
            };
            bdMainPanel.Visibility = Visibility.Visible;

            mTubeMonitorPageModel.SelectedTube = 3;
            mTubeMonitorPageModel.UpdateDataSource();
            ComNodeHelper.Instance.ReadOpcNodes(3);
            TubeTabHeader.Visibility = Visibility.Visible;
            bd0.Visibility = Visibility.Visible;
        }

        private void btnTube4_Click(object sender, RoutedEventArgs e)
        {
            this.bd1.Margin = new Thickness(130, 367, 0, 0);
            DisableAllTabs();
            borderTube4.Background = new SolidColorBrush(Colors.White);
            borderTube4.Effect = new DropShadowEffect
            {
                Color = new Color { A = 255, R = 0, G = 0, B = 0 },
                Direction = 315,
                ShadowDepth = 5,
                BlurRadius = 5,
                RenderingBias = RenderingBias.Performance,
                Opacity = 100
            };
            bdMainPanel.Visibility = Visibility.Visible;

            mTubeMonitorPageModel.SelectedTube = 4;
            mTubeMonitorPageModel.UpdateDataSource();
            ComNodeHelper.Instance.ReadOpcNodes(4);
            TubeTabHeader.Visibility = Visibility.Visible;
            bd0.Visibility = Visibility.Visible;
        }

        private void btnTube5_Click(object sender, RoutedEventArgs e)
        {
            this.bd1.Margin = new Thickness(130, 489, 0, 0);
            DisableAllTabs();
            borderTube5.Background = new SolidColorBrush(Colors.White);
            borderTube5.Effect = new DropShadowEffect
            {
                Color = new Color { A = 255, R = 0, G = 0, B = 0 },
                Direction = 315,
                ShadowDepth = 5,
                BlurRadius = 5,
                RenderingBias = RenderingBias.Performance,
                Opacity = 100
            };
            bdMainPanel.Visibility = Visibility.Visible;

            mTubeMonitorPageModel.SelectedTube = 5;
            mTubeMonitorPageModel.UpdateDataSource();
            ComNodeHelper.Instance.ReadOpcNodes(5);
            TubeTabHeader.Visibility = Visibility.Visible;
            bd0.Visibility = Visibility.Visible;
        }

        private void btnTube6_Click(object sender, RoutedEventArgs e)
        {
            this.bd1.Margin = new Thickness(130, 611, 0, 0);
            DisableAllTabs();
            borderTube6.Background = new SolidColorBrush(Colors.White);
            borderTube6.Effect = new DropShadowEffect
            {
                Color = new Color { A = 255, R = 0, G = 0, B = 0 },
                Direction = 315,
                ShadowDepth = 5,
                BlurRadius = 5,
                RenderingBias = RenderingBias.Performance,
                Opacity = 100
            };
            bdMainPanel.Visibility = Visibility.Visible;

            mTubeMonitorPageModel.SelectedTube = 6;
            mTubeMonitorPageModel.UpdateDataSource();
            ComNodeHelper.Instance.ReadOpcNodes(6);
            TubeTabHeader.Visibility = Visibility.Visible;
            bd0.Visibility = Visibility.Visible;
        }

        private void btnMonitorClick(object sender, RoutedEventArgs e)
        {
            bd0.Margin = new Thickness(330, 42, 0, 0);
            DisableAllTubePages();
            tubeMonitorPage.Visibility = Visibility.Visible;
            tubePageTitle.Text = "Monitor";
        }

        private void btnTrendClick(object sender, RoutedEventArgs e)
        {
            bd0.Margin = new Thickness(410, 42, 0, 0);
            DisableAllTubePages();
            tubeTrendPage.Visibility = Visibility.Visible;
            tubePageTitle.Text = "Trend";
        }

        private void btnRecipeClick(object sender, RoutedEventArgs e)
        {
            bd0.Margin = new Thickness(490, 42, 0, 0);
            DisableAllTubePages();
            tubeRecipePage.Visibility = Visibility.Visible;
            tubePageTitle.Text = "Recipe";
        }

        private void btnSettingsClick(object sender, RoutedEventArgs e)
        {
            bd0.Margin = new Thickness(570, 42, 0, 0);
            DisableAllTubePages();
            tubeSettingsPage.Visibility = Visibility.Visible;
            tubePageTitle.Text = "Settings";
        }

        private void btnEventsClick(object sender, RoutedEventArgs e)
        {
            bd0.Margin = new Thickness(650, 42, 0, 0);
            DisableAllTubePages();
            tubeEventsPage.Visibility = Visibility.Visible;
            tubePageTitle.Text = "Events";
        }

        private void DisableAllTubePages()
        {
            tubeMonitorPage.Visibility = Visibility.Hidden;
            tubeTrendPage.Visibility = Visibility.Hidden;
            tubeRecipePage.Visibility = Visibility.Hidden;
            tubeSettingsPage.Visibility = Visibility.Hidden;
            tubeEventsPage.Visibility = Visibility.Hidden;
        }

        private void DisableAllTabs()
        {
            borderTube1.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderTube2.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderTube3.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderTube4.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderTube5.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderTube6.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderTube1.ClearValue(EffectProperty);
            borderTube2.ClearValue(EffectProperty);
            borderTube3.ClearValue(EffectProperty);
            borderTube4.ClearValue(EffectProperty);
            borderTube5.ClearValue(EffectProperty);
            borderTube6.ClearValue(EffectProperty);
            borderTube1.Effect = new BlurEffect
            {
                Radius = 5
            };
            borderTube2.Effect = new BlurEffect
            {
                Radius = 5
            };
            borderTube3.Effect = new BlurEffect
            {
                Radius = 5
            };
            borderTube4.Effect = new BlurEffect
            {
                Radius = 5
            };
            borderTube5.Effect = new BlurEffect
            {
                Radius = 5
            };
            borderTube6.Effect = new BlurEffect
            {
                Radius = 5
            };
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            ComNodeService.Instance.Connect();
            Thread t1 = new Thread(new ThreadStart(ReadOpcNodes));
            t1.IsBackground = true;
            t1.Start();
            //SubscriptComNodes();
        }

        private void btnReconnect_Click(object sender, RoutedEventArgs e)
        {
            ComNodeService.Instance.Reconnect();
            Thread t1 = new Thread(new ThreadStart(ReadOpcNodes));
            t1.IsBackground = true;
            t1.Start();
        }

        private void ReadOpcNodes()
        {
            log.Debug("start read nodes thread");

            List<OpcNode> opcReadNodes = new List<OpcNode>();
            for (byte tubeIndex = 0; tubeIndex < 6; ++tubeIndex)
            {
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[0].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[1].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[2].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[3].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[4].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[5].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[0].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[1].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[2].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[3].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[4].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[5].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[0].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[1].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[2].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[3].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[4].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[5].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[0].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[1].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[2].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[3].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[4].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[5].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[0].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[1].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[4].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[5].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[7].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[0].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[1].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[4].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[5].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[7].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[0].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[2].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[3].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].PaddleNodeComponent.PosAct);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].DioNodeComponent.Ev);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].DioNodeComponent.DigInput);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].DioNodeComponent.DigOutput);
                ComNodeService.Instance.ReadComNodes((byte)(tubeIndex+1), opcReadNodes);
            }

        }

    }
}
