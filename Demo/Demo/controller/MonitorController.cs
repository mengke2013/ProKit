﻿using System;
using System.Collections.Generic;

using Demo.ui.view;
using Demo.ui.model;
using Demo.service;
using Demo.model;

namespace Demo.controller
{
    class MonitorController
    {
        public delegate void OnCommitEditSetpointComplete();

        private TubeMonitorPage mPage;
        ProcessService.OnCommitEditSetpointComplete mCommitChangeCompleteCallback;
        List<History> mCommitItems;


        public MonitorController(TubeMonitorPage page)
        {
            mPage = page;
            mCommitItems = new List<History>();
        }

        public void LoadMonitorData(byte tubeIndex)
        {
            SettingsService.Instance.LoadSettings(tubeIndex);
            UpdateLabels();
        }

        public void LoadMonitorSetpoints()
        {
            TubeMonitorViewModel uiModel = mPage.PageModel;
            uiModel.Ana1Sp = ProcessService.Instance.GetAna1Sp(uiModel.SelectedTube);
            uiModel.TemperIntSp = uiModel.TemperInt;
            uiModel.PaddlePosSp = ProcessService.Instance.GetPaddlePosSp(uiModel.SelectedTube);
            uiModel.EditPaddleSpeedSp = uiModel.PaddleSpeedSp;
            uiModel.EvSp = ProcessService.Instance.GetEv(uiModel.SelectedTube);
            uiModel.DoSp = uiModel.DoValue;
        }

        public void UpdateMonitorModel()
        {
            TubeMonitorViewModel uiModel = mPage.PageModel;
            uiModel.ProcessStatus = ProcessService.Instance.GetProcessStatus(uiModel.SelectedTube);
            uiModel.ProcessName = ProcessService.Instance.GetProcessName(uiModel.SelectedTube);
            uiModel.StepName = ProcessService.Instance.GetStepName(uiModel.SelectedTube);
            uiModel.StepTime = ProcessService.Instance.GetStepEscapedTime(uiModel.SelectedTube);
            //uiModel.Gas1Sp = ProcessService.Instance.GetGas1Sp(uiModel.SelectedTube).ToString();
            uiModel.Gas1CurMeas = ProcessService.Instance.GetGas1Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetGas1Value(uiModel.SelectedTube);
            uiModel.Gas2CurMeas = ProcessService.Instance.GetGas2Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetGas2Value(uiModel.SelectedTube);
            uiModel.Gas5CurMeas = ProcessService.Instance.GetGas5Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetGas5Value(uiModel.SelectedTube);
            uiModel.Gas6CurMeas = ProcessService.Instance.GetGas6Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetGas6Value(uiModel.SelectedTube);
            uiModel.Gas8CurMeas = ProcessService.Instance.GetGas8Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetGas8Value(uiModel.SelectedTube);
            uiModel.Ana1CurMeas = ProcessService.Instance.GetAna1Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetAna1Value(uiModel.SelectedTube);
            uiModel.Ana3CurMeas = ProcessService.Instance.GetAna1Value(uiModel.SelectedTube).ToString();
            uiModel.Ana4CurMeas = ProcessService.Instance.GetAna1Value(uiModel.SelectedTube).ToString();

            if (ProcessService.Instance.GetTemperInt(uiModel.SelectedTube))
            {
                uiModel.Temper1IntValue = ProcessService.Instance.GetTemper1Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper1IntValue(uiModel.SelectedTube);
                uiModel.Temper2IntValue = ProcessService.Instance.GetTemper2Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper2IntValue(uiModel.SelectedTube);
                uiModel.Temper3IntValue = ProcessService.Instance.GetTemper3Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper3IntValue(uiModel.SelectedTube);
                uiModel.Temper4IntValue = ProcessService.Instance.GetTemper4Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper4IntValue(uiModel.SelectedTube);
                uiModel.Temper5IntValue = ProcessService.Instance.GetTemper5Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper5IntValue(uiModel.SelectedTube);
                uiModel.Temper6IntValue = ProcessService.Instance.GetTemper6Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper6IntValue(uiModel.SelectedTube);

                uiModel.Temper1ExtValue = ProcessService.Instance.GetTemper1ExtValue(uiModel.SelectedTube).ToString();
                uiModel.Temper2ExtValue = ProcessService.Instance.GetTemper2ExtValue(uiModel.SelectedTube).ToString();
                uiModel.Temper3ExtValue = ProcessService.Instance.GetTemper3ExtValue(uiModel.SelectedTube).ToString();
                uiModel.Temper4ExtValue = ProcessService.Instance.GetTemper4ExtValue(uiModel.SelectedTube).ToString();
                uiModel.Temper5ExtValue = ProcessService.Instance.GetTemper5ExtValue(uiModel.SelectedTube).ToString();
                uiModel.Temper6ExtValue = ProcessService.Instance.GetTemper6ExtValue(uiModel.SelectedTube).ToString();

            }
            else
            {
                uiModel.Temper1ExtValue = ProcessService.Instance.GetTemper1Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper1ExtValue(uiModel.SelectedTube);
                uiModel.Temper2ExtValue = ProcessService.Instance.GetTemper2Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper2ExtValue(uiModel.SelectedTube);
                uiModel.Temper3ExtValue = ProcessService.Instance.GetTemper3Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper3ExtValue(uiModel.SelectedTube);
                uiModel.Temper4ExtValue = ProcessService.Instance.GetTemper4Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper4ExtValue(uiModel.SelectedTube);
                uiModel.Temper5ExtValue = ProcessService.Instance.GetTemper5Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper5ExtValue(uiModel.SelectedTube);
                uiModel.Temper6ExtValue = ProcessService.Instance.GetTemper6Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetTemper6ExtValue(uiModel.SelectedTube);

                uiModel.Temper1IntValue = ProcessService.Instance.GetTemper1IntValue(uiModel.SelectedTube).ToString();
                uiModel.Temper2IntValue = ProcessService.Instance.GetTemper2IntValue(uiModel.SelectedTube).ToString();
                uiModel.Temper3IntValue = ProcessService.Instance.GetTemper3IntValue(uiModel.SelectedTube).ToString();
                uiModel.Temper4IntValue = ProcessService.Instance.GetTemper4IntValue(uiModel.SelectedTube).ToString();
                uiModel.Temper5IntValue = ProcessService.Instance.GetTemper5IntValue(uiModel.SelectedTube).ToString();
                //uiModel.Temper6IntValue = ProcessService.Instance.GetTemper6IntValue(uiModel.SelectedTube).ToString();
                uiModel.Temper6IntValue = ProcessService.Instance.GetTemper6ExtValue(uiModel.SelectedTube).ToString();
            }
            uiModel.Temper7ExtValue = ProcessService.Instance.GetTemper6ExtValue(uiModel.SelectedTube).ToString();
            uiModel.Temper1HeatPower = ProcessService.Instance.GetTemper1HeatPower(uiModel.SelectedTube).ToString();
            uiModel.Temper2HeatPower = ProcessService.Instance.GetTemper2HeatPower(uiModel.SelectedTube).ToString();
            uiModel.Temper3HeatPower = ProcessService.Instance.GetTemper3HeatPower(uiModel.SelectedTube).ToString();
            uiModel.Temper4HeatPower = ProcessService.Instance.GetTemper4HeatPower(uiModel.SelectedTube).ToString();
            uiModel.Temper5HeatPower = ProcessService.Instance.GetTemper5HeatPower(uiModel.SelectedTube).ToString();
            uiModel.Temper6HeatPower = ProcessService.Instance.GetTemper6HeatPower(uiModel.SelectedTube).ToString();

            uiModel.PaddleSpeedSp = ProcessService.Instance.GetPaddleSpeedSp(uiModel.SelectedTube);
            uiModel.PaddlePosAct = ProcessService.Instance.GetPaddlePosSp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetPaddlePosAct(uiModel.SelectedTube);
            uiModel.PaddlePosActI = ProcessService.Instance.GetPaddlePosAct(uiModel.SelectedTube);
            uiModel.EvValue = ProcessService.Instance.GetEv(uiModel.SelectedTube);
            uiModel.DiValue = ProcessService.Instance.GetDi(uiModel.SelectedTube);
            uiModel.DoValue = ProcessService.Instance.GetDo(uiModel.SelectedTube);
        }

        public ProcessStatus GetStatus(byte tubeIndex)
        {
            return ProcessService.Instance.GetStatus(tubeIndex);
        }

        public string GetProcessName(byte tubeIndex)
        {
            return ProcessService.Instance.GetProcessName(tubeIndex);
        }

        public string GetProcessStatus(byte tubeIndex)
        {
            return ProcessService.Instance.GetProcessStatus(tubeIndex);
        }

        public int GetProcessTime(byte tubeIndex)
        {
            return ProcessService.Instance.GetProcessTime(tubeIndex);
        }

        public int GetStepNum(byte tubeIndex)
        {
            return ProcessService.Instance.GetStepNum(tubeIndex);
        }

        public int GetStepEscapedTime(byte tubeIndex)
        {
            return ProcessService.Instance.GetStepEscapedTime(tubeIndex);
        }

        public int GetProcessEscapedTime(byte tubeIndex)
        {
            return ProcessService.Instance.GetProcessTime(tubeIndex);
        }

        public int GetRemainingTime(byte tubeIndex)
        {
            return ProcessService.Instance.GetRemainingTime(tubeIndex);
        }

        public void CommitChanges(byte tubeIndex, ProcessService.OnCommitEditSetpointComplete callback)
        {
            //add validation
            UpdateChangedItems();
            if (mCommitItems.Count > 0)
            {
                //display the confirmation of changed item
                ConvertEditProcessModel();
                mCommitChangeCompleteCallback = callback;
                ProcessService.Instance.CommitChanges(tubeIndex, OnCommitChangesComplete);
            }
            else
            {
                //display message: nothing is changed
            }

        }

        public void StartProcess(byte tubeIndex, ProcessService.OnStartProcessComplete callback)
        {
            //add validation
            ProcessService.Instance.StartProcess(tubeIndex, callback);
        }

        public void HoldProcess(byte tubeIndex, ProcessService.OnHoldProcessComplete callback)
        {
            //add validation
            ProcessService.Instance.HoldProcess(tubeIndex, callback);
        }

        public void NextProcess(byte tubeIndex, ProcessService.OnHoldProcessComplete callback)
        {
            //add validation
            ProcessService.Instance.NextProcess(tubeIndex, callback);
        }

        public void IdleProcess(byte tubeIndex, ProcessService.OnIdleProcessComplete callback)
        {
            //add validation
            ProcessService.Instance.IdleProcess(tubeIndex, callback);
        }

        public void AbortProcess(byte tubeIndex, ProcessService.OnAbortProcessComplete callback)
        {
            //add validation
            ProcessService.Instance.AbortProcess(tubeIndex, callback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnCommitChangesComplete()
        {
            //save history records
            for (int i = 0; i < mCommitItems.Count; ++i)
            {
                mCommitItems[i].Time = new System.DateTime();
            }
            HistoryService.Instance.SaveHistoryItems(mCommitItems);
            mCommitChangeCompleteCallback();
            mCommitItems.Clear();
        }

        private void UpdateChangedItems()
        {
            mCommitItems.Clear();
            TubeMonitorViewModel monitorPageModel = mPage.PageModel;
            EditProcess process = ProcessService.Instance.GetEditProcess();
            if (process.EditGas1Sp != monitorPageModel.Gas1Sp)
            {
                History history = new History("Gas1Sp_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditGas1Sp;
                history.NewValue = monitorPageModel.Gas1Sp;
                mCommitItems.Add(history);
            }
            if (process.EditGas2Sp != monitorPageModel.Gas2Sp)
            {
                History history = new History("Gas2Sp_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditGas2Sp;
                history.NewValue = monitorPageModel.Gas2Sp;
                mCommitItems.Add(history);
            }
            if (process.EditGas5Sp != monitorPageModel.Gas5Sp)
            {
                History history = new History("Gas5Sp_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditGas5Sp;
                history.NewValue = monitorPageModel.Gas5Sp;
                mCommitItems.Add(history);
            }
            if (process.EditGas6Sp != monitorPageModel.Gas6Sp)
            {
                History history = new History("Gas6Sp_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditGas6Sp;
                history.NewValue = monitorPageModel.Gas6Sp;
                mCommitItems.Add(history);
            }
            if (process.EditGas8Sp != monitorPageModel.Gas8Sp)
            {
                History history = new History("Gas8Sp_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditGas8Sp;
                history.NewValue = monitorPageModel.Gas8Sp;
                mCommitItems.Add(history);
            }
            if (process.EditAna1Sp != monitorPageModel.Ana1Sp)
            {
                History history = new History("Ana1Sp_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditAna1Sp;
                history.NewValue = monitorPageModel.Ana1Sp;
                mCommitItems.Add(history);
            }
            if (process.EditTemperIntSp != monitorPageModel.TemperIntSp)
            {
                History history = new History("TemperInt_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditTemperIntSp;
                history.NewValue = monitorPageModel.TemperIntSp;
                mCommitItems.Add(history);
            }
            if (process.EditTemper1Sp != monitorPageModel.Temper1Sp)
            {
                History history = new History("Temper1_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditTemper1Sp;
                history.NewValue = monitorPageModel.Temper1Sp;
                mCommitItems.Add(history);
            }
            if (process.EditTemper2Sp != monitorPageModel.Temper2Sp)
            {
                History history = new History("Temper2_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditTemper2Sp;
                history.NewValue = monitorPageModel.Temper2Sp;
                mCommitItems.Add(history);
            }
            if (process.EditTemper3Sp != monitorPageModel.Temper3Sp)
            {
                History history = new History("Temper3_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditTemper3Sp;
                history.NewValue = monitorPageModel.Temper3Sp;
                mCommitItems.Add(history);
            }
            if (process.EditTemper4Sp != monitorPageModel.Temper4Sp)
            {
                History history = new History("Temper4_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditTemper4Sp;
                history.NewValue = monitorPageModel.Temper4Sp;
                mCommitItems.Add(history);
            }
            if (process.EditTemper5Sp != monitorPageModel.Temper5Sp)
            {
                History history = new History("Temper5_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditTemper5Sp;
                history.NewValue = monitorPageModel.Temper5Sp;
                mCommitItems.Add(history);
            }
            if (process.EditTemper6Sp != monitorPageModel.Temper6Sp)
            {
                History history = new History("Temper6_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditTemper6Sp;
                history.NewValue = monitorPageModel.Temper6Sp;
                mCommitItems.Add(history);
            }
            if (process.EditPaddlePosSp != monitorPageModel.PaddlePosSp)
            {
                History history = new History("PaddlePosition_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditPaddlePosSp;
                history.NewValue = monitorPageModel.PaddlePosSp;
                mCommitItems.Add(history);
            }
            if (process.EditPaddleSpeedSp != monitorPageModel.EditPaddleSpeedSp)
            {
                History history = new History("PaddleSpeed_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditPaddleSpeedSp;
                history.NewValue = monitorPageModel.EditPaddleSpeedSp;
                mCommitItems.Add(history);
            }
            if (process.EditEvSp != monitorPageModel.EvSp)
            {
                History history = new History("EV_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditEvSp;
                history.NewValue = monitorPageModel.EvSp;
                mCommitItems.Add(history);
            }
            if (process.EditDoSp != monitorPageModel.DoSp)
            {
                History history = new History("DO_T" + monitorPageModel.SelectedTube);
                history.OldValue = process.EditDoSp;
                history.NewValue = monitorPageModel.DoSp;
                mCommitItems.Add(history);
            }
        }

        private void ConvertEditProcessModel()
        {
            TubeMonitorViewModel monitorPageModel = mPage.PageModel;
            EditProcess process = ProcessService.Instance.GetEditProcess();
            process.EditGas1Sp = monitorPageModel.Gas1Sp;
            process.EditGas2Sp = monitorPageModel.Gas2Sp;
            process.EditGas5Sp = monitorPageModel.Gas5Sp;
            process.EditGas6Sp = monitorPageModel.Gas6Sp;
            process.EditGas8Sp = monitorPageModel.Gas8Sp;
            process.EditAna1Sp = monitorPageModel.Ana1Sp;
            process.EditTemperIntSp = monitorPageModel.TemperIntSp;
            process.EditTemper1Sp = monitorPageModel.Temper1Sp;
            process.EditTemper2Sp = monitorPageModel.Temper2Sp;
            process.EditTemper3Sp = monitorPageModel.Temper3Sp;
            process.EditTemper4Sp = monitorPageModel.Temper4Sp;
            process.EditTemper5Sp = monitorPageModel.Temper5Sp;
            process.EditTemper6Sp = monitorPageModel.Temper6Sp;
            process.EditPaddlePosSp = monitorPageModel.PaddlePosSp;
            process.EditPaddleSpeedSp = monitorPageModel.EditPaddleSpeedSp;
            process.EditEvSp = monitorPageModel.EvSp;
            process.EditDoSp = monitorPageModel.DoSp;
        }

        private void UpdateLabels()
        {
            TubeMonitorViewModel viewModel = mPage.PageModel;
            Settings settings = SettingsService.Instance.GetSettings();
            viewModel.Gas1Name = settings.Gas1Name;
            viewModel.Gas2Name = settings.Gas2Name;
            viewModel.Gas5Name = settings.Gas5Name;
            viewModel.Gas6Name = settings.Gas6Name;
            viewModel.Gas8Name = settings.Gas8Name;
            viewModel.Ana1Name = settings.Ana1Name;
            viewModel.Ana3Name = settings.Ana3Name;
            viewModel.Ana4Name = settings.Ana4Name;

            string[] evNames = new string[32];
            Array.Copy(settings.EvNames, 0, evNames, 0, 32);
            viewModel.EvNames = evNames;

            string[] diNames = new string[32];
            Array.Copy(settings.DiNames, 0, diNames, 0, 32);
            viewModel.DiNames = diNames;

            string[] doNames = new string[16];
            Array.Copy(settings.DoNames, 0, doNames, 0, 16);
            viewModel.DoNames = doNames;
        }
    }
}
