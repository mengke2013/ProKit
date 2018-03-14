using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Demo.ui.model
{
    public class TubeTrendViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<double[]> mDataPoints;

        private byte mTubeIndex;
        private TrendPlotType mPlotType; 

        public TubeTrendViewModel()
        {
            
        }

        public List<double[]> DataPoints
        {
            get { return mDataPoints; }
            set { mDataPoints = value; }
        }

        public byte TubeIndex
        {
            get { return mTubeIndex; }
            set { mTubeIndex = value; }
        }

        public TrendPlotType PlotType
        {
            get { return mPlotType; }
            set
            {
                mPlotType = value;
                Notify("PlotType");
            }
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
