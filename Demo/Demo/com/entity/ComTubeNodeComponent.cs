﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.com
{
    class ComTubeNodeComponent
    {
        ComFurnaceNodeComponent mFurnaceNodeComponent;
        ComMfcNodeComponent mMfcNodeComponent;
        ComVacuumNodeComponent mVacuumNodeComponent;
        ComPaddleNodeComponent mPaddleNodeComponent;
        ComDioNodeComponent mDioNodeComponent;
        ComCommandNodeComponent mCommandNodeComponent;
        ComTubeStatusInfoNodeComponent mStatusInfoNodeComponent;


        public ComTubeNodeComponent(byte tubeIndex)
        {
            mFurnaceNodeComponent = new ComFurnaceNodeComponent(tubeIndex);
            mMfcNodeComponent = new ComMfcNodeComponent(tubeIndex);
            mVacuumNodeComponent = new ComVacuumNodeComponent(tubeIndex);
            mPaddleNodeComponent = new ComPaddleNodeComponent(tubeIndex);
            mDioNodeComponent = new ComDioNodeComponent(tubeIndex);
            mCommandNodeComponent = new ComCommandNodeComponent(tubeIndex);
            mStatusInfoNodeComponent = new ComTubeStatusInfoNodeComponent(tubeIndex);
        }

        public ComFurnaceNodeComponent FurnaceNodeComponent
        {
            get
            {
                return mFurnaceNodeComponent;
            }
        }

        public ComVacuumNodeComponent VacuumNodeComponent
        {
            get
            {
                return mVacuumNodeComponent;
            }
        }

        public ComMfcNodeComponent MfcNodeComponent
        {
            get
            {
                return mMfcNodeComponent;
            }
        }

        public ComPaddleNodeComponent PaddleNodeComponent
        {
            get
            {
                return mPaddleNodeComponent;
            }
        }

        public ComDioNodeComponent DioNodeComponent
        {
            get
            {
                return mDioNodeComponent;
            }
        }

        public ComCommandNodeComponent CommandNodeComponent
        {
            get
            {
                return mCommandNodeComponent;
            }
        }

        public ComTubeStatusInfoNodeComponent StatusInfoNodeComponent
        {
            get
            {
                return mStatusInfoNodeComponent;
            }
        }
    }
}
