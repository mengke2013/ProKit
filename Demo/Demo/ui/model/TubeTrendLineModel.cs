using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ui.model
{
    public class TubeTrendLineModel
    {
        private double mX1;
        private double mX2;
        private double mY1;
        private double mY2;

        public TubeTrendLineModel(double x1, double y1, double x2, double y2)
        {
            mX1 = x1;
            mY1 = y1;
            mX2 = x2;
            mY2 = y2;
        }

        public double X1
        {
            get { return mX1; }
            set { mX1 = value; }
        }

        public double X2
        {
            get { return mX2; }
            set { mX2 = value; }
        }

        public double Y1
        {
            get { return mY1; }
            set { mY1 = value; }
        }

        public double Y2
        {
            get { return mY2; }
            set { mY2 = value; }
        }
    }
}
