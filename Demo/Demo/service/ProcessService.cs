using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;
using Demo.com;

namespace Demo.service
{
    class ProcessService
    {
        private static ProcessService instance;

        private ProcessService() { }

        public static ProcessService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProcessService();
                }
                return instance;
            }
        }

        public void StartProcess(byte tubeIndex)
        {
            List<OpcNode> opcWriteNodes = new List<OpcNode>();
            bool comCommand = true;
            ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchLoad.Value = comCommand;

            opcWriteNodes.Add(ComProcessNodeComponent.Instance.TubeNodeComponents[tubeIndex - 1].CommandNodeComponent.TchStart);
            ComNodeService.Instance.WriteComNodes(tubeIndex, opcWriteNodes);
        }

        public int GetRemainingTime()
        {
            return 0;
        }
    }
}
