using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;

namespace Demo.com
{
    class ComGasNodeComponent
    {
        private OpcNode mCurMeas;
        private OpcNode mCurSp;

        public ComGasNodeComponent(byte tubeIndex, byte gasIndex)
        {
            string sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.Gas{1}.CurMeas", tubeIndex, gasIndex);
            mCurMeas = new OpcNode(sNodeId);
            sNodeId = string.Format("ns=4;s=|var|CODESYS Control for PFC200 SL.Application.TUBE{0}.Gas{1}.CurSp", tubeIndex, gasIndex);
            mCurSp = new OpcNode(sNodeId);
        }

        public OpcNode CurMeas
        {
            get
            {
                return mCurMeas;
            }
        }

        public OpcNode CurSp
        {
            get
            {
                return mCurSp;
            }
        }
    }
}
