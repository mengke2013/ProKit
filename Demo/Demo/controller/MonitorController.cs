using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.ui.model;
using Demo.service;

namespace Demo.controller
{
    class MonitorController
    {
        public MonitorController()
        {
        }

        public void UpdateMonitorModel(TubeMonitorPageModel uiModel)
        {
            uiModel.ProcessStatus = ProcessService.Instance.GetProcessStatus(uiModel.SelectedTube);
            uiModel.ProcessName = ProcessService.Instance.GetProcessName(uiModel.SelectedTube);
        }

        public void StartProcess(byte tubeIndex, ProcessService.OnStartProcessComplete callback)
        {
            //add validation
            ProcessService.Instance.StartProcess(tubeIndex, callback);
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
    }
}
