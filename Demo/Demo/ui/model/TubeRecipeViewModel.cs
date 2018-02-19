using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Demo.ui.model
{
    public class TubeRecipeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool mUpdateView;
        private bool mDirty;

        private TubePageStyle mTubePageStyle;
        private byte mStepIndex;
        private string mStepName;
        private sbyte mStepType;
        private int mStepTime;

        private short mGas1Sp;
        private int mGas2Sp;
        private int mGas5Sp;
        private int mGas6Sp;
        private int mGas8Sp;
        private int mAna1Sp;
        private int mTemper1Sp;
        private int mTemper2Sp;
        private int mTemper3Sp;
        private int mTemper4Sp;
        private int mTemper5Sp;
        private int mTemper6Sp;

        private int mGas1Abort;
        private int mGas1Hold;
        private int mGas1Alarm;
        private int mGas1Next;
        private int mGas2Abort;
        private int mGas2Hold;
        private int mGas2Alarm;
        private int mGas2Next;
        private int mGas5Abort;
        private int mGas5Hold;
        private int mGas5Alarm;
        private int mGas5Next;
        private int mGas6Abort;
        private int mGas6Hold;
        private int mGas6Alarm;
        private int mGas6Next;
        private int mGas8Abort;
        private int mGas8Hold;
        private int mGas8Alarm;
        private int mGas8Next;
        private int mAna1Abort;
        private int mAna1Hold;
        private int mAna1Alarm;
        private int mAna1Next;

        private int mTemper1Abort;
        private int mTemper1Hold;
        private int mTemper1Alarm;
        private int mTemper1Next;
        private int mTemper2Abort;
        private int mTemper2Hold;
        private int mTemper2Alarm;
        private int mTemper2Next;
        private int mTemper3Abort;
        private int mTemper3Hold;
        private int mTemper3Alarm;
        private int mTemper3Next;
        private int mTemper4Abort;
        private int mTemper4Hold;
        private int mTemper4Alarm;
        private int mTemper4Next;
        private int mTemper5Abort;
        private int mTemper5Hold;
        private int mTemper5Alarm;
        private int mTemper5Next;
        private int mTemper6Abort;
        private int mTemper6Hold;
        private int mTemper6Alarm;
        private int mTemper6Next;

        private short mTemperRegulInt;

        private int mAxisPosSp;
        private int mAxisSpeedSp;

        private int mRamp;
        private int mDigOutput;
        private int mEv;
        private byte mNum;
        private int mCheckSum;

        private byte mAnalogAbort;
        private byte mDigitalAbort;
        private byte mTemperAbort;
        private byte mManualAbort;
        private byte mPowerAbort;
        private byte mMfcDelay;
        private byte mAnalogDelay;

        private byte[] mAlrmDigIns;
        private DiSelectorModel[] mDiSelectorModels;

        public TubeRecipeViewModel(byte stepIndex)
        {
            mStepIndex = stepIndex;
            mStepName = "";
            mAlrmDigIns = new byte[32];
            mDiSelectorModels = new DiSelectorModel[32];
            for (byte i = 0; i < 32; ++i)
            {
                mAlrmDigIns[i] = 0;
                mDiSelectorModels[i] = new DiSelectorModel();
            }
            mDigOutput = 0;
        }

        public byte StepIndex
        {
            get { return mStepIndex; }
            set
            {
                mStepIndex = value;
                UpdateProperty("StepIndex");
            }
        }

        public string StepName
        {
            get { return mStepName; }
            set
            {
                if (mStepName != value)
                {
                    mDirty = true;
                }
                mStepName = value;
                UpdateProperty("StepName");
            }
        }

        public sbyte StepType
        {
            get { return mStepType; }
            set
            {
                if (value > 10)
                    return;
                mStepType = value;
                Notify("StepType");
            }
        }

        public int StepTime
        {
            get { return mStepTime; }
            set
            {
                mStepTime = value;
                Notify("StepTime");
            }
        }

        public short Gas1Sp
        {
            get { return mGas1Sp; }
            set
            {
                mGas1Sp = value;
                Notify("Gas1Sp");
            }
        }

        public int Gas2Sp
        {
            get { return mGas2Sp; }
            set
            {
                mGas2Sp = value;
                Notify("Gas2Sp");
            }
        }

        public int Gas5Sp
        {
            get { return mGas5Sp; }
            set
            {
                mGas5Sp = value;
                Notify("Gas5Sp");
            }
        }

        public int Gas6Sp
        {
            get { return mGas6Sp; }
            set
            {
                mGas6Sp = value;
                Notify("Gas6Sp");
            }
        }

        public int Gas8Sp
        {
            get { return mGas8Sp; }
            set
            {
                mGas8Sp = value;
                Notify("Gas8Sp");
            }
        }

        public int Ana1Sp
        {
            get { return mAna1Sp; }
            set
            {
                mAna1Sp = value;
                Notify("Ana1Sp");
            }
        }

        public int Temper1Sp
        {
            get { return mTemper1Sp; }
            set
            {
                mTemper1Sp = value;
                Notify("Temper1Sp");
            }
        }

        public int Temper2Sp
        {
            get { return mTemper2Sp; }
            set
            {
                mTemper2Sp = value;
                Notify("Temper2Sp");
            }
        }

        public int Temper3Sp
        {
            get { return mTemper3Sp; }
            set
            {
                mTemper3Sp = value;
                Notify("Temper3Sp");
            }
        }

        public int Temper4Sp
        {
            get { return mTemper4Sp; }
            set
            {
                mTemper4Sp = value;
                Notify("Temper4Sp");
            }
        }

        public int Temper5Sp
        {
            get { return mTemper5Sp; }
            set
            {
                mTemper5Sp = value;
                Notify("Temper5Sp");
            }
        }

        public int Temper6Sp
        {
            get { return mTemper6Sp; }
            set
            {
                mTemper6Sp = value;
                Notify("Temper6Sp");
            }
        }

        public int Gas1Abort
        {
            get { return mGas1Abort; }
            set
            {
                mGas1Abort = value;
                Notify("Gas1Abort");
            }
        }

        public int Gas1Hold
        {
            get { return mGas1Hold; }
            set
            {
                mGas1Hold = value;
                Notify("Gas1Hold");
            }
        }

        public int Gas1Alarm
        {
            get { return mGas1Alarm; }
            set
            {
                mGas1Alarm = value;
                Notify("Gas1Alarm");
            }
        }

        public int Gas1Next
        {
            get { return mGas1Next; }
            set
            {
                mGas1Next = value;
                Notify("Gas1Next");
            }
        }

        public int Gas2Abort
        {
            get { return mGas2Abort; }
            set
            {
                mGas2Abort = value;
                Notify("Gas2Abort");
            }
        }

        public int Gas2Hold
        {
            get { return mGas2Hold; }
            set
            {
                mGas2Hold = value;
                Notify("Gas2Hold");
            }
        }

        public int Gas2Alarm
        {
            get { return mGas2Alarm; }
            set
            {
                mGas2Alarm = value;
                Notify("Gas2Alarm");
            }
        }

        public int Gas2Next
        {
            get { return mGas2Next; }
            set
            {
                mGas2Next = value;
                Notify("Gas2Next");
            }
        }

        public int Gas5Abort
        {
            get { return mGas5Abort; }
            set
            {
                mGas5Abort = value;
                Notify("Gas5Abort");
            }
        }

        public int Gas5Hold
        {
            get { return mGas5Hold; }
            set
            {
                mGas5Hold = value;
                Notify("Gas5Hold");
            }
        }

        public int Gas5Alarm
        {
            get { return mGas5Alarm; }
            set
            {
                mGas5Alarm = value;
                Notify("Gas5Alarm");
            }
        }

        public int Gas5Next
        {
            get { return mGas5Next; }
            set
            {
                mGas5Next = value;
                Notify("Gas5Next");
            }
        }

        public int Gas6Abort
        {
            get { return mGas6Abort; }
            set
            {
                mGas6Abort = value;
                Notify("Gas6Abort");
            }
        }

        public int Gas6Hold
        {
            get { return mGas6Hold; }
            set
            {
                mGas6Hold = value;
                Notify("Gas6Hold");
            }
        }

        public int Gas6Alarm
        {
            get { return mGas6Alarm; }
            set
            {
                mGas6Alarm = value;
                Notify("Gas6Alarm");
            }
        }

        public int Gas6Next
        {
            get { return mGas6Next; }
            set
            {
                mGas6Next = value;
                Notify("Gas6Next");
            }
        }

        public int Gas8Abort
        {
            get { return mGas8Abort; }
            set
            {
                mGas8Abort = value;
                Notify("Gas8Abort");
            }
        }

        public int Gas8Hold
        {
            get { return mGas8Hold; }
            set
            {
                mGas8Hold = value;
                Notify("Gas8Hold");
            }
        }

        public int Gas8Alarm
        {
            get { return mGas8Alarm; }
            set
            {
                mGas8Alarm = value;
                Notify("Gas8Alarm");
            }
        }

        public int Gas8Next
        {
            get { return mGas8Next; }
            set
            {
                mGas8Next = value;
                Notify("Gas8Next");
            }
        }

        public int Ana1Abort
        {
            get { return mAna1Abort; }
            set
            {
                mAna1Abort = value;
                Notify("Ana1Abort");
            }
        }

        public int Ana1Hold
        {
            get { return mAna1Hold; }
            set
            {
                mAna1Hold = value;
                Notify("Ana1Hold");
            }
        }

        public int Ana1Alarm
        {
            get { return mAna1Alarm; }
            set
            {
                mAna1Alarm = value;
                Notify("Ana1Alarm");
            }
        }

        public int Ana1Next
        {
            get { return mAna1Next; }
            set
            {
                mAna1Next = value;
                Notify("Ana1Next");
            }
        }

        public int Temper1Abort
        {
            get { return mTemper1Abort; }
            set
            {
                mTemper1Abort = value;
                Notify("Temper1Abort");
            }
        }

        public int Temper1Hold
        {
            get { return mTemper1Hold; }
            set
            {
                mTemper1Hold = value;
                Notify("Temper1Hold");
            }
        }

        public int Temper1Alarm
        {
            get { return mTemper1Alarm; }
            set
            {
                mTemper1Alarm = value;
                Notify("Temper1Alarm");
            }
        }

        public int Temper1Next
        {
            get { return mTemper1Next; }
            set
            {
                mTemper1Next = value;
                Notify("Temper1Next");
            }
        }

        public int Temper2Abort
        {
            get { return mTemper2Abort; }
            set
            {
                mTemper2Abort = value;
                Notify("Temper2Abort");
            }
        }

        public int Temper2Hold
        {
            get { return mTemper2Hold; }
            set
            {
                mTemper2Hold = value;
                Notify("Temper2Hold");
            }
        }

        public int Temper2Alarm
        {
            get { return mTemper2Alarm; }
            set
            {
                mTemper2Alarm = value;
                Notify("Temper2Alarm");
            }
        }

        public int Temper2Next
        {
            get { return mTemper2Next; }
            set
            {
                mTemper2Next = value;
                Notify("Temper2Next");
            }
        }

        public int Temper3Abort
        {
            get { return mTemper3Abort; }
            set
            {
                mTemper3Abort = value;
                Notify("Temper3Abort");
            }
        }

        public int Temper3Hold
        {
            get { return mTemper3Hold; }
            set
            {
                mTemper3Hold = value;
                Notify("Temper3Hold");
            }
        }

        public int Temper3Alarm
        {
            get { return mTemper3Alarm; }
            set
            {
                mTemper3Alarm = value;
                Notify("Temper3Alarm");
            }
        }

        public int Temper3Next
        {
            get { return mTemper3Next; }
            set
            {
                mTemper3Next = value;
                Notify("Temper3Next");
            }
        }

        public int Temper4Abort
        {
            get { return mTemper4Abort; }
            set
            {
                mTemper4Abort = value;
                Notify("Temper4Abort");
            }
        }

        public int Temper4Hold
        {
            get { return mTemper4Hold; }
            set
            {
                mTemper4Hold = value;
                Notify("Temper4Hold");
            }
        }

        public int Temper4Alarm
        {
            get { return mTemper4Alarm; }
            set
            {
                mTemper4Alarm = value;
                Notify("Temper4Alarm");
            }
        }

        public int Temper4Next
        {
            get { return mTemper4Next; }
            set
            {
                mTemper4Next = value;
                Notify("Temper4Next");
            }
        }

        public int Temper5Abort
        {
            get { return mTemper5Abort; }
            set
            {
                mTemper5Abort = value;
                Notify("Temper5Abort");
            }
        }

        public int Temper5Hold
        {
            get { return mTemper5Hold; }
            set
            {
                mTemper5Hold = value;
                Notify("Temper5Hold");
            }
        }

        public int Temper5Alarm
        {
            get { return mTemper5Alarm; }
            set
            {
                mTemper5Alarm = value;
                Notify("Temper5Alarm");
            }
        }

        public int Temper5Next
        {
            get { return mTemper5Next; }
            set
            {
                mTemper5Next = value;
                Notify("Temper5Next");
            }
        }

        public int Temper6Abort
        {
            get { return mTemper6Abort; }
            set
            {
                mTemper6Abort = value;
                Notify("Temper6Abort");
            }
        }

        public int Temper6Hold
        {
            get { return mTemper6Hold; }
            set
            {
                mTemper6Hold = value;
                Notify("Temper6Hold");
            }
        }

        public int Temper6Alarm
        {
            get { return mTemper6Alarm; }
            set
            {
                mTemper6Alarm = value;
                Notify("Temper6Alarm");
            }
        }

        public int Temper6Next
        {
            get { return mTemper6Next; }
            set
            {
                mTemper6Next = value;
                Notify("Temper6Next");
            }
        }

        public short TemperRegulInt
        {
            get { return mTemperRegulInt; }
            set
            {
                mTemperRegulInt = value;
                Notify("TemperRegulInt");
            }
        }

        public int AxisPosSp
        {
            get { return mAxisPosSp; }
            set
            {
                mAxisPosSp = value;
                Notify("AxisPosSp");
            }
        }

        public int AxisSpeedSp
        {
            get { return mAxisSpeedSp; }
            set
            {
                mAxisSpeedSp = value;
                Notify("AxisSpeedSp");
            }
        }

        public int Ramp
        {
            get { return mRamp; }
            set
            {
                mRamp = value;
                Notify("Ramp");
            }
        }

        public int DigOutput
        {
            get
            {
                return mDigOutput;
            }
            set
            {
                mDigOutput = value;

                Notify("DigOutput1");
                Notify("DigOutput2");
                Notify("DigOutput3");
                Notify("DigOutput4");
                Notify("DigOutput5");
                Notify("DigOutput6");
                Notify("DigOutput7");
                Notify("DigOutput8");
                Notify("DigOutput9");
                Notify("DigOutput10");
            }
        }

        public bool DigOutput1
        {
            get
            {
                return GetBitValue(mDigOutput, 0);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 0, value);
                Notify("DigOutput1");
            }
        }

        public bool DigOutput2
        {
            get
            {
                return GetBitValue(mDigOutput, 1);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 1, value);
                Notify("DigOutput2");
            }
        }

        public bool DigOutput3
        {
            get
            {
                return GetBitValue(mDigOutput, 2);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 2, value);
                Notify("DigOutput3");
            }
        }

        public bool DigOutput4
        {
            get
            {
                return GetBitValue(mDigOutput, 3);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 3, value);
                Notify("DigOutput4");
            }
        }

        public bool DigOutput5
        {
            get
            {
                return GetBitValue(mDigOutput, 4);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 4, value);
                Notify("DigOutput5");
            }
        }

        public bool DigOutput6
        {
            get
            {
                return GetBitValue(mDigOutput, 5);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 5, value);
                Notify("DigOutput6");
            }
        }

        public bool DigOutput7
        {
            get
            {
                return GetBitValue(mDigOutput, 6);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 6, value);
                Notify("DigOutput7");
            }
        }

        public bool DigOutput8
        {
            get
            {
                return GetBitValue(mDigOutput, 7);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 7, value);
                Notify("DigOutput8");
            }
        }

        public bool DigOutput9
        {
            get
            {
                return GetBitValue(mDigOutput, 8);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 8, value);
                Notify("DigOutput9");
            }
        }

        public bool DigOutput10
        {
            get
            {
                return GetBitValue(mDigOutput, 9);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 9, value);
                Notify("DigOutput10");
            }
        }

        public int Ev
        {
            get { return mEv; }
            set
            {
                mEv = value;

                Notify("Ev1");
                Notify("Ev2");
                Notify("Ev3");
                Notify("Ev4");
                Notify("Ev5");
                Notify("Ev6");
                Notify("Ev7");
                Notify("Ev8");
                Notify("Ev9");
                Notify("Ev10");
            }
        }

        public bool Ev1
        {
            get
            {
                return GetBitValue(mEv, 0);
            }
            set
            {
                mEv = SetBitValue(mEv, 0, value);
                Notify("Ev1");
            }
        }

        public bool Ev2
        {
            get
            {
                return GetBitValue(mEv, 1);
            }
            set
            {
                mEv = SetBitValue(mEv, 1, value);
                Notify("Ev2");
            }
        }

        public bool Ev3
        {
            get
            {
                return GetBitValue(mEv, 2);
            }
            set
            {
                mEv = SetBitValue(mEv, 2, value);
                Notify("Ev3");
            }
        }

        public bool Ev4
        {
            get
            {
                return GetBitValue(mEv, 3);
            }
            set
            {
                mEv = SetBitValue(mEv, 3, value);
                Notify("Ev4");
            }
        }

        public bool Ev5
        {
            get
            {
                return GetBitValue(mEv, 4);
            }
            set
            {
                mEv = SetBitValue(mEv, 4, value);
                Notify("Ev5");
            }
        }

        public bool Ev6
        {
            get
            {
                return GetBitValue(mEv, 5);
            }
            set
            {
                mEv = SetBitValue(mEv, 5, value);
                Notify("Ev6");
            }
        }

        public bool Ev7
        {
            get
            {
                return GetBitValue(mEv, 6);
            }
            set
            {
                mEv = SetBitValue(mEv, 6, value);
                Notify("Ev7");
            }
        }

        public bool Ev8
        {
            get
            {
                return GetBitValue(mEv, 7);
            }
            set
            {
                mEv = SetBitValue(mEv, 7, value);
                Notify("Ev8");
            }
        }

        public bool Ev9
        {
            get
            {
                return GetBitValue(mEv, 8);
            }
            set
            {
                mEv = SetBitValue(mEv, 8, value);
                Notify("Ev9");
            }
        }

        public bool Ev10
        {
            get
            {
                return GetBitValue(mEv, 9);
            }
            set
            {
                mEv = SetBitValue(mEv, 9, value);
                Notify("Ev10");
            }
        }

        public static bool GetBitValue(int value, byte index)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index"); //索引出错
            var val = 1 << index;
            return (value & val) == val;
        }

        public static int SetBitValue(int value, ushort index, bool bitValue)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index"); //索引出错
            var val = 1 << index;
            return bitValue ? (value | val) : (value & ~val);
        }

        public byte Num
        {
            get { return mNum; }
            set
            {
                mNum = value;
                Notify("Num");
            }
        }

        public int CheckSum
        {
            get { return mCheckSum; }
            set
            {
                mCheckSum = value;
                Notify("CheckSum");
            }
        }

        public byte AnalogAbort
        {
            get { return mAnalogAbort; }
            set
            {
                mAnalogAbort = value;
                Notify("AnalogAbort");
            }
        }

        public byte DigitalAbort
        {
            get { return mDigitalAbort; }
            set
            {
                mDigitalAbort = value;
                Notify("DigitalAbort");
            }
        }

        public byte TemperAbort
        {
            get { return mTemperAbort; }
            set
            {
                mTemperAbort = value;
                Notify("TemperAbort");
            }
        }

        public byte ManualAbort
        {
            get { return mManualAbort; }
            set
            {
                mManualAbort = value;
                Notify("ManualAbort");
            }
        }

        public byte PowerAbort
        {
            get { return mPowerAbort; }
            set
            {
                mPowerAbort = value;
                Notify("PowerAbort");
            }
        }

        public byte MfcDelay
        {
            get { return mMfcDelay; }
            set
            {
                mMfcDelay = value;
                Notify("MfcDelay");
            }
        }

        public byte AnalogDelay
        {
            get { return mAnalogDelay; }
            set
            {
                mAnalogDelay = value;
                Notify("AnalogDelay");
            }
        }

        public TubePageStyle TubePageStyle
        {
            get { return mTubePageStyle; }
            set
            {
                mTubePageStyle = value;
                Notify("TubePageStyle");
            }
        }

        public DiSelectorModel[] DiSelectorModels
        {
            get { return mDiSelectorModels; }
            set
            {
                mDiSelectorModels = value;
                Notify("DiSelectorModels");
            }
        }

        public byte[] AlrmDigIns
        {
            get
            {
                for (byte i = 0; i < mAlrmDigIns.Length; ++i)
                {
                    mAlrmDigIns[i] = mDiSelectorModels[i].DiTypeId;
                }
                return mAlrmDigIns;
            }
            set
            {
                mAlrmDigIns = value;

                for (byte i = 0; i < mAlrmDigIns.Length; ++i)
                {
                    mDiSelectorModels[i].DiTypeId = mAlrmDigIns[i];
                }

                Notify("AlrmDigIns");
            }
        }


        public bool UpdateView
        {
            get { return mUpdateView; }
            set { mUpdateView = value; }
        }

        private bool Dirty
        {
            get { return mDirty; }
            set { mDirty = value; }
        }

        private void UpdateProperty(string pName)
        {
            if (mUpdateView)
            {
                Notify(pName);
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
