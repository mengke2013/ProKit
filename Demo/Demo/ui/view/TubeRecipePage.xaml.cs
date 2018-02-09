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

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for TubeRecipePage.xaml
    /// </summary>
    public partial class TubeRecipePage : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private byte mSelectedTube;
        TubeRecipePageModel mTubeRecipePageModel;
        private TubePageStyle mTubePageStyle;

        public event TubeControlBar.ClickHandler CloseClick;

        public TubeRecipePage()
        {
            InitializeComponent();

            mTubeRecipePageModel = new TubeRecipePageModel();
            mTubePageStyle = new TubePageStyle();
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseClick(sender, e);
            e.Handled = false;
        }

        public void LoadTubePage(byte selectedTube)
        {
            log.Debug("TubeRecipePage:LoadTubePage");
            mSelectedTube = selectedTube;
            
            mTubeRecipePageModel.TubePageStyle = mTubePageStyle;

            //load recipe data from PLC
            mTubeRecipePageModel.LoadData(mSelectedTube);
            this.DataContext = mTubeRecipePageModel;
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("TubeRecipePage:DownloadBtn_Click");
            //download recipe data from DB or File to PLC

            LoadTubePage(mSelectedTube);
        }

        private void BackupBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("TubeRecipePage:BackupBtn_Click");
            //save recipe data from PLC to DB or File

        }
    }
}
