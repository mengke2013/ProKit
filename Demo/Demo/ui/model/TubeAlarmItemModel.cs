using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Demo.ui.model
{
    public class TubeAlarmItemModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private byte mTubeIndex;

        private int mID;
        private string mErrorCode;
        private string mDescription;

        public TubeAlarmItemModel()
        {
        }

        public TubeAlarmItemModel(int id, string errorCode, string description)
        {
            this.mID = id;
            this.mErrorCode = errorCode;
            this.mDescription = description;
        }

        public byte TubeIndex
        {
            get { return mTubeIndex; }
            set { mTubeIndex = value; }
        }

        public int ID
        {
            get { return mID; }
            set { mID = value; }
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

        void Notify(string propName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
