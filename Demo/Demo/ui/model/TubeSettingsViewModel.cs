using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Demo.utilities;

namespace Demo.ui.model
{
    public class TubeSettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private short mGas1MaxValue = 0;
        private short mGas2MaxValue = 0;
        private short mGas5MaxValue = 0;
        private short mGas6MaxValue = 0;
        private short mGas8MaxValue = 0;
        private short mAna1MaxValue = 0;
        private short mAna3MaxValue = 0;
        private short mAna4MaxValue = 0;
        private short mAna5MaxValue = 0;
        private short mAna6MaxValue = 0;
        private short mVacuumKp = 0;
        private short mVacuumTn = 0;
        private short mVacuumTd = 0;
        private byte mGas1Ev = 0;
        private byte mGas2Ev = 0;
        private byte mGas5Ev = 0;
        private byte mGas6Ev = 0;
        private byte mGas8Ev = 0;

        private short mTemperIntKp1 = 0;
        private short mTemperIntKp2 = 0;
        private short mTemperIntKp3 = 0;
        private short mTemperIntKp4 = 0;
        private short mTemperIntKp5 = 0;
        private short mTemperIntKp6 = 0;
        private short mTemperIntTn1 = 0;
        private short mTemperIntTn2 = 0;
        private short mTemperIntTn3 = 0;
        private short mTemperIntTn4 = 0;
        private short mTemperIntTn5 = 0;
        private short mTemperIntTn6 = 0;
        private short mTemperIntTd1 = 0;
        private short mTemperIntTd2 = 0;
        private short mTemperIntTd3 = 0;
        private short mTemperIntTd4 = 0;
        private short mTemperIntTd5 = 0;
        private short mTemperIntTd6 = 0;

        private short mTemperExtKp1 = 0;
        private short mTemperExtKp2 = 0;
        private short mTemperExtKp3 = 0;
        private short mTemperExtKp4 = 0;
        private short mTemperExtKp5 = 0;
        private short mTemperExtKp6 = 0;
        private short mTemperExtTn1 = 0;
        private short mTemperExtTn2 = 0;
        private short mTemperExtTn3 = 0;
        private short mTemperExtTn4 = 0;
        private short mTemperExtTn5 = 0;
        private short mTemperExtTn6 = 0;
        private short mTemperExtTd1 = 0;
        private short mTemperExtTd2 = 0;
        private short mTemperExtTd3 = 0;
        private short mTemperExtTd4 = 0;
        private short mTemperExtTd5 = 0;
        private short mTemperExtTd6 = 0;

        private short mMaxPressure = 0;
        private short mMinPressure = 0;
        private short mMaxTemper = 0;
        private short mMaxTemper5 = 0;
        private short mMinTemper5 = 0;
        private short mMaxPump = 0;

        private int mDi;

        private int mOffset;
        private int mPositionDev;
        private int mClosePosition;

        public TubeSettingsViewModel()
        {

        }

        public int Di
        {
            get { return mDi; }
            set
            {
                mDi = value;
                Notify("Di1");
                Notify("Di2");
                Notify("Di3");
                Notify("Di4");
                Notify("Di5");
                Notify("Di6");
                Notify("Di7");
                Notify("Di8");
                Notify("Di9");
                Notify("Di10");
                Notify("Di11");
                Notify("Di12");
                Notify("Di13");
                Notify("Di14");
                Notify("Di15");
            }
        }

        public bool Di1
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 0);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 0, value);
                Notify("Di1");
            }
        }

        public bool Di2
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 1);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 1, value);
                Notify("Di2");
            }
        }

        public bool Di3
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 2);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 2, value);
                Notify("Di3");
            }
        }

        public bool Di4
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 3);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 3, value);
                Notify("Di4");
            }
        }

        public bool Di5
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 4);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 4, value);
                Notify("Di5");
            }
        }

        public bool Di6
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 5);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 5, value);
                Notify("Di6");
            }
        }
        public bool Di7
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 6);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 6, value);
                Notify("Di7");
            }
        }

        public bool Di8
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 7);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 7, value);
                Notify("Di8");
            }
        }

        public bool Di9
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 8);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 8, value);
                Notify("Di9");
            }
        }

        public bool Di10
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 9);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 9, value);
                Notify("Di10");
            }
        }

        public bool Di11
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 10);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 10, value);
                Notify("Di11");
            }
        }

        public bool Di12
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 11);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 11, value);
                Notify("Di12");
            }
        }

        public bool Di13
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 12);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 12, value);
                Notify("Di13");
            }
        }

        public bool Di14
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 13);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 13, value);
                Notify("Di14");
            }
        }

        public bool Di15
        {
            get
            {
                return BitUtility.GetBitValue(mDi, 14);
            }
            set
            {
                mDi = BitUtility.SetBitValue(mDi, 14, value);
                Notify("Di15");
            }
        }

        public short Gas1MaxValue
        {
            get { return mGas1MaxValue; }
            set
            {
                mGas1MaxValue = value;
                Notify("Gas1MaxValue");
            }
        }

        public short Gas2MaxValue
        {
            get { return mGas2MaxValue; }
            set
            {
                mGas2MaxValue = value;
                Notify("Gas2MaxValue");
            }
        }

        public short Gas5MaxValue
        {
            get { return mGas5MaxValue; }
            set
            {
                mGas5MaxValue = value;
                Notify("Gas5MaxValue");
            }
        }

        public short Gas6MaxValue
        {
            get { return mGas6MaxValue; }
            set
            {
                mGas6MaxValue = value;
                Notify("Gas6MaxValue");
            }
        }

        public short Gas8MaxValue
        {
            get { return mGas8MaxValue; }
            set
            {
                mGas8MaxValue = value;
                Notify("Gas8MaxValue");
            }
        }

        public short Ana1MaxValue
        {
            get { return mAna1MaxValue; }
            set
            {
                mAna1MaxValue = value;
                Notify("Ana1MaxValue");
            }
        }

        public short Ana3MaxValue
        {
            get { return mAna3MaxValue; }
            set
            {
                mAna3MaxValue = value;
                Notify("Ana3MaxValue");
            }
        }

        public short Ana4MaxValue
        {
            get { return mAna4MaxValue; }
            set
            {
                mAna4MaxValue = value;
                Notify("Ana4MaxValue");
            }
        }

        public short Ana5MaxValue
        {
            get { return mAna5MaxValue; }
            set
            {
                mAna5MaxValue = value;
                Notify("Ana5MaxValue");
            }
        }

        public short Ana6MaxValue
        {
            get { return mAna6MaxValue; }
            set
            {
                mAna6MaxValue = value;
                Notify("Ana6MaxValue");
            }
        }

        public short VacuumKp
        {
            get { return mVacuumKp; }
            set
            {
                mVacuumKp = value;
                Notify("VacuumKp");
            }
        }

        public short VacuumTn
        {
            get { return mVacuumTn; }
            set
            {
                mVacuumTn = value;
                Notify("VacuumTn");
            }
        }

        public short VacuumTd
        {
            get { return mVacuumTd; }
            set
            {
                mVacuumTd = value;
                Notify("VacuumTd");
            }
        }

        public byte Gas1Ev
        {
            get { return mGas1Ev; }
            set
            {
                mGas1Ev = value;
                Notify("Gas1Ev");
            }
        }

        public byte Gas2Ev
        {
            get { return mGas2Ev; }
            set
            {
                mGas2Ev = value;
                Notify("Gas2Ev");
            }
        }

        public byte Gas5Ev
        {
            get { return mGas5Ev; }
            set
            {
                mGas5Ev = value;
                Notify("Gas5Ev");
            }
        }

        public byte Gas6Ev
        {
            get { return mGas6Ev; }
            set
            {
                mGas6Ev = value;
                Notify("Gas6Ev");
            }
        }

        public byte Gas8Ev
        {
            get { return mGas8Ev; }
            set
            {
                mGas8Ev = value;
                Notify("Gas8Ev");
            }
        }

        public short TemperIntKp1
        {
            get { return mTemperIntKp1; }
            set
            {
                mTemperIntKp1 = value;
                Notify("TemperIntKp1");
            }
        }

        public short TemperIntKp2
        {
            get { return mTemperIntKp2; }
            set
            {
                mTemperIntKp2 = value;
                Notify("TemperIntKp2");
            }
        }

        public short TemperIntKp3
        {
            get { return mTemperIntKp3; }
            set
            {
                mTemperIntKp3 = value;
                Notify("TemperIntKp3");
            }
        }

        public short TemperIntKp4
        {
            get { return mTemperIntKp4; }
            set
            {
                mTemperIntKp4 = value;
                Notify("TemperIntKp4");
            }
        }

        public short TemperIntKp5
        {
            get { return mTemperIntKp5; }
            set
            {
                mTemperIntKp5 = value;
                Notify("TemperIntKp5");
            }
        }

        public short TemperIntKp6
        {
            get { return mTemperIntKp6; }
            set
            {
                mTemperIntKp6 = value;
                Notify("TemperIntKp6");
            }
        }

        public short TemperIntTn1
        {
            get { return mTemperIntTn1; }
            set
            {
                mTemperIntTn1 = value;
                Notify("TemperIntTn1");
            }
        }

        public short TemperIntTn2
        {
            get { return mTemperIntTn2; }
            set
            {
                mTemperIntTn2 = value;
                Notify("TemperIntTn2");
            }
        }

        public short TemperIntTn3
        {
            get { return mTemperIntTn3; }
            set
            {
                mTemperIntTn3 = value;
                Notify("TemperIntTn3");
            }
        }

        public short TemperIntTn4
        {
            get { return mTemperIntTn4; }
            set
            {
                mTemperIntTn4 = value;
                Notify("TemperIntTn4");
            }
        }

        public short TemperIntTn5
        {
            get { return mTemperIntTn5; }
            set
            {
                mTemperIntTn5 = value;
                Notify("TemperIntTn5");
            }
        }

        public short TemperIntTn6
        {
            get { return mTemperIntTn6; }
            set
            {
                mTemperIntTn6 = value;
                Notify("TemperIntTn6");
            }
        }

        public short TemperIntTd1
        {
            get { return mTemperIntTd1; }
            set
            {
                mTemperIntTd1 = value;
                Notify("TemperIntTd1");
            }
        }

        public short TemperIntTd2
        {
            get { return mTemperIntTd2; }
            set
            {
                mTemperIntTd2 = value;
                Notify("TemperIntTd2");
            }
        }

        public short TemperIntTd3
        {
            get { return mTemperIntTd3; }
            set
            {
                mTemperIntTd3 = value;
                Notify("TemperIntTd3");
            }
        }

        public short TemperIntTd4
        {
            get { return mTemperIntTd4; }
            set
            {
                mTemperIntTd4 = value;
                Notify("TemperIntTd4");
            }
        }

        public short TemperIntTd5
        {
            get { return mTemperIntTd5; }
            set
            {
                mTemperIntTd5 = value;
                Notify("TemperIntTd5");
            }
        }

        public short TemperIntTd6
        {
            get { return mTemperIntTd6; }
            set
            {
                mTemperIntTd6 = value;
                Notify("TemperIntTd6");
            }
        }

        public short TemperExtKp1
        {
            get { return mTemperExtKp1; }
            set
            {
                mTemperExtKp1 = value;
                Notify("TemperExtKp1");
            }
        }

        public short TemperExtKp2
        {
            get { return mTemperExtKp2; }
            set
            {
                mTemperExtKp2 = value;
                Notify("TemperExtKp2");
            }
        }

        public short TemperExtKp3
        {
            get { return mTemperExtKp3; }
            set
            {
                mTemperExtKp3 = value;
                Notify("TemperExtKp3");
            }
        }

        public short TemperExtKp4
        {
            get { return mTemperExtKp4; }
            set
            {
                mTemperExtKp4 = value;
                Notify("TemperExtKp4");
            }
        }

        public short TemperExtKp5
        {
            get { return mTemperExtKp5; }
            set
            {
                mTemperExtKp5 = value;
                Notify("TemperExtKp5");
            }
        }

        public short TemperExtKp6
        {
            get { return mTemperExtKp6; }
            set
            {
                mTemperExtKp6 = value;
                Notify("TemperExtKp6");
            }
        }

        public short TemperExtTn1
        {
            get { return mTemperExtTn1; }
            set
            {
                mTemperExtTn1 = value;
                Notify("TemperExtTn1");
            }
        }

        public short TemperExtTn2
        {
            get { return mTemperExtTn2; }
            set
            {
                mTemperExtTn2 = value;
                Notify("TemperExtTn2");
            }
        }

        public short TemperExtTn3
        {
            get { return mTemperExtTn3; }
            set
            {
                mTemperExtTn3 = value;
                Notify("TemperExtTn3");
            }
        }

        public short TemperExtTn4
        {
            get { return mTemperExtTn4; }
            set
            {
                mTemperExtTn4 = value;
                Notify("TemperExtTn4");
            }
        }

        public short TemperExtTn5
        {
            get { return mTemperExtTn5; }
            set
            {
                mTemperExtTn5 = value;
                Notify("TemperExtTn5");
            }
        }

        public short TemperExtTn6
        {
            get { return mTemperExtTn6; }
            set
            {
                mTemperExtTn6 = value;
                Notify("TemperExtTn6");
            }
        }

        public short TemperExtTd1
        {
            get { return mTemperExtTd1; }
            set
            {
                mTemperExtTd1 = value;
                Notify("TemperExtTd1");
            }
        }

        public short TemperExtTd2
        {
            get { return mTemperExtTd2; }
            set
            {
                mTemperExtTd2 = value;
                Notify("TemperExtTd2");
            }
        }

        public short TemperExtTd3
        {
            get { return mTemperExtTd3; }
            set
            {
                mTemperExtTd3 = value;
                Notify("TemperExtTd3");
            }
        }

        public short TemperExtTd4
        {
            get { return mTemperExtTd4; }
            set
            {
                mTemperExtTd4 = value;
                Notify("TemperExtTd4");
            }
        }

        public short TemperExtTd5
        {
            get { return mTemperExtTd5; }
            set
            {
                mTemperExtTd5 = value;
                Notify("TemperExtTd5");
            }
        }

        public short TemperExtTd6
        {
            get { return mTemperExtTd6; }
            set
            {
                mTemperExtTd6 = value;
                Notify("TemperExtTd6");
            }
        }

        public short MaxPressure
        {
            get { return mMaxPressure; }
            set
            {
                mMaxPressure = value;
                Notify("MaxPressure");
            }
        }

        public short MinPressure
        {
            get { return mMinPressure; }
            set
            {
                mMinPressure = value;
                Notify("MinPressure");
            }
        }

        public short MaxTemper
        {
            get { return mMaxTemper; }
            set
            {
                mMaxTemper = value;
                Notify("MaxTemper");
            }
        }

        public short MaxTemper5
        {
            get { return mMaxTemper5; }
            set
            {
                mMaxTemper5 = value;
                Notify("MaxTemper5");
            }
        }

        public short MinTemper5
        {
            get { return mMinTemper5; }
            set
            {
                mMinTemper5 = value;
                Notify("MinTemper5");
            }
        }

        public short MaxPump
        {
            get { return mMaxPump; }
            set
            {
                mMaxPump = value;
                Notify("MaxPump");
            }
        }

        public int Offset
        {
            get { return mOffset; }
            set
            {
                mOffset = value;
                Notify("Offset");
            }
        }

        public int PositionDev
        {
            get { return mPositionDev; }
            set
            {
                mPositionDev = value;
                Notify("PositionDev");
            }
        }

        public int ClosePosition
        {
            get { return mClosePosition; }
            set
            {
                mClosePosition = value;
                Notify("ClosePosition");
            }
        }

        public void Notify(string propName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
