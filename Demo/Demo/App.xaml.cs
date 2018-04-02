using System.Windows;
using log4net;
using Demo.com;
using Demo.service;

namespace Demo
{
    public enum ProcessStatus
    {
        UNKNOWN = -1,
        RUNNING = 0,
        HOLDING = 1,
        IDLE = 2,
        ABORT = 3,
        DOWNLOADING = 6,
        END = 10,
        INIT = 20
    }

    public enum TrendPlotType
    {
        UNKNOWN = 0,
        Temperature = 1,
        Gas = 2,
        Vacuum = 3
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public App()
        {
            StartServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            base.OnStartup(e);
            log.Info("==Startup=====================>>>");
        }
        protected override void OnExit(ExitEventArgs e)
        {
            log.Info("<<<========================End==");
            base.OnExit(e);
        }

        

        private void StartServices()
        {
            ComService.Instance.StartHeartBeatService();

            SocketClient.Instance.StartTcpService(new SocketClient.OnConnectEnd(OnConnectEnd));
            TrendService.Instance.StartPullTrendDataService();
            AlarmService.Instance.StartPullAlarmService();
        }

        private void OnConnectEnd()
        {
            ProcessService.Instance.StartPullInfoService();
        }
    }
}
