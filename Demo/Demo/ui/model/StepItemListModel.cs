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
    public class StepItemListModel 
    {
        private List<StepItemModel> mStepItemModels;

        public StepItemListModel()
        {
            mStepItemModels = new List<StepItemModel>();
            for (byte i = 0; i < 64; ++i)
            {
                StepItemModel stepListItemModel = new StepItemModel((byte)(i + 1));
                stepListItemModel.RowIndex = i;
                mStepItemModels.Add(stepListItemModel);
            }
        }

        public void LoadData(byte selectedTube)
        {
            //LoadTestData();
        }

        public void UpdateStepListItems(byte[] stepListBytes)
        {
            for (int i = 0; i < 64; ++i)
            {
                mStepItemModels[i].StepName = Encoding.ASCII.GetString(stepListBytes, 0+i*64, 32+i*64).TrimEnd('\0');
                mStepItemModels[i].StepType = (sbyte)stepListBytes[36 + i * 64];
                mStepItemModels[i].StepTime = BitConverter.ToInt32(stepListBytes, 32 + i * 64);
            }
        }


        public List<StepItemModel> StepListItems
        {
            get
            {
                return mStepItemModels;
            }
        }

    }
}
