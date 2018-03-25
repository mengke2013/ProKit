using Demo.model;

using log4net;
using System.Collections.Generic;

namespace Demo.service
{
    public class HistoryService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static HistoryService instance;

        private byte mTubeIndex;

        private HistoryService()
        {
            mTubeIndex = 0;
  
        }

        public static HistoryService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HistoryService();
                }
                return instance;
            }
        }

        public void SaveHistoryItems(List<History> historyItems)
        {
            //save the historys records into database
            for (int i = 0; i < historyItems.Count; ++i)
            {
                log.Info(historyItems[i].Name + "," + historyItems[i].OldValue + "," + historyItems[i].NewValue + historyItems[i].Time + "," + UserService.Instance.GetLoginUser());
            }
        }
    }
}

