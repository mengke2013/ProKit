using System.Windows.Controls;

using log4net;


namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TubeSettingsView : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public TubeSettingsView()
        {
            InitializeComponent();
        }

         public void LoadPage(byte selectedTube)
        {
            log.Info("TubeSettingsPage");
        }
    }
}
