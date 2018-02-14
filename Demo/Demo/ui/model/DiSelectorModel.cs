using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Demo.ui.model
{
    public class DiSelectorModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propNameForAge)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propNameForAge));
            }
        }

        public DiSelectorModel()
        {
            mDiTypeId = 0;
        }

        string mDiTypeLabel;
        public string DiTypeLabel
        {
            get { return this.mDiTypeLabel; }
            set
            {
                if (this.mDiTypeLabel == value)
                { return; }
                this.mDiTypeLabel = value;
                Notify("DiTypeLabel");
            }
        }

        byte mDiTypeId;
        public byte DiTypeId
        {
            get { return this.mDiTypeId; }
            set
            {
                if (this.mDiTypeId == value)
                { return; }
                this.mDiTypeId = value;
                Notify("DiTypeId");
            }
        }
    }

    public class DiSelectorModels : ObservableCollection<DiSelectorModel> { }
}
