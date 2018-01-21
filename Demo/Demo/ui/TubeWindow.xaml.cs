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
using System.Threading;
using Rocky.Core.Opc.Ua;
using Demo.service;
using Demo.com;

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TubeWindow : Window
    {
        private byte mSelectedTube = 1;

        public TubeWindow(byte selectedTube)
        {
            InitializeComponent();

            mSelectedTube = selectedTube;
            labelTitle.Content = "Tube " + selectedTube;
            LoadOPC();
        }

        private void LoadOPC()
        {
            List<OpcNode> opcReadNodes = new List<OpcNode>();
            opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[0].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas);
            opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[0].MfcNodeComponent.GasNodeComponents[0].CurMeas);
            opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[0].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas);
            ComNodeService.Instance.ReadComNodes(1, opcReadNodes);

            //textBox.Text = ComProcessNodeComponent.Instance.TubeNodeComponents[0].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas.Value.ToString();
            //textBox1.Text = ComProcessNodeComponent.Instance.TubeNodeComponents[0].MfcNodeComponent.GasNodeComponents[0].CurMeas.Value.ToString();
            //textBox2.Text = ComProcessNodeComponent.Instance.TubeNodeComponents[0].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas.Value.ToString();

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
            ComProcessNodeComponent.Instance.Test.Notification += new NodeValueUpdateEventHandler(NodeValueUpdate);
            ComNodeWraper testBinding = new ComNodeWraper(ComProcessNodeComponent.Instance.Test);
            textBox3.DataContext = testBinding;

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

        private void btnTube1_Click(object sender, RoutedEventArgs e)
        {
            textBox4.Text = "" + ComNodeService.Instance.TubeStatus(mSelectedTube);
        }
    }
}
