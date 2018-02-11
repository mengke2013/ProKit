using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;


namespace Demo.ui.model
{
    class TubeRecipePageModel 
    {
        private List<StepListItemModel> mStepListItemModels;
        private TubeRecipeViewModel mRecipeViewModel;

        public TubeRecipePageModel()
        {
            mStepListItemModels = new List<StepListItemModel>();
            for (int i = 0; i < 64; ++i)
            {
                StepListItemModel stepListItemModel = new StepListItemModel(i + 1);
                stepListItemModel.RowIndex = i;
                mStepListItemModels.Add(stepListItemModel);
            }
        }

        public void LoadData(byte selectedTube)
        {
            mRecipeViewModel = new TubeRecipeViewModel(1);
            mRecipeViewModel.StepName = "Test Step";
            mRecipeViewModel.StepType = 1;
            mRecipeViewModel.StepTime = 60;

            mRecipeViewModel.Gas1Sp = 1000;
            mRecipeViewModel.Gas2Sp = 2000;
            mRecipeViewModel.Gas5Sp = 3000;
            mRecipeViewModel.Gas6Sp = 4000;
            mRecipeViewModel.Gas8Sp = 5000;
            mRecipeViewModel.Ana1Sp = 6000;
            mRecipeViewModel.Temper1Sp = 100;
            mRecipeViewModel.Temper2Sp = 200;
            mRecipeViewModel.Temper3Sp = 300;
            mRecipeViewModel.Temper4Sp = 400;
            mRecipeViewModel.Temper5Sp = 500;
            mRecipeViewModel.Temper6Sp = 600;

            mRecipeViewModel.Gas1Abort = 1;
            mRecipeViewModel.Gas1Hold = 2;
            mRecipeViewModel.Gas1Alarm = 3;
            mRecipeViewModel.Gas1Next = 4;
            mRecipeViewModel.Gas2Abort = 5;
            mRecipeViewModel.Gas2Hold = 6;
            mRecipeViewModel.Gas2Alarm = 7;
            mRecipeViewModel.Gas2Next = 8;
            mRecipeViewModel.Gas5Abort = 9;
            mRecipeViewModel.Gas5Hold = 10;
            mRecipeViewModel.Gas5Alarm = 11;
            mRecipeViewModel.Gas5Next = 12;
            mRecipeViewModel.Gas6Abort = 13;
            mRecipeViewModel.Gas6Hold = 14;
            mRecipeViewModel.Gas6Alarm = 15;
            mRecipeViewModel.Gas6Next = 16;
            mRecipeViewModel.Gas8Abort = 17;
            mRecipeViewModel.Gas8Hold = 18;
            mRecipeViewModel.Gas8Alarm = 19;
            mRecipeViewModel.Gas8Next = 20;
            mRecipeViewModel.Ana1Abort = 21;
            mRecipeViewModel.Ana1Hold = 22;
            mRecipeViewModel.Ana1Alarm = 23;
            mRecipeViewModel.Ana1Next = 24;

            mRecipeViewModel.Temper1Abort = 1;
            mRecipeViewModel.Temper1Hold = 2;
            mRecipeViewModel.Temper1Alarm = 3;
            mRecipeViewModel.Temper1Next = 4;
            mRecipeViewModel.Temper2Abort = 5;
            mRecipeViewModel.Temper2Hold = 6;
            mRecipeViewModel.Temper2Alarm = 7;
            mRecipeViewModel.Temper2Next = 8;
            mRecipeViewModel.Temper3Abort = 9;
            mRecipeViewModel.Temper3Hold = 10;
            mRecipeViewModel.Temper3Alarm = 11;
            mRecipeViewModel.Temper3Next = 12;
            mRecipeViewModel.Temper4Abort = 13;
            mRecipeViewModel.Temper4Hold = 14;
            mRecipeViewModel.Temper4Alarm = 15;
            mRecipeViewModel.Temper4Next = 16;
            mRecipeViewModel.Temper5Abort = 17;
            mRecipeViewModel.Temper5Hold = 18;
            mRecipeViewModel.Temper5Alarm = 19;
            mRecipeViewModel.Temper5Next = 20;
            mRecipeViewModel.Temper6Abort = 21;
            mRecipeViewModel.Temper6Hold = 22;
            mRecipeViewModel.Temper6Alarm = 23;
            mRecipeViewModel.Temper6Next = 24;

            mRecipeViewModel.AxisPosSp = 1000;
            mRecipeViewModel.AxisSpeedSp = 300;

            mRecipeViewModel.AnalogAbort = 51;
            mRecipeViewModel.DigitalAbort = 52;
            mRecipeViewModel.TemperAbort = 53;
            mRecipeViewModel.ManualAbort = 54;
            mRecipeViewModel.PowerAbort = 55;
            mRecipeViewModel.MfcDelay = 56;
            mRecipeViewModel.AnalogDelay = 57;

            System.Int32 test = 1;
            //test |= 1 << 0;
            test &= ~(1 << 0);
            test |= 1 << 3;
            mRecipeViewModel.AnalogDelay = test;
            //intValue |= 1 << bitPosition;
            //intValue &= ~(1 << bitPosition);
        }

        public void ParseRecipeData(byte[] recipeBytes)
        {
            TubeRecipeViewModel.StepName = Encoding.ASCII.GetString(recipeBytes, 0, 32).TrimEnd('\0');
            TubeRecipeViewModel.StepType = (sbyte)recipeBytes[36];
            TubeRecipeViewModel.StepTime = BitConverter.ToInt32(recipeBytes, 32);

            TubeRecipeViewModel.Gas1Sp = BitConverter.ToInt16(recipeBytes, 77);
            TubeRecipeViewModel.Gas2Sp = BitConverter.ToInt16(recipeBytes, 83);
            TubeRecipeViewModel.Gas5Sp = BitConverter.ToInt16(recipeBytes, 101);
            TubeRecipeViewModel.Gas6Sp = BitConverter.ToInt16(recipeBytes, 107);
            TubeRecipeViewModel.Gas8Sp = BitConverter.ToInt16(recipeBytes, 119);
            TubeRecipeViewModel.Ana1Sp = BitConverter.ToInt16(recipeBytes, 125);
        }

        public List<StepListItemModel> StepListItems
        {
            get
            {
                return mStepListItemModels;
            }
        }

        public TubeRecipeViewModel TubeRecipeViewModel
        {
            get { return mRecipeViewModel; }
        }

    }
}
