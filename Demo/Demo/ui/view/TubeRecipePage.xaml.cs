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
using System.Windows.Threading;
using System.Threading;

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
        private StepListItem[] StepItems;

        public event TubeControlBar.ClickHandler CloseClick;
        private ProgressDlg mProgressDlg;

        public TubeRecipePage()
        {
            InitializeComponent();
            mProgressDlg = new ProgressDlg();
            mProgressDlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
           // mProgressDlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
           // mProgressDlg.VerticalAlignment = VerticalAlignment.Center;

            mTubeRecipePageModel = new TubeRecipePageModel();
            mTubePageStyle = new TubePageStyle();
            mTubeRecipePageModel.TubeRecipeViewModel.TubePageStyle = mTubePageStyle;
            this.DataContext = mTubeRecipePageModel.TubeRecipeViewModel;


            StepItems = new StepListItem[4];
            StepItems[0] = StepItem1;
            StepItems[1] = StepItem2;
            StepItems[2] = StepItem3;
            StepItems[3] = StepItem4;

            for (byte i = 0; i < StepItems.Length; ++i)
            {
                StepItems[i].ItemMode = mTubeRecipePageModel.StepListItems[i];
                StepItems[i].ItemClick += new StepListItem.ClickHandler(Item_Select_Click);
            }
            RecipeView.RecipeViewMode = mTubeRecipePageModel.TubeRecipeViewModel;
            RecipeView.CommitClick += new TubeRecipeView.ClickHandler(Step_Commit_Click);
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseClick(sender, e);
            e.Handled = false;
        }

        private void Item_Select_Click(object sender, RoutedEventArgs e, byte stepIndex)
        {
            log.Debug("Step " + stepIndex);
            // MessageBox.Show("Step" + stepIndex);
            for (byte i = 0; i < StepItems.Length; ++i)
            {
                if (i != stepIndex-1)
                {
                    StepItems[i].Background = new SolidColorBrush(Colors.White);
                }
            }
            mTubeRecipePageModel.TubeRecipeViewModel.UpdateView = true;
            mTubeRecipePageModel.TubeRecipeViewModel.StepIndex = stepIndex;
            mTubeRecipePageModel.TubeRecipeViewModel.UpdateView = false;
            SynRecipeStep(stepIndex);
        }

        private void Step_Commit_Click(object sender, RoutedEventArgs e, byte stepIndex)
        {
            log.Debug("Commit Step " + stepIndex);
            CommitRecipeStep(stepIndex);
        }

        public void LoadTubePage(byte selectedTube)
        {
            log.Debug("TubeRecipePage:LoadTubePage");
            mSelectedTube = selectedTube;
            
            //synchronize recipe step data
            //SynRecipeStep(1);
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("TubeRecipePage:DownloadBtn_Click");
            //download recipe data from DB or File to PLC
            //mTubeRecipePageModel.LoadData(mSelectedTube);
            //synchronize recipe step data
            SynRecipeStep(1);
        }

        private void BackupBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("TubeRecipePage:BackupBtn_Click");
            //save recipe data from PLC to DB or File

        }

        private void SynRecipeStep(byte stepIndex)
        {
            ReadRecipeData(stepIndex);
            mProgressDlg.ShowDialog();
        }

        public void ReadRecipeData(byte stepIndex)
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(ReadRecipeDataExec));
            t1.IsBackground = true;
            t1.Start(stepIndex);
        }

        private void ReadRecipeDataExec(Object obj)
        {
            byte stepIndex = (byte)obj;
            List<OpcNode> opcWriteNodes = new List<OpcNode>();
            //            byte stepIndex = 2;
            sbyte comCommand = 21;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord.Value = stepIndex;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.TchLoad.Value = comCommand;

            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord);
            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.TchLoad);
            ComNodeService.Instance.WriteComNodes(mSelectedTube, opcWriteNodes);
            byte[] recipeBytes = new byte[328];
            try
            {
                //TcpClient.Instance.Connect();
                TcpClient.Instance.GetRecipe(stepIndex, recipeBytes, SynRecipeStepCallback);

                //TcpClient.Instance.Close();
            }
            catch (Exception ee)
            {
                TcpClient.Instance.Close();
            }
        }


        private void SynRecipeStepCallback(byte[] recipeBytes)
        {
            mTubeRecipePageModel.ParseRecipeData(recipeBytes);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
            });
                
        }

        private void CommitRecipeStep(byte stepIndex)
        {
            List<OpcNode> opcWriteNodes = new List<OpcNode>();
            sbyte comCommand = 22;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord.Value = stepIndex;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.TchLoad.Value = comCommand;
            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord);
            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.TchLoad);
            ComNodeService.Instance.WriteComNodes(mSelectedTube, opcWriteNodes);


            byte[] recipeBytes = new byte[328];
            mTubeRecipePageModel.ConvertRecipeData(recipeBytes);
            try
            {
                //TcpClient.Instance.Connect();
                TcpClient.Instance.SendRecipe(stepIndex, recipeBytes, CommitRecipeStepCallback);
                //TcpClient.Instance.Close();
            }
            catch (Exception ee)
            {
                TcpClient.Instance.Close();
            }
        }

        private void CommitRecipeStepCallback()
        {
            MessageBox.Show("Done");
        }
    }
}
