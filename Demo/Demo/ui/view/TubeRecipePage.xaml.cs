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
using Demo.com;
using Rocky.Core.Opc.Ua;
using Demo.com.entity;

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
            mTubeRecipePageModel.LoadData(mSelectedTube);
            mTubeRecipePageModel.TubeRecipeViewModel.TubePageStyle = mTubePageStyle;

            //load recipe data from PLC
            this.DataContext = mTubeRecipePageModel.TubeRecipeViewModel;
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("TubeRecipePage:DownloadBtn_Click");
            //download recipe data from DB or File to PLC

            List<OpcNode> opcWriteNodes = new List<OpcNode>();
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord.Value = (byte)2;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube-1].CommandNodeComponent.TchLoad.Value = (sbyte)21;

            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord);
            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube-1].CommandNodeComponent.TchLoad);
            ComNodeService.Instance.WriteComNodes(mSelectedTube, opcWriteNodes);
            byte[] recipeBytes = new byte[328];
            try
            {
                TcpClient.Instance.Connect();
                TcpClient.Instance.GetRecipe(1, recipeBytes);
                TcpClient.Instance.Close();
            }
            catch (Exception ee)
            {
                TcpClient.Instance.Close();
            }
            mTubeRecipePageModel.ParseRecipeData(recipeBytes);

 //           mTubeRecipePageModel.LoadData(mSelectedTube);
 //           this.DataContext = mTubeRecipePageModel;
        }

        private void BackupBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("TubeRecipePage:BackupBtn_Click");
            //save recipe data from PLC to DB or File

        }
    }
}
