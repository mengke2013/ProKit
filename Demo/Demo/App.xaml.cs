using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using Rocky.Core.Opc.Ua;
using Demo.service;
using Demo.com;

namespace Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public App()
        {
            SubscriptComNodes();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            base.OnStartup(e);
            log.Info("==Startup=====================>>>");
        }
        protected override void OnExit(ExitEventArgs e)
        {
            log.Info("<<<========================End==");
            base.OnExit(e);
        }

        private void SubscriptComNodes()
        {

            ComTubeNodeComponent[] tubeNodeComponents = ComProcessNodeComponent.Instance.TubeNodeComponents;
            for (byte tubeIndex = 1; tubeIndex <= tubeNodeComponents.Length; ++tubeIndex)
            {
                List<OpcNode> opcSubscriptNodes = new List<OpcNode>();
                ComTubeNodeComponent tubeNodeComponent = tubeNodeComponents[tubeIndex - 1];
                ComFurnaceNodeComponent furnaceNodeComponent = tubeNodeComponent.FurnaceNodeComponent;
                ComTemperNodeComponent[] temperNodeComponents = furnaceNodeComponent.TemperNodeComponents;
                for (int j = 0; j < temperNodeComponents.Length; ++j)
                {
                    ComTemperNodeComponent temperNodeComponent = temperNodeComponents[j];
                    opcSubscriptNodes.Add(temperNodeComponent.IntValue);
                    opcSubscriptNodes.Add(temperNodeComponent.ExtValue);
                    opcSubscriptNodes.Add(temperNodeComponent.HeatPower);
                }
                ComMfcNodeComponent mfcNodeComponent = tubeNodeComponent.MfcNodeComponent;
                ComGasNodeComponent[] gasNodeComponents = mfcNodeComponent.GasNodeComponents;
                for (int j = 0; j < gasNodeComponents.Length; ++j)
                {
                    ComGasNodeComponent gasNodeComponent = gasNodeComponents[j];
                    opcSubscriptNodes.Add(gasNodeComponent.CurMeas);
                }
                ComVacuumNodeComponent vacuumNodeComponent = tubeNodeComponent.VacuumNodeComponent;
                ComAnalogNodeComponent[] analogNodeComponents = vacuumNodeComponent.AnalogNodeComponents;
                for (int j = 0; j < analogNodeComponents.Length; ++j)
                {
                    ComAnalogNodeComponent analogNodeComponent = analogNodeComponents[j];
                    opcSubscriptNodes.Add(analogNodeComponent.CurMeas);
                }
                ComPaddleNodeComponent paddleNodeComponent = tubeNodeComponent.PaddleNodeComponent;
                opcSubscriptNodes.Add(paddleNodeComponent.PosAct);
                ComDioNodeComponent dioNodeComponent = tubeNodeComponent.DioNodeComponent;
                opcSubscriptNodes.Add(dioNodeComponent.DigInput);
                opcSubscriptNodes.Add(dioNodeComponent.DigOutput);
                opcSubscriptNodes.Add(dioNodeComponent.Ev);

                opcSubscriptNodes.Add(ComProcessNodeComponent.Instance.Test);
                ComNodeService.Instance.SubscriptComNodes(tubeIndex, opcSubscriptNodes);
            }
        }
    }
}
