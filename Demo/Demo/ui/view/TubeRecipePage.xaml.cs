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
using System.Net;
using System.Net.Sockets;
using Demo.model;
using Demo.controller;

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
        private RecipeController mController;

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
            mController = new RecipeController();

            StepItems = new StepListItem[63];
            StepItems[0] = StepItem1;
            StepItems[1] = StepItem2;
            StepItems[2] = StepItem3;
            StepItems[3] = StepItem4;
            StepItems[4] = StepItem5;
            StepItems[5] = StepItem6;
            StepItems[6] = StepItem7;
            StepItems[7] = StepItem8;
            StepItems[8] = StepItem9;
            StepItems[9] = StepItem10;
            StepItems[10] = StepItem11;
            StepItems[11] = StepItem12;
            StepItems[12] = StepItem13;
            StepItems[13] = StepItem14;
            StepItems[14] = StepItem15;
            StepItems[15] = StepItem16;
            StepItems[16] = StepItem17;
            StepItems[17] = StepItem18;
            StepItems[18] = StepItem19;
            StepItems[19] = StepItem20;
            StepItems[20] = StepItem21;
            StepItems[21] = StepItem22;
            StepItems[22] = StepItem23;
            StepItems[23] = StepItem24;
            StepItems[24] = StepItem25;
            StepItems[25] = StepItem26;
            StepItems[26] = StepItem27;
            StepItems[27] = StepItem28;
            StepItems[28] = StepItem29;
            StepItems[29] = StepItem30;
            StepItems[30] = StepItem31;
            StepItems[31] = StepItem32;
            StepItems[32] = StepItem33;
            StepItems[33] = StepItem34;
            StepItems[34] = StepItem35;
            StepItems[35] = StepItem36;
            StepItems[36] = StepItem37;
            StepItems[37] = StepItem38;
            StepItems[38] = StepItem39;
            StepItems[39] = StepItem40;
            StepItems[40] = StepItem41;
            StepItems[41] = StepItem42;
            StepItems[42] = StepItem43;
            StepItems[43] = StepItem44;
            StepItems[44] = StepItem45;
            StepItems[45] = StepItem46;
            StepItems[46] = StepItem47;
            StepItems[47] = StepItem48;
            StepItems[48] = StepItem49;
            StepItems[49] = StepItem50;
            StepItems[50] = StepItem51;
            StepItems[51] = StepItem52;
            StepItems[52] = StepItem53;
            StepItems[53] = StepItem54;
            StepItems[54] = StepItem55;
            StepItems[55] = StepItem56;
            StepItems[56] = StepItem57;
            StepItems[57] = StepItem58;
            StepItems[58] = StepItem59;
            StepItems[59] = StepItem60;
            StepItems[60] = StepItem61;
            StepItems[61] = StepItem62;
            StepItems[62] = StepItem63;

            for (byte i = 0; i < StepItems.Length; ++i)
            {
                StepItems[i].ItemMode = mTubeRecipePageModel.StepListItems[i];
                StepItems[i].ItemClick += new StepListItem.ClickHandler(Item_Select_Click);
            }
            RecipeView.RecipeViewMode = mTubeRecipePageModel.TubeRecipeViewModel;
            RecipeView.CommitClick += new TubeRecipeView.ClickHandler(Step_Commit_Click);
        }

        public void LoadTubePage(byte selectedTube)
        {
            log.Debug("TubeRecipePage:LoadTubePage");
            mSelectedTube = selectedTube;

            Recipe recipe = mController.LoadRecipe(mSelectedTube);
            if (recipe == null)
            {
                txtBlockRecipeInfo.Visibility = Visibility.Visible;
                gridRecipePanel.Visibility = Visibility.Hidden;
            }
            else
            {
                txtBlockRecipeInfo.Visibility = Visibility.Hidden;
                gridRecipePanel.Visibility = Visibility.Visible;

                //synchronize recipe step data
                //mTubeRecipePageModel.LoadData(mSelectedTube);
                StepItems[0].Item_Click(null, null);
            }
            mTubeRecipePageModel.TubeRecipeViewModel.ProcessName = mController.GetRecipeName(mSelectedTube);

            /*
            bool startSyn = mController.SynStep(mSelectedTube, 1, null, OnSynStepComplete1);
            if (startSyn)
            {
                mProgressDlg.ProgressModel.MaxValue = 64;
                mProgressDlg.ProgressModel.Progress = 0;
                mProgressDlg.ShowDialog();
            }
            */
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseClick(sender, e);
            e.Handled = false;
        }

        private void Item_Select_Click(object sender, RoutedEventArgs e, int stepIndex)
        {
            log.Debug("Step " + stepIndex);
            // MessageBox.Show("Step" + stepIndex);

            for (byte i = 0; i < StepItems.Length; ++i)
            {
                if (i != stepIndex - 1)
                {
                    StepItems[i].Background = new SolidColorBrush(Colors.White);
                }
            }

            //LoadStep(stepIndex);
            bool startSyn = mController.SynStep(mSelectedTube, (byte)stepIndex, null, OnSynStepComplete1);
            if (startSyn)
            {
                mProgressDlg.ProgressModel.MaxValue = 64;
                mProgressDlg.ProgressModel.Progress = 0;
                mProgressDlg.ShowDialog();
            }
        }

        private void Step_Commit_Click(object sender, RoutedEventArgs e, int stepIndex)
        {
            log.Debug("Commit Step " + stepIndex);
            RecipeStep step = mController.GetRecipeStep(mSelectedTube, stepIndex);
            mController.ConvertRecipeModel(step, mTubeRecipePageModel); 
            bool startCommit = mController.CommitStep(mSelectedTube, stepIndex, null, OnCommitStepComplete);
            if (startCommit)
            {
                mProgressDlg.ShowDialog();
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("TubeRecipePage:RefreshBtn_Click");
            bool startSyn = mController.SynRecipe(mSelectedTube, OnSynRecipeComplete, OnSynStepComplete);
            if (startSyn)
            {
                mProgressDlg.ProgressModel.MaxValue = 64;
                mProgressDlg.ProgressModel.Progress = 0;
                mProgressDlg.ShowDialog();
            }
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("TubeRecipePage:DownloadBtn_Click");
            //download recipe data from DB or File to PLC
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Recipe Data Files (*.rcd)|*.rcd"
            };
            openFileDialog.Title = "Open a Recipe Data File";
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                bool startDownload = mController.DownloadRecipe(openFileDialog.FileName, mSelectedTube, OnDownRecipeComplete, OnDownloadStepComplete);
                if (startDownload)
                {
                    mProgressDlg.ProgressModel.MaxValue = 64;
                    mProgressDlg.ProgressModel.Progress = 0;
                    mProgressDlg.ShowDialog();
                }
            }
        }

        private void BackupBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("TubeRecipePage:BackupBtn_Click");
            //save recipe data from PLC to DB or File
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "Recipe Data Files (*.rcd)|*.rcd"
            };
            saveFileDialog.Title = "Save a Recipe Data File";
            var result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                bool startBackup = mController.BackupRecipe(saveFileDialog.FileName, mSelectedTube, OnBackupRecipeComplete);
                if (startBackup)
                {
                    mProgressDlg.ShowDialog();
                }
            }
        }

        private void LoadStep(int stepIndex)
        {
            RecipeStep step = mController.GetRecipeStep(mSelectedTube,stepIndex);
            mController.ConvertRecipePageModel(mTubeRecipePageModel, step);
        }

        private void OnSynRecipeComplete(Recipe recipe)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                StepItems[0].Item_Click(null, null);
                MessageBox.Show("OnSynRecipeComplete");
                //LoadStep(1);
            });
        }

        private void OnSynStepComplete(RecipeStep step)
        {
            mProgressDlg.ProgressModel.Progress = step.StepIndex;
        }

        private void OnSynStepComplete1(RecipeStep step)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                mController.ConvertRecipePageModel(mTubeRecipePageModel, step);
                //StepItems[0].Item_Click(null, null);
                //MessageBox.Show("OnDownloadRecipeComplete");
            });
        }

        private void OnDownRecipeComplete(Recipe recipe)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                StepItems[0].Item_Click(null, null);
                MessageBox.Show("OnDownloadRecipeComplete");
            });
        }

        private void OnDownloadStepComplete(RecipeStep step)
        {
            mProgressDlg.ProgressModel.Progress = step.StepIndex;
        }

        private void OnBackupRecipeComplete()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                MessageBox.Show("Done");
            });
        }

        private void OnCommitStepComplete(RecipeStep step)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                MessageBox.Show("Done");
            });
        }
    }
}
