using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Demo.ui.model
{
    public class ProgressDlgModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int mProgress;
        private int mMaxValue;

        void Notify(string propName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }

        }

        public int Progress
        {
            get { return mProgress; }
            set
            {
                mProgress = value;
                Notify("Progress");
            }
        }

        public int MaxValue
        {
            get { return mMaxValue; }
            set
            {
                mMaxValue = value;
                Notify("MaxValue");
            }
        }

    }


}
