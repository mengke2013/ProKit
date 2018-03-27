using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.model
{
    public class Settings
    {
        private short mGas1MaxValue;
        private short mGas2MaxValue;
        private short mGas5MaxValue;
        private short mGas6MaxValue;
        private short mGas8MaxValue;

        private byte mGas1Ev;
        private byte mGas2Ev;
        private byte mGas5Ev;
        private byte mGas6Ev;
        private byte mGas8Ev;

        private short mAna1MaxValue;
        private short mAna3MaxValue;
        private short mAna4MaxValue;
        private short mAna5MaxValue;
        private short mAna6MaxValue;

        private short mTemperIntKp1;
        private short mTemperIntKp2;
        private short mTemperIntKp3;
        private short mTemperIntKp4;
        private short mTemperIntKp5;
        private short mTemperIntKp6;
        private short mTemperIntTn1;
        private short mTemperIntTn2;
        private short mTemperIntTn3;
        private short mTemperIntTn4;
        private short mTemperIntTn5;
        private short mTemperIntTn6;
        private short mTemperIntTd1;
        private short mTemperIntTd2;
        private short mTemperIntTd3;
        private short mTemperIntTd4;
        private short mTemperIntTd5;
        private short mTemperIntTd6;

        private short mTemperExtKp1;
        private short mTemperExtKp2;
        private short mTemperExtKp3;
        private short mTemperExtKp4;
        private short mTemperExtKp5;
        private short mTemperExtKp6;
        private short mTemperExtTn1;
        private short mTemperExtTn2;
        private short mTemperExtTn3;
        private short mTemperExtTn4;
        private short mTemperExtTn5;
        private short mTemperExtTn6;
        private short mTemperExtTd1;
        private short mTemperExtTd2;
        private short mTemperExtTd3;
        private short mTemperExtTd4;
        private short mTemperExtTd5;
        private short mTemperExtTd6;

        private short mVacuumKp;
        private short mVacuumTn;
        private short mVacuumTd;

        private short mMaxPressure;
        private short mMinPressure;
        private short mMaxTemper;
        private short mMaxTemper5;
        private short mMinTemper5;
        private short mMaxPump;

        private int mOffset;
        private int mPositionDev;
        private int mClosePosition;

        private int mDi;

        private string mGas1Name;
        private string mGas2Name;
        private string mGas5Name;
        private string mGas6Name;
        private string mGas8Name;
        private string mAna1Name;
        private string mAna3Name;
        private string mAna4Name;
        private string mAna5Name;
        private string mAna6Name;

        private string[] mEvNames;
        private string[] mDiNames;
        private string[] mDoNames;

        public Settings()
        {
            mEvNames = new string[32];
            mDiNames = new string[32];
            mDoNames = new string[16];
        }

        public short Gas1MaxValue
        {
            get { return mGas1MaxValue; }
            set { mGas1MaxValue = value; }
        }

        public short Gas2MaxValue
        {
            get { return mGas2MaxValue; }
            set { mGas2MaxValue = value; }
        }

        public short Gas5MaxValue
        {
            get { return mGas5MaxValue; }
            set { mGas5MaxValue = value; }
        }

        public short Gas6MaxValue
        {
            get { return mGas6MaxValue; }
            set { mGas6MaxValue = value; }
        }

        public short Gas8MaxValue
        {
            get { return mGas8MaxValue; }
            set { mGas8MaxValue = value; }
        }

        public byte Gas1Ev
        {
            get { return mGas1Ev; }
            set { mGas1Ev = value; }
        }

        public byte Gas2Ev
        {
            get { return mGas2Ev; }
            set { mGas2Ev = value; }
        }

        public byte Gas5Ev
        {
            get { return mGas5Ev; }
            set { mGas5Ev = value; }
        }

        public byte Gas6Ev
        {
            get { return mGas6Ev; }
            set { mGas6Ev = value; }
        }

        public byte Gas8Ev
        {
            get { return mGas8Ev; }
            set { mGas8Ev = value; }
        }

        public short Ana1MaxValue
        {
            get { return mAna1MaxValue; }
            set { mAna1MaxValue = value; }
        }

        public short Ana3MaxValue
        {
            get { return mAna3MaxValue; }
            set { mAna3MaxValue = value; }
        }

        public short Ana4MaxValue
        {
            get { return mAna4MaxValue; }
            set { mAna4MaxValue = value; }
        }

        public short Ana5MaxValue
        {
            get { return mAna5MaxValue; }
            set { mAna5MaxValue = value; }
        }

        public short Ana6MaxValue
        {
            get { return mAna6MaxValue; }
            set { mAna6MaxValue = value; }
        }

        public short TemperIntKp1
        {
            get { return mTemperIntKp1; }
            set { mTemperIntKp1 = value; }
        }

        public short TemperIntKp2
        {
            get { return mTemperIntKp2; }
            set { mTemperIntKp2 = value; }
        }

        public short TemperIntKp3
        {
            get { return mTemperIntKp3; }
            set { mTemperIntKp3 = value; }
        }

        public short TemperIntKp4
        {
            get { return mTemperIntKp4; }
            set { mTemperIntKp4 = value; }
        }

        public short TemperIntKp5
        {
            get { return mTemperIntKp5; }
            set { mTemperIntKp5 = value; }
        }

        public short TemperIntKp6
        {
            get { return mTemperIntKp6; }
            set { mTemperIntKp6 = value; }
        }

        public short TemperIntTn1
        {
            get { return mTemperIntTn1; }
            set { mTemperIntTn1 = value; }
        }

        public short TemperIntTn2
        {
            get { return mTemperIntTn2; }
            set { mTemperIntTn2 = value; }
        }

        public short TemperIntTn3
        {
            get { return mTemperIntTn3; }
            set { mTemperIntTn3 = value; }
        }

        public short TemperIntTn4
        {
            get { return mTemperIntTn4; }
            set { mTemperIntTn4 = value; }
        }

        public short TemperIntTn5
        {
            get { return mTemperIntTn5; }
            set { mTemperIntTn5 = value; }
        }

        public short TemperIntTn6
        {
            get { return mTemperIntTn6; }
            set { mTemperIntTn6 = value; }
        }

        public short TemperIntTd1
        {
            get { return mTemperIntTd1; }
            set { mTemperIntTd1 = value; }
        }

        public short TemperIntTd2
        {
            get { return mTemperIntTd2; }
            set { mTemperIntTd2 = value; }
        }

        public short TemperIntTd3
        {
            get { return mTemperIntTd3; }
            set { mTemperIntTd3 = value; }
        }

        public short TemperIntTd4
        {
            get { return mTemperIntTd4; }
            set { mTemperIntTd4 = value; }
        }

        public short TemperIntTd5
        {
            get { return mTemperIntTd5; }
            set { mTemperIntTd5 = value; }
        }

        public short TemperIntTd6
        {
            get { return mTemperIntTd6; }
            set { mTemperIntTd6 = value; }
        }

        public short TemperExtKp1
        {
            get { return mTemperExtKp1; }
            set { mTemperExtKp1 = value; }
        }

        public short TemperExtKp2
        {
            get { return mTemperExtKp2; }
            set { mTemperExtKp2 = value; }
        }

        public short TemperExtKp3
        {
            get { return mTemperExtKp3; }
            set { mTemperExtKp3 = value; }
        }

        public short TemperExtKp4
        {
            get { return mTemperExtKp4; }
            set { mTemperExtKp4 = value; }
        }

        public short TemperExtKp5
        {
            get { return mTemperExtKp5; }
            set { mTemperExtKp5 = value; }
        }

        public short TemperExtKp6
        {
            get { return mTemperExtKp6; }
            set { mTemperExtKp6 = value; }
        }

        public short TemperExtTn1
        {
            get { return mTemperExtTn1; }
            set { mTemperExtTn1 = value; }
        }

        public short TemperExtTn2
        {
            get { return mTemperExtTn2; }
            set { mTemperExtTn2 = value; }
        }

        public short TemperExtTn3
        {
            get { return mTemperExtTn3; }
            set { mTemperExtTn3 = value; }
        }

        public short TemperExtTn4
        {
            get { return mTemperExtTn4; }
            set { mTemperExtTn4 = value; }
        }

        public short TemperExtTn5
        {
            get { return mTemperExtTn5; }
            set { mTemperExtTn5 = value; }
        }

        public short TemperExtTn6
        {
            get { return mTemperExtTn6; }
            set { mTemperExtTn6 = value; }
        }

        public short TemperExtTd1
        {
            get { return mTemperExtTd1; }
            set { mTemperExtTd1 = value; }
        }

        public short TemperExtTd2
        {
            get { return mTemperExtTd2; }
            set { mTemperExtTd2 = value; }
        }

        public short TemperExtTd3
        {
            get { return mTemperExtTd3; }
            set { mTemperExtTd3 = value; }
        }

        public short TemperExtTd4
        {
            get { return mTemperExtTd4; }
            set { mTemperExtTd4 = value; }
        }

        public short TemperExtTd5
        {
            get { return mTemperExtTd5; }
            set { mTemperExtTd5 = value; }
        }

        public short TemperExtTd6
        {
            get { return mTemperExtTd6; }
            set { mTemperExtTd6 = value; }
        }

        public short VacuumKp
        {
            get { return mVacuumKp; }
            set { mVacuumKp = value; }
        }

        public short VacuumTn
        {
            get { return mVacuumTn; }
            set { mVacuumTn = value; }
        }

        public short VacuumTd
        {
            get { return mVacuumTd; }
            set { mVacuumTd = value; }
        }

        public short MaxPressure
        {
            get { return mMaxPressure; }
            set { mMaxPressure = value; }
        }

        public short MinPressure
        {
            get { return mMinPressure; }
            set { mMinPressure = value; }
        }

        public short MaxTemper
        {
            get { return mMaxTemper; }
            set { mMaxTemper = value; }
        }

        public short MaxTemper5
        {
            get { return mMaxTemper5; }
            set { mMaxTemper5 = value; }
        }

        public short MinTemper5
        {
            get { return mMinTemper5; }
            set { mMinTemper5 = value; }
        }

        public short MaxPump
        {
            get { return mMaxPump; }
            set { mMaxPump = value; }
        }

        public int Offset
        {
            get { return mOffset; }
            set { mOffset = value; }
        }

        public int PositionDev
        {
            get { return mPositionDev; }
            set { mPositionDev = value; }
        }

        public int ClosePosition
        {
            get { return mClosePosition; }
            set { mClosePosition = value; }
        }

        public int Di
        {
            get { return mDi; }
            set { mDi = value; }
        }

        public string Gas1Name
        {
            get { return mGas1Name; }
            set { mGas1Name = value; }
        }

        public string Gas2Name
        {
            get { return mGas2Name; }
            set { mGas2Name = value; }
        }

        public string Gas5Name
        {
            get { return mGas5Name; }
            set { mGas5Name = value; }
        }

        public string Gas6Name
        {
            get { return mGas6Name; }
            set { mGas6Name = value; }
        }

        public string Gas8Name
        {
            get { return mGas8Name; }
            set { mGas8Name = value; }
        }

        public string Ana1Name
        {
            get { return mAna1Name; }
            set { mAna1Name = value; }
        }

        public string Ana3Name
        {
            get { return mAna3Name; }
            set { mAna3Name = value; }
        }

        public string Ana4Name
        {
            get { return mAna4Name; }
            set { mAna4Name = value; }
        }

        public string Ana5Name
        {
            get { return mAna5Name; }
            set { mAna5Name = value; }
        }

        public string Ana6Name
        {
            get { return mAna6Name; }
            set { mAna6Name = value; }
        }

        public string[] EvNames
        {
            get { return mEvNames; }
        }

        public string[] DiNames
        {
            get { return mDiNames; }
        }

        public string[] DoNames
        {
            get { return mDoNames; }
        }
    }
}
