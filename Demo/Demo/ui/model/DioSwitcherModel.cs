using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Demo.ui.model
{
    public class DioSwitcherModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool mSwitchOn;

        protected void Notify(string propNameForAge)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propNameForAge));
            }
        }

        public DioSwitcherModel()
        {
            mSwitchOn = false;
        }

        public bool SwitchOn
        {
            get { return mSwitchOn; }
            set
            {
                mSwitchOn = value;
                Notify("SwitchOn");
            }
        }
    }
}
