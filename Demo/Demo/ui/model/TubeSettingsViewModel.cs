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

        private TubePageStyle mTubePageStyle;
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

        private string[] mEvNames;
        private string[] mDiNames;
        private string[] mDoNames;

        public TubeSettingsViewModel()
        {
            mEvNames = new string[32];
            mDiNames = new string[32];
            mDoNames = new string[16];
        }

        public TubePageStyle TubePageStyle
        {
            get { return mTubePageStyle; }
            set { mTubePageStyle = value; }
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

        public string Gas1Name
        {
            get { return mGas1Name; }
            set
            {
                mGas1Name = value;
                Notify("Gas1Name");
            }
        }

        public string Gas2Name
        {
            get { return mGas2Name; }
            set
            {
                mGas2Name = value;
                Notify("Gas2Name");
            }
        }

        public string Gas5Name
        {
            get { return mGas5Name; }
            set
            {
                mGas5Name = value;
                Notify("Gas5Name");
            }
        }

        public string Gas6Name
        {
            get { return mGas6Name; }
            set
            {
                mGas6Name = value;
                Notify("Gas6Name");
            }
        }

        public string Gas8Name
        {
            get { return mGas8Name; }
            set
            {
                mGas8Name = value;
                Notify("Gas8Name");
            }
        }

        public string Ana1Name
        {
            get { return mAna1Name; }
            set
            {
                mAna1Name = value;
                Notify("Ana1Name");
            }
        }

        public string Ana3Name
        {
            get { return mAna3Name; }
            set
            {
                mAna3Name = value;
                Notify("Ana3Name");
            }
        }

        public string Ana4Name
        {
            get { return mAna4Name; }
            set
            {
                mAna4Name = value;
                Notify("Ana4Name");
            }
        }

        public string Ana5Name
        {
            get { return mAna5Name; }
            set
            {
                mAna5Name = value;
                Notify("Ana5Name");
            }
        }

        public string Ana6Name
        {
            get { return mAna6Name; }
            set
            {
                mAna6Name = value;
                Notify("Ana6Name");
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

        public string EvName1
        {
            get { return mEvNames[0]; }
            set
            {
                mEvNames[0] = value;
                Notify("EvName1");
            }
        }

        public string EvName2
        {
            get { return mEvNames[1]; }
            set
            {
                mEvNames[1] = value;
                Notify("EvName2");
            }
        }

        public string EvName3
        {
            get { return mEvNames[2]; }
            set
            {
                mEvNames[2] = value;
                Notify("EvName3");
            }
        }

        public string EvName4
        {
            get { return mEvNames[3]; }
            set
            {
                mEvNames[3] = value;
                Notify("EvName4");
            }
        }

        public string EvName5
        {
            get { return mEvNames[4]; }
            set
            {
                mEvNames[4] = value;
                Notify("EvName5");
            }
        }

        public string EvName6
        {
            get { return mEvNames[5]; }
            set
            {
                mEvNames[5] = value;
                Notify("EvName6");
            }
        }

        public string EvName7
        {
            get { return mEvNames[6]; }
            set
            {
                mEvNames[6] = value;
                Notify("EvName7");
            }
        }

        public string EvName8
        {
            get { return mEvNames[7]; }
            set
            {
                mEvNames[7] = value;
                Notify("EvName8");
            }
        }

        public string EvName9
        {
            get { return mEvNames[8]; }
            set
            {
                mEvNames[8] = value;
                Notify("EvName9");
            }
        }

        public string EvName10
        {
            get { return mEvNames[9]; }
            set
            {
                mEvNames[9] = value;
                Notify("EvName10");
            }
        }

        public string EvName11
        {
            get { return mEvNames[10]; }
            set
            {
                mEvNames[10] = value;
                Notify("EvName11");
            }
        }

        public string EvName12
        {
            get { return mEvNames[11]; }
            set
            {
                mEvNames[11] = value;
                Notify("EvName12");
            }
        }

        public string EvName13
        {
            get { return mEvNames[12]; }
            set
            {
                mEvNames[12] = value;
                Notify("EvName13");
            }
        }

        public string EvName14
        {
            get { return mEvNames[13]; }
            set
            {
                mEvNames[13] = value;
                Notify("EvName14");
            }
        }

        public string EvName15
        {
            get { return mEvNames[14]; }
            set
            {
                mEvNames[14] = value;
                Notify("EvName15");
            }
        }

        public string EvName16
        {
            get { return mEvNames[15]; }
            set
            {
                mEvNames[15] = value;
                Notify("EvName16");
            }
        }

        public string EvName17
        {
            get { return mEvNames[16]; }
            set
            {
                mEvNames[16] = value;
                Notify("EvName17");
            }
        }

        public string EvName18
        {
            get { return mEvNames[17]; }
            set
            {
                mEvNames[17] = value;
                Notify("EvName18");
            }
        }

        public string EvName19
        {
            get { return mEvNames[18]; }
            set
            {
                mEvNames[18] = value;
                Notify("EvName19");
            }
        }

        public string EvName20
        {
            get { return mEvNames[19]; }
            set
            {
                mEvNames[19] = value;
                Notify("EvName20");
            }
        }

        public string EvName21
        {
            get { return mEvNames[20]; }
            set
            {
                mEvNames[20] = value;
                Notify("EvName21");
            }
        }

        public string EvName22
        {
            get { return mEvNames[21]; }
            set
            {
                mEvNames[21] = value;
                Notify("EvName22");
            }
        }

        public string EvName23
        {
            get { return mEvNames[22]; }
            set
            {
                mEvNames[22] = value;
                Notify("EvName23");
            }
        }

        public string EvName24
        {
            get { return mEvNames[23]; }
            set
            {
                mEvNames[23] = value;
                Notify("EvName24");
            }
        }

        public string EvName25
        {
            get { return mEvNames[24]; }
            set
            {
                mEvNames[24] = value;
                Notify("EvName25");
            }
        }

        public string EvName26
        {
            get { return mEvNames[25]; }
            set
            {
                mEvNames[25] = value;
                Notify("EvName26");
            }
        }

        public string EvName27
        {
            get { return mEvNames[26]; }
            set
            {
                mEvNames[26] = value;
                Notify("EvName27");
            }
        }

        public string EvName28
        {
            get { return mEvNames[27]; }
            set
            {
                mEvNames[27] = value;
                Notify("EvName28");
            }
        }

        public string EvName29
        {
            get { return mEvNames[28]; }
            set
            {
                mEvNames[28] = value;
                Notify("EvName29");
            }
        }

        public string EvName30
        {
            get { return mEvNames[29]; }
            set
            {
                mEvNames[29] = value;
                Notify("EvName30");
            }
        }

        public string EvName31
        {
            get { return mEvNames[30]; }
            set
            {
                mEvNames[30] = value;
                Notify("EvName31");
            }
        }

        public string EvName32
        {
            get { return mEvNames[31]; }
            set
            {
                mEvNames[31] = value;
                Notify("EvName32");
            }
        }

        public string DiName1
        {
            get { return mDiNames[0]; }
            set
            {
                mDiNames[0] = value;
                Notify("DiName1");
            }
        }

        public string DiName2
        {
            get { return mDiNames[1]; }
            set
            {
                mDiNames[1] = value;
                Notify("DiName2");
            }
        }

        public string DiName3
        {
            get { return mDiNames[2]; }
            set
            {
                mDiNames[2] = value;
                Notify("DiName3");
            }
        }

        public string DiName4
        {
            get { return mDiNames[3]; }
            set
            {
                mDiNames[3] = value;
                Notify("DiName4");
            }
        }

        public string DiName5
        {
            get { return mDiNames[4]; }
            set
            {
                mDiNames[4] = value;
                Notify("DiName5");
            }
        }

        public string DiName6
        {
            get { return mDiNames[5]; }
            set
            {
                mDiNames[5] = value;
                Notify("DiName6");
            }
        }

        public string DiName7
        {
            get { return mDiNames[6]; }
            set
            {
                mDiNames[6] = value;
                Notify("DiName7");
            }
        }

        public string DiName8
        {
            get { return mDiNames[7]; }
            set
            {
                mDiNames[7] = value;
                Notify("DiName8");
            }
        }

        public string DiName9
        {
            get { return mDiNames[8]; }
            set
            {
                mDiNames[8] = value;
                Notify("DiName9");
            }
        }

        public string DiName10
        {
            get { return mDiNames[9]; }
            set
            {
                mDiNames[9] = value;
                Notify("DiName10");
            }
        }

        public string DiName11
        {
            get { return mDiNames[10]; }
            set
            {
                mDiNames[10] = value;
                Notify("DiName11");
            }
        }

        public string DiName12
        {
            get { return mDiNames[11]; }
            set
            {
                mDiNames[11] = value;
                Notify("DiName12");
            }
        }

        public string DiName13
        {
            get { return mDiNames[12]; }
            set
            {
                mDiNames[12] = value;
                Notify("DiName13");
            }
        }

        public string DiName14
        {
            get { return mDiNames[13]; }
            set
            {
                mDiNames[13] = value;
                Notify("DiName14");
            }
        }

        public string DiName15
        {
            get { return mDiNames[14]; }
            set
            {
                mDiNames[14] = value;
                Notify("DiName15");
            }
        }

        public string DiName16
        {
            get { return mDiNames[15]; }
            set
            {
                mDiNames[15] = value;
                Notify("DiName16");
            }
        }

        public string DiName17
        {
            get { return mDiNames[16]; }
            set
            {
                mDiNames[16] = value;
                Notify("DiName17");
            }
        }

        public string DiName18
        {
            get { return mDiNames[17]; }
            set
            {
                mDiNames[17] = value;
                Notify("DiName18");
            }
        }

        public string DiName19
        {
            get { return mDiNames[18]; }
            set
            {
                mDiNames[18] = value;
                Notify("DiName19");
            }
        }

        public string DiName20
        {
            get { return mDiNames[19]; }
            set
            {
                mDiNames[19] = value;
                Notify("DiName20");
            }
        }

        public string DiName21
        {
            get { return mDiNames[20]; }
            set
            {
                mDiNames[20] = value;
                Notify("DiName21");
            }
        }

        public string DiName22
        {
            get { return mDiNames[21]; }
            set
            {
                mDiNames[21] = value;
                Notify("DiName22");
            }
        }

        public string DiName23
        {
            get { return mDiNames[22]; }
            set
            {
                mDiNames[22] = value;
                Notify("DiName23");
            }
        }

        public string DiName24
        {
            get { return mDiNames[23]; }
            set
            {
                mDiNames[23] = value;
                Notify("DiName24");
            }
        }

        public string DiName25
        {
            get { return mDiNames[24]; }
            set
            {
                mDiNames[24] = value;
                Notify("DiName25");
            }
        }

        public string DiName26
        {
            get { return mDiNames[25]; }
            set
            {
                mDiNames[25] = value;
                Notify("DiName26");
            }
        }

        public string DiName27
        {
            get { return mDiNames[26]; }
            set
            {
                mDiNames[26] = value;
                Notify("DiName27");
            }
        }

        public string DiName28
        {
            get { return mDiNames[27]; }
            set
            {
                mDiNames[27] = value;
                Notify("DiName28");
            }
        }

        public string DiName29
        {
            get { return mDiNames[28]; }
            set
            {
                mDiNames[28] = value;
                Notify("DiName29");
            }
        }

        public string DiName30
        {
            get { return mDiNames[29]; }
            set
            {
                mDiNames[29] = value;
                Notify("DiName30");
            }
        }

        public string DiName31
        {
            get { return mDiNames[30]; }
            set
            {
                mDiNames[30] = value;
                Notify("DiName31");
            }
        }

        public string DiName32
        {
            get { return mDiNames[31]; }
            set
            {
                mDiNames[31] = value;
                Notify("DiName32");
            }
        }

        public string DoName1
        {
            get { return mDoNames[0]; }
            set
            {
                mDoNames[0] = value;
                Notify("DoName1");
            }
        }

        public string DoName2
        {
            get { return mDoNames[1]; }
            set
            {
                mDoNames[1] = value;
                Notify("DoName2");
            }
        }

        public string DoName3
        {
            get { return mDoNames[2]; }
            set
            {
                mDoNames[2] = value;
                Notify("DoName3");
            }
        }

        public string DoName4
        {
            get { return mDoNames[3]; }
            set
            {
                mDoNames[3] = value;
                Notify("DoName4");
            }
        }

        public string DoName5
        {
            get { return mDoNames[4]; }
            set
            {
                mDoNames[4] = value;
                Notify("DoName5");
            }
        }

        public string DoName6
        {
            get { return mDoNames[5]; }
            set
            {
                mDoNames[5] = value;
                Notify("DoName6");
            }
        }

        public string DoName7
        {
            get { return mDoNames[6]; }
            set
            {
                mDoNames[6] = value;
                Notify("DoName7");
            }
        }

        public string DoName8
        {
            get { return mDoNames[7]; }
            set
            {
                mDoNames[7] = value;
                Notify("DoName8");
            }
        }

        public string DoName9
        {
            get { return mDoNames[8]; }
            set
            {
                mDoNames[8] = value;
                Notify("DoName9");
            }
        }

        public string DoName10
        {
            get { return mDoNames[9]; }
            set
            {
                mDoNames[9] = value;
                Notify("DoName10");
            }
        }

        public string DoName11
        {
            get { return mDoNames[10]; }
            set
            {
                mDoNames[10] = value;
                Notify("DoName11");
            }
        }

        public string DoName12
        {
            get { return mDoNames[11]; }
            set
            {
                mDoNames[11] = value;
                Notify("DoName12");
            }
        }

        public string DoName13
        {
            get { return mDoNames[12]; }
            set
            {
                mDoNames[12] = value;
                Notify("DoName13");
            }
        }

        public string DoName14
        {
            get { return mDoNames[13]; }
            set
            {
                mDoNames[13] = value;
                Notify("DoName14");
            }
        }

        public string DoName15
        {
            get { return mDoNames[14]; }
            set
            {
                mDoNames[14] = value;
                Notify("DoName15");
            }
        }

        public string DoName16
        {
            get { return mDoNames[15]; }
            set
            {
                mDoNames[15] = value;
                Notify("DoName16");
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
