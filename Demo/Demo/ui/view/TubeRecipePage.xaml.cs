using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Threading;

using log4net;

using Demo.ui.model;
using Demo.controller;
using System;
using System.Reflection;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for TubeRecipePage.xaml
    /// </summary>
    public partial class TubeRecipePage : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private byte mSelectedTube;
        private byte mSelectedStep;

        private RecipeController mController;
        TubeRecipeViewModel mViewModel;
        StepItemListModel mStepListModel;

        private StepListItem[] StepItems;
        public event Home.ClickHandler CloseClick;
        private ProgressDlg mProgressDlg;


        public TubeRecipePage()
        {
            InitializeComponent();
            mProgressDlg = new ProgressDlg();
            mProgressDlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // mProgressDlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            // mProgressDlg.VerticalAlignment = VerticalAlignment.Center;

            mViewModel = new TubeRecipeViewModel(1);
            mViewModel.TubePageStyle = new TubePageStyle();
            this.DataContext = mViewModel;
            mStepListModel = new StepItemListModel();
            mController = new RecipeController(this);
            RecipeView.RecipeViewMode = mViewModel;
            RecipeView.CommitClick += new TubeRecipeView.ClickHandler(Step_Commit_Click);

            StepItems = new StepListItem[63];
            for (byte i = 0; i < StepItems.Length; ++i)
            {
                StepItems[i] = (StepListItem)this.FindName("StepItem" + (i + 1));
                StepItems[i].ItemMode = mStepListModel.StepListItems[i];
                StepItems[i].ItemClick += new StepListItem.ClickHandler(Item_Select_Click);
            }
        }

        public void LoadPage(byte selectedTube)
        {
            log.Debug("TubeRecipePage:LoadTubePage");
            mSelectedTube = selectedTube;

            Visibility = Visibility.Visible;

            mController.LoadRecipe(mSelectedTube);

            //synchronize recipe step data
            //mTubeRecipePageModel.LoadData(mSelectedTube);
            mSelectedStep = 1;
            StepItems[0].Item_Click(null, null);

            mViewModel.ProcessName = mController.GetRecipeName(mSelectedTube);

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

        public void UnloadPage(byte selectedTube)
        {
            Visibility = Visibility.Hidden;
            //            ClearValue(EffectProperty);
        }

        public TubeRecipeViewModel ViewModel
        {
            get { return mViewModel; }
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
            mSelectedStep = (byte)stepIndex;
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
            mController.ConvertRecipeModel((byte)stepIndex);
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

        private void LoadStep(byte stepIndex)
        {
            mController.ConvertRecipePageModel(stepIndex);
        }

        private void OnSynRecipeComplete()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                StepItems[0].Item_Click(null, null);
                MessageBox.Show("OnSynRecipeComplete");
                //LoadStep(1);
            });
        }

        private void OnSynStepComplete(byte stepIndex)
        {
            mProgressDlg.ProgressModel.Progress = stepIndex;
        }

        private void OnSynStepComplete1(byte stepIndex)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                mController.ConvertRecipePageModel(stepIndex);
                //StepItems[0].Item_Click(null, null);
                //MessageBox.Show("OnDownloadRecipeComplete");
            });
        }

        private void OnDownRecipeComplete()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                StepItems[0].Item_Click(null, null);
                MessageBox.Show("OnDownloadRecipeComplete");
            });
        }

        private void OnDownloadStepComplete(byte stepIndex)
        {
            mProgressDlg.ProgressModel.Progress = stepIndex;
        }

        private void OnBackupRecipeComplete()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                MessageBox.Show("Done");
            });
        }

        private void OnCommitStepComplete(byte stepIndex)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                mProgressDlg.Hide();
                MessageBox.Show("Done");
            });
        }
    }
}
