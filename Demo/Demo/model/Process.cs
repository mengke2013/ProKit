using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.model
{
    class Process
    {
        private string mProcessName;
        private int mProcessRemainingTime;
        private int mProcessTime;
        private int mStepNum;
        private int mStepEscapedTime;

        private int mGas1Sp;
        private int mGas2Sp;
        private int mGas3Sp;
        private int mGas4Sp;
        private int mGas5Sp;
        private int mGas6Sp;
        private int mGas7Sp;
        private int mGas8Sp;
        private int mGas1CurMeas;
        private int mGas2CurMeas;
        private int mGas3CurMeas;
        private int mGas4CurMeas;
        private int mGas5CurMeas;
        private int mGas6CurMeas;
        private int mGas7CurMeas;
        private int mGas8CurMeas;
        private int mAna1Sp;
        private int mAna2Sp;
        private int mAna3Sp;
        private int mAna4Sp;
        private int mAna5Sp;
        private int mAna6Sp;
        private int mAna7Sp;
        private int mAna8Sp;
        private int mAna1CurMeas;
        private int mAna2CurMeas;
        private int mAna3CurMeas;
        private int mAna4CurMeas;
        private int mAna5CurMeas;
        private int mAna6CurMeas;
        private int mAna7CurMeas;
        private int mAna8CurMeas;
        private int mTemper1Sp;
        private int mTemper2Sp;
        private int mTemper3Sp;
        private int mTemper4Sp;
        private int mTemper5Sp;
        private int mTemper6Sp;
        private int mTemper7Sp;
        private int mTemper8Sp;
        private int mTemper1IntValue;
        private int mTemper2IntValue;
        private int mTemper3IntValue;
        private int mTemper4IntValue;
        private int mTemper5IntValue;
        private int mTemper6IntValue;
        private int mTemper7IntValue;
        private int mTemper8IntValue;
        private int mTemper1ExtValue;
        private int mTemper2ExtValue;
        private int mTemper3ExtValue;
        private int mTemper4ExtValue;
        private int mTemper5ExtValue;
        private int mTemper6ExtValue;
        private int mTemper7ExtValue;
        private int mTemper8ExtValue;
        private int mTemper1HeatPower;
        private int mTemper2HeatPower;
        private int mTemper3HeatPower;
        private int mTemper4HeatPower;
        private int mTemper5HeatPower;
        private int mTemper6HeatPower;
        private int mTemper7HeatPower;
        private int mTemper8HeatPower;
        private int mPaddlePosAct;
        private int mPaddlePosSp;
        private int mPaddleSpeedSp;

        private int mStatus;

        public Process()
        {

        }

        public int Status
        {
            get { return mStatus; }
            set { mStatus = value; }
        }

        public int StepEscapedTime
        {
            get { return mStepEscapedTime; }
            set { mStepEscapedTime = value; }
        }
        public int StepNum
        {
            get { return mStepNum; }
            set { mStepNum = value; }
        }
        public string ProcessName
        {
            get { return mProcessName; }
            set { mProcessName = value; }
        }
        public int ProcessRemainingTime
        {
            get { return mProcessRemainingTime; }
            set { mProcessRemainingTime = value; }
        }
        public int ProcessTime
        {
            get { return mProcessTime; }
            set { mProcessTime = value; }
        }

        public int Gas1Sp
        {
            get { return mGas1Sp; }
            set { mGas1Sp = value; }
        }

        public int Gas2Sp
        {
            get { return mGas2Sp; }
            set { mGas2Sp = value; }
        }
        public int Gas3Sp
        {
            get { return mGas3Sp; }
            set { mGas3Sp = value; }
        }
        public int Gas4Sp
        {
            get { return mGas4Sp; }
            set { mGas4Sp = value; }
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
        public int Gas7Sp
        {
            get { return mGas7Sp; }
            set { mGas7Sp = value; }
        }
        public int Gas8Sp
        {
            get { return mGas8Sp; }
            set { mGas8Sp = value; }
        }

        public int Gas1CurMeas
        {
            get { return mGas1CurMeas; }
            set { mGas1CurMeas = value; }
        }
        public int Gas2CurMeas
        {
            get { return mGas2CurMeas; }
            set { mGas2CurMeas = value; }
        }
        public int Gas3CurMeas
        {
            get { return mGas3CurMeas; }
            set { mGas3CurMeas = value; }
        }
        public int Gas4CurMeas
        {
            get { return mGas4CurMeas; }
            set { mGas4CurMeas = value; }
        }
        public int Gas5CurMeas
        {
            get { return mGas5CurMeas; }
            set { mGas5CurMeas = value; }
        }
        public int Gas6CurMeas
        {
            get { return mGas6CurMeas; }
            set { mGas6CurMeas = value; }
        }
        public int Gas7CurMeas
        {
            get { return mGas7CurMeas; }
            set { mGas7CurMeas = value; }
        }
        public int Gas8CurMeas
        {
            get { return mGas8CurMeas; }
            set { mGas8CurMeas = value; }
        }

        public int Ana1Sp
        {
            get { return mAna1Sp; }
            set { mAna1Sp = value; }
        }
        public int Ana2Sp
        {
            get { return mAna2Sp; }
            set { mAna2Sp = value; }
        }
        public int Ana3Sp
        {
            get { return mAna3Sp; }
            set { mAna3Sp = value; }
        }
        public int Ana4Sp
        {
            get { return mAna4Sp; }
            set { mAna4Sp = value; }
        }
        public int Ana5Sp
        {
            get { return mAna5Sp; }
            set { mAna5Sp = value; }
        }
        public int Ana6Sp
        {
            get { return mAna6Sp; }
            set { mAna6Sp = value; }
        }
        public int Ana7Sp
        {
            get { return mAna7Sp; }
            set { mAna7Sp = value; }
        }
        public int Ana8Sp
        {
            get { return mAna8Sp; }
            set { mAna8Sp = value; }
        }

        public int Ana1CurMeas
        {
            get { return mAna1CurMeas; }
            set { mAna1CurMeas = value; }
        }
        public int Ana2CurMeas
        {
            get { return mAna2CurMeas; }
            set { mAna2CurMeas = value; }
        }
        public int Ana3CurMeas
        {
            get { return mAna3CurMeas; }
            set { mAna3CurMeas = value; }
        }
        public int Ana4CurMeas
        {
            get { return mAna4CurMeas; }
            set { mAna4CurMeas = value; }
        }
        public int Ana5CurMeas
        {
            get { return mAna5CurMeas; }
            set { mAna5CurMeas = value; }
        }
        public int Ana6CurMeas
        {
            get { return mAna6CurMeas; }
            set { mAna6CurMeas = value; }
        }
        public int Ana7CurMeas
        {
            get { return mAna7CurMeas; }
            set { mAna7CurMeas = value; }
        }
        public int Ana8CurMeas
        {
            get { return mAna8CurMeas; }
            set { mAna8CurMeas = value; }
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
        public int Temper7Sp
        {
            get { return mTemper7Sp; }
            set { mTemper7Sp = value; }
        }
        public int Temper8Sp
        {
            get { return mTemper8Sp; }
            set { mTemper8Sp = value; }
        }

        public int Temper1ExtValue
        {
            get { return mTemper1ExtValue; }
            set { mTemper1ExtValue = value; }
        }
        public int Temper2ExtValue
        {
            get { return mTemper2ExtValue; }
            set { mTemper2ExtValue = value; }
        }
        public int Temper3ExtValue
        {
            get { return mTemper3ExtValue; }
            set { mTemper3ExtValue = value; }
        }
        public int Temper4ExtValue
        {
            get { return mTemper4ExtValue; }
            set { mTemper4ExtValue = value; }
        }
        public int Temper5ExtValue
        {
            get { return mTemper5ExtValue; }
            set { mTemper5ExtValue = value; }
        }
        public int Temper6ExtValue
        {
            get { return mTemper6ExtValue; }
            set { mTemper6ExtValue = value; }
        }
        public int Temper7ExtValue
        {
            get { return mTemper7ExtValue; }
            set { mTemper7ExtValue = value; }
        }
        public int Temper8ExtValue
        {
            get { return mTemper8ExtValue; }
            set { mTemper8ExtValue = value; }
        }

        public int Temper1IntValue
        {
            get { return mTemper1IntValue; }
            set { mTemper1IntValue = value; }
        }
        public int Temper2IntValue
        {
            get { return mTemper2IntValue; }
            set { mTemper2IntValue = value; }
        }
        public int Temper3IntValue
        {
            get { return mTemper3IntValue; }
            set { mTemper3IntValue = value; }
        }
        public int Temper4IntValue
        {
            get { return mTemper4IntValue; }
            set { mTemper4IntValue = value; }
        }
        public int Temper5IntValue
        {
            get { return mTemper5IntValue; }
            set { mTemper5IntValue = value; }
        }
        public int Temper6IntValue
        {
            get { return mTemper6IntValue; }
            set { mTemper6IntValue = value; }
        }
        public int Temper7IntValue
        {
            get { return mTemper7IntValue; }
            set { mTemper7IntValue = value; }
        }
        public int Temper8IntValue
        {
            get { return mTemper8IntValue; }
            set { mTemper8IntValue = value; }
        }

        public int Temper1HeatPower
        {
            get { return mTemper1HeatPower; }
            set { mTemper1HeatPower = value; }
        }
        public int Temper2HeatPower
        {
            get { return mTemper2HeatPower; }
            set { mTemper2HeatPower = value; }
        }
        public int Temper3HeatPower
        {
            get { return mTemper3HeatPower; }
            set { mTemper3HeatPower = value; }
        }
        public int Temper4HeatPower
        {
            get { return mTemper4HeatPower; }
            set { mTemper4HeatPower = value; }
        }
        public int Temper5HeatPower
        {
            get { return mTemper5HeatPower; }
            set { mTemper5HeatPower = value; }
        }
        public int Temper6HeatPower
        {
            get { return mTemper6HeatPower; }
            set { mTemper6HeatPower = value; }
        }
        public int Temper7HeatPower
        {
            get { return mTemper7HeatPower; }
            set { mTemper7HeatPower = value; }
        }
        public int Temper8HeatPower
        {
            get { return mTemper8HeatPower; }
            set { mTemper8HeatPower = value; }
        }

        public int PaddlePosAct
        {
            get { return mPaddlePosAct; }
            set { mPaddlePosAct = value; }
        }
        public int PaddlePosSp
        {
            get { return mPaddlePosSp; }
            set { mPaddlePosSp = value; }
        }
        public int PaddleSpeedSp
        {
            get { return mPaddleSpeedSp; }
            set { mPaddleSpeedSp = value; }
        }
    }
}
