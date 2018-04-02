using System.Windows;
using System.Windows.Controls;

using log4net;

using Demo.controller;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for TubeEventsPage.xaml
    /// </summary>
    public partial class TubeAlarmPage : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event Home.ClickHandler CloseClick;
        private byte mSelectedTube;
        private AlarmController mAlarmController;

        public TubeAlarmPage()
        {
            InitializeComponent();

            mAlarmController = new AlarmController(this);
        }

        public void AcknowledgeButton_Click(object sender, RoutedEventArgs e)
        {
            mAlarmController.AcknowledgeAlarms(mSelectedTube);

        }

        public void LoadPage(byte selectedTube)
        {
            log.Debug("TubeAlarmPage:LoadTubePage");
            mSelectedTube = selectedTube;

            Visibility = Visibility.Visible;

            AlarmView.DescriptionColumn.Width = AlarmView.dataGrid.ActualWidth - 88;
            mAlarmController.UpdateAlarmItems(selectedTube);
        }

        public void UnloadPage(byte selectedTube)
        {
            Visibility = Visibility.Hidden;
            //            ClearValue(EffectProperty);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseClick(sender, e);
            e.Handled = false;
        }
    }
}
