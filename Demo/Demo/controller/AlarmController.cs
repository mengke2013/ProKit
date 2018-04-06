using Demo.ui.view;
using Demo.ui.model;
using System.Collections.Generic;
using Demo.model;
using Demo.service;
using System;

namespace Demo.controller
{
    class AlarmController
    {
        TubeAlarmPage mPage;

        public AlarmController(TubeAlarmPage page)
        {
            mPage = page;
        }

        public void UpdateAlarmItems(byte selectedTube)
        {
            List<TubeAlarmItemModel> alarmItemModels = new List<TubeAlarmItemModel>();
            List<Alarm> alarms = AlarmService.Instance.LoadAlarms(selectedTube);
            for (int i = 0; i < alarms.Count; ++i)
            {
                alarmItemModels.Add(new TubeAlarmItemModel(alarms[i].ID, alarms[i].ErrorCode, alarms[i].Description));
            }
            
            mPage.AlarmView.dataGrid.DataContext = alarmItemModels;
        }

        public void AcknowledgeAlarms(byte selectedTube)
        {
            AlarmService.Instance.AcknowledgeAlarms(selectedTube);
        }
    }
}
