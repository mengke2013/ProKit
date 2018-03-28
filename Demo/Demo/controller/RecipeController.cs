using System;

using Demo.ui.model;
using Demo.ui.view;
using Demo.model;
using Demo.service;

namespace Demo.controller
{
    public class RecipeController
    {
        private TubeRecipePage mPage;

        public RecipeController(TubeRecipePage page)
        {
            mPage = page;
        }

        public void LoadRecipe(byte tubeIndex)
        {
            //add validation
            RecipeService.Instance.LoadRecipe(tubeIndex);
            SettingsService.Instance.LoadSettings(tubeIndex);
            UpdateRecipeLabel();
        }

        public bool SynRecipe(byte tubeIndex, RecipeService.OnSynRecipeComplete rCallback, RecipeService.OnSynStepComplete sCallback)
        {
            //add validation
            bool startSyn = RecipeService.Instance.SynRecipe(tubeIndex, rCallback, sCallback);
            return startSyn;
        }

        public bool SynStep(byte tubeIndex, byte stepIndex, RecipeService.OnSynRecipeComplete rCallback, RecipeService.OnSynStepComplete sCallback)
        {
            //add validation
            bool startSyn = RecipeService.Instance.SynStep(tubeIndex, stepIndex, rCallback, sCallback);
            return startSyn;
        }

        public bool DownloadRecipe(string fileName, byte tubeIndex, RecipeService.OnDownloadRecipeComplete rCallback, RecipeService.OnDownloadStepComplete sCallback)
        {
            //add validation
            bool startDownload = RecipeService.Instance.DownloadRecipe(fileName, tubeIndex, rCallback, sCallback);
            return startDownload;
        }

        public bool BackupRecipe(string fileName, byte tubeIndex, RecipeService.OnBackupRecipeComplete callback)
        {
            //add validation
            bool startBackup = RecipeService.Instance.BackupRecipe(fileName, tubeIndex, callback);
            return startBackup;
        }

        public bool CommitStep(byte tubeIndex, int stepIndex, RecipeService.OnDownloadRecipeComplete rCallback, RecipeService.OnDownloadStepComplete sCallback)
        {
            //add validation
            bool startCommit = RecipeService.Instance.CommitStep(tubeIndex, stepIndex, rCallback, sCallback);
            return startCommit;
        }

        public RecipeStep GetRecipeStep(byte tubeIndex, int stepIndex)
        {
            //add validation
            RecipeStep step = RecipeService.Instance.GetRecipeStep(stepIndex);
            return step;
        }

        public string GetRecipeName(byte tubeIndex)
        {
            //add validation
            string recipeName = ProcessService.Instance.GetProcessName(tubeIndex);
            return recipeName;
        }

        public void ConvertRecipePageModel(byte stepIndex)
        {
            TubeRecipeViewModel viewModel = mPage.ViewModel;
            RecipeStep step = RecipeService.Instance.GetRecipeStep(stepIndex);
            viewModel.UpdateView = true;
            viewModel.StepIndex = step.StepIndex;
            viewModel.StepName = step.StepName;
            viewModel.StepType = step.StepType;
            viewModel.StepTime = step.StepTime;

            viewModel.Gas1Sp = step.Gas1Sp;
            viewModel.Gas2Sp = step.Gas2Sp;
            viewModel.Gas5Sp = step.Gas5Sp;
            viewModel.Gas6Sp = step.Gas6Sp;
            viewModel.Gas8Sp = step.Gas8Sp;
            viewModel.Ana1Sp = step.Ana1Sp;
            viewModel.Temper1Sp = step.Temper1Sp;
            viewModel.Temper2Sp = step.Temper2Sp;
            viewModel.Temper3Sp = step.Temper3Sp;
            viewModel.Temper4Sp = step.Temper4Sp;
            viewModel.Temper5Sp = step.Temper5Sp;
            viewModel.Temper6Sp = step.Temper6Sp;

            viewModel.TemperRegulInt = step.TemperRegulInt;
            viewModel.AxisPosSp = step.AxisPosSp;
            viewModel.AxisSpeedSp = step.AxisSpeedSp;
            viewModel.Ramp = step.Ramp;
            viewModel.DigOutput = step.DigOutput;
            viewModel.Ev = step.Ev;
            viewModel.Num = step.Num;
            viewModel.CheckSum = step.CheckSum;

            viewModel.AnalogAbort = step.AnalogAbort;
            viewModel.DigitalAbort = step.DigitalAbort;
            viewModel.TemperAbort = step.TemperAbort;
            viewModel.ManualAbort = step.ManualAbort;
            viewModel.PowerAbort = step.PowerAbort;
            viewModel.AnalogDelay = step.AnalogDelay;
            viewModel.MfcDelay = step.MfcDelay;
            viewModel.AlrmDigIns = step.AlrmDigIns;

            viewModel.Gas1Abort = step.Gas1Abort;
            viewModel.Gas2Abort = step.Gas2Abort;
            viewModel.Gas5Abort = step.Gas5Abort;
            viewModel.Gas6Abort = step.Gas6Abort;
            viewModel.Gas8Abort = step.Gas8Abort;
            viewModel.Ana1Abort = step.Ana1Abort;
            viewModel.Gas1Hold = step.Gas1Hold;
            viewModel.Gas1Hold = step.Gas2Hold;
            viewModel.Gas1Hold = step.Gas5Hold;
            viewModel.Gas1Hold = step.Gas6Hold;
            viewModel.Gas1Hold = step.Gas8Hold;
            viewModel.Ana1Hold = step.Ana1Hold;
            viewModel.Gas1Alarm = step.Gas1Alarm;
            viewModel.Gas2Alarm = step.Gas2Alarm;
            viewModel.Gas5Alarm = step.Gas5Alarm;
            viewModel.Gas6Alarm = step.Gas6Alarm;
            viewModel.Gas8Alarm = step.Gas8Alarm;
            viewModel.Ana1Alarm = step.Ana1Alarm;
            viewModel.Gas1Next = step.Gas1Next;
            viewModel.Gas2Next = step.Gas2Alarm;
            viewModel.Gas5Next = step.Gas5Next;
            viewModel.Gas6Next = step.Gas6Next;
            viewModel.Gas8Next = step.Gas8Next;
            viewModel.Ana1Next = step.Ana1Next;
            viewModel.Temper1Abort = step.Temper1Abort;
            viewModel.Temper2Abort = step.Temper2Abort;
            viewModel.Temper3Abort = step.Temper3Abort;
            viewModel.Temper4Abort = step.Temper4Abort;
            viewModel.Temper5Abort = step.Temper5Abort;
            viewModel.Temper6Abort = step.Temper6Abort;
            viewModel.Temper1Hold = step.Temper1Hold;
            viewModel.Temper2Hold = step.Temper2Hold;
            viewModel.Temper3Hold = step.Temper3Hold;
            viewModel.Temper4Hold = step.Temper4Hold;
            viewModel.Temper5Hold = step.Temper5Hold;
            viewModel.Temper6Hold = step.Temper6Hold;
            viewModel.Temper1Alarm = step.Temper1Alarm;
            viewModel.Temper2Alarm = step.Temper2Alarm;
            viewModel.Temper3Alarm = step.Temper3Alarm;
            viewModel.Temper4Alarm = step.Temper4Alarm;
            viewModel.Temper5Alarm = step.Temper5Alarm;
            viewModel.Temper6Alarm = step.Temper6Alarm;
            viewModel.Temper1Next = step.Temper1Next;
            viewModel.Temper2Next = step.Temper2Next;
            viewModel.Temper3Next = step.Temper3Next;
            viewModel.Temper4Next = step.Temper4Next;
            viewModel.Temper5Next = step.Temper5Next;
            viewModel.Temper6Next = step.Temper6Next;


            viewModel.UpdateView = false;
        }

        public void ConvertRecipeModel(byte stepIndex)
        {
            //add validation
            TubeRecipeViewModel viewModel = mPage.ViewModel;
            RecipeStep step = RecipeService.Instance.GetRecipeStep(stepIndex);
            step.StepIndex = viewModel.StepIndex;
            step.StepName = viewModel.StepName;
            step.StepType = viewModel.StepType;
            step.StepTime = viewModel.StepTime;

            step.Gas1Sp = viewModel.Gas1Sp;
            step.Gas2Sp = viewModel.Gas2Sp;
            step.Gas5Sp = viewModel.Gas5Sp;
            step.Gas6Sp = viewModel.Gas6Sp;
            step.Gas8Sp = viewModel.Gas8Sp;
            step.Ana1Sp = viewModel.Ana1Sp;
            step.Temper1Sp = viewModel.Temper1Sp;
            step.Temper2Sp = viewModel.Temper2Sp;
            step.Temper3Sp = viewModel.Temper3Sp;
            step.Temper4Sp = viewModel.Temper4Sp;
            step.Temper5Sp = viewModel.Temper5Sp;
            step.Temper6Sp = viewModel.Temper6Sp;

            step.TemperRegulInt = viewModel.TemperRegulInt;
            step.AxisPosSp = viewModel.AxisPosSp;
            step.AxisSpeedSp = viewModel.AxisSpeedSp;
            step.Ramp = viewModel.Ramp;
            step.DigOutput = viewModel.DigOutput;
            step.Ev = viewModel.Ev;
            step.Num = viewModel.Num;
            step.CheckSum = viewModel.CheckSum;

            step.AnalogAbort = viewModel.AnalogAbort;
            step.DigitalAbort = viewModel.DigitalAbort;
            step.TemperAbort = viewModel.TemperAbort;
            step.ManualAbort = viewModel.ManualAbort;
            step.PowerAbort = viewModel.PowerAbort;
            step.AnalogDelay = viewModel.AnalogDelay;
            step.MfcDelay = viewModel.MfcDelay;
            step.AlrmDigIns = viewModel.AlrmDigIns;

            step.Gas1Abort = viewModel.Gas1Abort;
            step.Gas2Abort = viewModel.Gas2Abort;
            step.Gas5Abort = viewModel.Gas5Abort;
            step.Gas6Abort = viewModel.Gas6Abort;
            step.Gas8Abort = viewModel.Gas8Abort;
            step.Ana1Abort = viewModel.Ana1Abort;
            step.Gas1Hold = viewModel.Gas1Hold;
            step.Gas2Hold = viewModel.Gas2Hold;
            step.Gas5Hold = viewModel.Gas5Hold;
            step.Gas6Hold = viewModel.Gas6Hold;
            step.Gas8Hold = viewModel.Gas8Hold;
            step.Ana1Hold = viewModel.Ana1Hold;
            step.Gas1Alarm = viewModel.Gas1Alarm;
            step.Gas2Alarm = viewModel.Gas2Alarm;
            step.Gas5Alarm = viewModel.Gas5Alarm;
            step.Gas6Alarm = viewModel.Gas6Alarm;
            step.Gas8Alarm = viewModel.Gas8Alarm;
            step.Ana1Alarm = viewModel.Ana1Alarm;
            step.Gas1Next = viewModel.Gas1Next;
            step.Gas2Next = viewModel.Gas2Next;
            step.Gas5Next = viewModel.Gas5Next;
            step.Gas6Next = viewModel.Gas6Next;
            step.Gas8Next = viewModel.Gas8Next;
            step.Ana1Next = viewModel.Ana1Next;
            step.Temper1Abort = viewModel.Temper1Abort;
            step.Temper2Abort = viewModel.Temper2Abort;
            step.Temper3Abort = viewModel.Temper3Abort;
            step.Temper4Abort = viewModel.Temper4Abort;
            step.Temper5Abort = viewModel.Temper5Abort;
            step.Temper6Abort = viewModel.Temper6Abort;
            step.Temper1Hold = viewModel.Temper1Hold;
            step.Temper2Hold = viewModel.Temper2Hold;
            step.Temper3Hold = viewModel.Temper3Hold;
            step.Temper4Hold = viewModel.Temper4Hold;
            step.Temper5Hold = viewModel.Temper5Hold;
            step.Temper6Hold = viewModel.Temper6Hold;
            step.Temper1Alarm = viewModel.Temper1Alarm;
            step.Temper2Alarm = viewModel.Temper2Alarm;
            step.Temper3Alarm = viewModel.Temper3Alarm;
            step.Temper4Alarm = viewModel.Temper4Alarm;
            step.Temper5Alarm = viewModel.Temper5Alarm;
            step.Temper6Alarm = viewModel.Temper6Alarm;
            step.Temper1Next = viewModel.Temper1Next;
            step.Temper2Next = viewModel.Temper2Next;
            step.Temper3Next = viewModel.Temper3Next;
            step.Temper4Next = viewModel.Temper4Next;
            step.Temper5Next = viewModel.Temper5Next;
            step.Temper6Next = viewModel.Temper6Next;
        }

        private void UpdateRecipeLabel()
        {
            TubeRecipeViewModel viewModel = mPage.ViewModel;
            Settings settings = SettingsService.Instance.GetSettings();
            viewModel.Gas1Name = settings.Gas1Name ;
            viewModel.Gas2Name = settings.Gas2Name;
            viewModel.Gas5Name = settings.Gas5Name;
            viewModel.Gas6Name = settings.Gas6Name;
            viewModel.Gas8Name = settings.Gas8Name;
            viewModel.Ana1Name = settings.Ana1Name;
            viewModel.EvName1 = settings.EvNames[0];
            viewModel.EvName2 = settings.EvNames[1];
            viewModel.EvName3 = settings.EvNames[2];
            viewModel.EvName4 = settings.EvNames[3];
            viewModel.EvName5 = settings.EvNames[4];
            viewModel.EvName6 = settings.EvNames[5];
            viewModel.EvName7 = settings.EvNames[6];
            viewModel.EvName8 = settings.EvNames[7];
            viewModel.EvName9 = settings.EvNames[8];
            viewModel.EvName10 = settings.EvNames[9];
            viewModel.EvName11 = settings.EvNames[10];
            viewModel.EvName12 = settings.EvNames[11];
            viewModel.EvName13 = settings.EvNames[12];
            viewModel.EvName14 = settings.EvNames[13];
            viewModel.EvName15 = settings.EvNames[14];
            viewModel.EvName16 = settings.EvNames[15];
            viewModel.DoName1 = settings.DoNames[0];
            viewModel.DoName2 = settings.DoNames[1];
            viewModel.DoName3 = settings.DoNames[2];
            viewModel.DoName4 = settings.DoNames[3];
            viewModel.DoName5 = settings.DoNames[4];
            viewModel.DoName6 = settings.DoNames[5];
            viewModel.DoName7 = settings.DoNames[6];
            viewModel.DoName8 = settings.DoNames[7];

            string[] diNames = new string[24];
            Array.Copy(settings.DiNames, 0, diNames, 0, 24);
            viewModel.DiNames = diNames;
        }
    }
}
