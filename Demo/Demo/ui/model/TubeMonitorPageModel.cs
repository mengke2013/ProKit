using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Rocky.Core.Opc.Ua;
using Demo.com;

namespace Demo.ui.model
{
    class TubeMonitorPageModel : INotifyPropertyChanged
    {
        private byte mSelectedTube = 1;
        private byte mPreSelectedTube = 1;
        private NodeValueUpdateEventHandler mPreUpdateHandler;
        private string mGas1Sp;
        private string mGas2Sp;
        private string mGas5Sp;
        private string mGas6Sp;
        private string mGas8Sp;
        private string mGas1CurMeas;
        private string mGas2CurMeas;
        private string mGas5CurMeas;
        private string mGas6CurMeas;
        private string mGas8CurMeas;
        private string mAna1Sp;
        private string mAna3Sp;
        private string mAna4Sp;
        private string mAna1CurMeas;
        private string mAna3CurMeas;
        private string mAna4CurMeas;
        private string mTemper1Sp;
        private string mTemper2Sp;
        private string mTemper3Sp;
        private string mTemper4Sp;
        private string mTemper5Sp;
        private string mTemper6Sp;
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
        private string mTemper1HeatPower;
        private string mTemper2HeatPower;
        private string mTemper3HeatPower;
        private string mTemper4HeatPower;
        private string mTemper5HeatPower;
        private string mTemper6HeatPower;
        private string mPaddlePosAct;
        private string mPaddlePosSp;
        private string mPaddleSpeedSp;
        private string[] mEvColors;
        private string[] mPipeColors;

        private TubePageStyle mTubePageStyle;

        public event PropertyChangedEventHandler PropertyChanged;

        public TubeMonitorPageModel()
        {
            mEvColors = new string[32];
            for (int i = 0; i < 32; ++i)
            {
                mEvColors[i] = "#FFD3C7C7";
            }

            mPipeColors = new string[14];
            for(int i = 0; i < 14; ++i)
            {
                mPipeColors[i] = "#FFD3C7C7";
            }
        }

        public void UpdateDataSource()
        {
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].MfcNodeComponent.GasNodeComponents[0].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].MfcNodeComponent.GasNodeComponents[1].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].MfcNodeComponent.GasNodeComponents[4].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].MfcNodeComponent.GasNodeComponents[5].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].MfcNodeComponent.GasNodeComponents[7].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].MfcNodeComponent.GasNodeComponents[0].CurMeas.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].MfcNodeComponent.GasNodeComponents[1].CurMeas.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].MfcNodeComponent.GasNodeComponents[4].CurMeas.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].MfcNodeComponent.GasNodeComponents[5].CurMeas.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].MfcNodeComponent.GasNodeComponents[7].CurMeas.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[0].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[2].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[3].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].CurSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].IntValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].IntValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].IntValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].IntValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].IntValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].IntValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].ExtValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].ExtValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].ExtValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].ExtValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].ExtValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].ExtValue.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].HeatPower.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].HeatPower.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].HeatPower.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].HeatPower.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].HeatPower.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].HeatPower.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].PaddleNodeComponent.PosAct.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].PaddleNodeComponent.CurPosSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mPreSelectedTube - 1].PaddleNodeComponent.CurSpeedSp.Notification -= mPreUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].DioNodeComponent.Ev.Notification -= mPreUpdateHandler;
            NodeValueUpdateEventHandler newUpdateHandler = new NodeValueUpdateEventHandler(NodeValueUpdate);
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[0].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[1].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[4].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[5].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[7].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[0].CurMeas.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[1].CurMeas.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[4].CurMeas.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[5].CurMeas.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[7].CurMeas.Notification += newUpdateHandler;

            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[0].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[2].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[3].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas.Notification += newUpdateHandler;

            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].CurSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].IntValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].IntValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].IntValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].IntValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].IntValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].IntValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].ExtValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].ExtValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].ExtValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].ExtValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].ExtValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].ExtValue.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].HeatPower.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].HeatPower.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].HeatPower.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].HeatPower.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].HeatPower.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].HeatPower.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].PaddleNodeComponent.PosAct.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].PaddleNodeComponent.CurPosSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].PaddleNodeComponent.CurSpeedSp.Notification += newUpdateHandler;
            ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].DioNodeComponent.Ev.Notification += newUpdateHandler;
            mPreUpdateHandler = newUpdateHandler;
        }

        private void UpdateEv()
        {
            try {
               System.Int32 val = (System.Int32)ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].DioNodeComponent.Ev.Value;
                var arr = new BitArray(BitConverter.GetBytes(val));
           
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
                    Notify("Ev"+(i+1)+"Color");
                    Notify("Pipe" + (i + 1) + "Color");
                }
            }
            catch (Exception ee)
            {
                
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

        public string Gas1Sp
        {
            get { return mGas1Sp; }
            set
            {
                mGas1Sp = value;
                Notify("Gas1Sp");
            }
        }

        public string Gas2Sp
        {
            get { return mGas2Sp; }
            set
            {
                mGas2Sp = value;
                Notify("Gas2Sp");
            }
        }

        public string Gas5Sp
        {
            get { return mGas5Sp; }
            set
            {
                mGas5Sp = value;
                Notify("Gas5Sp");
            }
        }

        public string Gas6Sp
        {
            get { return mGas6Sp; }
            set
            {
                mGas6Sp = value;
                Notify("Gas6Sp");
            }
        }

        public string Gas8Sp
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

        public string Ana1Sp
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

        public string Temper1Sp
        {
            get { return mTemper1Sp; }
            set
            {
                mTemper1Sp = value;
                Notify("Temper1Sp");
            }
        }

        public string Temper2Sp
        {
            get { return mTemper2Sp; }
            set
            {
                mTemper2Sp = value;
                Notify("Temper2Sp");
            }
        }

        public string Temper3Sp
        {
            get { return mTemper3Sp; }
            set
            {
                mTemper3Sp = value;
                Notify("Temper3Sp");
            }
        }

        public string Temper4Sp
        {
            get { return mTemper4Sp; }
            set
            {
                mTemper4Sp = value;
                Notify("Temper4Sp");
            }
        }

        public string Temper5Sp
        {
            get { return mTemper5Sp; }
            set
            {
                mTemper5Sp = value;
                Notify("Temper5Sp");
            }
        }

        public string Temper6Sp
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

        public String PaddlePosAct
        {
            get { return mPaddlePosAct; }
            set
            {
                mPaddlePosAct = value;
                Notify("PaddlePosAct");
            }
        }

        public String PaddlePosSp
        {
            get { return mPaddlePosSp; }
            set
            {
                mPaddlePosSp = value;
                Notify("PaddlePosSp");
            }
        }

        public String PaddleSpeedSp
        {
            get { return mPaddleSpeedSp; }
            set
            {
                mPaddleSpeedSp = value;
                Notify("PaddleSpeedSp");
            }
        }

        void Notify(string propName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void NodeValueUpdate(OpcNode opcNode, Object newValue)
        {
            if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].CurSp.NodeID)
            {
                Temper1Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].CurSp.NodeID)
            {
                Temper2Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].CurSp.NodeID)
            {
                Temper3Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].CurSp.NodeID)
            {
                Temper4Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].CurSp.NodeID)
            {
                Temper5Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].CurSp.NodeID)
            {
                Temper6Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].IntValue.NodeID)
            {
                Temper1IntValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].IntValue.NodeID)
            {
                Temper2IntValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].IntValue.NodeID)
            {
                Temper3IntValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].IntValue.NodeID)
            {
                Temper4IntValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].IntValue.NodeID)
            {
                Temper5IntValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].IntValue.NodeID)
            {
                Temper6IntValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].ExtValue.NodeID)
            {
                Temper1ExtValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].ExtValue.NodeID)
            {
                Temper2ExtValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].ExtValue.NodeID)
            {
                Temper3ExtValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].ExtValue.NodeID)
            {
                Temper4ExtValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].ExtValue.NodeID)
            {
                Temper5ExtValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].ExtValue.NodeID)
            {
                Temper6ExtValue = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[0].HeatPower.NodeID)
            {
                Temper1HeatPower = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[1].HeatPower.NodeID)
            {
                Temper2HeatPower = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[2].HeatPower.NodeID)
            {
                Temper3HeatPower = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[3].HeatPower.NodeID)
            {
                Temper4HeatPower = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[4].HeatPower.NodeID)
            {
                Temper5HeatPower = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].FurnaceNodeComponent.TemperNodeComponents[5].HeatPower.NodeID)
            {
                Temper6HeatPower = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[0].CurSp.NodeID)
            {
                Gas1Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[1].CurSp.NodeID)
            {
                Gas2Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[4].CurSp.NodeID)
            {
                Gas5Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[5].CurSp.NodeID)
            {
                Gas6Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[7].CurSp.NodeID)
            {
                Gas8Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[0].CurMeas.NodeID)
            {
                Gas1CurMeas = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[1].CurMeas.NodeID)
            {
                Gas2CurMeas = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[4].CurMeas.NodeID)
            {
                Gas5CurMeas = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[5].CurMeas.NodeID)
            {
                Gas6CurMeas = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].MfcNodeComponent.GasNodeComponents[7].CurMeas.NodeID)
            {
                Gas8CurMeas = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[0].CurSp.NodeID)
            {
                Ana1Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[2].CurSp.NodeID)
            {
                Ana3Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[3].CurSp.NodeID)
            {
                Ana4Sp = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[0].CurMeas.NodeID)
            {
                Ana1CurMeas = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[2].CurMeas.NodeID)
            {
                Ana3CurMeas = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].VacuumNodeComponent.AnalogNodeComponents[3].CurMeas.NodeID)
            {
                Ana4CurMeas = newValue.ToString();
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].PaddleNodeComponent.PosAct.NodeID)
            {
                if (newValue != null)
                {
                    PaddlePosAct = newValue.ToString();
                }
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].PaddleNodeComponent.CurPosSp.NodeID)
            {
                if (newValue != null)
                {
                    PaddlePosSp = newValue.ToString();
                }
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].PaddleNodeComponent.CurSpeedSp.NodeID)
            {
                if (newValue != null)
                {
                    PaddleSpeedSp = newValue.ToString();
                }
            }
            else if (opcNode.NodeID == ComProcessNodeComponent.Instance.TubeNodeComponents[mSelectedTube - 1].DioNodeComponent.Ev.NodeID)
            {
                UpdateEv();
            }
        }
    }
}
