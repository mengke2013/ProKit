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
            Random r = new Random();
            int v = r.Next(0, alarms.Count);
            for (int i = 0; i < v; ++i)
            {
                alarmItemModels.Add(new TubeAlarmItemModel(i+1, "test" + (i+1)));
            }
            
            mPage.AlarmView.dataGrid.DataContext = alarmItemModels;
        }

        public void AcknowledgeAlarms(byte selectedTube)
        {

        }
    }
}
