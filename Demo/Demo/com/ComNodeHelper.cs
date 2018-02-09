using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using log4net;
using Rocky.Core.Opc.Ua;
using Demo.com.entity;

namespace Demo.com
{
    class ComNodeHelper
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ComNodeHelper instance;

        public static ComNodeHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ComNodeHelper();
                }
                return instance;
            }
        }

        public void ReadOpcNodes(byte selectedTube)
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(ReadOpcNodesExec));
            t1.IsBackground = true;
            t1.Start((byte)(selectedTube-1));
        }

        public ComRecipeStepNodeComponent ReadRecipeStep(byte selectedTybe)
        {
            //read from socket
            return null;
        }

        private ComNodeHelper() { }

        private void ReadOpcNodesExec(Object obj)
        {
            log.Debug("start read nodes thread");
            byte tubeIndex = (byte)obj;
            List<OpcNode> opcReadNodes = new List<OpcNode>();
            //for (byte tubeIndex = 0; tubeIndex < 6; ++tubeIndex)
            {
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[0].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[1].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[2].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[3].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[4].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[5].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[0].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[1].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[2].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[3].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[4].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[5].IntValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[0].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[1].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[2].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[3].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[4].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[5].ExtValue);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[0].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[1].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[2].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[3].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[4].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].FurnaceNodeComponent.TemperNodeComponents[5].HeatPower);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[0].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[1].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[4].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[5].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[7].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[0].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[1].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[4].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[5].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].MfcNodeComponent.GasNodeComponents[7].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[0].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[2].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[3].CurSp);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].PaddleNodeComponent.PosAct);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].DioNodeComponent.Ev);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].DioNodeComponent.DigInput);
                opcReadNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex].DioNodeComponent.DigOutput);
                ComNodeService.Instance.ReadComNodes((byte)(tubeIndex + 1), opcReadNodes);
            }

        }
    }
}
