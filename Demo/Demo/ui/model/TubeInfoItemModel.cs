using System;
using System.ComponentModel;
using System.Collections;
using System.Windows;

namespace Demo.ui.model
{
    public class TubeInfoItemModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool mViewVisible;
        private int mFurnaceHeight;
        private string mTabBackground;

        private byte mTubeIndex;
        private int mProcessRemainingTime;
        private string mProcessName;
        private string mProcessStatus;

        private string mGas1CurMeas;
        private string mGas2CurMeas;
        private string mGas5CurMeas;
        private string mGas6CurMeas;
        private string mGas8CurMeas;

        private string mAna1CurMeas;
        private string mAna3CurMeas;
        private string mAna4CurMeas;

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

        private int mEvValue;
        private string[] mEvColors;
        private string[] mPipeColors;

        private string mGas1Name;
        private string mGas2Name;
        private string mGas5Name;
        private string mGas6Name;
        private string mGas8Name;
        private string mAna1Name;
        private string mAna3Name;
        private string mAna4Name;

        private bool mAlarm;
        private bool mLocked;

        public TubeInfoItemModel(byte tubeIndex)
        {
            mTubeIndex = tubeIndex;

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
            mTabBackground = "white";
        }

        public int FurnaceHeight
        {
            get { return mFurnaceHeight; }
            set
            {
                mFurnaceHeight = value;
                Notify("FurnaceHeight");
            }
        }

        public string TabBackground
        {
            get { return mTabBackground; }
            set
            {
                mTabBackground = value;
                Notify("TabBackground");
            }
        }

        public byte TubeIndex
        {
            get { return mTubeIndex; }
            set { mTubeIndex = value; }
        }

        public string TubeIndexS
        {
            get { return "Tube" + mTubeIndex; }
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
                        mEvColors[i] = "#FFD3C7C7";// "#FF18A6AD";
                        mPipeColors[i] = "#FFD3C7C7";// "#FFBDB7FB";// "#FFBFF3F7";
                    }
                    Notify("Ev" + (i + 1) + "Color");
                    Notify("Pipe" + (i + 1) + "Color");
                }
            }
        }

        public bool Alarm
        {
            get { return mAlarm; }
            set
            {
                mAlarm = value;
                Notify("AlarmV");
            }
        }

        public bool Locked
        {
            get { return mLocked; }
            set
            {
                mLocked = value;
                Notify("LabelColor");
                Notify("ProcessColor");
                Notify("LockedT");
            }
        }

        public Visibility AlarmV
        {
            get { return mAlarm ? Visibility.Visible : Visibility.Hidden; }
        }

        public string LabelColor
        {
            get { return mLocked ? "gray" : "#FF236EC9"; }
        }

        public string LockedT
        {
            get { return mLocked ? "UnLock":"Lock"; }
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

        public bool ViewVisible
        {
            get { return mViewVisible; }
            set
            {
                mViewVisible = value;
                Notify("ViewVisibleV");
            }
        }

        public string ProcessColor
        {
            get { return mLocked ? "gray" : "green"; }
        }

        public Visibility ViewVisibleV
        {
            get { return mViewVisible?Visibility.Visible:Visibility.Hidden; }
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

        void Notify(string propName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
