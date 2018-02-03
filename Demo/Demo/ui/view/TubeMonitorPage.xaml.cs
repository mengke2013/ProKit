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
using Demo.ui.model;
using Rocky.Core.Opc.Ua;
using Demo.com;
using log4net;

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for TubeMonitorPage.xaml
    /// </summary>
    public partial class TubeMonitorPage : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TubeMonitorPageModel mTubeMonitorPageModel;
        private TubePageStyle mTubePageStyle;

        public TubeMonitorPage()
        {
            InitializeComponent();

            mTubeMonitorPageModel = new TubeMonitorPageModel();
            mTubePageStyle = new TubePageStyle(); ;
        }

        public void LoadPage(byte selectedTube)
        {
            log.Info("TubeMonitorPage");
            mTubeMonitorPageModel.SelectedTube = selectedTube;

            mTubePageStyle.TextBoxWidth = this.ActualWidth / 20;
            mTubePageStyle.TextBoxHeight = this.ActualHeight / 20;

            mTubeMonitorPageModel.TubePageStyle = mTubePageStyle;

            DataContext = mTubeMonitorPageModel;
            mTubeMonitorPageModel.UpdateDataSource();
        }
    }
}
