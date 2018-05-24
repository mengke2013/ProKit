using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.model
{
    public class Dioev
    {
        private int mTubeIndex;
 
        private int mEv;
        private int mDi;
        private int mDo;

        private int mStatus;

        public Dioev()
        {

        }

        public int TubeIndex
        {
            get { return mTubeIndex; }
            set { mTubeIndex = value; }
        }

        public int Ev
        {
            get { return mEv; }
            set { mEv = value; }
        }

        public int Di
        {
            get { return mDi; }
            set { mDi = value; }
        }

        public int Do
        {
            get { return mDo; }
            set { mDo = value; }
        }
    }
}
