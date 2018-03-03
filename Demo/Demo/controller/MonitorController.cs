using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.ui;
using Demo.ui.model;
using Demo.service;
using Demo.model;

namespace Demo.controller
{
    class MonitorController
    {

        private TubeMonitorPage mPage;
        public MonitorController(TubeMonitorPage page)
        {
            mPage = page;
        }

        public void UpdateMonitorModel(TubeMonitorPageModel uiModel)
        {
            uiModel.ProcessStatus = ProcessService.Instance.GetProcessStatus(uiModel.SelectedTube);
            uiModel.ProcessName = ProcessService.Instance.GetProcessName(uiModel.SelectedTube);
            uiModel.StepName = ProcessService.Instance.GetStepName(uiModel.SelectedTube); 
            uiModel.StepTime = ProcessService.Instance.GetStepEscapedTime(uiModel.SelectedTube); 
            //uiModel.Gas1Sp = ProcessService.Instance.GetGas1Sp(uiModel.SelectedTube).ToString();
            uiModel.Gas1CurMeas = ProcessService.Instance.GetGas1Sp(uiModel.SelectedTube) +"/"+ProcessService.Instance.GetGas1Value(uiModel.SelectedTube);
            uiModel.Gas2CurMeas = ProcessService.Instance.GetGas2Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetGas2Value(uiModel.SelectedTube);
            uiModel.Gas5CurMeas = ProcessService.Instance.GetGas5Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetGas5Value(uiModel.SelectedTube);
            uiModel.Gas6CurMeas = ProcessService.Instance.GetGas6Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetGas6Value(uiModel.SelectedTube);
            uiModel.Gas8CurMeas = ProcessService.Instance.GetGas8Sp(uiModel.SelectedTube) + "/" + ProcessService.Instance.GetGas8Value(uiModel.SelectedTube);
            if (uiModel.TemperInt)
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
                uiModel.Temper6IntValue = ProcessService.Instance.GetTemper6IntValue(uiModel.SelectedTube).ToString();
            }
            
        }

        public void CommitChanges(byte tubeIndex, ProcessService.OnCommitEditSetpointComplete callback)
        {
            //add validation
            ConvertEditProcessModel(ProcessService.Instance.GetEditProcess(), mPage.GetPageModel());
            ProcessService.Instance.CommitChanges(tubeIndex, callback);
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

        public ProcessStatus GetStatus(byte tubeIndex)
        {
            //add validation
            return ProcessService.Instance.GetStatus(tubeIndex);
        }

        public string GetProcessName(byte tubeIndex)
        {
            //add validation
            return ProcessService.Instance.GetProcessName(tubeIndex);
        }

        public string GetProcessStatus(byte tubeIndex)
        {
            //add validation
            return ProcessService.Instance.GetProcessStatus(tubeIndex);
        }

        public int GetProcessTime(byte tubeIndex)
        {
            //add validation
            return ProcessService.Instance.GetProcessTime(tubeIndex);
        }

        public int GetStepNum(byte tubeIndex)
        {
            //add validation
            return ProcessService.Instance.GetStepNum(tubeIndex);
        }

        public int GetStepEscapedTime(byte tubeIndex)
        {
            //add validation
            return ProcessService.Instance.GetStepEscapedTime(tubeIndex);
        }

        public int GetProcessEscapedTime(byte tubeIndex)
        {
            //add validation
            return ProcessService.Instance.GetProcessTime(tubeIndex);
        }

        public int GetRemainingTime(byte tubeIndex)
        {
            //add validation
            return ProcessService.Instance.GetRemainingTime(tubeIndex);
        }

        private void ConvertEditProcessModel(EditProcess process, TubeMonitorPageModel monitorPageModel)
        {
            process.EditGas1Sp = monitorPageModel.Gas1Sp;
            process.EditGas2Sp = monitorPageModel.Gas2Sp;
            process.EditGas5Sp = monitorPageModel.Gas5Sp;
            process.EditGas6Sp = monitorPageModel.Gas6Sp;
            process.EditGas8Sp = monitorPageModel.Gas8Sp;
            process.EditTemper1Sp = monitorPageModel.Temper1Sp;
            process.EditTemper2Sp = monitorPageModel.Temper2Sp;
            process.EditTemper3Sp = monitorPageModel.Temper3Sp;
            process.EditTemper4Sp = monitorPageModel.Temper4Sp;
            process.EditTemper5Sp = monitorPageModel.Temper5Sp;
            process.EditTemper6Sp = monitorPageModel.Temper6Sp;
        }
    }
}
