using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.model
{
    public class Alarm
    {
        private int mID;
        private int mType;
        private int mTubeIndex;
        private string mErrorCode;
        private string mDescription;

        public Alarm()
        {

        }

        public int ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public int Type
        {
            get { return mType; }
            set { mType = value; }
        }

        public int TubeIndex
        {
            get { return mTubeIndex; }
            set { mTubeIndex = value; }
        }

        public string ErrorCode
        {
            get { return mErrorCode; }
            set { mErrorCode = value; }
        }

        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
    }
}
