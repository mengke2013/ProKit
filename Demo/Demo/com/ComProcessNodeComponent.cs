using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComProcessNodeComponent
    {
        private static ComProcessNodeComponent instance;

        
        private ComTubeNodeComponent[] mTubeNodeComponents;
        private ComTubeGroupConfNodeComponent[] mTubeGroupConfNodeComponents;
        private OpcNode test;


        public static ComProcessNodeComponent Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ComProcessNodeComponent();
                }
                return instance;
            }
        }

        public ComTubeNodeComponent[] TubeNodeComponents
        {
            get
            {
                return mTubeNodeComponents;
            }
        }

        public OpcNode Test
        {
            get
            {
                return test;
            }
        }

        private ComProcessNodeComponent()
        {
            mTubeNodeComponents = new ComTubeNodeComponent[6];
            for (byte tubeIndex = 1; tubeIndex <= 6; ++tubeIndex)
            {
                mTubeNodeComponents[tubeIndex - 1] = new ComTubeNodeComponent(tubeIndex);
            }
            mTubeGroupConfNodeComponents = new ComTubeGroupConfNodeComponent[2];
            for (byte tubeGroupIndex = 1; tubeGroupIndex <= 2; ++tubeGroupIndex)
            {
                mTubeGroupConfNodeComponents[tubeGroupIndex - 1] = new ComTubeGroupConfNodeComponent(tubeGroupIndex);
            }
            test = new OpcNode("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.MES.WaferID");
        }
    }
}
