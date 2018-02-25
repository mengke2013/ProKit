using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.model;
using Demo.ui.model;

namespace Demo.ui.converter
{
    class RecipeConverter
    {
        public static void ConvertRecipePageModel(TubeRecipePageModel recipePage, RecipeStep step)
        {
            recipePage.TubeRecipeViewModel.UpdateView = true;
            recipePage.TubeRecipeViewModel.StepIndex = step.StepIndex;
            recipePage.TubeRecipeViewModel.StepName = step.StepName;
            recipePage.TubeRecipeViewModel.StepType = step.StepType;
            recipePage.TubeRecipeViewModel.StepTime = step.StepTime;

            recipePage.TubeRecipeViewModel.Gas1Sp = step.Gas1Sp;
            recipePage.TubeRecipeViewModel.Gas2Sp = step.Gas2Sp;
            recipePage.TubeRecipeViewModel.Gas5Sp = step.Gas5Sp;
            recipePage.TubeRecipeViewModel.Gas6Sp = step.Gas6Sp;
            recipePage.TubeRecipeViewModel.Gas8Sp = step.Gas8Sp;
            recipePage.TubeRecipeViewModel.Ana1Sp = step.Ana1Sp;
            recipePage.TubeRecipeViewModel.Temper1Sp = step.Temper1Sp;
            recipePage.TubeRecipeViewModel.Temper2Sp = step.Temper2Sp;
            recipePage.TubeRecipeViewModel.Temper3Sp = step.Temper3Sp;
            recipePage.TubeRecipeViewModel.Temper4Sp = step.Temper4Sp;
            recipePage.TubeRecipeViewModel.Temper5Sp = step.Temper5Sp;
            recipePage.TubeRecipeViewModel.Temper6Sp = step.Temper6Sp;

            recipePage.TubeRecipeViewModel.TemperRegulInt = step.TemperRegulInt;
            recipePage.TubeRecipeViewModel.AxisPosSp = step.AxisPosSp;
            recipePage.TubeRecipeViewModel.AxisSpeedSp = step.AxisSpeedSp;
            recipePage.TubeRecipeViewModel.Ramp = step.Ramp;
            recipePage.TubeRecipeViewModel.DigOutput = step.DigOutput;
            recipePage.TubeRecipeViewModel.Ev = step.Ev;
            recipePage.TubeRecipeViewModel.Num = step.Num;
            recipePage.TubeRecipeViewModel.CheckSum = step.CheckSum;

            recipePage.TubeRecipeViewModel.AnalogAbort = step.AnalogAbort;
            recipePage.TubeRecipeViewModel.DigitalAbort = step.DigitalAbort;
            recipePage.TubeRecipeViewModel.TemperAbort = step.TemperAbort;
            recipePage.TubeRecipeViewModel.ManualAbort = step.ManualAbort;
            recipePage.TubeRecipeViewModel.PowerAbort = step.PowerAbort;
            recipePage.TubeRecipeViewModel.AnalogDelay = step.AnalogDelay;
            recipePage.TubeRecipeViewModel.MfcDelay = step.MfcDelay;
            recipePage.TubeRecipeViewModel.AlrmDigIns = step.AlrmDigIns;
            recipePage.TubeRecipeViewModel.UpdateView = false;
        }

        public static void ConvertRecipeModel(RecipeStep step, TubeRecipePageModel recipePage)
        {
            step.StepIndex = recipePage.TubeRecipeViewModel.StepIndex;
            step.StepName = recipePage.TubeRecipeViewModel.StepName;
            step.StepType = recipePage.TubeRecipeViewModel.StepType;
            step.StepTime = recipePage.TubeRecipeViewModel.StepTime;

            step.Gas1Sp = recipePage.TubeRecipeViewModel.Gas1Sp;
            step.Gas2Sp = recipePage.TubeRecipeViewModel.Gas2Sp;
            step.Gas5Sp = recipePage.TubeRecipeViewModel.Gas5Sp;
            step.Gas6Sp = recipePage.TubeRecipeViewModel.Gas6Sp;
            step.Gas8Sp = recipePage.TubeRecipeViewModel.Gas8Sp;
            step.Ana1Sp = recipePage.TubeRecipeViewModel.Ana1Sp;
            step.Temper1Sp = recipePage.TubeRecipeViewModel.Temper1Sp;
            step.Temper2Sp = recipePage.TubeRecipeViewModel.Temper2Sp;
            step.Temper3Sp = recipePage.TubeRecipeViewModel.Temper3Sp;
            step.Temper4Sp = recipePage.TubeRecipeViewModel.Temper4Sp;
            step.Temper5Sp = recipePage.TubeRecipeViewModel.Temper5Sp;
            step.Temper6Sp = recipePage.TubeRecipeViewModel.Temper6Sp;

            step.TemperRegulInt = recipePage.TubeRecipeViewModel.TemperRegulInt;
            step.AxisPosSp = recipePage.TubeRecipeViewModel.AxisPosSp;
            step.AxisSpeedSp = recipePage.TubeRecipeViewModel.AxisSpeedSp;
            step.Ramp = recipePage.TubeRecipeViewModel.Ramp;
            step.DigOutput = recipePage.TubeRecipeViewModel.DigOutput;
            step.Ev = recipePage.TubeRecipeViewModel.Ev;
            step.Num = recipePage.TubeRecipeViewModel.Num;
            step.CheckSum = recipePage.TubeRecipeViewModel.CheckSum;

            step.AnalogAbort = recipePage.TubeRecipeViewModel.AnalogAbort;
            step.DigitalAbort = recipePage.TubeRecipeViewModel.DigitalAbort;
            step.TemperAbort = recipePage.TubeRecipeViewModel.TemperAbort;
            step.ManualAbort = recipePage.TubeRecipeViewModel.ManualAbort;
            step.PowerAbort = recipePage.TubeRecipeViewModel.PowerAbort;
            step.AnalogDelay = recipePage.TubeRecipeViewModel.AnalogDelay;
            step.MfcDelay = recipePage.TubeRecipeViewModel.MfcDelay;
            step.AlrmDigIns = recipePage.TubeRecipeViewModel.AlrmDigIns;
        }
    }
}
