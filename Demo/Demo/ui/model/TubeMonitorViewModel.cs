using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Rocky.Core.Opc.Ua;
using Demo.com;
using System.Windows;
using Demo.utilities;

namespace Demo.ui.model
{
    public class TubeMonitorViewModel : INotifyPropertyChanged
    {
        private byte mSelectedTube = 1;
        private byte mPreSelectedTube = 1;

        private Visibility mEditVisible;
        private int mFurnaceHeight;

        private string mProcessName;
        private string mStepName;
        private int mStepTime;
        private string mProcessStatus;
        private int mGas1Sp;
        private int mGas2Sp;
        private int mGas5Sp;
        private int mGas6Sp;
        private int mGas8Sp;
        private string mGas1CurMeas;
        private string mGas2CurMeas;
        private string mGas5CurMeas;
        private string mGas6CurMeas;
        private string mGas8CurMeas;
        private int mAna1Sp;
        private string mAna3Sp;
        private string mAna4Sp;
        private string mAna1CurMeas;
        private string mAna3CurMeas;
        private string mAna4CurMeas;
        private bool mTemperInt;
        private bool mTemperIntSp;
        private int mTemper1Sp;
        private int mTemper2Sp;
        private int mTemper3Sp;
        private int mTemper4Sp;
        private int mTemper5Sp;
        private int mTemper6Sp;
        private string mTemper1IntValue;
        private string mTemper2IntValue;
        private string mTemper3IntValue;
        private string mTemper4IntValue;
        private string mTemper5IntValue;
        private string mTemper6IntValue;
        private string mTemper1ExtValue;
        private string mTemper2ExtValue;
        private string mTemper3ExtValue;
        private string mTemper4ExtValue;
        private string mTemper5ExtValue;
        private string mTemper6ExtValue;
        private string mTemper7ExtValue;
        private string mTemper1HeatPower;
        private string mTemper2HeatPower;
        private string mTemper3HeatPower;
        private string mTemper4HeatPower;
        private string mTemper5HeatPower;
        private string mTemper6HeatPower;
        private string mPaddlePosAct;
        private int mPaddlePosSp;
        private int mPaddleSpeedSp;
        private int mEditPaddleSpeedSp;
        private string[] mEvColors;
        private string[] mPipeColors;
        private int mEvSp;
        private int mEvValue;
        private string[] mDiColors;
        private int mDiValue;
        private string[] mDoColors;
        private int mDoSp;
        private int mDoValue;

        private int mProcessRemainingTime;

        private TubePageStyle mTubePageStyle;

        public event PropertyChangedEventHandler PropertyChanged;

        public TubeMonitorViewModel()
        {
            mEvColors = new string[32];
            for (int i = 0; i < 32; ++i)
            {
                mEvColors[i] = "#FFD3C7C7";
            }

            mPipeColors = new string[32];
            for (int i = 0; i < 32; ++i)
            {
                mPipeColors[i] = "#FFD3C7C7";
            }

            mDiColors = new string[32];
            for (int i = 0; i < 32; ++i)
            {
                mDiColors[i] = "#FFD3C7C7";
            }

            mDoColors = new string[32];
            for (int i = 0; i < 32; ++i)
            {
                mDoColors[i] = "#FFD3C7C7";
            }
        }

        public int DiValue
        {
            get { return mDiValue; }
            set
            {
                mDiValue = value;
                var arr = new BitArray(BitConverter.GetBytes(mDiValue));
                for (int i = 0; i < 32; ++i)
                {
                    if (arr[i])
                    {
                        mDiColors[i] = "Green";
                    }
                    else
                    {
                        mDiColors[i] = "Red";
                    }
                    Notify("Di" + (i + 1) + "Color");
                }
            }
        }

        public int DoSp
        {
            get { return mDoSp; }
            set
            {
                mDoSp = value;
                Notify("Do1");
                Notify("Do2");
                Notify("Do3");
                Notify("Do4");
                Notify("Do5");
                Notify("Do6");
                Notify("Do7");
            }
        }

        public int DoValue
        {
            get { return mDoValue; }
            set
            {
                mDoValue = value;
                var arr = new BitArray(BitConverter.GetBytes(mDoValue));
                for (int i = 0; i < 32; ++i)
                {
                    if (arr[i])
                    {
                        mDoColors[i] = "Green";
                    }
                    else
                    {
                        mDoColors[i] = "Red";
                    }
                    Notify("Do" + (i + 1) + "Color");
                }
            }
        }

        public bool Do1
        {
            get
            {
                return BitUtility.GetBitValue(mDoSp, 0);
            }
            set
            {
                mDoSp = BitUtility.SetBitValue(mDoSp, 0, value);
                Notify("Do1");
            }
        }

        public bool Do2
        {
            get
            {
                return BitUtility.GetBitValue(mDoSp, 1);
            }
            set
            {
                mDoSp = BitUtility.SetBitValue(mDoSp, 1, value);
                Notify("Do2");
            }
        }

        public bool Do3
        {
            get
            {
                return BitUtility.GetBitValue(mDoSp, 2);
            }
            set
            {
                mDoSp = BitUtility.SetBitValue(mDoSp, 2, value);
                Notify("Do3");
            }
        }

        public bool Do4
        {
            get
            {
                return BitUtility.GetBitValue(mDoSp, 3);
            }
            set
            {
                mDoSp = BitUtility.SetBitValue(mDoSp, 3, value);
                Notify("Do4");
            }
        }

        public bool Do5
        {
            get
            {
                return BitUtility.GetBitValue(mDoSp, 4);
            }
            set
            {
                mDoSp = BitUtility.SetBitValue(mDoSp, 4, value);
                Notify("Do5");
            }
        }

        public bool Do6
        {
            get
            {
                return BitUtility.GetBitValue(mDoSp, 5);
            }
            set
            {
                mDoSp = BitUtility.SetBitValue(mDoSp, 5, value);
                Notify("Do6");
            }
        }

        public bool Do7
        {
            get
            {
                return BitUtility.GetBitValue(mDoSp, 6);
            }
            set
            {
                mDoSp = BitUtility.SetBitValue(mDoSp, 6, value);
                Notify("Do6");
            }
        }

        public int EvSp
        {
            get { return mEvSp; }
            set
            {
                mEvSp = value;
                Notify("Ev1");
                Notify("Ev2");
                Notify("Ev3");
                Notify("Ev5");
                Notify("Ev6");
                Notify("Ev8");
                Notify("Ev10");
                Notify("Ev11");
                Notify("Ev14");
            }
        }

        public int EvValue
        {
            get { return mEvValue; }
            set
            {
                mEvValue = value;
                var arr = new BitArray(BitConverter.GetBytes(mEvValue));

                var myVal = arr[21];
                for (int i = 0; i < 32; ++i)
                {
                    if (arr[i])
                    {
                        mEvColors[i] = "Green";
                        mPipeColors[i] = "#FF9FCDD4";
                    }
                    else
                    {
                        mEvColors[i] = "Red";
                        mPipeColors[i] = "#FFD3C7C7";
                    }
                    Notify("Ev" + (i + 1) + "Color");
                    Notify("Pipe" + (i + 1) + "Color");
                }
            }
        }

        public bool Ev1
        {
            get
            {
                return BitUtility.GetBitValue(mEvSp, 0);
            }
            set
            {
                mEvSp = BitUtility.SetBitValue(mEvSp, 0, value);
                Notify("Ev1");
            }
        }

        public bool Ev2
        {
            get
            {
                return BitUtility.GetBitValue(mEvSp, 1);
            }
            set
            {
                mEvSp = BitUtility.SetBitValue(mEvSp, 1, value);
                Notify("Ev2");
            }
        }

        public bool Ev3
        {
            get
            {
                return BitUtility.GetBitValue(mEvSp, 2);
            }
            set
            {
                mEvSp = BitUtility.SetBitValue(mEvSp, 2, value);
                Notify("Ev3");
            }
        }

        public bool Ev5
        {
            get
            {
                return BitUtility.GetBitValue(mEvSp, 4);
            }
            set
            {
                mEvSp = BitUtility.SetBitValue(mEvSp, 4, value);
                Notify("Ev5");
            }
        }

        public bool Ev6
        {
            get
            {
                return BitUtility.GetBitValue(mEvSp, 5);
            }
            set
            {
                mEvSp = BitUtility.SetBitValue(mEvSp, 5, value);
                Notify("Ev6");
            }
        }

        public bool Ev8
        {
            get
            {
                return BitUtility.GetBitValue(mEvSp, 7);
            }
            set
            {
                mEvSp = BitUtility.SetBitValue(mEvSp, 7, value);
                Notify("Ev8");
            }
        }

        public bool Ev10
        {
            get
            {
                return BitUtility.GetBitValue(mEvSp, 9);
            }
            set
            {
                mEvSp = BitUtility.SetBitValue(mEvSp, 9, value);
                Notify("Ev10");
            }
        }

        public bool Ev11
        {
            get
            {
                return BitUtility.GetBitValue(mEvSp, 10);
            }
            set
            {
                mEvSp = BitUtility.SetBitValue(mEvSp, 10, value);
                Notify("Ev11");
            }
        }

        public bool Ev14
        {
            get
            {
                return BitUtility.GetBitValue(mEvSp, 13);
            }
            set
            {
                mEvSp = BitUtility.SetBitValue(mEvSp, 13, value);
                Notify("Ev14");
            }
        }

        public Visibility EditVisible
        {
            get { return mEditVisible; }
            set
            {
                mEditVisible = value;
                Notify("EditVisible");
            }
        }

        public byte SelectedTube
        {
            get { return mSelectedTube; }
            set
            {
                mPreSelectedTube = mSelectedTube;
                mSelectedTube = value;
                Notify("SelectedTube");
            }
        }

        public TubePageStyle TubePageStyle
        {
            get { return mTubePageStyle; }
            set { mTubePageStyle = value; }
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

        public string ProcessStatus
        {
            get { return mProcessStatus; }
            set
            {
                mProcessStatus = value;
                Notify("ProcessStatus");
            }
        }

        public string StepName
        {
            get { return mStepName; }
            set
            {
                mStepName = value;
                Notify("StepName");
            }
        }

        public int StepTime
        {
            get { return mStepTime; }
            set
            {
                mStepTime = value;
                Notify("StepTimeS");
            }
        }

        public string StepTimeS
        {
            get { return string.Format("{2}:{1}:{0}", (mStepTime % 3600) % 60, (int)((mStepTime % 3600) / 60), ((int)mStepTime / 3600)); }
        }

        public string Do1Color
        {
            get { return mDoColors[0]; }
        }
        public string Do2Color
        {
            get { return mDoColors[1]; }
        }
        public string Do3Color
        {
            get { return mDoColors[2]; }
        }
        public string Do4Color
        {
            get { return mDoColors[3]; }
        }
        public string Do5Color
        {
            get { return mDoColors[4]; }
        }
        public string Do6Color
        {
            get { return mDoColors[5]; }
        }

        public string Do7Color
        {
            get { return mDoColors[6]; }
        }

        public string Ev1Color
        {
            get { return mEvColors[0]; }
        }
        public string Ev2Color
        {
            get { return mEvColors[1]; }
        }
        public string Ev3Color
        {
            get { return mEvColors[2]; }
        }
        public string Ev4Color
        {
            get { return mEvColors[3]; }
        }
        public string Ev5Color
        {
            get { return mEvColors[4]; }
        }
        public string Ev6Color
        {
            get { return mEvColors[5]; }
        }

        public string Ev7Color
        {
            get { return mEvColors[6]; }
        }

        public string Ev8Color
        {
            get { return mEvColors[7]; }
        }
        public string Ev9Color
        {
            get { return mEvColors[8]; }
        }
        public string Ev10Color
        {
            get { return mEvColors[9]; }
        }
        public string Ev11Color
        {
            get { return mEvColors[10]; }
        }
        public string Ev12Color
        {
            get { return mEvColors[11]; }
        }
        public string Ev13Color
        {
            get { return mEvColors[12]; }
        }
        public string Ev14Color
        {
            get { return mEvColors[13]; }
        }

        public string Pipe1Color
        {
            get { return mPipeColors[0]; }
        }
        public string Pipe2Color
        {
            get { return mPipeColors[1]; }
        }
        public string Pipe3Color
        {
            get { return mPipeColors[2]; }
        }
        public string Pipe4Color
        {
            get { return mPipeColors[3]; }
        }
        public string Pipe5Color
        {
            get { return mPipeColors[4]; }
        }
        public string Pipe6Color
        {
            get { return mPipeColors[5]; }
        }

        public string Pipe7Color
        {
            get { return mPipeColors[6]; }
        }

        public string Pipe8Color
        {
            get { return mPipeColors[7]; }
        }
        public string Pipe9Color
        {
            get { return mPipeColors[8]; }
        }
        public string Pipe10Color
        {
            get { return mPipeColors[9]; }
        }
        public string Pipe11Color
        {
            get { return mPipeColors[10]; }
        }
        public string Pipe12Color
        {
            get { return mPipeColors[11]; }
        }
        public string Pipe13Color
        {
            get { return mPipeColors[12]; }
        }
        public string Pipe14Color
        {
            get { return mPipeColors[13]; }
        }

        public int Gas1Sp
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

        public string Gas1CurMeas
        {
            get { return mGas1CurMeas; }
            set
            {
                mGas1CurMeas = value;
                Notify("Gas1CurMeas");
            }
        }

        public string Gas2CurMeas
        {
            get { return mGas2CurMeas; }
            set
            {
                mGas2CurMeas = value;
                Notify("Gas2CurMeas");
            }
        }

        public string Gas5CurMeas
        {
            get { return mGas5CurMeas; }
            set
            {
                mGas5CurMeas = value;
                Notify("Gas5CurMeas");
            }
        }

        public string Gas6CurMeas
        {
            get { return mGas6CurMeas; }
            set
            {
                mGas6CurMeas = value;
                Notify("Gas6CurMeas");
            }
        }

        public string Gas8CurMeas
        {
            get { return mGas8CurMeas; }
            set
            {
                mGas8CurMeas = value;
                Notify("Gas8CurMeas");
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

        public string Ana3Sp
        {
            get { return mAna3Sp; }
            set
            {
                mAna3Sp = value;
                Notify("Ana3Sp");
            }
        }

        public string Ana4Sp
        {
            get { return mAna4Sp; }
            set
            {
                mAna4Sp = value;
                Notify("Ana4Sp");
            }
        }

        public string Ana1CurMeas
        {
            get { return mAna1CurMeas; }
            set
            {
                mAna1CurMeas = value;
                Notify("Ana1CurMeas");
            }
        }

        public string Ana3CurMeas
        {
            get { return mAna3CurMeas; }
            set
            {
                mAna3CurMeas = value;
                Notify("Ana3CurMeas");
            }
        }

        public string Ana4CurMeas
        {
            get { return mAna4CurMeas; }
            set
            {
                mAna4CurMeas = value;
                Notify("Ana4CurMeas");
            }
        }

        public bool TemperInt
        {
            get { return mTemperInt; }
            set
            {
                mTemperInt = value;
                Notify("TemperInt");
            }
        }

        public bool TemperIntSp
        {
            get { return mTemperIntSp; }
            set
            {
                mTemperIntSp = value;
                Notify("TemperIntSp");
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

        public String Temper1IntValue
        {
            get { return mTemper1IntValue; }
            set
            {
                mTemper1IntValue = value;
                Notify("Temper1IntValue");
            }
        }

        public String Temper2IntValue
        {
            get { return mTemper2IntValue; }
            set
            {
                mTemper2IntValue = value;
                Notify("Temper2IntValue");
            }
        }

        public String Temper3IntValue
        {
            get { return mTemper3IntValue; }
            set
            {
                mTemper3IntValue = value;
                Notify("Temper3IntValue");
            }
        }

        public String Temper4IntValue
        {
            get { return mTemper4IntValue; }
            set
            {
                mTemper4IntValue = value;
                Notify("Temper4IntValue");
            }
        }

        public String Temper5IntValue
        {
            get { return mTemper5IntValue; }
            set
            {
                mTemper5IntValue = value;
                Notify("Temper5IntValue");
            }
        }

        public String Temper6IntValue
        {
            get { return mTemper6IntValue; }
            set
            {
                mTemper6IntValue = value;
                Notify("Temper6IntValue");
            }
        }

        public String Temper1ExtValue
        {
            get { return mTemper1ExtValue; }
            set
            {
                mTemper1ExtValue = value;
                Notify("Temper1ExtValue");
            }
        }

        public String Temper2ExtValue
        {
            get { return mTemper2ExtValue; }
            set
            {
                mTemper2ExtValue = value;
                Notify("Temper2ExtValue");
            }
        }

        public String Temper3ExtValue
        {
            get { return mTemper3ExtValue; }
            set
            {
                mTemper3ExtValue = value;
                Notify("Temper3ExtValue");
            }
        }

        public String Temper4ExtValue
        {
            get { return mTemper4ExtValue; }
            set
            {
                mTemper4ExtValue = value;
                Notify("Temper4ExtValue");
            }
        }

        public String Temper5ExtValue
        {
            get { return mTemper5ExtValue; }
            set
            {
                mTemper5ExtValue = value;
                Notify("Temper5ExtValue");
            }
        }

        public String Temper6ExtValue
        {
            get { return mTemper6ExtValue; }
            set
            {
                mTemper6ExtValue = value;
                Notify("Temper6ExtValue");
            }
        }

        public String Temper7ExtValue
        {
            get { return mTemper6ExtValue; }
            set
            {
                mTemper7ExtValue = value;
                Notify("Temper7ExtValue");
            }
        }

        public String Temper1HeatPower
        {
            get { return mTemper1HeatPower; }
            set
            {
                mTemper1HeatPower = value;
                Notify("Temper1HeatPower");
            }
        }

        public String Temper2HeatPower
        {
            get { return mTemper2HeatPower; }
            set
            {
                mTemper2HeatPower = value;
                Notify("Temper2HeatPower");
            }
        }

        public String Temper3HeatPower
        {
            get { return mTemper3HeatPower; }
            set
            {
                mTemper3HeatPower = value;
                Notify("Temper3HeatPower");
            }
        }

        public String Temper4HeatPower
        {
            get { return mTemper4HeatPower; }
            set
            {
                mTemper4HeatPower = value;
                Notify("Temper4HeatPower");
            }
        }

        public String Temper5HeatPower
        {
            get { return mTemper5HeatPower; }
            set
            {
                mTemper5HeatPower = value;
                Notify("Temper5HeatPower");
            }
        }

        public String Temper6HeatPower
        {
            get { return mTemper6HeatPower; }
            set
            {
                mTemper6HeatPower = value;
                Notify("Temper6HeatPower");
            }
        }

        public string PaddlePosAct
        {
            get { return mPaddlePosAct; }
            set
            {
                mPaddlePosAct = value;
                Notify("PaddlePosAct");
            }
        }

        public int PaddlePosSp
        {
            get { return mPaddlePosSp; }
            set
            {
                mPaddlePosSp = value;
                Notify("PaddlePosSp");
            }
        }

        public int EditPaddleSpeedSp
        {
            get { return mEditPaddleSpeedSp; }
            set
            {
                mEditPaddleSpeedSp = value;
                Notify("EditPaddleSpeedSp");
            }
        }

        public int PaddleSpeedSp
        {
            get { return mPaddleSpeedSp; }
            set
            {
                mPaddleSpeedSp = value;
                Notify("PaddleSpeedSp");
            }
        }

        public int ProcessRemainingTime
        {
            get { return mProcessRemainingTime; }
            set
            {
                mProcessRemainingTime = value;
                Notify("ProcessRemainingTimeS");
            }
        }

        public string ProcessRemainingTimeS
        {
            get { return string.Format("{2}:{1}:{0}", (mProcessRemainingTime % 3600) % 60, (int)((mProcessRemainingTime % 3600) / 60), ((int)mProcessRemainingTime / 3600)); }
        }

        public int FurnaceHeight
        {
            get { return mFurnaceHeight; }
            set
            {
                mFurnaceHeight = value;
                //Notify("FurnaceHeight");
            }
        }

        void Notify(string propName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}
