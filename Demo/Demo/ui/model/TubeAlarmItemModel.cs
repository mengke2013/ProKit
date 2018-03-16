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

        public TubeAlarmItemModel()
        {
            
        }

        public byte TubeIndex
        {
            get { return mTubeIndex; }
            set { mTubeIndex = value; }
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
