using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.ui.view;
using Demo.service;
using Demo.ui.model;

namespace Demo.controller
{
    class TubeInfoItemController
    {
        private TubeInfoItem mPage;
        public TubeInfoItemController(TubeInfoItem page)
        {
            mPage = page;
        }

        public ProcessStatus GetStatus(byte tubeIndex)
        {
            //add validation
            return ProcessService.Instance.GetStatus(tubeIndex);
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

        public void UpdateTubeInfoItemModel(TubeInfoItemModel uiModel)
        {
            uiModel.ProcessStatus = ProcessService.Instance.GetProcessStatus(uiModel.TubeIndex);
            uiModel.ProcessName = ProcessService.Instance.GetProcessName(uiModel.TubeIndex);
            uiModel.Gas1CurMeas = ProcessService.Instance.GetGas1Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetGas1Value(uiModel.TubeIndex);
            uiModel.Gas2CurMeas = ProcessService.Instance.GetGas2Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetGas2Value(uiModel.TubeIndex);
            uiModel.Gas5CurMeas = ProcessService.Instance.GetGas5Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetGas5Value(uiModel.TubeIndex);
            uiModel.Gas6CurMeas = ProcessService.Instance.GetGas6Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetGas6Value(uiModel.TubeIndex);
            uiModel.Gas8CurMeas = ProcessService.Instance.GetGas8Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetGas8Value(uiModel.TubeIndex);
            uiModel.Ana1CurMeas = ProcessService.Instance.GetAna1Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetAna1Value(uiModel.TubeIndex);
            uiModel.Ana3CurMeas = ProcessService.Instance.GetAna3Value(uiModel.TubeIndex).ToString();
            uiModel.Ana4CurMeas = ProcessService.Instance.GetAna4Value(uiModel.TubeIndex).ToString();
            if (ProcessService.Instance.GetTemperInt(uiModel.TubeIndex))
            {
                uiModel.Temper1IntValue = ProcessService.Instance.GetTemper1Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper1IntValue(uiModel.TubeIndex);
                uiModel.Temper2IntValue = ProcessService.Instance.GetTemper2Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper2IntValue(uiModel.TubeIndex);
                uiModel.Temper3IntValue = ProcessService.Instance.GetTemper3Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper3IntValue(uiModel.TubeIndex);
                uiModel.Temper4IntValue = ProcessService.Instance.GetTemper4Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper4IntValue(uiModel.TubeIndex);
                uiModel.Temper5IntValue = ProcessService.Instance.GetTemper5Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper5IntValue(uiModel.TubeIndex);
                uiModel.Temper6IntValue = ProcessService.Instance.GetTemper6Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper6IntValue(uiModel.TubeIndex);

                uiModel.Temper1ExtValue = ProcessService.Instance.GetTemper1ExtValue(uiModel.TubeIndex).ToString();
                uiModel.Temper2ExtValue = ProcessService.Instance.GetTemper2ExtValue(uiModel.TubeIndex).ToString();
                uiModel.Temper3ExtValue = ProcessService.Instance.GetTemper3ExtValue(uiModel.TubeIndex).ToString();
                uiModel.Temper4ExtValue = ProcessService.Instance.GetTemper4ExtValue(uiModel.TubeIndex).ToString();
                uiModel.Temper5ExtValue = ProcessService.Instance.GetTemper5ExtValue(uiModel.TubeIndex).ToString();
                uiModel.Temper6ExtValue = ProcessService.Instance.GetTemper6ExtValue(uiModel.TubeIndex).ToString();

            }
            else
            {
                uiModel.Temper1ExtValue = ProcessService.Instance.GetTemper1Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper1ExtValue(uiModel.TubeIndex);
                uiModel.Temper2ExtValue = ProcessService.Instance.GetTemper2Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper2ExtValue(uiModel.TubeIndex);
                uiModel.Temper3ExtValue = ProcessService.Instance.GetTemper3Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper3ExtValue(uiModel.TubeIndex);
                uiModel.Temper4ExtValue = ProcessService.Instance.GetTemper4Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper4ExtValue(uiModel.TubeIndex);
                uiModel.Temper5ExtValue = ProcessService.Instance.GetTemper5Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper5ExtValue(uiModel.TubeIndex);
                uiModel.Temper6ExtValue = ProcessService.Instance.GetTemper6Sp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetTemper6ExtValue(uiModel.TubeIndex);

                uiModel.Temper1IntValue = ProcessService.Instance.GetTemper1IntValue(uiModel.TubeIndex).ToString();
                uiModel.Temper2IntValue = ProcessService.Instance.GetTemper2IntValue(uiModel.TubeIndex).ToString();
                uiModel.Temper3IntValue = ProcessService.Instance.GetTemper3IntValue(uiModel.TubeIndex).ToString();
                uiModel.Temper4IntValue = ProcessService.Instance.GetTemper4IntValue(uiModel.TubeIndex).ToString();
                uiModel.Temper5IntValue = ProcessService.Instance.GetTemper5IntValue(uiModel.TubeIndex).ToString();
                uiModel.Temper6IntValue = ProcessService.Instance.GetTemper6IntValue(uiModel.TubeIndex).ToString();
            }
            //uiModel.PaddleSpeedSp = ProcessService.Instance.GetPaddleSpeedSp(uiModel.TubeIndex);
            //uiModel.PaddlePosAct = ProcessService.Instance.GetPaddlePosSp(uiModel.TubeIndex) + "/" + ProcessService.Instance.GetPaddlePosAct(uiModel.TubeIndex);
            uiModel.EvValue = ProcessService.Instance.GetEv(uiModel.TubeIndex);
            //uiModel.DiValue = ProcessService.Instance.GetDi(uiModel.TubeIndex);
            //uiModel.DoValue = ProcessService.Instance.GetDo(uiModel.TubeIndex);
        }
    }
}
