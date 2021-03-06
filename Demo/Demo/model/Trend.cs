﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.model
{
    public class Trend
    {
        private List<double[]> mDataPoints;

        public Trend()
        {
            mDataPoints = new List<double[]>();
            for (int i = 0; i < 300; ++i)
            {
                mDataPoints.Add(new double[]{0});
            }
        }

        public List<double[]> DataPoints
        {
            get { return mDataPoints; }
            set { mDataPoints = value; }
        }
    }
}
