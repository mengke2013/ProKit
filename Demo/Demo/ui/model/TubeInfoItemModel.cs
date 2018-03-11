using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Demo.utilities;
using System.Collections;

namespace Demo.ui.model
{
    public class TubeInfoItemModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        void Notify(string propName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
