﻿using System;
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
        private string mProcessName;
        private int mStepIndex;
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

        private bool mTemperRegulInt;

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

        private string mGas1Name;
        private string mGas2Name;
        private string mGas5Name;
        private string mGas6Name;
        private string mGas8Name;
        private string mAna1Name;
        private string[] mEvNames;
        private string[] mDoNames;
        private string[] mDiNames;

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

            mEvNames = new string[16];
            mDoNames = new string[16];
            mDiNames = new string[24];

        }

        public int StepIndex
        {
            get { return mStepIndex; }
            set
            {
                mStepIndex = value;
                UpdateProperty("StepIndex");
            }
        }

        public string ProcessName
        {
            get { return mProcessName; }
            set
            {
                mProcessName = value;
                Notify("ProcessName");
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

        public bool TemperRegulInt
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
                Notify("Ramp1");
                Notify("Ramp2");
                Notify("Ramp3");
                Notify("Ramp4");
                Notify("Ramp5");
                Notify("Ramp6");
                Notify("Ramp7");
                Notify("Ramp8");
                Notify("Ramp9");
                Notify("Ramp10");
                Notify("Ramp11");
                Notify("Ramp12");
            }
        }

        public bool Ramp1
        {
            get
            {
                return GetBitValue(mRamp, 0);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 0, value);
                Notify("Ramp1");
            }
        }

        public bool Ramp2
        {
            get
            {
                return GetBitValue(mRamp, 1);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 1, value);
                Notify("Ramp2");
            }
        }

        public bool Ramp3
        {
            get
            {
                return GetBitValue(mRamp, 2);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 2, value);
                Notify("Ramp3");
            }
        }

        public bool Ramp4
        {
            get
            {
                return GetBitValue(mRamp, 3);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 3, value);
                Notify("Ramp4");
            }
        }

        public bool Ramp5
        {
            get
            {
                return GetBitValue(mRamp, 4);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 4, value);
                Notify("Ramp5");
            }
        }

        public bool Ramp6
        {
            get
            {
                return GetBitValue(mRamp, 5);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 5, value);
                Notify("Ramp6");
            }
        }

        public bool Ramp7
        {
            get
            {
                return GetBitValue(mRamp, 6);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 6, value);
                Notify("Ramp7");
            }
        }

        public bool Ramp8
        {
            get
            {
                return GetBitValue(mRamp, 7);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 7, value);
                Notify("Ramp8");
            }
        }

        public bool Ramp9
        {
            get
            {
                return GetBitValue(mRamp, 8);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 8, value);
                Notify("Ramp9");
            }
        }

        public bool Ramp10
        {
            get
            {
                return GetBitValue(mRamp, 9);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 9, value);
                Notify("Ramp10");
            }
        }
        public bool Ramp11
        {
            get
            {
                return GetBitValue(mRamp, 10);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 10, value);
                Notify("Ramp11");
            }
        }

        public bool Ramp12
        {
            get
            {
                return GetBitValue(mRamp, 11);
            }
            set
            {
                mRamp = SetBitValue(mRamp, 11, value);
                Notify("Ramp12");
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
                Notify("DigOutput11");
                Notify("DigOutput12");
                Notify("DigOutput13");
                Notify("DigOutput14");
                Notify("DigOutput15");
                Notify("DigOutput16");
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

        public bool DigOutput11
        {
            get
            {
                return GetBitValue(mDigOutput, 10);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 10, value);
                Notify("DigOutput11");
            }
        }

        public bool DigOutput12
        {
            get
            {
                return GetBitValue(mDigOutput, 11);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 11, value);
                Notify("DigOutput12");
            }
        }

        public bool DigOutput13
        {
            get
            {
                return GetBitValue(mDigOutput, 12);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 12, value);
                Notify("DigOutput13");
            }
        }

        public bool DigOutput14
        {
            get
            {
                return GetBitValue(mDigOutput, 13);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 13, value);
                Notify("DigOutput14");
            }
        }

        public bool DigOutput15
        {
            get
            {
                return GetBitValue(mDigOutput, 14);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 14, value);
                Notify("DigOutput15");
            }
        }

        public bool DigOutput16
        {
            get
            {
                return GetBitValue(mDigOutput, 15);
            }
            set
            {
                mDigOutput = SetBitValue(mDigOutput, 15, value);
                Notify("DigOutput16");
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
                //for (byte i = 0; i < mAlrmDigIns.Length; ++i)
                {
                    //mAlrmDigIns[i] = mDiSelectorModels[i].DiTypeId;
                }
                return mAlrmDigIns;
            }
            set
            {
                if (value != null)
                {
                    mAlrmDigIns = value;

                    //for (byte i = 0; i < mAlrmDigIns.Length; ++i)
                    {
                        //mDiSelectorModels[i].DiTypeId = mAlrmDigIns[i];
                    }

                    Notify("AlrmDigIns");
                }
                
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

        public string[] DiNames
        {
            get { return mDiNames; }
            set
            {
                mDiNames = value;
                Notify("DiNames");
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
