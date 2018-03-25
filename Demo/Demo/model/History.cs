using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.model
{
    public class History
    {
        private string mName;
        private Object mOldValue;
        private object mNewValue;
        private DateTime mTime;

        public History(string name)
        {
            mName = name;
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public Object OldValue
        {
            get { return mOldValue; }
            set { mOldValue = value; }
        }

        public Object NewValue
        {
            get { return mNewValue; }
            set { mNewValue = value; }
        }

        public DateTime Time
        {
            get { return mTime; }
            set { mTime = value; }
        }
    }
}
