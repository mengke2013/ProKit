using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ui.model
{
    class TubeSettingsViewModel
    {
        private string mGas1MaxValue = "10";
        private string mGas2MaxValue = "10";
        private string mGas5MaxValue = "10";
        private string mGas6MaxValue = "10";
        private string mGas8MaxValue = "10";
        private string mAna1MaxValue = "20";
        private string mAna3MaxValue = "20";
        private string mAna4MaxValue = "20";
        private string mAna5MaxValue = "20";
        private string mAna6MaxValue = "20";
        private string mVacuumKp = "30";
        private string mVacuumTn = "30";
        private string mVacuumTd = "30";
        private string mGas1Ev = "1";
        private string mGas2Ev = "2";
        private string mGas5Ev = "3";
        private string mGas6Ev = "4";
        private string mGas8Ev = "5";

        private string mTemperIntKp1 = "10";
        private string mTemperIntKp2 = "10";
        private string mTemperIntKp3 = "10";
        private string mTemperIntKp4 = "10";
        private string mTemperIntKp5 = "10";
        private string mTemperIntKp6 = "10";
        private string mTemperIntTn1 = "10";
        private string mTemperIntTn2 = "10";
        private string mTemperIntTn3 = "10";
        private string mTemperIntTn4 = "10";
        private string mTemperIntTn5 = "10";
        private string mTemperIntTn6 = "10";
        private string mTemperIntTd1 = "10";
        private string mTemperIntTd2 = "10";
        private string mTemperIntTd3 = "10";
        private string mTemperIntTd4 = "10";
        private string mTemperIntTd5 = "10";
        private string mTemperIntTd6 = "10";

        private string mTemperExtKp1 = "10";
        private string mTemperExtKp2 = "10";
        private string mTemperExtKp3 = "10";
        private string mTemperExtKp4 = "10";
        private string mTemperExtKp5 = "10";
        private string mTemperExtKp6 = "10";
        private string mTemperExtTn1 = "10";
        private string mTemperExtTn2 = "10";
        private string mTemperExtTn3 = "10";
        private string mTemperExtTn4 = "10";
        private string mTemperExtTn5 = "10";
        private string mTemperExtTn6 = "10";
        private string mTemperExtTd1 = "10";
        private string mTemperExtTd2 = "10";
        private string mTemperExtTd3 = "10";
        private string mTemperExtTd4 = "10";
        private string mTemperExtTd5 = "10";
        private string mTemperExtTd6 = "10";

        private string mMaxPressure = "100";
        private string mMinPressure = "10";
        private string mMaxTemper = "1000";
        private string mMaxTemper5 = "300";
        private string mMinTemper5 = "10";
        private string mMaxPump = "100";

        private string[] mDIs;

        private string mOffset;
        private string mPositionDev;
        private string mClosePosition;

        public TubeSettingsViewModel()
        {
            mDIs = new string[15];
            for (int i = 0; i < 15; ++i)
            {
                mDIs[i] = "0";
            }

        }

        public string Gas1MaxValue
        {
            get { return mGas1MaxValue; }
            set { mGas1MaxValue = value; }
        }

        public string Gas2MaxValue
        {
            get { return mGas2MaxValue; }
            set { mGas2MaxValue = value; }
        }

        public string Gas5MaxValue
        {
            get { return mGas5MaxValue; }
            set { mGas5MaxValue = value; }
        }

        public string Gas6MaxValue
        {
            get { return mGas6MaxValue; }
            set { mGas6MaxValue = value; }
        }

        public string Gas8MaxValue
        {
            get { return mGas8MaxValue; }
            set { mGas8MaxValue = value; }
        }

        public string Ana1MaxValue
        {
            get { return mAna1MaxValue; }
            set { mAna1MaxValue = value; }
        }

        public string Ana3MaxValue
        {
            get { return mAna3MaxValue; }
            set { mAna3MaxValue = value; }
        }

        public string Ana4MaxValue
        {
            get { return mAna4MaxValue; }
            set { mAna4MaxValue = value; }
        }

        public string Ana5MaxValue
        {
            get { return mAna5MaxValue; }
            set { mAna5MaxValue = value; }
        }

        public string Ana6MaxValue
        {
            get { return mAna6MaxValue; }
            set { mAna6MaxValue = value; }
        }

        public string VacuumKp
        {
            get { return mVacuumKp; }
            set { mVacuumKp = value; }
        }

        public string VacuumTn
        {
            get { return mVacuumTn; }
            set { mVacuumTn = value; }
        }

        public string VacuumTd
        {
            get { return mVacuumTd; }
            set { mVacuumTd = value; }
        }

        public string Gas1Ev
        {
            get { return mGas1Ev; }
            set { mGas1Ev = value; }
        }

        public string Gas2Ev
        {
            get { return mGas2Ev; }
            set { mGas2Ev = value; }
        }

        public string Gas5Ev
        {
            get { return mGas5Ev; }
            set { mGas5Ev = value; }
        }

        public string Gas6Ev
        {
            get { return mGas6Ev; }
            set { mGas6Ev = value; }
        }

        public string Gas8Ev
        {
            get { return mGas8Ev; }
            set { mGas8Ev = value; }
        }

        public string TemperIntKp1
        {
            get { return mTemperIntKp1; }
            set { mTemperIntKp1 = value; }
        }

        public string TemperIntKp2
        {
            get { return mTemperIntKp2; }
            set { mTemperIntKp2 = value; }
        }

        public string TemperIntKp3
        {
            get { return mTemperIntKp3; }
            set { mTemperIntKp3 = value; }
        }

        public string TemperIntKp4
        {
            get { return mTemperIntKp4; }
            set { mTemperIntKp4 = value; }
        }

        public string TemperIntKp5
        {
            get { return mTemperIntKp5; }
            set { mTemperIntKp5 = value; }
        }

        public string TemperIntKp6
        {
            get { return mTemperIntKp6; }
            set { mTemperIntKp6 = value; }
        }

        public string TemperIntTn1
        {
            get { return mTemperIntTn1; }
            set { mTemperIntTn1 = value; }
        }

        public string TemperIntTn2
        {
            get { return mTemperIntTn2; }
            set { mTemperIntTn2 = value; }
        }

        public string TemperIntTn3
        {
            get { return mTemperIntTn3; }
            set { mTemperIntTn3 = value; }
        }

        public string TemperIntTn4
        {
            get { return mTemperIntTn4; }
            set { mTemperIntTn4 = value; }
        }

        public string TemperIntTn5
        {
            get { return mTemperIntTn5; }
            set { mTemperIntTn5 = value; }
        }

        public string TemperIntTn6
        {
            get { return mTemperIntTn6; }
            set { mTemperIntTn6 = value; }
        }

        public string TemperIntTd1
        {
            get { return mTemperIntTd1; }
            set { mTemperIntTd1 = value; }
        }

        public string TemperIntTd2
        {
            get { return mTemperIntTd2; }
            set { mTemperIntTd2 = value; }
        }

        public string TemperIntTd3
        {
            get { return mTemperIntTd3; }
            set { mTemperIntTd3 = value; }
        }

        public string TemperIntTd4
        {
            get { return mTemperIntTd4; }
            set { mTemperIntTd4 = value; }
        }

        public string TemperIntTd5
        {
            get { return mTemperIntTd5; }
            set { mTemperIntTd5 = value; }
        }

        public string TemperIntTd6
        {
            get { return mTemperIntTd6; }
            set { mTemperIntTd6 = value; }
        }

        public string TemperExtKp1
        {
            get { return mTemperExtKp1; }
            set { mTemperExtKp1 = value; }
        }

        public string TemperExtKp2
        {
            get { return mTemperExtKp2; }
            set { mTemperExtKp2 = value; }
        }

        public string TemperExtKp3
        {
            get { return mTemperExtKp3; }
            set { mTemperExtKp3 = value; }
        }

        public string TemperExtKp4
        {
            get { return mTemperExtKp4; }
            set { mTemperExtKp4 = value; }
        }

        public string TemperExtKp5
        {
            get { return mTemperExtKp5; }
            set { mTemperExtKp5 = value; }
        }

        public string TemperExtKp6
        {
            get { return mTemperExtKp6; }
            set { mTemperExtKp6 = value; }
        }

        public string TemperExtTn1
        {
            get { return mTemperExtTn1; }
            set { mTemperExtTn1 = value; }
        }

        public string TemperExtTn2
        {
            get { return mTemperExtTn2; }
            set { mTemperExtTn2 = value; }
        }

        public string TemperExtTn3
        {
            get { return mTemperExtTn3; }
            set { mTemperExtTn3 = value; }
        }

        public string TemperExtTn4
        {
            get { return mTemperExtTn4; }
            set { mTemperExtTn4 = value; }
        }

        public string TemperExtTn5
        {
            get { return mTemperExtTn5; }
            set { mTemperExtTn5 = value; }
        }

        public string TemperExtTn6
        {
            get { return mTemperExtTn6; }
            set { mTemperExtTn6 = value; }
        }

        public string TemperExtTd1
        {
            get { return mTemperExtTd1; }
            set { mTemperExtTd1 = value; }
        }

        public string TemperExtTd2
        {
            get { return mTemperExtTd2; }
            set { mTemperExtTd2 = value; }
        }

        public string TemperExtTd3
        {
            get { return mTemperExtTd3; }
            set { mTemperExtTd3 = value; }
        }

        public string TemperExtTd4
        {
            get { return mTemperExtTd4; }
            set { mTemperExtTd4 = value; }
        }

        public string TemperExtTd5
        {
            get { return mTemperExtTd5; }
            set { mTemperExtTd5 = value; }
        }

        public string TemperExtTd6
        {
            get { return mTemperExtTd6; }
            set { mTemperExtTd6 = value; }
        }

        public string MaxPressure
        {
            get { return mMaxPressure; }
            set { mMaxPressure = value; }
        }

        public string MinPressure
        {
            get { return mMinPressure; }
            set { mMinPressure = value; }
        }

        public string MaxTemper
        {
            get { return mMaxTemper; }
            set { mMaxTemper = value; }
        }

        public string MaxTemper5
        {
            get { return mMaxTemper5; }
            set { mMaxTemper5 = value; }
        }

        public string MinTemper5
        {
            get { return mMinTemper5; }
            set { mMinTemper5 = value; }
        }

        public string MaxPump
        {
            get { return mMaxPump; }
            set { mMaxPump = value; }
        }

        public string DI1 {
            get { return mDIs[0]; }
            set { mDIs[0] = value; }
        }

        public string DI2
        {
            get { return mDIs[1]; }
            set { mDIs[1] = value; }
        }

        public string DI3
        {
            get { return mDIs[2]; }
            set { mDIs[2] = value; }
        }

        public string DI4
        {
            get { return mDIs[3]; }
            set { mDIs[3] = value; }
        }

        public string DI5
        {
            get { return mDIs[4]; }
            set { mDIs[4] = value; }
        }

        public string DI6
        {
            get { return mDIs[5]; }
            set { mDIs[5] = value; }
        }

        public string DI7
        {
            get { return mDIs[6]; }
            set { mDIs[6] = value; }
        }

        public string DI8
        {
            get { return mDIs[7]; }
            set { mDIs[7] = value; }
        }

        public string DI9
        {
            get { return mDIs[8]; }
            set { mDIs[8] = value; }
        }

        public string DI10
        {
            get { return mDIs[9]; }
            set { mDIs[9] = value; }
        }

        public string DI11
        {
            get { return mDIs[10]; }
            set { mDIs[10] = value; }
        }

        public string DI12
        {
            get { return mDIs[11]; }
            set { mDIs[11] = value; }
        }

        public string DI13
        {
            get { return mDIs[12]; }
            set { mDIs[12] = value; }
        }

        public string DI14
        {
            get { return mDIs[13]; }
            set { mDIs[13] = value; }
        }

        public string DI15
        {
            get { return mDIs[14]; }
            set { mDIs[14] = value; }
        }

        public string Offset
        {
            get { return mOffset; }
            set { mOffset = value; }
        }

        public string PositionDev
        {
            get { return mPositionDev; }
            set { mPositionDev = value; }
        }

        public string ClosePosition
        {
            get { return mClosePosition; }
            set { mClosePosition = value; }
        }

    }
}
