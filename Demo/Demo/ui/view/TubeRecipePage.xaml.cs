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
        Demo.utilities.Properties mRecipeBak = new Demo.utilities.Properties("recipe_bak.data");

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
                if (i != stepIndex - 1)
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
            if (ComNodeService.Instance.IsConnected())
            {
                CommitRecipeStep(stepIndex);
            }

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
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Recipe Data Files (*.rcd)|*.rcd"
            };
            openFileDialog.Title = "Open a Recipe Data File";
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                //synchronize recipe step data
                //WriteRecipeData();
                // mProgressDlg.ShowDialog();
                mRecipeBak = new Demo.utilities.Properties(openFileDialog.FileName);
                MessageBox.Show(mRecipeBak.get("1"));
            }

        }

        private void BackupBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("TubeRecipePage:BackupBtn_Click");
            //save recipe data from PLC to DB or File
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog() {
                Filter = "Recipe Data Files (*.rcd)|*.rcd"
            };
            saveFileDialog.Title = "Save a Recipe Data File";
            var result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                mRecipeBak = new Demo.utilities.Properties(saveFileDialog.FileName);
                //ReadRecipeData();
                //mProgressDlg.ShowDialog();

                mRecipeBak.set("1", "Test");
                mRecipeBak.Save();
                MessageBox.Show("Recipe Data File is saved successfully.");
            }

           
 

        }

        private void SynRecipeStep(byte stepIndex)
        {
            if (ComNodeService.Instance.IsConnected())
            {
                ReadRecipeData(stepIndex);
                mProgressDlg.ShowDialog();
            }

        }

        public void ReadRecipeData()
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(ReadRecipeStepExec));
            t1.IsBackground = true;
            t1.Start((byte)1);
        }

        private void ReadRecipeStepExec(Object obj)
        {
            byte stepIndex = (byte)obj;
            List<OpcNode> opcWriteNodes = new List<OpcNode>();
            sbyte comCommand = 21;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord.Value = (byte)70;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.TchLoad.Value = comCommand;

            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord);
            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.TchLoad);
            ComNodeService.Instance.WriteComNodes(mSelectedTube, opcWriteNodes);
            ReadCompleteRecipe();
        }

        public void WriteRecipeData()
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(WriteRecipeStepExec));
            t1.IsBackground = true;
            t1.Start((byte)1);
        }

        private void WriteRecipeStepExec(Object obj)
        {
            byte stepIndex = (byte)obj;
            List<OpcNode> opcWriteNodes = new List<OpcNode>();
            sbyte comCommand = 22;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord.Value = (byte)70;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.TchLoad.Value = comCommand;

            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord);
            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.TchLoad);
            ComNodeService.Instance.WriteComNodes(mSelectedTube, opcWriteNodes);
            WriteCompleteRecipe();
        }

        private void ReadRecipeData(byte stepIndex)
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(ReadRecipeDataExec));
            t1.IsBackground = true;
            t1.Start(stepIndex);
        }

        private void ReadRecipeDataExec(Object obj)
        {
            byte stepIndex = (byte)obj;
            List<OpcNode> opcWriteNodes = new List<OpcNode>();
            sbyte comCommand = 21;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord.Value = stepIndex;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.TchLoad.Value = comCommand;

            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.ControlWord);
            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].CommandNodeComponent.TchLoad);
            ComNodeService.Instance.WriteComNodes(mSelectedTube, opcWriteNodes);
            byte[] recipeBytes = new byte[328];
            try
            {
                Demo.com.TcpClient.Instance.GetRecipe(stepIndex, recipeBytes, SynRecipeStepCallback);
            }
            catch (Exception ee)
            {
                Demo.com.TcpClient.Instance.Close();
            }
        }


        private void SynRecipeStepCallback(byte[] recipeBytes, byte stepIndex)
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
                Demo.com.TcpClient.Instance.SendRecipe(stepIndex, recipeBytes, CommitRecipeStepCallback);
            }
            catch (Exception ee)
            {
                Demo.com.TcpClient.Instance.Close();
            }
        }

        private void CommitRecipeStepCallback(byte stepIndex)
        {
            MessageBox.Show("Done");
        }


        public void ReadCompleteRecipe()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            string host = "192.168.1.64";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            StateObject so2 = new StateObject();
            so2.socket = socket;
            so2.stepIndex = (byte)1;
            socket.BeginConnect(ipe, asyncResult =>
            {
                so2.aResult = asyncResult;
                socket.BeginReceive(so2.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None,
                          new AsyncCallback(ReadCompleteRecipeCallback), so2);
            }, null);
        }

        private void ReadCompleteRecipeCallback(IAsyncResult ar)
        {
            SocketError errorCode;
            StateObject so2 = (StateObject)ar.AsyncState;
            Socket socket = so2.socket;
            int nBytesRec = socket.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                nBytesRec = 0;
            }

            if (nBytesRec != 328)
            {
                //return with error code
                //MessageBox.Show("error");
                log.Error("error:step " + so2.stepIndex);
            }
            else
            {
                //parse recipe step data
                byte[] recipeBytes = new byte[328];
                Array.Copy(so2.buffer, 0, recipeBytes, 0, 328);
                mRecipeBak.set(String.Format("{0}", so2.stepIndex), Encoding.ASCII.GetString(so2.buffer));
                mRecipeBak.Save();
            }

            if (so2.stepIndex < 63)
            {
                //go next step 
                so2.stepIndex = (byte)(so2.stepIndex + 1);
                socket.BeginReceive(so2.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None,
                    new AsyncCallback(ReadCompleteRecipeCallback), so2);
            }
            else
            {
                //finish recipe
                socket.EndConnect(so2.aResult);
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    mProgressDlg.Hide();
                });
                MessageBox.Show("done");
                return;
            }
        }

        public void WriteCompleteRecipe()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 2000;
            string host = "192.168.1.64";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);

            StateObject so2 = new StateObject();
            so2.socket = socket;

            so2.stepIndex = (byte)1;
            string strRecipeData = mRecipeBak.get(String.Format("{0}", so2.stepIndex));
            so2.buffer = Encoding.ASCII.GetBytes(strRecipeData);
            socket.BeginConnect(ipe, ar =>
            {
                so2.aResult = ar;
                socket.BeginSend(so2.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None,
                      new AsyncCallback(WriteCompleteRecipeCallback), so2);
            }, null);
        }

        private void WriteCompleteRecipeCallback(IAsyncResult ar)
        {
            SocketError errorCode;
            StateObject so2 = (StateObject)ar.AsyncState;
            Socket sSocket = so2.socket;
            int nBytesSend = sSocket.EndSend(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                nBytesSend = 0;
            }

            if (nBytesSend != 328)
            {
                //return with error code
                //MessageBox.Show("error");
                log.Error("error:step " + so2.stepIndex);
            }
            else
            {
                if (so2.stepIndex < 63)
                {
                    Thread.Sleep(200);
                    //go next step 
                    so2.stepIndex = (byte)(so2.stepIndex + 1);
                    string strRecipeData = mRecipeBak.get(String.Format("{0}", so2.stepIndex));
                    so2.buffer = Encoding.ASCII.GetBytes(strRecipeData);
                    log.Debug(so2.buffer);
                    sSocket.BeginSend(so2.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None,
                        new AsyncCallback(WriteCompleteRecipeCallback), so2);
                }
                else
                {
                    //finish recipe
                    sSocket.EndConnect(so2.aResult);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        mProgressDlg.Hide();
                    });
                    MessageBox.Show("done");
                    //SynRecipeStep(1);
                    return;
                }
            }
        }

        public class StateObject
        {
            public Socket socket = null;
            public const int BUFFER_SIZE = 328;
            public byte[] buffer = new byte[BUFFER_SIZE];
            public byte stepIndex;
            public IAsyncResult aResult;
            public StringBuilder sb = new StringBuilder();
        }
    }
}
