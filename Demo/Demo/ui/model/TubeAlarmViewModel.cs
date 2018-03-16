using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Demo.ui.model
{
    public class TubeAlarmViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<TubeAlarmItemModel> mItemModelList;
        private byte mTubeIndex;

        public TubeAlarmViewModel()
        {
            
        }

        public byte TubeIndex
        {
            get { return mTubeIndex; }
            set { mTubeIndex = value; }
        }

        public List<TubeAlarmItemModel> ItemModelList
        {
            get { return mItemModelList; }
            set { mItemModelList = value; }
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
