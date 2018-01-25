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
using System.Windows.Threading;
using log4net;
using System.Threading;
using Rocky.Core.Opc.Ua;
using Demo.com;

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TubeWindow : Window
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private byte mSelectedTube = 1;

        public TubeWindow(byte selectedTube)
        {
            InitializeComponent();

            mSelectedTube = selectedTube;
            LoadOPC();
        }

        private void LoadOPC()
        {

            List<OpcNode> opcSubscriptNodes = new List<OpcNode>();
            for (int j = 0; j < 3; ++j)
            {
                for (int i = 0; i < 8; ++i)
                {
                    OpcNode opcNode = ComProcessNodeComponent.Instance.TubeNodeComponents[j].FurnaceNodeComponent.TemperNodeComponents[i].IntValue;
                    opcNode.Notification += new NodeValueUpdateEventHandler(NodeValueUpdate);
                    opcNode = ComProcessNodeComponent.Instance.TubeNodeComponents[j].FurnaceNodeComponent.TemperNodeComponents[i].ExtValue;
                    opcNode.Notification += new NodeValueUpdateEventHandler(NodeValueUpdate);
                    opcNode = ComProcessNodeComponent.Instance.TubeNodeComponents[j].FurnaceNodeComponent.TemperNodeComponents[i].HeatPower;
                    opcNode.Notification += new NodeValueUpdateEventHandler(NodeValueUpdate);
                    opcNode = ComProcessNodeComponent.Instance.TubeNodeComponents[j].MfcNodeComponent.GasNodeComponents[i].CurMeas;
                    opcNode.Notification += new NodeValueUpdateEventHandler(NodeValueUpdate);
                    opcNode = ComProcessNodeComponent.Instance.TubeNodeComponents[j].VacuumNodeComponent.AnalogNodeComponents[i].CurMeas;
                    opcNode.Notification += new NodeValueUpdateEventHandler(NodeValueUpdate);

                    //opcSubscriptNodes.Add(opcNode);
                }

            }

            textBoxTemper1Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].CurSp);
            textBoxTemper2Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].CurSp);
            textBoxTemper3Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].CurSp);
            textBoxTemper4Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].CurSp);
            textBoxTemper5Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].CurSp);
            textBoxTemper6Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].CurSp);
            textBoxTemper1IntValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].IntValue);
            textBoxTemper2IntValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].IntValue);
            textBoxTemper3IntValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].IntValue);
            textBoxTemper4IntValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].IntValue);
            textBoxTemper5IntValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].IntValue);
            textBoxTemper6IntValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].IntValue);
            textBoxTemper1ExtValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].ExtValue);
            textBoxTemper2ExtValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].ExtValue);
            textBoxTemper3ExtValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].ExtValue);
            textBoxTemper4ExtValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].ExtValue);
            textBoxTemper5ExtValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].ExtValue);
            textBoxTemper6ExtValue.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].ExtValue);
            textBoxTemper1HeatPower.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].HeatPower);
            textBoxTemper2HeatPower.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].HeatPower);
            textBoxTemper3HeatPower.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].HeatPower);
            textBoxTemper4HeatPower.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].HeatPower);
            textBoxTemper5HeatPower.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].HeatPower);
            textBoxTemper6HeatPower.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].HeatPower);
            textBoxGasN2Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[0].CurSp);
            textBoxGasH2Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[1].CurSp);
            textBoxGasO2Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[4].CurSp);
            textBoxGasBCl3Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[5].CurSp);
            textBoxGasPC8Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[7].CurSp);
            textBoxGasN2.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[0].CurMeas);
            textBoxGasH2.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[1].CurMeas);
            textBoxGasO2.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[4].CurMeas);
            textBoxGasBCl3.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[5].CurMeas);
            textBoxGasPC8.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[7].CurMeas);
            textBoxAnalog1Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[0].CurSp);
            textBoxAnalog3Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[2].CurSp);
            textBoxAnalog4Sp.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[3].CurSp);
            textBoxAnalog1.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas);
            textBoxAnalog3.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas);
            textBoxAnalog4.DataContext = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas);

            ComNodeWraper paddleNodeWraper = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].PaddleNodeComponent.PosAct);
            textBoxPaddlePos.DataContext = paddleNodeWraper;
            ComNodeWraper evNodeWraper = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].DioNodeComponent.Ev);
            textBoxEV.DataContext = evNodeWraper;
            ComNodeWraper diNodeWraper = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].DioNodeComponent.DigInput);
            textBoxDI.DataContext = diNodeWraper;
            ComNodeWraper doNodeWraper = new ComNodeWraper(ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].DioNodeComponent.DigOutput);
            textBoxDO.DataContext = doNodeWraper;



            //ComNodeService.Instance.SubscriptComNodes(opcSubscriptNodes);
        }

        

        private void NodeValueUpdate(OpcNode opcNode, Object newValue)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                /*
                if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube-1].FurnaceNodeComponent.TemperNodeComponents[0].IntValue.NodeID)
                {
                    textBox3.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].IntValue.NodeID)
                {
                    textBox4.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].IntValue.NodeID)
                {
                    textBox5.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].IntValue.NodeID)
                {
                    textBox6.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].IntValue.NodeID)
                {
                    textBox7.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].IntValue.NodeID)
                {
                    textBox8.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].ExtValue.NodeID)
                {
                    textBoxTemper1ExtValue.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].ExtValue.NodeID)
                {
                    textBoxTemper2ExtValue.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].ExtValue.NodeID)
                {
                    textBoxTemper3ExtValue.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].ExtValue.NodeID)
                {
                    textBoxTemper4ExtValue.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].ExtValue.NodeID)
                {
                    textBoxTemper5ExtValue.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].ExtValue.NodeID)
                {
                    textBoxTemper6ExtValue.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].HeatPower.NodeID)
                {
                    textBoxTemper1HeatPower.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].HeatPower.NodeID)
                {
                    textBoxTemper2HeatPower.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].HeatPower.NodeID)
                {
                    textBoxTemper3HeatPower.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].HeatPower.NodeID)
                {
                    textBoxTemper4HeatPower.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].HeatPower.NodeID)
                {
                    textBoxTemper5HeatPower.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].HeatPower.NodeID)
                {
                    textBoxTemper6HeatPower.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].MfcNodeComponent.GasNodeComponents[0].CurMeas.NodeID)
                {
                    textBoxGas1Value.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].MfcNodeComponent.GasNodeComponents[1].CurMeas.NodeID)
                {
                    textBoxGas2Value.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].MfcNodeComponent.GasNodeComponents[3].CurMeas.NodeID)
                {
                    textBoxGas3Value.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].MfcNodeComponent.GasNodeComponents[4].CurMeas.NodeID)
                {
                    textBoxGas4Value.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].MfcNodeComponent.GasNodeComponents[6].CurMeas.NodeID)
                {
                    textBoxGas5Value.Text = newValue.ToString();
                }
                else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[selectedTube - 1].MfcNodeComponent.GasNodeComponents[7].CurMeas.NodeID)
                {
                    textBoxGas6Value.Text = newValue.ToString();
                }
                */
            });
        }
    }
}
