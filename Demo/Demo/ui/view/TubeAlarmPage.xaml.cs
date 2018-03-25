using System.Windows;
using System.Windows.Controls;

using log4net;

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

        public TubeAlarmPage()
        {
            InitializeComponent();
        }

        public void LoadPage(byte selectedTube)
        {
            log.Debug("TubeAlarmPage:LoadTubePage");
            mSelectedTube = selectedTube;

            Visibility = Visibility.Visible;
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
