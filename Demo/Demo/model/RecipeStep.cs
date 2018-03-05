using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.model
{
    public class RecipeStep
    {
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



        public RecipeStep()
        {

        }

        public string StepName
        {
            get { return mStepName; }
            set { mStepName = value; }
        }

        public sbyte StepType
        {
            get { return mStepType; }
            set { mStepType = value; }
        }

        public int StepTime
        {
            get { return mStepTime; }
            set { mStepTime = value; }
        }

        public short Gas1Sp
        {
            get { return mGas1Sp; }
            set { mGas1Sp = value; }
        }

        public int Gas2Sp
        {
            get { return mGas2Sp; }
            set { mGas2Sp = value; }
        }

        public int Gas5Sp
        {
            get { return mGas5Sp; }
            set { mGas5Sp = value; }
        }

        public int Gas6Sp
        {
            get { return mGas6Sp; }
            set { mGas6Sp = value; }
        }

        public int Gas8Sp
        {
            get { return mGas8Sp; }
            set { mGas8Sp = value; }
        }

        public int Ana1Sp
        {
            get { return mAna1Sp; }
            set { mAna1Sp = value; }
        }

        public int Temper1Sp
        {
            get { return mTemper1Sp; }
            set { mTemper1Sp = value; }
        }

        public int Temper2Sp
        {
            get { return mTemper2Sp; }
            set { mTemper2Sp = value; }
        }

        public int Temper3Sp
        {
            get { return mTemper3Sp; }
            set { mTemper3Sp = value; }
        }

        public int Temper4Sp
        {
            get { return mTemper4Sp; }
            set { mTemper4Sp = value; }
        }

        public int Temper5Sp
        {
            get { return mTemper5Sp; }
            set { mTemper5Sp = value; }
        }

        public int Temper6Sp
        {
            get { return mTemper6Sp; }
            set { mTemper6Sp = value; }
        }

        public int Gas1Abort
        {
            get { return mGas1Abort; }
            set { mGas1Abort = value; }
        }

        public int Gas1Hold
        {
            get { return mGas1Hold; }
            set { mGas1Hold = value; }
        }

        public int Gas1Alarm
        {
            get { return mGas1Alarm; }
            set { mGas1Alarm = value; }
        }

        public int Gas1Next
        {
            get { return mGas1Next; }
            set { mGas1Next = value; }
        }

        public int Gas2Abort
        {
            get { return mGas2Abort; }
            set { mGas2Abort = value; }
        }

        public int Gas2Hold
        {
            get { return mGas2Hold; }
            set { mGas2Hold = value; }
        }

        public int Gas2Alarm
        {
            get { return mGas2Alarm; }
            set { mGas2Alarm = value; }
        }

        public int Gas2Next
        {
            get { return mGas2Next; }
            set { mGas2Next = value; }
        }

        public int Gas5Abort
        {
            get { return mGas5Abort; }
            set { mGas5Abort = value; }
        }

        public int Gas5Hold
        {
            get { return mGas5Hold; }
            set { mGas5Hold = value; }
        }

        public int Gas5Alarm
        {
            get { return mGas5Alarm; }
            set { mGas5Alarm = value; }
        }

        public int Gas5Next
        {
            get { return mGas5Next; }
            set { mGas5Next = value; }
        }

        public int Gas6Abort
        {
            get { return mGas6Abort; }
            set { mGas6Abort = value; }
        }

        public int Gas6Hold
        {
            get { return mGas6Hold; }
            set { mGas6Hold = value; }
        }

        public int Gas6Alarm
        {
            get { return mGas6Alarm; }
            set { mGas6Alarm = value; }
        }

        public int Gas6Next
        {
            get { return mGas6Next; }
            set { mGas6Next = value; }
        }

        public int Gas8Abort
        {
            get { return mGas8Abort; }
            set { mGas8Abort = value; }
        }

        public int Gas8Hold
        {
            get { return mGas8Hold; }
            set {  mGas8Hold = value; }
        }

        public int Gas8Alarm
        {
            get { return mGas8Alarm; }
            set { mGas8Alarm = value; }
        }

        public int Gas8Next
        {
            get { return mGas8Next; }
            set { mGas8Next = value; }
        }

        public int Ana1Abort
        {
            get { return mAna1Abort; }
            set { mAna1Abort = value; }
        }

        public int Ana1Hold
        {
            get { return mAna1Hold; }
            set { mAna1Hold = value; }
        }

        public int Ana1Alarm
        {
            get { return mAna1Alarm; }
            set { mAna1Alarm = value; }
        }

        public int Ana1Next
        {
            get { return mAna1Next; }
            set { mAna1Next = value; }
        }

        public int Temper1Abort
        {
            get { return mTemper1Abort; }
            set { mTemper1Abort = value; }
        }

        public int Temper1Hold
        {
            get { return mTemper1Hold; }
            set { mTemper1Hold = value; }
        }

        public int Temper1Alarm
        {
            get { return mTemper1Alarm; }
            set { mTemper1Alarm = value; }
        }

        public int Temper1Next
        {
            get { return mTemper1Next; }
            set { mTemper1Next = value; }
        }

        public int Temper2Abort
        {
            get { return mTemper2Abort; }
            set { mTemper2Abort = value; }
        }

        public int Temper2Hold
        {
            get { return mTemper2Hold; }
            set { mTemper2Hold = value; }
        }

        public int Temper2Alarm
        {
            get { return mTemper2Alarm; }
            set { mTemper2Alarm = value; }
        }

        public int Temper2Next
        {
            get { return mTemper2Next; }
            set { mTemper2Next = value; }
        }

        public int Temper3Abort
        {
            get { return mTemper3Abort; }
            set { mTemper3Abort = value; }
        }

        public int Temper3Hold
        {
            get { return mTemper3Hold; }
            set { mTemper3Hold = value; }
        }

        public int Temper3Alarm
        {
            get { return mTemper3Alarm; }
            set { mTemper3Alarm = value; }
        }

        public int Temper3Next
        {
            get { return mTemper3Next; }
            set { mTemper3Next = value; }
        }

        public int Temper4Abort
        {
            get { return mTemper4Abort; }
            set { mTemper4Abort = value; }
        }

        public int Temper4Hold
        {
            get { return mTemper4Hold; }
            set { mTemper4Hold = value; }
        }

        public int Temper4Alarm
        {
            get { return mTemper4Alarm; }
            set { mTemper4Alarm = value; }
        }

        public int Temper4Next
        {
            get { return mTemper4Next; }
            set { mTemper4Next = value; }
        }

        public int Temper5Abort
        {
            get { return mTemper5Abort; }
            set { mTemper5Abort = value; }
        }

        public int Temper5Hold
        {
            get { return mTemper5Hold; }
            set { mTemper5Hold = value; }
        }

        public int Temper5Alarm
        {
            get { return mTemper5Alarm; }
            set { mTemper5Alarm = value; }
        }

        public int Temper5Next
        {
            get { return mTemper5Next; }
            set { mTemper5Next = value; }
        }

        public int Temper6Abort
        {
            get { return mTemper6Abort; }
            set { mTemper6Abort = value; }
        }

        public int Temper6Hold
        {
            get { return mTemper6Hold; }
            set { mTemper6Hold = value; }
        }

        public int Temper6Alarm
        {
            get { return mTemper6Alarm; }
            set { mTemper6Alarm = value; }
        }

        public int Temper6Next
        {
            get { return mTemper6Next; }
            set { mTemper6Next = value; }
        }

        public bool TemperRegulInt
        {
            get { return mTemperRegulInt; }
            set { mTemperRegulInt = value; }
        }

        public int AxisPosSp
        {
            get { return mAxisPosSp; }
            set { mAxisPosSp = value; }
        }

        public int AxisSpeedSp
        {
            get { return mAxisSpeedSp; }
            set { mAxisSpeedSp = value; }
        }

        public int Ramp
        {
            get { return mRamp; }
            set { mRamp = value; }
        }

        public int DigOutput
        {
            get { return mDigOutput; }
            set { mDigOutput = value; }
        }


        public int Ev
        {
            get { return mEv; }
            set { mEv = value; }
        }

        public byte[] AlrmDigIns
        {
            get { return mAlrmDigIns; }
            set { mAlrmDigIns = value; }
        }


        public byte Num
        {
            get { return mNum; }
            set { mNum = value; }
        }


        public byte AnalogAbort
        {
            get { return mAnalogAbort; }
            set { mAnalogAbort = value; }
        }

        public byte DigitalAbort
        {
            get { return mDigitalAbort; }
            set { mDigitalAbort = value; }
        }

        public byte TemperAbort
        {
            get { return mTemperAbort; }
            set { mTemperAbort = value; }
        }

        public byte ManualAbort
        {
            get { return mManualAbort; }
            set { mManualAbort = value; }
        }

        public byte PowerAbort
        {
            get { return mPowerAbort; }
            set { mPowerAbort = value; }
        }

        public byte MfcDelay
        {
            get { return mMfcDelay; }
            set { mMfcDelay = value; }
        }

        public byte AnalogDelay
        {
            get { return mAnalogDelay; }
            set { mAnalogDelay = value; }
        }

        public int StepIndex
        {
            get { return mStepIndex; }
            set { mStepIndex = value; }
        }

        public int CheckSum
        {
            get { return mCheckSum; }
            set { mCheckSum = value; }
        }
    }
}
